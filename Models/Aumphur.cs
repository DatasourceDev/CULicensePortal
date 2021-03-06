using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CULCS.Models
{
    public class Aumphur
    {
        [Key]
        public int AumphurID { get; set; }
        public string AumphurCode { get; set; }
        public string AumphurName { get; set; }
        public string AumphurNameEn { get; set; }
        public string ProvinceCode { get; set; }
        public int? ProvinceID { get; set; }

        public virtual Province Province { get; set; }
        //public virtual Geography Geography { get; set; }

        //public virtual ICollection<Tumbon> Tumbons { get; set; }
    }
}
