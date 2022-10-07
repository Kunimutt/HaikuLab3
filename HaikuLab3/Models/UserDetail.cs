

using System.ComponentModel.DataAnnotations;

namespace HaikuLab3.Models
{
    public class UserDetail
    {
        // Konstruktor

        public UserDetail() { }


        // Publika egenskaper

        public int Us_Id { get; set; }

        [Required(ErrorMessage = "Förnamn krävs.")]
        public string Us_Fname { get; set; }

        [Required(ErrorMessage = "Efternamn krävs.")]
        public string Us_Lname { get; set; }

        [Required(ErrorMessage = "Alias krävs.")]
        public string Us_Alias { get; set; }

        [Required(ErrorMessage = "Födelseår krävs.")]
        [Range(1910,2014, ErrorMessage = "Ange ett giltigt födelseår. Du måste vara minst 8 år för att kunna skapa en användare.")]
        public int? Us_Age { get; set; }

        [Required(ErrorMessage = "Email krävs.")]
        public string Us_Email { get; set; }

        public string? Us_Description { get; set; }
    }
}
