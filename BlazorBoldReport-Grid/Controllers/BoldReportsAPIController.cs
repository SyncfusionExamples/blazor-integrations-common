using System.Collections.Generic;
using System.Data;
using System.IO;
using BoldReports.Web; // Added for ReportDataSource & ReportDataSourceCollection
using BoldReports.Web.ReportViewer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

[ApiController]
[Route("api/[controller]/[action]")]
public class BoldReportsAPIController : Controller, IReportController
{
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private static List<Order>? _sharedOrders;
    private static DataSet? _reportDataSet;

    public BoldReportsAPIController(IMemoryCache cache, IWebHostEnvironment env)
    {
        _cache = cache;
        _env = env;
    }

    [HttpPost]
    public object PostReportAction([FromBody] Dictionary<string, object> json)
        => ReportHelper.ProcessReport(json, this, _cache);

    [HttpPost]
    public object PostFormReportAction()
        => ReportHelper.ProcessReport(null, this, _cache);

    [HttpGet]
    public object GetResource([FromQuery] ReportResource resource)
        => ReportHelper.GetResource(resource, this, _cache);

    [HttpPost]
    public IActionResult SetReportData([FromBody] ReportDataModel dataModel)
    {
        try
        {
            if (dataModel?.DataSources != null && dataModel.DataSources.Count > 0)
            {
                _sharedOrders = dataModel.DataSources;

                // Convert List<Order> to DataSet with DataTable
                DataSet ds = new();
                DataTable dt = new("OrdersDataSet");

                // Add columns matching the RDLC report
                dt.Columns.Add("OrderID", typeof(int));
                dt.Columns.Add("CustomerID", typeof(string));
                dt.Columns.Add("OrderDate", typeof(DateTime));
                dt.Columns.Add("Freight", typeof(double));

                // Add rows from orders
                foreach (var order in _sharedOrders)
                {
                    dt.Rows.Add(order.OrderID, order.CustomerID, order.OrderDate, order.Freight);
                }

                ds.Tables.Add(dt);
                _reportDataSet = ds;
                _cache.Set("ReportDataSet", ds, TimeSpan.FromMinutes(10));

                return Ok(new { success = true, message = "Data set successfully" });
            }
            return BadRequest(new { success = false, message = "No data provided" });
        }
        catch (Exception ex)
        {
           return BadRequest(new { success = false, message = ex.Message });
        }
    }

    [NonAction]
    public void OnInitReportOptions(ReportViewerOptions options)
    {
        try
        {
            options.ReportModel.ProcessingMode = ProcessingMode.Local;

            var path = Path.Combine(_env.WebRootPath, "Reports", "Orders.rdlc");
            
            if (!System.IO.File.Exists(path))
            {
                throw new FileNotFoundException($"RDLC file not found at: {path}");
            }

            var stream = new MemoryStream();
            using (var fs = System.IO.File.OpenRead(path))
            {
                fs.CopyTo(stream);
            }
            stream.Position = 0;

            options.ReportModel.Stream = stream;
            options.ReportModel.ReportPath = "Orders.rdlc";
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"[OnInitReportOptions] Error: {ex.Message}");
        }
    }

    [NonAction]
    public void OnReportLoaded(ReportViewerOptions options)
    {
        try
        {
            // Retrieve the DataSet from cache or static variable
            DataSet? ds = null;
            if (_cache.TryGetValue("ReportDataSet", out object? cachedData) && cachedData is DataSet cachedDs)
            {
                ds = cachedDs;
            }
            else if (_reportDataSet != null)
            {
                ds = _reportDataSet;
            }

            if (ds != null && ds.Tables.Contains("OrdersDataSet"))
            {
                var table = ds.Tables["OrdersDataSet"];
                options.ReportModel.DataSources = new ReportDataSourceCollection
                {
                    new ReportDataSource("OrdersDataSet", table)
                };
            }
            else
            {
                System.Console.WriteLine("[OnReportLoaded] No DataSet/Table named 'OrdersDataSet' found");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"[OnReportLoaded] Error: {ex.Message}");
        }
    }

    public class Order
    {
        public int? OrderID { get; set; }
        public string? CustomerID { get; set; }
        public DateTime? OrderDate { get; set; }
        public double? Freight { get; set; }
    }

    public class ReportDataModel
    {
        public List<Order> DataSources { get; set; } = [];
    }
}
