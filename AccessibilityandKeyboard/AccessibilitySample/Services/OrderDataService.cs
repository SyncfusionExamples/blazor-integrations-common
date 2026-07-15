using AccessibilitySample.Models;

namespace AccessibilitySample.Services;

public class OrderDataService
{
    public List<Order> GetOrders() => GenerateOrders();

    private static List<Order> GenerateOrders()
    {
        var template = new (string CustomerID, int EmployeeID, DateTime OrderDate,
                            string ShipName, string ShipCity, string ShipAddress,
                            string ShipRegion, string ShipPostalCode, string ShipCountry,
                            double Freight, bool Verified)[]
        {
            ("VINET", 5, new DateTime(1996, 7, 4),  "Vins et alcools Chevalier",      "Reims",          "59 rue de l Abbaye",                                "CJ",      "51100",     "France",       32.38,  true),
            ("TOMSP", 6, new DateTime(1996, 7, 5),  "Toms Spezialitäten",            "Münster",        "Luisenstr. 48",                                     "CJ",      "44087",     "Germany",      11.61,  false),
            ("HANAR", 2, new DateTime(1996, 7, 8),  "Hanari Carnes",                 "Rio de Janeiro", "Rua do Paço, 67",                                   "RJ",      "05454-876", "Brazil",       65.83,  true),
            ("VICTE", 3, new DateTime(1996, 7, 8),  "Victuailles en stock",          "Lyon",           "2, rue du Commerce",                                "CJ",      "69004",     "France",       41.34,  true),
            ("SUPRD", 4, new DateTime(1996, 7, 9),  "Suprêmes délices",              "Charleroi",      "Boulevard Tirou, 255",                              "CJ",      "B-6000",    "Belgium",      51.3,   true),
            ("HANAR", 3, new DateTime(1996, 7, 10), "Hanari Carnes",                 "Rio de Janeiro", "Rua do Paço, 67",                                   "RJ",      "05454-876", "Brazil",       58.17,  true),
            ("CHOPS", 5, new DateTime(1996, 7, 11), "Chop-suey Chinese",             "Bern",           "Hauptstr. 31",                                      "CJ",      "3012",      "Switzerland",  22.98,  false),
            ("RICSU", 9, new DateTime(1996, 7, 12), "Richter Supermarkt",            "Genève",         "Starenweg 5",                                       "CJ",      "1204",      "Switzerland",  148.33, true),
            ("WELLI", 3, new DateTime(1996, 7, 15), "Wellington Importadora",        "Resende",        "Rua do Mercado, 12",                                "SP",      "08737-363", "Brazil",       13.97,  false),
            ("HILAA", 4, new DateTime(1996, 7, 16), "HILARION-Abastos",              "San Cristóbal",  "Carrera 22 con Ave. Carlos Soublette #8-35",        "Táchira", "5022",      "Venezuela",    81.91,  true),
            ("ERNSH", 1, new DateTime(1996, 7, 17), "Ernst Handel",                  "Graz",           "Kirchgasse 6",                                      "CJ",      "8010",      "Austria",      140.51, true),
            ("CENTC", 7, new DateTime(1996, 7, 18), "Centro comercial Moctezuma",    "México D.F.",    "Sierras de Granada 9993",                           "CJ",      "05022",     "Mexico",       3.25,   false),
            ("OTTIK", 4, new DateTime(1996, 7, 19), "Ottilies Käseladen",            "Köln",           "Mehrheimerstr. 369",                                "CJ",      "50739",     "Germany",      55.09,  true),
            ("QUEDE", 2, new DateTime(1996, 7, 19), "Que Delícia",                   "Rio de Janeiro", "Rua da Panificadora, 12",                           "RJ",      "02389-673", "Brazil",       3.05,   false),
            ("RATTC", 8, new DateTime(1996, 7, 22), "Rattlesnake Canyon Grocery",    "Albuquerque",    "2817 Milton Dr.",                                   "NM",      "87110",     "USA",          48.29,  true)
        };

        var orders = new List<Order>(75);
        for (int cycle = 0; cycle < 5; cycle++)
        {
            for (int i = 0; i < template.Length; i++)
            {
                var t = template[i];
                orders.Add(new Order
                {
                    OrderID = 10248 + (cycle * 15) + i,
                    CustomerID = t.CustomerID,
                    EmployeeID = t.EmployeeID,
                    OrderDate = t.OrderDate,
                    ShipName = t.ShipName,
                    ShipCity = t.ShipCity,
                    ShipAddress = t.ShipAddress,
                    ShipRegion = t.ShipRegion,
                    ShipPostalCode = t.ShipPostalCode,
                    ShipCountry = t.ShipCountry,
                    Freight = t.Freight,
                    Verified = t.Verified
                });
            }
        }

        return orders;
    }
}