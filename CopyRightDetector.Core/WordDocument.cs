using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using System.Xml;
using System.IO.Compression;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using Microsoft.Office.Core;
using TaskRuner = System.Threading.Tasks.Task;
using System.Security.Cryptography;
using HtmlAgilityPack;
using System.Collections.Concurrent;
using CopyRightDetector.Core.Models;
using CopyRightDetector.Core.Data;
using System.Text.RegularExpressions;

namespace CopyRightDetector.Core
{
    public class WordDocument
    {
        private ConcurrentBag<MatcheDocument> _MatchedDocuments = new ConcurrentBag<MatcheDocument>();
        private Func<int, int, string> InternalFormatRefrenceUrl = null;

        private ConcurrentBag<DetectRange> DetectRenageList = new ConcurrentBag<DetectRange>();

        public event EventHandler<ProgressEventArgs> Progress;
        public event EventHandler<Exception> ExceptionOccurred;
        public event EventHandler<MatchDetectedEventArgs> MatchDetected;
        public event EventHandler<ValidationStartEventArgs> ValidationStart;
        public event EventHandler<ValidationCompleteEventArgs> ValidationComplete;

        /// <summary>
        /// توقف جستجو با اولین ارجاع یافت شده
        /// </summary>
        public bool StopByFirstDetect { get; set; } = false;

        // اسنادی که دارای کپی هستند
        public ConcurrentBag<MatcheDocument> MatchedDocuments
        {
            get
            {
                return _MatchedDocuments;
            }
        }

        /// <summary>
        /// تعداد پاراگراف‌هایی که باید اعتبارسنجی شوند
        /// </summary>
        public int ParagraphsToValidateCount { get; set; }

        /// <summary>
        /// حداقل تعداد کلماتی که جمله باید داشته باشد تا اعتبارسنجی شود
        /// </summary>
        public int MinWordsCount { get; set; }

