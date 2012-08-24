using System.IO;
using System.Xml.Serialization;

namespace System
{
   /// <summary>
   /// These extensions must NEVER fail but be as much version agnostic as possible!!!
   /// </summary>
   public static class TrivialUnreliablePersistenceExtensions
   {
      public static T TrivialDeserialize<T>(this string s) where T : class
      {
         T r = default(T);

         if (!string.IsNullOrEmpty(s))
         {
            try
            {
               var xs = new XmlSerializer(typeof(T));
               byte[] buffer = Convert.FromBase64String(s);
               using (var ms = new MemoryStream(buffer))
               {
                  r = xs.Deserialize(ms) as T;
                  //todo: MSDN doesn't say which exceptions it may throw, gradually add the list
               }
            }
            catch (FormatException)
            {
               //string is not in base64 format
            }
         }

         return r;
      }

      /// <summary>
      /// Use to serialize simple "xml-compatible" classes
      /// </summary>
      /// <param name="o"></param>
      /// <returns></returns>
      public static string TrivialSerialize(this object o)
      {
         if (o == null) return null;

         //do not catch exceptions, incompatible classes must throw runtime errors

         var xs = new XmlSerializer(o.GetType());
         using (var ms = new MemoryStream())
         {
            xs.Serialize(ms, o);
            return Convert.ToBase64String(ms.ToArray());
         }
      }
   }
}
