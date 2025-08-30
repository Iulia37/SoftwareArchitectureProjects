using PaymentService.API.Data;
using PaymentService.API.Models;

namespace PaymentService.API.Services
{
    public class PaymentService: IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public Payment getPaymentById(int id)
        {
            var payment = _paymentRepository.getPaymentById(id);
            if(payment == null)
            {
                throw new ArgumentException("Payment not found!");
            }
            return payment;
        }

        public IEnumerable<Payment> getPaymentsByUserId(int userId)
        {
            return _paymentRepository.getPaymentsByUserId(userId);
        }

        public void createPayment(Payment payment)
        {
            _paymentRepository.createPayment(payment);
        }

        public void updatePayment(Payment payment)
        {
            var existingPayment = _paymentRepository.getPaymentById(payment.Id);
            if (existingPayment == null)
            {
                throw new ArgumentException("Payment not found!");
            }
            else
            {
                existingPayment.OrderId = payment.OrderId;
                existingPayment.UserId = payment.UserId;
                existingPayment.Amount = payment.Amount;
                existingPayment.Method = payment.Method;
                _paymentRepository.updatePayment(existingPayment);
            }
        }

        public void deletePayment(int id)
        {
            var payment = _paymentRepository.getPaymentById(id);
            if(payment == null)
            {
                throw new ArgumentException("Payment not found!");
            }
            _paymentRepository.deletePayment(id);
        }
    }
}
