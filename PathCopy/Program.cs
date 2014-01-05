using System;
using System.IO;

namespace PathCopy
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "--install")
            {
                InstallShellExtension();
            }
            else if(args[0] == "--uninstall")
            {
                UninstallShellExtension();
            }
            else
            {
                CopyPath(args[0]);
            }
            //Console.ReadLine();
        }

        private static void InstallShellExtension()
        {
            ShellExtension.AddFileShellExtension(GetCurrentExeFileInfo(), "Copy Path");
            ShellExtension.AddDirectoryShellExtension(GetCurrentExeFileInfo(), "Copy Path");
        }

        private static void UninstallShellExtension()
        {
            ShellExtension.RemoveFileShellExtension(GetCurrentExeFileInfo());
            ShellExtension.RemoveDirectoryShellExtension(GetCurrentExeFileInfo());
        }

        private static void CopyPath(string path)
        {
            System.Windows.Forms.Clipboard.SetText(path);
        }

        private static FileInfo GetCurrentExeFileInfo()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var currentExe = Path.Combine(currentDirectory, AppDomain.CurrentDomain.FriendlyName);
            var fileInfo = new FileInfo(currentExe);
            return fileInfo;
        }
    }
}
