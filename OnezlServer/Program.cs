using onezl.iocp;
using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text;

namespace OnezlServer
{
  class Program
  {
    public static ConcurrentDictionary<string, SocketAsyncEventArgs> userlist = new ConcurrentDictionary<string, SocketAsyncEventArgs>();
    public static ConcurrentDictionary<string, AsyncSocketUserToken> ABTEST = new ConcurrentDictionary<string, AsyncSocketUserToken>();


    public static ConcurrentDictionary<string, string> userid = new ConcurrentDictionary<string, string>();

    public static IoServer socketserver;
    static void Main(string[] args)
    {
      socketserver = new IoServer(60, 1024);

      socketserver.Start(6789);
      socketserver.ReceiveEvent = new onezl.iocp.ReceiveEventHandler(myReceive);
      socketserver.DownLineEvent = new onezl.iocp.DownLineHandler(Downline);
      socketserver.Name = "RegClean";//开启定时清理僵尸连接
      Console.WriteLine("Started Server");
    }
    private static void myReceive(onezl.iocp.AsyncSocketUserToken SocketArg, byte[] byteArr)
    {
      try
      {


        string strAll = Encoding.UTF8.GetString(byteArr);
        if (strAll.Length < 6)
        {
          return;//丢弃
        }
        Console.WriteLine("接受消息(" + SocketArg.IpportStr + "):" + strAll);
        string commandStr = strAll.Substring(0, 6);
        string[] strs = strAll.Split(new string[] { "[f.ff]" }, StringSplitOptions.RemoveEmptyEntries);
        switch (commandStr)
        {
          case "000001":
            if (strs.Length == 2)
            {
              if (!userid.Values.Contains(strs[1]) && !userid.ContainsKey(strs[1]) && !userid.ContainsKey(SocketArg.IpportStr))//有并发问题，先不考虑
              {
                userid.TryAdd(SocketArg.IpportStr, strs[1]);
                userid.TryAdd(strs[1], SocketArg.IpportStr);
                socketserver.PushSendQue(SocketArg.ReceiveEventArgs, Encoding.UTF8.GetBytes("注册成功！"));
                ABTEST.TryAdd(strs[1], SocketArg);
                //注册成功清理僵尸链接
                socketserver.RemoveZombieSocketAsyncEventArgs(SocketArg.ReceiveEventArgs);
              }
              else
              {
                socketserver.PushSendQue(SocketArg.ReceiveEventArgs, Encoding.UTF8.GetBytes("注册失败--id被重复或者已经注册其他id"));

              }
            }
            break;
          case "000002":
            if (strs.Length == 2)
            {
              if (userid.ContainsKey(strs[1]) && userid.ContainsKey(SocketArg.IpportStr) && ABTEST.ContainsKey(userid[SocketArg.IpportStr]) && ABTEST.ContainsKey(strs[1]))//存在要找的id
              {
                socketserver.PushSendQue(SocketArg.ReceiveEventArgs, Encoding.UTF8.GetBytes(userid[strs[1]]));
                socketserver.PushSendQue(ABTEST[strs[1]].ReceiveEventArgs, Encoding.UTF8.GetBytes(SocketArg.IpportStr));
              }
              else
              {
                socketserver.PushSendQue(SocketArg.ReceiveEventArgs, Encoding.UTF8.GetBytes("连接失败---id可能不存在"));
              }
            }
            break;
          default:
            break;
        }


      }
      catch (Exception e)
      {
        Console.WriteLine("exp:" + e.Message);

      }


    }
    private static void Downline(onezl.iocp.AsyncSocketUserToken SocketArg)
    {
      try
      {
        var ipstr = SocketArg.IpportStr;

        if (userid.ContainsKey(SocketArg.IpportStr))
        {
          string str = null;
          AsyncSocketUserToken o = null;
          userid.TryRemove(userid[SocketArg.IpportStr], out str);
          ABTEST.TryRemove(userid[SocketArg.IpportStr], out o);
          userid.TryRemove(SocketArg.IpportStr, out str);

        }


        Console.WriteLine("掉线客户端：" + ipstr);


      }
      catch (Exception e)
      {
        Console.WriteLine("exp:" + e.Message);

      }

    }

  }
}
