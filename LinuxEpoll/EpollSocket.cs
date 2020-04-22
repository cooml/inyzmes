#define buzhanbao

using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;

namespace LinuxEpoll
{
  public class EpollSocket
  {



    private ushort port = 43210;//端口
    private string ipaddr = "192.168.160.43";//ip

    /// <summary>/// 正在断开连接的句柄集合，正在回收状态的句柄
    /// </summary>
    private ConcurrentDictionary<int, int> ShutdownHand = new ConcurrentDictionary<int, int>();

    #region 接受消息的队列信息
    /// <summary>
    /// 指定接收消息的队列数量，最大是8
    /// </summary>
    private int workQueueCount = 4;

    /// <summary>/// 接受消息的都列
    /// </summary>
    private WorkQueue<int> workQ1 = new WorkQueue<int>();
    private WorkQueue<int> workQ2 = new WorkQueue<int>();
    private WorkQueue<int> workQ3 = new WorkQueue<int>();
    private WorkQueue<int> workQ4 = new WorkQueue<int>();

    private WorkQueue<int> workQ5 = new WorkQueue<int>();
    private WorkQueue<int> workQ6 = new WorkQueue<int>();
    private WorkQueue<int> workQ7 = new WorkQueue<int>();
    private WorkQueue<int> workQ8 = new WorkQueue<int>();

    /// <summary>/// 每个队列对应的粘包缓存区
    /// </summary>
    private ConcurrentDictionary<int, DynamicBufferManager> dicBufferDic1 = new ConcurrentDictionary<int, DynamicBufferManager>();

    private ConcurrentDictionary<int, DynamicBufferManager> dicBufferDic2 = new ConcurrentDictionary<int, DynamicBufferManager>();
    private ConcurrentDictionary<int, DynamicBufferManager> dicBufferDic3 = new ConcurrentDictionary<int, DynamicBufferManager>();
    private ConcurrentDictionary<int, DynamicBufferManager> dicBufferDic4 = new ConcurrentDictionary<int, DynamicBufferManager>();

    private ConcurrentDictionary<int, DynamicBufferManager> dicBufferDic5 = new ConcurrentDictionary<int, DynamicBufferManager>();

    private ConcurrentDictionary<int, DynamicBufferManager> dicBufferDic6 = new ConcurrentDictionary<int, DynamicBufferManager>();
    private ConcurrentDictionary<int, DynamicBufferManager> dicBufferDic7 = new ConcurrentDictionary<int, DynamicBufferManager>();
    private ConcurrentDictionary<int, DynamicBufferManager> dicBufferDic8 = new ConcurrentDictionary<int, DynamicBufferManager>();
    #endregion

    /// <summary>/// 处理到某一个连接事件通知后，每次最多读取的次数
    /// </summary>
    private int readCount = 300;

    /// <summary> /// 读取大小
    /// </summary>
    private int recvSize = 1024;
    /// <summary> /// Epoll监听的最多数量
    /// </summary>
    private int epollSize = 1000;

    /// <summary>/// socket监听的队列长度
    /// </summary>
    private int listenSize = 1000;

    /// <summary> /// Epoll的回调
    /// </summary>
    public LSocket.CSCallbacktest callback;

    /// <summary>/// 接收到消息后要调用的委托
    /// </summary>
    /// <param name="fid">socket文件标识</param>
    /// <param name="message">接收到的数据</param>
    public delegate void RecvCallback(int fid, byte[] message);

    /// <summary>/// 接收到消息后的回调
    /// </summary>
    public RecvCallback RecvCall = null;


    /// <summary>///  接收新连接后的回调
    /// </summary>
    /// <param name="fid">新连接标示</param>
    public delegate void AcceptCallback(int fid);

    /// <summary>/// 接收到消息后的回调
    /// </summary>
    public AcceptCallback AcceptCall = null;




    /// <summary>/// 监听端口socket的文件句柄标识
    /// </summary>
    private int sockfd = -1;
    /// <summary>/// epoll_create返回的标识描述
    /// </summary>
    private int epoll_fd = -1;//epoolid