        public static string GetDocumentText(int documentId, int paragraphId)
        {
            string result = "";

            try
            {
                string tempPath = null;
                var hashFile = false;

                var db = new DataProvider();

                db.GetFileAsync(documentId, (ext, stream) =>
                {
                    tempPath = GetTempFile(ext);

                    using (var fs = File.OpenWrite(tempPath))
                    {
                        var buffer = new byte[4096];
                        int len;
                        while ((len = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fs.Write(buffer, 0, len);
                        }
                    }

                    hashFile = true;
                });

                if (!hashFile)
                    return "";

                object utf = MsoEncoding.msoEncodingUTF8;
                object m = System.Reflection.Missing.Value;
                object path = tempPath;
                object falseValue = false;
                object trueValue = true;
                var app = new Application();
                object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
                Document doc = null;

                try
                {
                    app.Visible = false;
                    app.ScreenUpdating = false;
                    doc = app.Documents.Open(ref path, ref falseValue, ref falseValue, ref m, ref m, ref m, ref m, ref m, ref m, ref m, ref utf);
                    doc.WebOptions.Encoding = MsoEncoding.msoEncodingUTF8;

                    result = doc.Paragraphs[paragraphId].Range.Text;
                    result = Regex.Replace(result, "\\v+|\\a+", "");
                    result = Regex.Replace(result, "\\r+", "\r");
                    result = Regex.Replace(result, "\\n+", "\n");
                    result = Regex.Replace(result, "\\t+", " ");
                    result = Regex.Replace(result, "\\s+", " ");
                }
                finally
                {
                    if (doc != null)
                    {
                        doc.Close(ref saveChanges);
                        doc = null;
                    }

                    if (app != null)
                    {
                        app.Quit(ref saveChanges);
                        app = null;
                    }

                    DeleteTempFiles(tempPath);
                }

            }
            finally
            { }

            return result;
        }


        public WordDocument()
        {
            InternalFormatRefrenceUrl = (documentId, paragraphId) =>
            {
                return $"http://DoctorCopy?document={documentId}&paragraph={paragraphId}";
            };
        }

        public WordDocument(Func<int, int, string> formatRefrenceUrl)
        {
            InternalFormatRefrenceUrl = formatRefrenceUrl;
        }

        public async Task<byte[]> Validate(Stream fileStream)
        {
            var buffer = new byte[4096];
            int len;

            var tempPath = GetTempFile("docx");

            using (var fs = File.OpenWrite(tempPath))
            {
                while ((len = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fs.Write(buffer, 0, len);
                }
            }

            return await Validate(tempPath);
        }

        public async Task<byte[]> Validate(string filePath)
        {
            byte[] result = null;

            // ابتدا یک کپی از سند وارد شده می‌گیریم
            var copyPath = GetTempFile(Path.GetExtension(filePath).Trim('.'));
            File.Copy(filePath, copyPath, true);


            Application app = null;
            Document document = null;

            object utf = MsoEncoding.msoEncodingUTF8;
            object oMiss = System.Reflection.Missing.Value;
            object falseValue = false;
            object trueValue = true;
            object path = copyPath;
            object saveChanges = WdSaveOptions.wdDoNotSaveChanges;

            var db = new DataProvider();

            try
            {
                using (var fs = File.OpenRead(copyPath))
                {
                    var existsDocumentId = db.DocumentExists(fs);

                    if (existsDocumentId.HasValue)
                    {
                        // سند در دیتابیس موجود است
                        OnValidationComplete(new ValidationCompleteEventArgs
                        {
                            MatchedCount = 1,
                            ExistsDocumentId = existsDocumentId
                        });

                        return null;
                    }
                }

                app = new Application();
                app.Visible = false;
                app.ScreenUpdating = false;

                // تعداد اسناد موجود در پایگاه داده
                var totalDocuments = db.GetDocumentsCount();

                document = app.Documents.Open(ref path, ref trueValue, ref falseValue);

                var documentText = document.Content.Text;

                // اتمام عملیات اگر سندی یافت نشود
                if (totalDocuments == 0 || string.IsNullOrEmpty(documentText))
                {
                    // اتمام عملیات اصالت سنجی، سندی یافت نشد یا سند وارد شده خالی است
                    OnValidationComplete(new ValidationCompleteEventArgs
                    {
                        MatchedCount = 0,
                        ExistsDocumentId = null
                    });

                    return null;
                }

                OnValidationStart(new ValidationStartEventArgs
                {
                    TotalDocuments = totalDocuments
                });

                // تغییر رنگ سند
                object start = document.Content.Start;
                object end = document.Content.End;
                var docRange = document.Range(ref start, ref end);
                docRange.Font.Color = WdColor.wdColorBlue;

                // انتخاب پاراگراف برای جستجو در دیتابیس
                var paragraphIds = new List<int>();
                var totalParagraphs = (ParagraphsToValidateCount <= 0 || document.Paragraphs.Count < ParagraphsToValidateCount) ?
                                       document.Paragraphs.Count :
                                       ParagraphsToValidateCount;

                for (var i = 1; i <= totalParagraphs; i++)
                {
                    var paragraph = document.Paragraphs[i];

                    if (paragraph.Range.Words.Count >= MinWordsCount)
                        paragraphIds.Add(i);
                }


                // حداکثر تعداد تردها
                var maxThraeds = 20;

                // تعداد تردهای پردازشگر
                double totalThraeds = (totalDocuments > maxThraeds) ? maxThraeds : totalDocuments;

                // تعداد سطرهایی که هر ترد باید پردازش کند
                var pageSize = (int)Math.Floor(totalDocuments / totalThraeds);

                // لیست تردها
                var tasks = new List<TaskRuner>();

                for (var i = 1; i <= totalThraeds; i++)
                {
                    if (StopByFirstDetect == true && MatchedDocuments.Count > 0)
                        break;

                    var skipRows = (i - 1) * pageSize;

                    // اگر ترد آخر باشیم
                    if (i == totalThraeds)
                        pageSize = totalDocuments - ((i - 1) * pageSize);

                    var task = TaskRuner.Run(() =>
                    {
                        Compare(db, document, paragraphIds, skipRows, pageSize);
                    });

                    tasks.Add(task);
                }

                await TaskRuner.WhenAll(tasks);

                foreach (var item in DetectRenageList)
                {
                    var prag = document.Paragraphs[item.SourceParagraphIndex];
                    var sent = prag.Range.Sentences[item.SourceSentenceIndex];
                    sent.Font.Color = WdColor.wdColorRed;

                    var anchorText = $"[سند {item.DocumentId}]";
                    var range = document.Range(item.End, item.End);
                    range.InsertAfter(anchorText);

                    // افزودن لینک به ارجاع
                    var anchor = document.Range(range.End - anchorText.Length, range.End);

                    object address = InternalFormatRefrenceUrl(item.DocumentId, item.DistanceParagraphIndex);
                    object screenTip = $"{item.Tooltip}";

                    prag.Range.Hyperlinks.Add(anchor, ref address, ref oMiss, ref screenTip);

                    anchor.InsertAfter(" ");
                }

                document.Save();
                document.Close();
                document = null;

                result = File.ReadAllBytes(path.ToString());
            }
            catch (Exception ex)
            {
                OnExceptionOccurred(ex);
            }
            finally
            {
                if (document != null)
                {
                    document.Close(ref saveChanges);
                    document = null;
                }

                if (app != null)
                {
                    app.Quit(ref saveChanges);
                    app = null;
                }

                DeleteTempFiles(path.ToString());
            }

            // اتمام عملیات اصالت سنجی
            OnValidationComplete(new ValidationCompleteEventArgs
            {
                MatchedCount = MatchedDocuments.Count
            });

            return result;
        }


        public static async Task<DocumentInfo> GetDocumentInfo(string filePath)
        {
            DocumentInfo result = null;

            var app = new Application();
            Document doc = null;
            object utf = MsoEncoding.msoEncodingUTF8;
            object m = System.Reflection.Missing.Value;
            object falseValue = false;
            object trueValue = true;
            object path = filePath;
            object saveChanges = WdSaveOptions.wdDoNotSaveChanges;

            try
            {
                app.Visible = false;
                app.ScreenUpdating = false;
                doc = app.Documents.Open(ref path, ref trueValue, ref trueValue);

                result = new DocumentInfo
                {
                    TotalParagraphs = doc.Paragraphs.Count,
                    TotalWords = doc.Words.Count,
                    TotalCharacters = doc.Characters.Count,
                    TotalPages = doc.ComputeStatistics(WdStatistic.wdStatisticPages, Type.Missing)
                };
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close(ref saveChanges);
                    doc = null;
                }

                if (app != null)
                {
                    app.Quit(ref saveChanges);
                    app = null;
                }
            }

            if (result == null)
                throw new Exception("No Result");

            await TaskRuner.Delay(0);

            return result;
        }


        public static void SaveInDatabase(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            var db = new DataProvider();
            TaskRuner.Run(() => db.InsertFileAsync(fileName, mimeType, File.OpenRead(filePath))).Wait();
        }

        public static string Extract(string filename)
        {
            var NsMgr = new XmlNamespaceManager(new NameTable());
            NsMgr.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");


            using (var archive = ZipFile.OpenRead(filename))
            {

                return XDocument
                    .Load(archive.GetEntry(@"word/document.xml").Open())
                    .XPathSelectElements("//w:p", NsMgr)
                    .Aggregate(new StringBuilder(), (sb, p) => p
                        .XPathSelectElements(".//w:t|.//w:tab|.//w:br", NsMgr)
                        .Select(e =>
                        {
                            switch (e.Name.LocalName)
                            {
                                case "br": return "\r\n";
                                case "tab": return "\t";
                            }
                            return e.Value;
                        })
                        .Aggregate(sb, (sb1, v) => sb1.Append(v)))
                    .ToString();
            }
        }

        public static string GetXmlString(byte[] data)
        {
            var tempPath = GetTempFile("docx");
            File.WriteAllBytes(tempPath, data);
            return GetXmlDocument(tempPath).ToString();
        }

        public static XDocument GetXmlDocument(string filename)
        {
            var NsMgr = new XmlNamespaceManager(new NameTable());
            NsMgr.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");


            using (var archive = ZipFile.OpenRead(filename))
            {
                return XDocument.Load(archive.GetEntry(@"word/document.xml").Open());
            }
        }

        public static async Task<byte[]> ConvertDocument(byte[] documentData, ConvertMode type)
        {
            var tempFile = GetTempFile("docx");
            File.WriteAllBytes(tempFile, documentData);
            var result= await ConvertDocument(tempFile, type);
            DeleteTempFiles(tempFile);
            return result;
        }

        public static async Task<byte[]> ConvertDocument(string filePath, ConvertMode type)
        {
            byte[] result = null;

            object utf = MsoEncoding.msoEncodingUTF8;
            object m = System.Reflection.Missing.Value;
            object path = filePath;
            object falseValue = false;
            object trueValue = true;
            var app = new Application();
            object tempFilePath = null;
            object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
            Document doc = null;

            try
            {
                app.Visible = false;
                app.ScreenUpdating = false;

                doc = app.Documents.Open(ref path, ref falseValue, ref falseValue, ref m, ref m, ref m, ref m, ref m, ref m, ref m, ref utf);
                doc.WebOptions.Encoding = MsoEncoding.msoEncodingUTF8;

                tempFilePath = GetTempFile(type);

                object fileType = null;

                switch (type)
                {
                    case ConvertMode.Xml:
                        fileType = WdSaveFormat.wdFormatXMLDocument;
                        break;
                    case ConvertMode.Html:
                        fileType = WdSaveFormat.wdFormatFilteredHTML;
                        break;
                    case ConvertMode.Pdf:
                        fileType = WdSaveFormat.wdFormatPDF;
                        break;
                    case ConvertMode.Xps:
                        fileType = WdSaveFormat.wdFormatXPS;
                        break;
                }

                doc.SaveAs(ref tempFilePath, ref fileType, ref m, ref m, ref m, ref m, ref m, ref m, ref m, ref m, ref m, ref utf);

                doc.Close(ref saveChanges);
                doc = null;


                if (type == ConvertMode.Html)
                {
                    result = Encoding.UTF8.GetBytes(EmbeddingHtmlImages(tempFilePath.ToString()));
                }
                else
                {
                    result = File.ReadAllBytes(tempFilePath.ToString());
                }

            }
            finally
            {
                if (doc != null)
                {
                    doc.Close(ref saveChanges);
                    doc = null;
                }

                if (app != null)
                {
                    app.Quit(ref saveChanges);
                    app = null;
                }

                DeleteTempFiles(tempFilePath.ToString());
            }

            await TaskRuner.Delay(0);

            return result;
        }

        /// <summary>
        /// Html تبدیل بایت‌های یک سند ورد به رشته
        /// </summary>
        /// <param name="documentData">آرایه‌ای از بایت‌ها که حاوی سند است</param>
        public static string ConvertToHtml(byte[] documentData)
        {
            var temp = GetTempFile("docx");
            using (var fs = File.OpenWrite(temp))
            {
                fs.Write(documentData, 0, documentData.Length);
            }

            var htmlBytes = TaskRuner.Run(async () => { return await ConvertDocument(temp, ConvertMode.Html); }).Result;
            DeleteTempFiles(temp);
            return Encoding.UTF8.GetString(htmlBytes);
        }


        #region PrivateMethods
        private static string GetTempFile(string extension)
        {
            //return Path.Combine(@"C:\Users\Parsnet\Desktop", Path.ChangeExtension(Path.GetRandomFileName(), extension));
            return Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetRandomFileName(), extension));
        }

