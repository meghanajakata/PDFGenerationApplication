using iTextSharp.text;
using iTextSharp.text.pdf;
using Razor.Templating.Core;
using iTextSharp.text.html.simpleparser;
using System.Globalization;
using PDFGenerationApplication.Models;
using iTextSharp.tool.xml;
using PDFGenerator;

namespace PDFGenerationApplication.Repository
{
    public class HTMLToByte
    {
        /// <summary>
        /// Converts the html content to byte array
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async static Task<byte[]> GenerateString(UserModel model)
        {
            string result = await RazorTemplateEngine.RenderAsync("_userData", model);
            HTMLToPDFGenerator hTMLToPDFGenerator = new HTMLToPDFGenerator();
            byte[] docByte = hTMLToPDFGenerator.GeneratePDF(result);
            return docByte;
        }

        /// <summary>
        /// Converts string to byte array 
        /// </summary>
        /// <param name="stringData"></param>
        /// <returns></returns>
        public static byte[] GetByte(string stringData)
        {
            Document document = new Document();
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                document.Open();
                using (StringReader stringReader = new StringReader(stringData))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, document, stringReader);
                }
                document.Close();
                return stream.ToArray();
            }
        }


    }
}
