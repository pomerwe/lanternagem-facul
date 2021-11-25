using lanternagem_api.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace lanternagem_api.Domain
{
    public class SystemUser : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public User User { get; set; }

        public object GetPrimaryKey()
        {
            return Id;
        }

        public bool CheckPassword(string password)
        {
            return Password.Equals(password);
        }
    }
}
