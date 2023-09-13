using Microsoft.AspNetCore.Mvc;
using ProyectoPubligrafit.Data;
using ProyectoPubligrafit.Models;

namespace ProyectoPubligrafit.Controllers
{
    public class Ficha_TecnicaController : Controller
    {

            public readonly ApplicationDbContext _context;
            //Creamos el contructor
            public Ficha_TecnicaController(ApplicationDbContext context)
            {
                _context = context;
            }
            public IActionResult Index()
            {
                //Creamos lista
                IEnumerable<Ficha_Tecnica> ListFicha = _context.Ficha_Tecnica;
                return View(ListFicha);

            }

        [HttpPost]
        public IActionResult CambiarEstado(int productoId, bool nuevoEstado)
        {
            var producto = _context.Ficha_Tecnica.Find(productoId);

            if (producto != null)
            {
                producto.estado = nuevoEstado;
                _context.SaveChanges();
            }

            return Json(new { success = true }); // Puedes devolver una respuesta JSON si es necesario
        }

        public ActionResult Search(int? id)
        {

            Ficha_Tecnica cliente = _context.Ficha_Tecnica.FirstOrDefault(p => p.id_ft == id.Value);


            return View("Search", cliente);
        }

        //HTTP Get Create
        /// HTTP Get Create
        public IActionResult CreateA()
            {
            IEnumerable<Insumos> ListInsumo = _context.Insumos;
            return View(ListInsumo);
        }

            /// POST Create
            [HttpPost]
            // para que no cargen la bd con basura - protección del formulario
            [ValidateAntiForgeryToken]
            public IActionResult CreateA(Ficha_Tecnica fichaTecnica)
            {
                if (ModelState.IsValid)
                {
                    _context.Ficha_Tecnica.Add(fichaTecnica);
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
                var ficha = _context.Ficha_Tecnica.Find(id);
                if (ficha == null)
                {
                    return NotFound("Hubo un error");
                }
                return View(ficha);
            }


            /// POST update
            [HttpPost]
            // para que no cargen la bd con basura - protección del formulario
            [ValidateAntiForgeryToken]
            public IActionResult Edit(Ficha_Tecnica fichaTecnica)
            {
                if (ModelState.IsValid)
                {
                    _context.Ficha_Tecnica.Update(fichaTecnica);
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

                var ficha = _context.Ficha_Tecnica.Find(id);
                if (ficha == null)
                {
                    return NotFound("Hubo un error");
                }

                _context.Ficha_Tecnica.Remove(ficha);
                _context.SaveChanges();

                return RedirectToAction("Index"); // Redirige a la acción "Index" después de eliminar
            }

        }
    }
