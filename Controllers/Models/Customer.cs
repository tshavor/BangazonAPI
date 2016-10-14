using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class Customer
  {
    //[Key] Primary key is assumed to be required//
    [Key]
    public int CustomerId {get;set;}
//[Required]: validates the data- this is required!, [DataType(DataType.Date)]= Date required; 
    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
//every customer can have a collection of payment types//  this is just establishing the relationship 
//between the customer and Payment Types
    public ICollection<PaymentType> PaymentTypes;
  }
}