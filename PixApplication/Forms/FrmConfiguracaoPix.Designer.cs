namespace PixApplication
{
    partial class FrmConfiguracaoPix
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
            this.txtIdentificacao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtChavePix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Salvar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtExpirePix = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtIdentificacao
            // 
            this.txtIdentificacao.Location = new System.Drawing.Point(116, 39);
            this.txtIdentificacao.Name = "txtIdentificacao";
            this.txtIdentificacao.Size = new System.Drawing.Size(114, 20);
            this.txtIdentificacao.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CPF / CNPJ Devedor:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(116, 79);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(114, 20);
            this.txtName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nome Devedor:";
            // 
            // txtChavePix
            // 
            this.txtChavePix.Location = new System.Drawing.Point(297, 39);
            this.txtChavePix.Name = "txtChavePix";
            this.txtChavePix.Size = new System.Drawing.Size(114, 20);
            this.txtChavePix.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(236, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Chave Pix";
            // 
            // btn_Salvar
            // 
            this.btn_Salvar.Location = new System.Drawing.Point(336, 115);
            this.btn_Salvar.Name = "btn_Salvar";
            this.btn_Salvar.Size = new System.Drawing.Size(75, 23);
            this.btn_Salvar.TabIndex = 6;
            this.btn_Salvar.Text = "Salvar";
            this.btn_Salvar.UseVisualStyleBackColor = true;
            this.btn_Salvar.Click += new System.EventHandler(this.btn_Salvar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(236, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Expira Pix:";
            // 
            // txtExpirePix
            // 
            this.txtExpirePix.Location = new System.Drawing.Point(297, 79);
            this.txtExpirePix.Name = "txtExpirePix";
            this.txtExpirePix.Size = new System.Drawing.Size(114, 20);
            this.txtExpirePix.TabIndex = 7;
            // 
            // FrmConfiguracaoPix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 150);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtExpirePix);
            this.Controls.Add(this.btn_Salvar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtChavePix);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIdentificacao);
            this.Name = "FrmConfiguracaoPix";
            this.Text = "Configuração Pix";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIdentificacao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtChavePix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Salvar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtExpirePix;
    }
}