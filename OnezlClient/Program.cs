using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace OnezlClient
{
  class Program
  {
    static Socket socketSend;
    static Socket socketClient;
    static bool T = false;
    private static byte[] result = new byte[1024];
    static void Main(string[] args)
    {

      socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      IPAddress ip = IPAddress.Parse("127.0.0.1");
      IPEndPoint point = new IPEndPoint(ip, 6789);
      socketSend.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
      socketSend.Connect(point);

      socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      socketClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
      socketClient.Bind(new IPEndPoint(IPAddress.Parse(socketSend.LocalEndPoint.ToString().Split(":")[0]), Convert.ToInt32(socketSend.LocalEndPoint.ToString().Split(":")[1])));
      Thread c_thread = new Thread(Received);
      c_thread.IsBackground = true;
      c_thread.Start();

      Console.WriteLine("输入注册ID");
      var mes = Console.ReadLine();
      socketSend.Send(Encoding.UTF8.GetBytes("000001[f.ff]" + mes));
      Console.WriteLine("输入要连接的ID");
      while (!T)
      {
        mes = Console.ReadLine();
        if (T)
        {
          break;
        }
        socketSend.Send(Encoding.UTF8.GetBytes("000002[f.ff]" + mes));
      }


      while (true)
      {
        try
        {
          mes = Console.ReadLine();
          if (T)
          {
            socketClient.Send(Encoding.UTF8.GetBytes(mes + " "+System.DateTime.Now.ToString()));
          }



        }
        catch (Exception e)
        {

          Console.WriteLine(e.Message);
        }

      }

      Console.WriteLine("Hello World!");
    }

    static void ClientReceived()
    {
      while (true)
      {
        try
        {
          byte[] buffer = new byte[1024 * 1024 * 3];
          //实际接收到的有效字节数
          int len = socketClient.Receive(buffer);
          if (len == 0)
          {
            continue;
          }
          string str = Encoding.UTF8.GetString(buffer, 0, len);
          Console.WriteLine("接收到数据(" + socketClient.RemoteEndPoint + "):" + str);


        }
        catch
        {

        }
      }
    }

    static void Received()
    {
      try
      {

     
      while (true)
      {
        byte[] buffer = new byte[1024 * 1024 * 3];
        //实际接收到的有效字节数
        int len = socketSend.Receive(buffer);
        try
        {         
          if (len == 0)
          {
            continue;
          }
          string str = Encoding.UTF8.GetString(buffer, 0, len);
          Console.WriteLine("接收到(" + socketSend.RemoteEndPoint + "):" + str);
          if (!string.IsNullOrEmpty(str) && str.Split(":").Length == 2)
          {
            //尝试连接------

            IPAddress ip = IPAddress.Parse(str.Split(":")[0]);
            IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(str.Split(":")[1]));
            while (true)
            {
              try
              {
                socketClient.Connect(point);
                break;

              }
              catch (Exception ex)
              {
                Console.WriteLine("连接异常-正在重试："+ex.Message);

              }
            }
           
            Console.WriteLine("连接成功");
            T = true;
            Thread c_thread = new Thread(ClientReceived);
            c_thread.IsBackground = true;
            c_thread.Start();
          }
        }
        catch (Exception e)
        {
          Console.WriteLine("exp" + e.Message);

        }
      }
      }
      catch (Exception e)
      {

        Console.WriteLine("Receive exp" + e.Message);
      }


    }
  }
}
