﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPubligrafit.Data;
using ProyectoPubligrafit.Models;
using System.Diagnostics;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;



namespace ProyectoPubligrafit.Controllers
{

  
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext _context;

        public object FormsAuthentication { get; private set; }

        //Creamos el contructor
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Login()
        {
            
            return View();  
        }

        [HttpPost]

        public async Task<IActionResult> LoginA(Usuario model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _context.Usuario.SingleOrDefault(u => u.email == model.email && u.contrasena == model.contrasena);
                if (usuario != null)
                {

                    var Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, usuario.email),
                    new Claim("email", usuario.email)

                };
                    var claimsIndentity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);


                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIndentity));



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
                    return RedirectToAction("Incorrecto", "Usuario");
                }
            

                return RedirectToAction("Incorrecto", "Usuario");

                //ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
            }
            else
            {
                return View(model);
            }

           
        }

        //public IActionResult LoginA(Usuario model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Consulta la base de datos para verificar las credenciales del usuario
        //        var usuario = _context.Usuario.SingleOrDefault(u => u.email == model.email && u.contrasena == model.contrasena);
        //        if (usuario != null)
        //        {
        //            // Inicia sesión y redirige al usuario a la página de inicio
        //            // Puedes usar ASP.NET Identity o cualquier otro sistema de autenticación que prefieras
        //            // Ejemplo con ASP.NET Identity: SignInManager.SignInAsync(user, isPersistent: false);
        //            return RedirectToAction("Index","Usuario");
        //        }

        //        //ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
        //        return RedirectToAction("NoLogin", "Home");
        //    }

        //    return View(model);
        //}
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }

        public IActionResult NoLogin()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}