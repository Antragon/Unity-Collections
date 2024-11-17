namespace Collections {
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using UnityEngine;

    public static class DataSaver {
        public static void SaveJson(string savePath, object @object) {
            var json = JsonConvert.SerializeObject(@object);
            File.WriteAllText(savePath, json);
        }

        public static Task SaveJsonAsync(string savePath, object @object, CancellationToken token) {
            var json = JsonConvert.SerializeObject(@object);
            return File.WriteAllTextAsync(savePath, json, token);
        }

        public static bool TryLoadFile(string loadPath, out string? content) {
            content = null;

            try {
                content = File.ReadAllText(loadPath);
                return true;
            } catch (IOException ex) when (ex is FileNotFoundException or DirectoryNotFoundException) {
                Debug.LogWarning($"File not found: {loadPath}");
                return false;
            }
        }

        public static bool TryLoadJson<T>(string loadPath, out T? @object) {
            @object = default;

            try {
                @object = LoadJson<T>(loadPath);
                return @object != null;
            } catch (IOException ex) when (ex is FileNotFoundException or DirectoryNotFoundException) {
                Debug.LogWarning($"File not found: {loadPath}");
                return false;
            } catch (Exception exception) {
                Debug.LogException(exception);
                return false;
            }
        }

        private static T LoadJson<T>(string loadPath) {
            var json = File.ReadAllText(loadPath);
            var @object = JsonConvert.DeserializeObject<T>(json)!;
            return @object;
        }

        public static T? ReadJsonPart<T>(FileInfo file, string key) {
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