using Dapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
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
                connection.Execute("INSERT INTO Users (Login, Mail, Password, Usertype) VALUES (@Login, @Mail, @Password, @Usertype)", user);
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
        public void AddQuiz(Quiz quiz) 
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("INSERT INTO Quizzes (Name, PhoneNumber, CardNumber, BankAccount, TurnOver, Loan, Date, Currency, TypeOfCard) VALUES (@Name, @PhoneNumber, @CardNumber, @BankAccount, @TurnOver, @Loan, @Date, @Currency, @TypeOfCard)", quiz);
            }
        }
        public void SaveQuiz(ObservableCollection<Quiz> quizzes) 
        {
            string str;
            using (var connection = new SqlConnection(connectionString)) 
            {
                connection.Execute("TRUNCATE TABLE Quizzes");
                foreach (var quiz in quizzes) 
                {
                    str = JsonSerializer.Serialize(quiz);
                    connection.Execute($"INSERT INTO Quizzes(Quiz) VALUES ('{str}')");
                }
            }
        }
        public ObservableCollection<Quiz> GetQuiz()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                ObservableCollection<Quiz> values = new ObservableCollection<Quiz>() { };
                List<string> quizj = connection.Query<string>("Select Quiz FROM Quizzes").ToList();
                foreach (string quiz in quizj)
                    values.Add(JsonSerializer.Deserialize<Quiz>(quiz));
                return values;
            }
        }
    }
}
