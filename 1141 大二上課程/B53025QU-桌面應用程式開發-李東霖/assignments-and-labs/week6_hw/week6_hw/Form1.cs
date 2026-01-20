using System.Drawing.Drawing2D;

namespace week6_hw
{
    public partial class Form1 : Form
    {
        // 繪圖相關變數
        private Bitmap canvas;
        private Graphics canvasGraphics;
        private bool isDrawing = false;
        private Point startPoint;
        private Point endPoint;

        // 繪圖設定
        private Color currentColor = Color.Black;
        private float currentBrushSize = 2f;
        private DashStyle currentDashStyle = DashStyle.Solid;

        // 繪圖工具類型
        private enum DrawingTool
        {
            FreeDraw,
            Line,
            SingleArrow,
            DoubleArrow,
            Rectangle,
            Oval,
            Triangle,
            Star,
            Text,
            Eraser,
            Move
        }
        private DrawingTool currentTool = DrawingTool.FreeDraw; // 預設為自由曲線

        // Undo/Redo 功能
        private Stack<Bitmap> undoStack = new Stack<Bitmap>();
        private Stack<Bitmap> redoStack = new Stack<Bitmap>();

        // 追蹤是否合法修改
        private bool hasUnsavedChanges = false;

        // 雙緩衝區
        private BufferedGraphicsContext context;
        private BufferedGraphics buffer;

        // Timer for double buffering
        private System.Windows.Forms.Timer renderTimer;

        // 暫存預覽圖形
        private Bitmap previewBitmap;

        // 自由曲線繪製
        private List<Point> freeDrawPoints = new List<Point>();

        // 繪製物件管理
        private List<DrawingObject> drawingObjects = new List<DrawingObject>();
        private DrawingObject selectedObject = null;
        private Point moveStartPoint;
        private Point objectOffset;

        // Shift 鍵狀態
        private bool isShiftPressed = false;
        private ToolStripLabel shiftLabel;
        private ToolStripLabel lockTips;
        private System.Windows.Forms.Timer tipsTimer;

        // 文字輸入
        private TextBox drawingTextBox;
        private bool isTextInputActive = false;

        // 自定義橡皮擦游標
        private Cursor eraserCursor;

        public Form1()
        {
            InitializeComponent();
            InitializeShiftLabel();
            InitializeLockTipsLabel();
            InitializeCanvas();
            InitializeDoubleBuffer();
            InitializeEventHandlers();
            InitializeTipsTimer();
            UpdateToolButtonStates();
            UpdateUndoRedoButtons();
            UpdateNewCanvasMenuItem();

            // 初始化自定義橡皮擦游標
            CreateEraserCursor();
        }