        private static string GetTempFile(ConvertMode type)
        {
            string extension = type.ToString().ToLower();
            return GetTempFile(extension);
        }

        private static string EmbeddingHtmlImages(string filePath)
        {
            var html = File.ReadAllText(filePath, Encoding.UTF8);

            var direcory = Path.Combine(Path.GetDirectoryName(filePath), $"{Path.GetFileNameWithoutExtension(filePath)}_files");

            if (Directory.Exists(direcory))
            {
                var document = new HtmlDocument();
                document.LoadHtml(html);

                var images = document.DocumentNode.SelectNodes("//img");

                var cachedFile = new Dictionary<string, string>();

                foreach (var img in images)
                {
                    var src = img.GetAttributeValue("src", "");
                    var fileName = Path.GetFileName(src);

                    if (!cachedFile.ContainsKey(fileName))
                    {
                        var imgPath = Path.Combine(direcory, fileName);
                        var imgExt = Path.GetExtension(imgPath).Trim('.');

                        if (File.Exists(imgPath))
                        {
                            var imgBytes = File.ReadAllBytes(imgPath);
                            var base64 = $"data:image/{imgExt};base64,{Convert.ToBase64String(imgBytes)}";
                            cachedFile.Add(fileName, base64);
                        }
                        else
                        {
                            cachedFile.Add(fileName, src);
                        }
                    }

                    img.SetAttributeValue("src", cachedFile[fileName]);
                }

                html = document.DocumentNode.OuterHtml;
            }

            return html;
        }

