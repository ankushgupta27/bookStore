
 using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace bookStore.Models;
public partial class BookModel
{
    public int Id { get; set; }
[Required]
    public string? BookName { get; set; }
[Required]
    public string? Category { get; set; }

    public string? Image { get; set; }
[Required]
    public string? Author { get; set; }
[Required]
    public string? Publisher { get; set; }
[Required]
    public string? Description { get; set; }
}


public enum cat
{
    Novel,
    
    Comic,
    Sports 
}