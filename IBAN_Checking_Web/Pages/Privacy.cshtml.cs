using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBAN_Checking_Library;
using System.IO;

namespace IBAN_Checking_Web.Pages
{
    public class PrivacyModel : PageModel
    {
        [BindProperty]
        public string Input { get; set; }
        [BindProperty]
        public static string Result { get; set; }

        private readonly IChecker Checker;

        public void OnPostResult()
        {
            Result = "";
            
            if (Input!=null)
            {
                foreach (var item in Checker.CheckList(Input))
                {
                    Result += item.ToString() + "\n";
                }
            }

        }

        public void OnPostDelete()
        {
            Input = "";
            Result = "";
        }

        public PrivacyModel(IChecker checker)
        {
            Checker = checker;
        }

        public IActionResult OnGetDownload(string name)
        {
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    StreamWriter objstreamwriter = new StreamWriter(stream);
            //    objstreamwriter.Write("This is the content");
            //    objstreamwriter.Flush();
            //    objstreamwriter.Close();
            //    return File(stream.ToArray(), "text/plain", "file.txt");
            //}

            var content = new byte[] { 1, 2, 3 };
            return File(content, "application/octet-stream", name);
        }

    }
}
