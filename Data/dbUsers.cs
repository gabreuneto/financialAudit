using System.Data;

namespace dbUsers
{
    public class UserData
    {
        public DataTable CreateUserDataTable()
        {
            DataTable dataTable = new DataTable("TBLUser");
            //Realiza o autoincremento na TBLUser
            DataColumn inc = dataTable.Columns.Add("UserId" , typeof(Int64));
            inc.AutoIncrement = true;
            inc.AutoIncrementSeed = 1;
            inc.AutoIncrementStep = 1;
            
            // Adiciona as colunas ao DataTable.
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Cnpj", typeof(string));
            dataTable.Columns.Add("Password", typeof(string));

            //dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["UserId"] };
            var userIdColumn = dataTable.Columns["UserId"];
            if (userIdColumn != null)
            {
                dataTable.PrimaryKey = new DataColumn[] { userIdColumn };
            }
            else
            {
                userIdColumn = new DataColumn("UserId", typeof(int));
                dataTable.Columns.Add(userIdColumn);

                // Define a nova coluna como a chave primária
                dataTable.PrimaryKey = new DataColumn[] { userIdColumn };
            }

            return dataTable;
        }

    }
}