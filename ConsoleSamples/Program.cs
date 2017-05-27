using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSamples
{
	class Program
	{
		static void Main(string[] args)
		{
			int[] numbers = { 0, 2, 4, 8, 16, 32, 64 };
			object[] objects = { 0, new object[] { "test", 0 }, "str", 10, "13" };

			//normal tupple call
			var tup = Tally(numbers);
			Console.WriteLine(tup.sum);

			//deconstruction tupple call
			(int s1, int c1) = Tally(numbers);

			//deconstruction with discarded values
			(int s2, _) = Tally(numbers);

			//implicit declaration of all the types of variables
			var (s, _) = TallyObjects(objects);

		}

		static (int sum, int count) Tally(int[] numbers)
		{
			var r = (s: 0, c: 0);
			foreach (var number in numbers)
			{
				Add(number, 1);
			}
			return r;
			//local functions
			void Add(int s, int c) => r = (r.s + s, r.c + c);
		}

		static (int sum, int count) TallyObjects(object[] objects)
		{
			var r = (s: 0, c: 0);
			foreach (var obj in objects)
			{
				//inline temporary variable which is used further
				//if (obj is int i)
				//{
				//	Add(i, 1);
				//}

				//pattern matching in switch statements
				switch (obj)
				{
					case int num:
						Add(num, 1);
						break;
					case object[] v when v.Length > 0:
						var (sum, count) = TallyObjects(v);
						Add(sum, count);
						break;
					case null:
					case object[] _:
						break;
					case string str when int.TryParse(str, out var num): //num used like this does not conflict with the one from case int num because they are in their local scope
						//one can also use the discard parameter _ to discard the result of a TryParse
						Add(num, 1);
						break;
					default:
						throw new ArgumentException();
				}
			}
			return r;

			void Add(int s, int c) => r = (r.s + s, r.c + c);
		} 


	}
}
