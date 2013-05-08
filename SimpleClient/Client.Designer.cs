namespace SimpleClient
{
    partial class Client
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
            this.btnSend = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.Input = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(301, 9);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 59);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnConnect.Location = new System.Drawing.Point(6, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 58);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // Input
            // 
            this.Input.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Input.Location = new System.Drawing.Point(6, 76);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(370, 20);
            this.Input.TabIndex = 2;
            this.Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // lblIP
            // 
            this.lblIP.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(96, 25);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(51, 13);
            this.lblIP.TabIndex = 3;
            this.lblIP.Text = "Server IP";
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtAddress.Location = new System.Drawing.Point(99, 39);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(169, 20);
            this.txtAddress.TabIndex = 4;
            this.txtAddress.Text = "127.0.0.1";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 102);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnSend);
            this.Name = "ClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox Input;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox txtAddress;
    }
}

