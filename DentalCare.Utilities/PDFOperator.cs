using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DentalCare.Utilities
{
    public class PDFOperator
    {
        //public static void ConvertHTMLtoPDF()
        //{
        //    HtmlLoadOptions options = new HtmlLoadOptions();
        //    Document pdfDocument = new Document()_dataDir + "test.html", options);
        //    pdfDocument.Save(_dataDir + "html_test.PDF");
        //}

        private static object LockObject = new object();
        public static bool CreatePDFFile(string xml, string xsltTemplatePath, string pdfFilePath)
        {
            try
            {
                var stream = CreatePDFStream(xml, xsltTemplatePath);
                using (var fileStream = File.Create(pdfFilePath))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Stream CreatePDFStream(string xml, string xsltTemplatePath)
        {
            lock (LockObject)
            {
                Console.SetError(new StringWriter());


                var pdfDocument = new ibex4.FODocument();

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(xml);

                var pdfStream = new MemoryStream();

                using (var xmlStream = new MemoryStream(byteArray))
                {

                    using (var xslStream = new FileStream(xsltTemplatePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        pdfDocument.generate(xmlStream, xslStream, pdfStream, false);
                        xslStream.Close();
                        xslStream.Dispose();

                        xmlStream.Close();
                        xmlStream.Dispose();
                    }

                }


                var standardError = new StreamWriter(Console.OpenStandardError());
                standardError.AutoFlush = true;
                Console.SetError(standardError);
                pdfStream.Position = 0;
                return pdfStream;

            }

        }
    }
}
