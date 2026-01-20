using ScottPlot;

namespace week12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var plt = formsPlot1.Plot;
            double[] heights = [1, 2, 3, 4, 5, 4, 5, 4, 2];
            var hist = ScottPlot.Statistics.Histogram.WithBinSize(1, heights);
            plt.Add.Bars(hist.Bins, hist.Counts);

            /*
            double [] xs = { 1, 2, 3, 4, 5 };
            double [] ys = { 5, 4, 9, 7, 1 };
            var curve = plt.Add.Scatter(xs, ys);
            curve.LegendText = "數據點";
            curve.MarkerSize = 10;
            curve.Color = Colors.Blue;

            plt.Title("折線圖示例");
            plt.XLabel("x");
            plt.YLabel("y");
            plt.Font.Automatic();
            plt.ShowLegend();*/

            formsPlot1.Refresh();
        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {

        }
    }
}