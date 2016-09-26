using System;
using Gtk;

namespace Home.AppGtk
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			var win = new LoginView ();
			win.Show ();
			Application.Run ();
		}
	}
}
