using System.ComponentModel.DataAnnotations;
using System.Web;

namespace News.Models
{
    public class EmailFormModel
    {
        [Required, Display(Name = "Your name: ")]
        public string FromName { get; set; }
        [Required, Display(Name = "Your email: "), EmailAddress]
        public string FromEmail { get; set; }
        [Required, Display(Name = "To email: ")]
        public string ToEmail { get; set; }
        [Required, Display(Name = "Add your message: ")]
        public string Message { get; set; }
        public HttpPostedFileBase Upload { get; set; }
    }
}