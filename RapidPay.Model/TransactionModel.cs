namespace RapidPay.Model;

using System.ComponentModel.DataAnnotations;

public class TransactionModel
{

    [Required(ErrorMessage = "Amount is required!")]
    [DataType("decimal(18,5)")]
    public decimal Amount { get; set; } = 0;

    [Required(ErrorMessage = "Account Name is required!")]
    public string? AccountName{ get; set; }

}