using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using BepInEx.Logging;

namespace SilksongWithHelp.HelpButtonFunctionality
{
    public class Function : MonoBehaviour, ISubmitHandler
    {
        //public enum PauseButtonType { Help }

        public void OnSubmit(BaseEventData data)
        {
            HelpFunction();
        }

        public void HelpFunction()
        {
            ModHandler.Log.LogInfo("Help function works!");
        }
        
    }
}
