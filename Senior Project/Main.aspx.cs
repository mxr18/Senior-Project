using System;
using System.Net.Http;
using System.Web.UI;
using Newtonsoft.Json;

namespace Senior_Project
{
    public partial class Main : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["firstName"] != null)
                {
                    lblGreeting.Text = "Welcome Back, " + Session["firstName"].ToString();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            var searchTerm = searchBox.Value;
            if (string.IsNullOrEmpty(searchTerm))
            {
                litResults.Text = "<script>alert('Search term cannot be empty!');</script>";
                return;
            }

            string baseUrl = "https://www.googleapis.com/books/v1/volumes?q=";
            string apiKey = "key=YOUR_API_KEY";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{baseUrl}{Uri.EscapeDataString(searchTerm)}{apiKey}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<GoogleBooksResult>(result);
                    litResults.Text = FormatResults(data);
                }
                else
                {
                    litResults.Text = "<script>alert('No results found, please try again.');</script>";
                }
            }
        }
        private string FormatResults(GoogleBooksResult data)
        {
            var output = "";

            // Check if data.Items is null or if there are no items in the array
            if (data.Items == null || data.Items.Length == 0)
            {
                return "<div class='alert alert-warning'>No books found.</div>";
            }

            foreach (var item in data.Items)
            {
                string title = item.VolumeInfo.Title;
                string authors = item.VolumeInfo.Authors != null ? string.Join(", ", item.VolumeInfo.Authors) : "No authors listed";
                string publisher = item.VolumeInfo.Publisher ?? "Publisher not available";
                string description = item.VolumeInfo.Description ?? "No description available.";
                string bookLink = item.VolumeInfo.PreviewLink;
                string bookImg = item.VolumeInfo.ImageLinks?.Thumbnail ?? "https://via.placeholder.com/150";

                output += $"<div class=\"card\" style=\"margin-bottom: 20px;\"><div class=\"row no-gutters\">" +
                          $"<div class=\"col-md-4\"><img src=\"{bookImg}\" class=\"card-img\" alt=\"Cover image of {title}\"></div>" +
                          $"<div class=\"col-md-8\"><div class=\"card-body\">" +
                          $"<h5 class=\"card-title\">{title}</h5>" +
                          $"<p class=\"card-text\"><strong>Author:</strong> {authors}</p>" +
                          $"<p class=\"card-text\"><strong>Publisher:</strong> {publisher}</p>" +
                          $"<p class=\"card-text\"><strong>Description:</strong> {description}</p>" +
                          $"<a target=\"_blank\" href=\"{bookLink}\" class=\"btn btn-secondary\">Read Book</a>" +
                          $"</div></div></div></div>";
            }
            return output;
        }
    }
    public class GoogleBooksResult
    {
        public GoogleBook[] Items { get; set; }
    }
    public class GoogleBook
    {
        public VolumeInfo VolumeInfo { get; set; }
    }
    public class VolumeInfo
    {
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public string Publisher { get; set; }
        public ImageLinks ImageLinks { get; set; }
        public string PreviewLink { get; set; }
        public string Description { get; set; }
    }
    public class ImageLinks
    {
        public string Thumbnail { get; set; }
    }
}
