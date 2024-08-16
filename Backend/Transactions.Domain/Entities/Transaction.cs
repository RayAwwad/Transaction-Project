namespace Transactions.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        // Foreign key for Sender (User)
        public required int SenderId { get; set; }

        // Navigation property for Sender
        public User Sender { get; set; }

        // Foreign key for Receiver (User)
        public required int ReceiverId { get; set; }

        // Navigation property for Receiver
        public User Receiver { get; set; }

        public required double Amount { get; set; }
    }
}
