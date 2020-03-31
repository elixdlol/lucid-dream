using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WaterfallSimulator
{
    class ComWriter : IWriter
    {
        private SerialPort serialPort;

        public ComWriter()
        {
            serialPort = new SerialPort();
            Configure();
        }

        public void Configure()
        {
            #region Serial Port config
            serialPort.PortName = "COM2";
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.BaudRate = 38400;
            serialPort.Parity = Parity.None;
            serialPort.WriteBufferSize = 4096;
            serialPort.Encoding = Encoding.ASCII;
            #endregion
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }

        }

        public void SendCommand(char command)
        {
            //Task.Run(() =>
            //{
            serialPort.Write(command.ToString());
            serialPort.DiscardOutBuffer();
            //});
        }

        public void Send(byte[] data)
        {
            //Task.Run(() =>
            //{
            serialPort.Write(data, 0, data.Length);
            serialPort.DiscardOutBuffer();
            //});
        }

    }
}
