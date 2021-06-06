using System;
using System.Collections.Generic;
using System.IO;
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
                string recordsVictorys = records[index].Split(" ")[3];
                records[index] = records[index].Replace(recordsVictorys, player.Victorys.ToString());
            }
            else
            {
                string RecordsName = records[4].Split(" ")[1];
                string RecordsVictorys = records[4].Split(" ")[3];
                records[4] = records[4].Replace(RecordsName, player.Name);
                records[4] = records[4].Replace(RecordsVictorys, player.Victorys.ToString());
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
