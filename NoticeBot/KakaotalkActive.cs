using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NoticeBot
{
    class KakaotalkActive
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [Out] StringBuilder lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        public IntPtr prehandle;
        public static IntPtr SetPrevious(IntPtr handle, string classname, int time)
        {
            var previous = IntPtr.Zero;
            var nowhandle = FindWindowEx(handle, previous, classname, null);
            for (int i = 0; i < time; i++)
            {
                previous = nowhandle;
                nowhandle = FindWindowEx(handle, previous, classname, null);
            }

            return nowhandle;

        }

        public KakaotalkActive(string name, string context)
        {
            IntPtr kakaohd = FindWindow("EVA_Window_Dblclk", "카카오톡");
            IntPtr mainhd = FindWindowEx(kakaohd, IntPtr.Zero, "EVA_ChildWindow", null);
            IntPtr chathd = SetPrevious(mainhd, "EVA_Window", 1);
            IntPtr freindsrhd = SetPrevious(chathd, "Edit", 0);
            IntPtr resulthd = SetPrevious(chathd, "EVA_VH_ListControl_Dblclk", 1);
            SendMessage(freindsrhd, 0x000c, IntPtr.Zero, name);
            Thread.Sleep(500);
            NativeHelper.LbuttonDoubleClick(resulthd, 0, 0);
            Thread.Sleep(300);
            IntPtr talkhd = FindWindow("#32770", null); 
            IntPtr talktexthd = FindWindowEx(talkhd, IntPtr.Zero, "RichEdit20W", "");
            SendMessage(talktexthd, 0x000c, IntPtr.Zero, context);
            PostMessage(talktexthd, 0x0100, 0xD, 0x1C001);
            Thread.Sleep(200);
            SendMessage(talkhd, 0x0010, IntPtr.Zero, "0");

        }



    }
}
