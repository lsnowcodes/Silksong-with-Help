using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace SilksongWithHelp
{
    [HarmonyPatch(typeof(UIManager))]
    public static class HelpButtonCreation
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UIManager.ShowMenu))]
        public static void Prefix_ShowMenu(MenuScreen menu)
        {
            if (menu == null)
                return;

            // Only affect pause menu
            if (!menu)
                return;

            TryAddHelpButton(menu);
        }

        private static void TryAddHelpButton(MenuScreen menu)
        {
            // Already exists? Exit.
            if (menu.GetComponentInChildren<HelpButtonMarker>(true) != null)
                return;

            MenuButtonList list = menu.GetComponent<MenuButtonList>();
            if (list == null)
                return;

            PauseMenuButton template = menu.GetComponentInChildren<PauseMenuButton>(true);
            if (template == null)
                return;

            // Clone (inactive is fine, SetupActive will handle visibility)
            GameObject clone = UnityEngine.Object.Instantiate(template.gameObject, template.transform.parent);
            clone.name = "HelpButton";

            // Marker
            clone.AddComponent<HelpButtonMarker>();

            // Behavior
            var func = clone.AddComponent<HelpButtonFunc>();
            func.flashEffect = clone.GetComponent<Animator>();

            // Label
            Text label = clone.GetComponentInChildren<Text>(true);
            if (label != null)
                label.text = "Help";

            // Insert BEFORE SetupActive runs
            AddToButtonList(list, clone.GetComponent<PauseMenuButton>());
        }

        private static void AddToButtonList(MenuButtonList list, PauseMenuButton newButton)
        {
            var entryType = list.GetType().GetNestedType("Entry", BindingFlags.NonPublic);

            var entriesField = FindEntriesField(list);
            if (entriesField == null)
            {
                ModHandler.Log.LogError("[HelpButton] Could not locate Entry[] field on MenuButtonList");
                return;
            }


            var entries = (Array)entriesField.GetValue(list);
            if (entries == null || entries.Length == 0)
                return;

            // CLONE an existing entry (this is the magic)
            object templateEntry = entries.GetValue(0);
            object newEntry = System.Activator.CreateInstance(entryType, true);

            foreach (var field in entryType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                field.SetValue(newEntry, field.GetValue(templateEntry));
            }

            // Replace ONLY the button
            entryType.GetField("button", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(newEntry, newButton);

            // Append
            var newEntries = System.Array.CreateInstance(entryType, entries.Length + 1);
            entries.CopyTo(newEntries, 0);
            newEntries.SetValue(newEntry, entries.Length);
            entriesField.SetValue(list, newEntries);

            typeof(MenuButtonList)
                .GetField("isDirty", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(list, true);
        }

        private static FieldInfo FindEntriesField(MenuButtonList list)
        {
            var type = list.GetType();

            foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                // We are looking for: Entry[]
                if (!field.FieldType.IsArray)
                    continue;

                var elementType = field.FieldType.GetElementType();
                if (elementType == null)
                    continue;

                if (elementType.Name == "Entry")
                    return field;
            }

            return null;
        }


    }
}
