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
        public string Result { get; set; }

        public Checker Checker;

        public void OnPostResult()
        {
            Result = Checker.CheckCountry(Input);
        }

        private readonly ILogger<PrivacyModel> _logger;


        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
            Checker = new Checker();
        }

        public void OnGet()
        {
        }
        
    }
}
