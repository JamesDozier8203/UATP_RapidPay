namespace RapidPay.API.Helpers;

using AutoMapper;
using RapidPay.Domain;
using RapidPay.Model;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreditCard, CreditCardModel>().ReverseMap();
        CreateMap<CreditCard, CreditCardModel>();
    }
}
