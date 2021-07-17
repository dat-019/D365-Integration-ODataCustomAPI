using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Plugin_DynamicServices
{
    public static class Extension
    {
        public static string SerializeObject<T>(T obj)
        {
            var json = string.Empty;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, obj);
                memoryStream.Position = 0;
                using (StreamReader reader = new StreamReader(memoryStream))
                    json = reader.ReadToEnd();
            }
            return json;
        }
        public static T DeSerializeObject<T>(string json)
        {
            object obj = null;
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = true });
            obj = serializer.ReadObject(stream);
            stream.Close();
            return (T)obj;
        }

        public static string SerializeDictionary<T>(T obj)
        {
            var json = string.Empty;
            DataContractJsonSerializerSettings setting = new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = true };
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), setting);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, obj);
                memoryStream.Position = 0;
                using (StreamReader reader = new StreamReader(memoryStream))
                    json = reader.ReadToEnd();
            }
            return json;
        }

        public static T DeSerializeDictionary<T>(string json)
        {
            object obj = null;
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = true });
            obj = serializer.ReadObject(stream);
            stream.Close();
            return (T)obj;
        }

        public static T ConvertValue<T, U>(U value) where U : IConvertible
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static T ConvertValue<T>(this object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static string SerializeObjectToXml<T>(T obj)
        {
            var xml = string.Empty;

            using (StringWriter wr = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(wr, obj);
                xml = wr.ToString();
            }
            return xml;
        }

        public static T DeSerializeXmlToObject<T>(string xml)
        {
            object obj = null;
            using (StringReader rd = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                obj = serializer.Deserialize(rd);
            }
            return (T)obj;
        }
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string GetMd5Hash(this string input)
        {
            MD5 md5Hash = new MD5CryptoServiceProvider();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public static TokenModel RandomToken(List<TokenModel> tokens)
        {
            Random rnd = new Random();
            int index = rnd.Next(tokens.Count);
            return tokens[index];
        }
        
    }
}
