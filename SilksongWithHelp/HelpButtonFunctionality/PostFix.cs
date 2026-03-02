using HarmonyLib;
using UnityEngine.UI;

namespace SilksongWithHelp.HelpButtonFunctionality
{
    public static class PostFix
    {
        [HarmonyPatch(typeof(MenuButtonList), "SetupActive")]
        [HarmonyPostfix]
        public static void SetupActive_Postfix(MenuButtonList __instance)
        {
            foreach (var marker in __instance.GetComponentsInChildren<Marker>(true))
            {
                var text = marker.GetComponentInChildren<Text>(true);
                if (text != null)
                    text.text = "Help";

            }
        }
    }

}
