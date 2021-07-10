using IBAN_Checking_Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBAN_Checking_Web.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Input { get; set; }

        [BindProperty]
        public string Result { get; set; }

        public IChecker Checker { get; set; }


        public void OnPostResult()
        {
            Checker.Result = "";

            //string[] stringSeparators = new string[] { "\r\n", ";" };
            //string[] lines = Input.Split(stringSeparators, StringSplitOptions.None);
            //Input = string.Join("\n",lines);

            if (Input != null)
            {
                foreach (var item in Checker.CheckList(Input))
                {
                    Checker.Result += item + "\n";
                }
            }

            Result = Checker.Result;
        }

        public void OnPostDelete()
        {
            Input = "";
            Checker.Result = "";
        }

        public IndexModel(IChecker checker)
        {
            Checker = checker;
        }
    }
}
