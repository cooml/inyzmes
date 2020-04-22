using onezl.iocp.com;
using System;
using System.Collections.Generic;
using System.Text;

namespace onezl.iocp
{
   public static class Logger
    {
    public static void WriteLog(string logmes)
    {
      LogHelper.WriteLog(logmes);
    }
  }
}
