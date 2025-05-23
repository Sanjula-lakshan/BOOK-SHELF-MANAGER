using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Author { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$", 
            ErrorMessage = "Invalid ISBN format")]
        public string ISBN { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }
    }
}