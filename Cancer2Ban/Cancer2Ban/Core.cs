using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cancer2Ban
{
    public static class Core
    {
        private static Dictionary<double, Thread> threadPool = new Dictionary<double, Thread>();
        private static int threadCount = 0;

        public static Thread RunThread(Action methodName)
        {
            ManualResetEvent syncEvent = new ManualResetEvent(false);
            double unixMilli = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            Thread newThread = new Thread(
        () =>
        {
            syncEvent.Set();
            methodName();
            syncEvent.WaitOne();
            threadPool.Remove(unixMilli);
        }
    );
            if (threadPool.ContainsKey(unixMilli))
            {
                Thread.Sleep(5);
            }

            threadPool.Add(unixMilli, newThread);
            newThread.Start();
            return newThread;
        }

        public static void CloseThreads()
        {
            foreach (double key in threadPool.Keys)
            {
                threadPool[key].Abort();
            }
        }

        public static int ThreadCount()
        {
            return threadCount;
        }

        public static ManagementObject GetFirstObject(string sql)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", sql);
            ManagementObject firstObj = searcher.Get().OfType<ManagementObject>().FirstOrDefault();
            return firstObj;
        }
    }
}
