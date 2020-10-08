
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Vitals
    {
        [Key]
        public Guid Mrn { get; set; }
        public float Bpm { get; set; }
        public float Spo2 { get; set; }
        public float RespRate { get; set; }
    }
}


