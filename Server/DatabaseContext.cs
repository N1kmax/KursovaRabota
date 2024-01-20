using Dapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProgramm
{
    public class DatabaseContext
    {
        private string connectionString = $@"Data Source = SQL5112.site4now.net; Initial Catalog = db_aa433a_usersdatabase; User Id = db_aa433a_usersdatabase_admin; Password = 8VYHjgKHHR-b8Gb;";
        public ObservableCollection<User> GetUsers()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return new ObservableCollection<User>(connection.Query<User>("SELECT * FROM Users").ToList());
            }
        }

        public void AddNewUser(User user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("INSERT INTO Users (Name, PhoneNumber, CardNumber, BankAccount, TurnOver, Loan, Date, Currency, TypeOfCard) VALUES (@Name, @PhoneNumber, @CardNumber, @BankAccount, @TurnOver, @Loan, @Date, @Currency, @TypeOfCard)", user);
            }
        }

        public void DeleteUser(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute($"DELETE FROM Users WHERE Id={id+1}");
            }
        }

        public void UpdateUser(int id, float loan, float turnover, float bankaccount)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute($"UPDATE Users SET Loan = {loan}, TurnOver = {turnover}, BankAccount = {bankaccount} WHERE Id={id+1}");
            }
        }
    }
}
