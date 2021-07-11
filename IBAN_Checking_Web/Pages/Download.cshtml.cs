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
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    StreamWriter sr = new StreamWriter(stream);

                    if (format == "txt")
                    {
                        var maxLength = Checker.Result.Max(r => r.IBAN.Length);
                        var normalizedLength = maxLength < 4 ? 4 : maxLength;

                        sr.WriteLine($"+{new string('-', 21 + normalizedLength + 7)}+");


                        sr.WriteLine(string.Format("| {0," + -normalizedLength + "} | {1,-23} |", "IBAN", "Status"));
                        sr.WriteLine($"+{new string('-', 21 + normalizedLength + 7)}+");


                        foreach (var result in Checker.Result)
                        {
                            sr.WriteLine(string.Format("| {0," + -normalizedLength + "} | {1,-23} |", result.IBAN, result.CheckingResult));
                        }
                        sr.WriteLine($"+{new string('-', 21 + normalizedLength + 7)}+");

                    }
                    else
                    {
                        var sign = format == "csv" ? ";" : "\t";

                        sr.WriteLine($"IBAN{sign}Status");


                        foreach (var result in Checker.Result)
                        {
                            sr.WriteLine($"{result.IBAN}{sign}{result.CheckingResult}");
                        }
                    }

                    sr.Flush();
                    sr.Close();
                    return File(stream.ToArray(), "text/plain", $"{name}.{format}");

                }
            } catch
            {
            }

            return Redirect("/");
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
