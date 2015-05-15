using Fiddler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Utility
{
  static class Log
  {
    public static void LogString(string s)
    {
      FiddlerApplication.Log.LogString("FFRKInspector: " + s);
    }

    public static void LogFormat(string s, params object[] args)
    {
      FiddlerApplication.Log.LogFormat("FFRKInspector: " + s, args);
    }
  }
}
