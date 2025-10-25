using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.ApplicationService.Contracts
{
    public interface IPersonaService
    {
        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="subCategoriaId"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        List<PersonaBE> Listar(int? idiomaId, int? subCategoriaId, string nombre);

        /// <summary>
        /// Listar por idioma y subcategoría.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="subcategoria"></param>
        /// <returns></returns>
        List<PersonaBE> Listar(int idiomaId, string subcategoria);

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        PersonaBE Seleccionar(int id, int idiomaId);
        
        /// <summary>
        /// Seleccionar por slug.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="subcategoria"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        PersonaBE Seleccionar(int idiomaId, string subcategoria, string slug);

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insertar(PersonaBE entity);

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Actualizar(PersonaBE entity);

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Eliminar(PersonaBE entity);
    }
}
