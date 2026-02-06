using BepInEx;
using BepInEx.Logging;
using GlobalEnums;
using HarmonyLib;
using SilksongWithHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Main class

[BepInPlugin("com.lsnowcodes.silksongwithhelp", "Silksong with Help", "1.0.0")]
public class ModHandler : BaseUnityPlugin
{
    internal static ManualLogSource Log;
    private void Awake()
    {
        Log = base.Logger;


        Logger.LogInfo("Plugin loaded and initialized...");
            
        Harmony.CreateAndPatchAll(typeof(ModHandler));
        Harmony.CreateAndPatchAll(typeof(HelpButtonCreation));
        Harmony.CreateAndPatchAll(typeof(HelpButtonPostCreation));

        Logger.LogInfo("Loaded!");

    }



        
}

