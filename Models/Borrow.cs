using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CULCS.Models
{
    public class Borrow
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int? ProgramLicenseID { get; set; }

        [Required]
        public string Domain { get; set; }

        public ProgramLicense ProgramLicense { get; set; }


        public DateTime? BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public BorrowStatus BorrowStatus { get; set; }

        [NotMapped]
        public string BorrowDateStr { get; set; }

        [NotMapped]
        public string ReturnDateStr { get; set; }

        [NotMapped]
        public string ExpiryDateStr { get; set; }

        public string AzureUserId { get; set; }

        public string UserPrincipalName { get; set; }

        public string DisplayName { get; set; }

        #region tracking
        [Display(Name = "ผู้สร้าง")]
        [MaxLength(250)]
        public string Create_By { get; set; }
        [Display(Name = "เวลาสร้าง")]
        public Nullable<DateTime> Create_On { get; set; }
        [Display(Name = "ผู้แก้ไข")]
        [MaxLength(250)]
        public string Update_By { get; set; }
        [Display(Name = "เวลาแก้ไข")]
        public Nullable<DateTime> Update_On { get; set; }
        #endregion

    }
}
