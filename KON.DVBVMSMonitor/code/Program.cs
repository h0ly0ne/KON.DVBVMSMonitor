using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

using Topshelf;

namespace KON.DVBVMSMonitor {
    [SupportedOSPlatform("windows")]
    static class Program {
        static void Main(string[] args) {
            if (Environment.UserInteractive) {
                var strArguments = string.Concat(args);

                if (!Global.IsAdministrator())
                    MessageBox.Show(Resources.Program_AdministratorRequired, Resources.Program_Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                switch (strArguments) {
                    case "install": {
                        InstallService();
                        break;
                    }
                    case "uninstall": {
                        UnInstallService();
                        break;
                    }
                    case "console": {
                        new Service().RunAsConsole(args);
                        break;
                    }
                    default: {
                        Global.ShowWindow(Global.GetConsoleWindow(), 0);
                        new frmConfiguration().ShowDialog();
                        break;
                    }
                }
            }
            else
                Environment.ExitCode = ServiceFactory();
        }

        public static void InstallService() {
            try {
                Global.welCurrentWindowsEventLogger.WriteEntry(Resources.Service_Installing + string.Empty.Space() + Environment.ProcessPath, 0, WindowsEventLogger.LogType.Information);
                Global.welCurrentWindowsEventLogger.WriteConsole(Resources.Service_Installing + string.Empty.Space() + Environment.ProcessPath, WindowsEventLogger.LogType.Information, true);
                Environment.ExitCode = ServiceFactory();
            }
            catch (Exception excCurrentException) {
                Global.welCurrentWindowsEventLogger.WriteEntry(Resources.Service_Uninstalling_Error + string.Empty.Space() + excCurrentException.Message, 0, WindowsEventLogger.LogType.Error);
                Global.welCurrentWindowsEventLogger.WriteConsole(Resources.Service_Uninstalling_Error + string.Empty.Space() + excCurrentException.Message, WindowsEventLogger.LogType.Error, true);
            }
        }
        public static void UnInstallService() {
            try {
                Global.welCurrentWindowsEventLogger.WriteEntry(Resources.Service_Uninstalling + string.Empty.Space() + Environment.ProcessPath, 0, WindowsEventLogger.LogType.Information);
                Global.welCurrentWindowsEventLogger.WriteConsole(Resources.Service_Uninstalling + string.Empty.Space() + Environment.ProcessPath, WindowsEventLogger.LogType.Information, true);
                Environment.ExitCode = ServiceFactory();
            }
            catch (Exception excCurrentException) {
                Global.welCurrentWindowsEventLogger.WriteEntry(Resources.Service_Uninstalling_Error + string.Empty.Space() + excCurrentException.Message, 0, WindowsEventLogger.LogType.Error);
                Global.welCurrentWindowsEventLogger.WriteConsole(Resources.Service_Uninstalling_Error + string.Empty.Space() + excCurrentException.Message, WindowsEventLogger.LogType.Error, true);
            }
        }

        public static int ServiceFactory() {
            var tecCurrentTopshelfExitCode = HostFactory.Run(hcCurrentHostConfigurator => {
                hcCurrentHostConfigurator.Service<Service>(scCurrentServiceConfigurator => { scCurrentServiceConfigurator.ConstructUsing(_ => new Service()); scCurrentServiceConfigurator.WhenStarted(sfCurrentServiceFactory => sfCurrentServiceFactory.Start()); scCurrentServiceConfigurator.WhenStopped(sfCurrentServiceFactory => sfCurrentServiceFactory.Stop()); });
                hcCurrentHostConfigurator.RunAsLocalSystem();
                hcCurrentHostConfigurator.StartAutomatically();
                hcCurrentHostConfigurator.EnableServiceRecovery(srcCurrentServiceRecoveryConfiguration => { srcCurrentServiceRecoveryConfiguration.RestartService(1); });
                hcCurrentHostConfigurator.SetServiceName(Resources.Program_Name);
                hcCurrentHostConfigurator.SetDisplayName(Resources.Program_Name);
                hcCurrentHostConfigurator.SetDescription(Resources.Program_Name);
            });

            return (int)Convert.ChangeType(tecCurrentTopshelfExitCode, tecCurrentTopshelfExitCode.GetTypeCode());
        }
    }
}