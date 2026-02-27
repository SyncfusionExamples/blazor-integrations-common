window.BoldReports = {
    renderViewer: function (elementId, options) {
        $("#" + elementId).boldReportViewer({
            reportServiceUrl: options.serviceUrl,
            processingMode: "Local",
            reportPath: options.reportPath
        });
    },
    postDataAndRender: async function (orders, serviceUrl, reportPath, elementId) {
        try {
            const resp = await fetch('/api/BoldReportsAPI/SetReportData', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ dataSources: orders })
            });
            if (!resp.ok) throw new Error('POST failed: ' + resp.status);

            $("#" + elementId).empty();
            $("#" + elementId).boldReportViewer({
                reportServiceUrl: serviceUrl,
                processingMode: "Local",
                reportPath: reportPath
            });
        } catch (e) {
          throw e;
        }
    }
};