    /// <summary> /// 启动epoll服务
    /// </summary>
    /// <param name="ports">端口</param>
    /// <param name="ipaddrs">ip</param>
    /// <param name="recvSizes">每次接收大小</param>
    /// <param name="RecvCalls">收到消息后的回调用函数</param>
    /// <param name="SocketCloseCalls">连接断开后的回掉函数</param>
    /// <param name="listenSizes"> socket监听的队列长度</param>
    /// <param name="epollSizes"> Epoll监听的最多数量</param>
    /// <param name="AcceptCalls">有新的连接连入时候的回掉</param>
    /// <param name="readCounts">处理到某一个连接事件通知后，每次最多读取的次数</param>
    /// <param name="workQueueCounts">指定接收消息的队列数量，最大是8</param>
    /// <param name="istickpackage">受否选择粘包</param>
    /// <returns></returns>
    public bool Start(ushort ports, string ipaddrs, int recvSizes, RecvCallback RecvCalls, SocketCloseCallback SocketCloseCalls, int listenSizes, int epollSizes, AcceptCallback AcceptCalls, int readCounts, int workQueueCounts)
    {


      //开启队列接受粘包数据
      revMesQue1.UserWork += UserWorkRrevMesQue;
      if (revMesQue1.StartUserWork())
      {
        Console.WriteLine("UserWorkRrevMesQue1队列开启成功");
      }
      revMesQue2.UserWork += UserWorkRrevMesQue;
      if (revMesQue2.StartUserWork())
      {
        Console.WriteLine("UserWorkRrevMesQue2队列开启成功");
      }
      revMesQue3.UserWork += UserWorkRrevMesQue;
      if (revMesQue3.StartUserWork())
      {
        Console.WriteLine("UserWorkRrevMesQue3队列开启成功");
      }
      revMesQue4.UserWork += UserWorkRrevMesQue;
      if (revMesQue4.StartUserWork())
      {
        Console.WriteLine("UserWorkRrevMesQue4队列开启成功");
      }
      revMesQue5.UserWork += UserWorkRrevMesQue;
      if (revMesQue5.StartUserWork())
      {
        Console.WriteLine("UserWorkRrevMesQue5队列开启成功");
      }
      revMesQue6.UserWork += UserWorkRrevMesQue;
      if (revMesQue6.StartUserWork())
      {
        Console.WriteLine("UserWorkRrevMesQue6队列开启成功");
      }
      revMesQue7.UserWork += UserWorkRrevMesQue;
      if (revMesQue7.StartUserWork())
      {
        Console.WriteLine("UserWorkRrevMesQue7队列开启成功");
      }
      revMesQue8.UserWork += UserWorkRrevMesQue;
      if (revMesQue8.StartUserWork())
      {
        Console.WriteLine("UserWorkRrevMesQue8队列开启成功");
      }





      port = ports;
      ipaddr = ipaddrs;
      recvSize = recvSizes;
      RecvCall = RecvCalls;
      SocketCloseCall = SocketCloseCalls;
      listenSize = listenSizes;
      epollSize = epollSizes;
      AcceptCall = AcceptCalls;

      readCount = readCounts;
      workQueueCount = workQueueCounts;

      //回掉
      callback = CSCallbackFunction;
      var honssin_port = LSocket.htons(port);//端口

      Console.WriteLine("端口输出: {0}", honssin_port.ToString());

      var htonlip = LSocket.inet_addr(ipaddr); //htonl(0x00000000);

      Console.WriteLine("IP输出: {0}", htonlip.ToString());

      //创建socket
      sockfd = LSocket.socket(DomainType.AF_INET, SocketTypes.SOCK_STREAM, Protocol.IPPROTO_TCP);

      Console.WriteLine("创建socket输出: {0}", sockfd.ToString());

      //创建参数结构
      sockaddr_in addr = new sockaddr_in()
      {
        sin_family = DomainType.AF_INET,
        sin_port = honssin_port,
        //sin_zero =new byte[] ,// System.Text.Encoding.UTF8.GetBytes(""),
        sin_addr = new in_addr { s_addr = htonlip }
      };


      //绑定
      int bindretrun = LSocket.bind(sockfd, ref addr, 16);
      Console.WriteLine("绑定输出: {0}", bindretrun.ToString());

      if (bindretrun == 0)
      {
        Console.WriteLine("bind成功");
      }
      else
      {
        return false;
      }

      //监听
      int lien = LSocket.listen(sockfd, listenSize);

      if (lien == 0)
      {
        Console.WriteLine("listen成功");
      }
      else
      {
        return false;
      }

      epoll_fd = LSocket.epoll_create(epollSize);

      Console.WriteLine("epoll_create输出: {0}", epoll_fd.ToString());

      if (epoll_fd == -1)
      {
        return false;
      }



      var epollctlret = LSocket.epoll_ctl_ADD(epoll_fd, EPOLL_CTL.EPOLL_CTL_ADD, sockfd, (int)EpollEvents.EPOLLIN);


      Console.WriteLine("epoll_ctl注册返回值: {0}", epollctlret.ToString());
      if (epollctlret == -1)
      {
        return false;
      }

      //开启Epoll
      Thread th = new Thread(() =>
      {
        LSocket.PollWait(epoll_fd, epollSize, callback);//开启等待模式
      });
      th.Start();



      //开启线程读取消息-------1个线程池
      #region 开启线程读取消息
      Thread thr = new Thread(() =>
      {
        while (true)
        {
          var tepm = workQ1.DequeueItem();
          if (tepm != null && tepm != 0)
          {
            try
            {


              Redmes(tepm);
            }
            catch (Exception expWork)
            {

              Console.WriteLine("读取都列异常: {0}", expWork.Message.ToString());
            }
          }
          else
          {
            Thread.Sleep(20);
          }
        }


      });
      thr.Start();


      Thread thr2 = new Thread(() =>
      {
        while (true)
        {
          var tepm = workQ2.DequeueItem();
          if (tepm != null && tepm != 0)
          {
            try
            {


              Redmes(tepm);
            }
            catch (Exception expWork)
            {

              Console.WriteLine("读取都列异常: {0}", expWork.Message.ToString());
            }
          }
          else
          {
            Thread.Sleep(20);
          }
        }


      });
      thr2.Start();

      Thread thr3 = new Thread(() =>
      {
        while (true)
        {
          var tepm = workQ3.DequeueItem();
          if (tepm != null && tepm != 0)
          {
            try
            {

              Redmes(tepm);
            }
            catch (Exception expWork)
            {

              Console.WriteLine("读取都列异常: {0}", expWork.Message.ToString());
            }
          }
          else
          {
            Thread.Sleep(20);
          }
        }


      });
      thr3.Start();


      Thread thr4 = new Thread(() =>
      {
        while (true)
        {
          var tepm = workQ4.DequeueItem();
          if (tepm != null && tepm != 0)
          {
            try
            {


              Redmes(tepm);
            }
            catch (Exception expWork)
            {

              Console.WriteLine("读取都列异常: {0}", expWork.Message.ToString());
            }
          }
          else
          {
            Thread.Sleep(20);
          }
        }


      });
      thr4.Start();
      #endregion

      #region 开启线程读取消息2
      Thread thr5 = new Thread(() =>
      {
        while (true)
        {
          var tepm = workQ5.DequeueItem();
          if (tepm != null && tepm != 0)
          {
            try
            {


              Redmes(tepm);
            }
            catch (Exception expWork)
            {

              Console.WriteLine("读取都列异常: {0}", expWork.Message.ToString());
            }
          }
          else
          {
            Thread.Sleep(20);
          }
        }


      });
      thr5.Start();


      Thread thr6 = new Thread(() =>
      {
        while (true)
        {
          var tepm = workQ6.DequeueItem();
          if (tepm != null && tepm != 0)
          {
            try
            {


              Redmes(tepm);
            }
            catch (Exception expWork)
            {

              Console.WriteLine("读取都列异常: {0}", expWork.Message.ToString());
            }
          }
          else
          {
            Thread.Sleep(20);
          }
        }


      });
      thr6.Start();

      Thread thr7 = new Thread(() =>
      {
        while (true)
        {
          var tepm = workQ7.DequeueItem();
          if (tepm != null && tepm != 0)
          {
            try
            {

              Redmes(tepm);
            }
            catch (Exception expWork)
            {

              Console.WriteLine("读取都列异常: {0}", expWork.Message.ToString());
            }
          }
          else
          {
            Thread.Sleep(20);
          }
        }


      });
      thr7.Start();


      Thread thr8 = new Thread(() =>
      {
        while (true)
        {
          var tepm = workQ8.DequeueItem();
          if (tepm != null && tepm != 0)
          {
            try
            {


              Redmes(tepm);
            }
            catch (Exception expWork)
            {

              Console.WriteLine("读取都列异常: {0}", expWork.Message.ToString());
            }
          }
          else
          {
            Thread.Sleep(20);
          }
        }


      });
      thr8.Start();
      #endregion








      return true;


    }