        private void Compare(DataProvider db, Document document, List<int> paragraphIds, int skipRows, int pageSize)
        {
            try
            {
                db.ReadFileAsync(skipRows, pageSize, (documentId, ext, sqlStream) =>
                {
                    var buffer = new byte[4096];
                    int len;

                    Application app = null;
                    Document savedDocument = null;

                    object utf = MsoEncoding.msoEncodingUTF8;
                    object oMiss = System.Reflection.Missing.Value;
                    object falseValue = false;
                    object trueValue = true;
                    object tempPath = GetTempFile(ext);
                    object saveChanges = WdSaveOptions.wdDoNotSaveChanges;

                    try
                    {
                        using (var fs = File.OpenWrite(tempPath.ToString()))
                        {
                            while ((len = sqlStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fs.Write(buffer, 0, len);
                            }
                        }

                        app = new Application();
                        savedDocument = app.Documents.Open(ref tempPath, ref trueValue, ref falseValue);

                        for (int disIndex = 1; disIndex <= savedDocument.Paragraphs.Count; disIndex++)
                        {
                            var disFreeText = FormatText(savedDocument.Paragraphs[disIndex].Range.Text);

                            for (var srcIndex = 1; srcIndex <= paragraphIds.Count; srcIndex++)
                            {
                                var srcParagraph = document.Paragraphs[srcIndex];

                                var copyDetectCount = false;

                                for (var sentenceIndex = 1; sentenceIndex <= srcParagraph.Range.Sentences.Count; sentenceIndex++)
                                {
                                    var sentence = srcParagraph.Range.Sentences[sentenceIndex];
                                    var sentenceFreeText = FormatText(sentence.Text);

                                    if (!string.IsNullOrEmpty(sentenceFreeText) &&
                                        sentence.Words.Count >= MinWordsCount &&
                                        disFreeText.Contains(sentenceFreeText))
                                    {
                                        DetectRenageList.Add(new DetectRange
                                        {
                                            DocumentId = documentId,
                                            SourceParagraphIndex = srcIndex,
                                            SourceSentenceIndex = sentenceIndex,
                                            Start = sentence.Start,
                                            DistanceParagraphIndex = disIndex,
                                            End = sentence.End,
                                            Tooltip = savedDocument.Paragraphs[disIndex].Range.Text
                                        });

                                        copyDetectCount = true;
                                    }
                                }

                                if (copyDetectCount == true)
                                {
                                    _MatchedDocuments.Add(new MatcheDocument
                                    {
                                        DocumentId = documentId,
                                        DestinationIndex = disIndex,
                                        SourceIndex = srcIndex
                                    });
                                }
                                else
                                {
                                    // بررسی شد و کپی یافت نشد
                                    srcParagraph.Range.Font.Color = WdColor.wdColorGreen;
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        OnExceptionOccurred(ex);
                    }
                    finally
                    {
                        if (savedDocument != null)
                        {
                            savedDocument.Close(ref saveChanges);
                            savedDocument = null;
                        }

                        if (app != null)
                        {
                            app.Quit(ref saveChanges);
                            app = null;
                        }

                        DeleteTempFiles(tempPath.ToString());
                    }

                    OnProgress(new ProgressEventArgs
                    {
                        DocumentId = documentId
                    });

                });
            }
            catch (Exception ex)
            {
                OnExceptionOccurred(ex);
            }
        }

        private string FormatText(string input)
        {
            var result = Regex.Replace(input, "\\v+", "");
            result = Regex.Replace(result, "\\r+", " ");
            result = Regex.Replace(result, "\\n+", " ");
            result = Regex.Replace(result, "\\a+", "");
            result = Regex.Replace(result, "\\t+", " ");
            result = Regex.Replace(result, "\\s+", " ");

            return result.Trim(' ', '.');
            //return new string(result.Where(s => char.IsLetterOrDigit(s)).ToArray());
        }

        private static void DeleteTempFiles(params string[] filesPath)
        {
            try
            {
                foreach (var path in filesPath)
                {
                    if (File.Exists(path))
                        File.Delete(path);
                }
            }
            finally { }
        }
        #endregion

        //_____________________________________________ Events _____________________________________________

        #region Events
        private void OnMatchDetected(MatchDetectedEventArgs e)
        {
            MatchDetected?.Invoke(this, e);
        }

        private void OnValidationComplete(ValidationCompleteEventArgs e)
        {
            ValidationComplete?.Invoke(this, e);
        }

        private void OnProgress(ProgressEventArgs e)
        {
            Progress?.Invoke(this, e);
        }

        private void OnExceptionOccurred(Exception e)
        {
            ExceptionOccurred?.Invoke(this, e);
        }

        private void OnValidationStart(ValidationStartEventArgs e)
        {
            ValidationStart?.Invoke(this, e);
        }
        #endregion


        private class DetectRange
        {
            public int DocumentId { get; set; }
            public int SourceParagraphIndex { get; set; }
            public int SourceSentenceIndex { get; set; }
            public int DistanceParagraphIndex { get; set; }
            public int Start { get; set; }
            public int End { get; set; }
            public string Tooltip { get; set; }
        }

    }
}
