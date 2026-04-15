using Microsoft.Playwright;
using NUnit.Framework;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System;

namespace E2E.Tests
{
    public class BlazorPlaywrightTests
    {
        private Process? _serverProcess;
        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private string _url = "http://localhost:5002";  // Replace with your app URL and port from launchSettings.json.


        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            var projectPath = @"<Absolute path to your Blazor application's .csproj file>";
            var psi = new ProcessStartInfo("dotnet", $"run --project \"{projectPath}\" --urls {_url}")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            _serverProcess = Process.Start(psi);

            // wait for server to respond
            var http = new HttpClient();
            var started = false;
            for (int i = 0; i < 30; i++)
            {
                try
                {
                    var r = await http.GetAsync(_url);
                    if (r.IsSuccessStatusCode)
                    {
                        started = true;
                        break;
                    }
                }
                catch { }
                await Task.Delay(1000);
            }

            if (!started)
            {
                // capture output for diagnostics
                var outText = _serverProcess?.StandardOutput.ReadToEnd();
                var errText = _serverProcess?.StandardError.ReadToEnd();
                throw new Exception($"Server did not start in time. stdout:\n{outText}\n\nstderr:\n{errText}");
            }

            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 30 });
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _browser?.CloseAsync().GetAwaiter().GetResult();
            _playwright?.Dispose();
            if (_serverProcess != null && !_serverProcess.HasExited)
            {
                try { _serverProcess.Kill(true); } catch { }
            }
            _serverProcess?.Dispose();
        }

        [Test]
        public async Task SyncfusionButton_Works()
        {
            var page = await _browser!.NewPageAsync();
            await page.GotoAsync(_url + "/");
            await page.ClickAsync("text=Click Sync");
            var result = await page.InnerTextAsync("#sync-result");
            Assert.That(result, Is.EqualTo("Clicked"));
        }
    }
}
