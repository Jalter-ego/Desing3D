
namespace Diseño3D
{
    partial class MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Run = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RBMenos = new System.Windows.Forms.RadioButton();
            this.RBMas = new System.Windows.Forms.RadioButton();
            this.checkZ = new System.Windows.Forms.CheckBox();
            this.checkY = new System.Windows.Forms.CheckBox();
            this.checkX = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkEscalacion = new System.Windows.Forms.CheckBox();
            this.checkTraslacion = new System.Windows.Forms.CheckBox();
            this.checkRotacion = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.Run);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(783, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 560);
            this.panel1.TabIndex = 0;
            // 
            // Run
            // 
            this.Run.Enabled = false;
            this.Run.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Run.Location = new System.Drawing.Point(70, 471);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(70, 27);
            this.Run.TabIndex = 8;
            this.Run.Text = "RUN";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RBMenos);
            this.groupBox3.Controls.Add(this.RBMas);
            this.groupBox3.Controls.Add(this.checkZ);
            this.groupBox3.Controls.Add(this.checkY);
            this.groupBox3.Controls.Add(this.checkX);
            this.groupBox3.Location = new System.Drawing.Point(3, 323);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(194, 100);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ejes";
            // 
            // RBMenos
            // 
            this.RBMenos.AutoSize = true;
            this.RBMenos.Location = new System.Drawing.Point(116, 73);
            this.RBMenos.Name = "RBMenos";
            this.RBMenos.Size = new System.Drawing.Size(34, 21);
            this.RBMenos.TabIndex = 5;
            this.RBMenos.TabStop = true;
            this.RBMenos.Text = "-";
            this.RBMenos.UseVisualStyleBackColor = true;
            this.RBMenos.CheckedChanged += new System.EventHandler(this.RBMenos_CheckedChanged);
            // 
            // RBMas
            // 
            this.RBMas.AutoSize = true;
            this.RBMas.Location = new System.Drawing.Point(44, 73);
            this.RBMas.Name = "RBMas";
            this.RBMas.Size = new System.Drawing.Size(37, 21);
            this.RBMas.TabIndex = 4;
            this.RBMas.TabStop = true;
            this.RBMas.Text = "+";
            this.RBMas.UseVisualStyleBackColor = true;
            this.RBMas.CheckedChanged += new System.EventHandler(this.RBMas_CheckedChanged);
            // 
            // checkZ
            // 
            this.checkZ.AutoSize = true;
            this.checkZ.Location = new System.Drawing.Point(130, 32);
            this.checkZ.Name = "checkZ";
            this.checkZ.Size = new System.Drawing.Size(39, 21);
            this.checkZ.TabIndex = 2;
            this.checkZ.Text = "Z";
            this.checkZ.UseVisualStyleBackColor = true;
            // 
            // checkY
            // 
            this.checkY.AutoSize = true;
            this.checkY.Location = new System.Drawing.Point(72, 32);
            this.checkY.Name = "checkY";
            this.checkY.Size = new System.Drawing.Size(39, 21);
            this.checkY.TabIndex = 1;
            this.checkY.Text = "Y";
            this.checkY.UseVisualStyleBackColor = true;
            // 
            // checkX
            // 
            this.checkX.AutoSize = true;
            this.checkX.Location = new System.Drawing.Point(19, 32);
            this.checkX.Name = "checkX";
            this.checkX.Size = new System.Drawing.Size(39, 21);
            this.checkX.TabIndex = 0;
            this.checkX.Text = "X";
            this.checkX.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkEscalacion);
            this.groupBox2.Controls.Add(this.checkTraslacion);
            this.groupBox2.Controls.Add(this.checkRotacion);
            this.groupBox2.Location = new System.Drawing.Point(3, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 132);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transformaciones";
            // 
            // checkEscalacion
            // 
            this.checkEscalacion.AutoSize = true;
            this.checkEscalacion.Location = new System.Drawing.Point(13, 86);
            this.checkEscalacion.Name = "checkEscalacion";
            this.checkEscalacion.Size = new System.Drawing.Size(98, 21);
            this.checkEscalacion.TabIndex = 2;
            this.checkEscalacion.Text = "Escalacion";
            this.checkEscalacion.UseVisualStyleBackColor = true;
            // 
            // checkTraslacion
            // 
            this.checkTraslacion.AutoSize = true;
            this.checkTraslacion.Location = new System.Drawing.Point(13, 59);
            this.checkTraslacion.Name = "checkTraslacion";
            this.checkTraslacion.Size = new System.Drawing.Size(96, 21);
            this.checkTraslacion.TabIndex = 1;
            this.checkTraslacion.Text = "Traslacion";
            this.checkTraslacion.UseVisualStyleBackColor = true;
            // 
            // checkRotacion
            // 
            this.checkRotacion.AutoSize = true;
            this.checkRotacion.Location = new System.Drawing.Point(13, 32);
            this.checkRotacion.Name = "checkRotacion";
            this.checkRotacion.Size = new System.Drawing.Size(86, 21);
            this.checkRotacion.TabIndex = 0;
            this.checkRotacion.Text = "Rotacion";
            this.checkRotacion.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(3, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Objetos del escenario";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "escenarioU"});
            this.comboBox1.Location = new System.Drawing.Point(31, 43);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(138, 24);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Text = "Seleccionar...";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 60;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton RBMenos;
        private System.Windows.Forms.RadioButton RBMas;
        private System.Windows.Forms.CheckBox checkZ;
        private System.Windows.Forms.CheckBox checkY;
        private System.Windows.Forms.CheckBox checkX;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkEscalacion;
        private System.Windows.Forms.CheckBox checkTraslacion;
        private System.Windows.Forms.CheckBox checkRotacion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.Timer timer1;
    }
}