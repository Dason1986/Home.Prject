﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.Cores
{
    public class JWTPayload
    {
        [JsonProperty("sub")]
        public string Name { get; set; }
        [JsonProperty("auth")]
        public string Role { get; set; }
        [JsonProperty("exp")]
        [JsonConverter(typeof(UtcDateTimeConverter))]
        public DateTime Exp { get; set; }
    }
    public class TokenResult
    {
        public string Token { get; set; }

     
    }
    public class UtcDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;
            DateTime time = DateTime.MinValue;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            time = startTime.AddMilliseconds((long)reader.Value);
            return time;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var ticks = Decimal.ToInt64(Decimal.Divide(((DateTime)value).Ticks - 621355968000000000, 10000));
            writer.WriteValue(ticks);
        }
    }
    public class PassportConfig
    {
        public const string SecretKey = "HomeApp";
    }
    public class JWTHeader
    {
        public JWTHeader()
        {
            this.Type = "JWT";
        }

        public string Type { get; set; }

        public string Alg { get { return "HS256"; } }
    }
    /// <summary>
    /// 只支持HS256算法
    /// </summary>
    public class JWT
    {
        /// <summary>
        /// Token解码
        /// </summary>
        public T Decode<T>(string token, string key, bool noVerify = false)
        {
            var segments = token.Split('.');
            if (segments.Length != 3) throw new ArgumentException("Token格式不正确");

            var headerSeg = segments[0];
            var payloadSeg = segments[1];
            var signatureSeg = segments[2];

            var header = StringToJson<JWTHeader>(Base64Decrypt(headerSeg));
            var payload = StringToJson<T>(Base64Decrypt(payloadSeg));

            if (!noVerify)
            {
                if (!HmacSignature(headerSeg + "." + payloadSeg, key).Equals(signatureSeg))
                    throw new Exception("无效Token");
            }

            return payload;
        }

        /// <summary>
        /// Token编码
        /// </summary>
        public string Encode<T>(T payload, string key, JWTHeader header = null)
        {
            var segments = new List<string>();
            // 1、header
            if (header == null) header = new JWTHeader();
            segments.Add(Base64Encrypt(JsonToString(header)));
            // 2、payload
            segments.Add(Base64Encrypt(JsonToString(payload)));
            // 3、signature
            segments.Add(HmacSignature(String.Join(".", segments), key));

            // 最终以.号拼接做为Token
            return String.Join(".", segments);
        }

        #region helper

        private string Base64Encrypt(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        private string Base64Decrypt(string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }

        private string HmacSignature(string secret, string value)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var valueBytes = Encoding.UTF8.GetBytes(value);
            string signature;

            using (var hmac = new HMACSHA256(secretBytes))
            {
                var hash = hmac.ComputeHash(valueBytes);
                signature = Convert.ToBase64String(hash);
            }
            return signature;
        }

        private string JsonToString<T>(T obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        private T StringToJson<T>(string value)
        {
            if (String.IsNullOrEmpty(value)) return default(T);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }

        #endregion


    }
}
