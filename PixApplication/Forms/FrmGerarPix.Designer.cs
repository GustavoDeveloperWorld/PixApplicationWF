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
            this.components = new System.ComponentModel.Container();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGerar = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.Label();
            this.pictureQRCode = new System.Windows.Forms.PictureBox();
            this.btn_Autenticar = new System.Windows.Forms.Button();
            this.checkAuthentication = new System.Windows.Forms.CheckBox();
            this.btnConfiguracao = new System.Windows.Forms.Button();
            this.pixCopiaCola = new System.Windows.Forms.Label();
            this.timerExpirePix = new System.Windows.Forms.Timer(this.components);
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnPDF = new System.Windows.Forms.Button();
            this.btnPedido = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPedido = new System.Windows.Forms.ComboBox();
            this.pedidoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureQRCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pedidoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(279, 52);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(108, 20);
            this.txtValor.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(233, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Valor:";
            // 
            // btnGerar
            // 
            this.btnGerar.Location = new System.Drawing.Point(444, 50);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(63, 28);
            this.btnGerar.TabIndex = 4;
            this.btnGerar.Text = "&Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(27, 17);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(16, 13);
            this.status.TabIndex = 5;
            this.status.Text = "---";
            this.status.Click += new System.EventHandler(this.status_Click);
            // 
            // pictureQRCode
            // 
            this.pictureQRCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureQRCode.Location = new System.Drawing.Point(12, 97);
            this.pictureQRCode.Name = "pictureQRCode";
            this.pictureQRCode.Size = new System.Drawing.Size(495, 321);
            this.pictureQRCode.TabIndex = 8;
            this.pictureQRCode.TabStop = false;
            // 
            // btn_Autenticar
            // 
            this.btn_Autenticar.Location = new System.Drawing.Point(444, 13);
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
            this.checkAuthentication.Enabled = false;
            this.checkAuthentication.Location = new System.Drawing.Point(423, 21);
            this.checkAuthentication.Name = "checkAuthentication";
            this.checkAuthentication.Size = new System.Drawing.Size(15, 14);
            this.checkAuthentication.TabIndex = 10;
            this.checkAuthentication.UseVisualStyleBackColor = true;
            // 
            // btnConfiguracao
            // 
            this.btnConfiguracao.Location = new System.Drawing.Point(12, 52);
            this.btnConfiguracao.Name = "btnConfiguracao";
            this.btnConfiguracao.Size = new System.Drawing.Size(80, 23);
            this.btnConfiguracao.TabIndex = 11;
            this.btnConfiguracao.Text = "Configuração";
            this.btnConfiguracao.UseVisualStyleBackColor = true;
            this.btnConfiguracao.Click += new System.EventHandler(this.btnConfiguracao_Click);
            // 
            // pixCopiaCola
            // 
            this.pixCopiaCola.AutoSize = true;
            this.pixCopiaCola.Location = new System.Drawing.Point(27, 421);
            this.pixCopiaCola.MaximumSize = new System.Drawing.Size(400, 400);
            this.pixCopiaCola.Name = "pixCopiaCola";
            this.pixCopiaCola.Size = new System.Drawing.Size(16, 13);
            this.pixCopiaCola.TabIndex = 12;
            this.pixCopiaCola.Text = "---";
            // 
            // timerExpirePix
            // 
            this.timerExpirePix.Tick += new System.EventHandler(this.timerExpirePix_Tick);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(30, 499);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(440, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 15;
            // 
            // btnPDF
            // 
            this.btnPDF.Location = new System.Drawing.Point(116, 52);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(75, 23);
            this.btnPDF.TabIndex = 16;
            this.btnPDF.Text = "PDF";
            this.btnPDF.UseVisualStyleBackColor = true;
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // btnPedido
            // 
            this.btnPedido.Location = new System.Drawing.Point(116, 16);
            this.btnPedido.Name = "btnPedido";
            this.btnPedido.Size = new System.Drawing.Size(75, 23);
            this.btnPedido.TabIndex = 17;
            this.btnPedido.Text = "Pedido";
            this.btnPedido.UseVisualStyleBackColor = true;
            this.btnPedido.Click += new System.EventHandler(this.btnPedido_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(222, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "Pedido:";
            // 
            // cmbPedido
            // 
            this.cmbPedido.DataSource = this.pedidoBindingSource;
            this.cmbPedido.DisplayMember = "NumeroPedido";
            this.cmbPedido.FormattingEnabled = true;
            this.cmbPedido.Location = new System.Drawing.Point(281, 18);
            this.cmbPedido.Name = "cmbPedido";
            this.cmbPedido.Size = new System.Drawing.Size(106, 21);
            this.cmbPedido.TabIndex = 19;
            this.cmbPedido.ValueMember = "NumeroPedido";
            // 
            // pedidoBindingSource
            // 
            this.pedidoBindingSource.DataSource = typeof(PixApplication.Model.Pedido);
            // 
            // FrmGerarPix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 534);
            this.Controls.Add(this.cmbPedido);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnPedido);
            this.Controls.Add(this.btnPDF);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pixCopiaCola);
            this.Controls.Add(this.btnConfiguracao);
            this.Controls.Add(this.checkAuthentication);
            this.Controls.Add(this.btn_Autenticar);
            this.Controls.Add(this.pictureQRCode);
            this.Controls.Add(this.status);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtValor);
            this.Name = "FrmGerarPix";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureQRCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pedidoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.PictureBox pictureQRCode;
        private System.Windows.Forms.Button btn_Autenticar;
        private System.Windows.Forms.CheckBox checkAuthentication;
        private System.Windows.Forms.Button btnConfiguracao;
        private System.Windows.Forms.Label pixCopiaCola;
        private System.Windows.Forms.Timer timerExpirePix;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnPDF;
        private System.Windows.Forms.Button btnPedido;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPedido;
        private System.Windows.Forms.BindingSource pedidoBindingSource;
    }
}

