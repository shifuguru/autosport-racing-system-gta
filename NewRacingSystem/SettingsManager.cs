using System;
using System.IO;
using System.Windows.Forms;
using GTA;
using GTA.UI;
using Screen = GTA.UI.Screen;
using Control = GTA.Control;

namespace ARS
{
    class SettingsManager
    {
		public static bool modEnabled = true;
		public static bool debugEnabled = true;
		public static string settingsDirectory = "scripts\\ARS";
		public static string settingsFilePath = Path.Combine(settingsDirectory, "settings.ini");
		public static ScriptSettings settings = ScriptSettings.Load(settingsFilePath);
		public static string log = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ARS", "ARS.log");
		// Other Settings: 
		public static Keys menuToggleKey = Keys.F10; // Use this Key to open the Mod's Menu. 

		public static void CreateIni(string filePath)
		{
			using (StreamWriter writer = new StreamWriter(filePath))
			{
				writer.WriteLine("[Options]");
				// 
				writer.WriteLine($"Mod Enabled = {modEnabled}");
				writer.WriteLine($"Debug Enabled = {debugEnabled}");
				writer.WriteLine($"Menu Key = {menuToggleKey}");
				// writer.WriteLine($"");
			}
		}


		public static void LoadSettings()
		{
			try
			{
				if (!Directory.Exists(settingsDirectory))
                {
					Directory.CreateDirectory(settingsDirectory);
                }
                if (!File.Exists(settingsFilePath))
                {
                    CreateIni(settingsFilePath);
                }

				settings = ScriptSettings.Load(settingsFilePath);
                if (settings != null)
                {
                    modEnabled = settings.GetValue<bool>("Options", "Mod Enabled", modEnabled);
                    debugEnabled = settings.GetValue<bool>("Options", "Debug Enabled", debugEnabled);
                    menuToggleKey = settings.GetValue<Keys>("Options", "Menu Key", menuToggleKey);

					SaveSettings();

					if (debugEnabled)
						Notification.Show($"Loaded Advanced Interaction System settings", true);
                }
                else
                {
                    // Loading Failed! 
					if (debugEnabled)
                    {
						Notification.Show($"~r~Warning!: Loading Advanced Interaction System Settings failed.~s~", false);
                    }
                }
            }
            catch (Exception ex)
            {
				LogException("SettingsManager.LoadSettings", ex);
            }
        }

		public static void SaveSettings()
        {
            try
            {
                if (settings != null)
                {
                    settings.SetValue<bool>("Options", "Mod Enabled", modEnabled);
                    settings.SetValue<bool>("Options", "Debug Enabled", debugEnabled);
					settings.SetValue<Keys>("Options", "Menu Key", menuToggleKey);

					settings.Save();

					if (debugEnabled)
					{
						Notification.Show($"Saved Advanced Interaction System settings", true);
					}
				}
				else
				{
					// Saving Failed!
					if (debugEnabled)
					{
						Screen.ShowSubtitle($"~r~Warning!: Saving Advanced Interaction System Settings failed.~s~", 500);
					}
				}
			}
			catch (Exception ex)
			{
				LogException("SettingsManager.LoadSettings", ex);
			}

		}
		// EXCEPTION LOGGING: 
		public static void LogException(string methodName, Exception ex)
		{
			try
			{
				string message = $"[{DateTime.Now}] Error in {methodName} method. Exception: {ex.Message}";
				File.AppendAllText(log, $"{message}{Environment.NewLine}");
			}
			catch (Exception ex0)
			{
				Console.WriteLine($"Failed to log exception: {ex0.Message}");
			}
		}
	}
}
