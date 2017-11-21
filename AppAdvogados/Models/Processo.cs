using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppAdvogados.Models
{
    public class Processo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }
        [Required]
        [Display(Name = "Causa")]
        public string Causa { get; set; }
    }
}