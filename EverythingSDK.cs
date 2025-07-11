using System;
using System.Runtime.InteropServices;
using System.Text;

// This static class will hold all the direct calls to the Everything DLL.
public static class EverythingSDK
{
    // Define which DLL to import from. We use the 64-bit version.
    private const string EVERYTHING_DLL = "Everything64.dll";

    // Import the C functions from the DLL, making them available to C#.
    // We use MarshalAs to handle the string types correctly.
    [DllImport(EVERYTHING_DLL, CharSet = CharSet.Unicode)]
    public static extern void Everything_SetSearchW(string lpSearchString);

    [DllImport(EVERYTHING_DLL)]
    public static extern bool Everything_QueryW(bool bWait);

    [DllImport(EVERYTHING_DLL)]
    public static extern int Everything_GetNumResults();

    [DllImport(EVERYTHING_DLL, CharSet = CharSet.Unicode)]
    public static extern void Everything_GetResultFullPathW(int nIndex, StringBuilder lpString, int nMaxCount);

    // It's good practice to have a cleanup function, though not strictly required.
    [DllImport(EVERYTHING_DLL)]
    public static extern void Everything_CleanUp();
}