using System.Collections.Generic;

namespace FirePredictionSystem.Additional.C45
{

    public static class IO
    {
        public static void WriteFile(string path, string data)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding(1251).GetBytes(data);
            using (System.IO.FileStream fileStream = new System.IO.FileStream(
                    path,
                    System.IO.FileMode.Create))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }

        public static string ReadFile(string path)
            => new System.IO.StreamReader(path, System.Text.Encoding.Default).ReadToEnd();

        public static string GenerateInput(int numberOfData)
        {
            string data = "",
                   type = "", gunType = "", answer = "",
                   angle = "", distance = "", score = "";

            System.Random rand = new System.Random();

            data += "Type GunType Angle Distance Score Answer" + "\r\n";

            for (int i = 1; i < numberOfData; i++)
            {
                type = "Стрелок" + rand.Next(1, 6);
                gunType = rand.Next(0, 10) > 5 ? "Нарезной" : "Гладкий";
                angle = rand.Next(1, 1000) < 300 ? "<60" :
                        rand.Next(1, 1000) < 600 ? "60<=x<85" : ">85";//rand.Next(50, 95);
                distance = rand.Next(1, 1000) < 300 ? "100" :
                           rand.Next(1, 1000) < 600 ? "150" : "200";
                score = rand.Next(1, 1000) < 300 ? "<600" :
                        rand.Next(1, 1000) < 600 ? "600<=x<=700" : ">700";//rand.Next(500, 800);

                if (score == "<600")
                {
                    answer = "No";
                }
                else if (
                        (score == ">700" && distance == "100")
                            ||
                        (score == ">700" && distance == "150")
                            ||
                        (gunType == "Нарезной" && distance == "100")
                            ||
                        (gunType == "Нарезной" && distance == "150")
                            ||
                        (gunType == "Нарезной" && angle == ">85" && distance != "200")
                            ||
                        (gunType == "Нарезной" && angle == "<60" && distance != "200")
                            ||
                        (gunType == "Нарезной" && angle == "60<=x<85" && distance != "200")
                            ||
                        (gunType == "Гладкий" && distance == "100")
                            ||
                        (gunType == "Гладкий" && angle == ">85")

                        )
                {
                    answer = "Yes";
                }
                else if (
                         (gunType == "Нарезной" && distance == "150")
                            ||
                         (gunType == "Нарезной" && distance == "200")
                            ||
                         (gunType == "Гладкий" && angle == "60<=x<85")
                            ||
                         (gunType == "Гладкий" && angle == "<60")
                            ||
                         (angle == "60<=x<85" && distance == "150")
                            ||
                         (angle == "60<=x<85" && distance == "200")
                            ||
                         (angle != "<60" && distance == "200")
                        )
                {
                    answer = "No";
                }
                else
                {
                    System.Console.WriteLine("неучтенный");
                    answer = "Yes";
                }
                data += $"{type} {gunType} {angle} {distance} {score} {answer}" + ((i != numberOfData - 1) ? "\r\n" : "");
            }

            return data;
        }
    }

    public class Input
    {
        public string[][] Data { get; set; }

        public Input()
        {

        }

        public Input(string[][] data)
        {
            int rLength = data.Length,
                cLength;
            Data = new string[rLength][];
            for (int i = 0; i < rLength; i++)
            {
                Data[i] = new string[3];
            }
            for (int r = 0; r < rLength; r++)
            {
                cLength = data[r].Length;
                for (int c = 0; c < cLength; c++)
                {
                    Data[r][c] = data[r][c];
                }
            }
        }
        
        public string[] GetAttributeColumn(int attributeNumber)
        {
            string[] result = new string[Data.Length];

            for (int i = 0; i < Data.Length; i++)
                result[i] = Data[i][attributeNumber];

            return result;
        }
        
        public Input GetSubset(string attribute,
                               string attributeValue)
        {
            int attributeNumber;

            for (attributeNumber = 1;
                 attributeNumber < Data[0].Length - 1;
                 attributeNumber++)
            {
                if (Data[0][attributeNumber] == attribute)
                    break;
            }

            var data = new List<string[]>();
            data.Add(Data[0]);

            for (int i = 1; i < Data.Length; i++)
            {
                if (Data[i][attributeNumber] == attributeValue)
                    data.Add(Data[i]);
            }

            var subset = new Input();
            subset.Data = data.ToArray();

            return subset;
        }

    }
}
