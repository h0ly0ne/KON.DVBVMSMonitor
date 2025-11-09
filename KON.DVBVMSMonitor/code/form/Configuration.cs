using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace KON.DVBVMSMonitor {
    [SupportedOSPlatform("windows")]
    public partial class frmConfiguration : Form {
        public frmConfiguration() {
            InitializeComponent();
        }

        private void Configuration_Load(object sender, EventArgs e) {
            configurationLoad();
            configurationUIUpdate();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            if (btnSave.Text == Resources.frmConfiguration_btnSave_Text) {
                configurationSave();
                DialogResult = DialogResult.OK;
            }
            else
                DialogResult = DialogResult.Abort;

            Close();
        }

        private void configurationLoad() {
            var iTargetServiceStartDelay = Global.srsLocalRegistrySettings.GetInteger(Resources.frmConfiguration_srsKeyTargetServiceStartDelay, Convert.ToInt32(Resources.frmConfiguration_srsKeyTargetServiceStartDelay_DefaultValue));
            if (iTargetServiceStartDelay > 599)
                iTargetServiceStartDelay = 600;
            if (iTargetServiceStartDelay < 0)
                iTargetServiceStartDelay = 0;
            nudTargetServiceStartDelay.Value = iTargetServiceStartDelay;

            tbTargetServiceName.Text = Global.srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyTargetServiceName, Resources.frmConfiguration_srsKeyTargetServiceName_DefaultValue);
            tbTargetServiceProcessName.Text = Global.srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyTargetServiceProcessName, Resources.frmConfiguration_srsKeyTargetServiceProcessName_DefaultValue);
            tbTargetServicePathWithFilename.Text = Global.srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyTargetServicePathWithFilename, Resources.frmConfiguration_srsKeyTargetServicePathWithFilename_DefaultValue);

            var iTargetServiceCheckInterval = Global.srsLocalRegistrySettings.GetInteger(Resources.frmConfiguration_srsKeyTargetServiceCheckInterval, Convert.ToInt32(Resources.frmConfiguration_srsKeyTargetServiceCheckInterval_DefaultValue));
            if (iTargetServiceCheckInterval > 59)
                iTargetServiceCheckInterval = 60;
            if (iTargetServiceCheckInterval < 1)
                iTargetServiceCheckInterval = 1;
            nudTargetServiceCheckInterval.Value = iTargetServiceCheckInterval;

            var iTargetServiceTimeout = Global.srsLocalRegistrySettings.GetInteger(Resources.frmConfiguration_srsKeyTargetServiceTimeout, Convert.ToInt32(Resources.frmConfiguration_srsKeyTargetServiceTimeout_DefaultValue));
            if (iTargetServiceTimeout > 59)
                iTargetServiceTimeout = 60;
            if (iTargetServiceTimeout < 1)
                iTargetServiceTimeout = 1;
            nudTargetServiceTimeout.Value = iTargetServiceTimeout;

            gbWebService.Enabled = cbWebServiceEnabled.Checked = Global.srsLocalRegistrySettings.GetBoolean(Resources.frmConfiguration_srsKeyWebServiceEnabled, Convert.ToBoolean(Resources.frmConfiguration_srsKeyWebServiceEnabled_DefaultValue));
            tbWebServiceUrl.Text = Global.srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyWebServiceUrl, Resources.frmConfiguration_srsKeyWebServiceUrl_DefaultValue);

            var iWebServiceTimeout = Global.srsLocalRegistrySettings.GetInteger(Resources.frmConfiguration_srsKeyWebServiceTimeout, Convert.ToInt32(Resources.frmConfiguration_srsKeyWebServiceTimeout_DefaultValue));
            if (iWebServiceTimeout > 59)
                iWebServiceTimeout = 60;
            if (iWebServiceTimeout < 1)
                iWebServiceTimeout = 1;
            nudWebServiceTimeout.Value = iWebServiceTimeout;

            gbForceBind.Enabled = cbForceBindEnabled.Checked = Global.srsLocalRegistrySettings.GetBoolean(Resources.frmConfiguration_srsKeyForceBindEnabled, Convert.ToBoolean(Resources.frmConfiguration_srsKeyForceBindEnabled_DefaultValue));
            tbForceBindPathWithFilename.Text = Global.srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyForceBindPathWithFilename, Resources.frmConfiguration_srsKeyForceBindPathWithFilename_DefaultValue);
            tbForceBindIPAddress.Text = Global.srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyForceBindIPAddress, Resources.frmConfiguration_srsKeyForceBindIPAddress_DefaultValue);

            cbLogLevel.SelectedItem = Global.srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyLogLevel, Resources.frmConfiguration_srsKeyLogLevel_DefaultValue);
            cbLogToConsole.Checked = Global.srsLocalRegistrySettings.GetBoolean(Resources.frmConfiguration_srsKeyLogToConsole, Convert.ToBoolean(Resources.frmConfiguration_srsKeyLogToConsole_DefaultValue));
            cbVerboseLogging.Checked = Global.srsLocalRegistrySettings.GetBoolean(Resources.frmConfiguration_srsKeyVerboseLogging, Convert.ToBoolean(Resources.frmConfiguration_srsKeyVerboseLogging_DefaultValue));
        }

        private void configurationSave() {
            Global.srsLocalRegistrySettings.SetInteger(Resources.frmConfiguration_srsKeyTargetServiceStartDelay, Convert.ToInt32(nudTargetServiceStartDelay.Value));
            Global.srsLocalRegistrySettings.SetString(Resources.frmConfiguration_srsKeyTargetServiceName, tbTargetServiceName.Text);
            Global.srsLocalRegistrySettings.SetString(Resources.frmConfiguration_srsKeyTargetServiceProcessName, tbTargetServiceProcessName.Text);
            Global.srsLocalRegistrySettings.SetString(Resources.frmConfiguration_srsKeyTargetServicePathWithFilename, tbTargetServicePathWithFilename.Text);
            Global.srsLocalRegistrySettings.SetInteger(Resources.frmConfiguration_srsKeyTargetServiceCheckInterval, Convert.ToInt32(nudTargetServiceCheckInterval.Value));
            Global.srsLocalRegistrySettings.SetInteger(Resources.frmConfiguration_srsKeyTargetServiceTimeout, Convert.ToInt32(nudTargetServiceTimeout.Value));
            Global.srsLocalRegistrySettings.SetBoolean(Resources.frmConfiguration_srsKeyWebServiceEnabled, cbWebServiceEnabled.Checked);
            Global.srsLocalRegistrySettings.SetString(Resources.frmConfiguration_srsKeyWebServiceUrl, tbWebServiceUrl.Text);
            Global.srsLocalRegistrySettings.SetInteger(Resources.frmConfiguration_srsKeyWebServiceTimeout, Convert.ToInt32(nudWebServiceTimeout.Value));
            Global.srsLocalRegistrySettings.SetBoolean(Resources.frmConfiguration_srsKeyForceBindEnabled, cbForceBindEnabled.Checked);
            Global.srsLocalRegistrySettings.SetString(Resources.frmConfiguration_srsKeyForceBindPathWithFilename, tbForceBindPathWithFilename.Text);
            Global.srsLocalRegistrySettings.SetString(Resources.frmConfiguration_srsKeyForceBindIPAddress, tbForceBindIPAddress.Text);
            Global.srsLocalRegistrySettings.SetString(Resources.frmConfiguration_srsKeyLogLevel, Convert.ToString(cbLogLevel.SelectedItem));
            Global.srsLocalRegistrySettings.SetBoolean(Resources.frmConfiguration_srsKeyLogToConsole, cbLogToConsole.Checked);
            Global.srsLocalRegistrySettings.SetBoolean(Resources.frmConfiguration_srsKeyVerboseLogging, cbVerboseLogging.Checked);
        }

        private void configurationUIUpdate() {
            Text = Resources.Program_Name + string.Empty.Space() + Global.GetAssemblyVersion() + string.Empty.Space() + Resources.frmConfiguration_BaseTitle;
        }

        private void cbWebServiceEnabled_CheckedChanged(object sender, EventArgs e) {
            gbWebService.Enabled = cbWebServiceEnabled.Checked;
        }

        private void cbForceBindEnabled_CheckedChanged(object sender, EventArgs e) {
            gbForceBind.Enabled = cbForceBindEnabled.Checked;
        }
    }
}