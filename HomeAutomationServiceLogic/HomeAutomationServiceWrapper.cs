using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data;
using System.Data.SqlClient;

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;

using TSCHelpers;
using HomeAutomationServiceLogic.Helpers;

namespace HomeAutomationServiceLogic
{
    public static class HomeAutomationServiceWrapper
    {
        private static Task mqttListenerTask;
        private static Task databaseWriterTask;
        private static bool continueRunning;
        private static ConcurrentQueue<Tuple<string, string>> messageQueue = new ConcurrentQueue<Tuple<string, string>>();

        private static IMqttClient mqttClient;

        public static void OnStart(string[] args)
        {
            HelperSettings.ReadSettings();
            LogHelper.SetupLogging();

            LogHelper.LogTrace();

            continueRunning = true;

            LogHelper.LogInfo($"{Global.ServiceName} started.");

            mqttListenerTask = Task.Run(() => ListenForMqttMessages());
            databaseWriterTask = Task.Run(() => WriteMqttMessagesToDatabase());

            LogHelper.LogTrace();
        }

        public static void OnStop()
        {
            LogHelper.LogTrace();

            continueRunning = false;

            if (mqttClient != null)
            {
                mqttClient.DisconnectAsync().RunSynchronously();
            }

            Task.WaitAll(new[] { mqttListenerTask, databaseWriterTask }, TimeSpan.FromSeconds(SettingsHelper.Settings.ServiceStopWaitSeconds));

            LogHelper.LogInfo($"{Global.ServiceName} stopped.");
        }

        public static void OnShutdown()
        {
            LogHelper.LogTrace();
            LogHelper.LogInfo($"{Global.ServiceName} shutdown event.");
        }

