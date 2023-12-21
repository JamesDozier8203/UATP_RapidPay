namespace RapidPay.Model;

using System.ComponentModel.DataAnnotations;

public class AuthenticateModel
{

    [Required(ErrorMessage = "Card Number is required!")]
    [MaxLengthAttribute(length: 15, ErrorMessage = "Card Number cannot be greater than 15 digits")]
    [RegularExpression("([0-9]+)", ErrorMessage = "Numbers only!")]
    public string? CardNumber { get; set; }

    [Required(ErrorMessage = "Pin Code is required!")]
    [MaxLengthAttribute(length: 15, ErrorMessage = "Pin Code cannot be greater than 4 digits")]
    [RegularExpression("([0-9]+)", ErrorMessage = "Numbers only!")]
    public string? PinCode { get; set; }

}