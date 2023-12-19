using System.Data;

namespace dbTransaction
{
    public class TransactionData
    {
        public DataTable CreateTransactionDataTable()
        {
            DataTable dataTable = new DataTable("TBLTransaction");

            // Realiza o autoincremento na TBLTransaction
            DataColumn inc = dataTable.Columns.Add("TransactionId", typeof(Int64));
            inc.AutoIncrement = true;
            inc.AutoIncrementSeed = 1;
            inc.AutoIncrementStep = 1;

            // Adiciona as colunas ao DataTable para transações.
            dataTable.Columns.Add("UserId", typeof(Int64));
            dataTable.Columns.Add("Amount", typeof(decimal));
            dataTable.Columns.Add("TransactionType", typeof(string));
            dataTable.Columns.Add("TransactionDate", typeof(DateTime));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("CurrentBalance", typeof(decimal));
            dataTable.Columns.Add("PaymentMethod", typeof(string));
            dataTable.Columns.Add("TransactionStatus", typeof(string));

            // Define a coluna "TransactionId" como chave primária.
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["TransactionId"] };

            return dataTable;
        }
    }
}
