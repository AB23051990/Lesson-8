﻿using CardStorageService.Data;
using CardStorageService.Models.Requests;
using CardStorageService.Models;
using CardStorageService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using FluentValidation;
using CardStorageService.Models.Validators;
using FluentValidation.Results;

namespace CardStorageService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {

        #region Services

        private readonly ILogger<CardController> _logger;
        private readonly ICardRepositoryService _cardRepositoryService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCardRequest> _createCardRequestValidator;

        #endregion

        #region Constructors

        public CardController(ILogger<CardController> logger,
            ICardRepositoryService cardRepositoryService,
            IMapper mapper,
            IValidator<CreateCardRequest> createCardRequestValidator)
        {
            _logger = logger;
            _cardRepositoryService = cardRepositoryService;
            _mapper = mapper;
            _createCardRequestValidator = createCardRequestValidator;
        }

        #endregion


        #region Pulbic Methods

        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateCardRequest request)
        {
            ValidationResult validationResult = _createCardRequestValidator.Validate(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());                    
            try
            {
                //var cardId = _cardRepositoryService.Create(new Card
                //{
                //    ClientId = request.ClientId,
                //    CardNo = request.CardNo,
                //    ExpDate = request.ExpDate,
                //    CVV2 = request.CVV2
                //});
                var cardId = _cardRepositoryService.Create(_mapper.Map<Card>(request));
                return Ok(new CreateCardResponse
                {
                    CardId = cardId.ToString()
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create card error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 1012,
                    ErrorMessage = "Create card error."
                });
            }
        }

        [HttpGet("get-by-client-id")]
        [ProducesResponseType(typeof(GetCardsResponse), StatusCodes.Status200OK)]
        public IActionResult GetByClientId([FromQuery] string clientId)
        {
            try
            {
                var cards = _cardRepositoryService.GetByClientId(clientId);
                return Ok(new GetCardsResponse
                {
                    Cards = _mapper.Map<List<CardDto>>(cards)
                    //Cards = cards.Select(card => new CardDto
                    //{
                    //    CardNo = card.CardNo,
                    //    CVV2 = card.CVV2,
                    //    Name = card.Name,
                    //    ExpDate = card.ExpDate.ToString("MM/yy")
                    //}).ToList()
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get cards error.");
                return Ok(new GetCardsResponse
                {
                    ErrorCode = 1013,
                    ErrorMessage = "Get cards error."
                });
            }
        }

        #endregion



    }
}
