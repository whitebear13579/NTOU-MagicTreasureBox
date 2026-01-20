namespace week6_hw
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            toolStrip1 = new ToolStrip();
            fileMain = new ToolStripDropDownButton();
            newCanva = new ToolStripMenuItem();
            loadImg = new ToolStripMenuItem();
            saveFile = new ToolStripMenuItem();
            exitProgram = new ToolStripMenuItem();
            aboutProgram = new ToolStripDropDownButton();
            aboutButton = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            undoButton = new ToolStripButton();
            redoButton = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            moveButton = new ToolStripButton();
            brushStyle = new ToolStripDropDownButton();
            solidLineMenuItem = new ToolStripMenuItem();
            dashLineMenuItem = new ToolStripMenuItem();
            dotLineMenuItem = new ToolStripMenuItem();
            dashDotLineMenuItem = new ToolStripMenuItem();
            dashDotDotLineMenuItem = new ToolStripMenuItem();
            brushSize = new ToolStripButton();
            eraserButton = new ToolStripButton();
            colorPalette = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            textDraw = new ToolStripButton();
            freeDrawButton = new ToolStripButton();
            lineDraw = new ToolStripButton();
            singleArrowDraw = new ToolStripButton();
            doubleArrowDraw = new ToolStripButton();
            rectDraw = new ToolStripButton();
            ovalDraw = new ToolStripButton();
            triangleDraw = new ToolStripButton();
            starDraw = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            flowLayoutPanel1 = new FlowLayoutPanel();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.LightGray;
            toolStrip1.Items.AddRange(new ToolStripItem[] { fileMain, aboutProgram, toolStripSeparator1, undoButton, redoButton, toolStripSeparator2, moveButton, brushStyle, brushSize, eraserButton, colorPalette, toolStripSeparator3, textDraw, freeDrawButton, lineDraw, singleArrowDraw, doubleArrowDraw, rectDraw, ovalDraw, triangleDraw, starDraw, toolStripSeparator5 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(934, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            toolStrip1.ItemClicked += toolStrip1_ItemClicked;
            // 
            // fileMain
            // 
            fileMain.DisplayStyle = ToolStripItemDisplayStyle.Text;
            fileMain.DropDownItems.AddRange(new ToolStripItem[] { newCanva, loadImg, saveFile, exitProgram });
            fileMain.Font = new Font("Microsoft JhengHei UI", 12F);
            fileMain.ImageTransparentColor = Color.Magenta;
            fileMain.Name = "fileMain";
            fileMain.Size = new Size(54, 24);
            fileMain.Text = "檔案";
            // 
            // newCanva
            // 
            newCanva.Name = "newCanva";
            newCanva.Size = new Size(180, 24);
            newCanva.Text = "開新畫布";
            // 
            // loadImg
            // 
            loadImg.Name = "loadImg";
            loadImg.Size = new Size(180, 24);
            loadImg.Text = "載入圖片";
            // 
            // saveFile
            // 
            saveFile.Name = "saveFile";
            saveFile.Size = new Size(180, 24);
            saveFile.Text = "轉存圖片";
            // 
            // exitProgram
            // 
            exitProgram.Name = "exitProgram";
            exitProgram.Size = new Size(180, 24);
            exitProgram.Text = "結束程式";
            // 
            // aboutProgram
            // 
            aboutProgram.DisplayStyle = ToolStripItemDisplayStyle.Text;
            aboutProgram.DropDownItems.AddRange(new ToolStripItem[] { aboutButton });
            aboutProgram.Font = new Font("Microsoft JhengHei UI", 12F);
            aboutProgram.Image = (Image)resources.GetObject("aboutProgram.Image");
            aboutProgram.ImageTransparentColor = Color.Magenta;
            aboutProgram.Name = "aboutProgram";
            aboutProgram.Size = new Size(54, 24);
            aboutProgram.Text = "關於";
            // 
            // aboutButton
            // 
            aboutButton.Name = "aboutButton";
            aboutButton.Size = new Size(170, 24);
            aboutButton.Text = "ヽ( ^ω^ ゞ )";
            aboutButton.Click += aboutToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 27);
            // 
            // undoButton
            // 
            undoButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            undoButton.Font = new Font("Microsoft JhengHei UI", 12F);
            undoButton.Image = (Image)resources.GetObject("undoButton.Image");
            undoButton.ImageTransparentColor = Color.Magenta;
            undoButton.Name = "undoButton";
            undoButton.Size = new Size(23, 24);
            undoButton.Text = "復原";
            // 
            // redoButton
            // 
            redoButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            redoButton.Image = (Image)resources.GetObject("redoButton.Image");
            redoButton.ImageTransparentColor = Color.Magenta;
            redoButton.Name = "redoButton";
            redoButton.Size = new Size(23, 24);
            redoButton.Text = "重做";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 27);
            // 
            // moveButton
            // 
            moveButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            moveButton.Image = (Image)resources.GetObject("moveButton.Image");
            moveButton.ImageTransparentColor = Color.Magenta;
            moveButton.Name = "moveButton";
            moveButton.Size = new Size(23, 24);
            moveButton.Text = "托拽工具";
            // 
            // brushStyle
            // 
            brushStyle.DisplayStyle = ToolStripItemDisplayStyle.Image;
            brushStyle.DropDownItems.AddRange(new ToolStripItem[] { solidLineMenuItem, dashLineMenuItem, dotLineMenuItem, dashDotLineMenuItem, dashDotDotLineMenuItem });
            brushStyle.Image = (Image)resources.GetObject("brushStyle.Image");
            brushStyle.ImageTransparentColor = Color.Magenta;
            brushStyle.Name = "brushStyle";
            brushStyle.Size = new Size(29, 24);
            brushStyle.Text = "畫筆樣式";
            // 
            // solidLineMenuItem
            // 
            solidLineMenuItem.Name = "solidLineMenuItem";
            solidLineMenuItem.Size = new Size(203, 22);
            solidLineMenuItem.Text = "實線 (Solid)";
            // 
            // dashLineMenuItem
            // 
            dashLineMenuItem.Name = "dashLineMenuItem";
            dashLineMenuItem.Size = new Size(203, 22);
            dashLineMenuItem.Text = "虛線 (Dash)";
            // 
            // dotLineMenuItem
            // 
            dotLineMenuItem.Name = "dotLineMenuItem";
            dotLineMenuItem.Size = new Size(203, 22);
            dotLineMenuItem.Text = "點線 (Dot)";
            // 
            // dashDotLineMenuItem
            // 
            dashDotLineMenuItem.Name = "dashDotLineMenuItem";
            dashDotLineMenuItem.Size = new Size(203, 22);
            dashDotLineMenuItem.Text = "點劃線 (DashDot)";
            // 
            // dashDotDotLineMenuItem
            // 
            dashDotDotLineMenuItem.Name = "dashDotDotLineMenuItem";
            dashDotDotLineMenuItem.Size = new Size(203, 22);
            dashDotDotLineMenuItem.Text = "雙點劃線 (DashDotDot)";
            // 
            // brushSize
            // 
            brushSize.DisplayStyle = ToolStripItemDisplayStyle.Image;
            brushSize.Image = (Image)resources.GetObject("brushSize.Image");
            brushSize.ImageTransparentColor = Color.Magenta;
            brushSize.Name = "brushSize";
            brushSize.Size = new Size(23, 24);
            brushSize.Text = "筆刷大小";
            // 
            // eraserButton
            // 
            eraserButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            eraserButton.Image = (Image)resources.GetObject("eraserButton.Image");
            eraserButton.ImageTransparentColor = Color.Magenta;
            eraserButton.Name = "eraserButton";
            eraserButton.Size = new Size(23, 24);
            eraserButton.Text = "橡皮擦";
            // 
            // colorPalette
            // 
            colorPalette.DisplayStyle = ToolStripItemDisplayStyle.Image;
            colorPalette.Image = (Image)resources.GetObject("colorPalette.Image");
            colorPalette.ImageTransparentColor = Color.Magenta;
            colorPalette.Name = "colorPalette";
            colorPalette.Size = new Size(23, 24);
            colorPalette.Text = "調色盤";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 27);
            // 
            // textDraw
            // 
            textDraw.DisplayStyle = ToolStripItemDisplayStyle.Image;
            textDraw.Image = (Image)resources.GetObject("textDraw.Image");
            textDraw.ImageTransparentColor = Color.Magenta;
            textDraw.Name = "textDraw";
            textDraw.Size = new Size(23, 24);
            textDraw.Text = "繪製文字";
            textDraw.Click += toolStripButton1_Click_1;
            // 
            // freeDrawButton
            // 
            freeDrawButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            freeDrawButton.Image = (Image)resources.GetObject("freeDrawButton.Image");
            freeDrawButton.ImageTransparentColor = Color.Magenta;
            freeDrawButton.Name = "freeDrawButton";
            freeDrawButton.Size = new Size(23, 24);
            freeDrawButton.Text = "自由曲線";
            // 
            // lineDraw
            // 
            lineDraw.DisplayStyle = ToolStripItemDisplayStyle.Image;
            lineDraw.Image = (Image)resources.GetObject("lineDraw.Image");
            lineDraw.ImageTransparentColor = Color.Magenta;
            lineDraw.Name = "lineDraw";
            lineDraw.Size = new Size(23, 24);
            lineDraw.Text = "繪製直線";
            // 
            // singleArrowDraw
            // 
            singleArrowDraw.DisplayStyle = ToolStripItemDisplayStyle.Image;
            singleArrowDraw.Image = (Image)resources.GetObject("singleArrowDraw.Image");
            singleArrowDraw.ImageTransparentColor = Color.Magenta;
            singleArrowDraw.Name = "singleArrowDraw";
            singleArrowDraw.Size = new Size(23, 24);
            singleArrowDraw.Text = "繪製單向箭頭";
            // 
            // doubleArrowDraw
            // 
            doubleArrowDraw.DisplayStyle = ToolStripItemDisplayStyle.Image;
            doubleArrowDraw.Image = (Image)resources.GetObject("doubleArrowDraw.Image");
            doubleArrowDraw.ImageTransparentColor = Color.Magenta;
            doubleArrowDraw.Name = "doubleArrowDraw";
            doubleArrowDraw.Size = new Size(23, 24);
            doubleArrowDraw.Text = "繪製雙向箭頭";
            // 
            // rectDraw
            // 
            rectDraw.DisplayStyle = ToolStripItemDisplayStyle.Image;
            rectDraw.Image = (Image)resources.GetObject("rectDraw.Image");
            rectDraw.ImageTransparentColor = Color.Magenta;
            rectDraw.Name = "rectDraw";
            rectDraw.Size = new Size(23, 24);
            rectDraw.Text = "繪製矩形";
            // 
            // ovalDraw
            // 
            ovalDraw.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ovalDraw.Image = (Image)resources.GetObject("ovalDraw.Image");
            ovalDraw.ImageTransparentColor = Color.Magenta;
            ovalDraw.Name = "ovalDraw";
            ovalDraw.Size = new Size(23, 24);
            ovalDraw.Text = "繪製橢圓";
            // 
            // triangleDraw
            // 
            triangleDraw.DisplayStyle = ToolStripItemDisplayStyle.Image;
            triangleDraw.Image = (Image)resources.GetObject("triangleDraw.Image");
            triangleDraw.ImageTransparentColor = Color.Magenta;
            triangleDraw.Name = "triangleDraw";
            triangleDraw.Size = new Size(23, 24);
            triangleDraw.Text = "繪製三角形";
            // 
            // starDraw
            // 
            starDraw.DisplayStyle = ToolStripItemDisplayStyle.Image;
            starDraw.Image = (Image)resources.GetObject("starDraw.Image");
            starDraw.ImageTransparentColor = Color.Magenta;
            starDraw.Name = "starDraw";
            starDraw.Size = new Size(23, 24);
            starDraw.Text = "繪製星形";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 27);
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.BackColor = Color.White;
            flowLayoutPanel1.Location = new Point(0, 30);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(934, 631);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            ClientSize = new Size(934, 661);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(toolStrip1);
            MinimumSize = new Size(800, 600);
            Name = "Form1";
            Text = "大畫家";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton undoButton;
        private ToolStripButton redoButton;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripDropDownButton brushStyle;
        private ToolStripDropDownButton fileMain;
        private ToolStripButton colorPalette;
        private ToolStripDropDownButton aboutProgram;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton textDraw;
        private ToolStripButton lineDraw;
        private ToolStripButton singleArrowDraw;
        private ToolStripButton doubleArrowDraw;
        private ToolStripButton rectDraw;
        private ToolStripButton ovalDraw;
        private ToolStripButton triangleDraw;
        private ToolStripButton starDraw;
        private ToolStripMenuItem newCanva;
        private ToolStripMenuItem loadImg;
        private ToolStripMenuItem saveFile;
        private ToolStripMenuItem exitProgram;
        private ToolStripMenuItem aboutButton;
        private FlowLayoutPanel flowLayoutPanel1;
        private ToolStripButton brushSize;
        private ToolStripButton freeDrawButton;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton eraserButton;
        private ToolStripButton moveButton;
        private ToolStripMenuItem solidLineMenuItem;
        private ToolStripMenuItem dashLineMenuItem;
        private ToolStripMenuItem dotLineMenuItem;
        private ToolStripMenuItem dashDotLineMenuItem;
        private ToolStripMenuItem dashDotDotLineMenuItem;
    }
}
