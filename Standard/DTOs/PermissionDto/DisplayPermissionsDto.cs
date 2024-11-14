using Standard.DTOs.ItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs.PermissionDto
{
    public class DisplayPermissionsDto
    {
        public int PermId { get; set; }
        public string PermCode { get; set; } = null!;
        public int PermTypeFk { get; set; }
        public string PerTypeValueAr { get; set; } = null!;

        public string PerTypeValueEn { get; set; } = null!;
        //public int SubId { get; set; }
        public string SubNameEn { get; set; } = null!;

        public string SubNameAr { get; set; } = null!;
        //public int? DestinationSubId { get; set; }
        public string? DestinationSubNameEn { get; set; }
        public string? DestinationSubNameAr { get; set; }

        public List<PermissionItemDto> Items { get; set; }
        public int ItemCount { get; set; }
        public DateTime? PermCreatedat { get; set; } = DateTime.Now;
    }
}
