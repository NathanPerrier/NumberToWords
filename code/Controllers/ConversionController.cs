using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace NumberToWords.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversionController : ControllerBase
    {
        private readonly INumberToWordsService _conversionService;
        private readonly ILogger<ConversionController> _logger;

        public ConversionController(INumberToWordsService conversionService, ILogger<ConversionController> logger)
        {
            _conversionService = conversionService;
            _logger = logger;
        }

        [HttpPost("convert")]
        public IActionResult ConvertToWords([FromBody] ConversionRequest request)
        {
            try
            {
                // Validate request
                if (request == null || string.IsNullOrWhiteSpace(request.Value))
                {
                    return BadRequest(new ConversionResponse 
                    { 
                        Success = false, 
                        Error = "Please provide a valid number." 
                    });
                }

                // Validate input format
                if (!IsValidCurrencyFormat(request.Value))
                {
                    return BadRequest(new ConversionResponse 
                    { 
                        Success = false, 
                        Error = "Invalid format. Please enter a valid number (e.g., 123.45)" 
                    });
                }

                // Convert to words
                var result = _conversionService.ConvertToWords(request.Value);

                return Ok(new ConversionResponse 
                { 
                    Success = true, 
                    Words = result,
                    OriginalValue = request.Value
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid argument provided for conversion");
                return BadRequest(new ConversionResponse 
                { 
                    Success = false, 
                    Error = ex.Message 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error converting number to words");
                return StatusCode(500, new ConversionResponse 
                { 
                    Success = false, 
                    Error = "An error occurred while processing your request." 
                });
            }
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
        }

        private bool IsValidCurrencyFormat(string value)
        {
            // Regex to match valid currency format (optional negative, digits, optional decimal with up to 2 places)
            var pattern = @"^-?\d{1,9}(\.\d{0,2})?$";
            return Regex.IsMatch(value.Trim(), pattern);
        }
    }

    // Request/Response DTOs
    public class ConversionRequest
    {
        public string Value { get; set; } = string.Empty;
    }

    public class ConversionResponse
    {
        public bool Success { get; set; }
        public string? Words { get; set; }
        public string? Error { get; set; }
        public string? OriginalValue { get; set; }
    }

    // Service Interface
    public interface INumberToWordsService
    {
        string ConvertToWords(string value);
    }
}