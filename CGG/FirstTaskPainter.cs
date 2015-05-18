using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CGG
{
	static class FirstTaskPainter
	{
        /** get x from [arg, b] by xx
         * param double xx, arg, b, maxX
         */
	    private static double GetX(double xx, double a, double b, double maxX)
	    {
	        return a + xx*(b - a)/maxX;
	    }

	    private static double GetLayout(double c, double a, double b, double max)
	    {
	        return (c - a)/(b - a)*max;
	    } 

		public static void Drow(Arg arg, Canvas canvas)
		{
            double d = Arg.Get("d"), g = Arg.Get("g"), a = Arg.Get("a"), b = Arg.Get("b");
		    var h = canvas.Height;
		    var w = canvas.Width;
		    var dx = GetX(2, 0, b - a, w);

			var minY = Math.Min(Math.Min(F(-d - dx), F(-g - dx)), Math.Min(F(-d + dx), F(-g + dx)));
			var maxY = Math.Max(Math.Max(F(-d + dx), F(-g + dx)), Math.Max(F(-d - dx), F(-g - dx)));

		    if (-d > -g)
		    {
		        var buf = g;
		        g = d;
		        d = buf;
		    }
		    var dd = GetLayout(-d, a, b, w);
		    var gg = GetLayout(-g, a, b, w);
            
		    DrowOnAb(0, dd - 1, a, b, minY, maxY, canvas);
            DrowLine(dd - 2, GetLayout(F(GetX(dd - 2, a, b, w)), minY, maxY, h), dd - 2, F(GetX(dd - 2, a, b, w)) > 0 ? h : 0, canvas, Brushes.Gray);

            DrowLine(dd + 2, GetLayout(F(GetX(dd + 2, a, b, w)), minY, maxY, h), dd + 2, F(GetX(dd + 2, a, b, w)) > 0 ? h : 0, canvas, Brushes.Gray);
            DrowOnAb(dd + 2, gg - 1, a, b, minY, maxY, canvas);
            DrowLine(gg - 2, GetLayout(F(GetX(gg - 2, a, b, w)), minY, maxY, h), gg - 2, F(GetX(gg - 2, a, b, w)) > 0 ? h : 0, canvas, Brushes.Gray);

            DrowLine(gg + 2, GetLayout(F(GetX(gg + 2, a, b, w)), minY, maxY, h), gg + 2, F(GetX(gg + 2, a, b, w)) > 0 ? h : 0, canvas, Brushes.Gray);
            DrowOnAb(gg + 2, w, a, b, minY, maxY, canvas);


            /* оси */
            #region
            {
                double xox = 0, xoy1 = maxY, xoy2 = minY;
                xoy1 = (xoy1 - minY) * canvas.Height / (maxY - minY);
                xox = (xox - a) * canvas.Width / (b - a);
                xoy2 = (xoy2 - minY) * canvas.Height / (maxY - minY);
                DrowLine(xox, xoy1, xox, xoy2, canvas, Brushes.BlueViolet);
            }
            {
                double xox = 0, xoy1 = b, xoy2 = a;
                xoy1 = (xoy1 - a) * canvas.Width / (b - a);
                xox = (xox - minY) * canvas.Height / (maxY - minY);
                xoy2 = (xoy2 - a) * canvas.Width / (b - a);
                DrowLine(xoy1, xox, xoy2, xox, canvas, Brushes.BlueViolet);
            }
            #endregion

		}

		private static double F(double x)
		{
			var c = Arg.Get("c");
			var d = Arg.Get("d");
			var g = Arg.Get("g");
			return (x + c)/(x + d)/(x + g);
		}

	    private static void DrowOnAb(double xxBegin, double xxEnd, double a, double b, double minY, double maxY, Panel canvas)
	    {
	        var w = canvas.Width;
	        var h = canvas.Height;
            var x1 = GetX(xxBegin, a, b, w);
            var y1 = F(x1);
            var xx1 = GetLayout(x1, a, b, w);
            var yy1 = GetLayout(y1, minY, maxY, h);

            for (var xx = xxBegin + 1; xx < xxEnd; xx++)
            {
                var x2 = GetX(xx, a, b, w);
                var y2 = F(x2);
                var xx2 = GetLayout(x2, a, b, w);
                var yy2 = GetLayout(y2, minY, maxY, h);
                DrowLine(xx1, yy1, xx2, yy2, canvas, Brushes.Gray);
                xx1 = xx2;
                yy1 = yy2;
            }
	    }

        private static void DrowLine(double x1, double y1, double x2, double y2, Panel canvas, Brush color)
        {
            var line = new Line
            {
                X1 = x1,
                Y1 = canvas.Height - y1,
                X2 = x2,
                Y2 = canvas.Height - y2,
                Stroke = color,
                StrokeThickness = 1
            };
            canvas.Children.Add(line);
	    }
	}
}
