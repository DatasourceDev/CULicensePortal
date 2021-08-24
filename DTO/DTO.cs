using CULCS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CULCS.DTO
{
    public class SearchDTO
    {
        public string text_search { get; set; }
        public bool delete_suggest { get; set; }

        public Status? status_search { get; set; }
        public BorrowStatus? borrow_status_search { get; set; }
        public string dfrom { get; set; }
        public string domain_search { get; set; }
        public int? program_search { get; set; }
        public string dto { get; set; }
        public string logaction { get; set; }
        public string ou { get; set; }

        private int _pageno; // field
        public int pageno
        {
            get
            {
                if (_pageno == 0)
                    return 1;
                return _pageno;
            }
            set { _pageno = value; }
        }
        private int _pagelen; // field
        public int pagelen
        {
            get
            {
                if (_pagelen == 0)
                    return 1;
                return _pagelen;
            }
            set { _pagelen = value; }
        }

        public int itemcnt { get; set; }

        public ReturnCode? code { get; set; }
        public string msg { get; set; }

        public IQueryable<object> lists { get; set; }
    }


    public class LoginDTO : BaseDTO
    {
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(100)]
        public string Password { get; set; }
    }
    public class ChangePasswordDTO
    {
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,12}$", ErrorMessage = "รหัสผ่านต้องประกอบด้วยตัวเลขและตัวอักษร ความยาวต้องไม่น้อยกว่า 8 ตัวและไม่เกิน 12 ตัว")]
        [StringLength(12, ErrorMessage = "รหัสผ่านต้องไม่น้อยกว่า {2} ตัวและไม่เกิน {1} ตัว", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "รหัสผ่านไม่ตรงกัน")]
        public string ConfirmPassword { get; set; }
    }
    public class ChangePassword2DTO : BaseDTO
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,12}$", ErrorMessage = "รหัสผ่านต้องประกอบด้วยตัวเลขและตัวอักษร ความยาวต้องไม่น้อยกว่า 8 ตัวและไม่เกิน 12 ตัว")]
        [StringLength(12, ErrorMessage = "รหัสผ่านต้องไม่น้อยกว่า {2} ตัวและไม่เกิน {1} ตัว", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "รหัสผ่านไม่ตรงกัน")]
        public string ConfirmPassword { get; set; }
    }
    public class ChangePassword3DTO
    {
        [Required]
        public string sAMAccountName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,12}$", ErrorMessage = "รหัสผ่านต้องประกอบด้วยตัวเลขและตัวอักษร ความยาวต้องไม่น้อยกว่า 8 ตัวและไม่เกิน 12 ตัว")]
        [StringLength(12, ErrorMessage = "รหัสผ่านต้องไม่น้อยกว่า {2} ตัวและไม่เกิน {1} ตัว", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "รหัสผ่านไม่ตรงกัน")]
        public string ConfirmPassword { get; set; }
    }
    public class MacAddressDTO : BaseDTO
    {
        [MaxLength(17)]
        [Display(Name = "รหัสประจำอุปกรณ์ 1")]
        public string MacAddress1 { get; set; }
        [MaxLength(17)]
        [Display(Name = "รหัสประจำอุปกรณ์ 2")]
        public string MacAddress2 { get; set; }
        [MaxLength(17)]
        [Display(Name = "รหัสประจำอุปกรณ์ 3")]
        public string MacAddress3 { get; set; }

    }
    public class BaseDTO
    {
        public LanguageCode LanguageCode { get; set; }
        public IQueryable<Language> Languages { get; set; }

    }
    public class OTPDTO : BaseDTO
    {
        [Required]
        [MaxLength(6)]
        public string OTP { get; set; }
        public string RefNo { get; set; }
        public bool Renew { get; set; }

    }
    public class ForgotPasswordDTO : BaseDTO
    {
        [Required]
        [MaxLength(150)]
        public string GuestCode { get; set; }

        public SendMessageType SendMessageType { get; set; }
        //public Guest Guest { get; set; }


    }

    public class AzureAD
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
        public string Instance { get; set; }
        public string GraphResource { get; set; }
        public string GraphResourceEndPoint { get; set; }
    }
}
