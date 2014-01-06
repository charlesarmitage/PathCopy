using System;
using System.IO;
using Microsoft.Win32;

namespace PathCopy
{
    class ShellExtension
    {
        public static void AddDirectoryShellExtension(FileInfo executable, string commandText)
        {
            const string directoryExtensionKey = @"Directory\shell\";
            AddShellExtensionToRegistry(directoryExtensionKey, executable, commandText);
        }

        public static void AddFileShellExtension(FileInfo executable, string commandText)
        {
            const string fileExtensionKey = @"*\shell\";
            AddShellExtensionToRegistry(fileExtensionKey, executable, commandText);
        }

        public static void RemoveFileShellExtension(FileInfo executable)
        {
            const string fileExtensionKey = @"*\shell\";
            RemoveShellExtensionFromRegistry(fileExtensionKey, executable);
        }

        public static void RemoveDirectoryShellExtension(FileInfo executable)
        {
            const string directoryExtensionKey = @"Directory\shell\";
            RemoveShellExtensionFromRegistry(directoryExtensionKey, executable);
        }

        private static void AddShellExtensionToRegistry(string extensionRootKey, FileInfo executable, string commandText)
        {
            var executableName = Path.GetFileNameWithoutExtension(executable.Name);
            var commandNameKey = extensionRootKey + executableName;
            AddKeyValuePair(commandNameKey, commandText);

            var commandSubKey = commandNameKey + @"\command";
            var commandValue = executable + @" ""%1""";
            AddKeyValuePair(commandSubKey, commandValue);
        }

        private static void RemoveShellExtensionFromRegistry(string extensionRootKey, FileInfo executable)
        {
            var executableName = Path.GetFileNameWithoutExtension(executable.Name);
            var commandNameKey = extensionRootKey + executableName;

            Console.WriteLine("Removing Key:{0}", commandNameKey);
            Registry.ClassesRoot.DeleteSubKeyTree(commandNameKey);
        }

        private static void AddKeyValuePair(string key, string value)
        {
            Console.WriteLine("Adding Key:{0} Value:{1}", key, value);

            using (var keyValuePair = Registry.ClassesRoot.CreateSubKey(key))
            {
                if (keyValuePair != null)
                {
                    keyValuePair.SetValue(null, value);
                }
            }
        }
    }
}