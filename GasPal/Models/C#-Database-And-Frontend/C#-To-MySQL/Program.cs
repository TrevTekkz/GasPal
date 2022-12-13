
// IMPORTANT!!! Right click your solution file >> Manage NuGet Packages... >> And install MySQL.Data by Oracle

using System;
using MySql.Data.MySqlClient; // Import Statement for connecting C# to MySQL client

namespace CSharpToMySQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Four variables to create the connection string
            string server = "localhost"; // If not using a cloud database, use localhost. If using cloud database, use the IP to the server
            string database = "studentdb"; // Whatever database you want use for the application
            string username = "root"; // The username of your MySQL client
            string password = "password"; // The password of your MySQL client

            // Throw all the variables into this connection string
            // MUST USE THIS FORMAT! (server, database, username, then password)
            string connstring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";

            string userSelectionString;
            int userSelection = 0;

            // Initializes the query string to be used for when you GET POST or DELETE
            string query;

            // The variables you will be using for inputs... you should use objects not just global variables...
            string ID;
            string fName;
            string lName;
            string phone;
            string state;

            // Initializes the connection object called "conn", using the connection string
            MySqlConnection conn = new MySqlConnection(connstring);

            // Initializes the command object called "cmd"
            MySqlCommand cmd;

            // Initializes the reader objected called "reader"
            MySqlDataReader reader;

            while (userSelection != 4)
            {

                Console.WriteLine("Choose an option...");
                Console.WriteLine("1) Insert into database");
                Console.WriteLine("2) Remove from database");
                Console.WriteLine("3) View data from database");
                Console.WriteLine("4) Exit from interface");

                userSelectionString = Console.ReadLine();
                userSelection = Convert.ToInt32(userSelectionString);

                switch (userSelection)
                {
                    case 1:
                        Console.WriteLine("First name:");
                        fName = Console.ReadLine();
                        Console.WriteLine("Last name:");
                        lName = Console.ReadLine();
                        Console.WriteLine("Phone number:");
                        phone = Console.ReadLine();
                        Console.WriteLine("State:");
                        state = Console.ReadLine();

                        // You MUST open the connection to be able to execute any query... if you don't it won't work
                        conn.Open();

                        // Set your query to whatever SQL query you want... in this case we are doing an INSERT statement.
                        // Notice I use an @ symbol each time for the values we input into the datase...
                        query = "INSERT INTO person(fName, lName, phone, state) VALUES (@fName, @lName, @phone, @state)";

                        // Create the command you want to execute by passing through your query and connection
                        cmd = new MySqlCommand(query, conn);

                        // We use a cmd.Paramaters.AddWithValue to bind the @fName to the fName variable that was assigned when the user inputted the information
                        cmd.Parameters.AddWithValue("@fName", fName);
                        cmd.Parameters.AddWithValue("@lName", lName);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@state", state);

                        // We then run an ExecuteNonQuery() since we are executing our command we created, but it's a NonQuery since we are not expecting anything back from the database
                        cmd.ExecuteNonQuery();

                        // Lastly we MUST close the connection because connections are limited relatively expensive resource
                        conn.Close();

                        Console.WriteLine("Person has been inserted into the datbase.");

                        break;

                    case 2:
                        Console.WriteLine("Which person would you like to remove? Enter an ID.");
                        ID = Console.ReadLine();

                        conn.Open();
                        query = "DELETE FROM person WHERE ID = @ID";
                        cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        Console.WriteLine("Person has beem removed from the database.");

                        break;

                    case 3:
                        Console.WriteLine("Here are the people in the database.");
                    
                        conn.Open();
                        query = "SELECT * FROM person";
                        cmd = new MySqlCommand(query, conn);

                        // We set the reader variable to cmd.ExecuteReader() so it can read the entries of the table it receives from first to last
                        reader = cmd.ExecuteReader();

                        // While there is data to read it will keep looping
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["ID"]); // Row N ID... N indicates which row it is currently on
                            Console.WriteLine(reader["fName"]); // You should assign these variables to an array of objects rather than write them to the console
                            Console.WriteLine(reader["lName"]);
                            Console.WriteLine(reader["phone"]);
                            Console.WriteLine(reader["state"]);
                        }
                        conn.Close();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Invalid Selection. Choose again.");
                        break;
                }

            }
        }
    }
}