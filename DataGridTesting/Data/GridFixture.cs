using DataGridTesting.Models;

namespace DataGridTesting.Data;

public class GridFixture
{
    public List<GridRow> GetData() => new()
    {
        new() { Id = 1,  Name = "Alice",   Role = "Engineer",          Department = "Web Development", DateOfJoining = new DateTime(2020, 1, 15) },
        new() { Id = 2,  Name = "Bob",     Role = "HR Specialist",     Department = "HR",            DateOfJoining = new DateTime(2019, 3, 10) },
        new() { Id = 3,  Name = "Carol",   Role = "UI/UX Designer",    Department = "Design",        DateOfJoining = new DateTime(2021, 6, 25) },
        new() { Id = 4,  Name = "Dave",    Role = "DevOps Engineer",   Department = "Infrastructure",DateOfJoining = new DateTime(2018, 11, 5) },
        new() { Id = 5,  Name = "Eve",     Role = "QA Engineer",       Department = "Testing",        DateOfJoining = new DateTime(2022, 2, 18) },
        new() { Id = 6,  Name = "Frank",   Role = "Architect",         Department = "Web Development",DateOfJoining = new DateTime(2017, 9, 12) },
        new() { Id = 7,  Name = "Grace",   Role = "Business Analyst", Department = "Finance",          DateOfJoining = new DateTime(2020, 8, 30) },
        new() { Id = 8,  Name = "Hank",    Role = "Engineer",          Department = "Backend",        DateOfJoining = new DateTime(2021, 12, 1) },
        new() { Id = 9,  Name = "Ivy",     Role = "UI/UX Designer",    Department = "Design",         DateOfJoining = new DateTime(2019, 7, 14) },
        new() { Id = 10, Name = "Jack",    Role = "Manager",           Department = "Sales",          DateOfJoining = new DateTime(2016, 4, 22) },
        new() { Id = 11, Name = "Karen",   Role = "Engineer",          Department = "Frontend",       DateOfJoining = new DateTime(2022, 5, 9) },
        new() { Id = 12, Name = "Leo",     Role = "DevOps Engineer",   Department = "Infrastructure", DateOfJoining = new DateTime(2020, 3, 17) },
        new() { Id = 13, Name = "Mona",    Role = "Data Scientist",    Department = "Analytics",      DateOfJoining = new DateTime(2021, 10, 11) },
        new() { Id = 14, Name = "Nate",    Role = "Support",            Department = "Operations",    DateOfJoining = new DateTime(2019, 1, 28) },
        new() { Id = 15, Name = "Olivia",  Role = "QA Engineer",       Department = "Testing",        DateOfJoining = new DateTime(2023, 2, 2) },
        new() { Id = 16, Name = "Pete",    Role = "Engineer",          Department = "Mobile",         DateOfJoining = new DateTime(2020, 9, 19) },
        new() { Id = 17, Name = "Quinn",   Role = "Manager",           Department = "Design",         DateOfJoining = new DateTime(2018, 6, 7) },
        new() { Id = 18, Name = "Rita",    Role = "Architect",         Department = "Web Development",DateOfJoining = new DateTime(2017, 12, 15) },
        new() { Id = 19, Name = "Sam",     Role = "Developer",         Department = "Backend",        DateOfJoining = new DateTime(2021, 4, 23) },
        new() { Id = 20, Name = "Tina",    Role = "UI/UX Designer",    Department = "Design",          DateOfJoining = new DateTime(2022, 8, 5) },
        new() { Id = 21, Name = "Uma",     Role = "Business Analyst", Department = "Finance",          DateOfJoining = new DateTime(2019, 11, 30) },
        new() { Id = 22, Name = "Vince",   Role = "Engineer",          Department = "Frontend",       DateOfJoining = new DateTime(2020, 7, 21) },
        new() { Id = 23, Name = "Wendy",   Role = "QA Engineer",       Department = "Testing",         DateOfJoining = new DateTime(2023, 1, 10) },
        new() { Id = 24, Name = "Xavier",  Role = "DevOps Engineer",   Department = "Infrastructure", DateOfJoining = new DateTime(2018, 3, 3) },
        new() { Id = 25, Name = "Yara",    Role = "Data Scientist",    Department = "Analytics",       DateOfJoining = new DateTime(2021, 5, 26) },
        new() { Id = 26, Name = "Zack",    Role = "Support",           Department = "Operations",     DateOfJoining = new DateTime(2019, 9, 8) },
        new() { Id = 27, Name = "Ari",     Role = "UI/UX Designer",    Department = "Design",          DateOfJoining = new DateTime(2022, 6, 14) },
        new() { Id = 28, Name = "Bella",   Role = "Manager",           Department = "Sales",           DateOfJoining = new DateTime(2017, 2, 20) },
        new() { Id = 29, Name = "Chad",    Role = "Engineer",          Department = "Backend",         DateOfJoining = new DateTime(2021, 3, 12) },
        new() { Id = 30, Name = "Dina",    Role = "Architect",         Department = "Web Development", DateOfJoining = new DateTime(2016, 10, 1) },
        new() { Id = 31, Name = "Ethan",   Role = "Business Analyst", Department = "Finance",           DateOfJoining = new DateTime(2020, 12, 18) },
        new() { Id = 32, Name = "Fay",     Role = "QA Engineer",       Department = "Testing",          DateOfJoining = new DateTime(2023, 4, 6) },
    };
}