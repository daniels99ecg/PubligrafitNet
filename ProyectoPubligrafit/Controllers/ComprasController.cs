using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoPubligrafit.Data;
using ProyectoPubligrafit.Models;

namespace ProyectoPubligrafit.Controllers
{
    [Authorize]
    public class ComprasController : Controller
    {
        public readonly ApplicationDbContext _context;
        //Creamos el contructor
        public ComprasController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            IEnumerable<Compras> ListCompras = _context.Compras;
            return View(ListCompras);



        }

        [HttpPost]
        public IActionResult CambiarEstado(int productoId, bool nuevoEstado)
        {
            var producto = _context.Compras.Find(productoId);

            if (producto != null)
            {
                producto.estado = nuevoEstado;
                _context.SaveChanges();
            }

            return Json(new { success = true }); // Puedes devolver una respuesta JSON si es necesario
        }

        public ActionResult Search(int? id)
        {

            Compras compra = _context.Compras.FirstOrDefault(p => p.id_compra == id.Value);


            return View("Search", compra);
        }

        //HTTP Get Create
        /// HTTP Get Create
        public IActionResult CreateA()
        {
            IEnumerable<Insumos> ListInsumos = _context.Insumos;
            return View(ListInsumos);
            
        }

        /// POST Create
        [HttpPost]
        // para que no cargen la bd con basura - protección del formulario
        [ValidateAntiForgeryToken]
        public IActionResult CreateA(Compras Compras)
        {
            if (ModelState.IsValid)
            {
                _context.Compras.Add(Compras);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirige a la acción "Index"
            }
            return View();
        }


        /// get para editar
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            /// obtener el libro que queremos editar
            var compras = _context.Compras.Find(id);
            if (compras == null)
            {
                return NotFound("Hubo un error");
            }
            return View(compras);
        }


        /// POST update
        [HttpPost]
        // para que no cargen la bd con basura - protección del formulario
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Compras Compras)
        {
            if (ModelState.IsValid)
            {
                _context.Compras.Update(Compras);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirige a la acción "Index"
            }
            return View();
        }



        // GET para eliminar directamente
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var compras = _context.Compras.Find(id);
            if (compras == null)
            {
                return NotFound("Hubo un error");
            }

            _context.Compras.Remove(compras);
            _context.SaveChanges();

            return RedirectToAction("Index"); // Redirige a la acción "Index" después de eliminar
        }
    }
}
