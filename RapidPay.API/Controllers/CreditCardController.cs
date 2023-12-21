using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidPay.API.Helpers;
using RapidPay.Domain;
using RapidPay.Repository.Helpers;
using RapidPay.Model;
using AutoMapper;
using RapidPay.API.Business_Rules;

namespace RapidPay.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CreditCardController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICardSecurity _cardSecurity;

    public CreditCardController(IUnitOfWork unitOfWork,
                                IMapper mapper,
                                ICardSecurity cardSecurity)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cardSecurity = cardSecurity;
    }

    [HttpPost()]
    private async Task<IActionResult> Create(CreditCardModel creditCardModel)
    {
        #region Validation
        if (!ModelState.IsValid)
            return BadRequest(new { message = GetErrors() });
        #endregion

        var creditCard = _mapper.Map<CreditCard>(creditCardModel);

        // add object for inserting
        await _unitOfWork.CreditCardRepository.InsertAsync(creditCard);
        int complete = await _unitOfWork.Complete();

        return Ok(complete);
    }


    [HttpPut()]
    private async Task<IActionResult> Update(CreditCardModel creditCardModel)
    {
        #region Validation
        if (!ModelState.IsValid)
            return BadRequest(new { message = GetErrors() });
        #endregion

        var creditCard = _mapper.Map<CreditCard>(creditCardModel);

        // add object for inserting
        await _unitOfWork.CreditCardRepository.Update(creditCard, creditCard.Id);
        int complete = await _unitOfWork.Complete();

        return Ok(complete);
    }

    [HttpDelete()]
    private async Task<IActionResult> Delete(string id)
    {
        #region Validation
        if (string.IsNullOrEmpty(id))
            return BadRequest(new { message = "Id cannot be empty!" });

        Guid _id;
        bool isGuid = Guid.TryParse(id,out _id);

        if(!isGuid)
            return BadRequest(new { message = "Id is invalid!" });
        #endregion

        _unitOfWork.CreditCardRepository.Delete(_id);
        int complete = await _unitOfWork.Complete();

        return Ok(complete);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        #region Validation
        if (string.IsNullOrEmpty(id))
            return BadRequest(new { message = "Id cannot be empty" });

        Guid _id;
        bool isGuid = Guid.TryParse(id, out _id);

        if (!isGuid)
            return BadRequest(new { message = "Id is invalid" });
        #endregion


        var creditCard = await _unitOfWork.CreditCardRepository.GetByIdAsync(id);
        await _unitOfWork.Complete();

        if (creditCard == null)
            return BadRequest(new { message = "Credit Card not found!" });

        var creditCardModel = _mapper.Map<CreditCardModel>(creditCard);

        return Ok(creditCardModel);
    }

    [HttpGet("balance")]
    public async Task<IActionResult> GetBalance(CreditCardBalanceModel creditCardBalanceModel)
    {
        #region Validation
        if (!ModelState.IsValid)
            return BadRequest(new { message = GetErrors() });
        #endregion

        /*
         * Advice: Here I would a seperate validation and authentication method to do the following.
         * 1. Check if Card exists by card number
         * 2. Check if user is authorized to check balance with Pin Code 
         */

        var cardResult = await _unitOfWork.CreditCardRepository.GetBalance(creditCardBalanceModel.CardNumber, creditCardBalanceModel.PinCode);
        await _unitOfWork.Complete();

        if (string.IsNullOrEmpty(cardResult.Item1))
            return BadRequest(new { message = "Credit Card not found!" });
        else
            return Ok(cardResult.Item2);
    }

    [HttpPost("pay")]
    public async Task<IActionResult> Pay(CreditCardModel creditCardModel, TransactionModel transactionModel)
    {
        //I'm keeping this as simple as I can. I can go full blast with this payment section, but will stick to demostrating coding skills and not so much business acumen.
        
        #region Validation
        if (!ModelState.IsValid)
            return BadRequest(new { message = GetErrors() });
        #endregion

        var creditCard = _mapper.Map<CreditCard>(creditCardModel);

        //validate card
        if(!_cardSecurity.IsCardValid(creditCard))
            return BadRequest(new { message = "Card Invalid!" });

        //check if funds available in account balance
        if (!_cardSecurity.IsFundsAvailable(creditCard.CardNumber, transactionModel.Amount))
            return BadRequest(new { message = "Insufficient Funds!" });

        //pay to account. create payment transaction
        var transaction = _mapper.Map<Transaction>(transactionModel);
        await _unitOfWork.TransactionRepository.InsertAsync(transaction);

        //deduct from account balance
        var creditCardUpdate = await _unitOfWork.CreditCardRepository.GetCardByCardNumber(creditCardModel.CardNumber);
        creditCardUpdate.Balance = creditCardUpdate.Balance - transactionModel.Amount;
        await _unitOfWork.CreditCardRepository.Update(creditCardUpdate, creditCardUpdate.Id);

        await _unitOfWork.Complete();

        return Ok();
    }



    [HttpGet("list")]
    public async Task<IEnumerable<CreditCardModel>> List()
    {
        IEnumerable<CreditCard> creditCard = await _unitOfWork.CreditCardRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CreditCardModel>>(creditCard);
    }
}
