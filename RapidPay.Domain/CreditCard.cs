namespace RapidPay.Domain;

using System.ComponentModel.DataAnnotations;

public class CreditCard
{
    [Key]
    public Guid Id { get; set; }
    public decimal Balance { get; set; } = 0;
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public string CardType { get; set; }
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string SecurityCode { get; set; }
    public string PinCode { get; set; }
    public DateTime DateTimeCreated { get; set; } = DateTime.Now;
}