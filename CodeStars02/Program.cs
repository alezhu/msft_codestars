using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeStar02
{
    class Program
    {
        enum State { MakingLetter, MakedLetter, MakedWord };
        static void Main(string[] args)
        {
            var file = File.OpenText("66.result");
            var sb = new StringBuilder();
            var text = new List<string>();
            string line;
            var re = new Regex(@"\W", RegexOptions.Compiled);
            var yo = new Regex(@"Yo\W?", RegexOptions.Compiled);
            var nice = new Regex(@"Nice\W?", RegexOptions.Compiled);
            int count = 0;
            State state = State.MakedWord;
            char ch;
            const char start = 'a';
            int delta = 1;

            while ((line = file.ReadLine()) != null)
            {
                var words = line.Split(' ');
                foreach (var word in words)
                {
                    if (yo.IsMatch(word))
                    {
                        count++;
                        state = State.MakingLetter;
                    }
                    else if (nice.IsMatch(word))
                    {
                        if (state == State.MakingLetter)
                        {
                            state = State.MakedLetter;
                            ch = (char)(count + start - delta);
                            count = 0;
                            sb.Append(ch);
                        }
                        else if (state == State.MakedLetter)
                        {
                            state = State.MakedWord;
                            text.Add(sb.ToString());
                            sb.Clear();
                        }
                        else if (state == State.MakedWord)
                        {
                            text.Add("");
                        }
                    }
                    //Вашу мать! Зачем в задании писать, что знаки препинания разделют слова, если это не так! 
                    //if (count > 0 && re.IsMatch(word))
                    //{
                    //    state = State.MakedWord;
                    //    ch = (char)(count + start - delta);
                    //    count = 0;
                    //    sb.Append(ch);
                    //    text.Add(sb.ToString());
                    //    sb.Clear();
                    //}
                }

            }
            foreach (var item in text)
            {
                Console.Write(item);
                Console.Write(" ");

            }
            Console.ReadKey();
        }
    }
}
