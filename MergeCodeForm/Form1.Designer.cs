﻿namespace MergeCodeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbSourceBranch = new System.Windows.Forms.TextBox();
            this.tbTargetBranch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbChangeSets = new System.Windows.Forms.TextBox();
            this.btnMerge = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source branch";
            // 
            // tbSourceBranch
            // 
            this.tbSourceBranch.Location = new System.Drawing.Point(106, 13);
            this.tbSourceBranch.Name = "tbSourceBranch";
            this.tbSourceBranch.Size = new System.Drawing.Size(387, 20);
            this.tbSourceBranch.TabIndex = 1;
            // 
            // tbTargetBranch
            // 
            this.tbTargetBranch.Location = new System.Drawing.Point(106, 41);
            this.tbTargetBranch.Name = "tbTargetBranch";
            this.tbTargetBranch.Size = new System.Drawing.Size(387, 20);
            this.tbTargetBranch.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Target branch";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Change sets (seperate by comma)";
            // 
            // tbChangeSets
            // 
            this.tbChangeSets.Location = new System.Drawing.Point(25, 90);
            this.tbChangeSets.Name = "tbChangeSets";
            this.tbChangeSets.Size = new System.Drawing.Size(468, 20);
            this.tbChangeSets.TabIndex = 5;
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(25, 126);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(75, 23);
            this.btnMerge.TabIndex = 6;
            this.btnMerge.Text = "Merge";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 261);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.tbChangeSets);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbTargetBranch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSourceBranch);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSourceBranch;
        private System.Windows.Forms.TextBox tbTargetBranch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbChangeSets;
        private System.Windows.Forms.Button btnMerge;
    }
}
