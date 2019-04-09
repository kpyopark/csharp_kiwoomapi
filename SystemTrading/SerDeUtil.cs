using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jil;
using NetMQ;
using NetMQ.Sockets;
using System.Threading;
using System.Collections.Concurrent;

namespace SystemTrading
{
    public class SerDeUtil
    {
        public static string SerializeRealDataStructure(TrStructure structure)
        {
            using (var output = new StringWriter())
            {
                JSON.SerializeDynamic(
                   structure,
                    output
                );
                return output.ToString();
            }
        }

        public static TrStructure DeserializeRealDataStructure(string ser)
        {
            TrStructure rtn = JSON.Deserialize<TrStructure>(ser);
            return rtn;
        }

        static bool s_pubServerStart = false;
        static object s_pubServerLock = new object();
        static BlockingCollection<string> realTimeQueue = new BlockingCollection<string>();

        public static void PublishRealTimeStructure(TrStructure structure)
        {
            if(!s_pubServerStart)
                lock (s_pubServerLock)
                {
                    if (!s_pubServerStart)
                    {
                        ThreadStart ts = new ThreadStart(StartPublishingServer);
                        Thread daemonThread = new Thread(ts);
                        daemonThread.Name = "ZeroMQServer";
                        daemonThread.IsBackground = true;
                        daemonThread.Start();
                        s_pubServerStart = true;
                    }
                }
            realTimeQueue.Add(SerializeRealDataStructure(structure));
        }

        /**
         * The object pipeline is 
         * 
         * 
         * */
        public static void StartPublishingServer()
        {
            // TODO : The below codes from NetMQ Samples(
            using (var publisher = new PublisherSocket())
            {
                publisher.Bind("tcp://127.0.0.1:5556");
                while (true)
                {
                    string rtStruct;
                    rtStruct = realTimeQueue.Take();
                    publisher.SendMoreFrame("realtime").SendFrame($"{rtStruct}");
                }
            }
        }

    }
}