    #region 关闭socket
    /// <summary>/// socket断开后,要通知业务层的一个回调函数
    /// </summary>
    /// <param name="fid"></param>
    public delegate void SocketCloseCallback(int fid);

    /// <summary> socket断开后,要通知业务层的一个回调函数
    /// </summary>
    private SocketCloseCallback SocketCloseCall = null;


    /// <summary>
    /// 断开比较重要，可能需要业务通知,再封装一层调用
    /// </summary>
    /// <param name="fd"></param>
    /// <returns></returns>
    private void SocketClose(int conn)
    {
      try
      {
        //放入连接已经断开的队列
        if (!ShutdownHand.TryAdd(conn, conn))
        {
          Console.WriteLine("向连接断开队列添加数据失败！");

        }


        //清空粘包区域

        ClearBag(conn);

        //执行事件通知
        if (SocketCloseCall != null)
        {
          SocketCloseCall(conn);//通知业务层，哪一个连接断开了
        }


        int temp = LSocket.close(conn);//关闭连接
        if (temp == -1)
        {//失败写日志
          Console.WriteLine("关闭socket失败-1:" + conn.ToString());

        }

      }
      catch (Exception ex)//异常写日志
      {

        Console.WriteLine("关闭socket异常:" + ex.ToString());
      }
    }



