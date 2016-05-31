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

			CpuTest(stringList);

			HddTest(stringList);

			stopwatch.Stop();

			var miliSeconds =stopwatch.ElapsedMilliseconds;
			var rounded = Math.Round((decimal)miliSeconds,2,MidpointRounding.AwayFromZero);
			MessageBox.Show("Completed in "+rounded + " milliseconds.");

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
