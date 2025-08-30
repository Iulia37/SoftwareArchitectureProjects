using PaymentService.API.Models;

namespace PaymentService.API.Data
{
    public class PaymentRepository: IPaymentRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public Payment getPaymentById(int id)
        {
            return _context.Payments.Find(id);
        }

        public IEnumerable<Payment> getPaymentsByUserId(int userId)
        {
            return _context.Payments.Where(p => p.UserId == userId).ToList();
        }

        public void createPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void updatePayment(Payment payment)
        {
            _context.Payments.Update(payment);
            _context.SaveChanges();
        }

        public void deletePayment(int id)
        {
            _context.Payments.Remove(getPaymentById(id));
            _context.SaveChanges();
        }
    }
}
