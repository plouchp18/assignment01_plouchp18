using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;


namespace FormExample
{
    public partial class Form1 : Form
    {
        public static Form form;
        public static Thread thread;
        public static int s = 100;
        public static int fps = 30;
        public static double running_fps = 30.0;
        public static Font font = new Font("Ubuntu", 12);
        public static int counter = 1;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;
            thread = new Thread(new ThreadStart(run));
            thread.Start();

        }

        public static void run()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / fps);
            while (true)
            {
                DateTime temp = DateTime.Now;
                running_fps = .9 * running_fps + .1 * 1000.0 / (temp - now).TotalMilliseconds;
                Console.WriteLine(running_fps);
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds< frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime-diff).Milliseconds);
                last = DateTime.Now;
                
                s++;
                form.Invoke(new MethodInvoker(form.Refresh));
                
            }
        }

        private void UpdateSize()
        {
            
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            thread.Abort();
        }

        protected override void OnResize(EventArgs e)
        {

            
            Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //Refresh();
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //Console.WriteLine(s);
            int v = (int)(500 + 200 * Math.Sin(DateTime.Now.Millisecond / 50.0));
            Color randColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            SolidBrush brush = new SolidBrush(randColor);
            for (int i = 0; i < counter; i++)
            {

                e.Graphics.FillRectangle(brush, new Rectangle(15, 15, v, v));
            }
            e.Graphics.DrawString(running_fps.ToString(), font, Brushes.CadetBlue, 25, ClientSize.Height - 50);
            e.Graphics.DrawString(counter.ToString(), font, Brushes.IndianRed, 25, ClientSize.Height - 25);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.X)
            {
                counter = counter + 10;
            }
            base.OnKeyDown(e);
            Refresh();
        }
    }
    
}
