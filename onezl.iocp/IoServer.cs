﻿
#define buzhanbao


//#define zhanbao

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Collections;

using System.Collections.Concurrent;
using onezl.iocp.com;

namespace onezl.iocp
{

  public delegate void ReceiveEventHandler(AsyncSocketUserToken SocketArg, byte[] byteArr);

  /// <summary> 基于SocketAsyncEventArgs 实现 IOCP 服务器 
  /// <para>注:加入多队列处理机制,目前只应用于Socket服务器.</para>
  /// </summary>
  public class IoServer
  {

    #region 变量


    /// <summary> 接收触发的事件
    /// </summary>
    public ReceiveEventHandler ReceiveEvent { get; set; }

    /// <summary> 监听Socket，用于接受客户端的连接请求
    /// </summary>
    private Socket listenSocket;

    /// <summary> 用于每个I/O Socket操作的缓冲区大小
    /// </summary>
    private Int32 bufferSize;

    /// <summary> 服务器能接受的最大连接数量
    /// </summary>
    private Int32 numConnections;

    /// <summary> 服务器标识， 如果等于RegClean需要开启注册模式，长时间没有注册信息将被清除
    /// </summary>
    public string Name { get; set; }

    /// <summary> 完成端口上进行投递所用的IoContext对象池
    /// </summary>
    private IoContextPool ioContextPool;

    /// <summary>
    /// SocketAsyncEventArgs 对象发送池
    /// </summary>
    private SocketAsyncEventArgsSendPool SocketSendPool;

    /// <summary> 每一个处理线程对应一个byte缓存接受数据数组
    /// </summary>
    private ConcurrentDictionary<string, DynamicBufferManager> dicBuffer0, dicBuffer1, dicBuffer2, dicBuffer3, dicBuffer4, dicBuffer5, dicBuffer6, dicBuffer7, dicBuffer8, dicBuffer9, dicBuffer10, dicBuffer11, dicBuffer12, dicBuffer13, dicBuffer14, dicBuffer15, dicBuffer16, dicBuffer17, dicBuffer18, dicBuffer19, dicBuffer20, dicBuffer21, dicBuffer22, dicBuffer23, dicBuffer24, dicBuffer25, dicBuffer26, dicBuffer27, dicBuffer28, dicBuffer29, dicBuffer30, dicBuffer31, dicBuffer32, dicBuffer33, dicBuffer34, dicBuffer35, dicBuffer36, dicBuffer37, dicBuffer38, dicBuffer39;

    /// <summary> 接收工作队列承载对象池
    /// </summary>
    private AsyncSocketUserTokenPool _asyncSocketUserTokenPool = null;

    /// <summary> 发送工作队列承载对象池
    /// </summary>
    private AsynSocketSendUserTokenPool _asynSocketSendUserTokenPool = null;

    /// <summary> 接收工作队列列表
    /// </summary>
    private List<WorkQueue<AsyncSocketUserToken>> _workQueueList;

    /// <summary> 发送工作队列列表
    /// </summary>
    private List<SendWorkQueue<AsynSocketSendUserToken>> _sendWorkList;

    /// <summary> 记录僵尸SocketAsyncEventArgs对象
    /// </summary>
    ConcurrentDictionary<SocketAsyncEventArgs, DateTime> _zombieSocketAsyncEventArgsDic = new ConcurrentDictionary<SocketAsyncEventArgs, DateTime>();

    /// <summary>记录新进来的连接
    /// </summary>
    //public Stack<Socket> _listenCon = new Stack<Socket>();
    public Queue<Socket> _listenCon = new Queue<Socket>();

    /// <summary>记录发送数据的连接
    /// </summary>
    //public Stack<SocketAsyncEventArgs> _reciveCon = new Stack<SocketAsyncEventArgs>();
    public Queue<SocketAsyncEventArgs> _reciveCon = new Queue<SocketAsyncEventArgs>();
    #endregion

