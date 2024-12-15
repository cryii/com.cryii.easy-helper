using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace CryII.EasyHelper
{
    public static class JsonHelper
    {
        public static bool TryFromJson<T>(string path, out T t)
        {
            t = default;
            if (!File.Exists(path) || System.IO.Path.GetExtension(path) != ".json")
            {
                return false;
            }

            var jsonString = File.ReadAllText(path);
            t = JsonUtility.FromJson<T>(jsonString);
            return t != null;
        }

        public static bool TryToJson<T>(T t, string path)
        {
            if (t == null)
            {
                return false;
            }

            var jsonString = JsonUtility.ToJson(t);
            if (!File.Exists(path))
            {
                return false;
            }

            File.WriteAllText(path, jsonString);
            return true;
        }

        public static int TryParseInt(string jsonString)
        {
            return int.TryParse(jsonString, out var result) ? result : 0;
        }

        public static float TryParseFloat(string jsonString)
        {
            return float.TryParse(jsonString, out var result) ? result : 0f;
        }

        public static int TryReadInt(JObject jObject, string key)
        {
            if (jObject == null)
            {
                return 0;
            }

            if (jObject.TryGetValue(key, out var value))
            {
                return TryParseInt(value.ToString());
            }

            return 0;
        }

        public static float TryReadFloat(JObject jObject, string key)
        {
            if (jObject == null)
            {
                return 0f;
            }

            if (jObject.TryGetValue(key, out var value))
            {
                return TryParseFloat(value.ToString());
            }

            return 0f;
        }

        public static string TryReadString(JObject jObject, string key)
        {
            if (jObject == null)
            {
                return "";
            }

            if (jObject.TryGetValue(key, out var value))
            {
                return value.ToString();
            }

            return "";
        }

        public static IEnumerable<int> TryReadInts(JObject jObject, string key)
        {
            if (jObject == null)
            {
                return Array.Empty<int>();
            }

            if (jObject.TryGetValue(key, out var value))
            {
                var values = value.Values();
                var listInts = values.Select(v => TryParseInt(v.ToString()));
                return listInts;
            }

            return Array.Empty<int>();
        }

        public static IEnumerable<float> TryReadFloats(JObject jObject, string key)
        {
            if (jObject == null)
            {
                return Array.Empty<float>();
            }

            if (jObject.TryGetValue(key, out var value))
            {
                var values = value.Values();
                var listFloats = values.Select(v => TryParseFloat(v.ToString()));
                return listFloats;
            }

            return Array.Empty<float>();
        }

        public static IEnumerable<JProperty> TryReadProperties(JObject jObject, string key)
        {
            if (jObject == null)
            {
                return Array.Empty<JProperty>();
            }

            if (jObject.TryGetValue(key, out var value))
            {
                if (value is JObject jObjectValue)
                {
                    var properties = jObjectValue.Properties();
                    return properties;
                }
            }

            return Array.Empty<JProperty>();
        }
    }
}