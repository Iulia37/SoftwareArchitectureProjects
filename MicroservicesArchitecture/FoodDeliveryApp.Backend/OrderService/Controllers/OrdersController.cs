using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
using OrderService.API.DTOs;
using OrderService.API.Services;
using OrderService.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace OrderService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{orderId}")]
        public IActionResult GetOrderById(int orderId)
        {
            try
            {
                var order = _orderService.GetOrderById(orderId);
                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetOrdersByUserId(int userId)
        {
            return Ok(_orderService.GetAllOrdersByUserId(userId));
        }

        [HttpPost]
        public IActionResult AddOrder(OrderRequestDTO orderRequest)
        {
            try
            {
                var order = new Order
                {
                    UserId = orderRequest.UserId,
                    RestaurantId = orderRequest.RestaurantId,
                    Status = "Pending",
                    TotalPrice = orderRequest.OrderItems.Sum(item => item.UnitPrice * item.Quantity)
                };
                var orderItems = TinyMapper.Map<OrderItem[]>(orderRequest.OrderItems);
                _orderService.AddOrder(order, orderItems);
                return CreatedAtAction(nameof(GetOrderById),
                    new { orderId = order.Id },
                    order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderDTO orderDto)
        {
            try
            {
                var order = TinyMapper.Map<Order>(orderDto);
                _orderService.UpdateOrder(order);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _orderService.DeleteOrder(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{orderId}/items")]
        public IActionResult GetOrderItemsByOrderId(int orderId)
        {
            try
            {
                var orderItems = _orderService.GetOrderItemsByOrderId(orderId);
                return Ok(orderItems);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
