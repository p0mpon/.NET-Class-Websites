using System.ComponentModel.DataAnnotations;

namespace Store.Models;

public class Article
{
    public int Id { get; set; }
    
    [Required]
    [Display(Name = "Article Name")]
    public required string Name { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Display(Name = "Price")]
    public decimal Price { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Expiration Date")]
    public DateTime ExpirationDate { get; set; }

    [Required]
    [Display(Name = "Category")]
    public Category Category { get; set; }
}