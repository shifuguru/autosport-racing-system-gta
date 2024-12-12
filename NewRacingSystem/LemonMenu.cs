using GTA;
using LemonUI;
using LemonUI.Menus;
using System;
using Screen = GTA.UI.Screen;

namespace ARS
{
    public class LemonMenu : Script
    {
        public static bool modEnabled = SettingsManager.modEnabled;
        public static bool debugEnabled = SettingsManager.debugEnabled;

        public static readonly ObjectPool pool = new ObjectPool();
        public static readonly NativeMenu menu = new NativeMenu("ARS", "Main Menu", "");
        // public static readonly NativeMenu exampleMenu = new NativeMenu("Extra Module", "Extra Module", "Additional Settings:");

        // Toggles: 
        private static readonly NativeCheckboxItem modEnabledToggle = new NativeCheckboxItem("Mod Enabled: ", "Enables/Disables the Mod", SettingsManager.modEnabled);
        private static readonly NativeCheckboxItem debugEnabledToggle = new NativeCheckboxItem("Debug Enabled: ", "Enables Debug Notifications. Recommended: False", SettingsManager.debugEnabled);

        public LemonMenu()
        {
            LoadMenu();
            Tick += OnTick;
        }

        public void OnTick(object sender, EventArgs e)
        {
            pool.Process();
        }

        public static void LoadMenu()
        {
            pool.Add(menu);
            //
            menu.Add(modEnabledToggle);
            menu.Add(debugEnabledToggle);

            // 
            modEnabledToggle.Activated += ToggleMod;
            debugEnabledToggle.Activated += ToggleDebug;
            //
            modEnabledToggle.Checked = modEnabledToggle.Checked;
            debugEnabledToggle.Checked = debugEnabledToggle.Checked;
        }
        public static void OpenMenu()
        {
            menu.Visible = true;
        }
        public static void CloseMenu()
        {
            menu.Visible = false;
            SettingsManager.SaveSettings();
        }
        public static void ToggleMod(object sender, EventArgs e)
        {
            SettingsManager.modEnabled = !SettingsManager.modEnabled;
            modEnabledToggle.Checked = SettingsManager.modEnabled;
            Screen.ShowSubtitle($"Advanced Interaction System Enabled: {SettingsManager.modEnabled}", 1500);
            SettingsManager.SaveSettings();
        }
        public static void ToggleDebug(object sender, EventArgs e)
        {
            SettingsManager.debugEnabled = !SettingsManager.debugEnabled;
            debugEnabledToggle.Checked = SettingsManager.debugEnabled;
            Screen.ShowSubtitle($"Debug Enabled: {SettingsManager.debugEnabled}", 1500);
            SettingsManager.SaveSettings();
        }
    }
}
