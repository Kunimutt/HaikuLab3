namespace HaikuLab3.Models
{
    public class HaikuDetail
    {
        public HaikuDetail() { }



        public int Ha_Id { get; set; }

        public string Ha_Title { get; set; }

        public string Ha_Content { get; set; }

        public int Ha_Author { get; set; }

        //public DateTime Ha_Date { get; set; } // osäker på denna

        public string Ha_Photo { get; set; } // osäker på om det ska vara string
    }
}
