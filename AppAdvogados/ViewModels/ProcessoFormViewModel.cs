using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppAdvogados.Models;

namespace AppAdvogados.ViewModels
{
    public class ProcessoFormViewModel
    {

        public Processo Processo { get; set; }
        public string Titulo
        {
            get
            {
                if (Processo != null && Processo.Id != 0)
                    return "Editar Processos";

                return "Novo Processo";
            }


        }

    }
}