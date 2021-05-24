using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechnicalExam.Repository.Entity
{
    public class Transactions
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SourceAccountId { get; set; }

        [Required]
        public int DestinationAccountId { get; set; }

        [Required]
        public double TransferAmount { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
