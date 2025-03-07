using ExamenZapasPartialViewsPaginacion.Models;
using ExamenZapasPartialViewsPaginacion.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamenZapasPartialViewsPaginacion.Controllers
{


    public class ZapasController : Controller
    {
        private ZapasRepository repo;
        public ZapasController(ZapasRepository repo) {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Zapas> zapas = await this.repo.GetZapasAsync();
            return View(zapas);
        }

        public async Task<IActionResult> Details(int id)
        {
            Zapas zapas = await this.repo.FindZapasIdAsync(id);
   
            return View(zapas);
        }

        public async Task<IActionResult> _Fotos(int id, int? pos)
        {
            if (pos == null)
            {
                pos = 1;
            }

            ModelZapasPagination model = await this.repo.GetModelImagenPagAsync(id, pos.Value);
            ViewData["POS"] = pos;
            return PartialView("_ZapasPartial", model);
        }
    }
}
