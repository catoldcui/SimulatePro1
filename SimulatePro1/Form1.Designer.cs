﻿namespace SimulatePro1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.sourceTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.targetTextLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sourceTextBox
            // 
            this.sourceTextBox.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sourceTextBox.Location = new System.Drawing.Point(44, 36);
            this.sourceTextBox.Name = "sourceTextBox";
            this.sourceTextBox.Size = new System.Drawing.Size(538, 30);
            this.sourceTextBox.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(616, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "转换为拼音";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // targetTextLabel
            // 
            this.targetTextLabel.AutoSize = true;
            this.targetTextLabel.Font = new System.Drawing.Font("宋体", 20F);
            this.targetTextLabel.Location = new System.Drawing.Point(39, 99);
            this.targetTextLabel.MaximumSize = new System.Drawing.Size(500, 0);
            this.targetTextLabel.Name = "targetTextLabel";
            this.targetTextLabel.Size = new System.Drawing.Size(0, 27);
            this.targetTextLabel.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 261);
            this.Controls.Add(this.targetTextLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sourceTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sourceTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label targetTextLabel;
    }
}

