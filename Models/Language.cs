using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CULCS.Models
{
    public class Language
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "ภาษา")]       
        public LanguageCode LanguageCode { get; set; }

        [Required]
        [Display(Name = "คีย์")]
        public string LanguageKey { get; set; }

        [Required]
        [Display(Name = "Page")]
        public Page Page { get; set; }

        [Required]
        [Display(Name = "ภาษา")]
        public string LanguageValue { get; set; }

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

    }
}
