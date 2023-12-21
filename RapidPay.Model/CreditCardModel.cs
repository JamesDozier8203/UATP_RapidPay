namespace RapidPay.Model;

using System.ComponentModel.DataAnnotations;

public class CreditCardModel
{
    public Guid Id{ get; }

    [Required(ErrorMessage = "Balance is required!")]
    [DataType("decimal(18,5)")]
    public decimal Balance { get; set; } = 0;

    [Required(ErrorMessage = "Card Number is required!")]
    [MaxLengthAttribute(length: 15, ErrorMessage = "Card Number cannot be greater than 15 digits")]
    [RegularExpression("([0-9]+)", ErrorMessage = "Numbers only!")]
    public string? CardNumber { get; set; }

    [Required(ErrorMessage = "Card Holders Name is required!")]
    public string? CardHolderName { get; set; }

    [Required(ErrorMessage = "Card Type is required!")]
    public string? CardType { get; set; }

    [Required(ErrorMessage = "Expiry Month is required!")]
    [Range(0, 13, ErrorMessage = "Expiry Month range for {0} must be between {1} and {2}.")]
    public int ExpiryMonth { get; set; }

    [Required(ErrorMessage = "Expiry Year is required!")]
    [YearRangeAttribute(ErrorMessage = "Expiry Year range for {0} must be between {1} and {2}.")]
    public int ExpiryYear { get; set; }

    [Required(ErrorMessage = "Security Code is required!")]
    [RegularExpression("([0-9]+)", ErrorMessage = "Numbers only!")]
    public string? SecurityCode { get; set; }

    [Required(ErrorMessage = "Pin Code is required!")]
    [RegularExpression("([0-9]+)", ErrorMessage = "Numbers only!")]
    public string? PinCode { get; set; }
}