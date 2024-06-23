using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AdvantageClientEngine;

namespace Advantage.Data.Native
{
    public static class NativeBoot
    {
        public static void RegisterDefault()
            => Register(null, typeof(ACE));

        public static void Register(Assembly? assembly = null, Type? type = null)
        {
            var ass = assembly
                      ?? (type == null ? Assembly.GetCallingAssembly() : Assembly.GetAssembly(type))
                      ?? Assembly.GetCallingAssembly();

            var assDir = ass.Location;
            assDir = Path.GetDirectoryName(assDir);

            var exeDir = Assembly.GetEntryAssembly()?.Location;
            exeDir = Path.GetDirectoryName(exeDir);

            var currDir = Environment.CurrentDirectory;
            var rtDir = FindRuntimeDir(assDir, exeDir, currDir);

            Console.WriteLine($" HACK1 {assDir} ?? ");
            Console.WriteLine($" HACK2 {exeDir} ?? ");
            Console.WriteLine($" HACK3 {currDir} ?? ");
            Console.WriteLine($" HACK4 {rtDir} ?? ");
            
            CopyFiles(rtDir!, assDir!);
        }

        private static void CopyFiles(string source, string target)
        {
            foreach (var file in Directory.GetFiles(source, "*.*"))
            {
                var label = file.Replace(source, string.Empty).Trim(Path.DirectorySeparatorChar);
                var dest = Path.Combine(target, label);

                Console.WriteLine(" # " + file + "   --- " + dest);

                File.Copy(file, dest, overwrite: true);
            }
        }

        private static string? FindRuntimeDir(params string?[] dirs)
        {
            var platFolder = OperatingSystem.IsLinux() ? "linux-x64"
                : OperatingSystem.IsWindows() ? "win-x64"
                : throw new InvalidOperationException("Not supported OS!");
            foreach (var dir in dirs.Distinct())
            {
                if (dir == null)
                    continue;
                var rtDir = Path.Combine(dir, "runtimes", platFolder, "native");
                if (Directory.Exists(rtDir))
                    return rtDir;
            }
            return null;
        }
    }
}