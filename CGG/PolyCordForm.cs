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
		private const int _height = 467;
		private const int _width = 596;

		public PolyCordForm()
		{
			InitializeComponent();
			pictureBox1.Click += ClickHandler; 
		}

		private void ClickHandler(object sender, EventArgs e)
		{
			double x = (e as MouseEventArgs).X;
			double y = (e as MouseEventArgs).Y;
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
			bitmp = new Bitmap(_width, _height);
			DrawPoly(Color.Blue, BluePoly);
			DrawPoly(Color.Green, GreenPoly);
			pictureBox1.Image = bitmp;
		} 

		private void Button1ClickHandler(object sender, EventArgs e)
		{
			_color = !_color;
		}

		private void GoClickHander(object sender, EventArgs ev)
		{
			var redPoly = Poly.Intersection(GreenPoly, BluePoly);
			var g = Graphics.FromImage(bitmp);
			foreach (var e in redPoly.Edges)
				g.DrawLine(new Pen(Color.Red, 3f), new Point((int)e.A.X, (int)e.A.Y), new Point((int)e.B.X, (int)e.B.Y));
			pictureBox1.Image = bitmp;
		}

		private void RefreshClickHandler(object sender, EventArgs e)
		{
			pictureBox1.Image = new Bitmap(_width, _height);
			BluePoly = new Poly();
			GreenPoly = new Poly();
		}
	}
}
