using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeApp
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		
			button.Click += (x, y) =>
			  {
				var mvcName = Assembly.GetCallingAssembly().GetName();
				  var isMono = Type.GetType("Mono.Runtime") != null;
				  MessageBox.Show("fff");
			  };
		}
	}
}
