using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace E_G_FinalProject.Models.Entities
{
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }

        [MaxLength(12)]
        [Required( ErrorMessage = "This field is Required ")]
        [DisplayName("AccountNumber")]
        [Column(TypeName ="nvarchar(12)")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "This field is Required ")]
        [DisplayName("AccountName")]
        [Column(TypeName = "nvarchar(50)")]
        public string AccountName { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is Required ")]
        [DisplayName("BankName")]
        [Column(TypeName = "nvarchar(50)")]
        public string BankName { get; set; }

        [MaxLength(11)]
        [Required(ErrorMessage = "This field is Required ")]
        [DisplayName("SWIFTCode")]
        [Column(TypeName = "nvarchar(11)")]
        public string SWIFTCode { get; set; }

        [Required(ErrorMessage = "This field is Required ")]
        [DisplayName("Amount")]
        public string Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