    #region 初始化服务器 填充池数据
    /// <summary>  构造函数，建立一个未初始化的服务器实例
    /// </summary>
    /// <param name="numConnections">服务器的最大连接数据</param>
    /// <param name="bufferSize"></param>
    public IoServer(Int32 numConnections, Int32 bufferSize)
    {

      this.numConnections = numConnections;
      this.bufferSize = bufferSize;

      this.ioContextPool = new IoContextPool(numConnections);

      SocketSendPool = new SocketAsyncEventArgsSendPool(100000);

      // 为IoContextPool预分配SocketAsyncEventArgs对象
      for (Int32 i = 0; i < this.numConnections; i++)
      {
        SocketAsyncEventArgs ioContext = new SocketAsyncEventArgs();
        ioContext.Completed += new EventHandler<SocketAsyncEventArgs>(OnIOCompleted);
        ioContext.SetBuffer(new Byte[this.bufferSize], 0, this.bufferSize);

        // 将预分配的对象加入SocketAsyncEventArgs对象池中
        this.ioContextPool.Add(ioContext);
      }

      for (int i = 0; i < 100000; i++)
      {
        SocketAsyncEventArgs socketSend = new SocketAsyncEventArgs();
        socketSend.Completed += new EventHandler<SocketAsyncEventArgs>(sockasyn_Completed);
        SocketSendPool.Push(socketSend);
      }

      _asyncSocketUserTokenPool = new AsyncSocketUserTokenPool(numConnections * 4);
      for (int i = 0; i < numConnections * 4; i++)
      {
        _asyncSocketUserTokenPool.Push(new AsyncSocketUserToken());
      }

      _workQueueList = new List<WorkQueue<AsyncSocketUserToken>>();
      for (int i = 0; i < 40; i++)
      {
        _workQueueList.Add(new WorkQueue<AsyncSocketUserToken>());

        //创建处理队列的线程
        Thread t = new Thread(new ParameterizedThreadStart(DoWorkForQueue));

        //初始化缓存接受数据数组并开启处理队列的线程
        switch (i)
        {
          #region 初始化缓存接受数据数组并开启处理队列的线程

          case 0:
            dicBuffer0 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(0);

            break;
          case 1:
            dicBuffer1 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(1);
            break;
          case 2:
            dicBuffer2 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(2);
            break;
          case 3:
            dicBuffer3 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(3);
            break;
          case 4:
            dicBuffer4 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(4);
            break;
          case 5:
            dicBuffer5 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(5);
            break;
          case 6:
            dicBuffer6 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(6);
            break;
          case 7:
            dicBuffer7 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(7);
            break;
          case 8:
            dicBuffer8 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(8);
            break;
          case 9:
            dicBuffer9 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(9);
            break;
          case 10:
            dicBuffer10 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(10);
            break;
          case 11:
            dicBuffer11 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(11);
            break;
          case 12:
            dicBuffer12 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(12);
            break;
          case 13:
            dicBuffer13 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(13);
            break;
          case 14:
            dicBuffer14 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(14);
            break;
          case 15:
            dicBuffer15 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(15);
            break;
          case 16:
            dicBuffer16 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(16);
            break;
          case 17:
            dicBuffer17 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(17);
            break;
          case 18:
            dicBuffer18 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(18);
            break;
          case 19:
            dicBuffer19 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(19);
            break;
          case 20:
            dicBuffer20 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(20);
            break;
          case 21:
            dicBuffer21 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(21);
            break;
          case 22:
            dicBuffer22 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(22);
            break;
          case 23:
            dicBuffer23 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(23);
            break;
          case 24:
            dicBuffer24 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(24);
            break;
          case 25:
            dicBuffer25 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(25);
            break;
          case 26:
            dicBuffer26 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(26);
            break;
          case 27:
            dicBuffer27 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(27);
            break;
          case 28:
            dicBuffer28 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(28);
            break;
          case 29:
            dicBuffer29 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(29);
            break;
          case 30:
            dicBuffer30 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(30);
            break;
          case 31:
            dicBuffer31 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(31);
            break;
          case 32:
            dicBuffer32 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(32);
            break;
          case 33:
            dicBuffer33 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(33);
            break;
          case 34:
            dicBuffer34 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(34);
            break;
          case 35:
            dicBuffer35 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(35);
            break;
          case 36:
            dicBuffer36 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(36);
            break;
          case 37:
            dicBuffer37 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(37);
            break;
          case 38:
            dicBuffer38 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(38);
            break;
          case 39:
            dicBuffer39 = new ConcurrentDictionary<string, DynamicBufferManager>();
            t.Start(39);
            break;


          default:
            break;
            #endregion
        }
      }

      _asynSocketSendUserTokenPool = new AsynSocketSendUserTokenPool(numConnections * 4);
      for (int i = 0; i < numConnections * 4; i++)
      {
        _asynSocketSendUserTokenPool.Push(new AsynSocketSendUserToken());
      }

      _sendWorkList = new List<SendWorkQueue<AsynSocketSendUserToken>>();
      for (int i = 0; i < 40; i++)
      {
        _sendWorkList.Add(new SendWorkQueue<AsynSocketSendUserToken>());

        //创建处理队列的线程
        Thread t = new Thread(new ParameterizedThreadStart(DoWorkForSendQu));
        t.Start(i);
      }


      System.Timers.Timer tc = new System.Timers.Timer();

      tc.Elapsed += CheckZombieSocketAsyncEventArgs;
      tc.Interval = 60 * 1000;
      tc.Start();


      Thread acceptTh = new Thread(HandleAccept);
      acceptTh.Start();
      Thread reciceTh = new Thread(HandelRecive);
      reciceTh.Start();
    }

    #endregion

    #region 异步完成时回调的方法
    /// <summary> Accept 操作完成时回调函数
    /// </summary>
    /// <param name="sender">Object who raised the event.</param>
    /// <param name="e">SocketAsyncEventArg associated with the completed accept operation.</param>
    private void OnAcceptCompleted(object sender, SocketAsyncEventArgs e)
    {
      this.ProcessAccept(e);
    }

    /// <summary> 当Socket上接收,发送请求被完成时，调用此函数
    /// </summary>
    /// <param name="sender">激发事件的对象</param>
    /// <param name="e">与接收,发送完成操作相关联的SocketAsyncEventArg对象</param>
    private void OnIOCompleted(object sender, SocketAsyncEventArgs e)
    {

      switch (e.LastOperation)
      {
        case SocketAsyncOperation.Receive:
          this.ProcessReceive(e);
          break;
        case SocketAsyncOperation.Send:
          this.ProcessSend(e);
          break;
      }
    }
    #endregion

