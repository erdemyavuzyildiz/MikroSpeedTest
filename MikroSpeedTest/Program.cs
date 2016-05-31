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
			decimal cpuSpeed = 0;
			decimal hddSpeed = 0;
			List<string> stringList = new List<string>() {};


			stopwatch.Start();
			CpuTest(stringList);
			stopwatch.Stop();
			cpuSpeed = stopwatch.ElapsedMilliseconds;


			stopwatch.Restart();
			HddTest(stringList);
			stopwatch.Stop();
			hddSpeed = stopwatch.ElapsedMilliseconds;
			

			var miliSeconds =stopwatch.ElapsedMilliseconds;
			var rounded = Math.Round((decimal)miliSeconds,2,MidpointRounding.AwayFromZero);
			MessageBox.Show(
			"Completed in "+ (cpuSpeed+hddSpeed) + " milliseconds." + System.Environment.NewLine+
			"CPU in "+cpuSpeed + " milliseconds." + System.Environment.NewLine+
			"HDD in "+hddSpeed + " milliseconds." + System.Environment.NewLine
			);

		}

		private static void HddTest(List<string> stringList)
		{
			int fileSize = 100000;
			for (int i = 0; i < 100; i++)
			{
				File.WriteAllText("testfile.txt", string.Join("", stringList.Take(fileSize).ToArray()));
				File.Delete("testfile.txt");
				fileSize -= 100000/200;
			}
		}

		private static void CpuTest(List<string> stringList)
		{
			for (int i = 0; i < 50000000; i++)
			{
				stringList.Add("ABCDEFG");
			}

			for (int i = 0; i < 20; i++)
			{
				stringList.AsParallel().ForAll(Calculate);
			}
		}

		public static void Calculate(string variable1) { }

	}
}
