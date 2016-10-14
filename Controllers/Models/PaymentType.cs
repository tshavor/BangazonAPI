using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class PaymentType
  {
    [Key]
    //PaymentTypeId is the foriegn key to the PaymentType table//
    public int PaymentTypeId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [Required]
    [StringLength(12)]
    public string Description { get; set; }

    [Required]
    [StringLength(20)]
    public string AccountNumber { get; set; }
    public int CustomerId {get;set;}
    public Customer Customer {get;set;}
  }
}