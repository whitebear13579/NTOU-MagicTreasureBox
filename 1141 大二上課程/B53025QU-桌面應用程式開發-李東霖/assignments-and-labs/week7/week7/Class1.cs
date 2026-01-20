using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace week7
{
    internal class KeyboardHook
    {
        public event KeyEventHandler KeyDownEvent;
        public event KeyPressEventHandler KeyPressEvent;
        public event KeyEventHandler KeyUpEvent;
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        static int hKeyboardHook = 0; //宣告鍵盤掛勾的初始值
                                      //對應數徝可在Microsoft SDK的Winuser.h里查詢
        public const int WH_KEYBOARD_LL = 13; //單程序監控投為2，全系統監控設為13
        HookProc KeyboardHookProcedure; //宣告KeyboardHookProcedure為HookProc類型
                                        //鍵盤結構
        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode; //虛擬鍵碼。範圍1至254
            public int scanCode; // 硬體掃描碼
            public int flags; // 旗標
            public int time; // 時間戳記
            public int dwExtraInfo; // 額外資訊
        }

        //用來安裝掛勾
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        //用來卸載掛勾
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        //用來呼叫下一個掛鉤
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);
        // 取得當前程序編號（如採單程序監控投需要）
        [DllImport("kernel32.dll")]
        static extern int GetCurrentThreadId();
        //使用WINDOWS API函數取代取得目前執行個體的函數,防止掛鉤失效
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);

        public void Start()
        {
            // 安裝鍵盤掛鉤
            if (hKeyboardHook == 0)
            {
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure,
                GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName), 0);
                //如果SetWindowsHookEx失敗
                if (hKeyboardHook == 0)
                {
                    Stop();
                    throw new Exception("安裝鍵盤掛鉤失敗");
                }
            }
        }
        public void Stop()
        {
            bool retKeyboard = true;
            if (hKeyboardHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
            }
            if (!(retKeyboard)) throw new Exception("卸載掛鉤失敗!");
        }

        //將指定的虛擬鍵碼和鍵盤狀態的轉換成對應字元
        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, //[in] 虛擬關鍵程式碼
        int uScanCode, // [in] 硬體掃描碼
        byte[] lpbKeyState, // [in] 256位元組指針，包含目前鍵盤的狀態。
        byte[] lpwTransKey, // [out] 指標的緩衝區：用來接收字元。
        int fuState); // [in] 活動狀態
                      //取得按鍵的狀態
        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(int vKey);
        private const int WM_KEYDOWN = 0x100;//KEYDOWN
        private const int WM_KEYUP = 0x101;//KEYUP
        private const int WM_SYSKEYDOWN = 0x104;//SYSKEYDOWN
        private const int WM_SYSKEYUP = 0x105;//SYSKEYUP

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            // 偵聽鍵盤事件
            if ((nCode >= 0) && (KeyDownEvent != null || KeyUpEvent != null || KeyPressEvent != null))
            {
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                // raise KeyDown
                if (KeyDownEvent != null && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    KeyDownEvent(this, e);
                }
                //鍵盤按下
                if (KeyPressEvent != null && wParam == WM_KEYDOWN)
                {
                    byte[] keyState = new byte[256];
                    GetKeyboardState(keyState);
                    byte[] inBuffer = new byte[2];
                    if (ToAscii(MyKeyboardHookStruct.vkCode, MyKeyboardHookStruct.scanCode, keyState, inBuffer, MyKeyboardHookStruct.flags) == 1)
                    {
                        KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
                        KeyPressEvent(this, e);
                    }
                }
                if (KeyUpEvent != null && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    KeyUpEvent(this, e);
                }
            }
            //如果回傳1，則結束訊息，這個訊息到此為止，不再傳遞。
            //如果回傳0或呼叫CallNextHookEx函數則訊息出了這個掛鉤繼續往下傳遞，也就是傳給訊息真正的接受者
            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }
        ~KeyboardHook()
        {
            Stop();
        }
    }
}
