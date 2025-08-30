using PaymentService.API.Models;

namespace PaymentService.API.Services
{
    public interface IPaymentService
    {
        public Payment getPaymentById(int id);

        public IEnumerable<Payment> getPaymentsByUserId(int userId);

        public void createPayment(Payment payment);

        public void updatePayment(Payment payment);

        public void deletePayment(int id);
    }
}
