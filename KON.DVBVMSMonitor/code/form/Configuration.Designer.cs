namespace KON.DVBVMSMonitor
{
    partial class frmConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfiguration));
            btnSave = new System.Windows.Forms.Button();
            lblLogLevel = new System.Windows.Forms.Label();
            cbLogLevel = new System.Windows.Forms.ComboBox();
            cbLogToConsole = new System.Windows.Forms.CheckBox();
            tbTargetServiceProcessName = new System.Windows.Forms.TextBox();
            lbTargetServiceProcessName = new System.Windows.Forms.Label();
            tbTargetServiceName = new System.Windows.Forms.TextBox();
            lbTargetServiceName = new System.Windows.Forms.Label();
            cbVerboseLogging = new System.Windows.Forms.CheckBox();
            nudTargetServiceStartDelay = new System.Windows.Forms.NumericUpDown();
            lbTargetServiceStartDelay = new System.Windows.Forms.Label();
            tbTargetServicePathWithFilename = new System.Windows.Forms.TextBox();
            lbTargetServicePathWithFilename = new System.Windows.Forms.Label();
            nudTargetServiceCheckInterval = new System.Windows.Forms.NumericUpDown();
            lbTargetServiceCheckInterval = new System.Windows.Forms.Label();
            gbWebService = new System.Windows.Forms.GroupBox();
            nudWebServiceCheckInterval = new System.Windows.Forms.NumericUpDown();
            lbWebServiceCheckInterval = new System.Windows.Forms.Label();
            nudWebServiceTimeout = new System.Windows.Forms.NumericUpDown();
            tbWebServiceUrl = new System.Windows.Forms.TextBox();
            lbWebServiceTimeout = new System.Windows.Forms.Label();
            lblWebServiceUrl = new System.Windows.Forms.Label();
            cbWebServiceEnabled = new System.Windows.Forms.CheckBox();
            gbForceBind = new System.Windows.Forms.GroupBox();
            tbForceBindIPAddress = new System.Windows.Forms.TextBox();
            lbForceBindIPAddress = new System.Windows.Forms.Label();
            tbForceBindPathWithFilename = new System.Windows.Forms.TextBox();
            lbForceBindPathWithFilename = new System.Windows.Forms.Label();
            cbForceBindEnabled = new System.Windows.Forms.CheckBox();
            nudTargetServiceTimeout = new System.Windows.Forms.NumericUpDown();
            lbTargetServiceTimeout = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)nudTargetServiceStartDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTargetServiceCheckInterval).BeginInit();
            gbWebService.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudWebServiceCheckInterval).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudWebServiceTimeout).BeginInit();
            gbForceBind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTargetServiceTimeout).BeginInit();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(493, 476);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(75, 23);
            btnSave.TabIndex = 4;
            btnSave.Text = "&Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lblLogLevel
            // 
            lblLogLevel.AutoSize = true;
            lblLogLevel.Location = new System.Drawing.Point(18, 416);
            lblLogLevel.Name = "lblLogLevel";
            lblLogLevel.Size = new System.Drawing.Size(60, 15);
            lblLogLevel.TabIndex = 9;
            lblLogLevel.Text = "Log Level:";
            // 
            // cbLogLevel
            // 
            cbLogLevel.FormattingEnabled = true;
            cbLogLevel.Items.AddRange(new object[] { "Error", "Warning", "Information" });
            cbLogLevel.Location = new System.Drawing.Point(208, 413);
            cbLogLevel.Name = "cbLogLevel";
            cbLogLevel.Size = new System.Drawing.Size(354, 23);
            cbLogLevel.TabIndex = 10;
            cbLogLevel.Text = "Warning";
            // 
            // cbLogToConsole
            // 
            cbLogToConsole.AutoSize = true;
            cbLogToConsole.Location = new System.Drawing.Point(208, 442);
            cbLogToConsole.Name = "cbLogToConsole";
            cbLogToConsole.Size = new System.Drawing.Size(104, 19);
            cbLogToConsole.TabIndex = 11;
            cbLogToConsole.Text = "Log to console";
            cbLogToConsole.UseVisualStyleBackColor = true;
            // 
            // tbTargetServiceProcessName
            // 
            tbTargetServiceProcessName.Location = new System.Drawing.Point(208, 70);
            tbTargetServiceProcessName.Name = "tbTargetServiceProcessName";
            tbTargetServiceProcessName.Size = new System.Drawing.Size(360, 23);
            tbTargetServiceProcessName.TabIndex = 15;
            tbTargetServiceProcessName.Text = "DVBVservice";
            // 
            // lbTargetServiceProcessName
            // 
            lbTargetServiceProcessName.AutoSize = true;
            lbTargetServiceProcessName.Location = new System.Drawing.Point(12, 73);
            lbTargetServiceProcessName.Name = "lbTargetServiceProcessName";
            lbTargetServiceProcessName.Size = new System.Drawing.Size(158, 15);
            lbTargetServiceProcessName.TabIndex = 14;
            lbTargetServiceProcessName.Text = "TargetService Process Name:";
            // 
            // tbTargetServiceName
            // 
            tbTargetServiceName.Location = new System.Drawing.Point(208, 41);
            tbTargetServiceName.Name = "tbTargetServiceName";
            tbTargetServiceName.Size = new System.Drawing.Size(360, 23);
            tbTargetServiceName.TabIndex = 17;
            tbTargetServiceName.Text = "DVBVRecorder";
            // 
            // lbTargetServiceName
            // 
            lbTargetServiceName.AutoSize = true;
            lbTargetServiceName.Location = new System.Drawing.Point(12, 44);
            lbTargetServiceName.Name = "lbTargetServiceName";
            lbTargetServiceName.Size = new System.Drawing.Size(115, 15);
            lbTargetServiceName.TabIndex = 16;
            lbTargetServiceName.Text = "TargetService Name:";
            // 
            // cbVerboseLogging
            // 
            cbVerboseLogging.AutoSize = true;
            cbVerboseLogging.Location = new System.Drawing.Point(448, 442);
            cbVerboseLogging.Name = "cbVerboseLogging";
            cbVerboseLogging.Size = new System.Drawing.Size(114, 19);
            cbVerboseLogging.TabIndex = 18;
            cbVerboseLogging.Text = "Verbose Logging";
            cbVerboseLogging.UseVisualStyleBackColor = true;
            // 
            // nudTargetServiceStartDelay
            // 
            nudTargetServiceStartDelay.Location = new System.Drawing.Point(208, 12);
            nudTargetServiceStartDelay.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
            nudTargetServiceStartDelay.Name = "nudTargetServiceStartDelay";
            nudTargetServiceStartDelay.Size = new System.Drawing.Size(55, 23);
            nudTargetServiceStartDelay.TabIndex = 20;
            // 
            // lbTargetServiceStartDelay
            // 
            lbTargetServiceStartDelay.AutoSize = true;
            lbTargetServiceStartDelay.Location = new System.Drawing.Point(12, 14);
            lbTargetServiceStartDelay.Name = "lbTargetServiceStartDelay";
            lbTargetServiceStartDelay.Size = new System.Drawing.Size(152, 15);
            lbTargetServiceStartDelay.TabIndex = 19;
            lbTargetServiceStartDelay.Text = "TargetService StartDelay (s):";
            // 
            // tbTargetServicePathWithFilename
            // 
            tbTargetServicePathWithFilename.Location = new System.Drawing.Point(208, 99);
            tbTargetServicePathWithFilename.Name = "tbTargetServicePathWithFilename";
            tbTargetServicePathWithFilename.Size = new System.Drawing.Size(360, 23);
            tbTargetServicePathWithFilename.TabIndex = 30;
            tbTargetServicePathWithFilename.Text = "C:\\Program Files (x86)\\DVBViewer\\DVBVservice.exe";
            // 
            // lbTargetServicePathWithFilename
            // 
            lbTargetServicePathWithFilename.AutoSize = true;
            lbTargetServicePathWithFilename.Location = new System.Drawing.Point(12, 102);
            lbTargetServicePathWithFilename.Name = "lbTargetServicePathWithFilename";
            lbTargetServicePathWithFilename.Size = new System.Drawing.Size(184, 15);
            lbTargetServicePathWithFilename.TabIndex = 29;
            lbTargetServicePathWithFilename.Text = "TargetService Path with Filename:";
            // 
            // nudTargetServiceCheckInterval
            // 
            nudTargetServiceCheckInterval.Location = new System.Drawing.Point(208, 128);
            nudTargetServiceCheckInterval.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            nudTargetServiceCheckInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTargetServiceCheckInterval.Name = "nudTargetServiceCheckInterval";
            nudTargetServiceCheckInterval.Size = new System.Drawing.Size(55, 23);
            nudTargetServiceCheckInterval.TabIndex = 32;
            nudTargetServiceCheckInterval.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lbTargetServiceCheckInterval
            // 
            lbTargetServiceCheckInterval.AutoSize = true;
            lbTargetServiceCheckInterval.Location = new System.Drawing.Point(12, 130);
            lbTargetServiceCheckInterval.Name = "lbTargetServiceCheckInterval";
            lbTargetServiceCheckInterval.Size = new System.Drawing.Size(171, 15);
            lbTargetServiceCheckInterval.TabIndex = 31;
            lbTargetServiceCheckInterval.Text = "TargetService CheckInterval (s):";
            // 
            // gbWebService
            // 
            gbWebService.Controls.Add(nudWebServiceCheckInterval);
            gbWebService.Controls.Add(lbWebServiceCheckInterval);
            gbWebService.Controls.Add(nudWebServiceTimeout);
            gbWebService.Controls.Add(tbWebServiceUrl);
            gbWebService.Controls.Add(lbWebServiceTimeout);
            gbWebService.Controls.Add(lblWebServiceUrl);
            gbWebService.Location = new System.Drawing.Point(12, 196);
            gbWebService.Name = "gbWebService";
            gbWebService.Size = new System.Drawing.Size(556, 116);
            gbWebService.TabIndex = 33;
            gbWebService.TabStop = false;
            gbWebService.Text = "WebService";
            // 
            // nudWebServiceCheckInterval
            // 
            nudWebServiceCheckInterval.Location = new System.Drawing.Point(196, 22);
            nudWebServiceCheckInterval.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            nudWebServiceCheckInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudWebServiceCheckInterval.Name = "nudWebServiceCheckInterval";
            nudWebServiceCheckInterval.Size = new System.Drawing.Size(55, 23);
            nudWebServiceCheckInterval.TabIndex = 19;
            nudWebServiceCheckInterval.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lbWebServiceCheckInterval
            // 
            lbWebServiceCheckInterval.AutoSize = true;
            lbWebServiceCheckInterval.Location = new System.Drawing.Point(6, 24);
            lbWebServiceCheckInterval.Name = "lbWebServiceCheckInterval";
            lbWebServiceCheckInterval.Size = new System.Drawing.Size(162, 15);
            lbWebServiceCheckInterval.TabIndex = 18;
            lbWebServiceCheckInterval.Text = "WebService CheckInterval (s):";
            // 
            // nudWebServiceTimeout
            // 
            nudWebServiceTimeout.Location = new System.Drawing.Point(196, 80);
            nudWebServiceTimeout.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            nudWebServiceTimeout.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudWebServiceTimeout.Name = "nudWebServiceTimeout";
            nudWebServiceTimeout.Size = new System.Drawing.Size(55, 23);
            nudWebServiceTimeout.TabIndex = 17;
            nudWebServiceTimeout.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // tbWebServiceUrl
            // 
            tbWebServiceUrl.Location = new System.Drawing.Point(196, 51);
            tbWebServiceUrl.Name = "tbWebServiceUrl";
            tbWebServiceUrl.Size = new System.Drawing.Size(354, 23);
            tbWebServiceUrl.TabIndex = 16;
            tbWebServiceUrl.Text = "http://localhost:8080/api/status2.html";
            // 
            // lbWebServiceTimeout
            // 
            lbWebServiceTimeout.AutoSize = true;
            lbWebServiceTimeout.Location = new System.Drawing.Point(6, 82);
            lbWebServiceTimeout.Name = "lbWebServiceTimeout";
            lbWebServiceTimeout.Size = new System.Drawing.Size(135, 15);
            lbWebServiceTimeout.TabIndex = 15;
            lbWebServiceTimeout.Text = "WebService Timeout (s):";
            // 
            // lblWebServiceUrl
            // 
            lblWebServiceUrl.AutoSize = true;
            lblWebServiceUrl.Location = new System.Drawing.Point(6, 54);
            lblWebServiceUrl.Name = "lblWebServiceUrl";
            lblWebServiceUrl.Size = new System.Drawing.Size(89, 15);
            lblWebServiceUrl.TabIndex = 14;
            lblWebServiceUrl.Text = "WebService Url:";
            // 
            // cbWebServiceEnabled
            // 
            cbWebServiceEnabled.AutoSize = true;
            cbWebServiceEnabled.Checked = true;
            cbWebServiceEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            cbWebServiceEnabled.Location = new System.Drawing.Point(497, 193);
            cbWebServiceEnabled.Name = "cbWebServiceEnabled";
            cbWebServiceEnabled.Size = new System.Drawing.Size(68, 19);
            cbWebServiceEnabled.TabIndex = 37;
            cbWebServiceEnabled.Text = "Enabled";
            cbWebServiceEnabled.UseVisualStyleBackColor = true;
            cbWebServiceEnabled.CheckedChanged += cbWebServiceEnabled_CheckedChanged;
            // 
            // gbForceBind
            // 
            gbForceBind.Controls.Add(tbForceBindIPAddress);
            gbForceBind.Controls.Add(lbForceBindIPAddress);
            gbForceBind.Controls.Add(tbForceBindPathWithFilename);
            gbForceBind.Controls.Add(lbForceBindPathWithFilename);
            gbForceBind.Location = new System.Drawing.Point(12, 318);
            gbForceBind.Name = "gbForceBind";
            gbForceBind.Size = new System.Drawing.Size(556, 89);
            gbForceBind.TabIndex = 34;
            gbForceBind.TabStop = false;
            gbForceBind.Text = "ForceBind";
            // 
            // tbForceBindIPAddress
            // 
            tbForceBindIPAddress.Location = new System.Drawing.Point(196, 51);
            tbForceBindIPAddress.Name = "tbForceBindIPAddress";
            tbForceBindIPAddress.Size = new System.Drawing.Size(354, 23);
            tbForceBindIPAddress.TabIndex = 30;
            tbForceBindIPAddress.Text = "127.0.0.1";
            // 
            // lbForceBindIPAddress
            // 
            lbForceBindIPAddress.AutoSize = true;
            lbForceBindIPAddress.Location = new System.Drawing.Point(6, 54);
            lbForceBindIPAddress.Name = "lbForceBindIPAddress";
            lbForceBindIPAddress.Size = new System.Drawing.Size(121, 15);
            lbForceBindIPAddress.TabIndex = 29;
            lbForceBindIPAddress.Text = "ForceBind IP Address:";
            // 
            // tbForceBindPathWithFilename
            // 
            tbForceBindPathWithFilename.Location = new System.Drawing.Point(196, 22);
            tbForceBindPathWithFilename.Name = "tbForceBindPathWithFilename";
            tbForceBindPathWithFilename.Size = new System.Drawing.Size(354, 23);
            tbForceBindPathWithFilename.TabIndex = 28;
            tbForceBindPathWithFilename.Text = "C:\\Program Files\\KON.DVBVMSMonitor\\ForceBindIP64.exe";
            // 
            // lbForceBindPathWithFilename
            // 
            lbForceBindPathWithFilename.AutoSize = true;
            lbForceBindPathWithFilename.Location = new System.Drawing.Point(6, 25);
            lbForceBindPathWithFilename.Name = "lbForceBindPathWithFilename";
            lbForceBindPathWithFilename.Size = new System.Drawing.Size(167, 15);
            lbForceBindPathWithFilename.TabIndex = 27;
            lbForceBindPathWithFilename.Text = "ForceBind Path with Filename:";
            // 
            // cbForceBindEnabled
            // 
            cbForceBindEnabled.AutoSize = true;
            cbForceBindEnabled.Location = new System.Drawing.Point(497, 315);
            cbForceBindEnabled.Name = "cbForceBindEnabled";
            cbForceBindEnabled.Size = new System.Drawing.Size(68, 19);
            cbForceBindEnabled.TabIndex = 37;
            cbForceBindEnabled.Text = "Enabled";
            cbForceBindEnabled.UseVisualStyleBackColor = true;
            cbForceBindEnabled.CheckedChanged += cbForceBindEnabled_CheckedChanged;
            // 
            // nudTargetServiceTimeout
            // 
            nudTargetServiceTimeout.Location = new System.Drawing.Point(208, 157);
            nudTargetServiceTimeout.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            nudTargetServiceTimeout.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTargetServiceTimeout.Name = "nudTargetServiceTimeout";
            nudTargetServiceTimeout.Size = new System.Drawing.Size(55, 23);
            nudTargetServiceTimeout.TabIndex = 36;
            nudTargetServiceTimeout.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lbTargetServiceTimeout
            // 
            lbTargetServiceTimeout.AutoSize = true;
            lbTargetServiceTimeout.Location = new System.Drawing.Point(12, 159);
            lbTargetServiceTimeout.Name = "lbTargetServiceTimeout";
            lbTargetServiceTimeout.Size = new System.Drawing.Size(144, 15);
            lbTargetServiceTimeout.TabIndex = 35;
            lbTargetServiceTimeout.Text = "TargetService Timeout (s):";
            // 
            // frmConfiguration
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(586, 513);
            Controls.Add(cbForceBindEnabled);
            Controls.Add(cbWebServiceEnabled);
            Controls.Add(nudTargetServiceTimeout);
            Controls.Add(lbTargetServiceTimeout);
            Controls.Add(gbForceBind);
            Controls.Add(gbWebService);
            Controls.Add(nudTargetServiceCheckInterval);
            Controls.Add(lbTargetServiceCheckInterval);
            Controls.Add(tbTargetServicePathWithFilename);
            Controls.Add(lbTargetServicePathWithFilename);
            Controls.Add(nudTargetServiceStartDelay);
            Controls.Add(lbTargetServiceStartDelay);
            Controls.Add(cbVerboseLogging);
            Controls.Add(tbTargetServiceName);
            Controls.Add(lbTargetServiceName);
            Controls.Add(tbTargetServiceProcessName);
            Controls.Add(lbTargetServiceProcessName);
            Controls.Add(cbLogToConsole);
            Controls.Add(cbLogLevel);
            Controls.Add(lblLogLevel);
            Controls.Add(btnSave);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmConfiguration";
            Text = "DVBMSMonitor Configuration";
            Load += Configuration_Load;
            ((System.ComponentModel.ISupportInitialize)nudTargetServiceStartDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTargetServiceCheckInterval).EndInit();
            gbWebService.ResumeLayout(false);
            gbWebService.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudWebServiceCheckInterval).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudWebServiceTimeout).EndInit();
            gbForceBind.ResumeLayout(false);
            gbForceBind.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudTargetServiceTimeout).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblLogLevel;
        private System.Windows.Forms.ComboBox cbLogLevel;
        private System.Windows.Forms.CheckBox cbLogToConsole;
        private System.Windows.Forms.TextBox tbTargetServiceProcessName;
        private System.Windows.Forms.Label lbTargetServiceProcessName;
        private System.Windows.Forms.TextBox tbTargetServiceName;
        private System.Windows.Forms.Label lbTargetServiceName;
        private System.Windows.Forms.CheckBox cbVerboseLogging;
        private System.Windows.Forms.NumericUpDown nudTargetServiceStartDelay;
        private System.Windows.Forms.Label lbTargetServiceStartDelay;
        private System.Windows.Forms.TextBox tbTargetServicePathWithFilename;
        private System.Windows.Forms.Label lbTargetServicePathWithFilename;
        private System.Windows.Forms.NumericUpDown nudTargetServiceCheckInterval;
        private System.Windows.Forms.Label lbTargetServiceCheckInterval;
        private System.Windows.Forms.GroupBox gbWebService;
        private System.Windows.Forms.NumericUpDown nudWebServiceCheckInterval;
        private System.Windows.Forms.Label lbWebServiceCheckInterval;
        private System.Windows.Forms.NumericUpDown nudWebServiceTimeout;
        private System.Windows.Forms.TextBox tbWebServiceUrl;
        private System.Windows.Forms.Label lbWebServiceTimeout;
        private System.Windows.Forms.Label lblWebServiceUrl;
        private System.Windows.Forms.GroupBox gbForceBind;
        private System.Windows.Forms.TextBox tbForceBindIPAddress;
        private System.Windows.Forms.Label lbForceBindIPAddress;
        private System.Windows.Forms.TextBox tbForceBindPathWithFilename;
        private System.Windows.Forms.Label lbForceBindPathWithFilename;
        private System.Windows.Forms.NumericUpDown nudTargetServiceTimeout;
        private System.Windows.Forms.Label lbTargetServiceTimeout;
        private System.Windows.Forms.CheckBox cbWebServiceEnabled;
        private System.Windows.Forms.CheckBox cbForceBindEnabled;
    }
}