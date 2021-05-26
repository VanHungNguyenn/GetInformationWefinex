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
            if (Properties.Settings.Default.userName != string.Empty)
            {
                textboxAccount.Text = Properties.Settings.Default.userName;
                textboxPassword.Text = Properties.Settings.Default.passUser;
            }
            //textboxAccount.Text = "phanhoaiduc.tvb@gmail.com";
            //textboxPassword.Text = "03121997";
        }

        List<Thread> threads = new List<Thread>();

        private MyChrome myChrome;

        public bool isRun = true;


        public void Run(string pathFile)
        {
            if (checkRemember.Checked)
            {
                Properties.Settings.Default.userName = textboxAccount.Text;
                Properties.Settings.Default.passUser = textboxPassword.Text;
                Properties.Settings.Default.Save();
            }

            myChrome = new MyChrome();
            string account = textboxAccount.Text;
            string password = textboxPassword.Text;

            

            //If Login success
            if (myChrome.Login(account, password)) {
                Thread.Sleep(3000);
                //Get thông tin những cột có sẵn
                myChrome.GetInformationAvailable(dGV);
                List<string> listValue = ExportTableInList(dGV);
                WriteExcel(listValue, pathFile);
                //Get thông tin theo thời gian 30s
                while (isRun)
                {
                    int countSave = 10;
                    while(countSave > 0)
                    {
                        myChrome.GetInformationCurrent(dGV);
                        countSave--;
                    }
                    listValue = ExportTableInList(dGV);
                    WriteExcel(listValue, pathFile);
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

            string pathFile = ChooseFileExcel();
            if (pathFile == "")
            {
                MessageBox.Show("Chưa chọn file excel để lưu", "Note", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create thread
            Thread t = new Thread(() =>
            {  
                Run(pathFile);
            });
            threads.Add(t);
            t.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            isRun = false;
        }


        //Tạo ô để đặt tên file

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.facebook.com/vanhung.dev");
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            List<string> listValue = ExportTableInList(dGV);
            //Lấy thông tin ở cột vào đây
            string path = ChooseFileExcel();
            WriteExcel(listValue, path);
        }

        public List<string> ExportTableInList(DataGridView dGV)
        {
            List<string> listValue = new List<string>();
            foreach (DataGridViewRow row in dGV.Rows)
            {
                listValue.Add((string)row.Cells["Status"].Value);
            }
            return listValue;
        }

        public string ChooseFileExcel()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select file";
            openFileDialog.Filter = "Excel Sheet(*.xlsx)|*.xlsx";
            string ExcelPathName = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileName != "")
                {
                    ExcelPathName = openFileDialog.FileName;
                }
                else
                {
                    MessageBox.Show("Choose Excel sheet path...", "Note", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return ExcelPathName;
        }

        public void WriteExcel(List<string> listValue, string p_strPath)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 15;
                workSheet.DefaultColWidth = 7;

                int x = listValue.Count / 50;
                var range = workSheet.Cells["A1:BC" + Convert.ToString(x * 3 + 3)];
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thick;
                range.Style.Border.Left.Color.SetColor(Color.Black);
                range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                range.Style.Border.Right.Color.SetColor(Color.Black);
                range.Style.Border.Top.Style = ExcelBorderStyle.Thick;
                range.Style.Border.Top.Color.SetColor(Color.Black);
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                range.Style.Border.Bottom.Color.SetColor(Color.Black);
                range.Style.Font.SetFromFont(new Font("Arial", 12));

                for (int i = 0; i < listValue.Count; i++)
                {
                    int timeX = i / 10;
                    int timeY = i / 50;
                    //Cập nhật số thứ tự
                    workSheet.Cells[1 + 3 * timeY, i + 1 * (timeX + 1) - 55 * timeY].Value = i + 1;
                    workSheet.Cells[1 + 3 * timeY, i + 1 * (timeX + 1) - 55 * timeY].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[1 + 3 * timeY, i + 1 * (timeX + 1) - 55 * timeY].Style.Fill.BackgroundColor.SetColor(Color.LightYellow);
                    //Cập nhật bảng
                    if (listValue[i] == "Tăng")
                    {
                        workSheet.Cells[2 + 3 * timeY, i + 1 * (timeX + 1) - 55 * timeY].Value = 1;
                        workSheet.Cells[2 + 3 * timeY, i + 1 * (timeX + 1) - 55 * timeY].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells[2 + 3 * timeY, i + 1 * (timeX + 1) - 55 * timeY].Style.Fill.BackgroundColor.SetColor(Color.Green);
                    }
                    else
                    {
                        workSheet.Cells[3 + 3 * timeY, i + 1 * (timeX + 1) - 55 * timeY].Value = 1;
                        workSheet.Cells[3 + 3 * timeY, i + 1 * (timeX + 1) - 55 * timeY].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells[3 + 3 * timeY, i + 1 * (timeX + 1) - 55 * timeY].Style.Fill.BackgroundColor.SetColor(Color.Red);
                    }
                    //Cập nhật tổng 10 số.
                    if (i % 10 == 9)
                    {
                        workSheet.Cells[2 + 3 * timeY, i + 1 + 1 * (timeX + 1) - 55 * timeY].Formula = "=SUM(" + workSheet.Cells[2 + 3 * timeY, i + 1 + 1 * (timeX + 1) - 10 - 55 * timeY].Address + ":" + workSheet.Cells[2 + 3 * timeY, i + 1 + 1 * (timeX + 1) - 1 - 55 * timeY].Address + ")";
                        workSheet.Cells[3 + 3 * timeY, i + 1 + 1 * (timeX + 1) - 55 * timeY].Formula = "=SUM(" + workSheet.Cells[3 + 3 * timeY, i + 1 + 1 * (timeX + 1) - 10 - 55 * timeY].Address + ":" + workSheet.Cells[3 + 3 * timeY, i + 1 + 1 * (timeX + 1) - 1 - 55 * timeY].Address + ")";
                    }
                }
                if (File.Exists(p_strPath))
                    File.Delete(p_strPath);

                // Create excel file on physical disk 
                FileStream objFileStrm = File.Create(p_strPath);
                objFileStrm.Close();

                // Write content to excel file 
                File.WriteAllBytes(p_strPath, excel.GetAsByteArray());
                //Close Excel package
                excel.Dispose();
            }
            catch (Exception)
            {                
                //MessageBox.Show("Vui lòng tắt file excel đang mở", "Note", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        
        
        public void SetStyleCell(ExcelWorksheet excelWorksheet, int indexX, int indexY, Color color)
        {
            excelWorksheet.Cells[indexX, indexY].Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelWorksheet.Cells[indexX, indexY].Style.Fill.BackgroundColor.SetColor(color);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}

