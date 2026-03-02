using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SilksongWithHelp;
using SilksongWithHelp.HelpButtonFunctionality;

// Main class

[BepInPlugin("com.lsnowcodes.silksongwithhelp", "Silksong with Help", "1.0.0")]
public class ModHandler : BaseUnityPlugin
{
    public static ManualLogSource Log;
    private void Awake()
    {
        Log = Logger;

        Log.LogInfo("Plugin loaded and initialized...");
            
        Harmony.CreateAndPatchAll(typeof(ModHandler));
        Harmony.CreateAndPatchAll(typeof(Creation));
        Harmony.CreateAndPatchAll(typeof(PostFix));
        Harmony.CreateAndPatchAll(typeof(Prefix));

        Log.LogInfo("Loaded!");

    }



        
}

