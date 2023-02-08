using CS_CLIB;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace sorec_gamma.modules.ModuleMAJ
{
    class TerminalMonitor
    {
        private static Tracing logger = new Tracing();
        public static void Shutdown()
        {
            var psi = new ProcessStartInfo("shutdown", "/s /f /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }

        public static void Reboot()
        {
            var psi = new ProcessStartInfo("shutdown", "/r /f /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }

        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        [DllImport("shell32")]
        public static extern bool IsUserAnAdmin();

        public static void Hibernate()
        {
            SetSuspendState(true, true, true);
        }

        public static void Sleep()
        {
            SetSuspendState(true, true, true);
        }

        public static bool AreUsbPortsEnabled()
        {
            string UsbStoreRegpath = "System\\CurrentControlSet\\Services\\USBSTOR";
            try
            {
                RegistryKey UsbStoreRegkey = Registry.LocalMachine.OpenSubKey(UsbStoreRegpath, true);
                int usbStoreStart = (int)UsbStoreRegkey.GetValue("Start");
                return usbStoreStart == 3;
            } catch (Exception ex)
            {
                logger.addLog(string.Format("Checking USB ports: {0}", ex.Message), 0);
                return false;
            }
        }

        public static bool EnableUsbPorts(bool enable)
        {
            string UsbStoreRegpath = "System\\CurrentControlSet\\Services\\USBSTOR";
            try
            {
                RegistryKey UsbStoreRegkey = Registry.LocalMachine.OpenSubKey(UsbStoreRegpath, true);
                UsbStoreRegkey.SetValue("Start", enable ? 3 : 4);
                return true;
            }
            catch (Exception ex)
            {
                logger.addLog(string.Format("Enable({0}) USB ports: {1}", enable, ex.Message), 0);
                return false;
            }
        }

        public static void SetScreensToExtendedMode()
        {
            try
            {
                var psi = new ProcessStartInfo(@"C:\WINDOWS\bureauEtendu.exe");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                psi.Verb = "runas";
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.Error("SetScreensToExtendedMode ERROR: " + ex.Message);
            }
        }
        public static void SetFormToScreen(Form self, Rectangle boundsScreen)
        {
            var oldState = self.WindowState;
            // If form is currently maximized ...
            if (oldState == FormWindowState.Maximized)
            {
                self.WindowState = FormWindowState.Normal;
                self.StartPosition = FormStartPosition.Manual;
                self.Location = boundsScreen.Location;
                self.WindowState = FormWindowState.Maximized;
            }
            else
            {
                self.StartPosition = FormStartPosition.Manual;
                // Center into new screen
                var sizeGaps = boundsScreen.Size - self.Size;
                var x = boundsScreen.Left + (sizeGaps.Width / 2);
                var y = boundsScreen.Top + (sizeGaps.Height / 2);
                self.Location = new Point(x, y);
            }
        }
    }
}
