using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SilksongWithHelp
{
    public class HelpButtonFunc : MonoBehaviour, ISubmitHandler
    {
        public enum PauseButtonType { Help }

        public PauseButtonType pauseButtonType;
        public Animator flashEffect;

        public void OnSubmit(BaseEventData eventData)
        {
            var ui = GameManager.instance.ui;
            var ih = GameManager.instance.inputHandler;

            ModHandler.Log.LogMessage("Help button was clicked!");

            if (!GameManager.instance.inputHandler.PauseAllowed)
            {
                return;
            }

            if (this.flashEffect != null)
            {
                this.flashEffect.ResetTrigger("Flash");
                this.flashEffect.SetTrigger("Flash");
            }

            var forceDeselect = typeof(MenuSelectable).GetMethod("ForceDeselect",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            forceDeselect?.Invoke(GetComponent<PauseMenuButton>(), null);

            var playSound = typeof(MenuSelectable).GetMethod("PlaySubmitSound",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            playSound?.Invoke(GetComponent<PauseMenuButton>(), null);

            ModHandler.Log.LogInfo("[HelpButton] Clicked!");

        }
        
    }
}
