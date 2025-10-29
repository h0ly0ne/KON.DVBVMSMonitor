using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Versioning;
using System.Threading;

namespace KON.DVBVMSMonitor
{
    [SupportedOSPlatform("windows")]
    public class Service {
        private static RegistrySettings srsLocalRegistrySettings { get; } = new(Resources.Program_Name, RegistrySettings.RegistryHive.HKLM);
        private static Process pLocalTargetServiceProcess;
        private Timer tLocalCheckTimer;
        private static HttpClientHandler hcLocalHttpClientHandler = new() { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator };
        private HttpClient hcLocalHttpClient = new(hcLocalHttpClientHandler);

        public void RunAsConsole(string[] args) {
            Start();
            Global.welCurrentWindowsEventLogger.WriteConsole(Resources.Program_RunAsConsole, WindowsEventLogger.LogType.Information, true);
            Console.ReadLine();
            Stop();
        }

        public void Start() {
            var bForceBindEnabled = srsLocalRegistrySettings.GetBoolean(Resources.frmConfiguration_srsKeyForceBindEnabled, Convert.ToBoolean(Resources.frmConfiguration_srsKeyForceBindEnabled_DefaultValue));
            var sTargetServiceName = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyTargetServiceName, Resources.frmConfiguration_srsKeyTargetServiceName_DefaultValue);
            var sTargetServiceProcessName = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyTargetServiceProcessName, Resources.frmConfiguration_srsKeyTargetServiceProcessName_DefaultValue);
            var sForceBindPathWithFilename = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyForceBindPathWithFilename, Resources.frmConfiguration_srsKeyForceBindPathWithFilename_DefaultValue);
            var sForceBindIPAddress = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyForceBindIPAddress, Resources.frmConfiguration_srsKeyForceBindIPAddress_DefaultValue);
            var sTargetServicePathWithFilename = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyTargetServicePathWithFilename, Resources.frmConfiguration_srsKeyTargetServicePathWithFilename_DefaultValue);
            var iTargetServiceStartDelay = srsLocalRegistrySettings.GetInteger(Resources.frmConfiguration_srsKeyTargetServiceStartDelay, Convert.ToInt32(Resources.frmConfiguration_srsKeyTargetServiceStartDelay_DefaultValue));
            var iTargetServiceTimeout = srsLocalRegistrySettings.GetInteger(Resources.frmConfiguration_srsKeyTargetServiceTimeout, Convert.ToInt32(Resources.frmConfiguration_srsKeyTargetServiceTimeout_DefaultValue));

            if (bForceBindEnabled) {
                if (!Global.IsServiceDisabled(sTargetServiceName)) {
                    if (Global.DisableService(sTargetServiceName, iTargetServiceTimeout)) {
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_DisabledSuccessful, 0, WindowsEventLogger.LogType.Information);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_DisabledSuccessful, WindowsEventLogger.LogType.Information, false);
                    }
                    else {
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_DisabledNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_DisabledNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                        return;
                    }
                }

                if (Global.IsServiceRunning(sTargetServiceName)) {
                    if (Global.StopService(sTargetServiceName, iTargetServiceTimeout)) {
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StoppedSuccessful, 0, WindowsEventLogger.LogType.Information);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StoppedSuccessful, WindowsEventLogger.LogType.Information, false);
                    }
                    else {
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StoppedNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StoppedNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                        return;
                    }
                }

                try {
                    Process.GetProcesses().Where(pr => pr.ProcessName.Equals(sTargetServiceProcessName)).ToList().ForEach(delegate (Process pCurrentProcess) { pCurrentProcess.Kill(); pCurrentProcess.WaitForExit(); pCurrentProcess.Dispose(); });
                    pLocalTargetServiceProcess = Process.GetProcesses().Where(pr => pr.ProcessName.Equals(sTargetServiceProcessName)).ToList().FirstOrDefault();

                    if (pLocalTargetServiceProcess == null) {
                        if (iTargetServiceStartDelay > 0) {
                            Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartDelayed, 0, WindowsEventLogger.LogType.Information);
                            Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartDelayed, WindowsEventLogger.LogType.Information, false);
                            Thread.Sleep(iTargetServiceStartDelay * 1000);
                        }

                        pLocalTargetServiceProcess = Process.Start("\"" + sForceBindPathWithFilename + "\" " + sForceBindIPAddress + " \"" + sTargetServicePathWithFilename + "\"");
                        Thread.Sleep(1000);
                        pLocalTargetServiceProcess = Process.GetProcesses().Where(pr => pr.ProcessName.Equals(srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyTargetServiceProcessName, Resources.frmConfiguration_srsKeyTargetServiceProcessName_DefaultValue))).ToList().FirstOrDefault();

                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartedSuccessful, 0, WindowsEventLogger.LogType.Information);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartedSuccessful, WindowsEventLogger.LogType.Information, false);
                    }
                    else {
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartedNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartedNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                    }
                }
                catch {
                    Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartedNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                    Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartedNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                }
            }
            else {
                if (iTargetServiceStartDelay > 0) {
                    Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartDelayed, 0, WindowsEventLogger.LogType.Information);
                    Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartDelayed, WindowsEventLogger.LogType.Information, false);
                    Thread.Sleep(iTargetServiceStartDelay * 1000);
                }

                if (Global.IsServiceDisabled(sTargetServiceName)) {
                    if (Global.EnableService(sTargetServiceName, iTargetServiceTimeout)) {
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_EnabledSuccessful, 0, WindowsEventLogger.LogType.Information);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_EnabledSuccessful, WindowsEventLogger.LogType.Information, false);
                    }
                    else {
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_EnabledNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_EnabledNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                        return;
                    }
                }

                if (!Global.IsServiceRunning(sTargetServiceName)) {
                    if (Global.StartService(sTargetServiceName, iTargetServiceTimeout)) {
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartedSuccessful, 0, WindowsEventLogger.LogType.Information);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartedSuccessful, WindowsEventLogger.LogType.Information, false);
                    }
                    else {
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartedNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartedNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                        return;
                    }
                }
            }

            try {
                hcLocalHttpClient.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(srsLocalRegistrySettings.GetInteger(Resources.frmConfiguration_srsKeyWebServiceTimeout, Convert.ToInt32(Resources.frmConfiguration_srsKeyWebServiceTimeout_DefaultValue))));
                tLocalCheckTimer = new Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(Convert.ToInt32(srsLocalRegistrySettings.GetInteger(Resources.frmConfiguration_srsKeyTargetServiceCheckInterval, Convert.ToInt32(Resources.frmConfiguration_srsKeyTargetServiceCheckInterval_DefaultValue)))));
            }
            catch {
                Stop();
            }
        }

        public void Stop()
        {
            var bForceBindEnabled = srsLocalRegistrySettings.GetBoolean(Resources.frmConfiguration_srsKeyForceBindEnabled, Convert.ToBoolean(Resources.frmConfiguration_srsKeyForceBindEnabled_DefaultValue));
            var sTargetServiceName = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyTargetServiceName, Resources.frmConfiguration_srsKeyTargetServiceName_DefaultValue);
            var sTargetServiceProcessName = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyTargetServiceProcessName, Resources.frmConfiguration_srsKeyTargetServiceProcessName_DefaultValue);
            var iTargetServiceTimeout = srsLocalRegistrySettings.GetInteger(Resources.frmConfiguration_srsKeyTargetServiceTimeout, Convert.ToInt32(Resources.frmConfiguration_srsKeyTargetServiceTimeout_DefaultValue));

            tLocalCheckTimer?.Dispose();

            if (!bForceBindEnabled)
                return;

            try {
                Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_Killing, 0, WindowsEventLogger.LogType.Information);
                Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_Killing, WindowsEventLogger.LogType.Information, false);
                Process.GetProcesses().Where(pr => pr.ProcessName.Equals(sTargetServiceProcessName)).ToList().ForEach(delegate (Process pCurrentProcess) { pCurrentProcess.Kill(); pCurrentProcess.WaitForExit(); pCurrentProcess.Dispose(); });
                Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_KilledSuccessful, 0, WindowsEventLogger.LogType.Information);
                Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_KilledSuccessful, WindowsEventLogger.LogType.Information, false);
            }
            catch {
                Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_KilledNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_KilledNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                return;
            }

            if (Global.IsServiceDisabled(sTargetServiceName)) {
                if (Global.EnableService(sTargetServiceName, iTargetServiceTimeout)) {
                    Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_EnabledSuccessful, 0, WindowsEventLogger.LogType.Information);
                    Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_EnabledSuccessful, WindowsEventLogger.LogType.Information, false);
                }
                else {
                    Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_EnabledNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                    Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_EnabledNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                    return;
                }
            }

            if (Global.IsServiceRunning(sTargetServiceName))
                return;

            if (Global.StartService(sTargetServiceName, iTargetServiceTimeout)) {
                Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartedSuccessful, 0, WindowsEventLogger.LogType.Information);
                Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartedSuccessful, WindowsEventLogger.LogType.Information, false);
            }
            else {
                Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartedNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartedNotSuccessful, WindowsEventLogger.LogType.Warning, false);
            }
        }

        private void TimerCallback(object oLocalState) {
            CheckWebServer();
        }

        private void CheckWebServer() {
            var sTargetServiceName = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyTargetServiceName, Resources.frmConfiguration_srsKeyTargetServiceName_DefaultValue);
            var strWebServiceUrl = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyWebServiceUrl, Resources.frmConfiguration_srsKeyWebServiceUrl_DefaultValue);
            var iTargetServiceTimeout = srsLocalRegistrySettings.GetInteger(Resources.frmConfiguration_srsKeyTargetServiceTimeout, Convert.ToInt32(Resources.frmConfiguration_srsKeyTargetServiceTimeout_DefaultValue));

            try {
                Global.welCurrentWindowsEventLogger.WriteEntry(Resources.WebService_RequestCreated, 0, WindowsEventLogger.LogType.Information);
                Global.welCurrentWindowsEventLogger.WriteConsole(Resources.WebService_RequestCreated, WindowsEventLogger.LogType.Information, false);

                using var hrmCurrentHttpResponseMessage = hcLocalHttpClient.GetAsync(strWebServiceUrl, HttpCompletionOption.ResponseHeadersRead).Result;

                Global.welCurrentWindowsEventLogger.WriteEntry(Resources.WebService_ResponseCreated, 0, WindowsEventLogger.LogType.Information);
                Global.welCurrentWindowsEventLogger.WriteConsole(Resources.WebService_ResponseCreated, WindowsEventLogger.LogType.Information, false);

                if (hrmCurrentHttpResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    Global.welCurrentWindowsEventLogger.WriteEntry(Resources.WebService_StatusCodeNotOK, 0, WindowsEventLogger.LogType.Warning);
                    Global.welCurrentWindowsEventLogger.WriteConsole(Resources.WebService_StatusCodeNotOK, WindowsEventLogger.LogType.Warning, false);
                    RestartService(sTargetServiceName, iTargetServiceTimeout);
                }
                else
                {
                    Global.welCurrentWindowsEventLogger.WriteEntry(Resources.WebService_StatusCodeOK, 0, WindowsEventLogger.LogType.Information);
                    Global.welCurrentWindowsEventLogger.WriteConsole(Resources.WebService_StatusCodeOK, WindowsEventLogger.LogType.Information, false);
                    Global.welCurrentWindowsEventLogger.WriteEntry(Resources.WebService_ServiceRestartNotRequired, 0, WindowsEventLogger.LogType.Information);
                    Global.welCurrentWindowsEventLogger.WriteConsole(Resources.WebService_ServiceRestartNotRequired, WindowsEventLogger.LogType.Information, false);
                }

                Global.welCurrentWindowsEventLogger.WriteEntry(Resources.WebService_ResponseDisposed, 0, WindowsEventLogger.LogType.Information);
                Global.welCurrentWindowsEventLogger.WriteConsole(Resources.WebService_ResponseDisposed, WindowsEventLogger.LogType.Information, false);
            }
            catch {
                Global.welCurrentWindowsEventLogger.WriteEntry(Resources.WebService_RequestTimedOut, 0, WindowsEventLogger.LogType.Warning);
                Global.welCurrentWindowsEventLogger.WriteConsole(Resources.WebService_RequestTimedOut, WindowsEventLogger.LogType.Warning, false);
                RestartService(sTargetServiceName, iTargetServiceTimeout);
            }

            Global.welCurrentWindowsEventLogger.WriteEntry(Resources.WebService_RequestDisposed, 0, WindowsEventLogger.LogType.Information);
            Global.welCurrentWindowsEventLogger.WriteConsole(Resources.WebService_RequestDisposed, WindowsEventLogger.LogType.Information, false);
        }

        private static void RestartService(string strLocalTargetServiceName, int iLocalTargetServiceTimeout) {
            var bForceBindEnabled = srsLocalRegistrySettings.GetBoolean(Resources.frmConfiguration_srsKeyForceBindEnabled, Convert.ToBoolean(Resources.frmConfiguration_srsKeyForceBindEnabled_DefaultValue));
            var sTargetServiceProcessName = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyTargetServiceProcessName, Resources.frmConfiguration_srsKeyTargetServiceProcessName_DefaultValue);
            var iTargetServiceStartDelay = srsLocalRegistrySettings.GetInteger(Resources.frmConfiguration_srsKeyTargetServiceStartDelay, Convert.ToInt32(Resources.frmConfiguration_srsKeyTargetServiceStartDelay_DefaultValue));
            var sForceBindPathWithFilename = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyForceBindPathWithFilename, Resources.frmConfiguration_srsKeyForceBindPathWithFilename_DefaultValue);
            var sForceBindIPAddress = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyForceBindIPAddress, Resources.frmConfiguration_srsKeyForceBindIPAddress_DefaultValue);
            var sTargetServicePathWithFilename = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyTargetServicePathWithFilename, Resources.frmConfiguration_srsKeyTargetServicePathWithFilename_DefaultValue);

            if (!bForceBindEnabled)
            {
                if (Global.IsServiceRunning(strLocalTargetServiceName)) {
                    Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_Stopping, 0, WindowsEventLogger.LogType.Information);
                    Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_Stopping, WindowsEventLogger.LogType.Information, false);

                    if (Global.StopService(strLocalTargetServiceName, iLocalTargetServiceTimeout)) {
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StoppedSuccessful, 0, WindowsEventLogger.LogType.Information);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StoppedSuccessful, WindowsEventLogger.LogType.Information, false);
                    }
                    else {
                        try {
                            Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_Killing, 0, WindowsEventLogger.LogType.Information);
                            Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_Killing, WindowsEventLogger.LogType.Information, false);
                            Process.GetProcesses().Where(pr => pr.ProcessName.Equals(sTargetServiceProcessName)).ToList().ForEach(delegate (Process pCurrentProcess) { pCurrentProcess.Kill(); pCurrentProcess.WaitForExit(); pCurrentProcess.Dispose(); });
                            Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_KilledSuccessful, 0, WindowsEventLogger.LogType.Information);
                            Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_KilledSuccessful, WindowsEventLogger.LogType.Information, false);
                        }
                        catch {
                            Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_KilledNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                            Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_KilledNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                            Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StoppedNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                            Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StoppedNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                            return;
                        }
                    }
                }

                if (!Global.IsServiceRunning(strLocalTargetServiceName)) {
                    Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_Starting, 0, WindowsEventLogger.LogType.Information);
                    Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_Starting, WindowsEventLogger.LogType.Information, false);

                    if (Global.StartService(strLocalTargetServiceName, iLocalTargetServiceTimeout)) {
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartedSuccessful, 0, WindowsEventLogger.LogType.Information);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartedSuccessful, WindowsEventLogger.LogType.Information, false);
                    }
                    else {
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartedNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartedNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_RestartedNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_RestartedNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                        return;
                    }
                }
            }
            else {
                try
                {
                    Process.GetProcesses().Where(pr => pr.ProcessName.Equals(sTargetServiceProcessName)).ToList().ForEach(delegate (Process pCurrentProcess) { pCurrentProcess.Kill(); pCurrentProcess.WaitForExit(); pCurrentProcess.Dispose(); });
                    pLocalTargetServiceProcess = Process.GetProcesses().Where(pr => pr.ProcessName.Equals(sTargetServiceProcessName)).ToList().FirstOrDefault();

                    if (pLocalTargetServiceProcess == null) {
                        if (iTargetServiceStartDelay > 0) {
                            Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartDelayed, 0, WindowsEventLogger.LogType.Information);
                            Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartDelayed, WindowsEventLogger.LogType.Information, false);
                            Thread.Sleep(iTargetServiceStartDelay * 1000);
                        }

                        pLocalTargetServiceProcess = Process.Start("\"" + sForceBindPathWithFilename + "\" " + sForceBindIPAddress + " \"" + sTargetServicePathWithFilename + "\"");
                        Thread.Sleep(1000);
                        pLocalTargetServiceProcess = Process.GetProcesses().Where(pr => pr.ProcessName.Equals(srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyTargetServiceProcessName, Resources.frmConfiguration_srsKeyTargetServiceProcessName_DefaultValue))).ToList().FirstOrDefault();

                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartedSuccessful, 0, WindowsEventLogger.LogType.Information);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartedSuccessful, WindowsEventLogger.LogType.Information, false);
                    }
                    else {
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartedNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartedNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                        Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_RestartedNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                        Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_RestartedNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                        return;
                    }
                }
                catch
                {
                    Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_StartedNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                    Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_StartedNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                    Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_RestartedNotSuccessful, 0, WindowsEventLogger.LogType.Warning);
                    Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_RestartedNotSuccessful, WindowsEventLogger.LogType.Warning, false);
                    return;
                }
            }

            Global.welCurrentWindowsEventLogger.WriteEntry(Resources.TargetService_RestartedSuccessful, 0, WindowsEventLogger.LogType.Information);
            Global.welCurrentWindowsEventLogger.WriteConsole(Resources.TargetService_RestartedSuccessful, WindowsEventLogger.LogType.Information, false);
        }
    }
}