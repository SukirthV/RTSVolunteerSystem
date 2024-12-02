using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RTSVolunteerSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public String volunteerEmail = "";
        public String volunteerFullname = "";
        public String volunteerId = "";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            String cookieemail = Request.Cookies["email"];
            if (cookieemail != null)
            {
                volunteerEmail = cookieemail;
            }
            String cookiefullname = Request.Cookies["fullname"];
            if (cookiefullname != null)
            {
                volunteerFullname = cookiefullname;
            }
            String cookieid = Request.Cookies["id"];
            if (cookieid != null)
            {
                volunteerId = cookieid;
            }
        }
    }

}
