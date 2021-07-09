using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBAN_Checking_Library;

namespace IBAN_Checking_Web.Pages
{
    public class PrivacyModel : PageModel
    {
        [BindProperty]
        public string Input { get; set; }
        [BindProperty]
        public CheckingResult Result { get; set; }

        private readonly IChecker Checker;


        public void OnPostResult()
        {
            Result = Checker.Check(Input);
        }


        public PrivacyModel(IChecker checker)
        {
            Checker = checker;
        }

        public void OnGet()
        {
        }
        
    }
}
