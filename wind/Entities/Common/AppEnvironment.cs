﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace wind.Entities.Common {
    /// <summary>应用程序环境配置</summary>
    public class AppEnvironment {
        /// <summary>Json序列号规则</summary>
        public JsonSerializerOptions JsonSerializerOptions{get;}=new JsonSerializerOptions{
            PropertyNamingPolicy=null,IgnoreNullValues=false,PropertyNameCaseInsensitive=false,Encoder=System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping};
        /// <summary>物理根路径,有路径分隔符</summary>
        public String BaseDirectory{get;}=AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>日志根路径,无路径分隔符</summary>
        public String LogsDirectory{get;}=AppDomain.CurrentDomain.BaseDirectory+"Logs";
        /// <summary>单元存放根路径,无路径分隔符</summary>
        public String UnitsDirectory{get;}=AppDomain.CurrentDomain.BaseDirectory+"Units";
        /// <summary>单元存放根路径,无路径分隔符</summary>
        public String UnitLogsDirectory{get;}=AppDomain.CurrentDomain.BaseDirectory+"UnitLogs";
        /// <summary>数据根路径,无路径分隔符</summary>
        public String DataDirectory{get;}=AppDomain.CurrentDomain.BaseDirectory+"Data";
        #if DEBUG
        /// <summary>命名管道</summary>
        public String PipelineName{get;}="WIND_DAEMON_PIPELINE_DEBUG";
        /// <summary>网络计数器ETW会话</summary>
        public String UnitsNetworkTraceEventSession{get;}="WIND_UNITS_NETWORK_TRACEEVENT_SESSION_DEBUG";
        /// <summary>是否开发模式</summary>
        public Boolean DevelopMode{get;}=true;
        #else
        /// <summary>命名管道</summary>
        public String PipelineName{get;}="WIND_DAEMON_PIPELINE";
        /// <summary>网络计数器ETW会话</summary>
        public String UnitsNetworkTraceEventSession{get;}="WIND_UNITS_NETWORK_TRACEEVENT_SESSION";
        /// <summary>是否开发模式</summary>
        public Boolean DevelopMode{get;}=false;
        #endif
        
        public override String ToString()=>JsonSerializer.Serialize(this);
    }
}
