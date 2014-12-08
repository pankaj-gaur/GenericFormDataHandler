using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Generic.FormDataHandler.Helper
{
    public enum DataFormat
    {
        JSON,
        XML
    }
    public class ContentHelper
    {
        public static T ByteArrayToObject<T>(byte[] arrBytes) where T : class
        {
            
            BinaryFormatter binForm = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream())
            {
                try
                {
                    memStream.Write(arrBytes, 0, arrBytes.Length);
                    memStream.Seek(0, SeekOrigin.Begin);

                    T obj = (T)binForm.Deserialize(memStream);

                    return obj;
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    memStream.Dispose();
                    binForm = null;
                }
            }
        }

        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            else
            {
                BinaryFormatter binForm = new BinaryFormatter();
                using (MemoryStream memStream = new MemoryStream())
                {
                    try
                    {
                        binForm.Serialize(memStream, obj);
                        return memStream.ToArray();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                    finally
                    {
                        memStream.Dispose();
                        binForm = null;
                    }
                }
            }
        }

        public static T GetObjectFromJSON<T>(string jsonString) where T : class
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                try
                {
                    return (T)ser.ReadObject(memStream);
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    memStream.Dispose();
                    ser = null;
                }
            }
        }

        public string GetValueFromConfiguration(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                var value = ConfigurationManager.AppSettings[key];
                return value != null ? value : string.Empty;
            }
            return string.Empty;
        }

        public static string GetJSONFromXml(string formData)
        {
            // To convert an XML node contained in string xml into a JSON string   
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(formData);
                string jsonText = JsonConvert.SerializeXmlNode(doc);

                return jsonText;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string GetJSONFromObject<T>(T objectToConvert) where T : class
        {
            if (objectToConvert != null)
            {
                string jsonText = JsonConvert.SerializeObject(objectToConvert,Newtonsoft.Json.Formatting.Indented);
                return jsonText;
            }
            return string.Empty;
        }

        public static string GetXmlFromObject(object objectToConvert)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(objectToConvert.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, objectToConvert);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }
    }
}
