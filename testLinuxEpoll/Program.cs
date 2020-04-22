using LinuxEpoll;
using System;
using System.Text;
using System.Linq;
using System.Collections.Concurrent;

namespace testLinuxEpoll
{
  class Program
  {

    public static void UserWorkpaly(object o, WorkQueue<string[]>.EnqueueEventArgs queue)
    {
      try
      {
        // Console.WriteLine("执行：" + queue.Item[0]);
        SendMessByIdentification(queue.Item[0], queue.Item[1]);
      }
      catch (Exception ex)
      {

        Console.WriteLine("发送队列异常：" + ex.Message);
      }

    }


    public static WorkQueue<string[]> SendMesQue = new WorkQueue<string[]>();



    //消息统计
    public static int num = 0;
    /// <summary> /// Epoll的一个实例
    /// </summary>
    public static EpollSocket ep = new EpollSocket();

    private static Encoding _encoding = Encoding.UTF8;


    static void Main(string[] args)
    {

      //            var ssss=Encoding.UTF8.GetBytes(@"<html>
      //<head>
      //<title>Wrox Homepage</title>
      //</head>
      //<body>
      //你好我是hello word 嘿嘿
      //</body>
      //</html>");

      SendMesQue.UserWork += UserWorkpaly;
      if (SendMesQue.StartUserWork())
      {
        Console.WriteLine("发送消息队列开启成功");
      }
      //Console.WriteLine("执行："  + "待发送消息数量：" + SendMesQue.GetQueeuCount().ToString());
      //开启Epoll模式的socket
      if (ep.Start(5678, "192.168.160.23", 1024, recv, Colse, 1000, 1000, Accept, 500, 8))
      {
        Console.WriteLine("Epoll服务开启成功");
        //Thread th1 = new Thread(() =>
        //{
        //    int temps = 0;
        //    int sssss = 0;
        //    while (true)
        //    {
        //        sssss = num;
        //        //Console.WriteLine("每秒: {0}", (sssss - temps).ToString());
        //        temps = sssss;
        //        Thread.Sleep(1000);

        //    }

        //});
        //th1.Start();


        ////向说有当前socket的连接发送消息
        //Thread re = new Thread(() =>
        //{
        //    var bytesert = System.Text.Encoding.UTF8.GetBytes("你好哈哈哈哈啊哈哈");
        //    while (true)
        //    {
        //        foreach (var item in lse.Keys)
        //        {
        //            //ep.SendMessage(item, bytesert);
        //            ep.SendMessage(item,"你好哈哈哈哈啊哈哈");
        //        }

        //    }


        //});

        //re.Start();




      }





      Console.ReadKey();



    }

