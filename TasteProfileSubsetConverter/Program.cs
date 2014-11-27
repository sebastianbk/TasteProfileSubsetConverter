using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasteProfileSubsetConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new Dictionary<string, List<int>>();
            var songs = new Dictionary<string, List<int>>();
            var timesPlayed = new Dictionary<int, int>();

            var file = new System.IO.StreamReader(@"C:\Temp\train_triplets.txt");

            int counter = 0;
            string line;
            while ((line = file.ReadLine()) != null)
            {
                try
                {
                    Console.WriteLine(counter);
                    var lineArray = line.Split('\t');
                    timesPlayed.Add(counter, Int32.Parse(lineArray[2]));

                    if (users.ContainsKey(lineArray[0]))
                    {
                        users[lineArray[0]].Add(counter);
                    }
                    else
                    {
                        users.Add(lineArray[0], new List<int> { counter });
                    }

                    if (songs.ContainsKey(lineArray[1]))
                    {
                        songs[lineArray[1]].Add(counter);
                    }
                    else
                    {
                        songs.Add(lineArray[1], new List<int> { counter });
                    }
                }
                catch
                {
                    // Couldn't process line. Moving on...
                }

                counter++;

                //if (counter > 50000)
                //    break;
            }

            file.Close();

            var newUsers = new Dictionary<int, int>();

            int userId = 1;
            foreach (var user in users)
            {
                Console.WriteLine(user.Key + " => " + userId);
                foreach (var triplet in user.Value)
                {
                    newUsers.Add(triplet, userId);
                }
                userId++;
            }

            var newSongs = new Dictionary<int, int>();

            int songId = 1;
            foreach (var song in songs)
            {
                Console.WriteLine(song.Key + " => " + songId);
                foreach (var triplet in song.Value)
                {
                    newSongs.Add(triplet, songId);
                }
                songId++;
            }

            var output = new System.IO.StreamWriter(@"C:\Temp\output.txt", true, Encoding.Default);

            for (int i = 0; i < timesPlayed.Count; i++)
            {
                var outputLine = newUsers[i] + "," + newSongs[i] + "," + timesPlayed[i];
                Console.WriteLine(i + ": " + outputLine);
                output.WriteLine(outputLine);
            }

            output.Close();

            var usersFile = new System.IO.StreamWriter(@"C:\Temp\users.txt", true, Encoding.Default);

            for (int i = 1; i <= userId; i++)
            {
                Console.WriteLine(i);
                usersFile.WriteLine(i);
            }

            usersFile.Close();
        }
    }
}
