using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPubligrafit.Data;
using ProyectoPubligrafit.Models;

namespace ProyectoPubligrafit.Controllers
{
    [Authorize]
    public class RolController : Controller
    {
        public readonly ApplicationDbContext _context;
        //Creamos el contructor
        public RolController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //Creamos lista
            IEnumerable<Rol> Listrol = _context.Rol;
            return View(Listrol);
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
        public IActionResult CreateA(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.Rol.Add(rol);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirige a la acción "Index"
            }
            return View();
        }


        // GET: RolController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    


        // GET para eliminar directamente
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var rol = _context.Rol.Find(id);
            if (rol == null)
            {
                return NotFound("Hubo un error");
            }

            _context.Rol.Remove(rol);
            _context.SaveChanges();

            return RedirectToAction("Index"); // Redirige a la acción "Index" después de eliminar
        }

    }
}
