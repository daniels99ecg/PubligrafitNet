using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPubligrafit.Data;
using ProyectoPubligrafit.Models;

namespace ProyectoPubligrafit.Controllers
{
    [Authorize]
    public class InsumosController : Controller
    {
        // GET: InsumosController
        public readonly ApplicationDbContext _context;
        //Creamos el contructor
        public InsumosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //Creamos lista
            IEnumerable<Insumos> ListInsumos = _context.Insumos;
            return View(ListInsumos);

            //var result = _context.Usuario.Join(_context.Rol, dir => dir.fk_rol2, per => per.id_rol, (dir, per) => new { dir, per }).FirstOrDefault(x => x.dir.id_usuario == 1);

            //return Json(_context.Usuario.ToList());
        }

        [HttpPost]
        public IActionResult CambiarEstado(int productoId, bool nuevoEstado)
        {
            var producto = _context.Insumos.Find(productoId);

            if (producto != null)
            {
                producto.estado = nuevoEstado;
                _context.SaveChanges();
            }

            return Json(new { success = true }); // Puedes devolver una respuesta JSON si es necesario
        }

    [HttpPost]
        public ActionResult Search(string consulta)
        {
            var resultado = _context.Insumos
                .Where(p => p.nombre.Contains(consulta))
                .ToList();

            return View("Search", resultado);
        }



        //HTTP Get Create
        /// HTTP Get Create
        public IActionResult CreateA()
        {
            return View();
        }

        /// POST Create
        [HttpPost]
        // para que no cargen la bd con basura - protección del formulario
        [ValidateAntiForgeryToken]
        public IActionResult CreateA(Insumos insumos)
        {
            if (ModelState.IsValid)
            {
                _context.Insumos.Add(insumos);
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
            var insumos = _context.Insumos.Find(id);
            if (insumos == null)
            {
                return NotFound("Hubo un error");
            }
            return View(insumos);
        }


        /// POST update
        [HttpPost]
        // para que no cargen la bd con basura - protección del formulario
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Insumos insumos)
        {
            if (ModelState.IsValid)
            {
                _context.Insumos.Update(insumos);
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

            var insumos = _context.Insumos.Find(id);
            if (insumos == null)
            {
                return NotFound("Hubo un error");
            }

            _context.Insumos.Remove(insumos);
            _context.SaveChanges();

            return RedirectToAction("Index"); // Redirige a la acción "Index" después de eliminar
        }
    }
}
