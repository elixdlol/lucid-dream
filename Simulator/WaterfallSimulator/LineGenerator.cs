using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterfallSimulator
{
    public class LineGenerator
    {
        private int diagonalPosition = 0;
        Random rnd = new Random();

        /// <summary>
        /// Generates a random bytes array.
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateRandomLine()
        {
            byte[] data = new byte[4096];
            rnd.NextBytes(data);

            for (int i = 0; i < data.Length; i++)
                data[i] = (byte)rnd.Next(0, 150);

            validateData(ref data);
            AddGrid(ref data);
            AddDiagonalLine(ref data);
            return data;
        }

        private void validateData(ref byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == (byte)'P' || data[i] == (byte)'C')
                    data[i] = 0;
            }
        }

        private void AddGrid(ref byte[] data)
        {
            for (int i = 0; i < data.Length; i += 256)
            {
                data[i] = 255;
            }
        }
        private void AddDiagonalLine(ref byte[] data)
        {
            if (diagonalPosition == 1000)
            {
                diagonalPosition = 0;
            }

            var diagonalPosition1 = (diagonalPosition + 250) % 1000;
            var diagonalPosition2 = (diagonalPosition + 500) % 1000;
            var diagonalPosition3 = (diagonalPosition + 750) % 1000;

            data[diagonalPosition] = 255;
            data[diagonalPosition1] = 255;
            data[diagonalPosition2] = 255;
            data[diagonalPosition3] = 255;

            if (diagonalPosition > 0)
            {
                data[diagonalPosition - 1] = 255;
            }
            if (diagonalPosition1 > 0)
            {
                data[diagonalPosition1 - 1] = 255;
            }
            if (diagonalPosition2 > 0)
            {
                data[diagonalPosition2 - 1] = 255;
            }
            if (diagonalPosition3 > 0)
            {
                data[diagonalPosition3 - 1] = 255;
            }



            diagonalPosition++;
        }

        private int currentLineNumber = 0;
        private int currentPageNumber = 0;
        public byte[] ReadLineFromWF()
        {
            byte[] tempData = new byte[4*4096];
            byte[] data = new byte[4096];


            var waterfallPage = FileManager.readWfPageFromFile(currentPageNumber);
            if (waterfallPage == null)
            {
                currentPageNumber = 0;
                waterfallPage = FileManager.readWfPageFromFile(currentPageNumber);
            }

            var linesList = waterfallPage.Values.ToList();

            if (currentLineNumber >= linesList.Count)
            {
                currentLineNumber = 0;
                currentPageNumber++;
                waterfallPage = FileManager.readWfPageFromFile(currentPageNumber);
                linesList = waterfallPage.Values.ToList();
            }
            var returnLineNumber = currentLineNumber;
            currentLineNumber++;

            for (int i = 0, j = 0; i < 4*4096; i = i+4, j++)
            {
                data[j] = linesList[returnLineNumber][i];
            }

            return data;
        }
    }
}
