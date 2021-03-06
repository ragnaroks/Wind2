> Wind is a daemon service  
> this project designed to create a `systemd` for windows

### Projects
- `wind` is Wind's windows service
- `windctl` is Wind's local command-line controller based pipeline
- `ExampleUnit` is an example unit to test functionality

****

### Install
0. require [dotnet core runtime 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
1. download released package file and unzip to local disk,i suggest `C:\ProgramData\wind\`
2. open an **Administrator** privilege command window
3. execute `wind.exe action:install`
4. create a unit file into `.\Units\` directory,example is down below
5. execute `sc.exe start Wind` to start Wind daemon service
6. under normal circumstances,your unit has been started

**if you be worry about high level privilege,you can use `services.msc` to change to low level privilege,but maybe some thing wrong,like network monitor must need privilege**

****

### Uninstall
1. open an **Administrator** privilege command window
2. execute `sc.exe stop Wind` to stop Wind daemon service and all units
3. change directory into Wind directory
4. execute `wind.exe action:uninstall`
5. delete Wind directory

****

### Unit settings file example
```javascript
{
    // unit display name,must set up
    "Name": "Example Unit",
    // unit display description,must set up
    "Description": "Example Unit Description",
    // unit type,must set up,0:simple,1:fork
    "Type": 0,
    // unit executeable file path,must set up
    "AbsoluteExecutePath": "D:\\Projects\\wind\\ExampleUnit\\bin\\Debug\\netcoreapp3.1\\ExampleUnit.exe",
    // unit work directory,must set up
    "AbsoluteWorkDirectory": "D:\\Projects\\wind\\ExampleUnit\\bin\\Debug\\netcoreapp3.1",
    // unit execute arguments
    "Arguments": null,
    // unit will start after Wind daemon service started
    "AutoStart": true,
    // unit start delay seconds,only for auto start
    "AutoStartDelay": 3,
    // unit will restart when unit exit by exception
    "RestartWhenException": false,
    // unit process priority,default is "Normal"
    // do not modify this field if you do not know what it is
    "PriorityClass": null,
    // unit process CPU affinity,default is "0(All)"
    // do not modify this field if you do not know what it is
    "ProcessorAffinity": null,
    // input encoding,default is native codepage,list:https://docs.microsoft.com/zh-cn/dotnet/api/system.text.encoding?view=net-5.0#list-of-encodings
    // do not modify this field if you do not know what it is
    "ProcessStandardInputEncoding": null,
    // output encoding,default is native codepage,list:https://docs.microsoft.com/zh-cn/dotnet/api/system.text.encoding?view=net-5.0#list-of-encodings
    // do not modify this field if you do not know what it is
    "ProcessStandardOutputEncoding": null,
    // error output encoding,default is native codepage,list:https://docs.microsoft.com/zh-cn/dotnet/api/system.text.encoding?view=net-5.0#list-of-encodings
    // do not modify this field if you do not know what it is
    "ProcessStandardErrorEncoding": null,
    // monitor unit performance
    "MonitorPerformanceUsage": false,
    // monitor unit network usage
    "MonitorNetworkUsage": false,
    // Environment Variables
    // field value must be String KeyValuePair,use null if you do not need this
    "EnvironmentVariables": {
        "key1": "value1",
        "key2": "value2"
    }
}
```

### Wind settings
> `.\Data\AppSettings.json`
```javascript
{
    // enable remote control
    // if it's false, windctl will unavailable
    "EnableRemoteControl": true,
    // remote control listen address,only IPv4
    "RemoteControlListenAddress": "localhost",
    // remote control listen port,1024 < PORT < 65535
    "RemoteControlListenPort": 3721,
    // remote control key,use by validate websocket connections
    "RemoteControlKey": "https://github.com/ragnaroks/Wind"
}
```

****

### Attention
- unit will inherited Wind daemon service privilege,only host your trust application
- if Wind exit by excetion,maybe not stop all units,you should stop them manual
- if add an unit file,but not did what you want,check in `.\Logs\`
- no plan to use `wss://`,you can use nginx proxy
- `MonitorNetworkUsage` setting need windows10 or higher

### Known Issues
- can't auto start service on windows7,even it's set auto-start

****

### Compatible
- [iPEX](https://github.com/ragnaroks/ipex)
- [aria2](https://github.com/aria2/aria2)
- [nginx](https://github.com/nginx/nginx)
- [v2ray](https://github.com/v2ray/v2ray-core)
- [kcptun](https://github.com/xtaci/kcptun)
- [frp](https://github.com/fatedier/frp)
- [webd](https://webd.cf/) require `-h` argument
- [minecraft-server](https://github.com/PaperMC) like `java -jar paperclip.jar -nogui`
- [terraria-server](https://www.terraria.org/) require `"StandardInputEncoding":"Unicode"`
- [cloudreve](https://github.com/cloudreve/Cloudreve)
- [IPBan](https://github.com/DigitalRuby/IPBan)

### Imcompatible
- any GUI application
