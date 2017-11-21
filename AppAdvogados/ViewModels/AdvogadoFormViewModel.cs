using AppAdvogados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAdvogados.ViewModels
{
    public class AdvogadoFormViewModel
    {

        public Advogado Advogado { get; set; }
        public string Titulo
        {
            get
            {
                if (Advogado != null && Advogado.Id != 0)
                    return "Editar Cliente";

                return "Novo Cliente";
            }
        }

    }
}