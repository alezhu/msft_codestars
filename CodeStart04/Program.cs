using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//На международном музыкальном фестивале участники говорят на множестве разных языков. 
//Чтобы все могли понимать друг друга, организаторы предложили использовать автоматические 
//переводчики, но переводчики есть не для всех пар языков. 
//В текстовом файле в каждой строке содержится (через пробел) имя переводчика, с какого языка 
//и на какой он может переводить. 
//Какое минимальное количество переводчиков необходимо, чтобы переводить с Исландского на Албанский?
namespace CodeStart04
{
    class Data
    {
        public string Src;
        public string Dst;
        public string Trans;
    }

    class ResultInfo : Tuple<int, List<string>, HashSet<string>>
    {
        public ResultInfo(int count, List<string> path, HashSet<string> trans) : base(count, path, trans) { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var links = new Dictionary<string, List<Data>>();
            using (var file = File.OpenText("25.data"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var split = line.Split(' ');

                    var data = new Data
                    {
                        Trans = split[0],
                        Src = split[1],
                        Dst = split[2]
                    };
                    List<Data> list = null;
                    if (!links.TryGetValue(data.Src, out list))
                    {
                        list = new List<Data>();
                        links.Add(data.Src, list);
                    }
                    list.Add(data);

                }
            }


            var info = GetMinPath(links, "Исландский", "Албанский", new HashSet<string>());
            Console.WriteLine(info.Item1);
            foreach (var item in info.Item2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(info.Item3.Count);
            Console.ReadKey();
        }


        private static ResultInfo GetMinPath(Dictionary<string, List<Data>> links, string from, string to, HashSet<string> entered)
        {
            int min = 0;
            List<string> path = null;
            Data data = null;
            HashSet<string> set = null;

            entered.Add(from);

            var list = links[from];

            foreach (var item in list)
            {
                if (item.Dst == to)
                {
                    return new ResultInfo(1, new List<string>(){
                        from,to
                    }, new HashSet<string>() {
                        item.Trans
                    }
                    );
                }
            }

            foreach (var item in list)
            {
                if (!entered.Contains(item.Dst))
                {

                    var info = GetMinPath(links, item.Dst, to, entered);
                    if (path == null || min > info.Item1)
                    {
                        min = info.Item1;
                        path = info.Item2;
                        set = info.Item3;
                        data = item;
                    }
                }

            }
            if (path != null)
            {
                path.Insert(0, from);

            }

            if (set != null)
            {
                set.Add(data.Trans);
            }
            return new ResultInfo(path == null ? 0 : min + 1, path, set);
        }
    }
}
