namespace DkS3ModSwitcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.radioAshes = new System.Windows.Forms.RadioButton();
            this.radioNone = new System.Windows.Forms.RadioButton();
            this.radioCinders = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.stateLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(121, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Switch Mod";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radioAshes
            // 
            this.radioAshes.AutoSize = true;
            this.radioAshes.Location = new System.Drawing.Point(13, 13);
            this.radioAshes.Name = "radioAshes";
            this.radioAshes.Size = new System.Drawing.Size(54, 17);
            this.radioAshes.TabIndex = 1;
            this.radioAshes.TabStop = true;
            this.radioAshes.Text = "Ashes";
            this.radioAshes.UseVisualStyleBackColor = true;
            // 
            // radioNone
            // 
            this.radioNone.AutoSize = true;
            this.radioNone.Location = new System.Drawing.Point(12, 59);
            this.radioNone.Name = "radioNone";
            this.radioNone.Size = new System.Drawing.Size(62, 17);
            this.radioNone.TabIndex = 2;
            this.radioNone.TabStop = true;
            this.radioNone.Text = "No mod";
            this.radioNone.UseVisualStyleBackColor = true;
            // 
            // radioCinders
            // 
            this.radioCinders.AutoSize = true;
            this.radioCinders.Location = new System.Drawing.Point(12, 36);
            this.radioCinders.Name = "radioCinders";
            this.radioCinders.Size = new System.Drawing.Size(60, 17);
            this.radioCinders.TabIndex = 3;
            this.radioCinders.TabStop = true;
            this.radioCinders.Text = "Cinders";
            this.radioCinders.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Current: ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Location = new System.Drawing.Point(147, 17);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(0, 13);
            this.stateLabel.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 98);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioCinders);
            this.Controls.Add(this.radioNone);
            this.Controls.Add(this.radioAshes);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Mod Switch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioAshes;
        private System.Windows.Forms.RadioButton radioNone;
        private System.Windows.Forms.RadioButton radioCinders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label stateLabel;
    }
}