    /// <summary>
    /// 服务端主动端口调用的方法
    /// </summary>
    /// <param name="fd"></param>
    /// <returns></returns>
    public void Close(int conn)
    {
      try
      {
        //放入连接已经断开的队列
        if (!ShutdownHand.TryAdd(conn, conn))
        {
          Console.WriteLine("向连接断开队列添加数据失败！");

        }


        //清空粘包区域

        ClearBag(conn);

        int temp = LSocket.close(conn);//关闭连接
        if (temp == -1)
        {//失败写日志
          Console.WriteLine("主动关闭socket失败-1:" + conn.ToString());

        }

      }
      catch (Exception ex)//异常写日志
      {

        Console.WriteLine("关闭socket异常:" + ex.ToString());
      }
    }

    #endregion


    /// <summary>/// 读取数据
    /// </summary>
    private void Redmes(int conn)
    {

      var byterecv = new byte[recvSize];
      System.IntPtr rbyterecv = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(byterecv, 0);

      try
      {

        bool tepmadd = true;
        //每次读取500此
        for (int j = 0; j < readCount; j++)
        {
          var intrecvret = LSocket.recv(conn, rbyterecv, recvSize, 0);

          if (intrecvret > 0 && intrecvret <= recvSize)
          {
            byte[] byt = new byte[intrecvret];
            for (int i = 0; i < intrecvret; i++)
            {

              byt[i] = Marshal.ReadByte(rbyterecv, i);
            }


#if zhanbao
  List<byte[]> templistbyte;
                        switch (conn % workQueueCount)
                        {
                            case 0:
                                templistbyte = StickingBag.MakeStickingBag(byt, conn, dicBufferDic1);
                                revMesQue1.EnqueueItem(new resobject() { fid = conn, mes = templistbyte });

                                break;
                            case 1:
                                templistbyte = StickingBag.MakeStickingBag(byt, conn, dicBufferDic2);
                                revMesQue2.EnqueueItem(new resobject() { fid = conn, mes = templistbyte });
                                break;
                            case 2:
                                templistbyte = StickingBag.MakeStickingBag(byt, conn, dicBufferDic3);
                                revMesQue3.EnqueueItem(new resobject() { fid = conn, mes = templistbyte });
                                break;
                            case 3:
                                templistbyte = StickingBag.MakeStickingBag(byt, conn, dicBufferDic4);
                                revMesQue4.EnqueueItem(new resobject() { fid = conn, mes = templistbyte });
                                break;


                            case 4:
                                templistbyte = StickingBag.MakeStickingBag(byt, conn, dicBufferDic5);
                                revMesQue5.EnqueueItem(new resobject() { fid = conn, mes = templistbyte });
                                break;
                            case 5:
                                templistbyte = StickingBag.MakeStickingBag(byt, conn, dicBufferDic6);
                                revMesQue6.EnqueueItem(new resobject() { fid = conn, mes = templistbyte });
                                break;
                            case 6:
                                templistbyte = StickingBag.MakeStickingBag(byt, conn, dicBufferDic7);
                                revMesQue7.EnqueueItem(new resobject() { fid = conn, mes = templistbyte });
                                break;
                            case 7:
                                templistbyte = StickingBag.MakeStickingBag(byt, conn, dicBufferDic8);
                                revMesQue8.EnqueueItem(new resobject() { fid = conn, mes = templistbyte });
                                break;

                            default:
                                templistbyte = StickingBag.MakeStickingBag(byt, conn, dicBufferDic1);
                                revMesQue1.EnqueueItem(new resobject() { fid = conn, mes = templistbyte });
                                break;
                        }
#endif

#if buzhanbao
                                revMesQue1.EnqueueItem(new resobject() { fid = conn, mes = new List<byte[]>() { byt } });
#endif




            ////粘包处理

            //foreach (var item in templistbyte)
            //{
            //    #region 读取消息后做的业务处理
            //    try
            //    {
            //        RecvCall(conn, item);

            //    }
            //    catch (Exception exrcall)
            //    {

            //        Console.WriteLine("接收消息后调用委托方法异常提示信息：" + exrcall.Message);
            //    }


            //    #endregion

            //}







          }
          else if (intrecvret == 0)
          {
            tepmadd = false;
            SocketClose(conn);//关闭连接
            break;

          }
          else if (intrecvret == -1)
          {
            break;
          }
          else
          {
            tepmadd = false;
            SocketClose(conn);//关闭连接
            break;
          }


        }

        if (tepmadd)
        {
          try
          {
            #region 需要再次添加连接的监听

            var epollctlretclient = LSocket.epoll_ctl_ADD(epoll_fd, EPOLL_CTL.EPOLL_CTL_ADD, conn, (int)EpollEvents.EPOLLIN);


            if (epollctlretclient == -1)
            {
              Console.WriteLine("epoll_ctl客户端-1: {0}", epollctlretclient.ToString());
            }
            //else
            //{
            //    Console.WriteLine("读取消息后把链接添加进去");
            //}
            #endregion
          }
          catch (Exception expzz)
          {

            Console.WriteLine("添加监听异常：" + expzz.Message);
          }
        }



      }
      catch (Exception ex2)
      {
        Console.WriteLine("接收消息异常提示信息：" + ex2.Message);
        SocketClose(conn);//关闭连接


      }



    }


