using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Doc;
using System.IO;
using System.Security.Cryptography;

namespace CopyRightDetector.Core
{
    public class Word
    {
        public async Task<DocumentInfo> GetDocumentInfo(string filePath)
        {
            var fs = File.OpenRead(filePath);
            return await GetDocumentInfo(fs);
        }

        public async Task<DocumentInfo> GetDocumentInfo(Stream fileStream)
        {
            DocumentInfo result = null;

            await Task.Run(() =>
            {
                try
                {
                    var doc = new Document(fileStream);
                    var document = doc.BuiltinDocumentProperties;

                    result = new DocumentInfo
                    {
                        TotalCharacters = document.CharCount,
                        TotalLines = document.LinesCount,
                        TotalPages = document.PageCount,
                        TotalParagraphs = document.ParagraphCount,
                        TotalWords = document.WordCount,
                        TotalCharactersWithSpaces = document.CharCountWithSpace
                    };
                }
                finally
                {

                }
            });

            if (result == null)
                throw new Exception("No Result");

            return result;
        }

        public string ComputeHash(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    return Encoding.UTF8.GetString(md5.ComputeHash(stream));
                }
            }
        }

        public async Task<byte[]> Convert(string filePath, ConvertMode type)
        {
            var fs = File.OpenRead(filePath);
            return await Convert(fs, type);
        }

        public async Task<byte[]> Convert(Stream file, ConvertMode type)
        {
            byte[] result = null;

            await Task.Run(() =>
            {
                try
                {
                    var ms = new MemoryStream();

                    var doc = new Document(file);

                    var fileFormat = FileFormat.Auto;

                    switch (type)
                    {
                        case ConvertMode.Xml:
                            fileFormat = FileFormat.Xml;
                            break;
                        case ConvertMode.Html:
                            fileFormat = FileFormat.Html;
                            break;
                        case ConvertMode.Pdf:
                            fileFormat = FileFormat.PDF;
                            break;
                        case ConvertMode.Xps:
                            fileFormat = FileFormat.XPS;
                            break;
                    }

                    doc.SaveToStream(ms, fileFormat);
                    result = ms.GetBuffer();
                }
                finally
                {

                }
            });

            return result;

        }

    }
}
