using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CULCS.Models
{
    public class Setup
    {
        [Key]
        public int ID { get; set; }


        [Display(Name = "ข้อความหน้าแรก")]
        public string FirstPageDesc { get; set; }

        [Display(Name = "Admin Azure Group Name")]
        public string AdminAzureGroupName { get; set; }

        [Display(Name = "สิทธิ์ในการยืม")]
        public bool PaymentFilter { get; set; }

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
