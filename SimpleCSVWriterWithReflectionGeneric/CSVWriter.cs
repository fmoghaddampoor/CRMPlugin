using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVManager
{
	public class CSVWriter<T> where T : CSVableBase
	{
		public void Write(IEnumerable<T> objects, string destination)
		{
			//The null-coalescing operator ?? returns the value of its left-hand operand if it isn't null
			var objs = objects as IList<T> ?? objects.ToList();
			if (objs.Any())
			{
				using (var sw = new StreamWriter(destination))
				{
					foreach (var obj in objs)
					{
						sw.WriteLine(obj.ToCsv());
					}
				}
			}
		}

		public void Write(IEnumerable<T> objects, string destination,
							string[] propertyNames, bool isIgnore)
		{
			var objs = objects as IList<T> ?? objects.ToList();
			if (objs.Any())
			{
				using (var sw = new StreamWriter(destination))
				{
					foreach (var obj in objs)
					{
						sw.WriteLine(obj.ToCsv(propertyNames, isIgnore));
					}
				}
			}
		}

		public void Write(IEnumerable<T> objects, string destination,
				int[] propertyIndexes, bool isIgnore)
		{
			var objs = objects as IList<T> ?? objects.ToList();
			if (objs.Any())
			{
				using (var sw = new StreamWriter(destination))
				{
					foreach (var obj in objs)
					{
						sw.WriteLine(obj.ToCsv(propertyIndexes, isIgnore));
					}
				}
			}
		}
	}
}
