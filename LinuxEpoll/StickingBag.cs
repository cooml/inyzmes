using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace LinuxEpoll
{
  public class StickingBag
  {


    /// <summary> /// 处理粘包信息
    /// </summary>
    /// <param name="socket">发送时即将传递的Socket</param>
    /// <param name="byteRecive">接收的字节流</param>
    /// <param name="dicBufferIndex">粘包的缓存区索引（目前使用终结点）</param>
    /// <param name="dicBufferDic">句柄所在数据群</param>
    /// <returns>一个或者多个完整的数据包集合</returns>
    public static List<byte[]> MakeStickingBag(byte[] byteRecive, int dicBufferIndex, ConcurrentDictionary<int, DynamicBufferManager> dicBufferDic)
    {

      //var complete = new List<byte[]>();//存放完整的数据包
      var completeByteArrList = new List<byte[]>();
      try
      {

        // var byteRecive = reciveByteArr;

        //判断粘包缓存区是否存在数据
        if (dicBufferDic.ContainsKey(dicBufferIndex))
        {
          dicBufferDic[dicBufferIndex].WriteBuffer(byteRecive);

          var tempByteArr = dicBufferDic[dicBufferIndex].Buffer;

          int commandLen = BitConverter.ToInt32(tempByteArr, 0);

          while (dicBufferDic[dicBufferIndex].GetDataCount() >= (commandLen + 4) && dicBufferDic[dicBufferIndex].GetDataCount() > 0)
          {

            var tempArr = new byte[commandLen];

            Array.Copy(tempByteArr, sizeof(int), tempArr, 0, commandLen);

            dicBufferDic[dicBufferIndex].Clear(commandLen + 4);

            completeByteArrList.Add(tempArr);

            if (dicBufferDic[dicBufferIndex].GetDataCount() <= 4)
            {
              break;
            }

            commandLen = BitConverter.ToInt32(tempByteArr, 0); //取出命令长度

          }
        }
        else
        {

          var dy = new DynamicBufferManager(1024);//新建一个DynamicBufferManager，如果处理完后有不完整的包就放入到缓存区

          dy.WriteBuffer(byteRecive);

          var tempByteArr = dy.Buffer;

          int commandLen = BitConverter.ToInt32(tempByteArr, 0);

          while (dy.GetDataCount() >= (commandLen + 4) && dy.GetDataCount() > 0)
          {

            var tempArr = new byte[commandLen];

            Array.Copy(tempByteArr, sizeof(int), tempArr, 0, commandLen);

            dy.Clear(commandLen + 4);

            completeByteArrList.Add(tempArr);

            if (dy.GetDataCount() <= 4)
            {
              break;
            }

            commandLen = BitConverter.ToInt32(tempByteArr, 0); //取出命令长度

          }

          //如果当前的 byte 数组 里面有剩余字节，就加到粘包的缓存区
          if (dy.GetDataCount() > 0)
          {
            dicBufferDic.TryAdd(dicBufferIndex, dy);
          }

        }

        return completeByteArrList;
        //complete.Add(completeByteArrList);
        //return complete;

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return completeByteArrList;
      }

    }





  }
}
