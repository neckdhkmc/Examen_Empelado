using Examen_Empelado.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examen_Empelado.Controllers
{
    public class EmpeladoController : Controller
    {
        // GET: Empelado
        public ActionResult Index()
        {
            LLenarestado();

            List<EmpleadoCLS> listaEmpleado = null;

            using (var bd = new Examen_empeladosEntities())
            {

                listaEmpleado = (from empl in bd.Empelados
                                 join Estados in bd.Estados
                                 on empl.Id_Estado equals Estados.Id_Estado
                                
                              
                                 select new EmpleadoCLS
                                 {
                                     id = empl.id_Empelado,
                                     Nombre = empl.Nombre,
                                     ApellidoPaterno = empl.ApPaterno,
                                     ApellidoMaterno = empl.ApMaterno,
                                     NumEmpleado = empl.NumEpleado,
                                     correo =  empl.Correo,
                                     Telefono =  empl.Telefono,
                                     
                                     Direccion = empl.Direccion,
                                     FechaNac = empl.fechaNac,
                                    
                                     NombreEstadoLogico = Estados.Nombre
                                 }).ToList();
            }
            return View(listaEmpleado);
        }

        List<SelectListItem> listadeEstado;//variable global 
        public void LLenarestado()
        {
            using (var bd = new Examen_empeladosEntities())
            {

                listadeEstado = (from estado in bd.Estados

                                 select new SelectListItem
                                 {
                                     Text = estado.Nombre,
                                     Value = estado.Id_Estado.ToString()
                                 }).ToList();
                listadeEstado.Insert(0, new SelectListItem { Text = "--Selecciones--", Value = "" });
                ViewBag.ListaEstado = listadeEstado;

            }
            return;

        }

        public ActionResult Agregar()
        {
           
            LLenarestado();

            return View();
        }

        [HttpPost]
        public ActionResult Agregar(EmpleadoCLS obj)
        {
            int nRegistrosEncontrados = 0;
            string Nombre = obj.Nombre;
            using (var bd = new Examen_empeladosEntities())
            {
                nRegistrosEncontrados = bd.Empelados.Where(p => p.Nombre.Equals(Nombre)).Count();
            }
            if (!ModelState.IsValid || nRegistrosEncontrados >= 1)
            {
              
                LLenarestado();
                if (nRegistrosEncontrados >= 1)
                {
                    obj.mensajeError = "El Nombre ya existe";
                }
                return View(obj);
            }
            using (var bd = new Examen_empeladosEntities())
            {

                Empelados objeto = new Empelados();
                
                objeto.Nombre = obj.Nombre;
                objeto.ApPaterno = obj.ApellidoPaterno;
                objeto.ApMaterno = obj.ApellidoMaterno;
                objeto.NumEpleado = obj.NumEmpleado;
                objeto.Correo = obj.correo;
                objeto.Telefono = obj.Telefono;
                objeto.Direccion = obj.Direccion;
                objeto.fechaNac = obj.FechaNac;
               
                objeto.Id_Estado = obj.idEstadoLogico;
                bd.Empelados.Add(objeto);
                bd.SaveChanges();
              
            }


            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            EmpleadoCLS objCLS = new EmpleadoCLS();
            using (var bd = new Examen_empeladosEntities())
            {
                LLenarestado();
                

                Empelados oEmpleado = bd.Empelados.Where(p => p.id_Empelado.Equals(id)).First();
                objCLS.id = oEmpleado.id_Empelado;
                objCLS.Nombre = oEmpleado.Nombre;
                objCLS.ApellidoPaterno = oEmpleado.ApPaterno;
                objCLS.ApellidoMaterno = oEmpleado.ApMaterno;
                objCLS.NumEmpleado = oEmpleado.NumEpleado;
                objCLS.correo = oEmpleado.Correo;
                objCLS.Telefono =oEmpleado.Telefono;
                objCLS.Direccion = oEmpleado.Direccion;
               // objCLS.FechaNac = oEmpleado.fechaNac;
           
                
                objCLS.idEstadoLogico = (int)oEmpleado.Id_Estado;

              

            }
            return View(objCLS);
        }
          [HttpPost]
        public ActionResult Editar(EmpleadoCLS OEmpleadoCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(OEmpleadoCLS);
            }
            else 
            {
                int idEmpleado = OEmpleadoCLS.id;
                using (var bd = new Examen_empeladosEntities())
                {
                    Empelados obj = bd.Empelados.Where(p => p.id_Empelado.Equals(idEmpleado)).First();

                    obj.Nombre = OEmpleadoCLS.Nombre;
                    obj.ApPaterno = OEmpleadoCLS.ApellidoPaterno;
                    obj.ApMaterno = OEmpleadoCLS.ApellidoMaterno;
                    obj.NumEpleado = OEmpleadoCLS.NumEmpleado;
                    obj.Correo = OEmpleadoCLS.correo;
                    obj.Telefono = OEmpleadoCLS.Telefono;
                    obj.Direccion = OEmpleadoCLS.Direccion;
                    //obj.fechaNac = OEmpleadoCLS.FechaNac;
                    
                    obj.Id_Estado = OEmpleadoCLS.idEstadoLogico;
                    bd.SaveChanges();


                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)//metodo para eliminar  solo hace el cambio logico de estado 
        {
            int rpta = 0;
            using (var bd = new Examen_empeladosEntities())
            {
                Empelados obj = bd.Empelados.Where(p => p.id_Empelado.Equals(id)).First();
                obj.Id_Estado = 2;           

                rpta = bd.SaveChanges();

            }
            return RedirectToAction("Index");
        }



    }
}