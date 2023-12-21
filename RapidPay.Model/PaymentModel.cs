namespace RapidPay.Model;

using System.ComponentModel.DataAnnotations;

public class PaymentModel
{

    [Required(ErrorMessage = "Amount is required!")]
    [DataType("decimal(18,5)")]
    public decimal Amount { get; set; } = 0;

    [Required(ErrorMessage = "Account Number is required!")]
    [RegularExpression("([0-9]+)", ErrorMessage = "Numbers only!")]
    public string? AccountNumber{ get; set; }

    [Required(ErrorMessage = "Bank Name is required!")]
    public string? BankName { get; set; }
}