using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace KMeans
{
    public partial class MainForm : Form
    {
        private Bitmap selected;
        public MainForm()
        {
            InitializeComponent();
            this.numericUpDown1.Minimum = 1;
            this.numericUpDown1.Value = 2;
            this.numericUpDown1.Maximum = 1024;
            this.numericUpDown2.Minimum = 1;
            this.numericUpDown2.Maximum = 10;
            this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void loadImg_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
                selected = (Bitmap)pictureBox1.Image.Clone();
                selected = ConvertToNonIndexed(selected, PixelFormat.Format24bppRgb);
            }
        }
        private Bitmap ConvertToNonIndexed(Image img, PixelFormat fmt)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height, fmt);
            Graphics gr = Graphics.FromImage(bmp);
            gr.DrawImage(img, 0, 0);
            gr.Dispose();
            return bmp;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void runKMeans_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 1;
            progressBar1.Refresh();
           KMeans kmeans = new KMeans(selected, (int)this.numericUpDown1.Value);
            progressBar1.Value = 2;
            kmeans.Run((int)this.numericUpDown2.Value);
            progressBar1.Value = 3;
            
            pictureBox2.Image = (Bitmap)kmeans.output.Clone();
            pictureBox2.Refresh();
            progressBar1.Value = 4;
        }
    }
}
