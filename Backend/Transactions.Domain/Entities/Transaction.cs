

namespace Transactions.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public required int SenderId { get; set; }
        public required int ReceiverId { get; set; }
        public required double Amount { get; set; }



    }
}
