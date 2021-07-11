using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using IBAN_Checking_Library;

namespace IBAN_Checking_Web.Pages
{
    public class DownloadModel : PageModel
    {
        public IChecker Checker;
        public DownloadModel(IChecker checker)
        {
            Checker = checker;
        }

        public IActionResult OnGet(string name, string format)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                StreamWriter objstreamwriter = new StreamWriter(stream);
                objstreamwriter.Write(Checker.Result);
                objstreamwriter.Flush();
                objstreamwriter.Close();
                return File(stream.ToArray(), "text/plain", $"{name}.{format}");
            }
        }

        public void JoinInputAndResult()
        {
            //string[] stringSeparators = new string[] { "\r\n", ";" };
            //string[] lines = Input.Split(stringSeparators, StringSplitOptions.None);

            //for (int i = 0; i < PrivacyModel.Result.; i++)
            //{

            //}
        }
    }
}
