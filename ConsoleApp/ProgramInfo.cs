using System;
using System.Reflection;

namespace ConsoleApp
{
    internal sealed class ProgramInfo
    {
        internal static void GetInformation()
        {
            const string ProgramAuthor = "Ken Hoo";
            string ProgramName = Assembly.GetExecutingAssembly().GetName().Name ?? "Windows CLI Installer";

            Version? ProgramVersion = Assembly.GetExecutingAssembly().GetName().Version;
#if DEBUG
            AssemblyConfigurationAttribute? ConfigurationAttribute = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyConfigurationAttribute>();

            string? ConfigurationMode = ConfigurationAttribute?.Configuration;

            Console.Title = $"[{ConfigurationMode?.ToString()}] {ProgramName}";
            Console.WriteLine($"Welcome to the {ProgramName} tool!\nCurrent version: {ProgramVersion}-testing\nCreated by {ProgramAuthor}");
#else
                Console.Title = $"{ProgramName}";
                Console.WriteLine($"Welcome to the {ProgramName} tool!\nCurrent version: {ProgramVersion}\nCreated by {ProgramAuthor}");
#endif
        }
    }
}
