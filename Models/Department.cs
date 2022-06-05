using System.ComponentModel.DataAnnotations;

namespace LWAApi.Models
{
    public class Department
    {
        [Key]
      public int  DepID { get; set; }
        public string? Depname { get; set; }
    }
}
