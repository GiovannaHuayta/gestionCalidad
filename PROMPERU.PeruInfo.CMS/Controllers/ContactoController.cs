using AutoMapper;
using OfficeOpenXml;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.ApplicationService.DTO;
using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    [Authorize]
    public class ContactoController : Controller
    {
        #region Private Properties

        private readonly IContactoService _contactoService;

        protected IMapper _mapper;

        #endregion

        #region Constructor

        public ContactoController(IContactoService contactoService, IMapper mapper)
        {
            _contactoService = contactoService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        // GET: Contacto        
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Retorna la lista de contactos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Listar()
        {
            var data = new
            {
                data = _mapper.Map<List<ContactoBE>, List<ContactoDTO>>(_contactoService.Listar())
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Exportar contactos a excel.
        /// </summary>
        public void Exportar()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet sheet = excelPackage.Workbook.Worksheets.Add("Contacto");
            sheet.Cells["A1"].Value = "Nombres";
            sheet.Cells["B1"].Value = "Apellidos";
            sheet.Cells["C1"].Value = "Correo";
            sheet.Cells["D1"].Value = "Consulta";
            sheet.Cells["E1"].Value = "Categoría";
            sheet.Cells["F1"].Value = "F. Registro";

            int row = 2;

            foreach (ContactoBE contacto in _contactoService.Listar())
            {
                sheet.Cells[$"A{row}"].Value = contacto.Nombre;
                sheet.Cells[$"B{row}"].Value = contacto.Apellidos;
                sheet.Cells[$"C{row}"].Value = contacto.Correo;
                sheet.Cells[$"D{row}"].Value = contacto.Consulta;
                sheet.Cells[$"E{row}"].Value = contacto.Categoria;
                sheet.Cells[$"F{row}"].Value = contacto.FechaRegistro.Value.ToString("dd/MM/yyyy HH:mm:ss");

                row++;
            }

            sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + $"Contactos-{DateTime.Now:ddMMyyyyHHmmss}.xlsx");
            Response.BinaryWrite(excelPackage.GetAsByteArray());
            Response.End();
        }

        #endregion
    }
}