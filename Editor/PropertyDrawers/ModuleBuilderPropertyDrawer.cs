namespace Collections.Editor.PropertyDrawers {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Modules;
    using MoreLinq;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(ModuleBuilder))]
    public class ModuleBuilderPropertyDrawer : PropertyDrawer {
        private readonly List<MonoScript> _availableScripts = new();

        private static MonoScript GetModuleScript(Type type) {
            var path = type.FullName!.Replace('.', '/');
            var moduleScript = AssetDatabase.LoadAssetAtPath<MonoScript>($"Assets/Scripts/{path}.cs");
            return moduleScript;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            Initialize(property);
            var moduleScriptProperty = property.FindPropertyRelative("_moduleScript");
            var referenceValue = (MonoScript)moduleScriptProperty.objectReferenceValue;
            var value = _availableScripts.IndexOf(referenceValue);
            var modulesNames = _availableScripts.Select(s => s.GetClass().Name).ToArray();
            var newValue = EditorGUI.Popup(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), value, modulesNames);
            moduleScriptProperty.objectReferenceValue = _availableScripts.ElementAtOrDefault(newValue);
        }

        private void Initialize(SerializedProperty property) {
            if (_availableScripts.Any()) return;
            var propertyHolderType = property.serializedObject.targetObject.GetType();
            var moduleRunnerAttribute = propertyHolderType.GetCustomAttribute<ModuleRunnerAttribute>();
            if (moduleRunnerAttribute == null) {
                Debug.LogWarning($"{propertyHolderType.Name} inherits from {nameof(ModuleRunner)} but is missing {nameof(ModuleRunnerAttribute)}");
                return;
            }

            var baseType = moduleRunnerAttribute.ModuleBaseType;
            Assembly.GetAssembly(baseType)
                .GetTypes()
                .Where(t => baseType.IsAssignableFrom(t) && !t.IsAbstract)
                .Select(GetModuleScript)
                .ForEach(_availableScripts.Add);
        }
    }
}