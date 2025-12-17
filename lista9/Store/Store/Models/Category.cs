using System.ComponentModel.DataAnnotations;

namespace Store.Models;

public class Category
{
    public int Id { get; set; }
    
    [Required]
    [Display(Name = "Category Name")]
    public required string Name { get; set; }
    
    public ICollection<Article>? Articles { get; set; }
}