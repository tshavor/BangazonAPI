using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Line Item is a "Join Table" which is a resource (like SongGenre) that houses 2 or more relationships.//
namespace Bangazon.Models
{
  public class LineItem
  {
    [Key]
    public int LineItemId {get;set;}

    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
  }
}