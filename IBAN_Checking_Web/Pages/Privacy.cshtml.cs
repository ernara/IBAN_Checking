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
        public int ID { get; set; }
        [BindProperty]
        public int ID2 { get; set; }
        [BindProperty]
        public int Ats { get; set; }

        public void OnPostSum()
        {
            Ats = Class1.Sum(ID, ID2);
        }

        private readonly ILogger<PrivacyModel> _logger;


        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
        
    }
}
