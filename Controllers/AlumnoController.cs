using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CRUD_Alumnos.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            try
            {
                #region  Usando SQL Comentado
                //int edad = 18;
                //string SqlQuery = @"select a.Id,a.Nombres,a.Apellidos,a.edad,a.Sexo,a.FechaRegistro, c.Nombre as NombreCiudad 
                //from Alumno a
                //inner
                //join Ciudad c on c.Id = a.CodCiudad
                //where a.Edad >@edadAlumno ";

                //using (var db = new AlumnosContext())
                //{
                //return View(db.Database.SqlQuery<AlumnoCE>(SqlQuery,
                //new SqlParameter("@EdadAlumno", edad)).ToList());
                //}

                //↑ Cuando se utiliza SQL
                #endregion

                #region Usando LinQ 
                using (var db = new AlumnosContext())
                {
                    var data = from a in db.Alumno
                               join c in db.Ciudad on a.CodCiudad equals c.Id
                               select new AlumnoCE()
                               {
                                   Id = a.Id,
                                   Nombres = a.Nombres,
                                   Apellidos = a.Apellidos,
                                   Edad = a.Edad,
                                   Sexo = a.Sexo,
                                   FechaRegistro = a.FechaRegistro,
                                   NombreCiudad = c.Nombre

                               };
                    return View(data.ToList());

                    //List<Alumno> lista = db.Alumno.Where(a => a.Edad > 18).ToList();
                    //return View(lista);

                    //return View(db.Alumno.ToList());                    
                }
                #endregion
            }
            catch (Exception)
            {

                throw;
            }

        }
        //httpGET
        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // para controlar que se trata del formulario correspondiente
        public ActionResult Agregar(Alumno al)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new AlumnosContext())
                {
                    al.FechaRegistro = DateTime.Now;
                    db.Alumno.Add(al); // agrego
                    db.SaveChanges(); // guardo la modificacion  
                    return RedirectToAction("index");
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error al Agregar un Alumno ", ex.Message);
                return View();
            }

        }

        public ActionResult ListadoCiudades()
        {
            using (var db = new AlumnosContext())
            {

                return PartialView(db.Ciudad.ToList());
            }
        }
        public ActionResult Editar(int Id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    //Alumno alu = db.Alumno.Where(valor => valor.Id == Id).FirstOrDefault();
                    //↑cuando se posee mas de una clave unica 
                    Alumno alu = db.Alumno.Find(Id);
                    //↑Solo se lo usa cuando se posee clave unica
                    return View(alu);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Alumno al)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    Alumno alumno = db.Alumno.Find(al.Id);
                    alumno.Nombres = al.Nombres;
                    alumno.Apellidos = al.Apellidos;
                    alumno.Edad = al.Edad;
                    alumno.Sexo = al.Sexo;
                    db.SaveChanges(); // guardo la modificacion                      
                    return RedirectToAction("index");
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error al Agregar un Alumno ", ex.Message);
                return View();
            }

        }
        public ActionResult DetalleAlumno(int Id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    Alumno al = db.Alumno.Find(Id);
                    return View(al);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult EliminarAlumno(int Id)
        {
            using (var db = new AlumnosContext())
            {
                Alumno alu = db.Alumno.Find(Id);
                db.Alumno.Remove(alu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


        }
        public static string NombreCiudad(int CodCiudad)
        {
            using (var db = new AlumnosContext())
            {

                return (db.Ciudad.Find(CodCiudad).Nombre);
            }
        }
    }
}