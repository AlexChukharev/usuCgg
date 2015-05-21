using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace CGG
{
    public abstract class Arg
    {
        private static readonly Dictionary<string, double> Args = new Dictionary<string, double>();

        public static double Get(string s)
        {
            return Args[s];
        }

        protected static void Set(String s, double value)
        {
            Args[s] = value;
        }
    }

    public class FirstArgs : Arg
    {
        public FirstArgs(IList<TextBox> a)
        {
            Set("a", a[0].Text == "" ? -10 : double.Parse(a[0].Text));
            Set("b", a[1].Text == "" ? 10 : double.Parse(a[1].Text));
            Set("c", a[2].Text == "" ? 1 : double.Parse(a[2].Text));
            Set("d", a[3].Text == "" ? -2 : double.Parse(a[3].Text));
            Set("g", a[4].Text == "" ? 3 : double.Parse(a[4].Text));
        }
    }
    public class SecondArgs : Arg
    {
        public SecondArgs(IList<TextBox> a)
        {
            Set("xlb", a[0].Text == "" ? -10 : double.Parse(a[0].Text));
            Set("ylb", a[1].Text == "" ? -10 : double.Parse(a[1].Text));

            Set("xrt", a[2].Text == "" ? 10 : double.Parse(a[2].Text));
            Set("yrt", a[3].Text == "" ? 10 : double.Parse(a[3].Text));

            Set("a", a[4].Text == "" ? 1 : double.Parse(a[4].Text));
            Set("b", a[5].Text == "" ? 0 : double.Parse(a[5].Text));
        }
    }
	public class ThirdArgs : Arg
	{
		public ThirdArgs(IList<TextBox> a)
		{
			Set("a", a[0].Text == "" ? 10: double.Parse(a[0].Text));
			Set("b", a[0].Text == "" ? 11 : double.Parse(a[0].Text));
		}
	}

	public class FourthArgs : Arg
	{
		public FourthArgs(IList<TextBox> a)
		{

		}
		
	}
}
