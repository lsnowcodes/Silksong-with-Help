using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace SilksongWithHelp
{
    [HarmonyPatch(typeof(MenuButtonList), "SetupActive")]
    public static class HelpButtonPostCreation
    {
        static void Postfix(MenuButtonList __instance)
        {
            foreach (var marker in __instance.GetComponentsInChildren<HelpButtonMarker>(true))
            {
                var text = marker.GetComponentInChildren<Text>(true);
                if (text != null)
                    text.text = "Help";
            }
        }
    }

}
