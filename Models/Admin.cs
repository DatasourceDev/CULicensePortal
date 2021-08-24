using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CULCS.Models
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "รหัสผู้ใช้")]
        [MaxLength(250)]
        public string AdminCode { get; set; }

        [Display(Name = "ชื่อ")]
        [MaxLength(250, ErrorMessage = "จำนวนอักษรไม่ควรเกิน 250 ตัวอักษร")]
        public string FirstName { get; set; }

        [Display(Name = "นามสกุล")]
        [MaxLength(250, ErrorMessage = "จำนวนอักษรไม่ควรเกิน 250 ตัวอักษร")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "สถานะการใช้งาน")]
        public Status Status { get; set; }

        [Display(Name = "ผู้ใช้งาน")]
        public int UserID { get; set; }      

        public User User { get; set; }


        [NotMapped]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

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
