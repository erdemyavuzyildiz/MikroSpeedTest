using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MikroSpeedTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			List<string> stringList = new List<string>() {};

			for (int i = 0; i < 50000000; i++)
			{
				stringList.Add("ABCDEFG");
			}

			for (int i = 0; i < 20; i++)
			{
				stringList.AsParallel().ForAll(Calculate);
			}


			for (int i = 0; i < 30; i++)
			{
				File.WriteAllText("testfile.txt",string.Join("",stringList.Take(100000).ToArray()));
				File.Delete("testfile.txt");
			}

			stopwatch.Stop();

			var miliSeconds =stopwatch.ElapsedMilliseconds;
			var rounded = Math.Round((decimal)miliSeconds,2,MidpointRounding.AwayFromZero);
			MessageBox.Show(rounded.ToString());

		}

		public static void Calculate(string variable1) { }

	}
}
