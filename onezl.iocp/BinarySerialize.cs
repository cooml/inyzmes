﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace onezl.iocp
{
  public class BinarySerialize
  {
    public static void Serialize<T>(T t, string strFile)
    {

      using (FileStream fs = new FileStream(strFile, FileMode.Create))
      {

        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Serialize(fs, t);

      }

    }

    public static T DeSerialize<T>(string strFile)
    {

      T t = default(T);
      if (File.Exists(strFile))
      {
        using (FileStream fs = new FileStream(strFile, FileMode.Open))
        {
          BinaryFormatter formatter = new BinaryFormatter();
          t = (T)formatter.Deserialize(fs);
        }
      }


      return t;

    }
  }
}
