using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RTSVolunteerSystem.Pages.Register;
namespace RTSVolunteerSystem.Pages.Volunteers
{
    public class EditModel : PageModel
    {
        public VolunteerInfo volunteerInfo = new VolunteerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress01;Initial Catalog=mystore;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                volunteerInfo.id = "" + reader.GetInt32(0);
                                volunteerInfo.fullname = reader.GetString(1);
                                volunteerInfo.email = reader.GetString(2);
                                volunteerInfo.phone = reader.GetString(3);
                                volunteerInfo.address = reader.GetString(4);
                                volunteerInfo.ecname = reader.GetString(5);
                                volunteerInfo.ecphone = reader.GetString(6);
                                volunteerInfo.dob = reader.GetString(7);
                                volunteerInfo.availablefrom = reader.GetString(8);
                                volunteerInfo.availableto = reader.GetString(9);
                                volunteerInfo.comments = reader.GetString(10);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            volunteerInfo.id = Request.Form["id"];
            volunteerInfo.fullname = Request.Form["name"];
            volunteerInfo.email = Request.Form["email"];
            volunteerInfo.phone = Request.Form["phone"];
            volunteerInfo.address = Request.Form["address"];
            volunteerInfo.ecname = Request.Form["ecname"];
            volunteerInfo.ecphone = Request.Form["ecphone"];
            volunteerInfo.dob = Request.Form["dob"];
            volunteerInfo.availablefrom = Request.Form["availablefrom"];
            volunteerInfo.availableto = Request.Form["availableto"];
            volunteerInfo.comments = Request.Form["comments"];

            if (volunteerInfo.id.Length == 0 || volunteerInfo.fullname.Length == 0 || volunteerInfo.email.Length == 0 ||
                volunteerInfo.phone.Length == 0 || volunteerInfo.address.Length == 0 || volunteerInfo.ecname.Length == 0 ||
                volunteerInfo.ecphone.Length == 0 || volunteerInfo.dob.Length == 0 || volunteerInfo.availablefrom.Length == 0 ||
                volunteerInfo.availableto.Length == 0 || volunteerInfo.comments.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress01;Initial Catalog=RTSVolunteerSystemDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients " +
                                 "SET name=@name, email=@email, phone=@phone, address=@address, ecname=@ecname, ecphone=@ecphone, " +
                                 "dob=@dob, availablefrom=@availablefrom, availablefrom=@availablefrom, comments=@comments" +
                                 "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", volunteerInfo.id);
                        command.Parameters.AddWithValue("@fullname", volunteerInfo.fullname);
                        command.Parameters.AddWithValue("@email", volunteerInfo.email);
                        command.Parameters.AddWithValue("@phone", volunteerInfo.phone);
                        command.Parameters.AddWithValue("@address", volunteerInfo.address);
                        command.Parameters.AddWithValue("@ecname", volunteerInfo.ecname);
                        command.Parameters.AddWithValue("@ecphone", volunteerInfo.ecphone);
                        command.Parameters.AddWithValue("@dob", volunteerInfo.dob);
                        command.Parameters.AddWithValue("@availablefrom", volunteerInfo.availablefrom);
                        command.Parameters.AddWithValue("@availableto", volunteerInfo.availableto);
                        command.Parameters.AddWithValue("@comments", volunteerInfo.comments);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Volunteers/Index");
        }
    }
}
