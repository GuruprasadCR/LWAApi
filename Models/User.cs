using System.ComponentModel.DataAnnotations;

namespace LWAApi.Models
{
    //[Obsolete("Its old ")]
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        
        public string? phonenumber { get; set; }
       
        public string? Gender { get; set; }
        public string? Password { get; set; }
        //public DateTime CreatedDate { get; set; }



        //public bool IsDeleted { get; set; }
        //public int CreatedUserId { get; set; }
        //public DateTime CreatedDateTime { get; set; }
        //public int ModifiedUserId { get; set; }
        //public DateTime ModifiedDateTime { get; set; }
    }
}
