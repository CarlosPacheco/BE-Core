using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;

namespace CrossCutting.Web.JsonConverters
{
    public class PointConverter : JsonConverter<Point>
    {
        private const int _defaultSRID = 4326;

        public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return Point.Empty;
            }

            double latitudeToken = 0;
            double longitudeToken = 0;
            int sridToken = 0;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                // Get the key.
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                string propertyName = reader.GetString();

                reader.Read();
                switch (propertyName)
                {
                    case "latitude":
                        latitudeToken = reader.GetDouble();
                        break;
                    case "longitude":
                        longitudeToken = reader.GetDouble();
                        break;
                    case "srid":
                        sridToken = reader.GetInt32();
                        break;
                }
            }

            if (latitudeToken <= 0 || longitudeToken <= 0)
            {
                return Point.Empty;
            }

            int srid;
            srid = (sridToken <= 0) ? _defaultSRID : sridToken;

            // use X for longitude and Y for latitude.
            Point point = new Point(longitudeToken, latitudeToken)
            {
                SRID = srid
            };
            return point;
        }

        public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
        {

            if (value == null || value.IsEmpty)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();
            writer.WriteNumber("latitude", (double)value.Y);
            writer.WriteNumber("longitude", (double)value.X);
            writer.WriteNumber("srid", value.SRID);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(Point) == typeToConvert;
        }
    }
}