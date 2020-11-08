using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace TesteDeloitte.Models
{
    public class NotaFiscal
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Cliente é obrigatório")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O campo Produto é obrigatório")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O campo Valor é obrigatório")]
        public decimal Valor { get; set; }

        public string Observacao { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Produto Produto { get; set; }
    }
}