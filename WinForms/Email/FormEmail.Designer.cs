namespace WinForms
{
    partial class FormEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEmail));
            this.textBoxPara = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonEnviar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCCo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAssunto = new System.Windows.Forms.TextBox();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxPara
            // 
            this.textBoxPara.Location = new System.Drawing.Point(110, 31);
            this.textBoxPara.Name = "textBoxPara";
            this.textBoxPara.Size = new System.Drawing.Size(476, 20);
            this.textBoxPara.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Para:";
            // 
            // buttonEnviar
            // 
            this.buttonEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonEnviar.FlatAppearance.BorderSize = 0;
            this.buttonEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEnviar.ForeColor = System.Drawing.Color.White;
            this.buttonEnviar.Location = new System.Drawing.Point(13, 12);
            this.buttonEnviar.Name = "buttonEnviar";
            this.buttonEnviar.Size = new System.Drawing.Size(91, 123);
            this.buttonEnviar.TabIndex = 0;
            this.buttonEnviar.Text = "ENVIAR";
            this.buttonEnviar.UseVisualStyleBackColor = false;
            this.buttonEnviar.Click += new System.EventHandler(this.ButtonEnviar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "CCo:";
            // 
            // textBoxCCo
            // 
            this.textBoxCCo.Location = new System.Drawing.Point(110, 115);
            this.textBoxCCo.Name = "textBoxCCo";
            this.textBoxCCo.Size = new System.Drawing.Size(476, 20);
            this.textBoxCCo.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "CC:";
            // 
            // textBoxCC
            // 
            this.textBoxCC.Location = new System.Drawing.Point(110, 73);
            this.textBoxCC.Name = "textBoxCC";
            this.textBoxCC.Size = new System.Drawing.Size(476, 20);
            this.textBoxCC.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Assunto:";
            // 
            // textBoxAssunto
            // 
            this.textBoxAssunto.Location = new System.Drawing.Point(12, 157);
            this.textBoxAssunto.Name = "textBoxAssunto";
            this.textBoxAssunto.Size = new System.Drawing.Size(574, 20);
            this.textBoxAssunto.TabIndex = 8;
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(12, 201);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(574, 342);
            this.textBoxMessage.TabIndex = 9;
            // 
            // FormEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 555);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxAssunto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxCC);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxCCo);
            this.Controls.Add(this.buttonEnviar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPara);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPara;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonEnviar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCCo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxAssunto;
        private System.Windows.Forms.TextBox textBoxMessage;
    }
}