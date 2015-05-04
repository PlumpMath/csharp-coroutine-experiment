using System;
using System.Collections;

public class Hello
{
	public static void Main()
	{
		var scheduler = new Scheduler();

		scheduler.Schedule((double)0.0, SomeTask);
		scheduler.Schedule((double)0.25, OtherTask);

		scheduler.Run();
	}
	
	public static IEnumerator SomeTask()
	{
		Console.WriteLine("Hello");
		yield return 0.25;
		Console.WriteLine("World");
		yield return 0.25;
		Console.WriteLine("Print this last");
	}
	
	public static IEnumerator OtherTask()
	{
		Console.WriteLine("Tony And");
		yield break;
	}
}