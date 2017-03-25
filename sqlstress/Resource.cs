using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace sqlstress
{
    static class Resource
    {
        public static string SCHEMEFOLDER = "Case";
        public static string SCHEMEPATH = Path.GetFullPath(string.Format(".\\{0}", Resource.SCHEMEFOLDER));
        public static string NEWSCHEMEDIALOG = "输入新建样本名称";
        public static string NEWSCHEMEERRMSG = "不符合要求的样本名称";
        public static string NEWSCHEMEERRMSG1 = "样本已存在！";
        public static string MSGERRTITLE = "错误";
        public static string BTNTITLESTART = "启动";
        public static string BTNTITLESTOP = "停止";
        public static string STRESSDONE = "完成!";

        public static string DIAG_DELETESURE = "是否确定删除样本{0}!";
        public static string DIAG_DELETETILE = "请确认!";
        public static string DIAG_NEWTILE = "新建样本!";
        public static string DIAG_NEWTEXT = "输入样本名称!";

        public const string CAT_BASE = "基本";
        public const string CAT_BASE_NAME = "样本名称";
        public const string CAT_EXEC = "执行";
        public const string CAT_EXEC_THREAD = "线程数";
        public const string CAT_EXEC_MAXTIMES = "执行次数";
        public const string CAT_EXEC_WAY = "执行方式";
        public const string CAT_EXEC_GETRESULT = "读取结果";
        public const string CAT_EXEC_COLOR = "颜色";
        public const string CAT_TASK = "任务";
        public const string CAT_TASK_INIT = "任务初始执行";
        public const string CAT_TASK_FINIT = "任务结束执行";
        public const string CAT_TASK_STRESS = "任务内容";
        public const string CAT_TASK_STRESSDETAIL = "参数化运行(mssql以'@参数名'表示，ODBC以'?'表示)";
        public const string CAT_CONN = "连接";
        public const string CAT_CONN_SETT = "连接设置";
        
    }
}
