using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
using OrderService.API.DTOs;
using OrderService.API.Models;
using OrderService.API.Services;
using System.Security.Claims;

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
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var order = _orderService.GetOrderById(orderId, currentUserId);
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
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var orders = _orderService.GetAllOrdersByUserId(userId, currentUserId);
                return Ok(orders);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
