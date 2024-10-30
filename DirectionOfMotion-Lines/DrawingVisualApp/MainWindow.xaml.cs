using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace DrawingVisualApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer;
        public static Random rnd = new Random();
        public static double width, height;

        DrawingVisual visual;
        DrawingContext dc;
        List<CarSinCos> carsSinCos = new List<CarSinCos>();
        List<CarMatrix> carsMatrix = new List<CarMatrix>();
        Vector2D mouse = new Vector2D();


        public MainWindow()
        {
            InitializeComponent();

            visual = new DrawingVisual();

            width = g.Width;
            height = g.Height;

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);

            for (int i = 0; i < 10; ++i)
            {
                carsSinCos.Add(new CarSinCos(Brushes.LimeGreen));
                carsMatrix.Add(new CarMatrix(Brushes.Red));
            }

            timer.Start();
        }

        private void timerTick(object sender, EventArgs e) => Drawing();


        private void Drawing()
        {
            g.RemoveVisual(visual);

            using (dc = visual.RenderOpen())
            {
                foreach (var car in carsMatrix)
                {
                    car.Update(mouse);
                    car.Draw(dc);
                }
                foreach (var car in carsSinCos)
                {
                    car.Update(mouse);
                    car.Draw(dc);
                }
                DrawText(dc);

                dc.Close();
                g.AddVisual(visual);
            }
        }

        private void g_MouseUp(object sender, MouseButtonEventArgs e)
        {
            foreach(var car in carsMatrix)
            { 
                car.GoAway(); 
            }
            foreach(var car in carsSinCos)
            {
                car.GoAway();
            }
        }

        private void g_MouseMove(object sender, MouseEventArgs e)
        {
            mouse.X = e.GetPosition(g).X;
            mouse.Y = e.GetPosition(g).Y;
        }

        private void DrawText(DrawingContext dc)
        {
            // SinCos text
            FormattedText formattedText = new FormattedText("Sin / Cos calculation", CultureInfo.GetCultureInfo("en-us"),
                                    FlowDirection.LeftToRight, new Typeface("Verdana"), 12, Brushes.LimeGreen,
                                    VisualTreeHelper.GetDpi(visual).PixelsPerDip);
            Point textPos = new Point(20, 20);
            dc.DrawText(formattedText, textPos);

            // Matrix text
            formattedText = new FormattedText("Matrix calculation", CultureInfo.GetCultureInfo("en-us"),
                                    FlowDirection.LeftToRight, new Typeface("Verdana"), 12, Brushes.Red,
                                    VisualTreeHelper.GetDpi(visual).PixelsPerDip);
            textPos = new Point(20, 40);
            dc.DrawText(formattedText, textPos);
            // Click button text
            formattedText = new FormattedText("Click any mouse button to change direction", CultureInfo.GetCultureInfo("en-us"),
                                    FlowDirection.LeftToRight, new Typeface("Verdana"), 12, Brushes.Gray,
                                    VisualTreeHelper.GetDpi(visual).PixelsPerDip);
            textPos = new Point(20, 60);
            dc.DrawText(formattedText, textPos);
        }
    }
}
