## Forked version of MatKlucznyk's Qsys Module for Crestron
This version is Simpl# C# only modified to work in Visual Studio 2022 .Net

## Importing
### Copying working folder/project into another project:
This is useful when you are still developing a project imported into another project 
copy both "my_project" & "packages" into the project folder
import existing project .csproj into the projects top level within visual studio
on your core project right click project and add reference, then navigate to my_project\bin\Debug and add my_project.dll
To access the methods use the Namespace of the imported program

### when adding compiled .dll only
add the my_project.dll to the project folder
on your core project right click project and add reference, then navigate to my_project\bin\Debug and add my_project.dll
To access the methods use the Namespace of the imported compiled program (.dll)

## Initialisation 
```c#
QsysCore = new QsysCore();
QsysCore.Initialise();
```

Initialise Named control for feedback (only required for fb)
```c#
QsysCore.LazyLoadNamedControl("My_Control").OnStateChanged += QsysCore_MyControl_OnStateChanged;
```
create the callback function for feedback (only required for fb):
```c#
QsysCore_MyControl_OnStateChanged(object sender, QsysInternalEventsArgs e)
```

Set debug level for debug print:
```c#
QsysCore.Debug();
```


## Example
Init:
```c#
QsysCore = new QsysCore();
QsysCore.OnPrimaryIsConnected += QsysCore_PrimaryIsConnected;
QsysCore.LazyLoadNamedControl("MyNamedControl").OnStateChanged += QsysCore_MyNamedControl_OnStateChanged;
QsysCore.Initialize(deviceDesc, qsysIpAddressString, "", "", "", 0);
QsysCore.Debug(1);

callback for named control:
private void QsysCore_MyNamedControl_OnStateChanged(object sender, QsysInternalEventsArgs e)
{
  CrestronConsole.PrintLine("MyControl State Changed: {0} - {1}", sender, e.BoolValue);
}
```
Sending named control command (bool example)
```c#
QsysCore.LazyLoadNamedControl("MyControl").SendChangeBoolValue(true);
```



