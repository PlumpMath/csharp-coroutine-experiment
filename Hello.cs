using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Scheduler
{
	List<Tuple<double,IEnumerator>> ScheduledTasks = new List<Tuple<double,IEnumerator>>();
	
	public void Schedule(double time, IEnumerator action)
	{
		ScheduledTasks.Add(new Tuple<double,IEnumerator>(time,action));
	}

	public void Run()
	{
		while(ScheduledTasks.Any())
		{
			var sortedTasks = ScheduledTasks.OrderBy(t => t.Item1);
			var nextTask = sortedTasks.First();
			ScheduledTasks.RemoveAt(ScheduledTasks.IndexOf(nextTask));
			var time = nextTask.Item1;
			var coroutine = nextTask.Item2;
			if(coroutine.MoveNext())
			{
				Console.WriteLine(coroutine.Current);
				Schedule((double)coroutine.Current, coroutine);
			}	
		}
	}
}

public class Hello
{
	public static void Main()
	{
		var scheduler = new Scheduler();

		scheduler.Schedule((double)1.0, SomeTask());
		scheduler.Schedule((double)2.0, OtherTask());

		scheduler.Run();
	}
	
	public static IEnumerator SomeTask()
	{
		Console.WriteLine("Hello");
		yield return 3.0;
		Console.WriteLine("World");
		yield return 4.0;
		Console.WriteLine("Print this last");
	}
	
	public static IEnumerator OtherTask()
	{
		yield return 2.0;
		Console.WriteLine("Tony And");
		yield break;
	}
}