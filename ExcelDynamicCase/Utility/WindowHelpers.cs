using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExcelDynamicCase.Utility
{
    static class WindowHelpers
    {
        private const int SW_RESTORE = 9;   // Restore & activate

        [DllImport("user32.dll")] static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")] static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        public static void ActivateWindow(Process proc)
        {
            // Wait until the process is ready to receive messages
            proc.WaitForInputIdle();

            // Poll until the main‐window handle appears
            while (proc.MainWindowHandle == IntPtr.Zero && !proc.HasExited)
            {
                Thread.Sleep(50);
                proc.Refresh();            // updates MainWindowHandle
            }

            if (proc.HasExited) return;     // child quit before showing a window

            // Make sure it’s not minimised, then bring it to the foreground
            ShowWindowAsync(proc.MainWindowHandle, SW_RESTORE);
            SetForegroundWindow(proc.MainWindowHandle);
        }


    }
}
