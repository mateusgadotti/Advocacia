using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppAdvogados.Models;

namespace AppAdvogados.ViewModels
{
    public class VaraFormViewModel
    {
        public Vara Vara { get; set; }
        public string Titulo
        {
            get
            {
                if (Vara != null && Vara.Id != 0)
                    return "Editar Vara";

                return "Nova Vara";
            }
        }

    }
}