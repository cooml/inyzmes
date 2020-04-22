using System;
using System.Collections.Generic;
using System.Text;

namespace LinuxEpoll
{
  public static class SocketTypes
  {
    /// <summary>/// stream socket 流,提供面向连接的稳定数据传输，即TCP协议
    /// </summary>    
    public const int SOCK_STREAM = 1;

    /// <summary>// datagram socket 数据报*/
    /// </summary>
    public const int SOCK_DGRAM = 2;

    /// <summary>/// 原始套接字编程可以接收到本机网卡上的数据帧或者数据包
    /// </summary>
    public const int SOCK_RAW = 3;
    /// <summary>/// reliably-delivered message,可靠传递消息
    /// </summary>
    public const int SOCK_RDM = 4;

    /// <summary>/// 与网络驱动程序直接通信。
    /// </summary>
    public const int SOCK_SEQPACKET = 5;

  }
}
