using System.Collections.Generic;

namespace WebApplication2.Models
{
    public class UserVacation
    {
        public UserVacation()
        {
           
            Suitcases = new HashSet<Suitcase>();
        }
        //IdUser, Login, Email, HashedPassword, RefreshToken
        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string RefreshToken { get; set; }
       public virtual ICollection<Suitcase> Suitcases{ get; set; }
    }
}