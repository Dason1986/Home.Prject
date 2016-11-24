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
	public partial class SheetMainView : Form
    {

        public SheetMainView()
		{
			//InitializeComponent();
		
			//button.Click += (x, y) =>
			//  {
			//	var mvcName = Assembly.GetCallingAssembly().GetName();
			//	  var isMono = Type.GetType("Mono.Runtime") != null;
			//	  MessageBox.Show("fff");
			//  };
		}

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SheetMainView
            // 
            this.ClientSize = new System.Drawing.Size(777, 543);
            this.Name = "SheetMainView";
            this.ResumeLayout(false);

        }
    }
}
