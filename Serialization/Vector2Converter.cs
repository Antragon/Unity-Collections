namespace Collections.Serialization {
    using System;
    using Newtonsoft.Json;
    using UnityEngine;

    public class Vector2Converter : JsonConverter<Vector2?> {
        public override Vector2? ReadJson(JsonReader reader, Type objectType, Vector2? existingValue, bool hasExistingValue, JsonSerializer serializer) {
            if (reader.TokenType != JsonToken.StartObject) throw new JsonReaderException("Invalid Vector2 format");
            float x = 0, y = 0;
            while (reader.Read()) {
                if (reader.TokenType == JsonToken.PropertyName) {
                    var propertyName = reader.Value!.ToString();
                    reader.Read();

                    switch (propertyName.ToLower()) {
                        case "x":
                            x = Convert.ToSingle(reader.Value);
                            break;
                        case "y":
                            y = Convert.ToSingle(reader.Value);
                            break;
                    }
                } else if (reader.TokenType == JsonToken.EndObject) {
                    break;
                }
            }

            return new Vector2(x, y);
        }

        public override void WriteJson(JsonWriter writer, Vector2? value, JsonSerializer serializer) {
            if (!value.HasValue) return;
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(value.Value.x);
            writer.WritePropertyName("y");
            writer.WriteValue(value.Value.y);
            writer.WriteEndObject();
        }
    }
}