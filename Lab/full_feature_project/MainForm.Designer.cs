namespace lab_final
{
	partial class MainForm
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
		private void InitializeComponent() {
			pictureBox1 = new PictureBox();
			listBox1 = new ListBox();
			button1 = new Button();
			button2 = new Button();
			button3 = new Button();
			button4 = new Button();
			button5 = new Button();
			button6 = new Button();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// pictureBox1
			// 
			pictureBox1.BackColor = Color.Gainsboro;
			pictureBox1.Location = new Point(12, 12);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(851, 723);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			// 
			// listBox1
			// 
			listBox1.FormattingEnabled = true;
			listBox1.Location = new Point(869, 12);
			listBox1.Name = "listBox1";
			listBox1.Size = new Size(296, 724);
			listBox1.TabIndex = 1;
			// 
			// button1
			// 
			button1.Location = new Point(1171, 12);
			button1.Name = "button1";
			button1.Size = new Size(171, 56);
			button1.TabIndex = 2;
			button1.Text = "Load Graph";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.Location = new Point(1171, 74);
			button2.Name = "button2";
			button2.Size = new Size(171, 56);
			button2.TabIndex = 3;
			button2.Text = "Save Graph";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// button3
			// 
			button3.Location = new Point(1171, 136);
			button3.Name = "button3";
			button3.Size = new Size(171, 56);
			button3.TabIndex = 4;
			button3.Text = "DFS";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// button4
			// 
			button4.Location = new Point(1171, 198);
			button4.Name = "button4";
			button4.Size = new Size(171, 56);
			button4.TabIndex = 5;
			button4.Text = "BFS";
			button4.UseVisualStyleBackColor = true;
			button4.Click += button4_Click;
			// 
			// button5
			// 
			button5.Location = new Point(1171, 260);
			button5.Name = "button5";
			button5.Size = new Size(171, 56);
			button5.TabIndex = 6;
			button5.Text = "Coloring";
			button5.UseVisualStyleBackColor = true;
			button5.Click += button5_Click;
			// 
			// button6
			// 
			button6.Location = new Point(1171, 322);
			button6.Name = "button6";
			button6.Size = new Size(171, 56);
			button6.TabIndex = 7;
			button6.Text = "Dijkstra";
			button6.UseVisualStyleBackColor = true;
			button6.Click += button6_Click;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.Control;
			ClientSize = new Size(1354, 747);
			Controls.Add(button6);
			Controls.Add(button5);
			Controls.Add(button4);
			Controls.Add(button3);
			Controls.Add(button2);
			Controls.Add(button1);
			Controls.Add(listBox1);
			Controls.Add(pictureBox1);
			Name = "MainForm";
			Text = "Metode Avansate de Programare Proiect Laborator";
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}

		#endregion

		public PictureBox pictureBox1;
		public ListBox listBox1;
		public Button button1;
		public Button button2;
		public Button button3;
		public Button button4;
		public Button button5;
		public Button button6;
	}
}
