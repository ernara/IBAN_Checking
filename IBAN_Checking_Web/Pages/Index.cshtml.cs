using IBAN_Checking_Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IBAN_Checking_Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHostEnvironment _environment;

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public async Task OnPostAsync()
        {
            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                return;
            }

            _logger.LogInformation($"Uploading {UploadedFile.FileName}.");
            string targetFileName = $"{_environment.ContentRootPath}/{UploadedFile.FileName}";

            using (var stream = new FileStream(targetFileName, FileMode.Create))
            {
                await UploadedFile.CopyToAsync(stream);

            }

            using (var sr = new StreamReader(targetFileName))
            {
                Input = sr.ReadToEnd();
                OnPostResult();
                sr.Close();
            }

            FileInfo fi = new FileInfo(targetFileName);
            fi.Delete();
        }

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


        public IndexModel(IChecker checker, ILogger<IndexModel> logger, IHostEnvironment environment)
        {
            Checker = checker;
            _logger = logger;
            _environment = environment;
        }
    }
}
