using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_7v2
{
    struct Worker
    {   
        /// <summary>
        /// Id сотрудника
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Время записи
        /// </summary>
        public DateTime RecTime { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string FIO { get; set; }
        
        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        public int Age { get; set; }
        
        /// <summary>
        /// Рост сотрудника
        /// </summary>
        public int Height { get; set; }
        
        /// <summary>
        /// Дата рождения сотрудника
        /// </summary>
        public DateTime Bday { get; set; }
       
        /// <summary>
        /// Место рождения
        /// </summary>
        public string POB { get; set; }

        /// <summary>
        /// Метод вывода на печать
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            return $"{Id,-5} {RecTime,-20} {FIO,-40} " +
               $"{Age,-10} {Height,-10} {Bday,-25} {POB,-15}";
        }
    }
}
