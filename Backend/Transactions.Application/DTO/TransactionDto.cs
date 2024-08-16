namespace TransactionProject.Models
{
    public class TransactionDto
    {
        
        public required int ReceiverId { get; set; }
        public required double Amount { get; set; }
    }
}
