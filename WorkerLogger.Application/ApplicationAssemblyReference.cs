using System.Reflection;

namespace WorkerLogger.Application;

public class ApplicationAssemblyReference
{
    internal static readonly Assembly assembly = typeof(ApplicationAssemblyReference).Assembly; 
}