    private void UserWorkRrevMesQue(object o, WorkQueue<resobject>.EnqueueEventArgs queue)
    {
      foreach (var item in queue.Item.mes)
      {
        RecvCall(queue.Item.fid, item);
      }

    }


    //接收消息队列
    public WorkQueue<resobject> revMesQue1 = new WorkQueue<resobject>();

    public WorkQueue<resobject> revMesQue2 = new WorkQueue<resobject>();

    public WorkQueue<resobject> revMesQue3 = new WorkQueue<resobject>();

    public WorkQueue<resobject> revMesQue4 = new WorkQueue<resobject>();

    public WorkQueue<resobject> revMesQue5 = new WorkQueue<resobject>();

    public WorkQueue<resobject> revMesQue6 = new WorkQueue<resobject>();

    public WorkQueue<resobject> revMesQue7 = new WorkQueue<resobject>();

    public WorkQueue<resobject> revMesQue8 = new WorkQueue<resobject>();








    /// <summary>///  Epoll的回调
    /// </summary>
    /// <param name="fidret"></param>
    private void CSCallbackFunction(int fidret)
    {
      try
      {
        if (fidret == sockfd)//// 客户端有新的连接请求
        {
          int conn = -1;
          sockaddr_in client_addr = new sockaddr_in();
          int length = 16;
          conn = LSocket.accept(sockfd, ref client_addr, ref length);

          if (conn >= 0)
          {
            //清空缓冲区
            ClearBag(conn);

            #region 设置no-block

            var setblock = LSocket.SetNoBlock(conn);
            if (setblock < 0)//失败
            {
              Console.WriteLine("设置bolk失败：标识为：" + conn + "," + setblock);
            }
            else
            {

              Console.WriteLine("设置bolk成功：标识为：" + conn + ",返回值为" + setblock);
            }

            #endregion

            // 向epoll注册client_sockfd监听事件  
            #region 向epoll注册client_sockfd监听事件


            var epollctlretclient = LSocket.epoll_ctl_ADD(epoll_fd, EPOLL_CTL.EPOLL_CTL_ADD, conn, (int)EpollEvents.EPOLLIN);


            if (epollctlretclient == -1)
            {
              Console.WriteLine("epoll_ctl客户端-1: {0}", epollctlretclient.ToString());
            }
            else
            {
              int outint = 0;
              //移除等待关闭的句柄
              ShutdownHand.TryRemove(conn, out outint);

              try
              {

                AcceptCall(conn);
              }
              catch (Exception exppp)
              {

                Console.WriteLine("调用AcceptCall委托异常：" + exppp.Message);
              }




            }

            #endregion

          }
          else
          {
            Console.WriteLine("有客户端接入失败：标识为：" + conn);
          }





        }
        else//有消息啦
        {


          var epollctlretclient = LSocket.epoll_ctl_ADD(epoll_fd, EPOLL_CTL.EPOLL_CTL_DEL, fidret, (int)EpollEvents.EPOLLIN);


          if (epollctlretclient == -1)
          {
            Console.WriteLine("epoll_ctl删除监听失败-1: {0}", epollctlretclient.ToString());
          }
          else
          {
            // Console.WriteLine("epoll_ctl删除成功");

            switch (fidret % workQueueCount)
            {
              case 0:
                workQ1.EnqueueItem(fidret);
                break;
              case 1:
                workQ2.EnqueueItem(fidret);
                break;
              case 2:
                workQ3.EnqueueItem(fidret);
                break;
              case 3:
                workQ4.EnqueueItem(fidret);
                break;


              case 4:
                workQ5.EnqueueItem(fidret);
                break;
              case 5:
                workQ6.EnqueueItem(fidret);
                break;
              case 6:
                workQ7.EnqueueItem(fidret);
                break;
              case 7:
                workQ8.EnqueueItem(fidret);
                break;

              default:
                workQ1.EnqueueItem(fidret);
                break;
            }
          }






        }


      }
      catch (Exception expall)
      {

        Console.WriteLine("Epoll的回调异常: {0}", expall.Message.ToString());
      }





    }

