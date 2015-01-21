using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeStars03
{
    class Program
    {
        static void Main(string[] args)
        {
            var langs = new List<HashSet<int>>();
            int[] pair = new int[2];
            HashSet<int>[] set = new HashSet<int>[pair.Length];

            using (var file = File.OpenText("20.data"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    int index = 0;
                    foreach (var item in line.Split(' '))
                    {
                        pair[index++] = int.Parse(item);
                    }

                    for (int i = 0; i < pair.Length; i++)
                    {
                        set[i] = null;
                        foreach (var lang in langs)
                        {
                            if (lang.Contains(pair[i]))
                            {
                                set[i] = lang;
                                break;
                            }
                        }
                    }

                    if (set[0] != set[1])
                    {
                        if (set[0] != null)
                        {
                            if (set[1] == null)
                            {
                                set[0].Add(pair[1]);
                            }
                            else
                            {
                                set[0].UnionWith(set[1]);
                                langs.Remove(set[1]);
                            }
                        }
                        else if (set[1] != null)
                        {
                            set[1].Add(pair[0]);
                        }
                    }
                    else if (set[0] == null)
                    {
                        langs.Add(new HashSet<int>(pair));
                    }

                }
            }
            Console.WriteLine(langs.Count);
            Console.ReadKey();


        }



    }
}
