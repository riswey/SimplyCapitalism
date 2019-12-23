namespace SimpleCapitalism
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dividendperc = new System.Windows.Forms.NumericUpDown();
            this.Label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtPopulation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numCapitalists = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.marketCap = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numCompany = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numAgent = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numGrowth = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numStartCash = new System.Windows.Forms.NumericUpDown();
            this.chkRandom = new System.Windows.Forms.CheckBox();
            this.tentimescStart = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dividendperc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCapitalists)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAgent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGrowth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartCash)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(12, 13);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(617, 240);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // dividendperc
            // 
            this.dividendperc.DecimalPlaces = 2;
            this.dividendperc.Location = new System.Drawing.Point(659, 171);
            this.dividendperc.Name = "dividendperc";
            this.dividendperc.Size = new System.Drawing.Size(60, 20);
            this.dividendperc.TabIndex = 1;
            this.dividendperc.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(658, 152);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(60, 13);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "Dividend %";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(658, 309);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPopulation
            // 
            this.txtPopulation.Location = new System.Drawing.Point(715, 357);
            this.txtPopulation.Name = "txtPopulation";
            this.txtPopulation.Size = new System.Drawing.Size(55, 20);
            this.txtPopulation.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(652, 360);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Population";
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            this.chart2.Location = new System.Drawing.Point(12, 273);
            this.chart2.Name = "chart2";
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(617, 240);
            this.chart2.TabIndex = 8;
            this.chart2.Text = "chart2";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(704, 309);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(38, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(656, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Number Capitalists";
            // 
            // numCapitalists
            // 
            this.numCapitalists.Location = new System.Drawing.Point(657, 222);
            this.numCapitalists.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numCapitalists.Name = "numCapitalists";
            this.numCapitalists.Size = new System.Drawing.Size(62, 20);
            this.numCapitalists.TabIndex = 10;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(748, 309);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(38, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Cont";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(652, 395);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Market Cap";
            // 
            // marketCap
            // 
            this.marketCap.Location = new System.Drawing.Point(715, 392);
            this.marketCap.Name = "marketCap";
            this.marketCap.Size = new System.Drawing.Size(55, 20);
            this.marketCap.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(656, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Number Companies";
            // 
            // numCompany
            // 
            this.numCompany.Location = new System.Drawing.Point(657, 29);
            this.numCompany.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numCompany.Name = "numCompany";
            this.numCompany.Size = new System.Drawing.Size(62, 20);
            this.numCompany.TabIndex = 15;
            this.numCompany.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(656, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Number Agents";
            // 
            // numAgent
            // 
            this.numAgent.Location = new System.Drawing.Point(657, 79);
            this.numAgent.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numAgent.Name = "numAgent";
            this.numAgent.Size = new System.Drawing.Size(62, 20);
            this.numAgent.TabIndex = 17;
            this.numAgent.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(656, 254);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Growth %";
            // 
            // numGrowth
            // 
            this.numGrowth.DecimalPlaces = 2;
            this.numGrowth.Location = new System.Drawing.Point(657, 273);
            this.numGrowth.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numGrowth.Name = "numGrowth";
            this.numGrowth.Size = new System.Drawing.Size(60, 20);
            this.numGrowth.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(656, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Start Cash";
            // 
            // numStartCash
            // 
            this.numStartCash.Location = new System.Drawing.Point(657, 125);
            this.numStartCash.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numStartCash.Name = "numStartCash";
            this.numStartCash.Size = new System.Drawing.Size(62, 20);
            this.numStartCash.TabIndex = 21;
            this.numStartCash.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // chkRandom
            // 
            this.chkRandom.AutoSize = true;
            this.chkRandom.Location = new System.Drawing.Point(727, 125);
            this.chkRandom.Name = "chkRandom";
            this.chkRandom.Size = new System.Drawing.Size(61, 17);
            this.chkRandom.TabIndex = 23;
            this.chkRandom.Text = "random";
            this.chkRandom.UseVisualStyleBackColor = true;
            // 
            // tenpercStart
            // 
            this.tentimescStart.AutoSize = true;
            this.tentimescStart.Location = new System.Drawing.Point(727, 222);
            this.tentimescStart.Name = "tenpercStart";
            this.tentimescStart.Size = new System.Drawing.Size(66, 17);
            this.tentimescStart.TabIndex = 24;
            this.tentimescStart.Text = "10x lead";
            this.tentimescStart.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 531);
            this.Controls.Add(this.tentimescStart);
            this.Controls.Add(this.chkRandom);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numStartCash);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numGrowth);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numAgent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numCompany);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.marketCap);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numCapitalists);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPopulation);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.dividendperc);
            this.Controls.Add(this.chart1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dividendperc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCapitalists)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAgent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGrowth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartCash)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.NumericUpDown dividendperc;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtPopulation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numCapitalists;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox marketCap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numCompany;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numAgent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numGrowth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numStartCash;
        private System.Windows.Forms.CheckBox chkRandom;
        private System.Windows.Forms.CheckBox tentimescStart;
    }
}

