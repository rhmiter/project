using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteDeloitte.Models
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public int FornecedorId { get; set; }
        public string Nome { get; set; }
        public string NomeCompleto { get; set; }
    }
}