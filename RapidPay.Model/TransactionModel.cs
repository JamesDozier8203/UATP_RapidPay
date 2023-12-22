namespace RapidPay.Model;

using System.ComponentModel.DataAnnotations;

public class TransactionModel
{

    [Required(ErrorMessage = "Amount is required!")]
    [DataType("decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Account Name is required!")]
    public string? AccountName{ get; set; }

}