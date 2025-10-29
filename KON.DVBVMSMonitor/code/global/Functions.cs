using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.ServiceProcess;

using Microsoft.Win32;

namespace KON.DVBVMSMonitor {
    [SupportedOSPlatform("windows")]
	public static class Global {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        public static readonly WindowsEventLogger welCurrentWindowsEventLogger = new();

        static Global() {
            welCurrentWindowsEventLogger.SetEventSource(Resources.Program_Name, Resources.Program_LogName);
        }

		public static bool IsAdministrator() {
            try {
                var wiCurrentWindowsIdentity = WindowsIdentity.GetCurrent();
                var wpCurrentWindowsPrincipal = new WindowsPrincipal(wiCurrentWindowsIdentity);
                return wpCurrentWindowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch {
                return false;
            }
        }

        public static Version GetAssemblyVersion() {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        public static ServiceController GetService(string strLocalServiceName) {
            return ServiceController.GetServices().FirstOrDefault(scCurrentService => scCurrentService.ServiceName == strLocalServiceName);
        }

        public static bool StartService(string strLocalServiceName, int iLocalServiceTimeout) {
            var tsTimeout = TimeSpan.FromSeconds(iLocalServiceTimeout);

            if (string.IsNullOrEmpty(strLocalServiceName))
                return false;

            if (IsServiceRunning(strLocalServiceName))
                return true;

            try
            {
                var scCurrentServiceController = GetService(strLocalServiceName);
                scCurrentServiceController.Start();
                scCurrentServiceController.WaitForStatus(ServiceControllerStatus.Running, tsTimeout);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool StopService(string strLocalServiceName, int strLocalServiceTimeout) {
            var tsTimeout = TimeSpan.FromSeconds(strLocalServiceTimeout);

            if (string.IsNullOrEmpty(strLocalServiceName))
                return false;

            if (!IsServiceRunning(strLocalServiceName))
                return true;

            try {
                var scCurrentServiceController = GetService(strLocalServiceName);
                scCurrentServiceController.Stop();
                scCurrentServiceController.WaitForStatus(ServiceControllerStatus.Stopped, tsTimeout);

                return true;
            }
            catch {
                return false;
            }
        }

        public static bool EnableService(string strLocalServiceName, int strLocalServiceTimeout)
        {
            if (string.IsNullOrEmpty(strLocalServiceName))
                return false;

            try {
                using var moCurrentManagementObject = new ManagementObject($"Win32_Service.Name=\"{strLocalServiceName}\"");
                var mboCurrentManagementBaseObject = moCurrentManagementObject.GetMethodParameters("ChangeStartMode");
                mboCurrentManagementBaseObject["StartMode"] = "Automatic";
                moCurrentManagementObject.InvokeMethod("ChangeStartMode", mboCurrentManagementBaseObject, null);

                return StartService(strLocalServiceName, strLocalServiceTimeout);
            }
            catch {
                return false;
            }
        }

        public static bool DisableService(string strLocalServiceName, int strLocalServiceTimeout) {
            if (string.IsNullOrEmpty(strLocalServiceName))
                return false;

            try {
                if (!StopService(strLocalServiceName, strLocalServiceTimeout) && IsServiceRunning(strLocalServiceName))
                    return false;

                using var moCurrentManagementObject = new ManagementObject($"Win32_Service.Name=\"{strLocalServiceName}\"");
                var mboCurrentManagementBaseObject = moCurrentManagementObject.GetMethodParameters("ChangeStartMode");
                mboCurrentManagementBaseObject["StartMode"] = "Disabled";
                moCurrentManagementObject.InvokeMethod("ChangeStartMode", mboCurrentManagementBaseObject, null);

                return true;
            }
            catch {
                return false;
            }
        }

        public static bool IsServiceRunning(string strLocalServiceName) {
            if (string.IsNullOrEmpty(strLocalServiceName))
                return false;

            try {
                return GetService(strLocalServiceName) is { Status: ServiceControllerStatus.Running };
            }
            catch {
                return false;
            }
        }

        public static bool IsServiceDisabled(string strLocalServiceName)
        {
            try
            {
                if (string.IsNullOrEmpty(strLocalServiceName))
                    return false;

                return Convert.ToString(new ManagementObjectSearcher($"SELECT * FROM Win32_Service WHERE Name='{strLocalServiceName}'").Get().Cast<ManagementObject>().ToList().FirstOrDefault()["StartMode"]) == "Disabled";
            }
            catch
            {
                return false;
            }
        }
    }

    [SupportedOSPlatform("windows")]
	public class RegistrySettings(string srsLocalAppName, RegistrySettings.RegistryHive rhLocalRegistryHive) {
		public enum RegistryHive { HKCU, HKLM }

		private readonly string strRegistryPath = @"SOFTWARE\" + srsLocalAppName;
		private readonly RegistryKey rkRegistryKeyRootHive = rhLocalRegistryHive switch { RegistryHive.HKCU => Registry.CurrentUser, _ => Registry.LocalMachine };

        public void SetString(string strLocalName, string strLocalValue) {
            using var rkCurrentRegistryKey = rkRegistryKeyRootHive.CreateSubKey(strRegistryPath);

            if (rkCurrentRegistryKey == null) 
                return;

            rkCurrentRegistryKey.SetValue(strLocalName, strLocalValue, RegistryValueKind.String);
            rkCurrentRegistryKey.Close();
        }

		public string GetString(string strLocalName, string strLocalValue) {
			using var rkCurrentRegistryKey = rkRegistryKeyRootHive.OpenSubKey(strRegistryPath);

			try {
                if (rkCurrentRegistryKey == null || rkCurrentRegistryKey.GetValueKind(strLocalName) != RegistryValueKind.String || string.IsNullOrEmpty(Convert.ToString(rkCurrentRegistryKey.GetValue(strLocalName, strLocalValue))))
                    return strLocalValue;

                return Convert.ToString(rkCurrentRegistryKey.GetValue(strLocalName, strLocalValue));
			}
			catch {
				return strLocalValue;
			}
		}

		public void SetInteger(string strLocalName, int iLocalValue) {
			using var rkCurrentRegistryKey = rkRegistryKeyRootHive.CreateSubKey(strRegistryPath);

            if (rkCurrentRegistryKey == null) 
                return;

            rkCurrentRegistryKey.SetValue(strLocalName, iLocalValue, RegistryValueKind.DWord);
            rkCurrentRegistryKey.Close();
        }

		public int GetInteger(string strLocalName, int iDefaultValue) {
			using var rkCurrentRegistryKey = rkRegistryKeyRootHive.OpenSubKey(strRegistryPath);

			try {
                if (rkCurrentRegistryKey == null || rkCurrentRegistryKey.GetValueKind(strLocalName) != RegistryValueKind.DWord)
                    return iDefaultValue;

                return Convert.ToInt32(rkCurrentRegistryKey.GetValue(strLocalName, iDefaultValue));
            }
			catch {
				return iDefaultValue;
			}
		}

        public void SetBoolean(string strLocalName, bool bLocalValue) {
            using var rkCurrentRegistryKey = rkRegistryKeyRootHive.CreateSubKey(strRegistryPath);

            if (rkCurrentRegistryKey == null)
                return;

            rkCurrentRegistryKey.SetValue(strLocalName, Convert.ToInt32(bLocalValue), RegistryValueKind.DWord);
            rkCurrentRegistryKey.Close();
        }

        public bool GetBoolean(string strLocalName, bool bLocalValue) {
            using var rkCurrentRegistryKey = rkRegistryKeyRootHive.OpenSubKey(strRegistryPath);

            try {
                if (rkCurrentRegistryKey == null || rkCurrentRegistryKey.GetValueKind(strLocalName) != RegistryValueKind.DWord)
                    return bLocalValue;

                return Convert.ToBoolean(rkCurrentRegistryKey.GetValue(strLocalName, Convert.ToInt32(bLocalValue)));
            }
            catch {
                return bLocalValue;
            }
        }
    }

    [SupportedOSPlatform("windows")]
	public class WindowsEventLogger {
        private RegistrySettings srsLocalRegistrySettings { get; } = new(Resources.Program_Name, RegistrySettings.RegistryHive.HKLM);
        public enum LogType { Error = 1, Information = 4, Warning = 2 }
        private string strEventSource;
        
        public void SetEventSource(string strLocalEventSourceName, string strLocalLogName) {
            if (Global.IsAdministrator()) {
                EstablishEventSource(strLocalEventSourceName, strLocalLogName);
                strEventSource = strLocalEventSourceName;
            }
            else {
                strEventSource = "Application";
            }
        }

		public void WriteEntry(string strLocalMessage, int intLocalID, LogType ltLocalLogType) {
			WriteWindowsEvent(strLocalMessage, intLocalID, ltLocalLogType);
		}
        public void WriteConsole(string strLocalMessage, LogType ltLocalLogType, bool bLocalMandatoryMessage) {
            var strLogLevel = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyLogLevel, Resources.frmConfiguration_srsKeyLogLevel_DefaultValue);
            var bLogToConsole = Convert.ToBoolean(srsLocalRegistrySettings.GetInteger(Resources.frmConfiguration_srsKeyLogToConsole, Convert.ToInt32(Convert.ToBoolean(Resources.frmConfiguration_srsKeyLogToConsole_DefaultValue))));
            var bVerboseLogging = Convert.ToBoolean(srsLocalRegistrySettings.GetInteger(Resources.frmConfiguration_srsKeyVerboseLogging, Convert.ToInt32(Convert.ToBoolean(Resources.frmConfiguration_srsKeyVerboseLogging_DefaultValue))));

            if (!bLogToConsole)
                return;
            
            if (bVerboseLogging || bLocalMandatoryMessage) {
                if (bLocalMandatoryMessage)
                    Console.WriteLine(@"[Mandatory]: " + strLocalMessage);
                else
                    Console.WriteLine(@"[Verbose]: " + strLocalMessage);
            }
            else {
                switch (strLogLevel) {
                    case "Information": {
                        Console.WriteLine(@"[" + (EventLogEntryType)ltLocalLogType + @"]: " + strLocalMessage);

                        break;
                    }
                    case "Warning": {
                        if (ltLocalLogType is LogType.Warning or LogType.Error)
                            Console.WriteLine(@"[" + (EventLogEntryType)ltLocalLogType + @"]: " + strLocalMessage);

                        break;
                    }
                    case "Error": {
                        if (ltLocalLogType is LogType.Error)
                            Console.WriteLine(@"[" + (EventLogEntryType)ltLocalLogType + @"]: " + strLocalMessage);

                        break;
                    }
                }
            }
        }

        private void WriteWindowsEvent(string strLocalMessage, int intLocalID, LogType ltLocalLogType) {
            var strLogLevel = srsLocalRegistrySettings.GetString(Resources.frmConfiguration_srsKeyLogLevel, Resources.frmConfiguration_srsKeyLogLevel_DefaultValue);
            var bVerboseLogging = Convert.ToBoolean(srsLocalRegistrySettings.GetInteger(Resources.frmConfiguration_srsKeyVerboseLogging, Convert.ToInt32(Convert.ToBoolean(Resources.frmConfiguration_srsKeyVerboseLogging_DefaultValue))));

            if (bVerboseLogging)
                EventLog.WriteEntry(strEventSource, strLocalMessage, (EventLogEntryType)ltLocalLogType, intLocalID);
            else {
                switch (strLogLevel) {
                    case "Information": {
                        EventLog.WriteEntry(strEventSource, strLocalMessage, (EventLogEntryType)ltLocalLogType, intLocalID);

                        break;
                    }
                    case "Warning": {
                        if (ltLocalLogType is LogType.Warning or LogType.Error)
                            EventLog.WriteEntry(strEventSource, strLocalMessage, (EventLogEntryType)ltLocalLogType, intLocalID);

                        break;
                    }
                    case "Error": {
                        if (ltLocalLogType is LogType.Error)
                            EventLog.WriteEntry(strEventSource, strLocalMessage, (EventLogEntryType)ltLocalLogType, intLocalID);

                        break;
                    }
                }
            }
		}

		private static void EstablishEventSource(string strLocalEventSourceName, string strLocalLogName) {
            if (!EventLog.SourceExists(strLocalEventSourceName))
                EventLog.CreateEventSource(strLocalEventSourceName, strLocalLogName);
        }
	}
}