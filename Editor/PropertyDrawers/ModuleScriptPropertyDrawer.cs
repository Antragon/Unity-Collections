namespace Collections.Editor.PropertyDrawers {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Modules;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(ModuleAttribute))]
    public class ModuleScriptPropertyDrawer : PropertyDrawer {
        private List<MonoScript> _availableScripts;

        private List<MonoScript> AvailableScripts => _availableScripts ??= InitializeAvailableScripts();

        private List<MonoScript> InitializeAvailableScripts() {
            var moduleAttribute = (ModuleAttribute)attribute;
            var type = moduleAttribute.ModuleType;
            return Assembly.GetAssembly(type)
                .GetTypes()
                .Where(t => type.IsAssignableFrom(t) && !t.IsAbstract)
                .Select(GetModuleScript)
                .ToList();
        }

        private static MonoScript GetModuleScript(Type type) {
            var path = type.FullName!.Replace('.', '/');
            var moduleScript = AssetDatabase.LoadAssetAtPath<MonoScript>($"Assets/Scripts/{path}.cs");
            return moduleScript;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var referenceValue = (MonoScript)property.objectReferenceValue;
            var value = AvailableScripts.IndexOf(referenceValue);
            var modulesNames = AvailableScripts.Select(s => s.GetClass().Name).ToArray();
            var newValue = EditorGUI.Popup(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), value, modulesNames);
            property.objectReferenceValue = _availableScripts.ElementAtOrDefault(newValue);
        }
    }
}