        private void InitializeLockTipsLabel()
        {
            lockTips = new ToolStripLabel
            {
                Text = "按下 shift 鍵來鎖定形狀比例",
                ForeColor = Color.Black,
                Font = new Font("Microsoft JhengHei", 12),
                Visible = false,
                Padding = new Padding(5, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleCenter,
            };
            toolStrip1.Items.Add(lockTips);
        }

        private void InitializeTipsTimer()
        {
            tipsTimer = new System.Windows.Forms.Timer();
            tipsTimer.Interval = 1500; // 3 seconds
            tipsTimer.Tick += TipsTimer_Tick;
        }

        private void TipsTimer_Tick(object sender, EventArgs e)
        {
            lockTips.Visible = false;
            tipsTimer.Stop();
        }

        private void InitializeShiftLabel()
        {
            shiftLabel = new ToolStripLabel
            {
                Text = "鬆開 shift 鍵來取消比例鎖定",
                ForeColor = Color.Black,
                Font = new Font("Microsoft JhengHei", 12, FontStyle.Bold),
                Visible = false,
                Padding = new Padding(5, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleCenter,
            };
            toolStrip1.Items.Add(shiftLabel);
        }

        private void InitializeCanvas()
        {
            // 設置畫布
            canvas = new Bitmap(flowLayoutPanel1.Width, flowLayoutPanel1.Height);
            canvasGraphics = Graphics.FromImage(canvas);
            canvasGraphics.Clear(Color.White);
            canvasGraphics.SmoothingMode = SmoothingMode.AntiAlias;

            // 設置 FlowLayoutPanel 為繪圖區域
            flowLayoutPanel1.Paint += FlowLayoutPanel1_Paint;
            flowLayoutPanel1.MouseDown += FlowLayoutPanel1_MouseDown;
            flowLayoutPanel1.MouseMove += FlowLayoutPanel1_MouseMove;
            flowLayoutPanel1.MouseUp += FlowLayoutPanel1_MouseUp;
            flowLayoutPanel1.Resize += FlowLayoutPanel1_Resize;
        }

        private void InitializeDoubleBuffer()
        {
            // 啟用雙緩衝
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            // 初始化雙緩衝區
            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
            buffer = context.Allocate(flowLayoutPanel1.CreateGraphics(), flowLayoutPanel1.DisplayRectangle);

            // 初始化 Timer
            renderTimer = new System.Windows.Forms.Timer();
            renderTimer.Interval = 16; // ~60 FPS
            renderTimer.Tick += RenderTimer_Tick;
            renderTimer.Start();
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // 使用雙緩衝區繪製
                if (buffer != null && canvas != null)
                {
                    buffer.Graphics.Clear(flowLayoutPanel1.BackColor);
                    buffer.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    // 繪製主畫布
                    buffer.Graphics.DrawImage(canvas, 0, 0);

                    // 繪製預覽
                    if (isDrawing && previewBitmap != null)
                    {
                        buffer.Graphics.DrawImage(previewBitmap, 0, 0);
                    }

                    // 繪製物件選取框
                    if (currentTool == DrawingTool.Move && selectedObject != null)
                    {
                        using (Pen selectPen = new Pen(Color.Blue, 2))
                        {
                            selectPen.DashStyle = DashStyle.Dash;
                            buffer.Graphics.DrawRectangle(selectPen, selectedObject.Bounds);
                        }
                    }

                    // 渲染到畫面
                    Graphics g = flowLayoutPanel1.CreateGraphics();
                    buffer.Render(g);
                    g.Dispose();
                }
            }
            catch { }
        }

        private void InitializeEventHandlers()
        {
            // 檔案選單
            newCanva.Click += NewCanva_Click;
            loadImg.Click += LoadImg_Click;
            saveFile.Click += SaveFile_Click;
            exitProgram.Click += ExitProgram_Click;

            // 繪圖工具
            freeDrawButton.Click += (s, e) => SelectTool(DrawingTool.FreeDraw, freeDrawButton);
            lineDraw.Click += (s, e) => SelectTool(DrawingTool.Line, lineDraw);
            singleArrowDraw.Click += (s, e) => SelectTool(DrawingTool.SingleArrow, singleArrowDraw);
            doubleArrowDraw.Click += (s, e) => SelectTool(DrawingTool.DoubleArrow, doubleArrowDraw);
            rectDraw.Click += (s, e) => SelectTool(DrawingTool.Rectangle, rectDraw);
            ovalDraw.Click += (s, e) => SelectTool(DrawingTool.Oval, ovalDraw);
            triangleDraw.Click += (s, e) => SelectTool(DrawingTool.Triangle, triangleDraw);
            starDraw.Click += (s, e) => SelectTool(DrawingTool.Star, starDraw);
            textDraw.Click += (s, e) => SelectTool(DrawingTool.Text, textDraw);
            eraserButton.Click += (s, e) => SelectTool(DrawingTool.Eraser, eraserButton);
            moveButton.Click += (s, e) => SelectTool(DrawingTool.Move, moveButton);

            // 工具設定
            colorPalette.Click += ColorPalette_Click;
            brushSize.Click += BrushSize_Click;
            
            solidLineMenuItem.Click += (s, e) => SetDashStyle(DashStyle.Solid);
            dashLineMenuItem.Click += (s, e) => SetDashStyle(DashStyle.Dash);
            dotLineMenuItem.Click += (s, e) => SetDashStyle(DashStyle.Dot);
            dashDotLineMenuItem.Click += (s, e) => SetDashStyle(DashStyle.DashDot);
            dashDotDotLineMenuItem.Click += (s, e) => SetDashStyle(DashStyle.DashDotDot);

            // Undo/Redo
            undoButton.Click += UndoButton_Click;
            redoButton.Click += RedoButton_Click;

            // 表單關閉事件
            this.FormClosing += Form1_FormClosing;

            // 鍵盤事件
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            this.KeyPreview = true;

            // 視窗大小改變事件
            this.Resize += Form1_Resize;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // 重新分配雙緩衝區
            if (flowLayoutPanel1.Width > 0 && flowLayoutPanel1.Height > 0)
            {
                buffer?.Dispose();
                context = BufferedGraphicsManager.Current;
                context.MaximumBuffer = new Size(flowLayoutPanel1.Width + 1, flowLayoutPanel1.Height + 1);
                buffer = context.Allocate(flowLayoutPanel1.CreateGraphics(), flowLayoutPanel1.DisplayRectangle);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey && !isShiftPressed)
            {
                isShiftPressed = true;
                if (currentTool == DrawingTool.Rectangle || currentTool == DrawingTool.Oval ||
                    currentTool == DrawingTool.Triangle || currentTool == DrawingTool.Star)
                {
                    shiftLabel.Visible = true;
                    lockTips.Visible = false;
                    tipsTimer.Stop();
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                isShiftPressed = false;
                shiftLabel.Visible = false;
            }
        }

        private void SelectTool(DrawingTool tool, ToolStripButton button)
        {
            currentTool = tool;
            UpdateToolButtonStates();

            // 隱藏文字輸入框
            if (drawingTextBox != null)
            {
                FinalizeTextInput();
            }

            // 更新滑鼠游標
            UpdateCursor();

            // 如果切換到非正比例工具，隱藏提示
            if (tool != DrawingTool.Rectangle && tool != DrawingTool.Oval &&
                tool != DrawingTool.Triangle && tool != DrawingTool.Star)
            {
                shiftLabel.Visible = false;
                lockTips.Visible = false;
                tipsTimer.Stop();
            }
            else
            {
                if (isShiftPressed)
                {
                    shiftLabel.Visible = true;
                }
                else
                {
                    lockTips.Visible = true;
                    tipsTimer.Start();
                }
            }
        }

        private void UpdateCursor()
        {
            switch (currentTool)
            {
                case DrawingTool.Eraser:
                    // 使用自定義橡皮擦游標
                    flowLayoutPanel1.Cursor = eraserCursor ?? Cursors.Hand;
                    break;
                case DrawingTool.Move:
                    flowLayoutPanel1.Cursor = Cursors.SizeAll;
                    break;
                case DrawingTool.Text:
                    flowLayoutPanel1.Cursor = Cursors.IBeam;
                    break;
                default:
                    flowLayoutPanel1.Cursor = Cursors.Cross;
                    break;
            }
        }

        private void UpdateToolButtonStates()
        {
            // 重設所有工具按鈕
            Color defaultColor = Color.Transparent;
            Color selectedColor = Color.LightSkyBlue;

            freeDrawButton.Checked = false;
            freeDrawButton.BackColor = defaultColor;
            lineDraw.Checked = false;
            lineDraw.BackColor = defaultColor;
            singleArrowDraw.Checked = false;
            singleArrowDraw.BackColor = defaultColor;
            doubleArrowDraw.Checked = false;
            doubleArrowDraw.BackColor = defaultColor;
            rectDraw.Checked = false;
            rectDraw.BackColor = defaultColor;
            ovalDraw.Checked = false;
            ovalDraw.BackColor = defaultColor;
            triangleDraw.Checked = false;
            triangleDraw.BackColor = defaultColor;
            starDraw.Checked = false;
            starDraw.BackColor = defaultColor;
            textDraw.Checked = false;
            textDraw.BackColor = defaultColor;
            eraserButton.Checked = false;
            eraserButton.BackColor = defaultColor;
            moveButton.Checked = false;
            moveButton.BackColor = defaultColor;

            // 設定當前選中的工具
            switch (currentTool)
            {
                case DrawingTool.FreeDraw:
                    freeDrawButton.Checked = true;
                    freeDrawButton.BackColor = selectedColor;
                    break;
                case DrawingTool.Line:
                    lineDraw.Checked = true;
                    lineDraw.BackColor = selectedColor;
                    break;
                case DrawingTool.SingleArrow:
                    singleArrowDraw.Checked = true;
                    singleArrowDraw.BackColor = selectedColor;
                    break;
                case DrawingTool.DoubleArrow:
                    doubleArrowDraw.Checked = true;
                    doubleArrowDraw.BackColor = selectedColor;
                    break;
                case DrawingTool.Rectangle:
                    rectDraw.Checked = true;
                    rectDraw.BackColor = selectedColor;
                    break;
                case DrawingTool.Oval:
                    ovalDraw.Checked = true;
                    ovalDraw.BackColor = selectedColor;
                    break;
                case DrawingTool.Triangle:
                    triangleDraw.Checked = true;
                    triangleDraw.BackColor = selectedColor;
                    break;
                case DrawingTool.Star:
                    starDraw.Checked = true;
                    starDraw.BackColor = selectedColor;
                    break;
                case DrawingTool.Text:
                    textDraw.Checked = true;
                    textDraw.BackColor = selectedColor;
                    break;
                case DrawingTool.Eraser:
                    eraserButton.Checked = true;
                    eraserButton.BackColor = selectedColor;
                    break;
                case DrawingTool.Move:
                    moveButton.Checked = true;
                    moveButton.BackColor = selectedColor;
                    break;
            }
        }

        private void FlowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                startPoint = e.Location;

                if (currentTool == DrawingTool.FreeDraw)
                {
                    SaveToUndoStack();
                    freeDrawPoints.Clear();
                    freeDrawPoints.Add(startPoint);
                }
                else if (currentTool == DrawingTool.Eraser)
                {
                    SaveToUndoStack();
                    EraseObject(e.Location);
                }
                else if (currentTool == DrawingTool.Move)
                {
                    // 托?模式 - 檢查是否點擊到物件
                    selectedObject = FindObjectAtPoint(e.Location);
                    if (selectedObject != null)
                    {
                        SaveToUndoStack();
                        moveStartPoint = e.Location;
                        objectOffset = new Point(e.Location.X - selectedObject.Bounds.X, e.Location.Y - selectedObject.Bounds.Y);
                    }
                }
                else if (currentTool == DrawingTool.Text)
                {
                    StartTextInput(e.Location);
                }
                else
                {
                    // 儲存當前狀態到 undo stack
                    SaveToUndoStack();
                }
            }
        }

