using System;
using System.Collections.Generic;
using System.Text;

namespace LinuxEpoll
{
  public static class EPOLL_CTL
  {
    /// <summary>
    /// 注册新的fd到epfd中
    /// </summary>
    public const int EPOLL_CTL_ADD = 1;
    /// <summary>
    /// 修改已经注册的fd的监听事件
    /// </summary>
    public const int EPOLL_CTL_MOD = 3;
    /// <summary>
    /// 从epfd中删除一个fd
    /// </summary>
    public const int EPOLL_CTL_DEL = 2;
  }
}
