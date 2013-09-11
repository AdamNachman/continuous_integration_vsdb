// <copyright file="Main.Designer.cs" company="Adam Nachman">
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
    /// The main entry point of the application
    /// </summary>
    public partial class Main
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
            this.cboServers = new System.Windows.Forms.ComboBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblDatabases = new System.Windows.Forms.Label();
            this.cboDatabases = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboServers
            // 
            this.cboServers.FormattingEnabled = true;
            this.cboServers.Location = new System.Drawing.Point(15, 25);
            this.cboServers.Name = "cboServers";
            this.cboServers.Size = new System.Drawing.Size(345, 21);
            this.cboServers.TabIndex = 0;
            this.cboServers.SelectedIndexChanged += new System.EventHandler(this.Servers_SelectedIndexChanged);
            this.cboServers.Leave += new System.EventHandler(this.Servers_Leave);
            this.cboServers.Click += new System.EventHandler(this.Servers_Click);
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(12, 9);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(95, 13);
            this.lblServer.TabIndex = 1;
            this.lblServer.Text = "Select SQL Server";
            // 
            // lblDatabases
            // 
            this.lblDatabases.AutoSize = true;
            this.lblDatabases.Location = new System.Drawing.Point(12, 62);
            this.lblDatabases.Name = "lblDatabases";
            this.lblDatabases.Size = new System.Drawing.Size(86, 13);
            this.lblDatabases.TabIndex = 3;
            this.lblDatabases.Text = "Select Database";
            // 
            // cboDatabases
            // 
            this.cboDatabases.FormattingEnabled = true;
            this.cboDatabases.Location = new System.Drawing.Point(15, 78);
            this.cboDatabases.Name = "cboDatabases";
            this.cboDatabases.Size = new System.Drawing.Size(345, 21);
            this.cboDatabases.TabIndex = 2;
            this.cboDatabases.Click += new System.EventHandler(this.Databases_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(194, 109);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(83, 25);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(283, 109);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 25);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(94, 109);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(94, 25);
            this.btnConfig.TabIndex = 6;
            this.btnConfig.Text = "Change Config";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.Config_Click);
            // 
            // Main
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(374, 146);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblDatabases);
            this.Controls.Add(this.cboDatabases);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.cboServers);
            this.Name = "Main";
            this.Text = "Database Deployment";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboServers;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblDatabases;
        private System.Windows.Forms.ComboBox cboDatabases;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnConfig;
    }
}

