using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CGG
{
	class ThirdTaskPainter
	{
		public static void Drow(Arg args, Canvas canvas)
		{
			var h = (int) canvas.Height;
			var w = (int) canvas.Width;
			var a = Arg.Get("a");
			var b = Arg.Get("b");
			int k;
			if (a > b)
				k = (int) (w/a);
			else
				k = (int) (h/b);
			var xRadius = (int)a * k / 2;
			var yRadius = (int)b * k / 2;

			var xr2 = xRadius * xRadius * 2;
			var yr2 = yRadius * yRadius * 2;
			var x = 0;
			var y = yRadius;
			var d = (yr2 - xr2 * y) + xr2 / 2;
			var xm = xr2 / Math.Sqrt((xr2 + yr2) * 2) - 1;
			Draw4Pixel(w / 2, h / 2, x, y, canvas);
			while (x < xm)
			{
				if (d > 0)
				{
					y--;
					d = d + yr2 * ((x * 2) + 3) - xr2 * y * 2;
				}
				else
					d = d + yr2 * ((x * 2) + 3);
				x++;
				Draw4Pixel(w / 2, h / 2, x, y, canvas);
			}
			d = (xr2 - yr2 * xRadius) + yr2 / 2;
			x = xRadius;
			y = 0;
			Draw4Pixel(w / 2, h / 2, x, y, canvas);
			xm = xm + 2;
			while (x > xm)
			{
				if (d > 0)
				{
					x--;
					d = d + xr2 * (y * 2 + 3) - yr2 * x * 2;
				}
				else
					d = d + xr2 * (y * 2 + 3);
				y++;
				Draw4Pixel(w / 2, h / 2, x, y, canvas);
			}
		}

		private static void Draw4Pixel(int xc, int yc, int x, int y, Canvas canvas)
		{
			DrawPixel(canvas, xc + x, yc + y);
			DrawPixel(canvas, xc - x, yc + y);
			DrawPixel(canvas, xc + x, yc - y);
			DrawPixel(canvas, xc - x, yc - y);
		}

		static void DrawPixel(Panel canvas, int x, int y)
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
	}
}
