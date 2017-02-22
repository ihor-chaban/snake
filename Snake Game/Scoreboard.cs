using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Snake_Game
{
    class Scoreboard
    {
        static public string AddScore(Snake a)
        {
            if (File.Exists("records.bin"))
            {
                string records = "";
                int[] data = new int[10];
                FileStream fs = new FileStream("records.bin", FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                BinaryWriter bw = new BinaryWriter(fs);

                for (int i = 0; i < 10; i++)
                    data[i] = br.ReadInt32();
                fs.Seek(0, SeekOrigin.Begin);

                int temp = 0;
                for (var i = 0; i < 10; i++)
                    if (a.score < data[i])
                        temp++;
                    else
                        if (a.score == data[i])
                        {
                            temp = -1;
                            break;
                        }
                        else
                            break;

                if (temp >= 0 && temp <= 8)
                {
                    for (int i = 9; i > temp; i--)
                        data[i] = data[i - 1];
                    data[temp] = a.score;
                }
                else
                    if (temp == 9)
                        data[temp] = a.score;

                for (int i = 0; i < 10; i++)
                    records += Convert.ToString(i + 1) + ": " + Convert.ToInt32(data[i]) + "\r\n";

                if (temp >= 0 && temp < 10)
                    for (int i = 0; i < 10; i++)
                        bw.Write(data[i]);

                bw.Close();
                br.Close();
                fs.Close();
                return records;
            }
            else
                return "records.bin not found";
        }
    }
}
