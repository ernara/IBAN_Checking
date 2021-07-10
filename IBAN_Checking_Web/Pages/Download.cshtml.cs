using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace IBAN_Checking_Web.Pages
{
    public class DownloadModel : PageModel
    {
        public IActionResult OnGet(string name)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                StreamWriter objstreamwriter = new StreamWriter(stream);
                objstreamwriter.Write(PrivacyModel.Result);
                objstreamwriter.Flush();
                objstreamwriter.Close();
                return File(stream.ToArray(), "text/plain", name);
            }
        }
    }
}