    /// <summary>/// 根据文件句柄发送消息
    /// </summary>
    /// <param name="fid">文件句柄</param>
    /// <param name="mes">消息</param>
    public void SendMessage(int fid, byte[] mes)
    {
      try
      {


        while (true)
        {

          System.IntPtr r = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(mes, 0);


          if (ShutdownHand.ContainsKey(fid))//当前消息舍弃
          {
            Console.WriteLine("链接以已经断开,消息舍弃:" + fid);
            return;
          }
          int tempsent = LSocket.send(fid, r, mes.Length, 0);
          if (tempsent == mes.Length)//发送完毕
          {

            //Console.WriteLine("发送完成：" + tempsent);
            break;
          }
          else if (tempsent > 0 & tempsent < mes.Length)//没有发送完毕
          {
            int tempindexLent = mes.Length - tempsent;
            byte[] bytesenttemp = new byte[tempindexLent];
            for (int i = 0; i < tempindexLent; i++)
            {
              bytesenttemp[i] = mes[tempsent + i];
            }

            mes = bytesenttemp;
            // Console.WriteLine("中间发送：" + tempsent);
          }
          else if (tempsent == -1)//没有准备好
          {
            // Console.WriteLine("发送：" + tempsent);
          }
          else
          {
            Console.WriteLine("发送数据异常" + fid);

          }
        }


      }
      catch (Exception ex)
      {

        Console.WriteLine("Send消息异常：标识为：" + fid + ex.Message);
      }
    }





    #region WrapNewByteArr

    private static byte[] WrapNewByteArr(string dataStr)
    {
      byte[] oldByteArr = Encoding.UTF8.GetBytes(dataStr);
      byte[] newByteArr = new byte[sizeof(int) + oldByteArr.Length];
      byte[] lengthArr = BitConverter.GetBytes(oldByteArr.Length);

      Array.Copy(lengthArr, 0, newByteArr, 0, lengthArr.Length);
      Array.Copy(oldByteArr, 0, newByteArr, lengthArr.Length, oldByteArr.Length);
      return newByteArr;
    }

    #endregion




