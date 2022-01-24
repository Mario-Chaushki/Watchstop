using System.Diagnostics;
using System.Reflection;

namespace Watchstop;

public static class Measure
{
    private const string Format = "Method{0} finished in {1}ms";

    public static void Method(Action action, string methodName = "")
    {
        var sw = Stopwatch.StartNew();
        action.Invoke();
        sw.Stop();
        Console.WriteLine(Format, GetFormattedMethodName(methodName), sw.ElapsedMilliseconds);
    }

    public static async Task LogTime(this Task task, string methodName = "")
    {
        var sw = Stopwatch.StartNew();
        await task;
        sw.Stop();
        Console.WriteLine(Format, GetFormattedMethodName(methodName), sw.ElapsedMilliseconds);
    }

    private static string GetFormattedMethodName(string method)
    {
        return string.IsNullOrWhiteSpace(method) ? method : $" {method}";
    }
}