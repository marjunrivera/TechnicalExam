using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechnicalExam.Repository.Entity
{
    public class Accounts
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        
        [Required]
        public double InitialBalance { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
