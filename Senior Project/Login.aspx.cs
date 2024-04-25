using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace Senior_Project
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e){}
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
        protected void btnPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("Security.aspx");
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert hashed bytes to string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            int error = 0;

            if (email == "" || password == "")
            {
                error = 1;
                lblMessage.Text = "Error!! Missing fields.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            if (error == 0)
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["db"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Hash entered password for comparison
                    string hashedEnteredPassword = HashPassword(password);

                    // Check if hashedEnteredPassword matches the hashed password stored in the database
                    string selectQuery = "SELECT COUNT(*) FROM Accounts WHERE email = @Email AND password = @Password";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", hashedEnteredPassword);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            string nameQuery = "SELECT firstName FROM Accounts WHERE email = @Email";
                            SqlCommand nameCommand = new SqlCommand(nameQuery, connection);
                            nameCommand.Parameters.AddWithValue("@Email", email);
                            var fName = nameCommand.ExecuteScalar() as string;

                            // Redirect to Main.aspx on successful login.
                            Session["firstName"] = fName;
                            Response.Redirect("Main.aspx");
                        }
                        else
                        {
                            lblMessage.Text = "Invalid Email or Password, Please try again.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }
    }
}
