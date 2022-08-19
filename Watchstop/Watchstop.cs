using System.Diagnostics;

namespace Watchstop;

public static class Watchstop
{
    // ReSharper disable once InconsistentNaming
    private static readonly Stopwatch _stopwatch = new ();
    private const string Format = "{0} finished in {1}ms.";

    public static void Measure(Action action, string message = "")
    {
        var sw = Stopwatch.StartNew();
        action.Invoke();
        sw.Stop();
        Console.WriteLine(Format, message, sw.ElapsedMilliseconds);
    }
    
    public static T Measure<T>(Action action, string message = "")
    {
        var sw = Stopwatch.StartNew();
        var result = action.DynamicInvoke();
        sw.Stop();
        Console.WriteLine(Format, message, sw.ElapsedMilliseconds);
    
        return (T)result;
    }
    
    public static T StartTimer<T>(this T value)
    {
        _stopwatch.Restart();
        return value;
    }
    
    public static T StopTimer<T>(this T value, string message = "")
    {
        if (!_stopwatch.IsRunning)
        {
            throw new ApplicationException("Stopwatch was not started. Please call StartTimer<T>() first.");
        }
        
        _stopwatch.Stop();
        Console.WriteLine(Format, message, _stopwatch.ElapsedMilliseconds);
        return value;
    }
    
    public static async Task LogTime(this Task task, string message = "")
    {
        var sw = Stopwatch.StartNew();
        await task;
        sw.Stop();
        Console.WriteLine(Format, message, sw.ElapsedMilliseconds);
    }
    
    public static async Task<T> LogTime<T>(this Task<T> task, string message = "")
    {
        var sw = Stopwatch.StartNew();
        var result = await task;
        sw.Stop();
        Console.WriteLine(Format, message, sw.ElapsedMilliseconds);

        return result;
    }
}