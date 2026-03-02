using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SilksongWithHelp.HelpButtonFunctionality
{
    public static class Prefix
    {
        // Rewiring the button to use the help logic
        [HarmonyPatch(typeof(PauseMenuButton), "OnSubmit")]
        [HarmonyPrefix]
        public static bool OnSubmit_Prefix(PauseMenuButton __instance, BaseEventData eventData)
        {
            if (__instance.gameObject.name != "Help")
            {
                return true;
            }

            var submitLogic = __instance.GetComponent<Function>();

            if (submitLogic != null)
            {
                submitLogic.OnSubmit(eventData);
            }

            return false;
        }
    }
}
