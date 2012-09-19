using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

using cMenu.Common;
using cMenu.Metadata;
using cMenu.Metadata.Attributes;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cMenu.IO
{
    public static class CSeriealizeExtensions
    {
        public static int SerializeBinaryFile(this object SerObject, string FileName)
        {
            return CSerialize.sSerializeBinaryFile(FileName, SerObject);
        }
        public static int SerializeXMLFile(this object SerObject, string FileName, Type ObjType)
        {
            return CSerialize.sSerializeXMLFile(FileName, SerObject, ObjType);
        }
        public static int SerializeJSONFile(this object SerObject, string FileName)
        {
            return CSerialize.sSerializeJSONFile(FileName, SerObject);
        }

        public static Stream SerializeBinaryStream(this object SerObject)
        {
            return CSerialize.sSerializeBinaryStream(SerObject);
        }
        public static Stream SerializeXMLStream(this object SerObject, Type ObjType)
        {
            return CSerialize.sSerializeXMLStream(SerObject, ObjType);
        }
        public static Stream SerializeJSONStream(this object SerObject)
        {
            return CSerialize.sSerializeJSONStream(SerObject);
        }

        public static T DeserializeBinaryStream<T>(this Stream Stream)
        {
            return (T)CSerialize.sDeserializeBinaryStream(Stream);
        }
        public static T DeserializeXMLStream<T>(this Stream Stream, Type ObjType)
        {
            return (T)CSerialize.sDeserializeXMLStream(Stream, ObjType);
        }
        public static T DeserializeJSONStream<T>(this Stream Stream, Type ObjType)
        {
            return (T)CSerialize.sDeserializeJSONStream(Stream, ObjType);
        }

        public static Stream ToDataStream(this byte[] Data)
        {
            return CSerialize.sGetStream(Data);
        }
        public static Stream ToDataStream(this string Data)
        {
            return CSerialize.sGetStream(Data);
        }
        public static byte[] ToDataByteArray(this MemoryStream Stream)
        {
            return CSerialize.sGetData(Stream);
        }
        public static byte[] ToDataByteArray(this Stream Stream)
        {
            return CSerialize.sGetData((MemoryStream)Stream);
        }
        public static string ToDataString(this MemoryStream Stream)
        {
            return CSerialize.sGetDataString(Stream);
        }
        public static string ToDataString(this Stream Stream)
        {            
            return CSerialize.sGetDataString((MemoryStream)Stream);
        }

        public static T Clone<T>(this T Object)
        {
            var S = Object.SerializeBinaryStream();
            return (T)CSerialize.sDeserializeBinaryStream(S);
        }
    }

    public class CSerialize
    {
        #region FS SERIALIZATION
        /// <summary>
        /// Бинарная сериализация
        /// </summary>
        /// <param name="FileName">Путь к файлу</param>
        /// <param name="SerObject">Сериализуемый объект</param>
        /// <returns>Код ошибки</returns>
        public static int sSerializeBinaryFile(string FileName, object SerObject)
        {

            try
            {
                IFormatter F = new BinaryFormatter();
                Stream S = File.Open(FileName, FileMode.Create, FileAccess.Write);
                F.Serialize(S, SerObject);
                S.Flush();
                S.Close();
            }
            catch (Exception ex)
            {
                return CErrors.ERR_FS_SERIALIZE;
            }

            return CErrors.ERR_SUC;
        }
        /// <summary>
        /// Бинарная десериализация
        /// </summary>
        /// <param name="FileName">Путь к файлу</param>
        /// <returns>Объект</returns>
        public static object sDeserializeBinaryFile(string FileName)
        {

            object Result = null;
            try
            {
                IFormatter F = new BinaryFormatter();
                Stream S = File.Open(FileName, FileMode.Open, FileAccess.Read);
                Result = F.Deserialize(S);
                S.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return Result;
        }
        /// <summary>
        /// XML сериализация
        /// </summary>
        /// <param name="FileName">Путь к файлу</param>
        /// <param name="SerObject">Сериализуемый объект</param>
        /// <param name="ObjType">Тип объекта</param>
        /// <returns>Код ошибки</returns>
        public static int sSerializeXMLFile(string FileName, object SerObject, Type ObjType)
        {
            try
            {
                XmlSerializer F = new XmlSerializer(ObjType);
                Stream S = File.Open(FileName, FileMode.Create, FileAccess.Write);
                F.Serialize(S, SerObject);
                S.Flush();
                S.Close();
            }
            catch (Exception ex)
            {
                return CErrors.ERR_FS_SERIALIZE;
            }

            return CErrors.ERR_SUC;
        }
        /// <summary>
        /// XML десериализация
        /// </summary>
        /// <param name="FileName">Путь к файлу</param>
        /// <param name="ObjType">Тип объекта</param>
        /// <returns>Объект</returns>
        public static object sDeserializeXMLFile(string FileName, Type ObjType)
        {
            object Result = null;
            try
            {
                XmlSerializer F = new XmlSerializer(ObjType);
                Stream S = File.Open(FileName, FileMode.Open, FileAccess.Read);
                Result = F.Deserialize(S);
                S.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return Result;
        }
        /// <summary>
        /// JSON сериализация
        /// </summary>
        /// <param name="FileName">Путь к файлу</param>
        /// <param name="SerObject">Сериализуемый объект</param>
        /// <returns>Код ошибки</returns>
        public static int sSerializeJSONFile(string FileName, object SerObject)
        {
            try
            {
                Stream S = File.Open(FileName, FileMode.Create, FileAccess.Write);
                TextWriter Writer = new StreamWriter(S);
                string Json = JsonConvert.SerializeObject(SerObject);
                Writer.WriteLine(Json);

                Writer.Flush();
                Writer.Close();
                S.Flush();
                S.Close();
            }
            catch (Exception ex)
            {
                return CErrors.ERR_FS_SERIALIZE;
            }

            return CErrors.ERR_SUC;
        }
        /// <summary>
        /// JSON десериализация
        /// </summary>
        /// <param name="FileName">Путь к файлу</param>
        /// <param name="ObjType">Тип объекта</param>
        /// <returns>Объект</returns>
        public static object sDeserializeJSONFile(string FileName, Type ObjType)
        {
            object Result = null;
            try
            {
                Stream S = File.Open(FileName, FileMode.Open, FileAccess.Read);
                TextReader Reader = new StreamReader(S);
                string Json = Reader.ReadToEnd();
                Result = JsonConvert.DeserializeObject(Json);
                Reader.Close();
                S.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return Result;
        }
        #endregion

        #region STREAM SERIALIZATION
        public static Stream sSerializeBinaryStream(object SerObject)
        {
            Stream S = new MemoryStream();
            try
            {
                IFormatter F = new BinaryFormatter();
                F.Serialize(S, SerObject);
                S.Flush();
            }
            catch (Exception ex)
            {
                S.Close();
                return null;
            }

            return S;
        }
        public static object sDeserializeBinaryStream(Stream Stream)
        {
            object Result = null;
            try
            {
                IFormatter F = new BinaryFormatter();
                Stream.Position = 0;
                Result = F.Deserialize(Stream);
                Stream.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return Result;
        }
        public static Stream sSerializeXMLStream(object SerObject, Type ObjType)
        {
            Stream S = new MemoryStream();
            try
            {
                XmlSerializer F = new XmlSerializer(ObjType);
                F.Serialize(S, SerObject);
                S.Flush();
            }
            catch (Exception ex)
            {
                S.Close();
                return null;
            }

            return S;
        }
        public static object sDeserializeXMLStream(Stream Stream, Type ObjType)
        {
            object Result = null;
            try
            {
                XmlSerializer F = new XmlSerializer(ObjType);
                Result = F.Deserialize(Stream);
                Stream.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return Result;
        }
        public static Stream sSerializeJSONStream(object SerObject)
        {
            Stream S = new MemoryStream();
            try
            {
                TextWriter Writer = new StreamWriter(S);
                string Json = JsonConvert.SerializeObject(SerObject);
                Writer.WriteLine(Json);
                Writer.Flush();
                S.Flush();
            }
            catch (Exception ex)
            {
                S.Close();
                return null;
            }

            return S;
        }
        public static object sDeserializeJSONStream(Stream Stream, Type ObjType)
        {
            object Result = null;
            try
            {
                TextReader Reader = new StreamReader(Stream);
                string Json = Reader.ReadToEnd();
                Result = JsonConvert.DeserializeObject(Json, ObjType);
                Reader.Close();
                Stream.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return Result;
        }
        #endregion

        #region DATA CONVERSION
        public static Stream sGetStream(byte[] Data)
        {
            MemoryStream Memory = new MemoryStream();
            Memory.Write(Data, 0, Data.Length);
            Memory.Flush();
            Memory.Position = 0;
            return Memory;
        }
        public static Stream sGetStream(string Data)
        {
            MemoryStream Memory = new MemoryStream();
            TextWriter Writer = new StreamWriter(Memory);
            Writer.WriteLine(Data);
            Writer.Flush();
            Memory.Flush();
            Memory.Position = 0;
            return Memory;
        }
        public static byte[] sGetData(MemoryStream Stream)
        {
            return (Stream == null ? new byte[0] : Stream.ToArray());
        }
        public static string sGetDataString(MemoryStream Stream)
        {
            if (Stream == null)
                return "";

            string R = "";
            Stream.Position = 0;
            TextReader Reader = new StreamReader(Stream);
            R = Reader.ReadToEnd();
            Reader.Close();
            return R;
        }
        #endregion
    }
}
