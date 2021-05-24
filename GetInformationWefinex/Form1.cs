using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetInformationWefinex
{
    public partial class FormMain : System.Windows.Forms.Form
    {
        public FormMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            textboxAccount.Text = "phanhoaiduc.tvb@gmail.com";
            textboxPassword.Text = "03121997";
        }

        List<Thread> threads = new List<Thread>();

        private MyChrome myChrome;

        public bool isRun = true;


        public void Run()
        {
            myChrome = new MyChrome();
            string account = textboxAccount.Text;
            string password = textboxPassword.Text;

            string filePath = @"F:\hello.txt";

            //If Login success
            if (myChrome.Login(account, password)) {
                //Get thông tin những cột có sẵn
                myChrome.GetInformationAvailable(filePath, dGV);
                //Get thông tin theo thời gian 30s
                while (isRun)
                {
                    myChrome.GetInformationCurrent(filePath, dGV);
                }    
            }

            myChrome.Exit();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textboxAccount.Text) | string.IsNullOrEmpty(textboxPassword.Text))
            {
                MessageBox.Show("Chưa nhập tài khoản hoặc mật khẩu", "Note", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create thread
            Thread t = new Thread(() =>
            {
                Run();
            });
            threads.Add(t);
            t.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.facebook.com/vanhung.dev");
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ChooseFileExcel();
        }

        public void ChooseFileExcel()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select file";
            openFileDialog.Filter = "Excel Sheet(*.xlsx)|*.xlsx";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileName != "")
                {
                    string ExcelPathName = openFileDialog.FileName;
                }
                else
                {
                    MessageBox.Show("Choose Excel sheet path...", "Note", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void WriteInforInExcel(string path)
        {

        }
    }
}
