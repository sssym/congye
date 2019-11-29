using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;//这是用到DllImport时候要引入的包

namespace congye_pe
{
    class ClassDLL
    {
        [DllImport("gdyj.dll", EntryPoint = "CoporationReg", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int CoporationReg();
        [DllImport("gdyj.dll", EntryPoint = "DLLInit", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int DLLInit(string str1,string str2);
        [DllImport("gdyj.dll", EntryPoint = "UploadInfo", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int UploadInfo(string str_in_VersionType, string str_in_upfilepath);
        [DllImport("gdyj.dll", EntryPoint = "GetDLLVersion", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetDLLVersion();
        [DllImport("gdyj.dll", EntryPoint = "DownloadNewDLL", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int DownloadNewDLL();
        [DllImport("gdyj.dll", EntryPoint = "UpdateDLL", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int UpdateDLL();

    }
}
