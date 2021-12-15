using System;
using System.ComponentModel.DataAnnotations;

namespace VinylAPI.Models
{
    public class RegisterUserDto
    {
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Nick { get; set; }
        public DateTime BirthDay { get; set; }
        public string Name { get; set; }
        public string SurrName { get; set; }
    }
}
