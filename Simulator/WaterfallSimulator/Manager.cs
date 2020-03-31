using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using WaterfallSimulator.Properties;

namespace WaterfallSimulator
{
    public class Manager
    {
        private DispatcherTimer timer;
        private LineGenerator generator;
        private LSRHeaderGenerator lsrHeaderGen;
        private ComWriter writer;

        public Manager()
        {
            writer = new ComWriter();
            generator = new LineGenerator();
            lsrHeaderGen = new LSRHeaderGenerator();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Tick += Timer_Tick;
        }

        public void Start()
        {
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //writer.SendCommand('F');
            byte[] dataToSend = new byte[4121];
            dataToSend[0] = Convert.ToByte('P');
            byte[] temp = lsrHeaderGen.CreateHeader();
            for (int i = 1; i < 25; i++)
            {
                dataToSend[i] = temp[i - 1];

            }

            if (Settings.Default.SendRecordedData)
            {
                temp = generator.ReadLineFromWF();
            }
            else
            {
                temp = generator.GenerateRandomLine();
                
            }
            for (int i = 25; i < dataToSend.Length; i++)
            {
                dataToSend[i] = temp[i - 25];
            }
            writer.Send(dataToSend);
            //writer.SendCommand('P');
            //writer.Send(lsrHeaderGen.CreateHeader());

            //if (Settings.Default.SendRecordedData)
            //{
            //    writer.Send(generator.ReadLineFromWF());
            //}
            //else
            //{
            //    writer.Send(generator.GenerateRandomLine());
            //}

        }
    }
}
