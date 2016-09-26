using System;
namespace Home.AppGtk
{
	public partial class LoginView : Gtk.Window
	{
		public LoginView() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}
	}
}
