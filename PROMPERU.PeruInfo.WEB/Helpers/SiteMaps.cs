using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Hosting;
using System.Xml;
using System.IO;

namespace PROMPERU.PeruInfo.WEB.Helpers
{
    public class SiteMaps 
    {
        public SiteMaps() { }
        public void GenerarSiteMap(List<PaginaBE> urls, List<PublicacionBE> publicaciones, string idioma)
        {
            if (urls.Count() == 0) return;
            XmlDocument xmlDoc = new XmlDocument(); 
           // Crear el elemento raíz del sitemap
            XmlElement root = xmlDoc.CreateElement("urlset");
            root.SetAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            xmlDoc.AppendChild(root);
            string dominioAbosoluto = ConfigurationManager.AppSettings.Get("DominioAbsoluto");
            string subcadena = "peru.info";
            // Agregar cada URL al sitemap
            foreach (PaginaBE url in urls)
            {
                XmlElement urlElement = xmlDoc.CreateElement("url");

                XmlElement locElement = xmlDoc.CreateElement("loc");
                if (!string.IsNullOrEmpty(url.LinkExterno))
                {
                    if(!url.LinkExterno.Contains(subcadena)) {
                        continue;
                    }
                    locElement.InnerText = url.LinkExterno;
                }
                else
                {
                    locElement.InnerText = dominioAbosoluto + "/" + idioma + "/" + url.Slug;
                }
                urlElement.AppendChild(locElement);
                // Agregar la fecha de última modificación
                XmlElement lastmodElement = xmlDoc.CreateElement("lastmod");
                lastmodElement.InnerText = url.FechaCreacion.Date.ToString("yyyy-MM-dd");


                urlElement.AppendChild(lastmodElement);
                // Agregar la frecuencia de cambio
                XmlElement changefreqElement = xmlDoc.CreateElement("changefreq");
                // Agregar la prioridad (ejemplo: 1.0)
                XmlElement priorityElement = xmlDoc.CreateElement("priority");
                if (url.Nombre == "Home" || url.Nombre == "home" || url.Nombre == "Inicio" || url.Nombre == "inicio")
                {
                    changefreqElement.InnerText = "daily";
                    priorityElement.InnerText = "1.0";
                    locElement.InnerText = dominioAbosoluto + "/" + idioma + "/";
                }
                else
                {
                    changefreqElement.InnerText = "always";
                    priorityElement.InnerText = "0.9";
                }
                urlElement.AppendChild(changefreqElement);
                urlElement.AppendChild(priorityElement);
                root.AppendChild(urlElement);
            }
            foreach (PublicacionBE publicacion in publicaciones)
            {
                XmlElement urlElement = xmlDoc.CreateElement("url");

                XmlElement locElement = xmlDoc.CreateElement("loc");
                locElement.InnerText = dominioAbosoluto + "/" + idioma + "/" + publicacion.CategoriaSlug + "/noticias/" + publicacion.CategoriaId + "/" + publicacion.SubcategoriaId + "/" + publicacion.Slug;
                urlElement.AppendChild(locElement);
                XmlElement lastmodElement = xmlDoc.CreateElement("lastmod");

                lastmodElement.InnerText = publicacion.FechaPublicacion.Date.ToString("yyyy-MM-dd");
                urlElement.AppendChild(lastmodElement);
                XmlElement changefreqElement = xmlDoc.CreateElement("changefreq");
                XmlElement priorityElement = xmlDoc.CreateElement("priority");
                changefreqElement.InnerText = "always";
                priorityElement.InnerText = "0.9";
                urlElement.AppendChild(changefreqElement);
                urlElement.AppendChild(priorityElement);
                root.AppendChild(urlElement);
            }

            string nombreIdioma = idioma == "es-pe" ? "pe" : "en";
            string nombreArchivo = "sitemap-" + nombreIdioma + ".xml";
            string rutaRaiz = HostingEnvironment.MapPath("~/");
            string rutaCompleta = Path.Combine(rutaRaiz, nombreArchivo);
            xmlDoc.Save(rutaCompleta);
        }
    }
}