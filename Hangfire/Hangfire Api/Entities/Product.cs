using System.ComponentModel.DataAnnotations;

namespace Hangfire_Api.Entities;

public class Product
{
    public Guid Id { get; set; }

    [StringLength(30, MinimumLength = 3, ErrorMessage = "Must be from 3 to 30 characters.")]
    public required string Name { get; set; }

    //---------------------------------------
    public Guid CategoryId { get; set; }

    public required Category Category { get; set; }
}