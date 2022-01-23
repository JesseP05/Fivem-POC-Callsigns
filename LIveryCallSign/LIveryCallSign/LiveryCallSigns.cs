using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace LiveryCallsign
{
    public class LiveryCallsign : BaseScript
    {
        public LiveryCallsign()
        {
            Tick += OnTick;
        }
        private async Task OnTick()
        {
            try
            {
                if (IsControlJustReleased(0, 73))
                {
                    Subtitles(await KeyboardInput("Set Callsign", "10-4", 10));
                    TestVehicle(Convert.ToInt32(await KeyboardInput("Set Callsign", "10-4", 10)));
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }
        public void Subtitles(string sub)
        {
            try
            {
                CitizenFX.Core.UI.Screen.ShowSubtitle(sub);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }


        }

        public async Task<string> KeyboardInput(string textEntry, string exampleText, int maxStringLenght)
        {
            //textEntry = Title i assume?
            //exampleText = written text in the field that can be deleted..
            //maxStringLenght = maximum of entries in the text field
            //FMMC_KEY_TIP1
            AddTextEntry("CALLSIGN", textEntry); //set title
            DisplayOnscreenKeyboard(1, "CALLSIGN", "", exampleText, "", "", "", maxStringLenght); // Call keyboard input

            DisableAllControlActions(0);

            while (UpdateOnscreenKeyboard() != 1 && UpdateOnscreenKeyboard() != 2)
            {
                await BaseScript.Delay(0);
            }
            if (UpdateOnscreenKeyboard() != 2)
            {
                string result = GetOnscreenKeyboardResult();
                Wait(500);
                EnableAllControlActions(0);
                return result;
            }
            else
            {
                Wait(500);
                EnableAllControlActions(0);
                return null;
            }
        }
        public void TestVehicle(int livery)
        {
            Ped player = Game.Player.Character;
            int curveh = player.CurrentVehicle.Handle;
            SetVehicleLivery(curveh, livery);
        }
    }
}

