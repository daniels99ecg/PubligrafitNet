using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoPubligrafit.Data;
using ProyectoPubligrafit.Models;
using System.Linq;


namespace ProyectoPubligrafit.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cliente
        public IActionResult Index()
        {
            IEnumerable<Cliente> listCliente = _context.Cliente;
            return View(listCliente);
        }

        //[HttpPost]
        //public IActionResult CambiarEstado(int productoId, bool nuevoEstado)
        //{
        //    var producto = _context.Cliente.Find(productoId);

        //    if (producto != null)
        //    {
        //        producto.estado = nuevoEstado;
        //        _context.SaveChanges();
        //    }

        //    return Json(new { success = true }); // Puedes devolver una respuesta JSON si es necesario
        //}

        public ActionResult Search(int? id)
        {
          
            Cliente cliente = _context.Cliente.FirstOrDefault(p => p.dni_cliente == id.Value);


            return View("Search", cliente);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el dni_cliente ya existe en la base de datos
                bool dniExists = _context.Cliente.Any(c => c.dni_cliente == cliente.dni_cliente);

                // Verificar si el email ya existe en la base de datos
                bool emailExists = _context.Cliente.Any(c => c.email == cliente.email);

                if (dniExists)
                {
                    // Mostrar un SweetAlert indicando que el DNI ya está registrado
                    return View();
                }

                if (emailExists)
                {
                    // Mostrar un SweetAlert indicando que el email ya está registrado
                    return View();
                }

                // Si el DNI y el email no están duplicados, guardar el cliente
                _context.Cliente.Add(cliente);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirige a la acción "Index"
            }
            return View();
        }

        // GET: Cliente/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            /// obtener el libro que queremos editar
            var cliente = _context.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound("Error");
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Cliente.Update(cliente);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirige a la acción "Index"
            }
            return View();
        }

        // GET: Cliente/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var cliente = _context.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound("Hubo un error");
            }

            _context.Cliente.Remove(cliente);
            _context.SaveChanges();

            return RedirectToAction("Index"); // Redirige a la acción "Index" después de eliminar
        }
    }
}
