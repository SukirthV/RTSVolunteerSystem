using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RTSVolunteerSystem.Pages.Register;
using System.Web;


namespace RTSVolunteerSystem.Pages.Login
{

    public class IndexModel : PageModel
    {
        public VolunteerInfo volunteerInfo = new VolunteerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            volunteerInfo.email = Request.Form["email"];
            volunteerInfo.password = Request.Form["password"];

            if (volunteerInfo.email.Length == 0 || volunteerInfo.password.Length == 0)

            {
                errorMessage = "All the fields are required";
                return;
            }
            //check user login info
            try
            {
                String connectionString = "Data Source=.\\sqlexpress01;Initial Catalog=RTSVolunteerSystemDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM volunteers " +
                                 "WHERE email=@email AND password=@password";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", volunteerInfo.email);
                        command.Parameters.AddWithValue("@password", volunteerInfo.password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                errorMessage = "Failed to Login - Please Provide Correct Email and Password";
                                return;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = "Logged In Successfully";
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(5);
            Response.Cookies.Append("email", volunteerInfo.email, cookies);
            Response.Cookies.Append("password", volunteerInfo.password, cookies);

            volunteerInfo.email = "";
            volunteerInfo.password = "";
            Response.Redirect("/Index");

        }
    }



    public class VolunteerInfo
    {
        public String id;
        public String fullname;
        public String email;
        public String password;
        public String phone;
        public String address;
        public String ecname;
        public String ecphone;
        public String dob;
        public String availablefrom;
        public String availableto;
        public String comments;
        public String created_at;
    }
}
