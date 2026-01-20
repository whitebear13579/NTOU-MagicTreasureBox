using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace week13
{
    public partial class Form1 : Form
    {
        // Added fields used across methods
        private VideoCapture capture;
        private Mat currentFrame;
        private int currentFrameNo = 0;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken cancellationToken;
        private delegate void delSBarChange(int pos);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Bitmap bitmap = new Bitmap(openFileDialog1.FileName);
                //pictureBox1.Image = bitmap;
                //Mat currentFrame = BitmapConverter.ToMat(bitmap);

                //Mat img = Cv2.ImRead(openFileDialog1.FileName);

                // 灰階圖像
                Mat grayImg = new Mat();
                //Cv2.CvtColor(img, grayImg, ColorConversionCodes.BGR2GRAY);
                //Cv2.ImShow("灰階圖像", grayImg);

                // 模糊圖像
                Mat blurImg = new Mat();
                //Cv2.GaussianBlur(img, blurImg, new OpenCvSharp.Size(5,5), 0);
                //Cv2.ImShow("模糊圖像", blurImg);

                // 邊緣檢測
                /*
                Mat CannyImg = new Mat();
                Cv2.Canny(blurImg, CannyImg, 150, 200);
                Cv2.ImShow("圖像邊緣檢測", CannyImg);
                */

                // 膨脹與侵蝕
                Mat dialationImg = new Mat();
                Mat kernel = new Mat(5, 5, MatType.CV_8UC1);
                //Cv2.Dilate(blurImg, dialationImg, kernel);
                //Cv2.ImShow("圖像膨脹", dialationImg);

                Mat erodeImg = new Mat();
                //Cv2.Erode(dialationImg, erodeImg, kernel);
                //Cv2.ImShow("圖像侵蝕", erodeImg);

                // 二值化圖像
                Mat binaryImg = new Mat();
                //Cv2.Threshold(grayImg, binaryImg, 50, 77, ThresholdTypes.Binary);
                //Cv2.ImShow("二值化圖像", binaryImg);

                // OpenCV 繪圖
                /*
                Mat img = new Mat(512, 512, MatType.CV_8UC3, new Scalar(0, 0, 0)); // 高度512，寬度512，顔色爲黑色
                int height = img.Height;
                int width = img.Width;
                int channels = img.Channels();
                Console.WriteLine("height: {0}, width: {1}, channels: {2}", height, width, channels);
                // 在圖像左上角原點(0,0)到右下角畫一條綠色的直綫，線條寬度爲3
                Cv2.Line(img, new OpenCvSharp.Point(0, 0), new OpenCvSharp.Point(height, width), new Scalar(0, 255, 0), 3);
                // 在左上角頂點(0,0)和右下角(250,350)處繪製一個紅色矩形，邊界線條寬度爲2
                Cv2.Rectangle(img, new OpenCvSharp.Point(0, 0), new OpenCvSharp.Point(250, 350), new Scalar(0, 0, 255), 2);
                // 以(400,50)爲中心，繪製半徑爲30的圓，顔色爲青色（綠＋藍＝青（Cyan)）
                Cv2.Circle(img, new OpenCvSharp.Point(400, 50), 30, new Scalar(255, 255, 0), 2);
                // 在(350,300)處繪製文字，字體爲FONT_HERSHEY_COMPLEX，比例爲1，顔色爲黃色，條線條寬爲2
                Cv2.PutText(img, "OpenCV", new OpenCvSharp.Point(350, 300), HersheyFonts.HersheyComplex, 1, new Scalar(0, 255, 255), 2);
                Cv2.ImShow("Image", img);
                */

                // 影片顯示
                capture = new VideoCapture(openFileDialog1.FileName);
                int TotalFrames = Convert.ToInt32(capture.FrameCount);
                int FPS = Convert.ToInt32(capture.Fps);
                currentFrame = new Mat();
                currentFrameNo = 0;
                hScrollBar1.Minimum = 0;
                hScrollBar1.Maximum = TotalFrames - 1;
                hScrollBar1.Value = 0;
                hScrollBar1.Enabled = true;
                capture.PosFrames = currentFrameNo;
                capture.Read(currentFrame);
                pictureBox1.Image = currentFrame.ToBitmap();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            // update frame based on scrollbar value
            if (capture != null && capture.IsOpened())
            {
                currentFrameNo = (int)hScrollBar1.Value;
                capture.PosFrames = currentFrameNo;
                if (capture.Grab())
                {
                    currentFrame = capture.RetrieveMat();
                    pictureBox1.Image = currentFrame.ToBitmap(); //將當前幀顯示在picturebox1上
                }
            }
        }

        public void mthdelSBarChange(int pos)
        {
            if (this.InvokeRequired) this.Invoke(new delSBarChange(mthdelSBarChange), pos);
            else this.hScrollBar1.Value = pos;
        }
        private async Task ViedoStreamAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested) // Run until cancellation is requested
            {
                capture.PosFrames = currentFrameNo;
                //if (capture.Read(currentFrame)) // 抓取並解碼，返回下一帧
                if (capture.Grab())
                {
                    currentFrame = capture.RetrieveMat();
                    pictureBox1.Image = currentFrame.ToBitmap(); //將當前幀顯示在picturebox1上

                }
                currentFrameNo = capture.PosFrames;
                mthdelSBarChange(currentFrameNo);
                //hScrollBar1.Value = currentFrameNo;
            }
        }

        private void webcam_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel(); // Request cancellation of the webcam feed
            }

            // dispose previous capture if exists
            if (capture != null)
            {
                capture.Release();
                capture.Dispose();
                capture = null;
            }

            capture = new VideoCapture(0);
            capture.AutoFocus = true; // Auto Focus
            capture.Brightness = 70; // Brightness
            if (!capture.IsOpened())
            {
                MessageBox.Show("VideoCapture open failed");
                return;
            }
            //Mat currentFrame = new Mat();
            cancellationTokenSource = new CancellationTokenSource(); // Create a cancellation token source
            cancellationToken = cancellationTokenSource.Token; // Get the cancellation token
            Task.Run(() => ViedoStreamAsync(cancellationToken), cancellationToken); // Start webcam async task
        }
    }
}