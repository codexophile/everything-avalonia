using System;
using System.Runtime.InteropServices;
using System.Text;

namespace everything_avalonia
{
    public static class EverythingSDK
    {
        private const string EVERYTHING_DLL = "Everything64.dll";


        // Basic search functions
    [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern void Everything_SetSearchW(string lpSearchString);

        [DllImport("Everything64.dll")]
        public static extern bool Everything_QueryW(bool bWait);

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetNumResults();

    // THIS IS THE LINE TO CHANGE (Function Name and CharSet)
        [DllImport(EVERYTHING_DLL, CharSet = CharSet.Unicode, EntryPoint = "Everything_GetResultFullPathNameW")]
        public static extern void Everything_GetResultFullPathNameW(int nIndex, StringBuilder lpString, int nMaxCount);
    // ^^^ Changed from Everything_GetResultFullPathW to Everything_GetResultFullPathNameW
    //     And added EntryPoint explicitly to be sure.
        [DllImport("Everything64.dll")]
        public static extern void Everything_CleanUp();

        // Additional methods for better error handling
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern bool Everything_IsDBLoaded();

        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetLastError();

        // Error codes constants
        public const uint EVERYTHING_OK = 0;
        public const uint EVERYTHING_ERROR_MEMORY = 1;
        public const uint EVERYTHING_ERROR_IPC = 2;
        public const uint EVERYTHING_ERROR_REGISTERCLASSEX = 3;
        public const uint EVERYTHING_ERROR_CREATEWINDOW = 4;
        public const uint EVERYTHING_ERROR_CREATETHREAD = 5;
        public const uint EVERYTHING_ERROR_INVALIDINDEX = 6;
        public const uint EVERYTHING_ERROR_INVALIDCALL = 7;
    }
}