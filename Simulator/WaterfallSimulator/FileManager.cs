using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WaterfallSimulator.Properties;

namespace WaterfallSimulator
{
    public static class FileManager
    {
        /*
            Presentation of the data that is stored in one line:
            ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
            ▌         8 bytes                ▐     4 bytes       ▐      <= 4096 bytes     ▐
            ▌        Time Info               ▐   WF Data Length  ▐  Actual Waterfall Data ▐
            ▌            ▼                   ▐        ▼          ▐            ▼           ▐
            ▌ [0] [1] [2] [3] [4] [5] [6] [7]▐ [8] [9] [10] [11] ▐      [12] - [4108]     ▐
            ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄

        */

        private static BinaryFormatter bformatter = new BinaryFormatter();

        //number of bytes of timeInfo at the beginning of the line represents the miliseconds in long type variable which spread to 8 bytes.
        public const int TimeInfoLength = 8;
        
        
        public static Dictionary<DateTime, byte[]> readWfPageFromFile(int numberOfPage)
        {
            string filePath = Settings.Default.FolderPath + "/" + numberOfPage + ".wf";
            Dictionary<DateTime, byte[]> page = null;

            if (File.Exists(filePath))
            {
                page = new Dictionary<DateTime, byte[]>();
                FileStream wfPageFile = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                byte[] line = new byte[Settings.Default.Page_Width];
                byte[] timeBuffer = new byte[TimeInfoLength];

                bool isPageFinished = false;

                for (int i = 0; i < Settings.Default.Page_Height && !isPageFinished; ++i)
                {
                    wfPageFile.Read(timeBuffer, 0, TimeInfoLength);
                    DateTime time = DateTime.FromBinary(BitConverter.ToInt64(timeBuffer.Take(TimeInfoLength).ToArray(), 0));

                    if (time == DateTime.MinValue)
                    {
                        isPageFinished = true;
                    }
                    else
                    {
                        wfPageFile.Read(line, 0, Settings.Default.Page_Width);

                        page.Add(time, line);

                        line = new byte[Settings.Default.Page_Width];
                        timeBuffer = new byte[TimeInfoLength];
                    }
                }

                wfPageFile.Close();
            }

            return page;
        }
    

       
    }
}
