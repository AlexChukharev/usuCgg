using System;
using System.Drawing;
using System.Windows.Forms;

namespace CGG
{
	public partial class PolyCordForm : Form
	{
		private bool _color = true;		//if true then blue poligon paint else green poligon
		public Poly BluePoly = new Poly();
		public Poly GreenPoly = new Poly();
		public Bitmap bitmp;
		private readonly int _heigth = 467;
		private readonly int _width = 596;

		public PolyCordForm()
		{
			InitializeComponent();
			pictureBox1.Click += ClickHandler; 
		}

		private void ClickHandler(object sender, EventArgs e)
		{
			double x = MousePosition.X - Location.X - pictureBox1.Location.X - 7;
			double y = MousePosition.Y - Location.Y - pictureBox1.Location.Y - 34;
			if (_color)
				BluePoly.AddVerb(x, y);
			else
				GreenPoly.AddVerb(x, y);
			PointAdder();
		}

		public void DrawPoly(Color color, Poly poly)
		{
			var g = Graphics.FromImage(bitmp);
			if (poly.Start == null) return;
			if (poly.Start == poly.End)
			{
				var x = (int)poly.Start.X;
				var y = (int)poly.Start.Y;
				g.DrawEllipse(new Pen(color), x - 1, y - 1, 2, 2);
			}
			else
				foreach (var e in poly.Edges)
					g.DrawLine(new Pen(color, 3f), new Point((int)e.A.X, (int)e.A.Y), new Point((int)e.B.X, (int)e.B.Y));
		}

		public void PointAdder()
		{
			bitmp = new Bitmap(_width, _heigth);
			DrawPoly(Color.Blue, BluePoly);
			DrawPoly(Color.Green, GreenPoly);
			pictureBox1.Image = bitmp;
		} 

		private void Button1ClickHandler(object sender, EventArgs e)
		{
			_color = !_color;
		}

		private void GoClickHander(object sender, EventArgs e)
		{
			//var redPoly = Poly.Intersection(BluePoly, GreenPoly);
			//DrawPoly(Color.DarkRed, redPoly);
		}

		private void RefreshClickHandler(object sender, EventArgs e)
		{
			pictureBox1.Image = new Bitmap(_width, _heigth);
			BluePoly = new Poly();
			GreenPoly = new Poly();
		}
	}
}
