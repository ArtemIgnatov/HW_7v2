using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_7v2
{
    class Menu
    {
        /// <summary>
        /// Метод по выводу на экран стартового окна с выбором действий
        /// </summary>
        public void ShowChoose()
        {
            Console.WriteLine(
                "Выерите вариант введя число:\n" +
                "1 - Вывести данные\n" +
                "2 - Добавить запись\n" +
                "3 - Удалить запись о сотруднике по ID\n"+
                "4 - Вывести данные по ID работника\n"+
                "5 - Вывести записи между двумя датами\n" +
                "6 - выход из програмы");
        }
    }
}
