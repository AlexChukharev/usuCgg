using System;
using System.Windows.Controls;

namespace CGG
{
    static class MainFunction
    {
        public static void Function(Arg a, Canvas c, Action<Arg, Canvas> f)
        {
            c.Children.Clear();
            f(a, c);
        }
    }
}
