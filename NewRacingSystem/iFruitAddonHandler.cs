using System;
using GTA;
using GTA.UI;
using Screen = GTA.UI.Screen;
using iFruitAddon2;

namespace ARS
{
    public class iFruitAddonHandler : Script
    {
        private string modName = ARS.ScriptName;
        CustomiFruit _iFruit;
        private bool debugEnabled = SettingsManager.debugEnabled;


        public iFruitAddonHandler()
        {
            LoadiFruitAddon();
            Tick += OnTick;
        }


        #region LOAD IFRUITADDON2 
        private void LoadiFruitAddon()
        {
            // Custom phone creation
            _iFruit = new CustomiFruit();

            // Phone customization (optional)
            /*
            _iFruit.CenterButtonColor = System.Drawing.Color.Orange;
            _iFruit.LeftButtonColor = System.Drawing.Color.LimeGreen;
            _iFruit.RightButtonColor = System.Drawing.Color.Purple;
            _iFruit.CenterButtonIcon = SoftKeyIcon.Fire;
            _iFruit.LeftButtonIcon = SoftKeyIcon.Police;
            _iFruit.RightButtonIcon = SoftKeyIcon.Website;
            */

            // New contact (wait 3 seconds (3000ms) before picking up the phone)
            iFruitContact contact = new iFruitContact($"{modName}");
            contact.Answered += ContactAnswered;   // Linking the Answered event with our function
            contact.DialTimeout = 3000;            // Delay before answering
            contact.Active = true;                 // true = the contact is available and will answer the phone
            contact.Icon = ContactIcon.Hao;      // Contact's icon
            _iFruit.Contacts.Add(contact);         // Add the contact to the phone
        }

        private void ContactAnswered(iFruitContact contact)
        {
            // The contact has answered: 
            if (debugEnabled)
            {
                Notification.Show($"{modName} Menu Opened");
            }

            if (!LemonMenu.menu.Visible)
            {
                LemonMenu.OpenMenu();
            }

            // We need to close the phone in a moment
            // We can close it as soon as the contact picks up by calling _iFruit.Close().
            // Here, we will close the phone in 5 seconds (5000ms). 
            _iFruit.Close();
        }
        #endregion

        #region ON TICK 
        private void OnTick(object sender, EventArgs e)
        {
            _iFruit.Update();
        }
        #endregion
    }
}
