using System;
using System.Data;

namespace dbTransaction
{
    public interface ITransactionService
    {
        void PerformTransaction(Transaction transaction);
        decimal GetBalance(int id);
        IEnumerable<Transaction> GetUserTransactions(int userId);

    }

    public class TransactionService : ITransactionService
    {
        private readonly IUserService _userService;
        private DataTable _transactionDataTable;

        public TransactionService(IUserService userService)
        {
            _userService = userService;

            TransactionData transactionData = new TransactionData();
            _transactionDataTable = transactionData.CreateTransactionDataTable();
        }

        public void PerformTransaction(Transaction transaction)
        {
            var user = _userService.GetUserByIdOrName(transaction.UserId.ToString());
            if (user == null)
                throw new InvalidOperationException("User not found.");

            // Get the current balance of the user before processing the transaction
            decimal currentBalance = GetBalance(user.UserId);

            // Business logic for transactions
            switch (transaction.Type)
            {
                case TransactionType.Deposit:
                    if (transaction.Amount <= 0)
                        throw new InvalidOperationException("Deposit amount must be greater than zero.");

                    user.Balance += transaction.Amount;
                    break;

                case TransactionType.Withdrawal:
                    if (transaction.Amount <= 0)
                        throw new InvalidOperationException("Withdrawal amount must be greater than zero.");

                    // Check if the balance is sufficient before processing the withdrawal
                    if (currentBalance < transaction.Amount)
                        throw new InvalidOperationException("Insufficient balance for withdrawal.");

                    user.Balance -= transaction.Amount;
                    break;

                case TransactionType.Purchase:
                    if (transaction.Amount <= 0)
                        throw new InvalidOperationException("Purchase amount must be greater than zero.");

                    // Specific logic for purchases (could be inventory, etc)
                    // May involve additional validations or specific processing for purchases
                    break;

                default:
                    throw new InvalidOperationException("Invalid transaction type.");
            }

            // Add additional information to the transaction
            transaction.Id = user.Transactions.Count + 1;
            transaction.Date = DateTime.Now;
            user.Transactions.Add(transaction);

            InputTransaction(transaction);
        }

        private void InputTransaction(Transaction transaction)
        {
            DataRow newTransaction = _transactionDataTable.NewRow();
 
            newTransaction["UserId"] = transaction.UserId;
            newTransaction["Amount"] = transaction.Amount;
            newTransaction["TransactionType"] = transaction.Type.ToString();
            newTransaction["TransactionDate"] = transaction.Date;

            //Hear can be other paramethers to transactions:
            // newTransaction["Description"] = transaction.Description;
            // newTransaction["CurrentBalance"] = transaction.CurrentBalance;
            // newTransaction["PaymentMethod"] = transaction.PaymentMethod;
            // newTransaction["TransactionStatus"] = transaction.TransactionStatus.ToString();

            _transactionDataTable.Rows.Add(newTransaction);
        }

        public decimal GetBalance(int id)
        {
            DataRow[] userTransactions = _transactionDataTable.Select($"UserId = {id}");

            // Calculate the balance considering transaction types
            decimal balance = 0;

            foreach (DataRow row in userTransactions)
            {
                decimal transactionAmount = Convert.ToDecimal(row["Amount"]);
                TransactionType transactionType = (TransactionType)Enum.Parse(typeof(TransactionType), row["TransactionType"].ToString());

                // Add the transaction amount to the balance if it is a deposit
                // Subtract the amount if it is a withdrawal
                if (transactionType == TransactionType.Deposit)
                {
                    balance += transactionAmount;
                }
                else if (transactionType == TransactionType.Withdrawal)
                {
                    balance -= transactionAmount;
                }
                // Logic for purchases or other transaction types if necessary
                // Purchases needs a definition to development
            }

            return balance;
        }
    
        public IEnumerable<Transaction> GetUserTransactions(int userId)
        {
            DataRow[] userTransactions = _transactionDataTable.Select($"UserId = {userId}");

            List<Transaction> transactions = [];

            foreach (DataRow row in userTransactions)
            {
                Transaction transaction = new Transaction
                {
                    Id = Convert.ToInt32(row["TransactionId"]),
                    UserId = Convert.ToInt32(row["UserId"]),
                    Amount = Convert.ToDecimal(row["Amount"]),
                    Type = Enum.Parse<TransactionType>(row["TransactionType"]?.ToString() ?? string.Empty),
                    Date = Convert.ToDateTime(row["TransactionDate"])
                };

                transactions.Add(transaction);
            }

            return transactions;
        }
    }
}
