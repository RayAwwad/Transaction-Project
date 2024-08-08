namespace TransactionProject.Models.Entities
{
    public class Transactions
    {
        public int Id { get; set; }
        public required int SenderId { get; set; }
        public required int ReceiverId { get; set; }
        public required double Amount { get; set; }
  


    }
}
