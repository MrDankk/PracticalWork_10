using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal class CustomerChanges
    {
        public int ID { get; set; }
        public string EditTime { get; set; }
        public string DataChanges { get; set; }
        public string TypeChanges { get; set; }
        public string WorkerPost { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id"> Индекс клиента </param>
        /// <param name="editTime"> Время изменений </param>
        /// <param name="dataChanges"> Изменяемое поле </param>
        /// <param name="typeChanges"> Изменения </param>
        /// <param name="workerPost"> Кто вносил изменения </param>
        public CustomerChanges(int id, string editTime, string dataChanges, string typeChanges, string workerPost) 
        { 
            this.ID = id;
            this.EditTime = editTime;
            this.DataChanges = dataChanges;
            this.TypeChanges = typeChanges;
            this.WorkerPost = workerPost;
        }

        public CustomerChanges(CustomerChanges customerChanges) 
        { 
            this.ID = customerChanges.ID;
            this.EditTime = customerChanges.EditTime;
            this.DataChanges = customerChanges.DataChanges;
            this.TypeChanges = customerChanges.TypeChanges;
            this.WorkerPost = customerChanges.WorkerPost;
        }
    }
}
