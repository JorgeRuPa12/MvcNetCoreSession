using Microsoft.AspNetCore.Mvc;
using MvcNetCoreSession.Extensions;
using MvcNetCoreSession.Helpers;
using MvcNetCoreSession.Models;

namespace MvcNetCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SessionMascotaObject(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Amador",
                        Raza = "Leon",
                        Edad = 48
                    };
                    HttpContext.Session.SetObject("MASCOTAOBJECT", mascota);
                    ViewData["MENSAJE"] = "Mascota como object almacenada";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    Mascota mascota = HttpContext.Session.GetObject<Mascota>("MASCOTAOBJECT");
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }

        public IActionResult SessionMascotaJson(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Simba",
                        Raza = "Leon",
                        Edad = 9
                    };
                    string jsonMascota = HelperJsonSession.SerializeObject(mascota);
                    //Almacenamos en session el string
                    HttpContext.Session.SetString("MASCOTA", jsonMascota);
                    ViewData["MENSAJE"] = "Mascota JSON almacenada";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    string json = HttpContext.Session.GetString("MASCOTA");
                    Mascota mascota = HelperJsonSession.DeserializeObject<Mascota>(json);
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }

        public IActionResult SessionMascota(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Toby",
                        Raza = "Negro",
                        Edad = 1964
                    };
                    byte[] data = HelperBinarySession.ObjectToByte(mascota);
                    HttpContext.Session.Set("MASCOTA", data);
                    ViewData["MENSAJE"] = "Mascota almacenada en session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTA");
                    Mascota mascota = (Mascota) HelperBinarySession.ByteToObject(data);
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }

        public IActionResult SessionSimple(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //Guardamos datos en session
                    HttpContext.Session.SetString("nombre", "Programeitor");
                    HttpContext.Session.SetString("hora",
                        DateTime.Now.ToLongTimeString());
                    ViewData["MENSAJE"] = "Datos almacenados en session";
                }else if (accion.ToLower() == "mostrar")
                {
                    ViewData["USUARIO"] = HttpContext.Session.GetString("nombre");
                    ViewData["HORA"] = HttpContext.Session.GetString("hora");
                }
            }
            return View();
        }

        public IActionResult SessionCollection(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    List<Mascota> mascotas = new List<Mascota>
                    {
                        new Mascota {Nombre = "Tussi", Raza = "Agapornis", Edad = 12},
                        new Mascota {Nombre = "Toby", Raza = "Nigga", Edad = 28},
                        new Mascota {Nombre = "Vinicius", Raza = "Macaco", Edad = 37},
                        new Mascota {Nombre = "Macarena", Raza = "Mujer", Edad = 2}
                    };
                    byte[] data = HelperBinarySession.ObjectToByte(mascotas);
                    HttpContext.Session.Set("MASCOTAS", data);
                    ViewData["MENSAJE"] = "Datos almacenados en session";
                    return View();
                }
                else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTAS");
                    List<Mascota> mascotas = (List<Mascota>)HelperBinarySession.ByteToObject(data);
                    return View(mascotas);
                }
            }
            return View();
        }
    }
}