    /// <summary>/// 接收消息的回调函数
    /// </summary>
    /// <param name="fid"></param>
    /// <param name="message"></param>
    public static void recv(int fid, byte[] message)
    {

      // Interlocked.Increment(ref num);

      string mescontent = System.Text.Encoding.UTF8.GetString(message);
      var mes = mescontent.Split(new string[] { "[f.f]" }, StringSplitOptions.None);


      Console.WriteLine("回调接收到" + fid + "的数据是：" + mescontent + "");

      #region 处理任务自己写


      ep.SendMessage(fid, Encoding.UTF8.GetBytes(@"HTTP/1.1 200 OK
Date: Sat, 31 Dec 2005 23:59:59 GMT
Content-Type: text/html;charset=utf-8
Content-Length: 110

<html>
<head>
<title>Wrox Homepage</title>
</head>
<body>
你是我是hello word 嘿嘿
</body>
</html>"));
      #endregion


      try
      {
        if (mes.Count() > 1)
        {
          var order = mes[0];
          switch (order)
          {

            case "001"://注册身份命令 001[f.f]黎明
                       //先移除原有的
              if (lseIdentification.ContainsKey(mes[1]))
              {
                DownIdEntification(mes[1]);
              }
              lseIdentification.TryAdd(mes[1], fid);
              break;
            case "002"://转发命令 002[f.f]黎明[f.f]你好黎明[f.f]张三

              //添加到队列
              SendMesQue.EnqueueItem(new string[] { mes[1], "003[f.f]" + mes[1] + "[f.f]" + mes[2] + "[f.f]" + mes[3] });
              //SendMessByIdentification(mes[1], "003[f.f]"+ mes[1]+ "[f.f]" + mes[2] + "[f.f]" + mes[3]);
              break;

            case "003"://转发命令 003[f.f]黎明  下线
              DownIdEntification(mes[1]);
              break;

            case "10001"://查看消息队列数量
              ep.SendMessage(fid, "待发送消息数量：" + SendMesQue.GetQueeuCount().ToString());

              //Console.WriteLine("执行：" + fid+ "待发送消息数量：" + SendMesQue.GetQueeuCount().ToString());
              break;
            default:


              #region 处理任务自己写


              ep.SendMessage(fid, Encoding.UTF8.GetBytes(@"HTTP/1.1 200 OK
Date: Sat, 31 Dec 2005 23:59:59 GMT
Content-Type: text/html;charset=utf-8
Content-Length: 110

<html>
<head>
<title>Wrox Homepage</title>
</head>
<body>
你是我是hello word 嘿嘿
</body>
</html>"));
              #endregion
              break;
          }



        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("收到命令，处理失败:" + ex.Message);

      }



    }

    /// <summary>/// 连接断开的回调函数
    /// </summary>
    /// <param name="fid"></param>
    public static void Colse(int fid)
    {
      Console.WriteLine("回调接收到" + fid + "已经断开连接");
      Downline(fid);
    }

    static ConcurrentDictionary<int, int> lse = new ConcurrentDictionary<int, int>();
    static ConcurrentDictionary<string, int> lseIdentification = new ConcurrentDictionary<string, int>();
    /// <summary>/// 接收到新连接的回调
    /// </summary>
    /// <param name="fid"></param>
    public static void Accept(int conn)
    {

      Console.WriteLine("回调方法有客户端接入成功：标识为：" + conn);
      #region 处理任务自己写


      //            ep.SendMessage(conn, Encoding.UTF8.GetBytes(@"HTTP/1.1 200 OK
      //Date: Sat, 31 Dec 2005 23:59:59 GMT
      //Content-Type: text/html;charset=utf-8
      //Content-Length: 110

      //<html>
      //<head>
      //<title>Wrox Homepage</title>
      //</head>
      //<body>
      //你好我是hello word 嘿嘿
      //</body>
      //</html>"));
      #endregion
      lse.TryAdd(conn, conn);


    }

    /// <summary>
    /// 移除连接
    /// </summary>
    /// <param name="fid"></param>
    public static void RemoveCon(int fid)
    {
      try
      {

        int a;
        lse.TryRemove(fid, out a);



      }
      catch
      {


      }
    }

    /// <summary>
    /// 移除身份
    /// </summary>
    /// <param name="fid"></param>
    public static void RemoveIdentification(string id)
    {
      try
      {

        int a;
        lseIdentification.TryRemove(id, out a);



      }
      catch
      {


      }
    }

    /// <summary> /// 掉线
    /// </summary>
    /// <param name="con"></param>
    public static void Downline(int con)
    {
      RemoveCon(con);
      try
      {


        foreach (var item in lseIdentification)
        {
          if (item.Value == con)
          {
            RemoveIdentification(item.Key);
          }
        }
      }
      catch (Exception ex)
      {

        Console.WriteLine("掉线移除身份异常：" + ex.Message);
      }
    }


    /// <summary>/// 主动下线
    /// </summary>
    public static void DownIdEntification(string iden)
    {
      var conid = lseIdentification[iden];
      ep.Close(conid);
      Downline(conid);


    }



    /// <summary>/// 通过身份发送id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="mess"></param>
    public static void SendMessByIdentification(string id, string mess)
    {
      try
      {
        ep.SendMessage(lseIdentification[id], mess);
      }
      catch (Exception ex)
      {


        Console.WriteLine("通过身份发送消息异常：" + ex.Message);

      }
    }




  }
}
