using System.ComponentModel.DataAnnotations;

namespace LWAApi.Models
{
    public class Players
    {
        [Key]
        public int Playerid { get; set; }
        public string? Playername { get; set; }
        public int Teamid { get; set; }

    }
}
