using System.Windows.Forms;

namespace Obj
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fillCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarTime = new System.Windows.Forms.TrackBar();
            this.FileBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.playButton = new System.Windows.Forms.Button();
            this.Move_RadioBTN = new System.Windows.Forms.RadioButton();
            this.Rotate_RadioBTN = new System.Windows.Forms.RadioButton();
            this.recordButton = new System.Windows.Forms.Button();
            this.PCT_SLIDEER_Z = new System.Windows.Forms.PictureBox();
            this.PCT_SLIDEER_Y = new System.Windows.Forms.PictureBox();
            this.PCT_SLIDEER_X = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.PCT_SLIDER_SCALE = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.treeView = new System.Windows.Forms.TreeView();
            this.PCT_Canvas = new System.Windows.Forms.PictureBox();
            this.TIMER_PlayButton = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTime)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PCT_SLIDEER_Z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PCT_SLIDEER_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PCT_SLIDEER_X)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PCT_SLIDER_SCALE)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PCT_Canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fillCheckBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.trackBarTime);
            this.panel1.Controls.Add(this.FileBTN);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1710, 52);
            this.panel1.TabIndex = 0;
            // 
            // fillCheckBox
            // 
            this.fillCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fillCheckBox.AutoSize = true;
            this.fillCheckBox.Checked = true;
            this.fillCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fillCheckBox.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fillCheckBox.ForeColor = System.Drawing.Color.White;
            this.fillCheckBox.Location = new System.Drawing.Point(1611, 8);
            this.fillCheckBox.Name = "fillCheckBox";
            this.fillCheckBox.Size = new System.Drawing.Size(69, 35);
            this.fillCheckBox.TabIndex = 11;
            this.fillCheckBox.Text = "Fill";
            this.fillCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(271, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 38);
            this.label2.TabIndex = 10;
            this.label2.Text = "Seconds";
            // 
            // trackBarTime
            // 
            this.trackBarTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBarTime.Location = new System.Drawing.Point(406, 8);
            this.trackBarTime.Maximum = 5;
            this.trackBarTime.Name = "trackBarTime";
            this.trackBarTime.Size = new System.Drawing.Size(1170, 56);
            this.trackBarTime.TabIndex = 9;
            this.trackBarTime.Scroll += new System.EventHandler(this.trackBarTime_Scroll);
            // 
            // FileBTN
            // 
            this.FileBTN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.FileBTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FileBTN.FlatAppearance.BorderSize = 0;
            this.FileBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FileBTN.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.FileBTN.ForeColor = System.Drawing.Color.White;
            this.FileBTN.Location = new System.Drawing.Point(19, 6);
            this.FileBTN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FileBTN.Name = "FileBTN";
            this.FileBTN.Size = new System.Drawing.Size(224, 42);
            this.FileBTN.TabIndex = 4;
            this.FileBTN.Text = "Select file";
            this.FileBTN.UseVisualStyleBackColor = false;
            this.FileBTN.Click += new System.EventHandler(this.FileBTN_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, -5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scale";
            // 
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Interval = 10;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.playButton);
            this.panel3.Controls.Add(this.Move_RadioBTN);
            this.panel3.Controls.Add(this.Rotate_RadioBTN);
            this.panel3.Controls.Add(this.recordButton);
            this.panel3.Controls.Add(this.PCT_SLIDEER_Z);
            this.panel3.Controls.Add(this.PCT_SLIDEER_Y);
            this.panel3.Controls.Add(this.PCT_SLIDEER_X);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 670);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1710, 156);
            this.panel3.TabIndex = 1;
            // 
            // playButton
            // 
            this.playButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.playButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playButton.FlatAppearance.BorderSize = 0;
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playButton.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.playButton.ForeColor = System.Drawing.Color.White;
            this.playButton.Location = new System.Drawing.Point(12, 89);
            this.playButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(177, 42);
            this.playButton.TabIndex = 6;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = false;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // Move_RadioBTN
            // 
            this.Move_RadioBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Move_RadioBTN.AutoSize = true;
            this.Move_RadioBTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Move_RadioBTN.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Move_RadioBTN.ForeColor = System.Drawing.Color.White;
            this.Move_RadioBTN.Location = new System.Drawing.Point(1606, 82);
            this.Move_RadioBTN.Name = "Move_RadioBTN";
            this.Move_RadioBTN.Size = new System.Drawing.Size(95, 35);
            this.Move_RadioBTN.TabIndex = 10;
            this.Move_RadioBTN.TabStop = true;
            this.Move_RadioBTN.Text = "Move";
            this.Move_RadioBTN.UseVisualStyleBackColor = true;
            // 
            // Rotate_RadioBTN
            // 
            this.Rotate_RadioBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Rotate_RadioBTN.AutoSize = true;
            this.Rotate_RadioBTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Rotate_RadioBTN.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rotate_RadioBTN.ForeColor = System.Drawing.Color.White;
            this.Rotate_RadioBTN.Location = new System.Drawing.Point(1607, 31);
            this.Rotate_RadioBTN.Name = "Rotate_RadioBTN";
            this.Rotate_RadioBTN.Size = new System.Drawing.Size(105, 35);
            this.Rotate_RadioBTN.TabIndex = 9;
            this.Rotate_RadioBTN.TabStop = true;
            this.Rotate_RadioBTN.Text = "Rotate";
            this.Rotate_RadioBTN.UseVisualStyleBackColor = true;
            // 
            // recordButton
            // 
            this.recordButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.recordButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.recordButton.FlatAppearance.BorderSize = 0;
            this.recordButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.recordButton.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.recordButton.ForeColor = System.Drawing.Color.White;
            this.recordButton.Location = new System.Drawing.Point(12, 18);
            this.recordButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(177, 42);
            this.recordButton.TabIndex = 5;
            this.recordButton.Text = "Record";
            this.recordButton.UseVisualStyleBackColor = false;
            this.recordButton.Click += new System.EventHandler(this.recordButton_Click);
            // 
            // PCT_SLIDEER_Z
            // 
            this.PCT_SLIDEER_Z.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PCT_SLIDEER_Z.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.PCT_SLIDEER_Z.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PCT_SLIDEER_Z.Location = new System.Drawing.Point(199, 113);
            this.PCT_SLIDEER_Z.Margin = new System.Windows.Forms.Padding(4);
            this.PCT_SLIDEER_Z.Name = "PCT_SLIDEER_Z";
            this.PCT_SLIDEER_Z.Size = new System.Drawing.Size(1300, 18);
            this.PCT_SLIDEER_Z.TabIndex = 8;
            this.PCT_SLIDEER_Z.TabStop = false;
            this.PCT_SLIDEER_Z.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PCT_SLIDEER_Z_MouseDown);
            this.PCT_SLIDEER_Z.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PCT_SLIDEER_Z_MouseMove);
            this.PCT_SLIDEER_Z.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PCT_SLIDEER_Z_MouseUp);
            // 
            // PCT_SLIDEER_Y
            // 
            this.PCT_SLIDEER_Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PCT_SLIDEER_Y.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.PCT_SLIDEER_Y.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PCT_SLIDEER_Y.Location = new System.Drawing.Point(199, 68);
            this.PCT_SLIDEER_Y.Margin = new System.Windows.Forms.Padding(4);
            this.PCT_SLIDEER_Y.Name = "PCT_SLIDEER_Y";
            this.PCT_SLIDEER_Y.Size = new System.Drawing.Size(1300, 18);
            this.PCT_SLIDEER_Y.TabIndex = 7;
            this.PCT_SLIDEER_Y.TabStop = false;
            this.PCT_SLIDEER_Y.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PCT_SLIDEER_Y_MouseDown);
            this.PCT_SLIDEER_Y.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PCT_SLIDEER_Y_MouseMove);
            this.PCT_SLIDEER_Y.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PCT_SLIDEER_Y_MouseUp);
            // 
            // PCT_SLIDEER_X
            // 
            this.PCT_SLIDEER_X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PCT_SLIDEER_X.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.PCT_SLIDEER_X.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PCT_SLIDEER_X.Location = new System.Drawing.Point(199, 18);
            this.PCT_SLIDEER_X.Margin = new System.Windows.Forms.Padding(4);
            this.PCT_SLIDEER_X.Name = "PCT_SLIDEER_X";
            this.PCT_SLIDEER_X.Size = new System.Drawing.Size(1300, 18);
            this.PCT_SLIDEER_X.TabIndex = 6;
            this.PCT_SLIDEER_X.TabStop = false;
            this.PCT_SLIDEER_X.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PCT_SLIDEER_X_MouseDown);
            this.PCT_SLIDEER_X.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PCT_SLIDEER_X_MouseMove);
            this.PCT_SLIDEER_X.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PCT_SLIDEER_X_MouseUp);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.PCT_Canvas);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 52);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1710, 774);
            this.panel4.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.PCT_SLIDER_SCALE);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(1582, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(128, 774);
            this.panel5.TabIndex = 5;
            // 
            // PCT_SLIDER_SCALE
            // 
            this.PCT_SLIDER_SCALE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.PCT_SLIDER_SCALE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.PCT_SLIDER_SCALE.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PCT_SLIDER_SCALE.Location = new System.Drawing.Point(54, 35);
            this.PCT_SLIDER_SCALE.Margin = new System.Windows.Forms.Padding(4);
            this.PCT_SLIDER_SCALE.Name = "PCT_SLIDER_SCALE";
            this.PCT_SLIDER_SCALE.Size = new System.Drawing.Size(20, 579);
            this.PCT_SLIDER_SCALE.TabIndex = 7;
            this.PCT_SLIDER_SCALE.TabStop = false;
            this.PCT_SLIDER_SCALE.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PCT_SLIDER_SCALE_MouseDown);
            this.PCT_SLIDER_SCALE.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PCT_SLIDER_SCALE_MouseMove);
            this.PCT_SLIDER_SCALE.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PCT_SLIDER_SCALE_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.treeView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 774);
            this.panel2.TabIndex = 4;
            // 
            // treeView
            // 
            this.treeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView.ForeColor = System.Drawing.Color.White;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(200, 774);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // PCT_Canvas
            // 
            this.PCT_Canvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.PCT_Canvas.Location = new System.Drawing.Point(199, 0);
            this.PCT_Canvas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PCT_Canvas.Name = "PCT_Canvas";
            this.PCT_Canvas.Size = new System.Drawing.Size(1703, 774);
            this.PCT_Canvas.TabIndex = 3;
            this.PCT_Canvas.TabStop = false;
            // 
            // TIMER_PlayButton
            // 
            this.TIMER_PlayButton.Interval = 1000;
            this.TIMER_PlayButton.Tick += new System.EventHandler(this.TIMER_PlayButton_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1710, 826);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1707, 839);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTime)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PCT_SLIDEER_Z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PCT_SLIDEER_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PCT_SLIDEER_X)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PCT_SLIDER_SCALE)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PCT_Canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private System.Windows.Forms.Timer Timer;
        private Label label1;
        private Button FileBTN;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Panel panel2;
        private PictureBox PCT_Canvas;
        private TreeView treeView;
        private PictureBox PCT_SLIDEER_Z;
        private PictureBox PCT_SLIDEER_Y;
        private PictureBox PCT_SLIDEER_X;
        private RadioButton Rotate_RadioBTN;
        private RadioButton Move_RadioBTN;
        private Button playButton;
        private Button recordButton;
        private PictureBox PCT_SLIDER_SCALE;
        private Label label2;
        private TrackBar trackBarTime;
        private Timer TIMER_PlayButton;
        private CheckBox fillCheckBox;
    }
}

