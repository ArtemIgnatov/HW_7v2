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

        ///// <summary>
        ///// Загаловки таблицы
        ///// </summary>
        //public string[] titles = {"ID", "Время записи", "ФИО", "Возраст", "Рост", "Дата рождения", "место рождения"};
        
        /// <summary>
        /// Конструктор позволяющий присвоить путь файлу
        /// </summary>
        /// <param name="Path"></param>  public Repository(string Path)
        public Repository(string Path)
        {
            this.path = Path;
            this.index = 0;
            //this.titles = new string[7];
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
        public void GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                Console.WriteLine("Вывод между двумя датами");
                int i = 1;

                while (!sr.EndOfStream)
                {
                    string[] lines = sr.ReadLine().Split('#');
                    if (lines[0] == "") { continue; }
                    if (Convert.ToDateTime(lines[i]) > dateFrom && Convert.ToDateTime(lines[i]) < dateTo)
                    {
                        foreach (var line in lines)
                        {
                            Console.Write($"{line}#");
                        }
                        Console.Write("\n");
                    }
                }
            }
            // здесь происходит чтение из файла
            // фильтрация нужных записей
            // и возврат массива считанных экземпляров
        }

        /// <summary>
        /// Метод печати в консоль
        /// </summary>
        public void PrintDbToConsole()
        {
            Console.WriteLine($"{"ID",-5} {"Время записи",-20} {"ФИО",-40} {"Возраст",-10} {"Рост",-10}" +
                $"{"Дата рождения",-25} {"Место рождения",-15}");
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(this.workers[i].Print());

            }
        }

        /// <summary>
        /// Метод позволяющий увидеть общее число сотрудников
        /// </summary>
        public int Count { get { return this.index; } }

        /// <summary>
        /// Метод для удаления работника по ID
        /// </summary>
        /// <param name="Path"></param>
        public void DelWorker(string Path)
        {
            Console.WriteLine("Введите Id работника для удаления");
            int id = Convert.ToInt32(Console.ReadLine());
            string[] arr = new string[index]; //массив в который будем писать все данные из справочник
            string[] temp = new string[index]; //массив в который перепишем все данные, кроме удаляемого работника
            using(StreamReader sr = new StreamReader(Path))
            {
                //вводим индексы для правильного заполнения после удаления
                int j = 0;
                int i = 0;
                int k = 0;

                while(!sr.EndOfStream)
                {
                    arr = sr.ReadLine().Split('#');

                    if (arr[i] != "" && Convert.ToInt32(arr[i]) != id)
                    {
                        temp[k] = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}",
                            this.workers[j].Id,
                            this.workers[j].RecTime,
                            this.workers[j].FIO,
                            this.workers[j].Age,
                            this.workers[j].Height,
                            this.workers[j].Bday,
                            this.workers[j].POB
                            );
                        j++;
                        k++;              
                    }
                    else
                    {
                        j++;
                        continue;
                    }
                }
                //Load(); хз зачем

            }
            File.WriteAllLines(Path, temp);
        
        }
    }
    
}