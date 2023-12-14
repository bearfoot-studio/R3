﻿
namespace R3.Tests.FactoryTests;

public class ReturnOnCompletedTest
{
    // test
    [Fact]
    public void ReturnOnCompleted()
    {
        {
            using var list = Event.ReturnOnCompleted<int>(Result.Success).ToLiveList();
            list.AssertEqual([]);
            list.AssertIsCompleted();
        }
        {
            var fakeTime = new FakeTimeProvider();

            using var list = Event.ReturnOnCompleted<int>(Result.Success, TimeSpan.FromSeconds(5), fakeTime).ToLiveList();

            fakeTime.Advance(TimeSpan.FromSeconds(4));
            list.AssertEqual([]);
            list.AssertIsNoResulted();

            fakeTime.Advance(TimeSpan.FromSeconds(1));
            list.AssertEqual([]);
            list.AssertIsCompleted();
        }
    }
}