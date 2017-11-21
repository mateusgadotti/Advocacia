using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppAdvogados.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "CPF")]
        public string CPF { get; set; }
        [Required]
        [Display(Name = "Idade")]
        public int Idade { get; set; }


    }
}