    #region Start 监听
    /// <summary> 启动服务，开始监听
    /// </summary>
    /// <param name="ipAddress">监听的ip</param>
    /// <param name="port">监听的端口</param>
    public void Start(string ipAddress, Int32 port)
    {
      // 获得主机相关信息
      //IPAddress[] addressList = Dns.GetHostEntry(Environment.MachineName).AddressList;
      IPAddress ip = IPAddress.Parse(ipAddress);
      IPEndPoint localEndPoint = new IPEndPoint(ip, port);

      // 创建监听socket
      this.listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
      this.listenSocket.ReceiveBufferSize = this.bufferSize;
      this.listenSocket.SendBufferSize = this.bufferSize;

      if (localEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
      {
        // 配置监听socket为 dual-mode (IPv4 & IPv6) 
        // 27 is equivalent to IPV6_V6ONLY socket option in the winsock snippet below,
        this.listenSocket.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, false);

        this.listenSocket.Bind(new IPEndPoint(IPAddress.IPv6Any, localEndPoint.Port));
      }
      else
      {
        this.listenSocket.Bind(localEndPoint);
      }

      // 开始监听
      this.listenSocket.Listen(this.numConnections);

      // 在监听Socket上投递一个接受请求。
      this.StartAccept(null);

    }

    public void Start(Int32 port)
    {
      // 获得主机相关信息
      //IPAddress[] addressList = Dns.GetHostEntry(Environment.MachineName).AddressList;
      IPAddress ip = IPAddress.Any;
      IPEndPoint localEndPoint = new IPEndPoint(ip, port);

      // 创建监听socket
      this.listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
      this.listenSocket.ReceiveBufferSize = this.bufferSize;
      this.listenSocket.SendBufferSize = this.bufferSize;

      if (localEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
      {
        // 配置监听socket为 dual-mode (IPv4 & IPv6) 
        // 27 is equivalent to IPV6_V6ONLY socket option in the winsock snippet below,
        this.listenSocket.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, false);

        this.listenSocket.Bind(new IPEndPoint(IPAddress.IPv6Any, localEndPoint.Port));
      }
      else
      {
        this.listenSocket.Bind(localEndPoint);
      }

      // 开始监听
      this.listenSocket.Listen(this.numConnections);

      // 在监听Socket上投递一个接受请求。
      this.StartAccept(null);

    }
    #endregion

    #region StartAccept 开始接收连接
    /// <summary> 从客户端开始接受一个连接操作
    /// </summary>
    /// <param name="acceptEventArg">The context object to use when issuing 
    /// the accept operation on the server's listening socket.</param>
    private void StartAccept(SocketAsyncEventArgs acceptEventArg)
    {
      if (acceptEventArg == null)
      {
        acceptEventArg = new SocketAsyncEventArgs();
        acceptEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
      }
      else
      {
        // 重用前进行对象清理
        acceptEventArg.AcceptSocket = null;
      }

      if (!this.listenSocket.AcceptAsync(acceptEventArg))
      {
        this.ProcessAccept(acceptEventArg);
      }
    }
    #endregion

    /// <summary> 针对Socket服务器,用户发送注册命令的时候从 僵尸字典里移除.
    /// </summary>
    /// <param name="socketArg"></param>
    public void RemoveZombieSocketAsyncEventArgs(SocketAsyncEventArgs socketArg)
    {
      DateTime dt = DateTime.Now;
      _zombieSocketAsyncEventArgsDic.TryRemove(socketArg, out dt);
    }

    #region 定时清理僵尸连接
    /// <summary>定时清理僵尸连接
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckZombieSocketAsyncEventArgs(object sender, System.Timers.ElapsedEventArgs e)
    {
      foreach (var item in _zombieSocketAsyncEventArgsDic)
      {
        try
        {

          if ((DateTime.Now - item.Value).TotalMinutes > 3)
          {

            if (item.Key.BytesTransferred <= 0 || Name == "RegClean")
            {
              //释放
              CloseClientSocketTopool(item.Key.UserToken as Socket, item.Key);

            }


          }
        }
        catch (Exception)
        {
        }
      }
    }
    #endregion


    #region ProcessAccept 处理接收到的连接
    /// <summary> 监听Socket接受处理
    /// </summary>
    /// <param name="e">SocketAsyncEventArg associated with the completed accept operation.</param>
    private void ProcessAccept(SocketAsyncEventArgs e)
    {

      lock (_listenCon)
      {
        _listenCon.Enqueue(e.AcceptSocket);
      }

      //投递下一次请求
      this.StartAccept(e);

    }