    /// <summary>/// 根据文件句柄发送消息
    /// </summary>
    /// <param name="fid">文件句柄</param>
    /// <param name="mes">消息</param>
    public void SendMessage(int fid, string messtr)
    {
      byte[] mes = WrapNewByteArr(messtr);
      try
      {


        while (true)
        {

          System.IntPtr r = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(mes, 0);


          if (ShutdownHand.ContainsKey(fid))//当前消息舍弃
          {
            Console.WriteLine("链接以已经断开,消息舍弃:" + fid);
            return;
          }
          int tempsent = LSocket.send(fid, r, mes.Length, 0);
          if (tempsent == mes.Length)//发送完毕
          {

            //Console.WriteLine("发送完成：" + tempsent);
            break;
          }
          else if (tempsent > 0 & tempsent < mes.Length)//没有发送完毕
          {
            int tempindexLent = mes.Length - tempsent;
            byte[] bytesenttemp = new byte[tempindexLent];
            for (int i = 0; i < tempindexLent; i++)
            {
              bytesenttemp[i] = mes[tempsent + i];
            }

            mes = bytesenttemp;
            // Console.WriteLine("中间发送：" + tempsent);
          }
          else if (tempsent == -1)//没有准备好
          {
            // Console.WriteLine("发送：" + tempsent);
          }
          else
          {
            Console.WriteLine("发送数据异常" + fid);

          }
        }


      }
      catch (Exception ex)
      {

        Console.WriteLine("Send消息异常：标识为：" + fid + ex.Message);
      }
    }








    /// <summary>/// 清空粘包缓存区
    /// </summary>
    /// <param name="con"></param>
    private void ClearBag(int conn)
    {

      try
      {
        DynamicBufferManager outbag = null;
        switch (conn % workQueueCount)
        {

          case 0:


            dicBufferDic1.TryRemove(conn, out outbag);
            break;
          case 1:
            dicBufferDic2.TryRemove(conn, out outbag);
            break;
          case 2:
            dicBufferDic3.TryRemove(conn, out outbag);

            break;
          case 3:
            dicBufferDic4.TryRemove(conn, out outbag);


            break;



          case 4:


            dicBufferDic5.TryRemove(conn, out outbag);
            break;
          case 5:
            dicBufferDic6.TryRemove(conn, out outbag);
            break;
          case 6:
            dicBufferDic7.TryRemove(conn, out outbag);

            break;
          case 7:
            dicBufferDic8.TryRemove(conn, out outbag);


            break;













          default:
            dicBufferDic1.TryRemove(conn, out outbag);

            break;
        }
      }
      catch (Exception exBag)
      {

        Console.WriteLine("情况粘包区异常:" + exBag.Message.ToString());
      }

    }


    #region 封装一个c的公共类库源码

    //        #include <sys/types.h>
    //#include <sys/socket.h>
    //#include <stdio.h>
    //#include <netinet/in.h>
    //#include <arpa/inet.h>
    //#include <unistd.h>
    //#include <string.h>
    //#include <stdlib.h>
    //#include <fcntl.h>
    //#include <sys/shm.h>
    //#include <string.h>  
    //#include <sys/epoll.h>  



    //typedef void(*CPPCallback)(int fid) ;

    //int SetNoBlock(int m_sock)
    //{
    //int flags = fcntl(m_sock, F_GETFL, 0);
    //return fcntl(m_sock, F_SETFL, flags|O_NONBLOCK);
    //}






    //int epoll_ctl_ADD(int epoll_fd,int ctltype,int socketid,int EpollEventstype)
    //{
    //     struct epoll_event ev;// epoll事件结构体 
    //         ev.events=EpollEventstype;  
    //         ev.data.fd=socketid;  //socketid
    //         // 向epoll注册server_sockfd监听事件  
    //     return   epoll_ctl(epoll_fd, ctltype,socketid,&ev);



    //}





    //void PollWait(int epoll_fd,int MaxCount,CPPCallback callback)
    //{


    // struct epoll_event events[MaxCount];
    //  int nfds;

    //      while(1)  
    //        { 



    //nfds=epoll_wait(epoll_fd,events,MaxCount,-1);


    //int i;
    //      for(i=0;i<nfds;i++)  
    //            {
    //callback(events[i].data.fd);
    //}



    //}


    //}




















    #endregion








  }

  public class resobject
  {
    public int fid { get; set; }
    public List<byte[]> mes { get; set; }
  }
}
