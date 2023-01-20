using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace LanguageSkillsWPF.Infrastructure
{
    internal class TextExtractor
    {
        public string Extract(string file)
        {
            StringBuilder sb = new StringBuilder();
            var extention = System.IO.Path.GetExtension(file);
            switch (extention)
            {
                case ".pdf":
                    using (PdfReader reader = new PdfReader(file))
                    {
                        for (int pageNo = 1; pageNo <= reader.NumberOfPages; pageNo++)
                        {
                            ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                            string text = PdfTextExtractor.GetTextFromPage(reader, pageNo, strategy);
                            text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                            sb.Append(text);
                        }
                    }
                    break;
                case ".fb2":
                case ".txt":
                case ".html":
                    using (StreamReader streamReader = new StreamReader(file, Encoding.UTF8))
                    {
                        sb.Append(streamReader.ReadToEnd());
                    }
                    break;
                case ".doc":
                case ".docx":

                    break;
                case ".epub":

                    break;
                case ".mobi":

                    break;
                case ".djvu":

                    break;
                default:

                    break;
            }
            return sb.ToString();


        }
    }
}
