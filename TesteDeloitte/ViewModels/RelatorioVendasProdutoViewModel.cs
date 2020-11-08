using System.Collections.Generic;

namespace TesteDeloitte.Models
{
    public class RelatorioVendasProdutoViewModel
    {
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public decimal QtdeVendas { get; set; }
        public decimal TotalVendas { get; set; }

        public virtual List<RelatorioVendasProdutoViewModel> Lista { get; set; }
    }
}