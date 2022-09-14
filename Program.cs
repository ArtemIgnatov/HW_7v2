using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_7v2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"artem.csv";

            //Проверяем на наличие файла, в протвном случае создаем файл
            if (File.Exists(path) == false)
            {
                File.Create(path);
            }

            Repository rep = new Repository(path);

            Menu menu = new Menu();

            while (true)
            {
                menu.ShowChoose();

                var action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        rep.GetAllWorkers();
                        rep.PrintDbToConsole();
                        break;
                    case "2":
                        rep.AddWorker();
                        break;
                    case "3":
                        Console.WriteLine("Введите ID удаляемого работника");
                        int delId = Convert.ToInt32(Console.ReadLine());
                        rep.DeleteWorker(delId);
                        break;
                    case "4":
                        Console.WriteLine("Введите ID работника");
                        int getId = Convert.ToInt32(Console.ReadLine());
                        rep.GetWorkerById(getId);
                        break;
                    case "5":
                        Console.WriteLine("Введите первую дату");
                        DateTime date1 = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("Введите вторую дату");
                        DateTime date2 = Convert.ToDateTime(Console.ReadLine());
                        rep.GetWorkersBetweenTwoDates(date1, date2);
                        rep.PrintDbToConsole();
                        break;
                    case "6":
                        goto end; //переходим на метку завершающую программу
                    default:
                        Console.WriteLine("Некорректный ввод действия, введите 1 или 2\n");
                        break;
                }
            }
        end:; //наша метка
        }
    }
}
