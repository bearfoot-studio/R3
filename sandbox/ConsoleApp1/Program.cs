﻿using Microsoft.Extensions.Logging;
using R3;
using R3.Internal;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Channels;
using ZLogger;

SubscriptionTracker.EnableTracking = true;
SubscriptionTracker.EnableStackTrace = true;

using var factory = LoggerFactory.Create(x =>
{
    x.SetMinimumLevel(LogLevel.Trace);
    x.AddZLoggerConsole();
});
EventSystem.Logger = factory.CreateLogger<EventSystem>();
var logger = factory.CreateLogger<Program>();



var ct = new CancellationTokenSource(1000);
EventSystem.DefaultFrameProvider = new ThreadSleepFrameProvider(60);


//var t = new Thread(() =>
//{
//    while (true)
//    {
//        Console.WriteLine("loop"); Thread.Sleep(60);
//    }
//});
//t.IsBackground = true;
//t.Start();

//var s = new NewThreadScheduler(_ => new Thread(() => { while (true) { Console.WriteLine("loop"); Thread.Sleep(60); } }));

//s.Schedule(() => Console.WriteLine("do once"));
//using var f = new ThreadSleepFrameProvider(60);

var source = Event.EveryUpdate(ct.Token);



source.DoOnDisposed(() => {/*Console.WriteLine("DISPOSED")*/}).WriteLine();



// TODO: if WaitAsync is using, not disposed???
await source.WaitAsync();
Console.WriteLine("Press Key to done.");

Console.ReadLine();

SubscriptionTracker.ForEachActiveTask(x =>
{
    Console.WriteLine(x);
});

public static class Extensions
{
    public static IDisposable WriteLine<T, U>(this Event<T, U> source)
    {
        return source.Subscribe(x => Console.WriteLine(x), _ => Console.WriteLine("COMPLETED"));
    }
}


class TestDisposable : IDisposable
{
    public int CalledCount = 0;

    public void Dispose()
    {
        CalledCount += 1;
    }
}
