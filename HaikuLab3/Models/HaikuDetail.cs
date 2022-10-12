using System.ComponentModel.DataAnnotations;

namespace HaikuLab3.Models
{
    public class HaikuDetail
    {
        public HaikuDetail() { }


        [Required(ErrorMessage = "Unik titel krävs")]
        public string Ha_Title { get; set; }
       
        [Required(ErrorMessage = "Unik haiku krävs")]
        public string Ha_Content { get; set; }

        public string Ha_Content1 { get; set; }
        public string Ha_Content2 { get; set; }
        public string Ha_Content3 { get; set; }

        public string Ha_Alias { get; set; }

        //public DateTime Ha_Date { get; set; } // osäker på denna

        //public byte? Ha_Photo { get; set; } // osäker på om det ska vara string

        public string Ha_Genre {get; set; }
    }
}
