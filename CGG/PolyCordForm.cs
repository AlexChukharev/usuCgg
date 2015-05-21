using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Effects;

namespace CGG
{
	public partial class PolyCordForm : Form
	{
		private bool _color = true;		//if true then blue poligon paint else green poligon
		public Poly BluePoly;
		public Poly GreenPoly;
		public PolyCordForm()
		{
			InitializeComponent();
			pictureBox1.Click += ClickHandler; 
		}

		private void ClickHandler(object sender, EventArgs e)
		{
			double x = MousePosition.X;
			double y = MousePosition.Y;
			if (_color)
				BluePoly.AddVerb(x, y);
			else
				GreenPoly.AddVerb(x, y);
		}

		private void Button1ClickHandler(object sender, EventArgs e)
		{
			_color = !_color;
		}

		private void GoClickHander(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void RefreshClickHandler(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
