using System.ComponentModel.DataAnnotations;

namespace DataGridTesting.Models;

public class GridRow
{
    [Key]
    public int Id { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z\s]+$",
        ErrorMessage = "Only letters and spaces allowed.")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[a-zA-Z\s]+$",
        ErrorMessage = "Only letters and spaces allowed.")]
    public string Role { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[a-zA-Z\s]+$",
        ErrorMessage = "Only letters and spaces allowed.")]
    public string Department { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfJoining { get; set; }
}