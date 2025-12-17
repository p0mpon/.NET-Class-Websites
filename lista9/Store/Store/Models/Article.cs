using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class Article
{
    public int Id { get; set; }
    
    [Required]
    [Display(Name = "Article Name")]
    [MinLength(1, ErrorMessage="Name too short")]
    [MaxLength(100, ErrorMessage ="Name too long")]
    public required string Name { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    [Display(Name = "Price")]
    public decimal Price { get; set; }
    
    [Display(Name = "Image")]
    public string? ImagePath { get; set; }
    
    [NotMapped]
    [Display(Name = "File")]
    public IFormFile? FormFile { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Expiration Date")]
    public DateTime ExpirationDate { get; set; }

    [Required]
    [Display(Name = "Category")]
    public Category Category { get; set; }
}