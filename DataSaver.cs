namespace Collections {
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using UnityEngine;

    public static class DataSaver {
        public static void SaveBinary(object @object, string savePath) {
            var file = File.Create(savePath);
            var bf = new BinaryFormatter();
            bf.Serialize(file, @object);
            file.Close();
        }

        public static bool TryLoadBinary(string loadPath, out object @object) {
            try {
                @object = LoadBinary(loadPath);
                return @object != null;
            } catch {
                @object = null;
                return false;
            }
        }

        private static object LoadBinary(string loadPath) {
            if (!File.Exists(loadPath)) return null;

            var file = File.Open(loadPath, FileMode.Open);
            var bf = new BinaryFormatter();
            var @object = bf.Deserialize(file);
            file.Close();
            return @object;
        }

        public static void SaveJson(object @object, string savePath) {
            var json = JsonConvert.SerializeObject(@object);
            File.WriteAllText(savePath, json);
        }

        public static bool TryLoadJson(string loadPath, out string json) {
            json = null;

            try {
                json = File.ReadAllText(loadPath);
                return true;
            } catch (IOException ex) when(ex is FileNotFoundException or DirectoryNotFoundException) {
                Debug.LogWarning($"File not found: {loadPath}");
                return false;
            }
        }

        public static bool TryLoadJson<T>(string loadPath, out T @object) {
            @object = default;

            try {
                @object = LoadJson<T>(loadPath);
                return @object != null;
            } catch (IOException ex) when(ex is FileNotFoundException or DirectoryNotFoundException) {
                Debug.LogWarning($"File not found: {loadPath}");
                return false;
            } catch (Exception exception) {
                Debug.LogException(exception);
                return false;
            }
        }

        private static T LoadJson<T>(string loadPath) {
            var json = File.ReadAllText(loadPath);
            var @object = JsonConvert.DeserializeObject<T>(json);
            return @object;
        }

        public static T ReadJsonPart<T>(FileInfo file, string key) {
            if (file.Exists) {
                var json = File.ReadAllText(file.FullName);
                var jObject = JObject.Parse(json);
                if (jObject.TryGetValue(key, out var jToken)) {
                    return jToken.ToObject<T>();
                }
            }

            return default;
        }
    }
}