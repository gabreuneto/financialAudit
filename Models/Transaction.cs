using System;
using System.ComponentModel.DataAnnotations;

public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }

    // Optional properties
    // [Required(AllowEmptyStrings = true)]
    // public decimal CurrentBalance { get; set; }
    // [Required(AllowEmptyStrings = true)]
    // public string PaymentMethod { get; set; }
    // [Required(AllowEmptyStrings = true)]
    // public string TransactionStatus { get; set; }
    // [Required(AllowEmptyStrings = true)]
    // public string Description { get; set; }

    [Required]
    public TransactionType Type { get; set; }

    [Required]
    public DateTime Date { get; set; }
}
