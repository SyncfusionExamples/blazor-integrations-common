namespace OrderManagementDashboard.Services
{
    public static class SharedDataConstants
    {
        public static readonly string[] FirstNames = new[]
        {
            "John", "Jane", "Michael", "Sarah", "David", "Emily", "Robert", "Lisa",
            "William", "Jennifer", "James", "Mary", "Christopher", "Patricia", "Daniel",
            "Linda", "Matthew", "Barbara", "Anthony", "Susan", "Mark", "Jessica",
            "Donald", "Karen", "Steven", "Nancy", "Paul", "Betty", "Andrew", "Helen"
        };

        public static readonly string[] LastNames = new[]
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis",
            "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson",
            "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Thompson", "White",
            "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker"
        };

        public static string GenerateEmail(string firstName, string lastName, int id)
        {
            var domains = new[] { "gmail.com", "yahoo.com", "outlook.com", "company.com", "email.com" };
            var random = new Random(id);
            var domain = domains[random.Next(domains.Length)];
            return $"{firstName.ToLower()}.{lastName.ToLower()}{id % 100}@{domain}";
        }
    }
}