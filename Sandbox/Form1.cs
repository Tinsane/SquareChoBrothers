using System;
using System.Windows.Forms;
using Geometry;
using Timer=System.Timers.Timer;

namespace Sandbox
{
    public sealed partial class Form1 : Form
    {
        protected override void OnPaint (PaintEventArgs e)
        {
            e.Graphics.FillRectangle(System.Drawing.Brushes.Red, 
                new System.Drawing.RectangleF(govno.A, new System.Drawing.SizeF(govno.C - govno.A)));
        }
        private Rectangle govno;
        public Form1 ()
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            govno = new Rectangle(new Point(100, 100), 100, 100);
            var timer = new Timer(30) { AutoReset = true };
            timer.Elapsed += Move1;
            timer.Start();
        }
        private void Move1(object sender, EventArgs eventArgs)
        {
            govno.Transfer(new Vector(10, 0));
            Invalidate();
        }
    }
}