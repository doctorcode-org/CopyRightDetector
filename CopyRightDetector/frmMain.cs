using CopyRightDetector.Core;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Threading.Tasks;
using CopyRightDetector.Global;
using System.Linq;

namespace CopyRightDetector
{
    public partial class frmMain : Form, INotifyPropertyChanged
    {
        private string _CurrentFilePath = "";
        private byte[] _DocumentResult = null;

        public event PropertyChangedEventHandler PropertyChanged;

        private string CurrentFilePath
        {
            get { return _CurrentFilePath; }
            set
            {
                _CurrentFilePath = value;
                NotifyPropertyChanged();
            }
        }

        private byte[] DocumentResult
        {
            get
            {
                return _DocumentResult;
            }
            set
            {
                _DocumentResult = value;
                NotifyPropertyChanged();
            }
        }

        public frmMain()
        {
            InitializeComponent();
            PropertyChanged += new PropertyChangedEventHandler(FrmMain_PropertyChanged);
        }

        private void FrmMain_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CurrentFilePath":
                    {
                        lblFilePath.Text = CurrentFilePath;
                        Task.Run(async () => await GetDocumentInfo());
                        btnValidate.Enabled = !string.IsNullOrEmpty(CurrentFilePath);
                        btnSaveInDb.Enabled = !string.IsNullOrEmpty(CurrentFilePath);
                        break;
                    }
                case "DocumentResult":
                    {
                        var enabled = (DocumentResult != null);
                        btnSaveAsHtml.Enabled = enabled;
                        btnSaveAsPdf.Enabled = enabled;
                        btnSaveAsXml.Enabled = enabled;
                        btnSaveAsXps.Enabled = enabled;

                        if (enabled)
                        {
                            InvokeMain(() =>
                            {
                                if (wbBrowser.Document == null)
                                {
                                    wbBrowser.DocumentText = WordDocument.ConvertToHtml(DocumentResult);
                                }
                                else
                                {
                                    wbBrowser.Document.Write(WordDocument.ConvertToHtml(DocumentResult));
                                    wbBrowser.Refresh();
                                }
                            });
                        }

                        break;
                    }
            }
        }

        private async Task GetDocumentInfo()
        {
            if (!string.IsNullOrEmpty(CurrentFilePath))
            {
                var doc = await WordDocument.GetDocumentInfo(CurrentFilePath);

                lblTotalParagraphs.Text = doc.TotalParagraphs.ToString();
                lblTotalWords.Text = doc.TotalWords.ToString();
                lblTotalCharacters.Text = doc.TotalCharacters.ToString();
                lblTotalPages.Text = doc.TotalPages.ToString();

                var fileData = System.IO.File.ReadAllBytes(CurrentFilePath);

                var html =  WordDocument.ConvertToHtml(fileData);

                if (html != null)
                {
                    InvokeMain(() =>
                    {
                        if (wbBrowser.Document == null)
                        {
                            wbBrowser.DocumentText = html;
                        }
                        else
                        {
                            wbBrowser.Document.Write(html);
                            wbBrowser.Refresh();
                        }
                    });
                }
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnSelectDocument_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dlgOpenFile.FileName))
                {
                    CurrentFilePath = dlgOpenFile.FileName;
                }
            }
        }

        private void btnSaveAsXml_Click(object sender, EventArgs e)
        {
            dlgSave.Filter = "Xml Document |*.xml";
            dlgSave.DefaultExt = "xml";
            dlgSave.FileName = null;

            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                var xmlString = WordDocument.GetXmlString(DocumentResult);
                System.IO.File.WriteAllText(dlgSave.FileName, xmlString);
            }
        }

        private async void btnSaveAsPdf_Click(object sender, EventArgs e)
        {
            var bytes = await WordDocument.ConvertDocument(DocumentResult, ConvertMode.Pdf);
            dlgSave.Filter = "Pdf Document |*.pdf";
            dlgSave.DefaultExt = "pdf";
            dlgSave.FileName = null;

            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllBytes(dlgSave.FileName, bytes);
            }
        }

        private void btnSaveAsHtml_Click(object sender, EventArgs e)
        {
            var html = WordDocument.ConvertToHtml(DocumentResult);

            dlgSave.Filter = "Heml Document |*.html;*.htm";
            dlgSave.DefaultExt = "html";
            dlgSave.FileName = null;

            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(dlgSave.FileName, html.ToString());
            }
        }

        private async void btnSaveAsXps_Click(object sender, EventArgs e)
        {
            var bytes = await WordDocument.ConvertDocument(DocumentResult, ConvertMode.Xps);

            dlgSave.Filter = "Xps Document |*.xps";
            dlgSave.DefaultExt = "xps";
            dlgSave.FileName = null;

            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllBytes(dlgSave.FileName, bytes);
            }
        }

        private async void btnValidate_Click(object sender, EventArgs e)
        {
            btnValidate.Enabled = false;
            btnSaveInDb.Enabled = false;

            int paragraph;
            int words;

            int.TryParse(txtParagraphToValidate.Text, out paragraph);
            int.TryParse(txtWordToValidate.Text, out words);

            var word = new WordDocument()
            {
                StopByFirstDetect = chbStopByFirstDetect.Checked,
                ParagraphsToValidateCount = paragraph,
                MinWordsCount = words
            };

            word.MatchDetected += new EventHandler<MatchDetectedEventArgs>(Word_MatchDetected);
            word.ValidationComplete += new EventHandler<ValidationCompleteEventArgs>(Word_ValidationComplete);
            word.Progress += new EventHandler<ProgressEventArgs>(Word_Progress);
            word.ValidationStart += new EventHandler<ValidationStartEventArgs>(Word_ValidationStart);
            word.ExceptionOccurred += new EventHandler<Exception>(Word_ExceptionOccurred);
            DocumentResult = await word.Validate(CurrentFilePath);
        }

        private void Word_ExceptionOccurred(object sender, Exception e)
        {
            MessageBox.Show(e.Message);
        }

        private void Word_ValidationStart(object sender, ValidationStartEventArgs e)
        {
            InvokeMain(() =>
            {
                progWorker.Maximum = e.TotalDocuments;
                progWorker.Value = 0;
                progWorker.Visible = true;
            });
        }

        private void Word_Progress(object sender, ProgressEventArgs e)
        {
            InvokeMain(() =>
            {
                var newValue = progWorker.Value + 1;
                progWorker.Value = Math.Min(newValue, progWorker.Maximum);
            });
        }

        private void Word_ValidationComplete(object sender, ValidationCompleteEventArgs e)
        {
            InvokeMain(() =>
            {
                progWorker.Visible = false;

                if (e.MatchedCount == 0)
                {
                    if (MessageBox.Show(Texts.NoDocumentMatchedMsg, Texts.SaveDocument, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        WordDocument.SaveInDatabase(CurrentFilePath);
                    }
                }
                else
                {
                    if (e.ExistsDocumentId.HasValue)
                        MessageBox.Show(string.Format(Texts.DocumentExistsMsg, e.ExistsDocumentId), Texts.DuplicateDocument);
                }


                btnValidate.Enabled = true;
                btnSaveInDb.Enabled = true;

            });
        }

        private void Word_MatchDetected(object sender, MatchDetectedEventArgs e)
        {

        }

        private void InvokeMain(Action action)
        {
            Invoke(action);
        }

        private void btnSaveInDb_Click(object sender, EventArgs e)
        {
            WordDocument.SaveInDatabase(CurrentFilePath);
        }

        private void wbBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            var ctl = sender as Control;

            e.Cancel = !(e.Url.AbsoluteUri == "about:blank");

            if (e.Url.Host.Equals("DoctorCopy", StringComparison.InvariantCultureIgnoreCase))
            {
                var query = e.Url.Query.Trim('?');
                var param = query.Split('&').ToDictionary(x => x.Split('=')[0], x => x.Split('=')[1]);

                var documentId = int.Parse(param["document"]);
                var paragraphId = int.Parse(param["paragraph"]);

                var text = WordDocument.GetDocumentText(documentId, paragraphId);

                var frmText = new ShowRefrence(text);
                frmText.ShowDialog(this);

            }
        }


    }
}
