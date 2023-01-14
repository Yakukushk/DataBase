using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using static Database.UserInvertorySqlLite;

namespace Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            var registerSqlLite = new UserRegisterSqlLite();
            var registerSql = new UserRegister();

            while (true) {
                Console.WriteLine("1: Sql");            
                Console.WriteLine("2: Sqlite");            
                Console.WriteLine("3: Exit");
                var choice = int.Parse(Console.ReadLine());
                switch (choice) {
                    case 1:
                        registerSql.Menu();
                        break;
                    case 2:
                        registerSqlLite.Menu();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Bye!");
                        return;
                    default:
                        Console.WriteLine("Error! Input");
                        break;

                        
                }
            }          
               
            var user = new User();
           
            

           

        }
        
        public class UserRegister {
            
            private string _connectionString = "Server=DESKTOP-B1R3PUO\\SQLEXPRESS;Database=LekcjaProgram;Integrated Security=True;TrustServerCertificate=True";
            public void Menu() {
                try
                {
                  
                    while (true)
                    {
                        Console.WriteLine("0.Add in database");
                        Console.WriteLine("1.Delete in database");
                        Console.WriteLine("2.Update in databse");
                        Console.WriteLine("3.GetAllUsers");
                        Console.WriteLine("4.Clear");
                        Console.WriteLine("5.Exit");
                        var str = Convert.ToInt32(Console.ReadLine());
                        switch (str)
                        {
                            case 0:
                                var user = new User();
                                var reg = AddUser(user);
                                foreach (var item in reg)
                                {
                                    Console.WriteLine($"id = {item.Id}, name= {item.Name}, surname = {item.Surname}, age = {item.Age}, email = {item.Email}");
                                }
                                Console.WriteLine("added into datebase");
                                break;
                            case 1:
                                var user2 = new User();
                                DeleteUser(user2);
                                Console.WriteLine("removed into database");
                                break;
                            case 2:
                                var user3 = new User();
                                var reg1 = UpdateAllUser(user3);
                                foreach (var item in reg1) {
                                    Console.WriteLine($"Id = {item.Id}, name = {item.Name}, surname = {item.Surname}, age = {item.Age}, email = {item.Email}");
                                }
                                Console.WriteLine("updated into database");
                                break;
                            case 3: 
                                var user4 = new User();
                                var reg2 = GetAllUsers(user4);
                                foreach (var item in reg2)
                                {
                                    Console.WriteLine($"Id = {item.Id}, name = {item.Name}, surname = {item.Surname}, age = {item.Age}, email = {item.Email}");
                                }
                                Console.WriteLine("selected");
                                break;
                        
                            case 4:
                                Console.Clear();
                                Console.WriteLine("Console was cleaned");
                                break;
                            case 5:
                                Console.WriteLine("Good Bye!");
                                return;

                            default:
                                Console.WriteLine("Error!");
                                break;



                        }
                    }
                } catch(Exception ex) {
                    Console.WriteLine(ex);
                }
            
           
            }
            public void ExecuteCommand(string query) {
            var sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();
                var sqlCommand = new SqlCommand(query, sqlConnection);  
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                sqlConnection.Dispose();
            }
            
            public IEnumerable<User> AddUser(User user) {
                var sqlConnection = new SqlConnection(_connectionString);

                var result = new List<User>();
                Console.WriteLine("Input ID");
                var choiceId = int.Parse(Console.ReadLine());
                Console.WriteLine("Input Name");
                var choiceName = Console.ReadLine();
                Console.WriteLine("Input Surname");
                var choiceSurname = Console.ReadLine();
                Console.WriteLine("Input Age");
                var choiceAge = byte.Parse(Console.ReadLine());
                Console.WriteLine("Input Email");
                var choiceEmail = Console.ReadLine();
                var quaery2 = $"Select * from [User] insert into [User](Id, Name, Surname, Age, Email) values({choiceId} , '{choiceName}', '{choiceSurname}', '{choiceAge}', '{choiceEmail}')";
                sqlConnection.Open();
               
                
                var SelectsqlCommand = new SqlCommand(quaery2, sqlConnection);
                SelectsqlCommand.CommandType = System.Data.CommandType.Text;
                var reader = SelectsqlCommand.ExecuteReader();
                while (reader.Read())
                {

                    var id = int.Parse(reader["Id"].ToString());
                    var name = reader["Name"].ToString();
                    var surname = reader["Surname"].ToString();
                    var age = byte.Parse(reader["Age"].ToString());
                    var email = reader["Email"].ToString();

                    user = new User { Id = id, Name = name, Surname = surname, Age = age, Email = email };
                    result.Add(user);
                }
                //AddsqlCommand.ExecuteReader();
                SelectsqlCommand.Dispose();
                
               
                sqlConnection.Dispose();
                return result;
           }
            public IEnumerable<User> UpdateAllUser(User user) {
                var sqlConnection = new SqlConnection(_connectionString);
                Console.WriteLine("Id : ");
                var choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Name : ");
                var choiceName = Console.ReadLine();
                Console.WriteLine("Surname : ");
                var choiceSurname = Console.ReadLine();
                Console.WriteLine("Age : ");
                var choiceAge = byte.Parse(Console.ReadLine());
                Console.WriteLine("Email : ");
                var choiceEmail = Console.ReadLine();
                var updatequary = $"Select * from [User] update [User] set Name = '{choiceName}', Surname = '{choiceSurname}', Age = '{choiceAge}', Email = '{choiceEmail}' where id = {choice};";
                sqlConnection.Open();
                var UpdateSqlCommand = new SqlCommand(updatequary, sqlConnection);
                var res = new List<User>();
                UpdateSqlCommand.CommandType = System.Data.CommandType.Text;
                var reader = UpdateSqlCommand.ExecuteReader();
                while (reader.Read()) {
                    var id = int.Parse(reader["Id"].ToString());
                    var name = reader["Name"].ToString();
                    var surname = reader["Surname"].ToString();
                    var age = byte.Parse(reader["Age"].ToString());
                    var mail = reader["Email"].ToString();
                    user = new User {
                    Id = id,
                    Name = name,
                    Surname = surname,
                    Age = age,
                    Email = mail

                    };
                    res.Add(user);  

                }
                UpdateSqlCommand.Dispose();
                sqlConnection.Dispose();
                return res;
            }
            public void DeleteUser(User user) {
            var sqlConnection = new SqlConnection(_connectionString);
                Console.WriteLine("Input your id num");
                var checkId = Convert.ToInt32(Console.ReadLine());
            var deletequary = $"delete from [User] Where Id = {user.Id}";
                
                sqlConnection.Open();
                var DeleteSqlCommand = new SqlCommand(deletequary, sqlConnection);
                DeleteSqlCommand.ExecuteNonQuery();
                DeleteSqlCommand.Dispose();
                sqlConnection.Dispose();
            }
            public void UpdateUser(User user) {
                
                var sqlConnection = new SqlConnection(_connectionString);
                Console.WriteLine("Id : ");
                var choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Name : ");
                var choiceName = Console.ReadLine();
                Console.WriteLine("Surname : ");
                var choiceSurname = Console.ReadLine();
                Console.WriteLine("Age :");
                var choiceAge = byte.Parse(Console.ReadLine());
                Console.WriteLine("Email :");
                var choiceEmail = Console.ReadLine();   
                
                
                var updatequary = $"update [User] set Name = '{choiceName}', Surname = '{choiceSurname}, Age = {choiceAge}, Email = {choiceEmail}' where id = {choice};";
                sqlConnection.Open();
                var UpdateSqlCommand = new SqlCommand(updatequary, sqlConnection);

                UpdateSqlCommand.ExecuteNonQuery();
                UpdateSqlCommand.Dispose();
                sqlConnection.Dispose();
            }
            public IEnumerable<User> GetAllUsers(User user) {
                var result = new List<User>();  
                var queary = "Select * from [User]";
                var sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();
                var sqlCommand = new SqlCommand(queary, sqlConnection); 
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read()) {
                   
                    var id = int.Parse(reader["Id"].ToString());
                    var name = reader["Name"].ToString();
                    var surname = reader["Surname"].ToString();
                    var age = byte.Parse(reader["Age"].ToString());
                    var email = reader["Email"].ToString();

                    var users = new User {Id = id, Name = name, Surname = surname, Age = age, Email = email };
                    result.Add(users);
                    
                }
                
                sqlCommand.Dispose();
                sqlConnection.Dispose();
                    return result;
            }
            public IEnumerable<User> GetAllFilterUsers(string str) {
                var result = new List<User>();
                var filter = char.Parse(Console.ReadLine());
                var quaery = $"select * from [User] where Name Like '{filter}'";
                var sqlConnection = new SqlConnection(_connectionString);   
                var sqlCommand = new SqlCommand(quaery, sqlConnection);
                sqlConnection.Open();
                var reader = sqlCommand.ExecuteReader();
                foreach (var item in reader) {
                    var id = int.Parse(reader["Id"].ToString());
                    var name = reader["Name"].ToString();
                    var surname = reader["Surname"].ToString();
                    var age = byte.Parse(reader["Age"].ToString());
                    var email = reader["Email"].ToString();

                    var user = new User { Id = id, Name = name, Surname = surname, Age = age, Email = email };
                    result.Remove(user);
                }
                sqlCommand.Dispose();
                sqlConnection.Dispose();
                return result;
            }
            public IEnumerable<User> FilterByNameUsers(string str) {
                var queary = $"select * FROM[User] where Name Like '@Value'";
                var res = new List<User>();
                var sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();
                var sqlCommand = new SqlCommand(queary, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Value", str);
                var ready = sqlCommand.ExecuteReader();
                ready.Read();
                while (ready.Read()) {
                    var id = int.Parse(ready["id"].ToString());
                    var name = ready["Name"].ToString();
                    var surname = ready["Surname"].ToString();
                    var age = byte.Parse(ready["Age"].ToString());
                    var mail = ready["Email"].ToString();
                    var user = new User { Id = id, Name = name, Surname = surname, Age = age, Email = mail };
                    res.Add(user);
                }
                sqlCommand.Dispose();
                sqlConnection.Dispose();

                return res; 
            }
     
          

        }
        static void Print() {
            var collection = new Collection();
            var filter = collection.Filter(CheckInt);
            foreach (var item in filter)
            {
                Console.WriteLine(item);
            }
        }
        static bool CheckInt(int value) {
            if (value > 100 && value < 130)
            {
                return true;
            }
            else {
            return false;
            }
        }
    }
    public class Collection {
        private int[] _ints;
        public Collection() {
        _ints = new int[200];
            for (var i = 0; i < 200; i++) {
            _ints[i] = i;
            }
        }
        public delegate bool MyDelegate(int value);
        public event MyDelegate _MyDelegate;
        public IEnumerable<int> Filter(Func<int, bool> myDelegate) {
            Action<int, string> action;
       var result = new List<int>();
            foreach (var item in _ints) {
                if (myDelegate.Invoke(item)) {
                result.Add(item);
                }
                
            }
            return result;
        }
    }
}
