using System;

public interface IScheduler
{
	void Schedule(double time, Action<IScheduler> action);
}

public class Scheduler : IScheduler
{
	public void Schedule(double time, Action<IScheduler> action)
	{

	}

	public void Wait(double time)
	{
		
	}
}

public class Hello
{
	public static Func<IScheduler> GetScheduler = delegate { return new Scheduler(); };

	public static void Main()
	{
		var scheduler = GetScheduler();

		scheduler.Schedule(2.0, s => Console.WriteLine("Hello"));
		scheduler.Schedule(1.0, s => Console.WriteLine("Tony"));
		scheduler.Schedule(3.0, s => Console.WriteLine("Hello"));
		scheduler.Schedule(4.0, s => Console.WriteLine("Tony"));

		Console.WriteLine("Hello, Tony");
	}
}