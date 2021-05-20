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
                while (true)
                {
                    myChrome.GetInformationCurrent(filePath, dGV);
                    Thread.Sleep(30000);
                }    
            }
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

        
        
        private List<TestItemClass> CreateTestItems()
        {
            var resultsList = new List<TestItemClass>();
            for (int i = 0; i < 15; i++)
            {
                var a = new TestItemClass()
                {
                    Id = i,
                    FullName = "Nguyen Van Hung " + i,
                    Money = 20000 + i * 10,
                    Address = "Tan Son Nhi " + i
                };
                resultsList.Add(a);
            }
            return resultsList;
        }

        //Test lưu file Excel
        private void SaveFileInExcel()
        {
            string filePath = "";
            // tạo SaveFileDialog để lưu file excel
            SaveFileDialog dialog = new SaveFileDialog();

            // chỉ lọc ra các file có định dạng Excel
            dialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            //Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
            }

            // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                return;
            }

            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    // đặt tên người tạo file
                    p.Workbook.Properties.Author = "Kteam by K9";

                    // đặt tiêu đề cho file
                    p.Workbook.Properties.Title = "Báo cáo thống kê";

                    //Tạo một sheet để làm việc trên đó
                    p.Workbook.Worksheets.Add("Kteam sheet");

                    // lấy sheet vừa add ra để thao tác
                    ExcelWorksheet ws = p.Workbook.Worksheets[1];

                    // đặt tên cho sheet
                    ws.Name = "Kteam sheet";
                    // fontsize mặc định cho cả sheet
                    ws.Cells.Style.Font.Size = 12;
                    // font family mặc định cho cả sheet
                    ws.Cells.Style.Font.Name = "Calibri";

                    // Tạo danh sách các column header
                    string[] arrColumnHeader = {
                                                "Id",
                                                "FullName",
                                                "Money",
                                                "Address"
                    };

                    // lấy ra số lượng cột cần dùng dựa vào số lượng header
                    var countColHeader = arrColumnHeader.Count();

                    // merge các column lại từ column 1 đến số column header
                    // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                    ws.Cells[1, 1].Value = "Thống kê thông tin User Kteam";
                    ws.Cells[1, 1, 1, countColHeader].Merge = true;
                    // in đậm
                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                    // căn giữa
                    ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    int colIndex = 1;
                    int rowIndex = 2;

                    //tạo các header từ column header đã tạo từ bên trên
                    foreach (var item in arrColumnHeader)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];

                        //set màu thành gray
                        var fill = cell.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                        //căn chỉnh các border
                        var border = cell.Style.Border;
                        border.Bottom.Style =
                        border.Top.Style =
                        border.Left.Style =
                        border.Right.Style = ExcelBorderStyle.Thin;

                        //gán giá trị
                        cell.Value = item;

                        colIndex++;
                    }

                    // Lấy danh sách tạo sẵn để test
                    List<TestItemClass> userList = CreateTestItems();

                    // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                    foreach (var item in userList)
                    {
                        // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                        colIndex = 1;

                        // rowIndex tương ứng từng dòng dữ liệu
                        rowIndex++;

                        //gán giá trị cho từng cell                      
                        ws.Cells[rowIndex, colIndex++].Value = Convert.ToString(item.Id);
                        ws.Cells[rowIndex, colIndex++].Value = Convert.ToString(item.FullName);
                        ws.Cells[rowIndex, colIndex++].Value = Convert.ToString(item.Money);
                        ws.Cells[rowIndex, colIndex++].Value = Convert.ToString(item.Address);
                    }

                    //Lưu file lại
                    Byte[] bin = p.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);
                }
                MessageBox.Show("Xuất excel thành công!");
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi khi lưu file!");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.facebook.com/vanhung.dev");
        }

    }
}
