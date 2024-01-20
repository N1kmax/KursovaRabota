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
        private string connectionString = $@"Data Source = {System.Net.Dns.GetHostName()}; Initial Catalog = Users; Trusted_Connection=True; TrustServerCertificate = True";
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
