namespace RapidPay.Model;

using System.ComponentModel.DataAnnotations;

public class YearRangeAttribute : RangeAttribute
{
    public YearRangeAttribute() : base(DateTime.Now.Year, DateTime.Now.Year + 5)
    {
    }
}