using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capper
{


    static class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

       [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
 
     
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new AppCtx());


        }

        private class AppCtx : ApplicationContext
        {
            public AppCtx()
            {
                Random rnd = new Random();
                Thread.Sleep(1000);
                while (true)
                {
                    if (!IsCapslockOn)
                    {
                        const int KEYEVENTF_EXTENDEDKEY = 0x1;
                        const int KEYEVENTF_KEYUP = 0x2;
                        keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                        keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,
                        (UIntPtr)0);
                    }
                    Thread.Sleep(new TimeSpan(0, rnd.Next(1, 30), rnd.Next(0, 59)));
                   
                }
            }
        }

        

        private static bool IsCapslockOn
        {
            get
            {
                return (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            }
        }
    }

    
}
