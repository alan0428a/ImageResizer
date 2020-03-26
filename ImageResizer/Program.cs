using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();

            imageProcess.Clean(destinationPath);
            //double a = 3000;
            //double b = 2000;
            //double rate = (double)1000 / 3000;



            Stopwatch sw = new Stopwatch();
            long refineTime, time;
            double ans = 0, rate;
            int tryCount = 5;

            for (int i = 1; i <= tryCount; i++)
            {
                sw.Reset();
                sw.Start();
                imageProcess.ResizeImagesRefine(sourcePath, destinationPath, 2.0);
                sw.Stop();
                refineTime = sw.ElapsedMilliseconds;
                Console.WriteLine($"{i} - 花費時間[Refine]: {refineTime} ms");

                sw.Reset();
                sw.Start();
                imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
                sw.Stop();
                time = sw.ElapsedMilliseconds;
                Console.WriteLine($"{i} - 花費時間: {time} ms");

                rate = ((double)(time - refineTime) / time);
                Console.WriteLine("{0} - 改善效率: {1:P}:", i, rate);
                ans += rate;
                GC.Collect();
                Console.WriteLine("-------------------------------");
            }

            Console.WriteLine("平均改善效率: {0:P}:", ans / tryCount);
        }
    }
}
