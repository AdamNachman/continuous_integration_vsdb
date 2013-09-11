// <copyright file="Main.cs" company="Adam Nachman">
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
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using Microsoft.SqlServer.Management.Smo;

    /// <summary>
    /// The main form of the application
    /// </summary>
    public partial class Main : Form
    {
        #region Private Variables

        private bool areServersLoaded = false;
        private bool areDatabasesLoaded = false;
        private string configPath = Path.Combine(Application.StartupPath, "ScriptExecutorConfiguration.xml");

        #endregion Private Variables

        #region Constructors

        public Main()
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Private Methods

        private void InitServerCombo()
        {
            DataTable dataTable = SmoApplication.EnumAvailableSqlServers(false);
            this.cboServers.ValueMember = "Name";
            this.cboServers.DataSource = dataTable;
            this.areServersLoaded = true;
        }

        private void InitDBaseCbo()
        {
            if (this.cboServers.SelectedIndex != -1 && !this.areDatabasesLoaded)
            {
                Cursor.Current = Cursors.WaitCursor;
                string serverName = this.cboServers.SelectedValue.ToString();
                Server server = new Server(serverName);
                try
                {
                    foreach (Database database in server.Databases)
                    {
                        // exclude the system databases
                        switch (database.Name.ToUpper())
                        {
                            case "MASTER":
                            case "TEMPDB":
                            case "MSDB":
                            case "MODEL":
                                break;
                            default:
                                this.cboDatabases.Items.Add(database.Name);
                                break;
                        }
                    }

                    this.areDatabasesLoaded = true;
                }
                finally
                {
                    server = null;
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void ClearDBaseCbo()
        {
            this.cboDatabases.Items.Clear();
            this.areDatabasesLoaded = false;
        }

        private void SaveConfig(string path, ScriptExecutorConfiguration configToSave)
        {
            StreamWriter w = new StreamWriter(path);
            XmlSerializer s = new XmlSerializer(configToSave.GetType());
            s.Serialize(w, configToSave);
            w.Close();
        }

        private ScriptExecutorConfiguration LoadDeploymentConfig(string path)
        {
            ScriptExecutorConfiguration retval = null;
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(ScriptExecutorConfiguration));
                    retval = (ScriptExecutorConfiguration)xs.Deserialize(sr.BaseStream);
                    sr.Close();
                }
            }

            return retval;
        }

        #endregion Private Methods

        #region Form Event Handlers

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void Servers_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearDBaseCbo();
        }

        private void Servers_Click(object sender, EventArgs e)
        {
            if (!this.areServersLoaded)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.InitServerCombo();
                    this.ClearDBaseCbo();
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void Databases_Click(object sender, EventArgs e)
        {
            this.InitDBaseCbo();
        }

        private void Servers_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cboServers.Text.Trim()))
            {
                this.cboServers.SelectedValue = this.cboServers.Text;
            }
        }

        private bool ConfigurationIsValid(ScriptExecutorConfiguration config)
        {
            if (config == null)
            {
                MessageBox.Show(this, "Unable to load the ScriptExecutorConfiguration.xml file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!string.IsNullOrEmpty(config.TargetServer) && this.cboServers.Text != config.TargetServer)
            {
                MessageBox.Show(this, string.Format("You may only deploy this update against the {0} server", config.TargetServer), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!string.IsNullOrEmpty(config.TargetDatabase) && this.cboDatabases.Text != config.TargetDatabase)
            {
                MessageBox.Show(this, string.Format("You may only deploy this update against the {0} database", config.TargetDatabase), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            Server server = null;
            Database database = null;
            ProgressForm progress = null;
            try
            {
                ScriptExecutorConfiguration config = this.LoadDeploymentConfig(this.configPath);

                if (this.ConfigurationIsValid(config))
                {
                    server = new Server(this.cboServers.Text);
                    database = new Database(server, this.cboDatabases.Text);

                    FileInfo f = new FileInfo(this.configPath);
                    Environment.CurrentDirectory = f.DirectoryName;

                    progress = new ProgressForm(server, database, config);
                    progress.ShowDialog(this);
                }
            }
            catch (FailedOperationException ex)
            {
                SqlException sqlEx = ex.InnerException.InnerException as SqlException;
                MessageBox.Show(sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                server = null;
                database = null;
                if (progress != null)
                {
                    if (progress.Visible)
                    {
                        progress.Hide();
                    }

                    if (!progress.Disposing)
                    {
                        progress.Dispose();
                    }
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        #endregion Form Event Handlers

        private void Config_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = Path.GetFullPath(this.configPath);
            fd.Title = "Select a new test file";

            this.configPath = (fd.ShowDialog() == DialogResult.OK) ? fd.FileName : this.configPath;
        }
    }
}
