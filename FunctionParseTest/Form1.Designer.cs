namespace FunctionParseTest
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.staticField = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fieldName = new System.Windows.Forms.TextBox();
            this.fieldValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.parseString = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "fixed test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(43, 223);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(572, 163);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "result";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.parseString);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.fieldValue);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.fieldName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.staticField);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(45, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(552, 134);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "custom test";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Static Number:";
            // 
            // staticField
            // 
            this.staticField.Location = new System.Drawing.Point(109, 22);
            this.staticField.Name = "staticField";
            this.staticField.Size = new System.Drawing.Size(165, 19);
            this.staticField.TabIndex = 1;
            this.staticField.Text = "5";
            this.staticField.TextChanged += new System.EventHandler(this.staticField_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "add field name:";
            // 
            // fieldName
            // 
            this.fieldName.Location = new System.Drawing.Point(109, 44);
            this.fieldName.Name = "fieldName";
            this.fieldName.Size = new System.Drawing.Size(165, 19);
            this.fieldName.TabIndex = 3;
            this.fieldName.Text = "A";
            this.fieldName.TextChanged += new System.EventHandler(this.fieldName_TextChanged);
            // 
            // fieldValue
            // 
            this.fieldValue.Location = new System.Drawing.Point(370, 41);
            this.fieldValue.Name = "fieldValue";
            this.fieldValue.Size = new System.Drawing.Size(165, 19);
            this.fieldValue.TabIndex = 5;
            this.fieldValue.Text = "ABC";
            this.fieldValue.TextChanged += new System.EventHandler(this.fieldValue_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(282, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "add field value:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "parse string";
            // 
            // parseString
            // 
            this.parseString.Location = new System.Drawing.Point(109, 69);
            this.parseString.Multiline = true;
            this.parseString.Name = "parseString";
            this.parseString.Size = new System.Drawing.Size(426, 41);
            this.parseString.TabIndex = 7;
            this.parseString.Text = "if (StaticNumber > 10, A, RepeatTimes(5))";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(8, 105);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "custom test";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 411);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox parseString;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox fieldValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox fieldName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox staticField;
        private System.Windows.Forms.Label label2;
    }
}

