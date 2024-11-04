namespace PixApplication
{
    partial class FrmGerarPix
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
            this.txtValor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGerar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureQRCode = new System.Windows.Forms.PictureBox();
            this.btn_Autenticar = new System.Windows.Forms.Button();
            this.checkAuthentication = new System.Windows.Forms.CheckBox();
            this.btnConfiguracao = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(179, 61);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(106, 20);
            this.txtValor.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Valor:";
            // 
            // btnGerar
            // 
            this.btnGerar.Location = new System.Drawing.Point(315, 54);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(63, 28);
            this.btnGerar.TabIndex = 4;
            this.btnGerar.Text = "&Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "---";
            // 
            // pictureQRCode
            // 
            this.pictureQRCode.Location = new System.Drawing.Point(42, 97);
            this.pictureQRCode.Name = "pictureQRCode";
            this.pictureQRCode.Size = new System.Drawing.Size(297, 247);
            this.pictureQRCode.TabIndex = 8;
            this.pictureQRCode.TabStop = false;
            // 
            // btn_Autenticar
            // 
            this.btn_Autenticar.Location = new System.Drawing.Point(315, 17);
            this.btn_Autenticar.Name = "btn_Autenticar";
            this.btn_Autenticar.Size = new System.Drawing.Size(63, 28);
            this.btn_Autenticar.TabIndex = 9;
            this.btn_Autenticar.Text = "Autenticar";
            this.btn_Autenticar.UseVisualStyleBackColor = true;
            this.btn_Autenticar.Click += new System.EventHandler(this.btn_Autenticar_Click);
            // 
            // checkAuthentication
            // 
            this.checkAuthentication.AutoSize = true;
            this.checkAuthentication.Location = new System.Drawing.Point(288, 24);
            this.checkAuthentication.Name = "checkAuthentication";
            this.checkAuthentication.Size = new System.Drawing.Size(15, 14);
            this.checkAuthentication.TabIndex = 10;
            this.checkAuthentication.UseVisualStyleBackColor = true;
            // 
            // btnConfiguracao
            // 
            this.btnConfiguracao.Location = new System.Drawing.Point(30, 58);
            this.btnConfiguracao.Name = "btnConfiguracao";
            this.btnConfiguracao.Size = new System.Drawing.Size(80, 23);
            this.btnConfiguracao.TabIndex = 11;
            this.btnConfiguracao.Text = "Configuração";
            this.btnConfiguracao.UseVisualStyleBackColor = true;
            this.btnConfiguracao.Click += new System.EventHandler(this.btnConfiguracao_Click);
            // 
            // FrmGerarPix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 380);
            this.Controls.Add(this.btnConfiguracao);
            this.Controls.Add(this.checkAuthentication);
            this.Controls.Add(this.btn_Autenticar);
            this.Controls.Add(this.pictureQRCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtValor);
            this.Name = "FrmGerarPix";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureQRCode;
        private System.Windows.Forms.Button btn_Autenticar;
        private System.Windows.Forms.CheckBox checkAuthentication;
        private System.Windows.Forms.Button btnConfiguracao;
    }
}

