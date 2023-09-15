using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoPubligrafit.Data;
using ProyectoPubligrafit.Models;
using System.Collections.Generic;

using System.Data;

using Microsoft.AspNetCore.Authorization;


[Authorize]

public class UsuarioController : Controller
    {
    

    public readonly ApplicationDbContext _context;
        //Creamos el contructor
        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            return View();


        }

        public IActionResult Algo()
        {

            return View();


        }


    public IActionResult Incorrecto()
    {

        return View();


    }

    public async Task<IActionResult> LoginA(Usuario model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _context.Usuario.SingleOrDefault(u => u.email == model.email && u.contrasena == model.contrasena);
                if (usuario != null)
                {

               

                    if (!usuario.estado)
                    {
                        return RedirectToAction("Index", "Usuario");
                    }
                    else
                    {
                        // El usuario tiene un estado "false," redirige a una página de no autorizado.
                        return RedirectToAction("algo", "Usuario");
                    }
                }
                else
                {
                    // Usuario no encontrado, redirige a una página de no autorizado.
                    return RedirectToAction("NoLogin", "Home");
                }


                return RedirectToAction("NoLogin", "Home");

                //ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
            }
            else
            {
                return View(model);
            }


        }




        public ActionResult Listar()
        {


            //var resul = (from data in _context.Usuario
            //             select new Usuario
            //             {
            //                 id_usuario = data.id_usuario,
            //                 fk_rol2 = data.fk_rol2,
            //                 nombres = data.nombres,
            //                 apellidos = data.apellidos,
            //                 email = data.email,
            //                 contrasena = data.contrasena,
            //                 rol = _context.Rol.Where(x => x.id_rol == data.fk_rol2).FirstOrDefault(),
            //                 //rol=_context.rol.where(x=>x.id_rol ==data.fk_rol2).firstordefault(),
            //             }
            //             );
            //return View(resul);


        //Creamos lista
        IEnumerable<Usuario> Listusuario = _context.Usuario;
         return View(Listusuario);

        }

        [HttpPost]
        public IActionResult CambiarEstado(int productoId, bool nuevoEstado)
        {
            var producto = _context.Usuario.Find(productoId);

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
            var resultado = _context.Usuario
                .Where(p => p.nombres.Contains(consulta))
                .ToList();

            return View("Search", resultado);
        }

        //HTTP Get Create
        /// HTTP Get Create
        public IActionResult CreateA()
        {


        
            IEnumerable<Rol> ListRol = _context.Rol;
            return View(ListRol);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateA(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el email ya existe en la base de datos
                bool emailExists = _context.Usuario.Any(u => u.email == usuario.email);

                if (emailExists)
                {
                    // Mostrar un SweetAlert indicando que el email ya está registrado
                    return Content("<script> alert('Este correo esta ya registrado en la base de datos')</script>", "text/html");
                }

                // Si el email no está duplicado, guardar el usuario
                _context.Usuario.Add(usuario);
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
            var usuario = _context.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound("Hubo un error");
            }
            return View(usuario);
        }


        /// POST update
        [HttpPost]
        // para que no cargen la bd con basura - protección del formulario
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuario.Update(usuario);
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

            var usuario = _context.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound("Hubo un error");
            }

            _context.Usuario.Remove(usuario);
            _context.SaveChanges();

            return RedirectToAction("Listar"); // Redirige a la acción "Index" después de eliminar
        }

    }

