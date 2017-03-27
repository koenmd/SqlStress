using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace sqlstress
{
    public class perfcount
    {
        public class perfmonconter
        {
            public PerformanceCounter counter{get; private set;}
            public string InstanceName = string.Format("process_{0}", Process.GetCurrentProcess().Id);
            public perfmonconter(string _categoryname, string countername)
            {
                counter = new PerformanceCounter();
                counter.CategoryName = _categoryname;
                counter.CounterName = countername;
                counter.InstanceName = InstanceName;
                counter.InstanceLifetime = PerformanceCounterInstanceLifetime.Process;
                counter.ReadOnly = false;
                counter.RawValue = 0;
            }
        }

        //计数器类型名称
        public const string CategoryName = "SQLStress2016";
        public const string Counter_Requests = "requests / sec";
        public const string Counter_ReqDone = "request done / sec";
        public const string Counter_ReqTimeAvg = "work time(ms) elapsed / request";
        public const string Counter_IO = "io avg (not working)";
        public const string Counter_CPU = "cpu avg (not working)";
        //计数器名称
        private string[] CounterNames = new string[] { Counter_Requests, Counter_ReqDone, Counter_ReqTimeAvg, Counter_IO, Counter_CPU };
        //计数器实例
        private Dictionary<string, perfmonconter> Counters = new Dictionary<string, perfmonconter>();

        public perfcount()
        {
            //PerformanceCounterCategory.Delete(CategoryName);
            if (!PerformanceCounterCategory.Exists(CategoryName))
            {
                CounterCreationDataCollection ccdc = new CounterCreationDataCollection();
                //计数器
                foreach (string countername in CounterNames)
                {
                    ccdc.Add(new CounterCreationData(countername, "need help? pray to god", PerformanceCounterType.NumberOfItems64));
                }
                //使用PerformanceCounterCategory.Create创建一个计数器类别
                PerformanceCounterCategory.Create(CategoryName, "there is no help", PerformanceCounterCategoryType.MultiInstance, ccdc);
                //初始化计数器实例
            }

            if (PerformanceCounterCategory.Exists(CategoryName))
            {
                foreach (string countername in CounterNames)
                {
                    if (PerformanceCounterCategory.CounterExists(countername, CategoryName))
                    {
                        Counters.Add(countername, new perfmonconter(CategoryName, countername));
                    }
                }
            }

        }

        ~perfcount()
        {
            foreach (KeyValuePair<string, perfmonconter> kv in Counters)
            {
                kv.Value.counter.Close();
            }
            foreach (KeyValuePair<string, perfmonconter> kv in Counters)
            {
                kv.Value.counter.RemoveInstance();
            }
        }

        public perfmonconter this[string name]
        {
            get{
                return Counters[name];
            }
        }

        public void Reset()
        {
            foreach (KeyValuePair<string, perfmonconter> kv in Counters)
            {
                kv.Value.counter.RawValue = 0;
            }
        }
    }
}
