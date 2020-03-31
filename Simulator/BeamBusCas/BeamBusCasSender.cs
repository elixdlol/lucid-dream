using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace BeamBusCas
{
    public static class BeamBusCasSender
    {
        static int subSegmentNum;
        static byte[][] subSements;
        static UDPSocket client;
        public static bool isSending = false;
        public static bool _isFirst = true;


        public static void SendMessage()
        {
            if (_isFirst)
            {
                _isFirst = false;
                subSements = FileEdit.GetRecording(Properties.Settings.Default.Recording_path);
                client = new UDPSocket();
                client.Client(Properties.Settings.Default.IP,
                Properties.Settings.Default.Port);
                subSegmentNum = subSements.Length;
            }


            while (isSending)
            {
                for (int j = 0; j < subSegmentNum; j++)
                {
                    client.Send(subSements[j]);
                    delayInMs(0.1);
                    if (!isSending)
                    {
                        break;
                    }
                }
            }
        }

        private static void delayInMs(double ms)
        {
            for (int i = 0; i < ms * 280000; i++)
            {

            }
        }
    }
}
