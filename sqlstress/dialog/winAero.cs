using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace sqlstress.dialog
{
    public class winAero
    {
        private Form Window;
        /// <summary>
        /// 开启界面Aero效果
        /// </summary>
        #region 界面处理

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;
        }

        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern bool DwmIsCompositionEnabled(); //Dll 导入 DwmApi

        private MARGINS rectTomargins(Rectangle r)
        {
            MARGINS m = new MARGINS();
            m.Left = r.Left;
            m.Right = r.Right;
            m.Top = r.Top;
            m.Bottom = r.Bottom;
            return m;
        }

        public void OpenAero()
        {
            //如果启用Aero
            if (DwmIsCompositionEnabled())
            {
                MARGINS m = new MARGINS();
                m.Right = -1; //设为负数,则全窗体透明
                //MARGINS m = rectTomargins(pnWizard.ClientRectangle);
                DwmExtendFrameIntoClientArea(Window.Handle, ref m); //开启全窗体透明效果
                FixAeroGDI();
            }
            //base.OnLoad(e);
        }

        [DllImport("user32.dll")]//获得有关指定窗口的信息
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        private static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, int bAlpha, int dwFlags);

        private void FixAeroGDI()
        {
            int WS_EX_LAYERED = 0x00080000;
            int GWL_EXSTYLE = -20;
            int windowstyle = GetWindowLong(Window.Handle, GWL_EXSTYLE);
            SetWindowLong(Window.Handle, GWL_EXSTYLE, windowstyle | WS_EX_LAYERED);

            Color new_transparent = Color.FromArgb(100, 101, 102);
            int LWA_COLORKEY = 0x00000001;
            SetLayeredWindowAttributes(Window.Handle, ColorTranslator.ToWin32(new_transparent), 0, LWA_COLORKEY);

            if (DwmIsCompositionEnabled())
            {
                //MARGINS mrg = { -1 };
                //DwmExtendFrameIntoClientArea(m_hWnd, &mrg);
                //SetBackgroundColor(TRANSPARENT_COLOR);
                Window.BackColor = new_transparent;
            }
        }

        #endregion 界面处理

        public winAero(Form form)
        {
            Window = form;
        }
    }
}
