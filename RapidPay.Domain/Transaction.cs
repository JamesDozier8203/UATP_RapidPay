namespace RapidPay.Domain;

using System.ComponentModel.DataAnnotations;

public class Transaction
{
    [Key]
    public Guid Id { get; }
    public decimal Amount { get; set; } = 0;
    public string AccountName { get; set; }
    public DateTime DateTimeCreated { get; } = DateTime.Now;
}