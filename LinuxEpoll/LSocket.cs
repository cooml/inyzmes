using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LinuxEpoll
{
  /// <summary>/// linux下函数操作api集合
  /// </summary>
  public static class LSocket
  {
    #region linux下c的系统函数



    /// <summary>/// 创建socket
    /// </summary>
    /// <param name="domain">指明使用的协议族，值</param>
    /// <param name="type"> 指明socket类型，值</param>
    /// <param name="protocol">参数protocol用来指明所要接收的协议包</param>
    /// <returns></returns>
    [DllImport("libdl", EntryPoint = "socket")]
    public static extern int socket(int domain, int type, int protocol);

    /// <summary>/// 绑定
    /// </summary>
    /// <param name="sockfd">创建socket的句柄标识,描述</param>
    /// <param name="addr"></param>
    /// <param name="addrlen"></param>
    /// <returns></returns>

    [DllImport("libdl", EntryPoint = "bind")]
    public static extern int bind(int sockfd, ref sockaddr_in addr, int addrlen);

    /// <summary>/// 主机字节顺序转化为网络字节顺序
    /// </summary>
    /// <param name="port"></param>
    /// <returns></returns>

    [DllImport("libdl", EntryPoint = "htons")]
    public static extern ushort htons(ushort port);


    /// <summary>/// 将主机数转换成无符号长整型的网络字节顺序
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>

    [DllImport("libdl", EntryPoint = "htonl")]
    public static extern uint htonl(uint ip);

    /// <summary>/// 若字符串有效则将字符串转换为32位二进制网络字节序的IPV4地址
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>

    [DllImport("libdl", EntryPoint = "inet_addr")]
    public static extern uint inet_addr(string ip);



    /// <summary>/// 监听
    /// </summary>
    /// <param name="s"></param>
    /// <param name="backlog"></param>
    /// <returns></returns>
    [DllImport("libdl", EntryPoint = "listen")]
    public static extern int listen(int s, int backlog);


    /// <summary>/// 接收请求
    /// </summary>
    /// <param name="s"></param>
    /// <param name="addr"></param>
    /// <param name="addrlen"></param>
    /// <returns></returns>

    [DllImport("libdl", EntryPoint = "accept")]
    public static extern int accept(int s, ref sockaddr_in addr, ref int addrlen);

    /// <summary>/// 发送数据
    /// </summary>
    /// <param name="sockfd"></param>
    /// <param name="s"></param>
    /// <param name="len"></param>
    /// <param name="flags"></param>
    /// <returns></returns>

    [DllImport("libdl", EntryPoint = "send")]
    public static extern int send(int sockfd, System.IntPtr s, int len, int flags);



    /// <summary> /// 接收数据
    /// </summary>
    /// <param name="socket"></param>
    /// <param name="s"></param>
    /// <param name="len"></param>
    /// <param name="flags"></param>
    /// <returns></returns>
    [DllImport("libdl", EntryPoint = "recv")]
    public static extern int recv(int socket, System.IntPtr s, int len, int flags);


    /// <summary>/// 关闭，返回值： 成功返回0，失败返回-1。
    /// </summary>
    /// <param name="fd"></param>
    /// <returns></returns>
    [DllImport("libdl", EntryPoint = "close")]
    public static extern int close(int fd);

    /// <summary>/// 创建一个epoll的句柄，size用来告诉内核这个监听的数目一共有多大
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    [DllImport("libdl", EntryPoint = "epoll_create")]
    public static extern int epoll_create(int size);

    #endregion

    #region 目前没有用时的，这两个函数没有实现


    [DllImport("libdl", EntryPoint = "epoll_ctl")]
    public static extern int epoll_ctl(int epfd, int op, int fd, ref epoll_event ev);



    [DllImport("libdl", EntryPoint = "epoll_wait")]
    public static extern int epoll_wait(int epfd, System.IntPtr events, int maxevents, int timeout);

    #endregion

    #region 自定义c动态库函数


    //        命令：
    //gcc -Wall -fPIC -O2 -c -o server.o server.c



    //gcc -shared -Wl,-soname, server.so -o server.so server.o（必须用这个才能调用成功）

    //或者gcc -shared -Wl,-soname -o server.so server.o（当上面的不能用的时候先使用下面这个，然后再用上面的）






    /// <summary>/// Epoll消息通知的一个回调函数
    /// </summary>
    /// <param name="fid"></param>
    public delegate void CSCallbacktest(int fid);
    /// <summary>
    /// Epoll函数，等待时间通知
    /// </summary>
    /// <param name="epoll_fd"></param>
    /// <param name="MaxCount"></param>
    /// <param name="callback"></param>

    [DllImport("/home/server.so", EntryPoint = "PollWait")]
    public static extern void PollWait(int epoll_fd, int MaxCount, CSCallbacktest callback);

    /// <summary>/// 添加epoll监听
    /// </summary>
    /// <param name="epoll_fd"></param>
    /// <param name="ctltype"></param>
    /// <param name="socketid"></param>
    /// <param name="EpollEventstype"></param>
    /// <returns></returns>
    [DllImport("/home/server.so", EntryPoint = "epoll_ctl_ADD")]
    public static extern int epoll_ctl_ADD(int epoll_fd, int ctltype, int socketid, int EpollEventstype);

    /// <summary>/// 设置非阻塞
    /// </summary>
    /// <param name="m_sock"></param>
    /// <returns></returns>

    [DllImport("/home/server.so", EntryPoint = "SetNoBlock")]
    public static extern int SetNoBlock(int m_sock);



    #region 封装一个c的公共类库源码

    //#include <sys/types.h>
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
    //struct epoll_event ev;// epoll事件结构体 
    //ev.events=EpollEventstype;  
    //ev.data.fd=socketid;  //socketid
    //// 向epoll注册server_sockfd监听事件  
    //return   epoll_ctl(epoll_fd, ctltype,socketid,&ev);



    //}





    //void PollWait(int epoll_fd,int MaxCount,CPPCallback callback)
    //{


    //struct epoll_event events[MaxCount];
    //int nfds;

    //while(1)  
    //{ 



    //nfds=epoll_wait(epoll_fd,events,MaxCount,-1);


    //int i;
    //for(i=0;i<nfds;i++)  
    //{
    //callback(events[i].data.fd);
    //}



    //}


    //}













    //# include <sys/types.h>
    //# include <sys/socket.h>
    //# include <stdio.h>
    //# include <netinet/in.h>
    //# include <arpa/inet.h>
    //# include <unistd.h>
    //# include <string.h>
    //# include <stdlib.h>
    //# include <fcntl.h>
    //# include <sys/shm.h>
    //# include <string.h>  
    //# include <sys/epoll.h>  



    //        typedef void(*CPPCallback)(int fid) ;

    //        int SetNoBlock(int m_sock)
    //        {
    //            int flags = fcntl(m_sock, F_GETFL, 0);
    //            return fcntl(m_sock, F_SETFL, flags | O_NONBLOCK);
    //        }






    //        int epoll_ctl_ADD(int epoll_fd, int ctltype, int socketid, int EpollEventstype)
    //        {
    //struct epoll_event ev;
    //ev.events=EpollEventstype;  
    //ev.data.fd=socketid; 
    //return   epoll_ctl(epoll_fd, ctltype, socketid,&ev);



    //    }





    //    void PollWait(int epoll_fd, int MaxCount, CPPCallback callback)
    //    {


    //struct epoll_event events[MaxCount];
    //int nfds;

    //while(1)  
    //{ 



    //nfds=epoll_wait(epoll_fd, events, MaxCount,-1);


    //    int i;
    //for(i=0;i<nfds;i++)  
    //{
    //callback(events[i].data.fd);
    //}



    //}


    //}








    #endregion


    #endregion



  }
}
