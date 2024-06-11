using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string userDataFile = "userData.dat";

        if (!File.Exists(userDataFile))
        {
            Console.WriteLine("Enter the User ID:");
            var userId = Console.ReadLine();

            Console.WriteLine("Enter the Password:");
            var password = Console.ReadLine();

            Console.WriteLine("Enter the Pin:");
            var pin = Console.ReadLine();

            var userData = $"{userId},{password},{pin}";
            var encryptedData = Encrypt(userData);
            File.WriteAllBytes(userDataFile, encryptedData);
        }
        
        Console.WriteLine("Enter the SecurID:");
        var securID = Console.ReadLine();

        var encryptedUserData = File.ReadAllBytes(userDataFile);
        var decryptedUserData = Decrypt(encryptedUserData).Split(',');

        var userIdStored = decryptedUserData[0];
        var passwordStored = decryptedUserData[1];
        var pinStored = decryptedUserData[2];

        var downloadDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

        var options = new ChromeOptions();
        options.AddUserProfilePreference("download.default_directory", downloadDirectory);
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);


        IWebDriver driver = new ChromeDriver();

        try
        {
            driver.Navigate().GoToUrl("https://tier2.mydesk.morganstanley.com/logon/LogonPoint/mydesk.html#/login");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var usernameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("login")));
            usernameField.SendKeys(userIdStored);

            var passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("passwd")));
            passwordField.SendKeys(passwordStored);

            var passwd1Field = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("passwd1")));
            passwd1Field.SendKeys($"{pinStored}{securID}");

            passwd1Field.SendKeys(Keys.Enter);

            var hostedWorkstationLink = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Hosted Workstation")));
            hostedWorkstationLink.Click();

            System.Threading.Thread.Sleep(5000);

            var downloadedFile = Directory.GetFiles(downloadDirectory)
                                           .OrderByDescending(f => new FileInfo(f).CreationTime)
                                           .FirstOrDefault();

            if (downloadedFile != null)
            {
                // Open the downloaded file
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = downloadedFile,
                    UseShellExecute = true
                });
            }
            else
            {
                Console.WriteLine("No downloaded file found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Uncomment the following line to close the browser after use
            // driver.Quit();
        }

        Console.WriteLine("Hello, World!");
    }

    static byte[] Encrypt(string data)
    {
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        return ProtectedData.Protect(dataBytes, null, DataProtectionScope.CurrentUser);
    }

    static string Decrypt(byte[] encryptedData)
    {
        byte[] decryptedBytes = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
        return Encoding.UTF8.GetString(decryptedBytes);
    }
}
