# drx-sdk-dotnet

C# .NET SDK for generating and consuming digital receipts to and from the Digital Receipt Exchange (dRx)

# Logging SDK Requests
The dRx .NET SDK uses log4net internally for logging. You can enable this by adding the XML below to your app.config file:
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net" />
  </configSections>
  <log4net>
    <appender name="Console" type="log4net.Appender.ConsoleAppender">
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%date [%thread] %-5level %logger - %message%newline" />
      </layout>
    </appender>
    <appender name="RollingFile" type="log4net.Appender.RollingFileAppender">
      <file value="c:\temp\Logs\drx_sdk_dotnet.log" />
      <appendToFile value="true" />
      <maximumFileSize value="10MB" />
      <maxSizeRollBackups value="10" />
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%date [%thread] %-5level %logger - %message%newline" />
      </layout>
    </appender>
    <root>
      <level value="DEBUG" />
      <appender-ref ref="RollingFile" />
    </root>
  </log4net>
</configuration>
```

For full details of log4net please consult the [log4net documentation](https://logging.apache.org/log4net/).