        private static void ListenForMqttMessages()
        {
            LogHelper.LogTrace();

            MqttClientOptions options = new MqttClientOptionsBuilder()

            public MqttClientOptionsBuilder WithAuthentication(string method, byte[] data);
            public MqttClientOptionsBuilder WithCleanSession(bool value = true);
            public MqttClientOptionsBuilder WithCleanStart(bool value = true);
            public MqttClientOptionsBuilder WithConnectionUri(string uri);
            public MqttClientOptionsBuilder WithCredentials(string username, string password);
            public MqttClientOptionsBuilder WithCredentials(string username, byte[] password = null);

            public MqttClientOptionsBuilder WithCredentials(IMqttClientCredentialsProvider credentials);




            public MqttClientOptionsBuilder WithNoKeepAlive();


            public MqttClientOptionsBuilder WithProtocolType(ProtocolType protocolType);
            public MqttClientOptionsBuilder WithProtocolVersion(MqttProtocolVersion value);
            [Obsolete("Use WithWebSocketServer(... configure) instead.")]
            public MqttClientOptionsBuilder WithProxy(string address, string username = null, string password = null, string domain = null, bool bypassOnLocal = false, string[] bypassList = null);
            [Obsolete("Use WithWebSocketServer(... configure) instead.")]
            public MqttClientOptionsBuilder WithProxy(Action<MqttClientWebSocketProxyOptions> optionsBuilder);
            public MqttClientOptionsBuilder WithReceiveMaximum(ushort receiveMaximum);
            public MqttClientOptionsBuilder WithRequestProblemInformation(bool requestProblemInformation = true);
            public MqttClientOptionsBuilder WithRequestResponseInformation(bool requestResponseInformation = true);
            public MqttClientOptionsBuilder WithSessionExpiryInterval(uint sessionExpiryInterval);
            public MqttClientOptionsBuilder WithTcpServer(Action<MqttClientTcpOptions> optionsBuilder);
            public MqttClientOptionsBuilder WithTcpServer(string host, int? port = null, AddressFamily addressFamily = AddressFamily.Unspecified);
            //
            // Summary:
            //     Sets the timeout which will be applied at socket level and internal operations.
            //     The default value is the same as for sockets in .NET in general.
            public MqttClientOptionsBuilder WithTimeout(TimeSpan value);
            [Obsolete("Use WithTlsOptions(... configure) instead.")]
            public MqttClientOptionsBuilder WithTls();
            [Obsolete("Use WithTlsOptions(... configure) instead.")]
            public MqttClientOptionsBuilder WithTls(Action<MqttClientOptionsBuilderTlsParameters> optionsBuilder);
            [Obsolete("Use WithTlsOptions(... configure) instead.")]
            public MqttClientOptionsBuilder WithTls(MqttClientOptionsBuilderTlsParameters parameters);
            public MqttClientOptionsBuilder WithTlsOptions(MqttClientTlsOptions tlsOptions);
            public MqttClientOptionsBuilder WithTlsOptions(Action<MqttClientTlsOptionsBuilder> configure);
            public MqttClientOptionsBuilder WithTopicAliasMaximum(ushort topicAliasMaximum);
            //
            // Summary:
            //     If set to true, the bridge will attempt to indicate to the remote broker that
            //     it is a bridge not an ordinary client. If successful, this means that loop detection
            //     will be more effective and that retained messages will be propagated correctly.
            //     Not all brokers support this feature so it may be necessary to set it to false
            //     if your bridge does not connect properly.
            public MqttClientOptionsBuilder WithTryPrivate(bool tryPrivate = true);
            public MqttClientOptionsBuilder WithUserProperty(string name, string value);
            [Obsolete("Use WithWebSocketServer(... configure) instead.")]
            public MqttClientOptionsBuilder WithWebSocketServer(string uri, MqttClientOptionsBuilderWebSocketParameters parameters = null);
            public MqttClientOptionsBuilder WithWebSocketServer(Action<MqttClientWebSocketOptionsBuilder> configure);
            [Obsolete("Use WithWebSocketServer(... configure) instead.")]
            public MqttClientOptionsBuilder WithWebSocketServer(Action<MqttClientWebSocketOptions> optionsBuilder);
            public MqttClientOptionsBuilder WithWillContentType(string willContentType);
            public MqttClientOptionsBuilder WithWillCorrelationData(byte[] willCorrelationData);
            public MqttClientOptionsBuilder WithWillDelayInterval(uint willDelayInterval);
            public MqttClientOptionsBuilder WithWillMessageExpiryInterval(uint willMessageExpiryInterval);
            public MqttClientOptionsBuilder WithWillPayload(byte[] willPayload);
            public MqttClientOptionsBuilder WithWillPayload(ArraySegment<byte> willPayload);
            public MqttClientOptionsBuilder WithWillPayload(string willPayload);
            public MqttClientOptionsBuilder WithWillPayloadFormatIndicator(MqttPayloadFormatIndicator willPayloadFormatIndicator);
            public MqttClientOptionsBuilder WithWillQualityOfServiceLevel(MqttQualityOfServiceLevel willQualityOfServiceLevel);
            public MqttClientOptionsBuilder WithWillResponseTopic(string willResponseTopic);
            public MqttClientOptionsBuilder WithWillRetain(bool willRetain = true);
            public MqttClientOptionsBuilder WithWillTopic(string willTopic);
            public MqttClientOptionsBuilder WithWillUserProperty(string name, string value);






                .WithClientId("HomeAutomationService")
                .WithTcpServer(SettingsHelper.Settings.MqttServer, SettingsHelper.Settings.MqttPort)
                .WithCredentials(SettingsHelper.Settings.MqttUsername, SettingsHelper.Settings.MqttPassword)
                .WithCleanSession()
                .Build();

            MqttClientOptions options123 = new MqttClientOptions();
            options123.AllowPacketFragmentation = false;
            options123.AuthenticationData = new byte[123];
            options123.AuthenticationMethod = "string";
            options123.CleanSession = false;
            options123.ClientId = "string";
            //options123.Credentials
            //options123.ChannelOptions
            //options123.ExtendedAuthenticationExchangeHandler
            options123.KeepAlivePeriod = TimeSpan.FromSeconds(5);
            options123.MaximumPacketSize = Convert.ToUInt32(123);
            options123.ProtocolVersion = MqttProtocolVersion.Unknown;
            options123.ProtocolVersion = MqttProtocolVersion.V310;
            options123.ProtocolVersion = MqttProtocolVersion.V311;
            options123.ProtocolVersion = MqttProtocolVersion.V500;
            options123.ReceiveMaximum = ushort.Parse(123.ToString());
            options123.RequestProblemInformation = false;
            options123.RequestResponseInformation = false;
            options123.SessionExpiryInterval = Convert.ToUInt32(123);
            options123.ThrowOnNonSuccessfulConnectResponse = false;
            options123.Timeout = TimeSpan.FromSeconds(0);
            options123.TopicAliasMaximum = ushort.Parse(123.ToString());
            options123.TryPrivate = false;
            options123.UserProperties = new List<MQTTnet.Packets.MqttUserProperty>();
            options123.ValidateFeatures = false;
            options123.WillContentType = "string";
            options123.WillCorrelationData = new byte[123];
            options123.WillDelayInterval = Convert.ToUInt32(123);
            options123.WillMessageExpiryInterval = Convert.ToUInt32(123);
            options123.WillPayload = new byte[123];
            options123.WillPayloadFormatIndicator = MQTTnet.Protocol.MqttPayloadFormatIndicator.Unspecified;
            options123.WillPayloadFormatIndicator = MQTTnet.Protocol.MqttPayloadFormatIndicator.CharacterData;
            options123.WillQualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce;
            options123.WillQualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce;
            options123.WillQualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce;
            options123.WillResponseTopic = "string";
            options123.WillRetain = false;
            options123.WillTopic = "string";
            options123.WillUserProperties = new List<MQTTnet.Packets.MqttUserProperty>();
            options123.WriterBufferSize = 123;
            options123.WriterBufferSizeMax = 123;

            options123.

            mqttClient = new MqttFactory().CreateMqttClient();

            mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;
            mqttClient.ConnectingAsync += MqttClient_ConnectingAsync;
            mqttClient.ConnectedAsync += MqttClient_ConnectedAsync;
            mqttClient.DisconnectedAsync += MqttClient_DisconnectedAsync;

            mqttClient.ConnectAsync(options, CancellationToken.None).RunSynchronously();

            while (continueRunning)
            {
                Thread.Sleep(100); // Keep the task alive
            }
        }

