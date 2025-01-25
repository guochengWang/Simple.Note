using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppNote
{
    public partial class AboutNote : Form
    {
        public AboutNote()
        {
            InitializeComponent();
        }

        private void AboutNote_Load(object sender, EventArgs e)
        {
            
             Graphics g = this.CreateGraphics();
            g.DrawLine(Pens.Black, 10, 10, 10, 10); 
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 创建画笔，颜色为灰色，宽度为1
            Pen p = new Pen(Color.Gray, 1);

            // 在画板上画直线，起点坐标为(10, 10)
            g.DrawLine(p, 10, 10, 800, 10);
        }

        private void confirmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
