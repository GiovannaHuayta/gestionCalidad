using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace PROMPERU.PeruInfo.WEB.Helpers
{
    public static class LanguageManager
    {
        private static readonly List<Languages> AvailableLanguages = new List<Languages>
        {
            new Languages
            {
                LanguageFullName = "Español", LanguageCultureName = "es-pe"
            },

            new Languages
            {
                LanguageFullName = "Inglés", LanguageCultureName = "en-us"
            }
        };

        private static bool IsLanguageAvailable(string lang)
        {
            return AvailableLanguages.FirstOrDefault(a => a.LanguageCultureName.Equals(lang)) != null;
        }

        public static string GetDefaultLanguage()
        {
            return AvailableLanguages[0].LanguageCultureName;
        }

        public static void SetLanguage(string lang)
        {
            try
            {
                if (!IsLanguageAvailable(lang)) lang = GetDefaultLanguage();

                CultureInfo cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);

                HttpCookie langCookie = new HttpCookie("culture", lang)
                {
                    Expires = DateTime.Now.AddYears(1)
                };

                HttpContext.Current.Response.Cookies.Add(langCookie);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    public class Languages
    {
        public string LanguageFullName { get; set; }
        public string LanguageCultureName { get; set; }
    }
}