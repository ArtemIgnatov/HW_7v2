using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HW_7v2
{
    internal class Repository
    {
        /// <summary>
        /// Массив, в котором храним данные
        /// </summary>
        public Worker[] workers;

        public Worker workerById;

        /// <summary>
        /// Путь к файлу из которого загружаем данные
        /// </summary>
        private string path;

        /// <summary>
        /// Индекс для обращения к элементам массива workers
        /// </summary>
        int index;

        /// <summary>
        /// Загаловки таблицы
        /// </summary>
        string[] titles = {"ID", "Время записи", "ФИО", "Возраст", "Рост", "Дата рождения", "место рождения"};

        /// <summary>
        /// Конструктор позволяющий присвоить путь файлу
        /// </summary>
        /// <param name="Path"></param>  public Repository(string Path)
        public Repository(string Path)
        {
            this.path = Path;
            this.index = 0;
            this.titles = new string[7];
            this.workers = new Worker[2];
        }

        /// <summary>
        /// Метод добавляющий место под новые запист
        /// </summary>
        /// <param name="Flag"></param>
        private void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.workers, this.workers.Length * 2);
            }
        }

        /// <summary>
        /// Метод для чтения данных из файла и выгрузки их в массив workers 
        /// </summary>
        /// <returns></returns>
        public Worker[] GetAllWorkers()
        {
            using (StreamReader sr = new StreamReader(this.path, Encoding.Unicode))
            {
                while (!sr.EndOfStream)
                {
                    this.Resize(index >= this.workers.Length);
                    /// Проверяем размеры массива, чтобы поместить все записис из файла, 
                    /// в случае чегоувеличиваем размер массива workers с помощью
                    /// метода Resize.

                    string[] data = sr.ReadLine().Split('#');
                    workers[index].Id = Convert.ToInt32(data[0]);
                    workers[index].RecTime = Convert.ToDateTime(data[1]);
                    workers[index].FIO = data[2];
                    workers[index].Age = Convert.ToInt32(data[3]);
                    workers[index].Height = Convert.ToInt32(data[4]);
                    workers[index].Bday = Convert.ToDateTime(data[5]);
                    workers[index].POB = data[6];
                    index++;
                }
                return workers;

            }

        }

        /// <summary>
        /// Метод для возвращения работника по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Worker GetWorkerById(int id)
        {
            //var selectWorker = from worker in this.workers where worker.Id == id select worker;
            //workerById = selectWorker;
            //return  workerById; Способ, который не получился
           
            GetAllWorkers(); ///Читаем файл в массив

            for (int i = 0; i < workers.Length; i++)
            {
                if (workers[i].Id == id)
                {
                    workerById = workers[i];
                    break;
                }
                else if (i == workers.Length - 1)
                {
                    Console.WriteLine("Отсутствует запись о работнке с указанным Id");
                }
            }
            return workerById;  
        }

        /// <summary>
        /// Метод для удаления работника по его Id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteWorker(int id)
        {
            GetAllWorkers();///Выгружаем всех работников
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Unicode))
            {
                for (int i = 0; i < workers.Length; i++)
                {
                    if (workers[i].Id != id)///проверяем id на совпадение
                    {
                        string note = string.Empty;
                        note += $"{workers[i].Id}#";
                        note += $"{workers[i].RecTime}#";
                        note += $"{workers[i].FIO}#";
                        note += $"{workers[i].Age}#";
                        note += $"{workers[i].Height}#";
                        note += $"{workers[i].Bday}#";
                        note += $"{workers[i].POB}#";
                        sw.WriteLine(note);
                    }
                }
            }

        }

        /// <summary>
        /// Метод для записи ноаого работника в файл
        /// </summary>
        /// <param name="worker"></param>
        public void AddWorker()
        {
            GetAllWorkers(); ///Выгружаем массив из файла, чтобы узнать Id последнего работника
            int newWorkerId = workers[workers.Length - 1].Id +1;
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Unicode))
                ///Дописываем нового работника в файл
            {
                char key = 'д';
                do
                {
                    string note = string.Empty;

                    note += $"{newWorkerId}#";

                    string now = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                    Console.Write($"Время заметки {now}");
                    note += $"{now}#";

                    Console.Write("\nВведите ФИО: ");
                    note += $"{Console.ReadLine()}#";

                    Console.Write("Введите возраст: ");
                    note += $"{Console.ReadLine()}#";

                    Console.Write("Введите рост: ");
                    note += $"{Console.ReadLine()}#";

                    Console.Write("Введите дату рождения: ");
                    note += $"{Console.ReadLine()}#";

                    Console.Write("Введите место рождения: ");
                    note += $"{Console.ReadLine()}#";

                    sw.WriteLine(note);
                    newWorkerId++;
                    Console.WriteLine("Продолжить? н/д");
                    key = Console.ReadKey(true).KeyChar;
                } while (char.ToLower(key) == 'д');
            }
        }

        /// <summary>
        /// Загрузка записей в выбраном диапазоне дат
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            GetAllWorkers();
            workers.OrderBy(w => w.Equals(dateFrom)).ThenBy(w => w.Equals(dateTo)).ToArray();
            return workers.ToArray();
            // здесь происходит чтение из файла
            // фильтрация нужных записей
            // и возврат массива считанных экземпляров
        }

        /// <summary>
        /// Метод печати в консоль
        /// </summary>
        public void PrintDbToConsole()
        {
            Console.WriteLine($"{this.titles[0],15} {this.titles[1],15} {this.titles[2],15} {this.titles[3],15} {this.titles[4],15}" +
                $"{this.titles[5],15} {this.titles[6],15}");
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(this.workers[i].Print());

            }
        }

        /// <summary>
        /// Метод позволяющий увидеть общее число сотрудников
        /// </summary>
        public int Count { get { return this.index; } }
    }
    
}