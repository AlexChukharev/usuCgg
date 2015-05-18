using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CGG
{
    static class SecondTaskPainter
    {
        public static void Drow(Arg args, Canvas canvas)
        {
            #region
            var h = canvas.Height;
            var w = canvas.Width;
            var l = (int) Arg.Get("xlb");
            var r = (int) Arg.Get("xrt");
            var maxY = (int) Arg.Get("yrt");
            var minY = (int) Arg.Get("ylb");
            var a = Arg.Get("a");
            var b = Arg.Get("b");
            #endregion
            // bounds of interval
            #region
            double dx1, dy1, dx2, dy2;
            if (F(args, l) < minY)
            {
                dy1 = minY;
                dx1 = (minY - b) / a;
            }
            else if (F(args, l) > maxY)
            {
                dy1 = maxY;
                dx1 = (maxY - b) / a;
            }
            else
            {
                dx1 = l;
                dy1 = F(args, l);
            }
            if (F(args, r) > maxY)
            {
                dy2 = maxY;
                dx2 = (maxY - b) / a;
            }
            else if (F(args, r) < minY)
            {
                dy2 = minY;
                dx2 = (minY - b) / a;
            } 
            else
            {
                dx2 = r;
                dy2 = F(args, r);
            }
            var x1 = (int)(w/(r - l)*(dx1 - l));
            var x2 = (int)(w/(r - l)*(dx2 - l));
            var y1 = (int)(h/(maxY - minY)*(dy1 - minY));
            var y2 = (int)(h/(maxY - minY)*(dy2 - minY));
            #endregion
            
            var steep = Math.Abs(x1 - x2) > Math.Abs(y1 - y2);
            if (!steep)
            {
                Swap(ref x1, ref y1);
                Swap(ref x2, ref y2);
            }
            if (x1 > x2)
            {
                Swap(ref x1, ref x2);
                Swap(ref y1, ref y2);
            }

            var dx = x2 - x1;
            var dy = Math.Abs(y2 - y1);
            var error = dx/2;
            var yStep = y2 > y1 ? 1 : -1;
            var y = y1;
            for (var x = x1; x <= x2; x++)
            {
                DrowPoint(canvas, steep ? x : y, steep ? y : x);
                error -= dy;
                if (error >= 0) continue;
                y += yStep;
                error += dx;
            }
        }

        static void DrowPoint(Panel canvas, int x, int y)
        {
            var line = new Ellipse
            {
                Height = 1,
                Width = 1,
                Margin = new Thickness(x, canvas.Height - y, 0, 0),
                Stroke = Brushes.Gray,
                StrokeThickness = 2
            };
            canvas.Children.Add(line);
        }

        private static void Swap(ref int a, ref int b)
        {
            var x = a;
            a = b;
            b = x;
        }

        static double F(Arg args, double x)
        {
            return Arg.Get("a")*x + Arg.Get("b");
        }
    }
}
