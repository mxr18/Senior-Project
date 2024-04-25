using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Senior_Project
{
    public partial class Security : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) {}

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;

            int error = 0;

            if (email == "")
            {
                error = 1;
                lblMessage.Text = "Error!! Missing Email.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            if (error == 0)
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["db"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT COUNT(*) FROM Accounts WHERE email = @Email";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            // Redirect to Question.aspx on success.
                            Session["email"] = email;
                            Response.Redirect("Question.aspx");
                        }
                        else
                        {
                            lblMessage.Text = "Invalid Email, Please try again.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }
    }
}