        private static Task MqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            if (continueRunning)
            {
                LogHelper.LogWarning("MQTT client disconnected. Reconnecting...");
                Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                mqttClient.ConnectAsync(options, CancellationToken.None).Wait();
            }
        }

        private static Task MqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("#").Build());
            LogHelper.LogInfo("MQTT client subscribed to all topics.");
        }

        private static Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            var topic = e.ApplicationMessage.Topic;

            LogHelper.LogTrace(message, topic);

            messageQueue.Enqueue(new Tuple<string, string>(topic, message));
        }

        private static Task MqttClient_ConnectingAsync(MqttClientConnectingEventArgs arg)
        {
            throw new NotImplementedException();
        }

        private static void WriteMqttMessagesToDatabase()
        {
            while (continueRunning || !messageQueue.IsEmpty)
            {
                try
                {
                    if (messageQueue.TryDequeue(out Tuple<string, string> message))
                    {
                        LogHelper.LogTrace();

                        SqlParameter[] sqlParms = new SqlParameter[2];
                        sqlParms[0] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 50) { Value = message.Item1 };
                        sqlParms[1] = new SqlParameter("@Message", SqlDbType.VarChar, -1) { Value = message.Item2 };

                        LogHelper.LogTrace(sqlParms[0], sqlParms[1]);

                        DatabaseHelper.ExecuteStoredProcedureNonQuery(SettingsHelper.Settings.StoredProcedure, sqlParms);

                        LogHelper.LogTrace();
                    }
                    else
                    {
                        Thread.Sleep(SettingsHelper.Settings.DatabaseWriteThreadSleepMilliSeconds);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogError("Exception in WriteSyslogMessagesToDatabase thread.", ex);
                }
            }
        }
    }
}

