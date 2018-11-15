using HtmlAgilityPack;
using PdfSharp;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
        public string urlSpoj = "https://www.spoj.com/problems/JULKA/";
        public string urlHackerrank = "https://www.hackerrank.com/challenges/designer-pdf-viewer/problem" ;

        public string getCodeForces(string url)
        {
            var web = new HtmlWeb();
            var html = web.Load(url);
            var divs = html.DocumentNode.SelectNodes("//div");
            string problemString = "";
            foreach (var i in divs)
            {
                if (i.HasClass("problem-statement"))
                {
                    problemString = i.InnerHtml;
                    break;
                }
            }
            string matchPatt = "/predownloaded/";
            string needToAdd = "http://codeforces.com";
            int myInt = problemString.IndexOf(matchPatt, 0);
            while (myInt != -1)
            {
                problemString = problemString.Insert(myInt, needToAdd);
                myInt = problemString.IndexOf(matchPatt, myInt + needToAdd.Length + 1);
            }
            return "<center>"+problemString+"</center>";
        }

        public string getSpoj(string url) {
            var web = new HtmlWeb();
            var html = web.Load(url);
            var divs = html.DocumentNode.SelectNodes("//div");
            string problemString = "";
            foreach (var i in divs)
            {

                if (i.Id == "ccontent") {
                    i.Remove();
                }
                
            } 
            foreach (var i in divs)
            {
                if (i.HasClass("prob"))
                {
                    //MessageBox.Show(i.LastChild.InnerHtml);
                    problemString = i.InnerHtml;
                    break;
                }
            }
            return problemString;
        }
        public string getHackerrank(string url) {
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            WebClient client = new WebClient();

            // Add a user agent header in case the 
            // requested URI contains a query.
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            //client.Headers.Add("user-agent", "Mozilla/32.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            Stream data = client.OpenRead(urlHackerrank);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();



            var html = new HtmlAgilityPack.HtmlDocument();
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            html.LoadHtml(s);

            var divs = html.DocumentNode.SelectNodes("//div");
            
            string problemString = "";
            foreach (var i in divs)
            {
                if (i.HasClass("problem-statement"))
                {
                    //MessageBox.Show(i.LastChild.InnerHtml);
                    problemString = i.InnerHtml;
                    break;
                }
            }
            return problemString;
        }
        
        public void generatePDF(string pdfString)
        {


            PdfDocument pdf = PdfGenerator.GeneratePdf(pdfString, PageSize.A4);
            //PdfDocument pdf = PdfGenerator.GeneratePdf("<p><h1>Hello World</h1>This is html rendered text</p>", PageSize.A4);
            pdf.Save("documen"+ 1.ToString() +".pdf");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string myString = getHackerrank(urlHackerrank);
            richTextBox1.Text = myString;
            //MessageBox.Show(myString.Length.ToString());
            if (myString != "")
            {
                generatePDF(myString);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.DocumentText = richTextBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string mystr = getCodeForces(urlCodeForces);
            richTextBox1.Text = mystr;
            webBrowser1.Navigate(urlCodeForces);
        }
    }
}