    /// <summary>处理监听到的连接
    /// </summary>
    private void HandleAccept()
    {
      while (true)
      {
        if (_listenCon == null || _listenCon.Count == 0)
        {
          Thread.Sleep(1);
        }
        else
        {
          try
          {
            Socket e;
            lock (_listenCon)
            {
              e = _listenCon.Dequeue();
            }
            if (e != null)
            {
              if (e.Connected)
              {


                SocketAsyncEventArgs ioContext = this.ioContextPool.Pop();
                if (ioContext != null)
                {
                  _zombieSocketAsyncEventArgsDic.TryAdd(ioContext, DateTime.Now);
                  if (ioContextPool.GetCount() < 1000)
                  {
                    Logger.WriteLog("TcpServerNew" + ioContextPool.GetCount().ToString());
                  }
                  ioContext.UserToken = e;

                  if (!e.ReceiveAsync(ioContext))
                  {
                    this.ProcessReceive(ioContext);
                  }

                }
                else        //已经达到最大客户连接数量，在这接受连接，发送“连接已经达到最大数”，然后断开连接
                {
                  #region 负载
                  ioContext = new SocketAsyncEventArgs();
                  //注册异步接收完成后的事件
                  ioContext.Completed += new EventHandler<SocketAsyncEventArgs>(OnIOCompleted);
                  ioContext.SetBuffer(new Byte[this.bufferSize], 0, this.bufferSize);
                  _zombieSocketAsyncEventArgsDic.TryAdd(ioContext, DateTime.Now);

                  ioContext.UserToken = e;

                  if (!e.ReceiveAsync(ioContext))
                  {
                    this.ProcessReceive(ioContext);
                  }
                  #endregion
                }


                try
                {


                  //string ipport = string.Empty;

                  //ipport = e.RemoteEndPoint.ToString();


                  //SocketAsyncEventArgs ioContexttemp = this.ioContextPool.Pop();
                  //if (ioContexttemp != null)
                  //{

                  //    if (ioContextPool.GetCount() < 1000)
                  //    {
                  //        Logger.WriteLog("TcpServerNew" + ioContextPool.GetCount().ToString());
                  //    }
                  //    ioContexttemp.UserToken = e;



                  //}
                  //else        //已经达到最大客户连接数量，在这接受连接，发送“连接已经达到最大数”，然后断开连接
                  //{
                  //    #region 负载
                  //    ioContexttemp = new SocketAsyncEventArgs();
                  //    //注册异步接收完成后的事件
                  //    ioContexttemp.Completed += new EventHandler<SocketAsyncEventArgs>(OnIOCompleted);
                  //    ioContexttemp.SetBuffer(new Byte[this.bufferSize], 0, this.bufferSize);

                  //    ioContexttemp.UserToken = e;

                  //    #endregion
                  //}


                  PutEnqueueItemBySys(ioContext, "000506", e.RemoteEndPoint.ToString());



                }
                catch
                {


                }










              }








            }
            else
            {
              Thread.Sleep(10);
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine("HandleAccept报的错:" + ex.Message);
          }
        }
      }

    }
    #endregion

    #region ProcessReceive 处理接收到的数据
    /// <summary> 接收完成时处理函数
    /// </summary>
    /// <param name="e">与接收完成操作相关联的SocketAsyncEventArg对象</param>
    private void ProcessReceive(SocketAsyncEventArgs e)
    {
      #region 优化之处
      //try
      //{
      //    Socket s = (Socket)e.UserToken;
      //    if (e.BytesTransferred > 0)
      //    {

      //        string ipport = s.RemoteEndPoint.ToString();
      //        if (e.SocketError == SocketError.Success)
      //        {
      //            byte[] byteRecive = new byte[e.BytesTransferred];
      //            Array.Copy(e.Buffer, 0, byteRecive, 0, e.BytesTransferred);

      //            AsyncSocketUserToken asyncUserToken = _asyncSocketUserTokenPool.Pop();
      //            while (asyncUserToken == null)
      //            {
      //                asyncUserToken = _asyncSocketUserTokenPool.Pop();
      //                if (asyncUserToken == null)
      //                {
      //                    Thread.Sleep(10);
      //                    Logger.WriteLog("ProcessReceive-IoServer-510弹出还是空");
      //                }

      //            }

      //            int socketHandle = s.Handle.ToInt32();
      //            asyncUserToken.ConnectSocketHandle = socketHandle;
      //            asyncUserToken.ReceiveBuffer = byteRecive;
      //            asyncUserToken.ReceiveEventArgs = e;
      //            asyncUserToken.IpportStr = ipport;

      //            int revInt = ((socketHandle % 160) / 4);
      //            asyncUserToken.QueueId = revInt;

      //            //收到的消息直接加入到队列里
      //            _workQueueList[revInt].EnqueueItem(asyncUserToken);

      //            #region 如果是负载则不再投递下一次请求，不再接受这个链接的下一次请求。(因为负载只是一次请求，短连接)

      //            e.SetBuffer(0, bufferSize);
      //            if (!s.ReceiveAsync(e))    //为接收下一段数据，投递接收请求，这个函数有可能同步完成，这时返回false，并且不会引发SocketAsyncEventArgs.Completed事件
      //            {
      //                // 同步接收时处理接收完成事件
      //                this.ProcessReceive(e);
      //            }

      //            #endregion

      //        }
      //        else
      //        {
      //            this.ProcessError(e);
      //        }
      //    }
      //    else//客户端主动断开 或者 客户端未断开连接而服务器端主动断开。
      //    {
      //        this.CloseClientSocket(e);
      //    }
      //}
      //catch
      //{
      //    this.CloseClientSocket(e);
      //}
      #endregion
      try
      {
        lock (_reciveCon)
        {
          _reciveCon.Enqueue(e);
        }
      }
      catch
      {

      }
    }

    /// <summary>接收数据后的处理
    /// </summary>
    /// <param name="e"></param>
    private void HandelRecive()
    {
      while (true)
      {

        if (_reciveCon == null || _reciveCon.Count == 0)
        {
          Thread.Sleep(1);
        }
        else
        {
          SocketAsyncEventArgs e = null;
          lock (_reciveCon)
          {
            e = _reciveCon.Dequeue();
          }
          if (e != null)
          {
            try
            {
              Socket s = (Socket)e.UserToken;
              if (e.BytesTransferred > 0)
              {
                string ipport = s.RemoteEndPoint.ToString();
                if (e.SocketError == SocketError.Success)
                {

                  AsyncSocketUserToken asyncUserToken = _asyncSocketUserTokenPool.Pop();
                  while (asyncUserToken == null)
                  {
                    asyncUserToken = _asyncSocketUserTokenPool.Pop();
                    if (asyncUserToken == null)
                    {
                      Thread.Sleep(5);
                    }
                  }
                  int socketHandle = s.Handle.ToInt32();
                  asyncUserToken.ConnectSocketHandle = socketHandle;
                  asyncUserToken.ReceiveBuffer = new byte[e.BytesTransferred];
                  Array.Copy(e.Buffer, 0, asyncUserToken.ReceiveBuffer, 0, e.BytesTransferred);
                  asyncUserToken.ReceiveEventArgs = e;
                  asyncUserToken.IpportStr = ipport;
                  int revInt = ((socketHandle % 160) / 4);
                  asyncUserToken.QueueId = revInt;
                  //收到的消息直接加入到队列里
                  _workQueueList[revInt].EnqueueItem(asyncUserToken);


                  e.SetBuffer(0, bufferSize);

                  if (!s.ReceiveAsync(e))    //为接收下一段数据，投递接收请求，这个函数有可能同步完成，这时返回false，并且不会引发SocketAsyncEventArgs.Completed事件
                  {
                    // 同步接收时处理接收完成事件
                    this.ProcessReceive(e);
                  }

                }
                else
                {
                  this.ProcessError(e);
                }
              }
              else//客户端主动断开 或者 客户端未断开连接而服务器端主动断开。
              {
                this.CloseClientSocket(e);
              }
            }
            catch (Exception ex)
            {
              Console.WriteLine("HandelRecive报的错:" + ex.Message);
            }
          }
        }
      }

    }
    #endregion

    #region ProcessSend 发送完成时处理函数
    /// <summary> 发送完成时处理函数
    /// </summary>
    /// <param name="e">与发送完成操作相关联的SocketAsyncEventArg对象</param>
    private void ProcessSend(SocketAsyncEventArgs e)
    {

      if (e.SocketError == SocketError.Success)
      {
        Socket s = (Socket)e.UserToken;
        //接收时根据接收的字节数收缩了缓冲区的大小，因此投递接收请求时，恢复缓冲区大小
        e.SetBuffer(0, bufferSize);
        if (!s.ReceiveAsync(e))     //投递接收请求
        {
          // 同步接收时处理接收完成事件
          this.ProcessReceive(e);
        }
      }
      else
      {
        this.ProcessError(e);
      }
    }
    #endregion

    #region ProcessError 处理socket错误
    /// <summary> 处理socket错误
    /// </summary>
    /// <param name="e"></param>
    private void ProcessError(SocketAsyncEventArgs e)
    {
      Socket s = e.UserToken as Socket;
      IPEndPoint localEp = s.LocalEndPoint as IPEndPoint;
      this.CloseClientSocket(s, e);
    }
    #endregion

    #region 添加消息到发送工作队列里
    /// <summary>添加消息到发送工作队列里
    /// </summary>
    public void PushSendQue(SocketAsyncEventArgs e, byte[] bytes)
    {
      try
      {


        AsynSocketSendUserToken asyncSend = _asynSocketSendUserTokenPool.Pop();
        while (asyncSend == null)
        {
          asyncSend = _asynSocketSendUserTokenPool.Pop();
          if (asyncSend == null)
          {
            Thread.Sleep(10);
            Logger.WriteLog("PushSendQue-IoServer-605弹出还是空");
          }
        }

        int revInt = 0;
        try
        {

          int socketHandle = (e.UserToken as Socket).Handle.ToInt32();
          asyncSend.ConnectSocketHandle = socketHandle;
          asyncSend.ReceiveBuffer = bytes;
          asyncSend.ReceiveEventArgs = e;

          revInt = ((socketHandle % 160) / 4);
        }
        catch (Exception exe)
        {
          GiveBackAsynSocketSendUserToken(asyncSend);

        }

        _sendWorkList[revInt].EnqueueItem(asyncSend);

      }
      catch (Exception ex)
      {

        ;
      }
    }
    #endregion

    #region 开始异步发送信息
    /// <summary>开始异步发送信息
    /// </summary>
    /// <param name="e"></param>
    /// <param name="bytes"></param>
    private void BeginSend(SocketAsyncEventArgs e, byte[] bytes)
    {
      SocketAsyncEventArgs sockaysn = SocketSendPool.Pop();
      try
      {
        byte[] byteSend = GetByte(bytes);
        sockaysn.AcceptSocket = e.UserToken as Socket;
        sockaysn.SetBuffer(byteSend, 0, byteSend.Length);
        if (!sockaysn.AcceptSocket.SendAsync(sockaysn))
        {
          ProcessSendSocket(sockaysn);
        }
      }
      catch
      {
        ProcessSendSocket(sockaysn);
      }
    }
    #endregion


    #region 处理  接收工作队列
    /// <summary> 处理接收队列中的消息
    /// </summary>
    /// <param name="obj">队列约定编号</param>
    public void DoWorkForQueue(object obj)
    {
      int workQueueIndex = Convert.ToInt32(obj);
      WorkQueue<AsyncSocketUserToken> que = _workQueueList[workQueueIndex];

      while (true)
      {
        try
        {
          if (que.GetQueeuCount() > 0)
          {

            AsyncSocketUserToken asyncUserToken = que.DequeueItem();
            if (asyncUserToken != null)
            {
              // List<byte[]> listcompletebt = StickingBag.MakeStickingBag(asyncUserToken.ReceiveBuffer, asyncUserToken.IpportStr, GetDicBuffer(asyncUserToken.QueueId));

#if zhanbao
              if (asyncUserToken.issystemorder == '0')
              {
                List<byte[]> listcompletebt = StickingBag.MakeStickingBag(asyncUserToken.ReceiveBuffer, asyncUserToken.IpportStr, GetDicBuffer(asyncUserToken.QueueId));
                for (int i = 0; i < listcompletebt.Count; i++)
                {
                  if (ReceiveEvent != null)
                  {
                    try
                    {
                      ReceiveEvent(asyncUserToken, listcompletebt[i]);
                    }
                    catch (Exception ex)
                    {
                      Logger.WriteLog("DoWorkForQueue677:" + ex.Message);
                    }
                  }
                }
              }
              else //系统的约定
              {
                try
                {
                  ReceiveEvent(asyncUserToken, asyncUserToken.ReceiveBuffer);
                }
                catch (Exception ex)
                {
                  Logger.WriteLog("DoWorkForQueue677:" + ex.Message);
                }
              }
#endif



#if buzhanbao
              try
              {
                ReceiveEvent(asyncUserToken, asyncUserToken.ReceiveBuffer);
              }
              catch (Exception ex)
              {
                Logger.WriteLog("DoWorkForQueue677:" + ex.Message);
              }
#endif



              GiveBackAsyncSocketUserToken(asyncUserToken);

            }
          }
          else
          {
            Thread.Sleep(10);
          }
        }
        catch (Exception ex)
        {
          Logger.WriteLog("DoWorkForQueue:" + ex.Message);
        }
      }

    }
    #endregion

    #region 处理  发送工作队列
    /// <summary> 处理发送消息队列
    /// </summary>
    /// <param name="obj"></param>
    public void DoWorkForSendQu(object obj)
    {
      int sendQueIndex = Convert.ToInt32(obj);
      SendWorkQueue<AsynSocketSendUserToken> que = _sendWorkList[sendQueIndex];
      while (true)
      {
        try
        {
          if (que.GetQueeuCount() > 0)
          {

            AsynSocketSendUserToken asyncUserToken = que.DequeueItem();
            if (asyncUserToken != null)
            {

              try
              {
                if ((asyncUserToken.ReceiveEventArgs.UserToken as Socket).Handle.ToInt32() == asyncUserToken.ConnectSocketHandle)
                {
                  BeginSend(asyncUserToken.ReceiveEventArgs, asyncUserToken.ReceiveBuffer);
                }
              }
              catch (Exception ex)
              {
                Logger.WriteLog("DoWorkForSendQu727:" + ex.Message);
              }

              GiveBackAsynSocketSendUserToken(asyncUserToken);
            }
          }
          else
          {
            Thread.Sleep(10);
          }
        }
        catch (Exception ex)
        {
          Logger.WriteLog("DoWorkForSendQu外面：" + ex.Message);
        }
      }
    }
    #endregion


    #region 异步发送完成,将SocketAsyncEventArgs 放回到发送池
    /// <summary> 异步操作完成执行的方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void sockasyn_Completed(object sender, SocketAsyncEventArgs e)
    {
      ProcessSendSocket(e);
    }
    /// <summary> 异步发送完成执行的操作
    /// </summary>
    /// <param name="e"></param>
    private void ProcessSendSocket(SocketAsyncEventArgs e)
    {
      e.AcceptSocket = null;
      e.SetBuffer(null, 0, 0);
      SocketSendPool.Push(e);
    }
    #endregion

    #region 把  接收工作承载对象放回到池里
    /// <summary> 把接收工作承载对象放回到池里
    /// </summary>
    /// <param name="asyncSocketUserToken"></param>
    public void GiveBackAsyncSocketUserToken(AsyncSocketUserToken asyncSocketUserToken)
    {
      asyncSocketUserToken.QueueId = -1;
      asyncSocketUserToken.ConnectSocketHandle = 0;
      asyncSocketUserToken.ReceiveBuffer = null;
      asyncSocketUserToken.issystemorder = '0';
      asyncSocketUserToken.ReceiveEventArgs = null;
      asyncSocketUserToken.IpportStr = "";

      _asyncSocketUserTokenPool.Push(asyncSocketUserToken);



    }
    #endregion

    #region 把  发送工作承载对象放回到池里
    /// <summary> 把发送工作承载对象放回到池里
    /// </summary>
    /// <param name="asyncSocketUserToken"></param>
    public void GiveBackAsynSocketSendUserToken(AsynSocketSendUserToken asyncSocketUserToken)
    {
      asyncSocketUserToken.ConnectSocketHandle = 0;
      asyncSocketUserToken.ReceiveBuffer = null;

      asyncSocketUserToken.ReceiveEventArgs = null;

      _asynSocketSendUserTokenPool.Push(asyncSocketUserToken);
    }
    #endregion

    #region 根据队列约定的id获取当前队列所使用的接收缓存区
    /// <summary> 根据队列约定的id获取当前队列所使用的接收缓存区
    /// </summary>
    /// <param name="QueueId">队列约定的id</param>
    /// <returns>当前队列所使用的接收缓存区</returns>
    private ConcurrentDictionary<string, DynamicBufferManager> GetDicBuffer(int QueueId)
    {
      switch (QueueId)
      {
        case 0: return dicBuffer0;
        case 1: return dicBuffer1;
        case 2: return dicBuffer2;
        case 3: return dicBuffer3;
        case 4: return dicBuffer4;
        case 5: return dicBuffer5;
        case 6: return dicBuffer6;
        case 7: return dicBuffer7;
        case 8: return dicBuffer8;
        case 9: return dicBuffer9;
        case 10: return dicBuffer10;
        case 11: return dicBuffer11;
        case 12: return dicBuffer12;
        case 13: return dicBuffer13;
        case 14: return dicBuffer14;
        case 15: return dicBuffer15;
        case 16: return dicBuffer16;
        case 17: return dicBuffer17;
        case 18: return dicBuffer18;
        case 19: return dicBuffer19;
        case 20: return dicBuffer20;
        case 21: return dicBuffer21;
        case 22: return dicBuffer22;
        case 23: return dicBuffer23;
        case 24: return dicBuffer24;
        case 25: return dicBuffer25;
        case 26: return dicBuffer26;
        case 27: return dicBuffer27;
        case 28: return dicBuffer28;
        case 29: return dicBuffer29;
        case 30: return dicBuffer30;
        case 31: return dicBuffer31;
        case 32: return dicBuffer32;
        case 33: return dicBuffer33;
        case 34: return dicBuffer34;
        case 35: return dicBuffer35;
        case 36: return dicBuffer36;
        case 37: return dicBuffer37;
        case 38: return dicBuffer38;
        case 39: return dicBuffer39;
        default: return null;

      }
    }
    #endregion

    #region 处理命令符在命令符的头部添加命令长度
    /// <summary>处理命令符在命令符的头部添加命令长度
    /// </summary>
    /// <param name="dataStr">需要处理的命令</param>
    /// <returns></returns>
    private byte[] GetByte(byte[] bytes)
    {
#if zhanbao
           byte[] oldByteArr = bytes;
      byte[] newByteArr = new byte[sizeof(int) + oldByteArr.Length];
      byte[] lengthArr = BitConverter.GetBytes(oldByteArr.Length);
      Array.Copy(lengthArr, 0, newByteArr, 0, lengthArr.Length);
      Array.Copy(oldByteArr, 0, newByteArr, lengthArr.Length, oldByteArr.Length);
      return newByteArr;  
#endif



#if buzhanbao

      return bytes;
#endif

    }
    #endregion

    #region 清理不在线用户
    /// <summary>清理不在线用户
    /// </summary>
    /// <param name="socket">清理用户的Socket</param>
    public void CleanDic(SocketAsyncEventArgs e)
    {
      Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      try
      {
        socket = e.UserToken as Socket;
      }
      catch
      {
      }
      finally
      {
        CloseClientSocket(socket, e);
      }
    }

    public void CleanDic(AsyncSocketUserToken e)
    {
      try
      {
        //判断当前队列消息的句柄是否还和异步对象的句柄是否一致，如果不一致说明当前消息队列中的发送者已经掉线，并且socket异步已经被新的请求使用
        if (e.ConnectSocketHandle == (e.ReceiveEventArgs.UserToken as Socket).Handle.ToInt32())
        {
          Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

          try
          {
            socket = e.ReceiveEventArgs.UserToken as Socket;
          }
          catch
          {
          }
          finally
          {
            CloseClientSocket(socket, e.ReceiveEventArgs);
          }
        }
      }
      catch
      {
      }
    }
    #endregion

    #region  清空指定的粘包处理的缓存区
    /// <summary> 清空指定的粘包处理的缓存区
    /// </summary>
    /// <param name="QueueId">队列约定的id（找缓存区）</param>
    /// <param name="IpportStr">（缓冲区索引，目前是ip加端口号）</param>
    public void CleandicBuffer(int QueueId, string IpportStr)
    {
      try
      {
        if (GetDicBuffer(QueueId).ContainsKey(IpportStr))
        {
          DynamicBufferManager dy = new DynamicBufferManager();
          GetDicBuffer(QueueId).TryRemove(IpportStr, out dy);
          dy.Clear();
          dy = null;
        }
      }
      catch
      {
      }
    }
    #endregion


    #region  清空指定的粘包处理的缓存区（暂且不用，这个可做主动掉线处理）
    /// <summary> 清空指定的粘包处理的缓存区
    /// </summary>
    /// <param name="QueueId">队列约定的id（找缓存区）</param>
    /// <param name="IpportStr">（缓冲区索引，目前是ip加端口号）</param>
    public void CleandicBufferforConnect(int QueueId, string IpportStr, SocketAsyncEventArgs e)
    {
      try
      {
        if (GetDicBuffer(QueueId).ContainsKey(IpportStr))
        {
          DynamicBufferManager dy = new DynamicBufferManager();
          GetDicBuffer(QueueId).TryRemove(IpportStr, out dy);
          dy.Clear();
          dy = null;
        }


        //try
        //{
        //    try
        //    {
        //        e.SetBuffer(e.Buffer, 0, e.Buffer.Length);
        //        e.UserToken = null;
        //        e.AcceptSocket = null;



        //        ioContextPool.Push(e);


        //    }
        //    catch { }




        //}
        //catch
        //{

        //}








      }
      catch
      {
      }
    }
    #endregion

    #region 关闭连接流程

    /// <summary>关闭socket连接
    /// </summary>
    /// <param name="e">SocketAsyncEventArg associated with the completed send/receive operation.</param>
    private void CloseClientSocket(SocketAsyncEventArgs e)
    {
      Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      try
      {
        s = e.UserToken as Socket;
      }
      catch
      {

      }
      finally
      {
        this.CloseClientSocket(s, e);
      }

    }

    /// <summary>向队列添加清空命令清除用户连接
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    private void CloseClientSocket(Socket s, SocketAsyncEventArgs e)
    {


      string ipport = string.Empty;
      try
      {
        ipport = s.RemoteEndPoint.ToString();
      }
      catch//说明是客户端还连接着,服务器端主动断开。
      {

        PushSocketAsyncEventArgsToPool(e);
        return;

      }
      PutEnqueueItemBySys(e, "000505", ipport);

      //AsyncSocketUserToken asyncUserToken = null;
      // try
      // {
      //     //创建释放粘包缓冲区的队列--start

      //     byte[] oldByteArr = System.Text.Encoding.UTF8.GetBytes("000505");//000505是命令
      //     byte[] newByteArr = GetByte(oldByteArr);

      //     asyncUserToken = _asyncSocketUserTokenPool.Pop();
      //     while (asyncUserToken == null)
      //     {
      //         asyncUserToken = _asyncSocketUserTokenPool.Pop();
      //         if (asyncUserToken == null)
      //         {
      //             Thread.Sleep(10);
      //             Logger.WriteLog("CloseClientSockett-IoServer-978弹出还是空");
      //         }
      //     }

      //     int socketHandle = (e.UserToken as Socket).Handle.ToInt32();
      //     asyncUserToken.ConnectSocketHandle = socketHandle;
      //     asyncUserToken.ReceiveBuffer = newByteArr;//释放命令
      //     asyncUserToken.ReceiveEventArgs = e;
      //     asyncUserToken.IpportStr = ipport;

      //     int revInt = ((socketHandle % 160) / 4);
      //     asyncUserToken.QueueId = revInt;

      //     _workQueueList[revInt].EnqueueItem(asyncUserToken);//加入队列

      //     //创建释放粘包缓冲区的队列--end
      // }
      // catch
      // {

      // }
    }

    //系统放置约定命令
    private void PutEnqueueItemBySys(SocketAsyncEventArgs e, string strm, string ipport)
    {

      try
      {
        AsyncSocketUserToken asyncUserToken = null;

        byte[] oldByteArr = System.Text.Encoding.UTF8.GetBytes(strm);//000505是命令
                                                                     //byte[] newByteArr = GetByte(oldByteArr);

        asyncUserToken = _asyncSocketUserTokenPool.Pop();
        while (asyncUserToken == null)
        {
          asyncUserToken = _asyncSocketUserTokenPool.Pop();
          if (asyncUserToken == null)
          {
            Thread.Sleep(10);
            Logger.WriteLog("CloseClientSockett-IoServer-978弹出还是空");
          }
        }

        int socketHandle = (e.UserToken as Socket).Handle.ToInt32();
        asyncUserToken.ConnectSocketHandle = socketHandle;
        asyncUserToken.ReceiveBuffer = oldByteArr;// //系统标识不粘包; newByteArr;//释放命令
        asyncUserToken.ReceiveEventArgs = e;
        asyncUserToken.IpportStr = ipport;

        int revInt = ((socketHandle % 160) / 4);
        asyncUserToken.QueueId = revInt;



        //系统标识
        asyncUserToken.issystemorder = '1';
        _workQueueList[revInt].EnqueueItem(asyncUserToken);//加入队列

        //创建释放粘包缓冲区的队列--end
      }
      catch
      {

      }

    }

    /// <summary>遇到队列清空命令使用
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    public void CloseClientSocketTopool(Socket s, SocketAsyncEventArgs e)
    {
      try
      {
        try
        {
          e.SetBuffer(e.Buffer, 0, e.Buffer.Length);
          e.UserToken = null;
          e.AcceptSocket = null;

          DateTime dt = new DateTime();

          _zombieSocketAsyncEventArgsDic.TryRemove(e, out dt);

          ioContextPool.Push(e);


        }
        catch { }

        s.Shutdown(SocketShutdown.Both);
        s.Close();//当客户端未断开,服务器端调用Close方法时,会模拟客户端发送0字节到服务端,服务器端接收到,执行断开操作.


      }
      catch
      {

      }
    }

    #region 清除僵尸连接
    /// <summary> 把SocketAsyncEventArgs放回到接收池里
    /// </summary>
    /// <param name="e"></param>
    public void PushSocketAsyncEventArgsToPool(SocketAsyncEventArgs e)
    {
      try
      {
        e.SetBuffer(e.Buffer, 0, e.Buffer.Length);
        e.UserToken = null;
        e.AcceptSocket = null;

        DateTime dt = new DateTime();

        _zombieSocketAsyncEventArgsDic.TryRemove(e, out dt);

        ioContextPool.Push(e);

      }
      catch (InvalidOperationException ex)//报错的原因:清除僵尸socket后,把SocketAsyncEventArgs放回到池子里,又被其他的socket所使用,这个时候SetBuffer会报错.
      {
        Logger.WriteLog("PushSocketAsyncEventArgsToPool:  " + ex.Message);
      }
    }
    #endregion

    #endregion

    #region 停止服务
    /// <summary>停止服务
    /// </summary>
    internal void Stop()
    {
      this.listenSocket.Close();
    }
    #endregion


    #region 获取池的数量
    public string GetCount()
    {
      return ioContextPool.GetCount().ToString();
    }
    public int GetCountInt()
    {
      return ioContextPool.GetCount();
    }

    public string GetJiang()
    {
      return this._zombieSocketAsyncEventArgsDic.Count.ToString();
    }

    #endregion
  }
}