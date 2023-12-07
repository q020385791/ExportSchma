namespace ExportSchma
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
            txtConnectionString = new TextBox();
            btnExport = new Button();
            label1 = new Label();
            labOutput = new Label();
            txtRoute = new TextBox();
            btnRoute = new Button();
            label2 = new Label();
            txtFileName = new TextBox();
            SuspendLayout();
            // 
            // txtConnectionString
            // 
            txtConnectionString.Location = new Point(123, 8);
            txtConnectionString.Name = "txtConnectionString";
            txtConnectionString.Size = new Size(431, 23);
            txtConnectionString.TabIndex = 0;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(479, 37);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(75, 23);
            btnExport.TabIndex = 1;
            btnExport.Text = "匯出";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 16);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 2;
            label1.Text = "ConnectionString";
            // 
            // labOutput
            // 
            labOutput.AutoSize = true;
            labOutput.Location = new Point(15, 42);
            labOutput.Name = "labOutput";
            labOutput.Size = new Size(79, 15);
            labOutput.TabIndex = 3;
            labOutput.Text = "匯出檔案路徑";
            // 
            // txtRoute
            // 
            txtRoute.Location = new Point(93, 39);
            txtRoute.Name = "txtRoute";
            txtRoute.Size = new Size(265, 23);
            txtRoute.TabIndex = 4;
            // 
            // btnRoute
            // 
            btnRoute.Location = new Point(364, 37);
            btnRoute.Name = "btnRoute";
            btnRoute.Size = new Size(75, 23);
            btnRoute.TabIndex = 5;
            btnRoute.Text = "檔案路徑";
            btnRoute.UseVisualStyleBackColor = true;
            btnRoute.Click += btnRoute_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 82);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 6;
            label2.Text = "匯出檔案名稱";
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(94, 82);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(117, 23);
            txtFileName.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(566, 128);
            Controls.Add(txtFileName);
            Controls.Add(label2);
            Controls.Add(btnRoute);
            Controls.Add(txtRoute);
            Controls.Add(labOutput);
            Controls.Add(label1);
            Controls.Add(btnExport);
            Controls.Add(txtConnectionString);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtConnectionString;
        private Button btnExport;
        private Label label1;
        private Label labOutput;
        private TextBox txtRoute;
        private Button btnRoute;
        private Label label2;
        private TextBox txtFileName;
    }
}