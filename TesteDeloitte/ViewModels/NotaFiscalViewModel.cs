using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteDeloitte.Models
{
    public class NotaFiscalViewModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; }
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public decimal Valor { get; set; }
        public string Observacao { get; set; }

        public virtual List<NotaFiscalViewModel> Notas { get; set; }
    }
}