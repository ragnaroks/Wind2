﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace Daemon.Modules {
    public class UnitPerformanceCounterModule:IDisposable {
        public Boolean Useable{get;private set;}=false;

        /// <summary>性能计数器字典</summary>
        private ConcurrentDictionary<String,PerformanceCounter> CpuPerformanceCounterDictionary{get;set;}=new ConcurrentDictionary<String,PerformanceCounter>();
        private ConcurrentDictionary<String,PerformanceCounter> RamPerformanceCounterDictionary{get;set;}=new ConcurrentDictionary<String,PerformanceCounter>();

        #region IDisposable
        private bool disposedValue;

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)
                    this.RemoveAll();
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                this.CpuPerformanceCounterDictionary=null;
                this.RamPerformanceCounterDictionary=null;

                disposedValue=true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~UnitPerformanceCounterModule(){
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: false);
        }

        public void Dispose() {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public Boolean Setup(){
            this.Useable=true;
            return true;
        }

        private void RemoveAll() {
            foreach(var item in this.CpuPerformanceCounterDictionary){item.Value.Dispose(); }
            this.CpuPerformanceCounterDictionary.Clear();
            foreach(var item in this.RamPerformanceCounterDictionary){item.Value.Dispose(); }
            this.RamPerformanceCounterDictionary.Clear();
        }

        public void Add(String unitKey,String processName){
            if(!this.Useable){return;}
            if(this.CpuPerformanceCounterDictionary.ContainsKey(unitKey)){ this.Remove(unitKey); }
            try {
                PerformanceCounter performanceCounter=new PerformanceCounter{CategoryName="Process",CounterName="% Processor Time",InstanceName=processName,ReadOnly=true};
                _=performanceCounter.NextValue();//预热
                if(!this.CpuPerformanceCounterDictionary.TryAdd(unitKey,performanceCounter)) {
                    Helpers.LoggerModuleHelper.TryLog("Modules.UnitPerformanceCounterModule.Add[Error]",$"创建CPU性能计数器成功但加入列表失败");
                }
            }catch(Exception exception) {
                Helpers.LoggerModuleHelper.TryLog("Modules.UnitPerformanceCounterModule.Add[Error]",$"创建CPU性能计数器异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
            }
            
            try {
                PerformanceCounter performanceCounter=new PerformanceCounter{CategoryName="Process",CounterName="Working Set",InstanceName=processName,ReadOnly=true};
                _=performanceCounter.NextValue();//预热
                if(!this.RamPerformanceCounterDictionary.TryAdd(unitKey,performanceCounter)) {
                    Helpers.LoggerModuleHelper.TryLog("Modules.UnitPerformanceCounterModule.Add[Error]",$"创建RAM性能计数器成功但加入列表失败");
                }
            }catch(Exception exception) {
                Helpers.LoggerModuleHelper.TryLog("Modules.UnitPerformanceCounterModule.Add[Error]",$"创建RAM性能计数器异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
            }
        }

        public Boolean Remove(String unitKey) {
            if(!this.Useable){return false;}
            Boolean b1=false;
            if(this.CpuPerformanceCounterDictionary.Count>0 && this.CpuPerformanceCounterDictionary.ContainsKey(unitKey)){
                this.CpuPerformanceCounterDictionary[unitKey].Dispose();
                b1=this.CpuPerformanceCounterDictionary.TryRemove(unitKey,out _);
            }
            Boolean b2=false;
            if(this.RamPerformanceCounterDictionary.Count>0 && this.RamPerformanceCounterDictionary.ContainsKey(unitKey)){
                this.RamPerformanceCounterDictionary[unitKey].Dispose();
                b2=this.RamPerformanceCounterDictionary.TryRemove(unitKey,out _);
            }
            return b1 && b2;
        }

        public Single GetCpuValue(String unitKey){
            if(!this.Useable){return 0F;}
            if(this.CpuPerformanceCounterDictionary.Count<1 || !this.CpuPerformanceCounterDictionary.ContainsKey(unitKey)){return 0F;}
            return this.CpuPerformanceCounterDictionary[unitKey].NextValue();
        }

        public Single GetRamValue(String unitKey){
            if(!this.Useable){return 0F;}
            if(this.RamPerformanceCounterDictionary.Count<1 || !this.RamPerformanceCounterDictionary.ContainsKey(unitKey)){return 0F;}
            return this.RamPerformanceCounterDictionary[unitKey].NextValue();
        }
    }
}