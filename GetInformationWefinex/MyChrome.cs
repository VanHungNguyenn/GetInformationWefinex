using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetInformationWefinex
{
    class MyChrome
    {
        public IWebDriver driver;

        public bool Logined = false;

        public const string CLICK = "CLICK";
        public const string SEND_KEYS = "SEND_KEYS";
        public const string CLEAR = "CLEAR";
        public const string GET_TEXT = "GET_TEXT";
        public const string GET_INNER_HTML = "GET_INNER_HTML";
        public const string GET_OUTER_HTML = "GET_OUTER_HTML";
        public const string SWITCH_FRAME = "SWITCH_FRAME";
        public const string SWITCH_DEFAULT = "SWITCH_DEFAULT";

        public string ElementAction(string action, string xpath = "", int index = 0, string text = "", int waits = 60, int delay = 0, bool is_exception = true)
        {
            if (action.Equals(SWITCH_FRAME))
            {
                driver.SwitchTo().Frame(driver.FindElements(By.XPath(xpath))[index]);
                Sleep(delay);
                return "";
            }
            int wait = 0;
            while (driver.FindElements(By.XPath(xpath)).Count == 0 && wait < waits)
            {
                Sleep(1);
                wait++;
            }
            if (wait == waits)
            {
                if (is_exception)
                {
                    throw new Exception(string.Format("Xpath not found: {0}", xpath));
                }
                else
                {
                    return "";
                }
            }
            if (index < 0)
            {
                index += driver.FindElements(By.XPath(xpath)).Count;
            }
            switch (action)
            {
                case CLICK:
                    try
                    {
                        driver.FindElements(By.XPath(xpath))[index].Click();
                    }
                    catch
                    {
                        IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        executor.ExecuteScript("arguments[0].click();", driver.FindElements(By.XPath(xpath))[index]);
                    }
                    Sleep(delay);
                    return "";
                case CLEAR:
                    driver.FindElements(By.XPath(xpath))[index].Clear();
                    Sleep(delay);
                    return "";
                case SEND_KEYS:
                    driver.FindElements(By.XPath(xpath))[index].SendKeys(text);
                    Sleep(delay);
                    return "";
                case GET_TEXT:
                    string t = driver.FindElements(By.XPath(xpath))[index].Text;
                    Console.WriteLine("t: " + t);
                    Sleep(delay);
                    return t;
                case GET_INNER_HTML:
                    string innerHTML = driver.FindElements(By.XPath(xpath))[index].GetAttribute("innerHTML");
                    Sleep(delay);
                    return innerHTML;
                case GET_OUTER_HTML:
                    string outerHTML = driver.FindElements(By.XPath(xpath))[index].GetAttribute("outerHTML");
                    Sleep(delay);
                    return outerHTML;
                case SWITCH_FRAME:
                    driver.SwitchTo().Frame(driver.FindElements(By.XPath(xpath))[index]);
                    Sleep(delay);
                    return "";
                case SWITCH_DEFAULT:
                    driver.SwitchTo().DefaultContent();
                    Sleep(delay);
                    return "";
                default: return "";
            }
        }


        private void Sleep(double seconds)
        {
            Thread.Sleep(Convert.ToInt32(seconds * 1000));
        }

        public MyChrome()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications");
            options.AddArgument("--window-size=1400,1100");
            options.AddArguments("--disable-extensions");
            driver = new ChromeDriver(service, options);
        }

        public void Exit()
        {
            Sleep(2);
            driver.Quit();
        }

        public bool Login(string account, string password)
        {
            string url = "https://wefinex.net/login";
            driver.Navigate().GoToUrl(url);
            Sleep(1);
            ElementAction(SEND_KEYS, "//input[@class='md-input']", text: account);
            ElementAction(SEND_KEYS, "//input[@class='md-input password']", text: password);
            ElementAction(CLICK, "//button[@class='button btn-large wbtn btn-radius w-100 siginButton']");
            Sleep(3);
            bool UrlRight = driver.Url.Contains("login");
            if (UrlRight)
            {
                MessageBox.Show("Vui lòng ấn capcha để tiếp tục...", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            while (!Logined)
            {
                UrlRight = driver.Url.Contains("index");
                if (UrlRight)
                {
                    Logined = true;
                }
                Sleep(3);
            }
            return Logined;
        }

        public void GetInformationAvailable(DataGridView dGV)
        {
            string xpath = "//div[@id='chart-instance']/div[@class='highcharts-container ']/*[name()='svg']/*[name()='g' and contains(@class,'highcharts')]/*[name()='g' and contains(@class,'highcharts-series-0')]/*[name()='path']";
            int CountPaths = driver.FindElements(By.XPath(xpath)).Count;
            string xpathOrderTime = "//div[@id='betAmount']/div[@class='bet-wrapper']/div/div/a/p";
            if(CountPaths % 2 == 1)
            {
                if (driver.FindElements(By.XPath(xpathOrderTime))[0].Text == "Hãy đặt lệnh")
                {
                    IReadOnlyCollection<IWebElement> Paths = driver.FindElements(By.XPath(xpath));
                    DateTime TimeCurrent = DateTime.Now;
                    string Time = "0";
                    for (int i = 1; i < Paths.Count - 2; i = i + 2)
                    {
                        string Status = GetStatusPath(Paths.ToList()[i]);
                        Time = TimeOfX(TimeCurrent, Paths.Count - 2, i + 1).ToString("h:mm:ss tt");
                        Addrows(dGV, Time, Status);
                    }
                }
                else
                {
                    IReadOnlyCollection<IWebElement> Paths = driver.FindElements(By.XPath(xpath));
                    DateTime TimeCurrent = DateTime.Now;
                    string Time = "0";
                    for (int i = 0; i < Paths.Count - 2; i = i + 2)
                    {
                        string Status = GetStatusPath(Paths.ToList()[i]);
                        Time = TimeOfX(TimeCurrent, Paths.Count - 2, i + 1).ToString("h:mm:ss tt");
                        Addrows(dGV, Time, Status);
                    }
                }
            } else
            {
                if (driver.FindElements(By.XPath(xpathOrderTime))[0].Text == "Hãy đặt lệnh")
                {
                    IReadOnlyCollection<IWebElement> Paths = driver.FindElements(By.XPath(xpath));
                    DateTime TimeCurrent = DateTime.Now;
                    string Time = "0";
                    for (int i = 0; i < Paths.Count - 2; i = i + 2)
                    {
                        string Status = GetStatusPath(Paths.ToList()[i]);
                        Time = TimeOfX(TimeCurrent, Paths.Count - 2, i + 1).ToString("h:mm:ss tt");
                        Addrows(dGV, Time, Status);
                    }
                }
                else
                {
                    IReadOnlyCollection<IWebElement> Paths = driver.FindElements(By.XPath(xpath));
                    DateTime TimeCurrent = DateTime.Now;
                    string Time = "0";
                    for (int i = 1; i < Paths.Count - 2; i = i + 2)
                    {
                        string Status = GetStatusPath(Paths.ToList()[i]);
                        Time = TimeOfX(TimeCurrent, Paths.Count - 2, i + 1).ToString("h:mm:ss tt");
                        Addrows(dGV, Time, Status);
                    }
                }
            }              
        }


        public void GetInformationCurrent(DataGridView dGV)
        {
            string xpathOrderTime = "//div[@id='betAmount']/div[@class='bet-wrapper']/div/div/a/p";
            if (driver.FindElements(By.XPath(xpathOrderTime))[0].Text == "Hãy đặt lệnh")
            {
                string xpath = "//div[@id='chart-instance']/div[@class='highcharts-container ']/*[name()='svg']/*[name()='g' and contains(@class,'highcharts')]/*[name()='g' and contains(@class,'highcharts-series-0')]/*[name()='path']";
                int CountPaths = driver.FindElements(By.XPath(xpath)).Count;
                string Status = GetStatusPath(driver.FindElements(By.XPath(xpath))[CountPaths - 2]);
                DateTime TimeCurrent = DateTime.Now;
                string Time = TimeCurrent.ToString("h:mm:ss tt");
                Addrows(dGV, Time, Status);

                int orderTime = Convert.ToInt32(driver.FindElements(By.XPath(xpathOrderTime))[1].Text.Split('s')[0]);
                if (orderTime < 10)
                {
                    Sleep(45);
                }
                else
                {
                    Sleep(60);
                }
            } else
            {
                Sleep(30);
            }         
        }


        public string GetStatusPath(IWebElement path)
        {
            string ColorColumn = path.GetAttribute("fill");
            string status = "";
            if (ColorColumn == "#31BAA0")
            {
                status = "Tăng";
            }
            else if (ColorColumn == "#FC5F5F")
            {
                status = "Giảm";
            }
            return status;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public double DateTimeToUnix(DateTime MyDateTime)
        {
            TimeSpan timeSpan = MyDateTime - new DateTime(1970, 1, 1, 7, 0, 0);

            return timeSpan.TotalSeconds;
        }

        public DateTime TimeOfX(DateTime TimeCurrent, int NumberTimes, int Time)
        {
            double timeX = DateTimeToUnix(TimeCurrent) - ((NumberTimes - Time) * 30);
            DateTime dateTime = UnixTimeStampToDateTime(timeX);
            return dateTime;
        }

        public void Addrows(DataGridView dGV, string time, string status)
        {
            dGV.Invoke(new MethodInvoker(() =>
            {
                int myRowIndex = dGV.Rows.Add();
                DataGridViewRow row = dGV.Rows[myRowIndex];
                row.Cells["STT"].Value = dGV.Rows.Count;
                row.Cells["Time"].Value = time;
                row.Cells["Status"].Value = status;
                if (status == "Tăng")
                {
                    row.Cells["Status"].Style.BackColor = Color.Green;
                }
                else
                {
                    row.Cells["Status"].Style.BackColor = Color.Red;
                }
            }));   
        }
    }
}




