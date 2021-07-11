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

        [BindProperty]
        public string Format { get; set; }

        [BindProperty]
        public string Input { get; set; }

        [BindProperty]
        public List<Result> Result { get; set; } = new List<Result>();



        public IChecker Checker { get; set; }


        public void OnGet()
        {
            Input = Checker.Input;
            Result = Checker.Result;
        }

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

        public IActionResult OnPostResult()
        {
            Checker.Result = new List<Result>();
            Checker.Input = Input;

            if (Input != null)
            {
                Checker.Result = Checker.CheckList(Input);
            }

            Result = Checker.Result;

            return Redirect("/");
        }

        public IActionResult OnPostDelete()
        {
            Checker.Input = "";
            Checker.Result = new List<Result>();
            return Redirect("/");
        }

        public IActionResult OnPostDownload()
        {
            return Redirect($"/download?name=results&format={Format}");
        }


        public IndexModel(IChecker checker, ILogger<IndexModel> logger, IHostEnvironment environment)
        {
            Checker = checker;
            _logger = logger;
            _environment = environment;
        }
    }
}
