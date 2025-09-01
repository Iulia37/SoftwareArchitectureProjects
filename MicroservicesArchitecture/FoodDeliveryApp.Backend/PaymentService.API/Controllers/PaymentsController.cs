using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
using PaymentService.API.DTOs;
using PaymentService.API.Models;
using PaymentService.API.Services;

namespace PaymentService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("{id}")]
        public IActionResult getPaymentById(int id)
        {
            try
            {
                var payment = _paymentService.getPaymentById(id);
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public IActionResult getPaymentsByUserId(int userId)
        {
            try
            {
                var payments = _paymentService.getPaymentsByUserId(userId);
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult createPayment([FromBody] PaymentDTO paymentDto)
        {
            try
            {
                var payment = TinyMapper.Map<Payment>(paymentDto);
                _paymentService.createPayment(payment);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult updatePayment(int id, [FromBody] PaymentDTO paymentDto)
        {
            try
            {
                var payment = TinyMapper.Map<Payment>(paymentDto);
                _paymentService.updatePayment(payment);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult deletePayment(int id)
        {
            try
            {
                _paymentService.deletePayment(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
