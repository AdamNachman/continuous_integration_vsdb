// <copyright file="ProgressForm.cs" company="Adam Nachman">
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
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using Microsoft.SqlServer.Management.Smo;

    // TODO: Handle cancel event to break execution on a given script, not just between scripts

    /// <summary>
    /// The ProgressForm class
    /// </summary>
    internal partial class ProgressForm : Form
    {
        #region Private Variables

        private Microsoft.SqlServer.Management.Smo.Server server = null;
        private Microsoft.SqlServer.Management.Smo.Database database = null;
        private ScriptExecutor executor = null;
        private ScriptExecutorConfiguration config = null;
        private Regex counterExpression = new Regex("(?<ObjectCount>[0-9])");
        private Regex progressExpression = new Regex("(\\d{1,})");
        private Thread executionThread = null;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ProgressForm class
        /// </summary>
        /// <param name="server">The server to which the class is connected</param>
        /// <param name="database">The database againts which the class is executing</param>
        /// <param name="config">The cofiguration</param>
        public ProgressForm(Server server, Database database, ScriptExecutorConfiguration config)
        {
            if (server == null)
            {
                throw new ArgumentNullException("server", "server cannot be null");
            }

            if (database == null)
            {
                throw new ArgumentNullException("database", "database cannot be null");
            }

            if (config == null)
            {
                throw new ArgumentNullException("config", "config cannot be null");
            }

            this.server = server;
            this.database = database;
            this.config = config;
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Delegates

        /// <summary>
        /// A delegate to handle the OnProgress events from the script executor
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The ScriptExecutorEventArgs arguments</param>
        private delegate void HandleOnProgressDelegate(object sender, ScriptExecutorEventArgs[] e);

        #endregion Delegates

        #region Form Event Handlers

        /// <summary>
        /// Fire on form load
        /// </summary>
        /// <param name="sender">The object sender</param>
        /// <param name="e">The EventArgs</param>
        private void ProgressForm_Load(object sender, EventArgs e)
        {
            this.InitializeFormValues();
        }

        /// <summary>
        /// Fires on form shown
        /// </summary>
        /// <param name="sender">The object sender</param>
        /// <param name="e">The EventArgs</param>
        private void ProgressForm_Shown(object sender, EventArgs e)
        {
            // TODO: Check Thread state
            this.ToggleButtonState(false);
            this.executionThread = new Thread(new ThreadStart(this.ExecuteScripts));
            this.executionThread.Start();
        }

        /// <summary>
        /// Fires on OK click
        /// </summary>
        /// <param name="sender">The object sender</param>
        /// <param name="e">The EventArgs</param>
        private void Ok_Click(object sender, EventArgs e)
        {
            // TODO: Check Thread state
            // TODO: Manage button state
            // TODO: Check execution state
            this.Close();
        }

        /// <summary>
        /// Cancels execution 
        /// </summary>
        /// <param name="sender">The object sender</param>
        /// <param name="e">The EventArgs</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.executor.Stop();
        }

        #endregion Form Event Handlers

        #region Private Methods

        /// <summary>
        /// Initializes the form values
        /// </summary>
        private void InitializeFormValues()
        {
            this.lblProgress.Text = string.Empty;
            this.lblCurrentOperation.Text = string.Empty;
            this.progressOverall.Value = 0;
            this.progressOverall.Maximum = this.config.ExecutionSequence.Count;
        }

        /// <summary>
        /// Handles the progress of the script executor
        /// </summary>
        /// <param name="sender">The object sender</param>
        /// <param name="e">The event arguments</param>
        private void OnProgress(object sender, ScriptExecutorEventArgs[] e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new HandleOnProgressDelegate(this.HandleOnProgress), sender, e);
            }
            else
            {
                this.HandleOnProgress(sender, e);
            }
        }

        /// <summary>
        /// Handles the progress of the script executor
        /// </summary>
        /// <param name="sender">The object sender</param>
        /// <param name="e">The event arguments</param>
        private void HandleOnProgress(object sender, ScriptExecutorEventArgs[] e)
        {
            Match m = null;
            if (e != null && e.Length > 0)
            {
                int length = e.Length;
                for (int i = 0; i < length; i++)
                {
                    switch (e[i].EventCode)
                    {
                        case ScriptExecutorEventArgs.EventMessageCode.ObjectCount:
                            this.progressCurrent.Value = 0;
                            m = this.counterExpression.Match(e[i].EventMessage);
                            if (m.Success)
                            {
                                this.progressCurrent.Maximum = int.Parse(m.Groups["ObjectCount"].Value);
                            }

                            break;
                        case ScriptExecutorEventArgs.EventMessageCode.Progress:
                            int progress = 0;
                            int count = 0;
                            m = this.progressExpression.Match(e[i].EventMessage);
                            this.lblCurrentOperation.Text = e[i].EventMessage.Replace("|", string.Empty);
                            if (m.Success)
                            {
                                progress = int.Parse(m.Groups[0].Value);
                                m = m.NextMatch();
                                count = int.Parse(m.Groups[0].Value);
                                if (count > this.progressCurrent.Maximum)
                                {
                                    this.progressCurrent.Maximum = count;
                                }

                                this.progressCurrent.Value = progress;
                            }

                            break;
                        case ScriptExecutorEventArgs.EventMessageCode.ExecutionComplete:
                            this.lblProgress.Text = "Complete";
                            this.ToggleButtonState(true);
                            break;
                        case ScriptExecutorEventArgs.EventMessageCode.ExecutionStoppingByRequest:
                            this.lblProgress.Text = "Stopping script execution";
                            break;
                        case ScriptExecutorEventArgs.EventMessageCode.ExecutionStoppedByRequest:
                            this.lblProgress.Text = "Execution stopped by user request";
                            this.ToggleButtonState(true);
                            break;
                        case ScriptExecutorEventArgs.EventMessageCode.ScriptExecution:
                            this.lblProgress.Text = e[i].EventMessage.Replace(".sql", string.Empty);
                            this.progressOverall.Value += 1;
                            break;
                        case ScriptExecutorEventArgs.EventMessageCode.SqlInfoMessage:
                            this.txtMessageInfo.AppendText(string.Concat(DateTime.Now.ToString("hh:mm tt"), " - ", e[i].EventMessage, Environment.NewLine));
                            break;
                    }
                }
            }

            Application.DoEvents();
        }

        /// <summary>
        /// Executes the scripts
        /// </summary>
        private void ExecuteScripts()
        {
            try
            {
                this.executor = new ScriptExecutor(this.server, this.database, this.config);
                this.executor.Progress += new ScriptExecutor.ScriptExecutorEventHandler(this.OnProgress);
                this.executor.Execute();
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
                this.executor.Progress -= new ScriptExecutor.ScriptExecutorEventHandler(this.OnProgress);
            }
        }

        /// <summary>
        /// Toggles the button states
        /// </summary>
        /// <param name="enableOK">The state of the "OK" button</param>
        private void ToggleButtonState(bool enableOK)
        {
            this.btnOK.Enabled = enableOK;
            this.btnCancel.Enabled = !enableOK;
        }

        #endregion Private Methods
    }
}
