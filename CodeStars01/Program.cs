using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CodeStars01
{
    class Program
    {
        static void Main(string[] args)
        {
            long iresult = 1;
            BigInteger bresult = 1;
            bool big = false;
            while (true)
            {
                iresult = 1;
                bresult = 1;
                big = false;
                try
                {
                    Console.Write("Enter num: ");
                    int num = int.Parse(Console.ReadLine());

                    for (int i = 1; i <= num; )
                    {
                        checked
                        {
                            try
                            {
                                if (!big)
                                {
                                    iresult = iresult * i;
                                }
                                else
                                {
                                    bresult *= i;
                                }
                                ++i;
                            }
                            catch (OverflowException)
                            {
                                big = true;
                                bresult = iresult;
                            }
                        }
                    }

                    if (big)
                    {
                        bresult /= 360;
                        Console.WriteLine(bresult.ToString());
                    }
                    else
                    {
                        iresult /= 360;
                        Console.WriteLine(iresult.ToString());
                    }
                    //Console.ReadKey();
                }
                catch (Exception)
                {
                    break;
                }
            }
        }
    }
}
