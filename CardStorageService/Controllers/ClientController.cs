using AutoMapper;
using CardStorageService.Data;
using CardStorageService.Models.Requests;
using CardStorageService.Models.Validators;
using CardStorageService.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardStorageService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        #region Services

        private readonly IClientRepositoryService _clientRepositoryService;
        private readonly ILogger<CardController> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateClientRequest> _createClientRequestValidator;

        #endregion

        #region Constructors

        public ClientController(
            ILogger<CardController> logger,
            IClientRepositoryService clientRepositoryService,
            IMapper mapper,
            IValidator<CreateClientRequest> createClientRequestValidator)
        {
            _logger = logger;
            _clientRepositoryService = clientRepositoryService;
            _mapper = mapper;
            _createClientRequestValidator = createClientRequestValidator;
        }

        #endregion

        #region Pulbic Methods

        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateClientRequest request)
        {
            ValidationResult validationResult = _createClientRequestValidator.Validate(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());
            try
            {
                //var clientId = _clientRepositoryService.Create(new Client
                //{
                //    FirstName = request.FirstName,
                //    Surname = request.Surname,
                //    Patronymic = request.Patronymic
                //});
                var clientId = _clientRepositoryService.Create(_mapper.Map<Client>(request));
                return Ok(new CreateClientResponse
                {
                    ClientId = clientId
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create client error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 912,
                    ErrorMessage = "Create clinet error."
                });
            }
        }

        #endregion

    }
}
