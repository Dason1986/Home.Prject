using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Home.WinForm.Views.Gallery
{
    public partial class GallerySlideView : Form
    {
        public GallerySlideView()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Location.X < 50)
            {
                BreakImage();
            }
            else if (pictureBox1.Width - e.Location.X < 50)
            {

                NextImage();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            NextImage();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            BreakImage();

        }
        private void BreakImage()
        {
            if (currentindex <= 0) return;

            currentindex--;
            SetImage();
        }
        private void NextImage()
        {
            if (currentindex > listView1.Items.Count) return;
            currentindex++;
            SetImage();
        }

        int currentindex = -1;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0) return;
            currentindex = listView1.SelectedIndices[0];
            SetImage();

        }

        private void SetImage()
        {
            var item = listView1.Items[currentindex];
            item.Selected = true;
            var imageindex = item.ImageIndex;
            Image image = null;
            if (imageindex != -1)
            {
                image = imageList1.Images[imageindex];


            }
            var key = item.ImageKey;
            if (image == null && !string.IsNullOrEmpty(key))
            {
                image = imageList1.Images[key];

            }
            pictureBox1.Image = image;
            if (image == null)  return;
            pictureBox1.SizeMode = (image.Width > pictureBox1.Width || image.Height > pictureBox1.Height) ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.CenterImage;
        }
    }
}
