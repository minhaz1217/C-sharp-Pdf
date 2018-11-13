using HtmlAgilityPack;
using PdfSharp;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace C_sharp_Pdf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string urlCodeForces = "http://codeforces.com/contest/924/problem/C";
        public string getCodeForces(string url)
        {
            var web = new HtmlWeb();
            var html = web.Load(url);
            var ht = html.DocumentNode.SelectNodes("//div");
            foreach (var i in ht)
            {
                if (i.HasClass("problem-statement"))
                {
                    return i.InnerHtml;
                }
            }
            return "";
        }

        public void generatePDF(string pdfString)
        {


            PdfDocument pdf = PdfGenerator.GeneratePdf(pdfString, PageSize.A4);
            //PdfDocument pdf = PdfGenerator.GeneratePdf("<p><h1>Hello World</h1>This is html rendered text</p>", PageSize.A4);
            pdf.Save("document.pdf");
        }




        private void button1_Click(object sender, EventArgs e)
        {
            string myString = getCodeForces(urlCodeForces);
            richTextBox1.Text = myString;
            if (myString != "")
            {
                generatePDF(myString);
            }
        }
    }
}
