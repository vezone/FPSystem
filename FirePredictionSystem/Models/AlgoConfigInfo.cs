using System;

namespace FirePredictionSystem.Models
{

    public static class IO
    {
        public static string ReadFile(string path)
            => new System.IO.StreamReader(path, System.Text.Encoding.Default).ReadToEnd();
    }

    public static class AlgoConfigInfo
    {
        public static string[] Info;
        
        public static void ReadConfig()
        {
            if (System.IO.File.Exists(@"config\config.txt"))
            {
                string input = IO.ReadFile(@"config\config.txt");
                if (input.Length > 0)
                {
                    int index = 0, configParamsCounter = 0;
                    while ((index = input.IndexOf(":", index)) != -1)
                    {
                        ++configParamsCounter;
                        ++index;
                    }

                    Info = new string[configParamsCounter];
                    int beginIndex = 0;
                    int endIndex = 0;

                    for (int p = 0; p < configParamsCounter; p++)
                    {
                        beginIndex = input.IndexOf(":", beginIndex) + 1;
                        endIndex = input.IndexOf("\r", beginIndex);

                        for (int i = beginIndex; i < endIndex; i++)
                        {
                            Info[p] += input[i];
                        }
                    }
                }
                else
                {
                    //handle config error
                }
            }
            else
            {
                //handle error
            }

            
        }

    }
}
