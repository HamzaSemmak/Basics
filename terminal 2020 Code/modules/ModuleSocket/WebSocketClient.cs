using sorec_gamma.modules.ModuleSocket.StompHelper;
using System;
using WebSocketSharp;
using sorec_gamma.modules.ModuleEvents.SignauxEvents.models;
using Newtonsoft.Json;
using sorec_gamma.modules.ModuleCoteRapport.models;
using sorec_gamma.modules.ModuleEvents.CotesEvents.models;
using sorec_gamma.modules.ModuleEvents.SignauxEvents.controls;
using sorec_gamma.modules.TLV;
using sorec_gamma.modules.ModulePari.Services;
using System.Threading;
using sorec_gamma.modules.Config;
using System.ComponentModel;

namespace sorec_gamma.modules.ModuleSocket
{
    public class WebSocketClient
    {
        private static WebSocket ws;
        private readonly StompMessageSerializer serializer = new StompMessageSerializer();
        private int idSubcriber = 0;
        private readonly System.Timers.Timer pingTimer;
        private readonly System.Timers.Timer _reconnectTimer;
        private static WebSocketClient instanceWSC = null;
        private static readonly object _lock = new object();

        private WebSocketClient()
        {
            try
            {
                ws_OnClose(null, null);
                ws = new WebSocket(ConfigUtils.ConfigData.GetWSAddress());
                pingTimer = new System.Timers.Timer(15 * 1000);
                pingTimer.Elapsed += (sender, args) =>
                {
                    _ = ws.Ping();
                };
                pingTimer.Enabled = false;
                _reconnectTimer = new System.Timers.Timer(2 * 1000);
                _reconnectTimer.Elapsed += (sender, args) =>
                {
                    ws.Connect();
                };
                _reconnectTimer.Enabled = false;
                ws.OnMessage += ws_OnMessage;
                ws.OnClose += ws_OnClose;
                ws.OnOpen += ws_OnOpen;
                ws.OnError += ws_OnError;
                ws.ConnectAsync();
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.Error("Erreur Creation webSocket. Cause : " + ex);
            }
        }
        private void Subscribe(string topic)
        {
            StompMessage sub = new StompMessage(StompFrame.SUBSCRIBE);
            sub["id"] = "sub-" + idSubcriber;
            sub["content-type"] = "application/json";
            sub["destination"] = topic;
            ws.Send(serializer.Serialize(sub));
            idSubcriber++;
        }

        private void ConnectStomp()
        {
            StompMessage connect = new StompMessage(StompFrame.CONNECT);
            connect["accept-version"] = "1.2";
            connect["host"] = "";
            // first number Zero mean client not able to send Heartbeat, 
            // second number mean Server will sending heartbeat to client instead
            connect["heart-beat"] = "0,10000";
            ws.Send(serializer.Serialize(connect));
        }

        private void ws_OnOpen(object sender, EventArgs e)
        {
            Thread getCoteThread = new Thread(new ThreadStart(_getCoteFromMt))
            {
                Name = "GETCOTES"
            };
            getCoteThread.Priority = ThreadPriority.Lowest;
            getCoteThread.Start();
            if (pingTimer != null)
                pingTimer.Enabled = true;
            if (_reconnectTimer != null)
                _reconnectTimer.Enabled = false;
            if (!ApplicationContext.IsNetworkOnline)
            {
                ApplicationContext.IsNetworkOnline = true;
                ProgressChangedEventArgs connEventArgs = new ProgressChangedEventArgs(0, ApplicationContext.IsNetworkOnline);
                ApplicationContext.HealthConnEventHandler?.Invoke(null, connEventArgs);
            }
            ConnectStomp();
        }

        private void ws_OnError(object sender, ErrorEventArgs e)
        {
            //ApplicationContext.Logger.Error("Erreur WebSocket : " + e.Message);
        }

        private void ws_OnClose(object sender, CloseEventArgs e)
        {
            try
            {
                idSubcriber = 0;
                if (pingTimer != null)
                {
                    pingTimer.Enabled = false;
                }
                if (ApplicationContext.IsNetworkOnline)
                {
                    ApplicationContext.IsNetworkOnline = false;
                    ProgressChangedEventArgs connEventArgs = new ProgressChangedEventArgs(0, ApplicationContext.IsNetworkOnline);
                    ApplicationContext.HealthConnEventHandler?.Invoke(null, connEventArgs);
                }
                if (_reconnectTimer != null)
                {
                    _reconnectTimer.Enabled = true;
                }
            }
            catch { }
        }

        private void ws_OnMessage(object sender, MessageEventArgs e)
        {
            StompMessage msg = serializer.Deserialize(e.Data);
            if (msg.Command == StompFrame.CONNECTED)
            {
                Subscribe("/topic-signaux");
                Subscribe("/topic-rapports");
                Subscribe("/topic-cotes");
            }
            else if (msg.Command == StompFrame.MESSAGE)
            {
                var destination = msg.Headers["destination"];
                switch (destination)
                {
                    case "/topic-signaux":
                        SignalEventModel sem = JsonConvert.DeserializeObject<SignalEventModel>(msg.Body);
                        SignalEventControl.HandleSignal(sem);
                        break;
                    case "/topic-rapports":
                        CourseRapport rem = JsonConvert.DeserializeObject<CourseRapport>(msg.Body);
                        SignalEventControl.HandleRapport(rem);
                        break;
                    case "/topic-cotes":
                        CotesEventModel cem = JsonConvert.DeserializeObject<CotesEventModel>(msg.Body);
                        SignalEventControl.HandleCotes(cem);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void InitWSC()
        {
            lock (_lock)
            {
                if (instanceWSC != null)
                {
                    instanceWSC.Close();
                }
                instanceWSC = new WebSocketClient();
            }
        }

        private static void _getCoteFromMt()
        {
            TLVHandler coteTagsHandler = new TLVHandler();
            coteTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_DEMANDE_COTE);
            string tlvData = coteTagsHandler.toString();
            string tlvDataHash256 = Utils.getSha256(tlvData);
            byte[] macBytes = Utils.macSign(tlvDataHash256);
            string macString = Utils.bytesToHex(macBytes);
            string res = CoteService.SendRequest(tlvData, macString);
            string coteTlv = TLVHandler.getTLVChamps(res);
            TLVHandler coteRespTagsHandler = new TLVHandler(coteTlv);
            TLVTags coteDataTag2 = coteRespTagsHandler.getTLV(TLVTags.SOREC_DATA_COTES);
            if (coteDataTag2 != null && coteDataTag2.length > 0)
            {
                ApplicationContext.SOREC_DATA_COTES.Add(coteDataTag2);
            }
        }
        private void Close()
        {
            if (pingTimer != null)
                pingTimer.Stop();
            if (_reconnectTimer != null)
                _reconnectTimer.Stop();
            if (ws != null)
            {
                ws.CloseAsync();
                ws.OnOpen -= ws_OnOpen;
                ws.OnMessage -= ws_OnMessage;
                ws.OnError -= ws_OnError;
                ws.OnClose -= ws_OnClose;
            }
        }
    }
}
