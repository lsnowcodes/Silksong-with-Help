using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SilksongWithHelp
{
    [HarmonyPatch(typeof(UIManager))]
    public class PauseMenu
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(UIManager.GoToPauseMenu))]
        static void CreateHelpButton(UIManager __instance)
        {
            MenuScreen pauseMenu = __instance.pauseMenuScreen;

            if (pauseMenu == null)
            {
                return;
            }


        }
    }
}
