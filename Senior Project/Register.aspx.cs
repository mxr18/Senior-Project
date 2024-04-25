using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace Senior_Project
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["db"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT questionID, secQuestion FROM SecurityQuestions";
                    SqlCommand command = new SqlCommand(query, connection);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        ddlQuestion.DataSource = reader;
                        ddlQuestion.DataTextField = "secQuestion";
                        ddlQuestion.DataValueField = "questionID";
                        ddlQuestion.DataBind();
                        ddlQuestion.Items.Insert(0, new ListItem("--Select a Question--", "0"));
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
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
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Retrieve data from input
            string fName = txtFirstName.Text;
            string lName = txtLastName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string selectedQuestion = ddlQuestion.SelectedValue;
            string answer = txtAnswer.Text;


            fName = fName[0].ToString().ToUpper() + fName.Substring(1);
            lName = lName[0].ToString().ToUpper() + lName.Substring(1);

            fName = HttpUtility.HtmlEncode(fName);
            lName = HttpUtility.HtmlEncode(lName);
            
            string emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            string passPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).+$";

            if (!Regex.IsMatch(email, emailPattern))
            {
                lblMessage.Text = "Invalid email address.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!Regex.IsMatch(password, passPattern))
            {
                lblMessage.Text = "Password must contain at least one uppercase letter, one lowercase letter, and one number.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if(password.Length < 8)
            {
                lblMessage.Text = "Password must be at least 8 characters long.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (email == "" || password == "" || fName == "" || lName == "")
            {

                lblMessage.Text = "Error!! Missing fields.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connectionString = WebConfigurationManager.ConnectionStrings["db"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Hash the password before storing it
                    string hashedPassword = HashPassword(password);

                    // Your existing code for database insertion
                    string insertQuery = "INSERT INTO Accounts (firstName, lastName, email, password, questionID, secAnswer) " +
                        "VALUES (@FirstName, @LastName, @Email, @Password, @QuestionID, @SecAnswer)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", fName);
                        command.Parameters.AddWithValue("@LastName", lName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.Parameters.AddWithValue("@QuestionID", selectedQuestion);
                        command.Parameters.AddWithValue("@SecAnswer", answer);
                        command.ExecuteNonQuery();

                        Response.Redirect("Login.aspx");
                    }
                }
            }
            catch (SqlException)
            {
                lblMessage.Text = "This user already exists";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
