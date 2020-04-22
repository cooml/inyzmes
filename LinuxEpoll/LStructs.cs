using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LinuxEpoll
{

  [StructLayout(LayoutKind.Sequential)]
  public struct sockaddr_in
  {
    public System.Int16 sin_family;
    public System.UInt16 sin_port;
    public in_addr sin_addr;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public byte[] sin_zero;
  };
  public struct in_addr
  {
    public uint s_addr;
  };


  [StructLayout(LayoutKind.Sequential)]
  public struct epoll_eventaaaa
  {
    [MarshalAs(UnmanagedType.I4, SizeConst = 4)]
    public int events; /* Epoll events */


  };


  public struct epoll_event
  {
    public System.UInt32 events; /* Epoll events */

    public epoll_data_t data; /* User data variable */
  };


  public struct epoll_data_t
  {
    [MarshalAs(UnmanagedType.AsAny)]
    public System.IntPtr ptr;

    public System.Int32 fd;
    public System.UInt32 u32;
    public System.UInt64 u64;

  }




}
