using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CULCS.Models
{
    public class ProgramLicense
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int? AzureGroupID { get; set; }

        public AzureGroup AzureGroup { get; set; }

        [Required]
        public string ProgramName { get; set; }

        public bool ChulaDomain { get; set; }
        public bool StudentChulaDomain { get; set; }
        public bool AlumniChulaDomain { get; set; }

        public int MaxBorrowAdvance { get; set; }

        public int MaxBorrow { get; set; }

        public PeriodType PeriodType { get; set; }

        public Status Status { get; set; }

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
