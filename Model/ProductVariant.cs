using pos_simple.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductVariant
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty; 

    public decimal? Price { get; set; }

}
