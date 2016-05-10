using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Geometry;

namespace Paint
{
    class Program
    {
        static Rectangle original;
        static Rectangle processed;
        static Point rotationCenter;
        static double angle = -1;
        static double defaultSizeOfPoint = 1;
        static double invisibleSize = 0.01;

        //TODO: create method "paint rectangle with dotted line"
        //TODO: create method "paint segment with dotted line"

        static void DrawCircle(System.Drawing.Graphics graphics, Circle circle)
        {
            graphics.DrawEllipse(System.Drawing.Pens.Black, (float)(circle.x - circle.r),
                (float)(circle.y - circle.r), (float)(2 * circle.r), (float)(2 * circle.r));
        }

        void DrawPoint(System.Drawing.Graphics graphics, Point point)
        {
            Circle currentCircle = new Circle(point, defaultSizeOfPoint);
            while (currentCircle.r > invisibleSize)
            {
                currentCircle.r -= invisibleSize;
                DrawCircle(graphics, currentCircle);
            }
        }

        void DrawSegment(System.Drawing.Graphics graphics, Segment segment)
        {
            graphics.DrawLine(System.Drawing.Pens.Black, (float)segment.A.x, 
                (float)segment.A.y, (float)segment.B.x, (float)segment.B.y);
        }

        void DrawSegmentWithDottedLine(System.Drawing.Graphics graphics, Segment segment)
        {
            var pen = new System.Drawing.Pen(System.Drawing.Color.Black);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            graphics.DrawLine(pen, (float)segment.A.x, (float)segment.A.y, (float)segment.B.x, 
                (float)segment.B.y);            
        }

        void DrawRectangleWithDottedLine(System.Drawing.Graphics graphics, Rectangle rectangle)
        {
            var segments = rectangle.GetSegments();
            for (int i = 0; i < segments.Length; ++i)
                DrawSegmentWithDottedLine(graphics, segments[i]);
        }

        void DrawRectangle(System.Drawing.Graphics graphics, Rectangle rectangle)
        {
            var segments = rectangle.GetSegments();
            for (int i = 0; i < segments.Length; ++i)
                DrawSegment(graphics, segments[i]);
        }

        void Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            DrawRectangleWithDottedLine(graphics, original);
            DrawPoint(graphics, rotationCenter);
            DrawRectangle(graphics, processed);
        }

        [STAThread]
        static void Main()
        {
            original = new Rectangle(new Point(150, 140), new Point(150, 160), new Point(170, 160));
            rotationCenter = new Point(130, 125);
            processed = original.GetRotated(rotationCenter, angle);
            var form = new Form();
            var program = new Program();
            form.Paint += program.Paint; // Окно form будет вызывать при каждой перерисовке метод program.Paint
            Application.Run(form);
        }
    }
}
