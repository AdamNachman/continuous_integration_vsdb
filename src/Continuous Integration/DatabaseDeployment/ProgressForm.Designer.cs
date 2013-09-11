// <copyright file="ProgressForm.Designer.cs" company="Adam Nachman">
// Copyright (c) 2009 All Right Reserved Adam Nachman
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Adam Nachman</author>
namespace SqlDeployment
{
    /// <summary>
    /// The internal components
    /// </summary>
    internal partial class ProgressForm
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.progressCurrent = new System.Windows.Forms.ProgressBar();
            this.progressOverall = new System.Windows.Forms.ProgressBar();
            this.lblCurrentOperation = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtMessageInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // progressCurrent
            // 
            this.progressCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressCurrent.Location = new System.Drawing.Point(10, 33);
            this.progressCurrent.Margin = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.progressCurrent.Name = "progressCurrent";
            this.progressCurrent.Size = new System.Drawing.Size(523, 20);
            this.progressCurrent.TabIndex = 0;
            // 
            // progressOverall
            // 
            this.progressOverall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressOverall.Location = new System.Drawing.Point(10, 82);
            this.progressOverall.Margin = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.progressOverall.Name = "progressOverall";
            this.progressOverall.Size = new System.Drawing.Size(523, 20);
            this.progressOverall.TabIndex = 1;
            // 
            // lblCurrentOperation
            // 
            this.lblCurrentOperation.AutoSize = true;
            this.lblCurrentOperation.Location = new System.Drawing.Point(10, 14);
            this.lblCurrentOperation.Margin = new System.Windows.Forms.Padding(5, 5, 5, 3);
            this.lblCurrentOperation.Name = "lblCurrentOperation";
            this.lblCurrentOperation.Size = new System.Drawing.Size(34, 13);
            this.lblCurrentOperation.TabIndex = 2;
            this.lblCurrentOperation.Text = "a of b";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(10, 63);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(5, 5, 5, 3);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(32, 13);
            this.lblProgress.TabIndex = 3;
            this.lblProgress.Text = "x of y";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            //this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(435, 540);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 27);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(327, 540);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5, 5, 5, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(98, 27);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.Ok_Click);
            // 
            // txtMessageInfo
            // 
            this.txtMessageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageInfo.Location = new System.Drawing.Point(10, 112);
            this.txtMessageInfo.Margin = new System.Windows.Forms.Padding(5);
            this.txtMessageInfo.Multiline = true;
            this.txtMessageInfo.Name = "txtMessageInfo";
            this.txtMessageInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessageInfo.Size = new System.Drawing.Size(523, 418);
            this.txtMessageInfo.TabIndex = 6;
            // 
            // ProgressForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 577);
            this.ControlBox = false;
            this.Controls.Add(this.txtMessageInfo);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblCurrentOperation);
            this.Controls.Add(this.progressOverall);
            this.Controls.Add(this.progressCurrent);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Deployment Progress";
            this.Load += new System.EventHandler(this.ProgressForm_Load);
            this.Shown += new System.EventHandler(this.ProgressForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressCurrent;
        private System.Windows.Forms.ProgressBar progressOverall;
        private System.Windows.Forms.Label lblCurrentOperation;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtMessageInfo;
    }
}