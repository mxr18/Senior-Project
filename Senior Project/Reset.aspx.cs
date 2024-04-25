using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace Senior_Project
{
    public partial class Reset : System.Web.UI.Page
    {
        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            string passPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).+$";

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                lblMessage.Text = "Please enter a new password and confirm it.";
                return;
            }

            if (newPassword != confirmPassword)
            {
                lblMessage.Text = "Passwords do not match.";
                return;
            }

            if (!Regex.IsMatch(newPassword, passPattern))
            {
                lblMessage.Text = "Password must contain at least one uppercase letter, one lowercase letter, and one number.";
                return;
            }

            if (newPassword.Length < 8)
            {
                lblMessage.Text = "Password must be at least 8 characters.";
                return;
            }

            string hashedPassword = HashPassword(newPassword);
            string email = Session["email"] as string;

            if (email == null)
            {
                Response.Redirect("Security.aspx");
                return;
            }

            string connectionString = WebConfigurationManager.ConnectionStrings["db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string updateQuery = "UPDATE Accounts SET password = @Password WHERE email = @Email";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Session.Remove("email");  //Clean up session after successful update
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Password reset failed. Please try again.";
                    }
                }
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
