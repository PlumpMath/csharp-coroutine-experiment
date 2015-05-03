using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Scheduler
{
	public delegate IEnumerator Coroutine();
	private List<Tuple<double,Coroutine>> _scheduledTasks = new List<Tuple<double,Coroutine>>();
	
	public void Schedule(double time, Coroutine action)
	{
		_scheduledTasks.Add(new Tuple<double,Coroutine>(time, action));
	}

	public void Run()
	{
		while(_scheduledTasks.Any())
		{
			var sortedTasks = _scheduledTasks.OrderBy(t => t.Item1);
			var nextTask = sortedTasks.First();
			_scheduledTasks.RemoveAt(_scheduledTasks.IndexOf(nextTask));
			var time = nextTask.Item1;
			var timeToExecute = DateTime.Now.AddSeconds(time);
			while(DateTime.Now < timeToExecute) System.Threading.Thread.Sleep(0);
			var coroutine = nextTask.Item2();
			if(coroutine.MoveNext())
			{
				Schedule((double)coroutine.Current, () => coroutine);
			}
		}
	}
}
