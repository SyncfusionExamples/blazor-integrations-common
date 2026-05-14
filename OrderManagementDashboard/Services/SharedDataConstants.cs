namespace OrderManagementDashboard.Services
{
    public static class SharedDataConstants
    {
        public static readonly string[] FirstNames = new[] 
        { 
            "John", "Emma", "Michael", "Sophia", "William", "Olivia", "James", "Ava", 
            "Robert", "Isabella", "David", "Mia", "Richard", "Charlotte", "Joseph", "Amelia", 
            "Thomas", "Harper", "Daniel", "Evelyn", "Matthew", "Abigail", "Christopher", "Emily", 
            "Andrew", "Elizabeth", "Joshua", "Sofia", "Kevin", "Avery", "Brian", "Ella", 
            "George", "Scarlett", "Timothy", "Grace", "Ronald", "Chloe", "Jason", "Victoria", 
            "Jeffrey", "Riley", "Ryan", "Aria", "Jacob", "Lily", "Gary", "Aubrey", "Nicholas", "Zoey"
        };
        
        public static readonly string[] LastNames = new[] 
        { 
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis",
            "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", 
            "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Thompson", "White", 
            "Harris", "Clark", "Lewis", "Robinson", "Walker", "Hall", "Allen"
        };
        
        public static readonly string[] EmailDomains = new[] 
        { 
            "@gmail.com", "@yahoo.com", "@outlook.com", "@hotmail.com", "@email.com" 
        };
        
        public static string GenerateEmail(string firstName, string lastName, int seed)
        {
            var domain = EmailDomains[seed % EmailDomains.Length];
            return $"{firstName.ToLower()}.{lastName.ToLower()}{seed % 1000}{domain}";
        }
    }
}