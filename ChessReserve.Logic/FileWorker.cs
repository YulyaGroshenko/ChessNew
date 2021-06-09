using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace ChessReserve.Logic
{
    public static class FileWorker
    {
        public static DirectoryInfo Saves
        {
            get
            {
                if (!Directory.Exists("Saves"))
                    Directory.CreateDirectory("Saves");
                return new DirectoryInfo("Saves");
            }
        }
        private static string[] RecordsList { get; set; } =  {"1) name1 - 0",
                                                              "2) name2 - 0",
                                                              "3) name3 - 0",
                                                              "4) name4 - 0",
                                                              "5) name5 - 0"};
        public static FileInfo Records
        {
            get
            {
                if (!File.Exists("Records.txt"))
                    File.WriteAllLines("Records.txt", RecordsList);
                return new FileInfo("Records.txt");
            }
        }
        public static void SetRecords(Player player)
        {
            string[] records = File.ReadAllLines(Records.FullName);
            int index = CheckName(records, player);
            if (index != -1)
            {
                records[index] = records[index].Replace(records[index].Split(" ")[3], player.Victorys.ToString());
            }
            else
            {
                int recordsVictorys = int.Parse(records[4].Split(" ")[3]);
                if (recordsVictorys < player.Victorys)
                {

                    for (int i = 0; i < records.Length; i++)
                    {
                        int victory = int.Parse(records[i].Split(" ")[3]);
                        if (victory == player.Victorys)
                        {
                            break;
                        }
                        else if (player.Victorys > victory)
                        {
                            string name = records[4].Split(" ")[1];
                            records[4] = records[4].Replace(name, player.Name);
                            records[4] = records[4].Replace(records[i].Split(" ")[3], player.Victorys.ToString());
                            break;
                        }
                    }
                }
            }
            SortRecords(records);
            RecordsList = records;
            File.WriteAllLines(Records.FullName, RecordsList);
        }
        private static int CheckName(string[] records, Player player)
        {
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i].Split(" ")[1] == player.Name)
                    return i;
            }
            return -1;
        }
        private static void SortRecords(string[] records)
        {
            for (int i = 0; i < records.Length; i++)
            {
                for (int j = 0; j < records.Length - i - 1; j++)
                {
                    if (int.Parse(records[j].Split(" ")[3]) < int.Parse(records[j + 1].Split(" ")[3]))
                    {
                        string temp = records[j];
                        records[j] = records[j + 1].Replace(records[j + 1].Split(" ")[0], records[j].Split(" ")[0]);
                        records[j + 1] = temp.Replace(temp.Split(" ")[0], records[j + 1].Split(" ")[0]);
                    }
                }
            }
        }
        public static void SavePlayer(Player player)
        {
            string json = JsonConvert.SerializeObject(player, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            File.WriteAllText($"{Saves.Name}\\{player.Name}", json);
        }
        public static Player GetPlayer(string name)
        {
            return JsonConvert.DeserializeObject<Player>(File.ReadAllText($"{Saves.Name}\\{name}"), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
