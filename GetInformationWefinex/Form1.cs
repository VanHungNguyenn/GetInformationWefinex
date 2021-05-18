using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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


            //Login success
            if (myChrome.Login(account, password)) {
                myChrome.GetInformation();
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
    }
}
