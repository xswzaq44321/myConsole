using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myConsole
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
		}

		public void foo(string msg)
		{
			a = new Label();
			a.Parent = this;
			a.Location = new Point(100, 100);
			a.Text = msg;
			a.Show();
		}

		Label a;
	}
}
