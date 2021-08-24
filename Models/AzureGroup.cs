using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace CULCS.Models
{
    public class AzureGroup
    {
        [Key]
        public int ID { get; set; }
        public string AzureGroupId { get; set; }
        public string AzureGroupName { get; set; }

        public string TempAzureGroupId { get; set; }
        public string TempAzureGroupName { get; set; }
        public string GroupName { get; set; }
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