        private void FlowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                endPoint = e.Location;

                if (currentTool == DrawingTool.FreeDraw)
                {
                    freeDrawPoints.Add(endPoint);

                    // 即時繪製到畫布
                    using (Pen pen = new Pen(currentColor, currentBrushSize))
                    {
                        pen.DashStyle = currentDashStyle;
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;
                        pen.LineJoin = LineJoin.Round;

                        if (freeDrawPoints.Count >= 2)
                        {
                            // 清除舊的預覽線條 (透過重繪物件)
                            RedrawCanvas();
                            // 繪製新的預覽線條
                            canvasGraphics.DrawLines(pen, freeDrawPoints.ToArray());
                        }
                    }
                }
                else if (currentTool == DrawingTool.Eraser)
                {
                    EraseObject(e.Location);
                }
                else if (currentTool == DrawingTool.Move && selectedObject != null)
                {
                    // 托?模式 - 移動選中的物件
                    int newX = e.Location.X - objectOffset.X;
                    int newY = e.Location.Y - objectOffset.Y;

                    selectedObject.Bounds = new Rectangle(newX, newY, selectedObject.Bounds.Width, selectedObject.Bounds.Height);

                    // 重繪畫布
                    RedrawCanvas();
                }
                else if (currentTool != DrawingTool.Text)
                {
                    // 創建預覽圖
                    if (previewBitmap != null)
                    {
                        previewBitmap.Dispose();
                    }
                    previewBitmap = new Bitmap(flowLayoutPanel1.Width, flowLayoutPanel1.Height);
                    using (Graphics g = Graphics.FromImage(previewBitmap))
                    {
                        g.Clear(Color.Transparent);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        DrawShape(g, startPoint, endPoint, true);
                    }
                }
            }
        }

        private void FlowLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                isDrawing = false;
                endPoint = e.Location;

                if (currentTool == DrawingTool.FreeDraw)
                {
                    hasUnsavedChanges = true;
                    // 創建自由曲線的獨立圖層
                    Rectangle bounds = GetBoundingBox(freeDrawPoints);
                    Bitmap freeDrawImage = new Bitmap(Math.Max(1, bounds.Width), Math.Max(1, bounds.Height));
                    using (Graphics g = Graphics.FromImage(freeDrawImage))
                    {
                        g.Clear(Color.Transparent);
                        g.SmoothingMode = SmoothingMode.AntiAlias;

                        using (Pen pen = new Pen(currentColor, currentBrushSize))
                        {
                            pen.DashStyle = currentDashStyle;
                            pen.StartCap = LineCap.Round;
                            pen.EndCap = LineCap.Round;
                            pen.LineJoin = LineJoin.Round;

                            // 調整點的座標到相對於邊界框
                            List<Point> adjustedPoints = new List<Point>();
                            foreach (Point p in freeDrawPoints)
                            {
                                adjustedPoints.Add(new Point(p.X - bounds.X, p.Y - bounds.Y));
                            }

                            if (adjustedPoints.Count >= 2)
                            {
                                g.DrawLines(pen, adjustedPoints.ToArray());
                            }
                        }
                    }
                    AddObjectToList(bounds, freeDrawImage, DrawingObjectType.FreeDraw);
                    RedrawCanvas();
                }
                else if (currentTool == DrawingTool.Eraser)
                {
                    hasUnsavedChanges = true;
                }
                else if (currentTool == DrawingTool.Move)
                {
                    if (selectedObject != null)
                    {
                        hasUnsavedChanges = true;
                        // 移動完成後重繪畫布
                        RedrawCanvas();
                    }
                    selectedObject = null;
                }
                else if (currentTool != DrawingTool.Text)
                {
                    // 創建形狀的獨立圖層
                    Rectangle bounds = GetShapeBounds(startPoint, endPoint);
                    if (bounds.Width > 0 && bounds.Height > 0)
                    {
                        Bitmap shapeImage = new Bitmap(bounds.Width, bounds.Height);
                        using (Graphics g = Graphics.FromImage(shapeImage))
                        {
                            g.Clear(Color.Transparent);
                            g.SmoothingMode = SmoothingMode.AntiAlias;


                            // 調整繪製座標到相對於邊界框
                            Point adjustedStart = new Point(startPoint.X - bounds.X, startPoint.Y - bounds.Y);
                            Point adjustedEnd = new Point(endPoint.X - bounds.X, endPoint.Y - bounds.Y);
                            DrawShape(g, adjustedStart, adjustedEnd, false);
                        }

                        DrawingObjectType objectType = GetObjectTypeFromTool(currentTool);
                        AddObjectToList(bounds, shapeImage, objectType);
                        hasUnsavedChanges = true;
                        RedrawCanvas();
                    }
                }

                if (previewBitmap != null)
                {
                    previewBitmap.Dispose();
                    previewBitmap = null;
                }

                UpdateUndoRedoButtons();
                UpdateNewCanvasMenuItem();
            }
        }

        private DrawingObjectType GetObjectTypeFromTool(DrawingTool tool)
        {
            switch (tool)
            {
                case DrawingTool.Line: return DrawingObjectType.Line;
                case DrawingTool.SingleArrow: return DrawingObjectType.SingleArrow;
                case DrawingTool.DoubleArrow: return DrawingObjectType.DoubleArrow;
                case DrawingTool.Rectangle: return DrawingObjectType.Rectangle;
                case DrawingTool.Oval: return DrawingObjectType.Oval;
                case DrawingTool.Triangle: return DrawingObjectType.Triangle;
                case DrawingTool.Star: return DrawingObjectType.Star;
                default: return DrawingObjectType.Other;
            }
        }

        private void FlowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Paint 事件由雙緩衝區處理
        }

        private void FlowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Width > 0 && flowLayoutPanel1.Height > 0)
            {
                // 調整畫布大小
                Bitmap newCanvas = new Bitmap(flowLayoutPanel1.Width, flowLayoutPanel1.Height);
                using (Graphics g = Graphics.FromImage(newCanvas))
                {
                    g.Clear(Color.White);
                    if (canvas != null)
                    {
                        g.DrawImage(canvas, 0, 0);
                    }
                }

                canvas?.Dispose();
                canvasGraphics?.Dispose();
                canvas = newCanvas;
                canvasGraphics = Graphics.FromImage(canvas);
                canvasGraphics.SmoothingMode = SmoothingMode.AntiAlias;

                // 重新初始化雙緩衝區
                buffer?.Dispose();
                context = BufferedGraphicsManager.Current;
                context.MaximumBuffer = new Size(flowLayoutPanel1.Width + 1, flowLayoutPanel1.Height + 1);
                buffer = context.Allocate(flowLayoutPanel1.CreateGraphics(), flowLayoutPanel1.DisplayRectangle);
            }
        }

        private void ColorPalette_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = currentColor;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    currentColor = colorDialog.Color;
                }
            }
        }

        private void BrushSize_Click(object sender, EventArgs e)
        {
            using (var sizeForm = new Form())
            {
                sizeForm.Text = "調整筆刷大小";
                sizeForm.Size = new Size(350, 220);
                sizeForm.StartPosition = FormStartPosition.CenterParent;
                sizeForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                sizeForm.MaximizeBox = false;
                sizeForm.MinimizeBox = false;

                var label = new Label
                {
                    Text = "輸入或使用拉桿調整大小",
                    AutoSize = true
                };
                sizeForm.Controls.Add(label);
                label.Location = new Point((sizeForm.ClientSize.Width - label.Width) / 2, 10);

                var numericUpDown = new NumericUpDown
                {
                    Size = new Size(100, 25),
                    Minimum = 1,
                    Maximum = 20,
                    Value = (decimal)currentBrushSize
                };
                numericUpDown.Location = new Point((sizeForm.ClientSize.Width - numericUpDown.Width) / 2, 35);

                var trackBar = new TrackBar
                {
                    Location = new Point(10, 70),
                    Size = new Size(310, 45),
                    Minimum = 1,
                    Maximum = 20,
                    Value = (int)currentBrushSize,
                    TickFrequency = 1
                };

                // 同步 NumericUpDown 和 TrackBar
                numericUpDown.ValueChanged += (s, evt) =>
                {
                    trackBar.Value = (int)numericUpDown.Value;
                };

                trackBar.ValueChanged += (s, evt) =>
                {
                    numericUpDown.Value = trackBar.Value;
                };

                var okButton = new Button
                {
                    Text = "確定",
                    DialogResult = DialogResult.OK,
                    Location = new Point(130, 130),
                    Size = new Size(80, 30)
                };

                sizeForm.Controls.Add(numericUpDown);
                sizeForm.Controls.Add(trackBar);
                sizeForm.Controls.Add(okButton);
                sizeForm.AcceptButton = okButton;

                if (sizeForm.ShowDialog() == DialogResult.OK)
                {
                    currentBrushSize = (float)numericUpDown.Value;
                }
            }
        }

        private void SetDashStyle(DashStyle style)
        {
            currentDashStyle = style;
            UpdateBrushStyleSelection();
        }

        private void UpdateBrushStyleSelection()
        {
            solidLineMenuItem.Checked = currentDashStyle == DashStyle.Solid;
            dashLineMenuItem.Checked = currentDashStyle == DashStyle.Dash;
            dotLineMenuItem.Checked = currentDashStyle == DashStyle.Dot;
            dashDotLineMenuItem.Checked = currentDashStyle == DashStyle.DashDot;
            dashDotDotLineMenuItem.Checked = currentDashStyle == DashStyle.DashDotDot;
        }

        private void SaveToUndoStack()
        {
            if (canvas != null)
            {
                undoStack.Push((Bitmap)canvas.Clone());
                redoStack.Clear();
                UpdateUndoRedoButtons();
            }
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 0)
            {
                redoStack.Push((Bitmap)canvas.Clone());
                canvas.Dispose();
                canvasGraphics.Dispose();

                canvas = undoStack.Pop();
                canvasGraphics = Graphics.FromImage(canvas);
                canvasGraphics.SmoothingMode = SmoothingMode.AntiAlias;

                UpdateUndoRedoButtons();
            }
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push((Bitmap)canvas.Clone());
                canvas.Dispose();
                canvasGraphics.Dispose();

                canvas = redoStack.Pop();
                canvasGraphics = Graphics.FromImage(canvas);
                canvasGraphics.SmoothingMode = SmoothingMode.AntiAlias;

                UpdateUndoRedoButtons();
            }
        }

        private void UpdateUndoRedoButtons()
        {
            undoButton.Enabled = undoStack.Count > 0;
            redoButton.Enabled = redoStack.Count > 0;
        }

        private void NewCanva_Click(object sender, EventArgs e)
        {
            if (hasUnsavedChanges)
            {
                var result = MessageBox.Show(
                    "確定離開嗎？畫布上所有未轉存的物件都會遺失。",
                    "バイバイ？",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            // 清空畫布
            canvasGraphics.Clear(Color.White);
            drawingObjects.Clear();
            undoStack.Clear();
            redoStack.Clear();
            hasUnsavedChanges = false;

            UpdateUndoRedoButtons();
            UpdateNewCanvasMenuItem();
        }

        private void LoadImg_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "圖片檔案|*.jpg;*.jpeg;*.png;|所有檔案|*.*";
                openFileDialog.Title = "載入圖片";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        SaveToUndoStack();
                        Image loadedImage = Image.FromFile(openFileDialog.FileName);

                        // 計算縮放比例和位置
                        float scaleX = (float)canvas.Width / loadedImage.Width;
                        float scaleY = (float)canvas.Height / loadedImage.Height;

                        int drawWidth, drawHeight, drawX, drawY;

                        if (loadedImage.Width > canvas.Width || loadedImage.Height > canvas.Height)
                        {
                            // 圖片太大，需要縮放
                            float scale = Math.Min(scaleX, scaleY);
                            drawWidth = (int)(loadedImage.Width * scale);
                            drawHeight = (int)(loadedImage.Height * scale);
                        }
                        else
                        {
                            // 圖片較小，維持原大小
                            drawWidth = loadedImage.Width;
                            drawHeight = loadedImage.Height;
                        }

                        // 置中
                        drawX = (canvas.Width - drawWidth) / 2;
                        drawY = (canvas.Height - drawHeight) / 2;

                        // 建立圖片物件的獨立圖層
                        Rectangle bounds = new Rectangle(drawX, drawY, drawWidth, drawHeight);
                        Bitmap imageLayer = new Bitmap(drawWidth, drawHeight);

                        using (Graphics g = Graphics.FromImage(imageLayer))
                        {
                            g.Clear(Color.Transparent);
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.DrawImage(loadedImage, 0, 0, drawWidth, drawHeight);
                        }

                        var imageObject = new DrawingObject
                        {
                            Bounds = bounds,
                            Image = imageLayer,
                            OriginalImage = (Image)loadedImage.Clone(),
                            ObjectType = DrawingObjectType.LoadedImage
                        };

                        drawingObjects.Add(imageObject);
                        loadedImage.Dispose();

                        // 重繪畫布以顯示圖片
                        RedrawCanvas();

                        hasUnsavedChanges = true;
                        UpdateNewCanvasMenuItem();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"載入圖片 {openFileDialog.FileName} 時發生錯誤：\n {ex.Message}", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG 圖片|*.png|JPEG 圖片|*.jpg|所有檔案|*.*";
                saveFileDialog.Title = "轉存成圖片";
                saveFileDialog.DefaultExt = "png";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string extension = Path.GetExtension(saveFileDialog.FileName).ToLower();
                        System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;

                        switch (extension)
                        {
                            case ".jpg":
                            case ".jpeg":
                                format = System.Drawing.Imaging.ImageFormat.Jpeg;
                                break;
                        }

                        canvas.Save(saveFileDialog.FileName, format);
                        hasUnsavedChanges = false;
                        UpdateNewCanvasMenuItem();
                        MessageBox.Show($"已將畫布轉存到：\n{saveFileDialog.FileName}", "畫布已轉存", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"無法將畫布轉存至 {saveFileDialog.FileName} \n {ex.Message}", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExitProgram_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 只在畫布不為空且有未儲存變更時提示
            if (hasUnsavedChanges && !IsCanvasEmpty())
            {
                var result = MessageBox.Show(
                    "確定離開嗎？畫布上所有未轉存的物件都會遺失。",
                    "バイバイ？",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void UpdateNewCanvasMenuItem()
        {
            // 檢查畫布是否為空白
            bool isCanvasEmpty = IsCanvasEmpty();
            newCanva.Enabled = !isCanvasEmpty;
        }

        private bool IsCanvasEmpty()
        {
            // 檢查畫布是否全為白色
            for (int x = 0; x < canvas.Width; x += 10)
            {
                for (int y = 0; y < canvas.Height; y += 10)
                {
                    if (x < canvas.Width && y < canvas.Height)
                    {
                        Color pixelColor = canvas.GetPixel(x, y);
                        if (pixelColor.ToArgb() != Color.White.ToArgb())
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void fileMain_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("大畫家 ver1.0\n\n" +
                            "NTOU CS2B 01357101\nYI HONG, HUANG\n\n" +
                            "This Windows Form App is the week6 assignment for the course,\n" +
                            "\"Desktop Application Development\" (B53025QU).\n\nAuthor : github/whitebear13579\nApp Licensed to :\n                            ヽ( ^ω^ ゞ ).",
                            "關於大畫家", MessageBoxButtons.OK);
        }

        private void brushSize_Click_1(object sender, EventArgs e)
        {

        }

        // 新增繪圖輔助方法
        private Rectangle GetBoundingBox(List<Point> points)
        {
            if (points.Count == 0) return Rectangle.Empty;

            int minX = points.Min(p => p.X);
            int minY = points.Min(p => p.Y);
            int maxX = points.Max(p => p.X);
            int maxY = points.Max(p => p.Y);

            return new Rectangle(minX - 5, minY - 5, maxX - minX + 10, maxY - minY + 10);
        }

        private Rectangle GetShapeBounds(Point start, Point end)
        {
            // 針對星形特殊處理
            if (currentTool == DrawingTool.Star)
            {
                float outerRadius;

                if (isShiftPressed)
                {
                    outerRadius = Math.Min(Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));
                }
                else
                {
                    float dx = end.X - start.X;
                    float dy = end.Y - start.Y;
                    outerRadius = (float)Math.Sqrt(dx * dx + dy * dy);
                }

                // 星形的邊界框是以起始點為中心，向四周延伸 outerRadius 的距離
                int padding = 10;
                int x = (int)(start.X - outerRadius - padding);
                int y = (int)(start.Y - outerRadius - padding);
                int size = (int)(outerRadius * 2 + padding * 2);

                return new Rectangle(x, y, size, size);
            }
            else
            {
                // 其他形狀使用原來的計算方式
                int x = Math.Min(start.X, end.X) - 10;
                int y = Math.Min(start.Y, end.Y) - 10;
                int width = Math.Abs(start.X - end.X) + 20;
                int height = Math.Abs(start.Y - end.Y) + 20;
                return new Rectangle(x, y, width, height);
            }
        }

        private void AddObjectToList(Rectangle bounds, Bitmap image, DrawingObjectType type = DrawingObjectType.Other)
        {
            if (image != null && bounds.Width > 0 && bounds.Height > 0)
            {
                drawingObjects.Add(new DrawingObject
                {
                    Bounds = bounds,
                    Image = image,
                    ObjectType = type
                });
            }
        }

        private DrawingObject FindObjectAtPoint(Point location)
        {
            // 從後往前查找（最新的物件在最上層）
            for (int i = drawingObjects.Count - 1; i >= 0; i--)
            {
                if (drawingObjects[i].Bounds.Contains(location))
                {
                    return drawingObjects[i];
                }
            }
            return null;
        }

        private void EraseObject(Point location)
        {
            DrawingObject objToRemove = FindObjectAtPoint(location);
            if (objToRemove != null)
            {
                drawingObjects.Remove(objToRemove);
                RedrawCanvas();
                hasUnsavedChanges = true;
            }
        }

        private void RedrawCanvas()
        {
            canvasGraphics.Clear(Color.White);

            // 按照物件列表的順序繪製（維持圖層順序）
            foreach (var obj in drawingObjects)
            {
                if (obj.IsTextObject)
                {
                    // 繪製文字物件
                    using (Font font = new Font("Microsoft JhengHei", obj.TextSize * 5))
                    using (SolidBrush brush = new SolidBrush(obj.TextColor))
                    {
                        canvasGraphics.DrawString(obj.Text, font, brush, obj.Bounds.Location);
                    }
                }
                else if (obj.Image != null)
                {
                    // 繪製圖像物件（包括形狀、自由曲線和載入的圖片）
                    canvasGraphics.DrawImage(obj.Image, obj.Bounds);
                }
            }
        }

        private void StartTextInput(Point location)
        {
            FinalizeTextInput(); // 結束之前的輸入

            isTextInputActive = true;

            // 將 flowLayoutPanel 的座標轉換為表單的座標
            Point formLocation = flowLayoutPanel1.PointToScreen(location);
            formLocation = this.PointToClient(formLocation);

            Font textFont = new Font("Microsoft JhengHei", currentBrushSize * 5);

            drawingTextBox = new TextBox
            {
                Location = formLocation,
                Font = textFont,
                ForeColor = currentColor,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Multiline = false,
                AutoSize = false,
            };

            // 處理文字變更事件以自動調整寬度
            drawingTextBox.TextChanged += (s, e) =>
            {
                const int padding = 10;
                Size textSize = TextRenderer.MeasureText(drawingTextBox.Text + " ", drawingTextBox.Font);

                int newWidth = textSize.Width + padding;

                // 限制最大寬度不超過畫布邊界
                int maxWidth = this.ClientSize.Width - drawingTextBox.Left - 5;

                if (newWidth > maxWidth)
                {
                    if (!drawingTextBox.Multiline)
                    {
                        drawingTextBox.Multiline = true;
                        drawingTextBox.ScrollBars = ScrollBars.Vertical;
                    }
                    newWidth = maxWidth;
                }

                drawingTextBox.Width = newWidth;

                if (drawingTextBox.Multiline)
                {
                    int numLines = drawingTextBox.GetLineFromCharIndex(drawingTextBox.Text.Length) + 1;
                    int newHeight = (textSize.Height / (drawingTextBox.Text.Split('\n').Length)) * numLines + padding;
                    drawingTextBox.Height = newHeight;
                }
                else
                {
                    drawingTextBox.Height = textSize.Height + padding;
                }
            };

            drawingTextBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!e.Shift)
                    {
                        FinalizeTextInput();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    else
                    {
                        if (!drawingTextBox.Multiline)
                        {
                            drawingTextBox.Multiline = true;
                            drawingTextBox.ScrollBars = ScrollBars.Vertical;
                        }
                        int selectionIndex = drawingTextBox.SelectionStart;
                        drawingTextBox.Text = drawingTextBox.Text.Insert(selectionIndex, Environment.NewLine);
                        drawingTextBox.SelectionStart = selectionIndex + Environment.NewLine.Length;
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    isTextInputActive = false;
                    this.Controls.Remove(drawingTextBox);
                    drawingTextBox.Dispose();
                    drawingTextBox = null;
                    e.Handled = true;
                }
            };

            drawingTextBox.LostFocus += (s, e) => FinalizeTextInput();

            this.Controls.Add(drawingTextBox);
            drawingTextBox.BringToFront();
            drawingTextBox.Focus();

            drawingTextBox.Text = " ";
            drawingTextBox.Text = "";
        }

        private void FinalizeTextInput()
        {
            if (drawingTextBox != null && isTextInputActive)
            {
                isTextInputActive = false;
                string textToDraw = drawingTextBox.Text;
                Point textLocation = drawingTextBox.Location;

                Point canvasLocation = flowLayoutPanel1.PointToClient(this.PointToScreen(textLocation));

                if (!string.IsNullOrEmpty(textToDraw))
                {
                    SaveToUndoStack();
                    using (Font font = new Font("Microsoft JhengHei", currentBrushSize * 5))
                    {
                        SizeF textSize = canvasGraphics.MeasureString(textToDraw, font);
                        Rectangle bounds = new Rectangle(canvasLocation.X, canvasLocation.Y, (int)Math.Ceiling(textSize.Width), (int)Math.Ceiling(textSize.Height));

                        var textObject = new DrawingObject
                        {
                            Bounds = bounds,
                            IsTextObject = true,
                            Text = textToDraw,
                            TextColor = currentColor,
                            TextSize = currentBrushSize,
                            ObjectType = DrawingObjectType.Text
                        };

                        drawingObjects.Add(textObject);

                        // 重繪畫布以顯示文字
                        RedrawCanvas();
                    }
                    hasUnsavedChanges = true;
                    UpdateNewCanvasMenuItem();
                }

                this.Controls.Remove(drawingTextBox);
                drawingTextBox.Dispose();
                drawingTextBox = null;
            }
        }

        private void DrawShape(Graphics g, Point start, Point end, bool isPreview)
        {
            using (Pen pen = new Pen(currentColor, currentBrushSize))
            {
                pen.DashStyle = currentDashStyle;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                switch (currentTool)
                {
                    case DrawingTool.Line:
                        g.DrawLine(pen, start, end);
                        break;

                    case DrawingTool.SingleArrow:
                        DrawArrow(g, pen, start, end, false);
                        break;

                    case DrawingTool.DoubleArrow:
                        DrawArrow(g, pen, start, end, true);
                        break;

                    case DrawingTool.Rectangle:
                        DrawRectangle(g, pen, start, end);
                        break;

                    case DrawingTool.Oval:
                        DrawOval(g, pen, start, end);
                        break;

                    case DrawingTool.Triangle:
                        DrawTriangle(g, pen, start, end);
                        break;

                    case DrawingTool.Star:
                        DrawStar(g, pen, start, end);
                        break;
                }
            }
        }

        private void DrawArrow(Graphics g, Pen pen, Point start, Point end, bool isDouble)
        {
            // 繪製線條
            g.DrawLine(pen, start, end);

            // 計算箭頭
            double angle = Math.Atan2(end.Y - start.Y, end.X - start.X);
            int arrowSize = (int)(currentBrushSize * 5);

            using (SolidBrush brush = new SolidBrush(currentColor))
            {
                // 終點箭頭
                PointF[] arrowHead1 = new PointF[3];
                arrowHead1[0] = end;
                arrowHead1[1] = new PointF(
                    end.X - arrowSize * (float)Math.Cos(angle - Math.PI / 6),
                    end.Y - arrowSize * (float)Math.Sin(angle - Math.PI / 6));
                arrowHead1[2] = new PointF(
                    end.X - arrowSize * (float)Math.Cos(angle + Math.PI / 6),
                    end.Y - arrowSize * (float)Math.Sin(angle + Math.PI / 6));

                g.FillPolygon(brush, arrowHead1);

                // 雙向箭頭
                if (isDouble)
                {
                    PointF[] arrowHead2 = new PointF[3];
                    arrowHead2[0] = start;
                    arrowHead2[1] = new PointF(
                        start.X + arrowSize * (float)Math.Cos(angle - Math.PI / 6),
                        start.Y + arrowSize * (float)Math.Sin(angle - Math.PI / 6));
                    arrowHead2[2] = new PointF(
                        start.X + arrowSize * (float)Math.Cos(angle + Math.PI / 6),
                        start.Y + arrowSize * (float)Math.Sin(angle + Math.PI / 6));

                    g.FillPolygon(brush, arrowHead2);
                }
            }
        }

        private void DrawRectangle(Graphics g, Pen pen, Point start, Point end)
        {
            int x = Math.Min(start.X, end.X);
            int y = Math.Min(start.Y, end.Y);
            int width = Math.Abs(start.X - end.X);
            int height = Math.Abs(start.Y - end.Y);

            // Shift 鍵按下時繪製正方形
            if (isShiftPressed)
            {
                int size = Math.Min(width, height);
                width = height = size;
            }

            g.DrawRectangle(pen, x, y, width, height);
        }

        private void DrawOval(Graphics g, Pen pen, Point start, Point end)
        {
            int x = Math.Min(start.X, end.X);
            int y = Math.Min(start.Y, end.Y);
            int width = Math.Abs(start.X - end.X);
            int height = Math.Abs(start.Y - end.Y);

            // Shift 鍵按下時繪製正圓形
            if (isShiftPressed)
            {
                int size = Math.Min(width, height);
                width = height = size;
            }

            g.DrawEllipse(pen, x, y, width, height);
        }

        private void DrawTriangle(Graphics g, Pen pen, Point start, Point end)
        {
            PointF[] points = new PointF[3];

            if (isShiftPressed)
            {
                // 繪製正三角形
                int size = Math.Min(Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));
                float height = (float)(size * Math.Sqrt(3) / 2);

                points[0] = new PointF(start.X + size / 2f, start.Y); // 頂點
                points[1] = new PointF(start.X, start.Y + height); // 左下
                points[2] = new PointF(start.X + size, start.Y + height); // 右下
            }
            else
            {
                points[0] = new PointF((start.X + end.X) / 2, start.Y); // 頂點
                points[1] = new PointF(start.X, end.Y); // 左下
                points[2] = new PointF(end.X, end.Y); // 右下
            }

            g.DrawPolygon(pen, points);
        }

        private void DrawStar(Graphics g, Pen pen, Point start, Point end)
        {
            // 使用起始點作為中心點，而不是起始點和結束點的中點
            PointF center = new PointF(start.X, start.Y);
            float outerRadius;

            if (isShiftPressed)
            {
                // 繪製正比例星形
                outerRadius = Math.Min(Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));
            }
            else
            {
                // 使用與滑鼠的距離作為半徑
                float dx = end.X - start.X;
                float dy = end.Y - start.Y;
                outerRadius = (float)Math.Sqrt(dx * dx + dy * dy);
            }

            float innerRadius = outerRadius / 2.5f;

            PointF[] starPoints = new PointF[10];
            double angle = -Math.PI / 2; // 從頂端開始

            for (int i = 0; i < 10; i++)
            {
                float radius = (i % 2 == 0) ? outerRadius : innerRadius;
                starPoints[i] = new PointF(
                    center.X + radius * (float)Math.Cos(angle),
                    center.Y + radius * (float)Math.Sin(angle));
                angle += Math.PI / 5;
            }

            g.DrawPolygon(pen, starPoints);
        }

        private void CreateEraserCursor()
        {
            try
            {
                // 從 eraserButton 取得圖示
                if (eraserButton.Image != null)
                {
                    // 建立一個新的 Bitmap 用於游標 (32x32 是標準大小)
                    int cursorSize = 32;
                    Bitmap cursorBitmap = new Bitmap(cursorSize, cursorSize);

                    using (Graphics g = Graphics.FromImage(cursorBitmap))
                    {
                        g.Clear(Color.Transparent);
                        g.SmoothingMode = SmoothingMode.AntiAlias;

                        // 將圖示繪製在游標中央
                        int imageSize = 24; // 圖示大小
                        int offset = (cursorSize - imageSize) / 2;

                        g.DrawImage(eraserButton.Image,
                            new Rectangle(offset, offset, imageSize, imageSize),
                            new Rectangle(0, 0, eraserButton.Image.Width, eraserButton.Image.Height),
                            GraphicsUnit.Pixel);
                    }

                    // 建立游標（熱點設在中心）
                    IntPtr hIcon = cursorBitmap.GetHicon();
                    Icon icon = Icon.FromHandle(hIcon);
                    eraserCursor = new Cursor(hIcon);

                    // 清理資源
                    icon.Dispose();
                    cursorBitmap.Dispose();
                }
            }
            catch
            {
                // 如果建立失敗，使用預設游標
                eraserCursor = Cursors.Hand;
            }
        }
    }

    // 繪圖物件類型枚舉
    public enum DrawingObjectType
    {
        FreeDraw,
        Line,
        SingleArrow,
        DoubleArrow,
        Rectangle,
        Oval,
        Triangle,
        Star,
        Text,
        LoadedImage,
        Other
    }

    // 繪圖物件類別（用於物件管理）
    public class DrawingObject
    {
        public Rectangle Bounds { get; set; }
        public Bitmap Image { get; set; }

        // 新增原始圖片屬性
        public Image OriginalImage { get; set; }

        // 新增文字相關屬性
        public bool IsTextObject { get; set; } = false;
        public string Text { get; set; }
        public Color TextColor { get; set; }
        public float TextSize { get; set; }

        // 新增物件類型屬性
        public DrawingObjectType ObjectType { get; set; } = DrawingObjectType.Other;
    }
}
