using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;



namespace File_Generation_System
{
    public class TextPrintDocument : PrintDocument
    {

        private Font printFont;
        private TextReader printStream;
        private string fileToPrint;
        private Bitmap imgWatermark;
        private Image curImage = null;
        private string curFileName = null;
        private string FilePath;

        public bool Watermark = false;
        //Got error cannot find Watermark.gif
        //public TextPrintDocument()
        //{
        //    imgWatermark = new Bitmap(GetType(), "Watermark.gif");
        //}
        //Had to comment on :this() got no overload error
        public TextPrintDocument(string fileName, string filepath)
        //    : this()
        {
            this.FileToPrint = fileName;
            this.FilePath = filepath;
        }

        public string FileToPrint
        {
            get
            {
                return fileToPrint;
            }
            set
            {
                if (File.Exists(value))
                {
                    fileToPrint = value;
                    this.DocumentName = value;
                }
                else
                    throw (new Exception("File not found."));
            }
        }

        public Font Font
        {
            get { return printFont; }
            set { printFont = value; }
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);
            printFont = new Font("Verdana", 08);
            printStream = new StreamReader(fileToPrint);
        }

        protected override void OnEndPrint(PrintEventArgs e)
        {
            base.OnEndPrint(e);
            printFont.Dispose();
            printStream.Close();
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            curFileName = FilePath;
            curImage = Image.FromFile(curFileName);
            
             
            // Slow down printing for demo.
            System.Threading.Thread.Sleep(200);

            Graphics gdiPage = e.Graphics;
            gdiPage.DrawImage(curImage, 650, 0, curImage.Width, curImage.Height);
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float lineHeight = printFont.GetHeight(gdiPage);
            float linesPerPage = e.MarginBounds.Height / lineHeight;
            int lineCount = 0;
            string lineText = null;

            // Watermark?
            if (this.Watermark)
            {
                int top = Math.Max(0,
                         (e.PageBounds.Height - imgWatermark.Height) / 2);
                int left = Math.Max(0,
                         (e.PageBounds.Width - imgWatermark.Width) / 2);
                gdiPage.DrawImage(imgWatermark, left, top,
                         imgWatermark.Width, imgWatermark.Height);
            }

            // Print each line of the file.
            while (lineCount < linesPerPage &&
                  ((lineText = printStream.ReadLine()) != null))
            {
                gdiPage.DrawString(lineText, printFont, Brushes.Black,
                leftMargin, (topMargin + (lineCount++ * lineHeight)));
            }

            // If more lines exist, print another page.
            if (lineText != null)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

    }
}
