using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RTSVolunteerSystem.Pages.Teams;
using System.Data;
using System.Globalization;


namespace RTSVolunteerSystem.Pages.LogHours
{
    public class IndexModel : PageModel
    {
        public HoursInfo hoursInfo = new HoursInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String cookieid = Request.Cookies["id"];
            if (cookieid != null)
            {
                hoursInfo.volunteerid = cookieid;
            }
        }
        /*public void populateteamsselect()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress01;Initial Catalog=RTSVolunteerSystemDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT teamnames FROM teams";
                    SqlDataAdapter sda = new SqlDataAdapter(sql, connection);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    teamnameselect.DataSource = dt;
                    teamnameselect.DataTextField = "teamname";
                    teamnameselect.DataValueField = "teamname";
                    teamnameselect.DataBild();
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }*/

        public void OnPost()
        {
            hoursInfo.volunteerid = Request.Form["volunteerid"];
            hoursInfo.date = Request.Form["date"];
            hoursInfo.teamname = Request.Form["teamname"];
            hoursInfo.hours = Request.Form["hours"];
            hoursInfo.description = Request.Form["description"];

            if (hoursInfo.volunteerid.Length == 0 || hoursInfo.date.Length == 0 || hoursInfo.teamname.Length == 0 ||
                hoursInfo.hours.Length == 0 || hoursInfo.description.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            //save new team into batabase
            try
            {
                String connectionString = "Data Source=.\\sqlexpress01;Initial Catalog=RTSVolunteerSystemDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO hours " +
                                 "(volunteerid, date, teamname, hours, description) VALUES " +
                                 "(@volunteerid, @date, @teamname, @hours, @description);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@volunteerid", hoursInfo.volunteerid);
                        command.Parameters.AddWithValue("@date", hoursInfo.date);
                        command.Parameters.AddWithValue("@teamname", hoursInfo.teamname);
                        command.Parameters.AddWithValue("@hours", hoursInfo.hours);
                        command.Parameters.AddWithValue("@description", hoursInfo.description);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            hoursInfo.date = "";
            hoursInfo.teamname = "";
            hoursInfo.hours = "";
            hoursInfo.description = "";
            successMessage = "New Log Added";

        }

        public class HoursInfo
        {
            public String volunteerid;
            public String volunteername;
            public String date;
            public String teamname;
            public String hours;
            public String description;
            public String created_at;
        }
    }
}
