using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppAdvogados.Models;

namespace AppAdvogados.ViewModels
{
    public class ClienteFormViewModel
    {

        public Cliente Cliente { get; set; }
        public string Titulo
        {
            get
            {
                if (Cliente != null && Cliente.Id != 0)
                    return "Editar Cliente";

                return "Novo Cliente";
            }
        }
    }
}