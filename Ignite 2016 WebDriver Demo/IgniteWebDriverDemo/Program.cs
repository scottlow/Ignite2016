using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.IO;

namespace IgniteWebDriverDemo
{
    class Program
    {
        static string edgeSideloadPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Packages\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\LocalState\Extensions\";
        static private RemoteWebDriver _driver;

        /// <summary>
        /// Copies extension source code from extensionPath to the necessary directory.</summary>
        /// <param name="extensionPath"> The absolute path of the extension to be side loaded.</param>
        static private string CopyExtensionForSideload(string extensionPath)
        {
            Process.Start("xcopy",
                string.Format(@"""{0}"" ""{1}"" /s/h/e/k/f/c", extensionPath, edgeSideloadPath));
            return edgeSideloadPath;
        }
        /// <summary>
        /// Deletes the side loading directory if it exists.</summary>
        static public void RemoveSideloadDirectory()
        {
            if (Directory.Exists(edgeSideloadPath))
            {
                Directory.Delete(edgeSideloadPath, true);
            }
        }
        static void Main(string[] args)
        {
            EdgeOptions edgeOptions = new EdgeOptions();
            var extensionPath = @"C:\Users\sclow\Documents\Ignite\IgniteExtensionSample";
            if (extensionPath != null)
            {
                RemoveSideloadDirectory();
                extensionPath = CopyExtensionForSideload(extensionPath);

                // Create the extensionPaths capability object
                string[] extensionPaths = new string[] { extensionPath };
                edgeOptions.AddAdditionalCapability("extensionPaths", extensionPaths);
            }

            // Create a new EdgeDriver with the specified capabilities object
            _driver = new EdgeDriver(edgeOptions);
            _driver.Manage().Window.Maximize();
            _driver.Url = "https://www.bing.com";
        }
    }
}
