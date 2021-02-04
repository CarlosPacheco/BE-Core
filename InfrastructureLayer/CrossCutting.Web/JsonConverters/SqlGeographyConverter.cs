using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.SqlServer.Types;

namespace CrossCutting.Web.JsonConverters
{
    public class SqlGeographyConverter : JsonConverter<SqlGeography>
    {
        private const int _defaultSRID = 4326;

        public override SqlGeography Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return SqlGeography.Null;
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
                return SqlGeography.Null;
            }

            int srid;
            srid = (sridToken <= 0) ? _defaultSRID : sridToken;

            SqlGeography sqlGeography = SqlGeography.Point(latitudeToken, longitudeToken, srid);

            return sqlGeography;
        }

        public override void Write(Utf8JsonWriter writer, SqlGeography value, JsonSerializerOptions options)
        {

            if (value == null || value.IsNull)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();
            writer.WriteNumber("latitude", (double)value.Lat);
            writer.WriteNumber("longitude", (double)value.Long);
            writer.WriteNumber("srid", (int)value.STSrid);
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
            return typeof(SqlGeography) == typeToConvert;
        }
    }

}