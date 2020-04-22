

namespace LinuxEpoll
{
  //public  class EpollEvents
  //{
  //    /// <summary> /// 
  //    /// </summary>
  //    public const int EPOLLIN = 0x001;
  //    /// <summary> /// 未定义类型
  //    /// </summary>
  //    public const int EPOLLOUT = 4;
  //    /// <summary> /// 未定义类型
  //    /// </summary>
  //    public const int EPOLLRDHUP = 8192;
  //    /// <summary> /// 未定义类型
  //    /// </summary>
  //    public const int EPOLLPRI =2;
  //    /// <summary> /// 未定义类型
  //    /// </summary>
  //    public const int EPOLLERR = 16;
  //    /// <summary> /// 未定义类型
  //    /// </summary>
  //    public const int EPOLLET = -2147483648;

  //    /// <summary> /// 未定义类型
  //    /// </summary>
  //    public const int EPOLLONESHOT = 1073741824;



  //    //        EPOLLIN:1
  //    //EPOLLOUT:4
  //    //EPOLLRDHUP:8192
  //    //EPOLLPRI:2
  //    //EPOLLERR:8
  //    //EPOLLHUP:16
  //    //EPOLLET:-2147483648
  //    //EPOLLONESHOT:1073741824
  //}


  enum EpollEvents
  {
    EPOLLIN = 0x001,

    EPOLLPRI = 0x002,

    EPOLLOUT = 0x004,

    EPOLLRDNORM = 0x040,

    EPOLLRDBAND = 0x080,

    EPOLLWRNORM = 0x100,

    EPOLLWRBAND = 0x200,

    EPOLLMSG = 0x400,

    EPOLLERR = 0x008,

    EPOLLHUP = 0x010,

    EPOLLRDHUP = 0x2000,

    EPOLLONESHOT = (1 << 30),

    EPOLLET = (1 << 31)


    //EPOLLIN ：表示对应的文件描述符可以读（包括对端SOCKET正常关闭）；
    //EPOLLOUT：表示对应的文件描述符可以写；
    //EPOLLPRI：表示对应的文件描述符有紧急的数据可读（这里应该表示有带外数据到来）；
    //EPOLLERR：表示对应的文件描述符发生错误；
    //EPOLLHUP：表示对应的文件描述符被挂断；
    //EPOLLET： 将EPOLL设为边缘触发(Edge Triggered)模式，这是相对于水平触发(Level Triggered)来说的。
    //EPOLLONESHOT：只监听一次事件，当监听完这次事件之后，如果还需要继续监听这个socket的话，需要再次把这个socket加入到EPOLL队列里







    //– EPOLLIN，读事件

    //– EPOLLOUT，写事件

    //– EPOLLPRI，带外数据，与select的异常事件集合对应

    //– EPOLLRDHUP，TCP连接对端至少写写半关闭

    //– EPOLLERR，错误事件

    //– EPOLLET，设置事件为边沿触发

    //– EPOLLONESHOT，只触发一次，事件自动被删除

    //epoll在一个文件描述符上只能有一个事件，在一个描述符上添加多个事件，会产生EEXIST的错误。同样，删除epoll的事件，只需描述符就够了

    //epoll_ctl(epfd, EPOLL_CTL_DEL, fd, NULL);

  };


}
