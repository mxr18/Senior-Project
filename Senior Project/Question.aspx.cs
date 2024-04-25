using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Senior_Project
{
    public partial class Question : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string email = Session["email"] as string;
                if (email == null)
                {
                    Response.Redirect("Security.aspx");
                    return;
                }

                string connectionString = WebConfigurationManager.ConnectionStrings["db"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT SecurityQuestions.secQuestion FROM Accounts JOIN SecurityQuestions ON Accounts.questionID = SecurityQuestions.questionID WHERE email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            lblQuestion.Text = result.ToString();
                        }
                        else
                        {
                            lblMessage.Text = "No security question found!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }
        protected void btnVerifyAnswer_Click(object sender, EventArgs e)
        {
            string email = Session["email"] as string;
            string userAnswer = txtAnswer.Text;
            string connectionString = WebConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT secAnswer FROM Accounts WHERE email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    string correctAnswer = command.ExecuteScalar() as string;

                    if (userAnswer.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
                    {
                        Response.Redirect("Reset.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Incorrect answer, please try again.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
    }
}
