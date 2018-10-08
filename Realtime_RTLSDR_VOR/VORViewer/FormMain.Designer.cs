namespace VORViewer
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnMapsCurrent = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLong = new System.Windows.Forms.TextBox();
            this.tbLat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnVORMaps = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.tbVORFreq = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbVORLong = new System.Windows.Forms.TextBox();
            this.tbVORLat = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbGNSSBearing = new System.Windows.Forms.TextBox();
            this.tbGNSSDistance = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbSignalLevel = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tbVORError = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbCalibrate = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbVORSD = new System.Windows.Forms.TextBox();
            this.tbVORAzimuth = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.pbCompass = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCompass)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbOutput
            // 
            this.rtbOutput.Location = new System.Drawing.Point(12, 504);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.ReadOnly = true;
            this.rtbOutput.Size = new System.Drawing.Size(1243, 276);
            this.rtbOutput.TabIndex = 1;
            this.rtbOutput.TabStop = false;
            this.rtbOutput.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnMapsCurrent);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbLong);
            this.groupBox1.Controls.Add(this.tbLat);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1243, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Location:";
            // 
            // btnMapsCurrent
            // 
            this.btnMapsCurrent.Location = new System.Drawing.Point(712, 37);
            this.btnMapsCurrent.Name = "btnMapsCurrent";
            this.btnMapsCurrent.Size = new System.Drawing.Size(161, 28);
            this.btnMapsCurrent.TabIndex = 6;
            this.btnMapsCurrent.Text = "Maps";
            this.btnMapsCurrent.UseVisualStyleBackColor = true;
            this.btnMapsCurrent.Click += new System.EventHandler(this.btnMapsCurrent_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(673, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "E";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(354, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "N";
            // 
            // tbLong
            // 
            this.tbLong.Location = new System.Drawing.Point(463, 40);
            this.tbLong.Name = "tbLong";
            this.tbLong.ReadOnly = true;
            this.tbLong.Size = new System.Drawing.Size(204, 22);
            this.tbLong.TabIndex = 3;
            this.tbLong.TabStop = false;
            // 
            // tbLat
            // 
            this.tbLat.Location = new System.Drawing.Point(109, 40);
            this.tbLat.Name = "tbLat";
            this.tbLat.ReadOnly = true;
            this.tbLat.Size = new System.Drawing.Size(239, 22);
            this.tbLat.TabIndex = 2;
            this.tbLat.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(394, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Latitude:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Latitude:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnVORMaps);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tbVORFreq);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tbVORLong);
            this.groupBox2.Controls.Add(this.tbVORLat);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(750, 127);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "VOR Ground Station:";
            // 
            // btnVORMaps
            // 
            this.btnVORMaps.Location = new System.Drawing.Point(342, 74);
            this.btnVORMaps.Name = "btnVORMaps";
            this.btnVORMaps.Size = new System.Drawing.Size(161, 28);
            this.btnVORMaps.TabIndex = 7;
            this.btnVORMaps.Text = "Maps";
            this.btnVORMaps.UseVisualStyleBackColor = true;
            this.btnVORMaps.Click += new System.EventHandler(this.btnVORMaps_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(283, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 17);
            this.label9.TabIndex = 8;
            this.label9.Text = "MHz";
            // 
            // tbVORFreq
            // 
            this.tbVORFreq.Location = new System.Drawing.Point(109, 82);
            this.tbVORFreq.Name = "tbVORFreq";
            this.tbVORFreq.Size = new System.Drawing.Size(171, 22);
            this.tbVORFreq.TabIndex = 7;
            this.tbVORFreq.TabStop = false;
            this.tbVORFreq.TextChanged += new System.EventHandler(this.tbVORFreq_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 17);
            this.label10.TabIndex = 6;
            this.label10.Text = "Frequency:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(585, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "E";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(284, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "N";
            // 
            // tbVORLong
            // 
            this.tbVORLong.Location = new System.Drawing.Point(408, 40);
            this.tbVORLong.Name = "tbVORLong";
            this.tbVORLong.Size = new System.Drawing.Size(171, 22);
            this.tbVORLong.TabIndex = 3;
            this.tbVORLong.TabStop = false;
            this.tbVORLong.TextChanged += new System.EventHandler(this.tbVORLong_TextChanged);
            // 
            // tbVORLat
            // 
            this.tbVORLat.Location = new System.Drawing.Point(109, 40);
            this.tbVORLat.Name = "tbVORLat";
            this.tbVORLat.Size = new System.Drawing.Size(170, 22);
            this.tbVORLat.TabIndex = 2;
            this.tbVORLat.TabStop = false;
            this.tbVORLat.TextChanged += new System.EventHandler(this.tbVORLat_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(339, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "Latitude:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "Latitude:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.tbGNSSBearing);
            this.groupBox3.Controls.Add(this.tbGNSSDistance);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Location = new System.Drawing.Point(12, 251);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(750, 100);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "GNSS Bearing:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(673, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 17);
            this.label11.TabIndex = 5;
            this.label11.Text = "degrees";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(354, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 17);
            this.label12.TabIndex = 4;
            this.label12.Text = "km";
            // 
            // tbGNSSBearing
            // 
            this.tbGNSSBearing.Location = new System.Drawing.Point(463, 40);
            this.tbGNSSBearing.Name = "tbGNSSBearing";
            this.tbGNSSBearing.ReadOnly = true;
            this.tbGNSSBearing.Size = new System.Drawing.Size(204, 22);
            this.tbGNSSBearing.TabIndex = 3;
            this.tbGNSSBearing.TabStop = false;
            // 
            // tbGNSSDistance
            // 
            this.tbGNSSDistance.Location = new System.Drawing.Point(109, 40);
            this.tbGNSSDistance.Name = "tbGNSSDistance";
            this.tbGNSSDistance.ReadOnly = true;
            this.tbGNSSDistance.Size = new System.Drawing.Size(239, 22);
            this.tbGNSSDistance.TabIndex = 2;
            this.tbGNSSDistance.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(394, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 17);
            this.label13.TabIndex = 1;
            this.label13.Text = "Azimuth:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(28, 40);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 17);
            this.label14.TabIndex = 0;
            this.label14.Text = "Distance:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.tbSignalLevel);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.tbVORError);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.cbCalibrate);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.tbVORSD);
            this.groupBox5.Controls.Add(this.tbVORAzimuth);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Location = new System.Drawing.Point(12, 357);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(750, 141);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "VOR Bearing:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(653, 40);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(36, 17);
            this.label17.TabIndex = 11;
            this.label17.Text = "dBm";
            // 
            // tbSignalLevel
            // 
            this.tbSignalLevel.Location = new System.Drawing.Point(477, 37);
            this.tbSignalLevel.Name = "tbSignalLevel";
            this.tbSignalLevel.ReadOnly = true;
            this.tbSignalLevel.Size = new System.Drawing.Size(170, 22);
            this.tbSignalLevel.TabIndex = 10;
            this.tbSignalLevel.TabStop = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(382, 40);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(89, 17);
            this.label18.TabIndex = 9;
            this.label18.Text = "Signal Level:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(261, 109);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 17);
            this.label16.TabIndex = 8;
            this.label16.Text = "degrees";
            // 
            // tbVORError
            // 
            this.tbVORError.Location = new System.Drawing.Point(109, 104);
            this.tbVORError.Name = "tbVORError";
            this.tbVORError.ReadOnly = true;
            this.tbVORError.Size = new System.Drawing.Size(146, 22);
            this.tbVORError.TabIndex = 7;
            this.tbVORError.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(28, 107);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 17);
            this.label15.TabIndex = 6;
            this.label15.Text = "Error:";
            // 
            // cbCalibrate
            // 
            this.cbCalibrate.AutoSize = true;
            this.cbCalibrate.Checked = true;
            this.cbCalibrate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCalibrate.Location = new System.Drawing.Point(397, 105);
            this.cbCalibrate.Name = "cbCalibrate";
            this.cbCalibrate.Size = new System.Drawing.Size(86, 21);
            this.cbCalibrate.TabIndex = 5;
            this.cbCalibrate.Text = "Calibrate";
            this.cbCalibrate.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(288, 40);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 17);
            this.label20.TabIndex = 4;
            this.label20.Text = "degrees";
            // 
            // tbVORSD
            // 
            this.tbVORSD.Location = new System.Drawing.Point(109, 68);
            this.tbVORSD.Name = "tbVORSD";
            this.tbVORSD.ReadOnly = true;
            this.tbVORSD.Size = new System.Drawing.Size(146, 22);
            this.tbVORSD.TabIndex = 3;
            this.tbVORSD.TabStop = false;
            // 
            // tbVORAzimuth
            // 
            this.tbVORAzimuth.Location = new System.Drawing.Point(109, 40);
            this.tbVORAzimuth.Name = "tbVORAzimuth";
            this.tbVORAzimuth.ReadOnly = true;
            this.tbVORAzimuth.Size = new System.Drawing.Size(170, 22);
            this.tbVORAzimuth.TabIndex = 2;
            this.tbVORAzimuth.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(28, 68);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(39, 17);
            this.label21.TabIndex = 1;
            this.label21.Text = "S.D.:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(28, 40);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(62, 17);
            this.label22.TabIndex = 0;
            this.label22.Text = "Azimuth:";
            // 
            // pbCompass
            // 
            this.pbCompass.BackColor = System.Drawing.Color.Black;
            this.pbCompass.Location = new System.Drawing.Point(768, 118);
            this.pbCompass.Name = "pbCompass";
            this.pbCompass.Size = new System.Drawing.Size(487, 380);
            this.pbCompass.TabIndex = 12;
            this.pbCompass.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1267, 792);
            this.Controls.Add(this.pbCompass);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rtbOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VOR Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCompass)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbLong;
        private System.Windows.Forms.TextBox tbLat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbVORFreq;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbVORLong;
        private System.Windows.Forms.TextBox tbVORLat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnMapsCurrent;
        private System.Windows.Forms.Button btnVORMaps;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbGNSSBearing;
        private System.Windows.Forms.TextBox tbGNSSDistance;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cbCalibrate;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbVORSD;
        private System.Windows.Forms.TextBox tbVORAzimuth;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbVORError;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbSignalLevel;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pbCompass;
    }
}

