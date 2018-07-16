using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TestCaseManager.Utilities
{
    public static class DeepCloneUtility
    {
        public static T DeepClone<T>(T obj)
        {
            T objResult;
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Position = 0;
                objResult = (T) bf.Deserialize(ms);
            }

            return objResult;
        }
    }
}