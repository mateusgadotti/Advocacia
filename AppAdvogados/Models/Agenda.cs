using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace AppAdvogados.Models
{
    public class Agenda
    {

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Nome")]
        public string Tarefa { get; set; }
        [Required]
        [Display(Name = "Data")]
        public DateTime? Data { get; set; }
        [Required]
        public int AdvogadoId { get; set; }
        public Advogado Advogado { get; set; }

    }
}