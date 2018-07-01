namespace 激光扫描雷达
{
    partial class LaserScannerForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.label1 = new System.Windows.Forms.Label();
            this.iptxt = new System.Windows.Forms.TextBox();
            this.pointtxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.num1 = new System.Windows.Forms.NumericUpDown();
            this.txtBoxMSG = new System.Windows.Forms.TextBox();
            this.errorLab = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.errorTxt = new System.Windows.Forms.TextBox();
            this.txtBoxAllReceive = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nearTxt = new System.Windows.Forms.TextBox();
            this.farTxt = new System.Windows.Forms.TextBox();
            this.num2 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ysuddenchangeTxt = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.downpointTxt = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.alarmTxt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.num1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "端口号:";
            // 
            // iptxt
            // 
            this.iptxt.Location = new System.Drawing.Point(203, 27);
            this.iptxt.Name = "iptxt";
            this.iptxt.Size = new System.Drawing.Size(100, 21);
            this.iptxt.TabIndex = 27;
            this.iptxt.Text = "192.168.1.254";
            // 
            // pointtxt
            // 
            this.pointtxt.Location = new System.Drawing.Point(88, 27);
            this.pointtxt.Name = "pointtxt";
            this.pointtxt.Size = new System.Drawing.Size(38, 21);
            this.pointtxt.TabIndex = 28;
            this.pointtxt.Text = "4001";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 29;
            this.label2.Text = "IP地址:";
            // 
            // num1
            // 
            this.num1.Location = new System.Drawing.Point(379, 27);
            this.num1.Maximum = new decimal(new int[] {
            273,
            0,
            0,
            0});
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(54, 21);
            this.num1.TabIndex = 30;
            this.num1.Value = new decimal(new int[] {
            229,
            0,
            0,
            0});
            // 
            // txtBoxMSG
            // 
            this.txtBoxMSG.Location = new System.Drawing.Point(24, 106);
            this.txtBoxMSG.Multiline = true;
            this.txtBoxMSG.Name = "txtBoxMSG";
            this.txtBoxMSG.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBoxMSG.Size = new System.Drawing.Size(867, 110);
            this.txtBoxMSG.TabIndex = 0;
            // 
            // errorLab
            // 
            this.errorLab.AutoSize = true;
            this.errorLab.Location = new System.Drawing.Point(664, 34);
            this.errorLab.Name = "errorLab";
            this.errorLab.Size = new System.Drawing.Size(11, 12);
            this.errorLab.TabIndex = 31;
            this.errorLab.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(320, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 32;
            this.label3.Text = "中心点：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(593, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "实际误差：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(459, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 34;
            this.label5.Text = "允许误差：";
            // 
            // errorTxt
            // 
            this.errorTxt.Location = new System.Drawing.Point(531, 28);
            this.errorTxt.Name = "errorTxt";
            this.errorTxt.Size = new System.Drawing.Size(41, 21);
            this.errorTxt.TabIndex = 35;
            this.errorTxt.Text = "30";
            // 
            // txtBoxAllReceive
            // 
            this.txtBoxAllReceive.Location = new System.Drawing.Point(6, 17);
            this.txtBoxAllReceive.Multiline = true;
            this.txtBoxAllReceive.Name = "txtBoxAllReceive";
            this.txtBoxAllReceive.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBoxAllReceive.Size = new System.Drawing.Size(231, 280);
            this.txtBoxAllReceive.TabIndex = 0;
            // 
            // chart1
            // 
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            this.chart1.Location = new System.Drawing.Point(243, 21);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(625, 276);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chart1);
            this.groupBox5.Controls.Add(this.txtBoxAllReceive);
            this.groupBox5.Location = new System.Drawing.Point(12, 222);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(879, 305);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Receive";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 36;
            this.label6.Text = "校准近端起点：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(214, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 37;
            this.label7.Text = "校准远端起点：";
            // 
            // nearTxt
            // 
            this.nearTxt.Location = new System.Drawing.Point(130, 68);
            this.nearTxt.Name = "nearTxt";
            this.nearTxt.Size = new System.Drawing.Size(56, 21);
            this.nearTxt.TabIndex = 38;
            this.nearTxt.Text = "50";
            // 
            // farTxt
            // 
            this.farTxt.Location = new System.Drawing.Point(309, 68);
            this.farTxt.Name = "farTxt";
            this.farTxt.Size = new System.Drawing.Size(56, 21);
            this.farTxt.TabIndex = 39;
            this.farTxt.Text = "200";
            // 
            // num2
            // 
            this.num2.Location = new System.Drawing.Point(760, 28);
            this.num2.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.num2.Name = "num2";
            this.num2.Size = new System.Drawing.Size(50, 21);
            this.num2.TabIndex = 40;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(701, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 41;
            this.label8.Text = "偏转角：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(374, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 44;
            this.label10.Text = "y突变值：";
            // 
            // ysuddenchangeTxt
            // 
            this.ysuddenchangeTxt.Location = new System.Drawing.Point(439, 68);
            this.ysuddenchangeTxt.Name = "ysuddenchangeTxt";
            this.ysuddenchangeTxt.Size = new System.Drawing.Size(54, 21);
            this.ysuddenchangeTxt.TabIndex = 45;
            this.ysuddenchangeTxt.Text = "300";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(507, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 46;
            this.label11.Text = "底板点数：";
            // 
            // downpointTxt
            // 
            this.downpointTxt.Location = new System.Drawing.Point(578, 68);
            this.downpointTxt.Name = "downpointTxt";
            this.downpointTxt.Size = new System.Drawing.Size(38, 21);
            this.downpointTxt.TabIndex = 47;
            this.downpointTxt.Text = "5";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(626, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(161, 12);
            this.label13.TabIndex = 49;
            this.label13.Text = "报警距离（前挡板距雷达）：";
            // 
            // alarmTxt
            // 
            this.alarmTxt.Location = new System.Drawing.Point(793, 68);
            this.alarmTxt.Name = "alarmTxt";
            this.alarmTxt.Size = new System.Drawing.Size(49, 21);
            this.alarmTxt.TabIndex = 50;
            this.alarmTxt.Text = "5270";
            // 
            // LaserScannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 524);
            this.Controls.Add(this.alarmTxt);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.downpointTxt);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ysuddenchangeTxt);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.num2);
            this.Controls.Add(this.farTxt);
            this.Controls.Add(this.nearTxt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtBoxMSG);
            this.Controls.Add(this.errorTxt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.errorLab);
            this.Controls.Add(this.num1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pointtxt);
            this.Controls.Add(this.iptxt);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "LaserScannerForm";
            this.Text = "LaserScannerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LaserScannerForm_FormClosing);
            this.Load += new System.EventHandler(this.LaserScannerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox iptxt;
        private System.Windows.Forms.TextBox pointtxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown num1;
        private System.Windows.Forms.TextBox txtBoxMSG;
        private System.Windows.Forms.Label errorLab;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox errorTxt;
        private System.Windows.Forms.TextBox txtBoxAllReceive;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox nearTxt;
        private System.Windows.Forms.TextBox farTxt;
        private System.Windows.Forms.NumericUpDown num2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ysuddenchangeTxt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox downpointTxt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox alarmTxt;

    }
}

