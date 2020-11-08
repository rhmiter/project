using System.Web.Mvc;
using TesteDeloitte.Data;
using TesteDeloitte.Models;

namespace TesteDeloitte.Controllers
{
    public class NotaFiscalController : Controller
    {
        private readonly DataAccessRepository _dataAccessRepository;

        public NotaFiscalController()
        {
            _dataAccessRepository = new DataAccessRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            NotaFiscalViewModel objCustomer = new NotaFiscalViewModel();
            objCustomer.Notas = _dataAccessRepository.SelectAllNotaFiscal();
            return View(objCustomer);
        }

        public ActionResult Insert()
        {
            CarregarViewBag();
            return View();
        }

        [HttpPost]  
        public ActionResult Insert(NotaFiscal nota)
        {
            if (ModelState.IsValid) 
            {
                _dataAccessRepository.InsertNotaFiscal(nota);
                ModelState.Clear(); 
                return RedirectToAction("Index");
            }

            else
            {
                CarregarViewBag();
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            NotaFiscalViewModel objCustomer = new NotaFiscalViewModel();
            objCustomer = _dataAccessRepository.SelectNotaFiscalByID(Id);
            CarregarViewBag();
            return View(objCustomer);
        }

        [HttpPost]
        public ActionResult Edit(NotaFiscal nota)
        {
            if (ModelState.IsValid)
            {
                _dataAccessRepository.UpdateNotaFiscal(nota);
                ModelState.Clear();
                return RedirectToAction("Index");
            }

            else
            {
                CarregarViewBag();
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(string Id)
        {
            _dataAccessRepository.DeleteNotaFiscal(Id); 
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Report()
        {
            RelatorioVendasProdutoViewModel objCustomer = new RelatorioVendasProdutoViewModel();
            objCustomer.Lista = _dataAccessRepository.SelectReportVendasProduto();
            return View(objCustomer);
        }

        private void CarregarViewBag()
        {
            ViewBag.ListaClientes = _dataAccessRepository.SelectAllClientes();
            ViewBag.ListaProdutos = _dataAccessRepository.SelectAllProdutos();
        }
    }
}