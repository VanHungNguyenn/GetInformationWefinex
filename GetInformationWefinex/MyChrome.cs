using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
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
            options.AddArgument("--window-size=1000,800");
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
                    Logined = false;
                }
                Sleep(3);
            }
            return Logined;
        }

        public void GetInformation()
        {
            string xpath = "//div[@id='chart-instance']/div[@class='highcharts-container ']/*[name()='svg']/*[name()='g' and contains(@class,'highcharts')]/*[name()='g' and contains(@class,'highcharts-series-0')]";
            IReadOnlyCollection<IWebElement> Paths = driver.FindElements(By.XPath(xpath));
            foreach (IWebElement Path in Paths)
            {

            }
            
        }
    }
}




//*[name()='path']