using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Collections;
using System.Reflection;

namespace ConsoleApplication1
{
    abstract class Worker
    {
        public Worker()
        {

        }
        public int ID { get; set; }
        public double Salary { get; set; }
        public string Name { get; set; }
        public abstract void CalculatesSalary();
    }
    class FixedPayment : Worker
    {
        double FixedMiddleSalary;
        public FixedPayment(double FixedMiddleSalary, string Name,int ID)
        {
            this.ID = ID;
            this.FixedMiddleSalary = FixedMiddleSalary;
            this.Name = Name;
        }
        public override void CalculatesSalary()
        {
            Salary = FixedMiddleSalary;
        }
    }
    class HourlyPayment : Worker
    {
        double HourlyPay;
        public HourlyPayment(double HourlyPay, string Name,int ID)
        {
            this.ID = ID;
            this.HourlyPay = HourlyPay;
            this.Name = Name;
        }
        public override void CalculatesSalary()
        {
            Salary = 20.8 * 8 * HourlyPay;
        }
    }
    class Management
    {
        private IEnumerable<Worker> Workers;
        public Management(IEnumerable<Worker> IEnum)
        {
            Workers = IEnum;
        }
        /// <summary>
        /// If "true" - 0...5
        /// </summary>
        /// <param name="Direction"></param>
        /// <returns></returns>
        public IEnumerable<Worker> Sorted(bool Direction)
        {
            return Workers.OrderByDescending(t => t.Salary).ThenBy(s => s.Name);
        }
        /// <summary>
        /// Return five first elements
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Worker> FirstFive()
        {
            return Sorted(true).Take(5);
        }
        /// <summary>
        /// Return three last elements
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Worker> LastThree()
        {
            return Sorted(false).Take(3);
        }
        IEnumerable<string> ConvertWorkersToStrins()
        {
            IEnumerable<string> col = new List<string>();
            foreach (var t in Workers)
                col.ToList().Add(t.GetType() + "/t" + t.ID + "/t" + t.Name + "/t" + t.Salary);
            return col;
        }
        void ConvertStringToWorkers(string[] array)
        {
            Workers=new List<Worker>();
            //foreach(var t in array)
            //    Workers.ToList().Add(new Worker())
        }
        /// <summary>
        /// Only *.txt
        /// </summary>
        /// <param name="FileSource"></param>
        public void WriteToFile(string FileSource)
        {
            if (File.Exists(FileSource))
            {
                FileInfo file = new FileInfo(FileSource);
                if (file.Extension != "txt")
                    Console.WriteLine("Wrong extension!");
                else
                {
                    File.WriteAllLines(FileSource, ConvertWorkersToStrins());
                    Console.WriteLine("Write to file done!");
                }
            }
        }
        /// <summary>
        /// Only *.txt
        /// </summary>
        /// <param name="FileSource"></param>
        public void ReadFromFile(string FileSource)
        {
            if (File.Exists(FileSource))
            {
                FileInfo file = new FileInfo(FileSource);
                if (file.Extension != "txt")
                    Console.WriteLine("Wrong extension!");
                else
                {
                    File.ReadAllLines(FileSource);
                    Console.WriteLine("Read from file done!");
                }
            }
        }
        public void Show()
        {
            foreach (var t in Workers)
                Console.WriteLine(t.ID + "/t" + t.Name + "/t" + t.Salary);
        }
        public void ShowOnlyID()
        {
            foreach (var t in Workers)
                Console.WriteLine(t.ID);
        }
        public Worker GetWorker(int ID)
        {
            return Workers.ElementAt(ID);
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Management management = new Management(new Worker[] 
            { 
                new HourlyPayment(6000,"One",1),
                new HourlyPayment(270,"Two",02),
                new HourlyPayment(3320,"Three",03),
                new HourlyPayment(8730,"Four",04),
                new HourlyPayment(50,"Five",05),
                new HourlyPayment(04040,"Six",06),
                new HourlyPayment(100,"Seven",07),
                new FixedPayment(10000,"Eight",8),
                new FixedPayment(730000,"Nine",09),
                new FixedPayment(7100,"Ten",10),
                new FixedPayment(4200,"Eleven",11),
                new FixedPayment(10000,"Twelve",12)
            });
            management.Sorted(true);
            //Worker a = new HourlyPayment();
            //a.CalculatesSalary();

            Console.WriteLine(management.GetWorker(4).GetType().DeclaringMethod);
            Console.ReadKey();
        }
    }
}







//public static double Calculate(string str)
//{
//    int nNearAction = str.IndexOfAny(new char[] { '+', '-', '*', '/' });
//    if (nNearAction != -1)
//    {
//        double number = Convert.ToDouble(str.Substring(0, nNearAction));
//        str = str.Remove(0, nNearAction);
//        while (str.IndexOfAny(new char[] { '+', '-', '*', '/' }) != -1)
//        {
//            nNearAction = str.IndexOfAny(new char[] { '+', '-', '*', '/' }, 1) != -1 ? str.IndexOfAny(new char[] { '+', '-', '*', '/' }, 1) : str.Length;
//            double number2 = Convert.ToDouble(str.Substring(1, nNearAction - 1));
//            switch (str[0])
//            {
//                case '+':
//                    number += number2;
//                    break;
//                case '-':
//                    number -= number2;
//                    break;
//                case '*':
//                    number *= number2;
//                    break;
//                case '/':
//                    number /= number2;
//                    break;
//                default:
//                    break;
//            }
//            str = str.Remove(0, nNearAction);
//        }
//        return number;
//    }
//    return str != "" ? Convert.ToInt32(str) : -1;
//}