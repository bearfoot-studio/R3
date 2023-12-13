﻿namespace R3;

public static partial class Event
{
    // Never
    public static Event<TMessage, TComplete> Never<TMessage, TComplete>()
    {
        return R3.Never<TMessage, TComplete>.Instance;
    }
}

internal sealed class Never<TMessage, TComplete> : Event<TMessage, TComplete>
{
    // singleton
    public static readonly Never<TMessage, TComplete> Instance = new Never<TMessage, TComplete>();

    Never()
    {

    }

    protected override IDisposable SubscribeCore(Subscriber<TMessage, TComplete> subscriber)
    {
        return Disposable.Empty;
    }
}
