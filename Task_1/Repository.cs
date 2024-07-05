using System;
using System.Dynamic;
using System.IO;

namespace Task_1
{
    class Repository
    {
        private string customersPath;
        private string changesPath;

        /// <summary>
        /// Выбор пути к файлу клиентов
        /// </summary>
        public string CustomersPath
        {
            set { this.customersPath = value; }
            get { return this.customersPath; }
        }

        /// <summary>
        /// Выбор пути к файлу изменений
        /// </summary>
        public string ChangesPath
        {
            set { this.changesPath = value; }
            get { return this.changesPath; }
        }

        /// <summary>
        /// Найти клиента по номеру телефона
        /// </summary>
        /// <param name="phoneNumber"> номер телефона </param>
        /// <returns></returns>
        public Customers GetWorkerByPhoneNumber(string phoneNumber)
        {
            Customers[] customers = CreateCustomersArray();
            Customers customer;

            for (int i = 0; i < customers.Length; i++)
            {
                if (phoneNumber == customers[i].PhoneNumber)
                {
                    customer = customers[i];
                    return customer;
                }
            }

            return null;
            
        }

        /// <summary>
        /// Применение изменений клиента
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="oldId"></param>
        public void ApplyingChange(Customers customer)
        {
            Customers[] customers = CreateCustomersArray();

            File.Delete(customersPath);
            bool workerChange = false;

            for (int i = 0; i < customers.Length; i++)
            {
                if (customers[i].ID != customer.ID || workerChange == true)
                {
                    AddCustomer(customers[i]);
                }
                else
                {
                    AddCustomer(customer);
                    workerChange = true;
                }
            }
        }

        /// <summary>
        /// Сохранение изменений
        /// </summary>
        /// <param name="id"> Индекс клиента </param>
        /// <param name="editTime"> Время изменений </param>
        /// <param name="dataChanges"> Изменяемое поле </param>
        /// <param name="typeChanges"> Изменение </param>
        /// <param name="workerPost"> Кто вносил изменения </param>
        public void SaveDataChanges(string id,string editTime, string dataChanges, string typeChanges, string workerPost)
        {
            string line = string.Join("#",
                                      id,
                                      editTime,
                                      dataChanges,
                                      typeChanges,
                                      workerPost);

            using (StreamWriter streamWriter = new StreamWriter(changesPath, true))
            {
                if (line != "")
                {
                    streamWriter.WriteLine(line);
                }
            }
        }

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="worker"></param>
        public void AddCustomer(Customers customer)
        {
            customer.ID = ArrayLength(customersPath);

            string line = string.Join("#",
                                      customer.ID,
                                      customer.LastName,
                                      customer.FirstName,
                                      customer.MiddleName,
                                      customer.PhoneNumber,
                                      customer.Passport);
            using (StreamWriter streamWriter = new StreamWriter(customersPath, true))
            {
                if (line != "")
                {
                    streamWriter.WriteLine(line);
                }
            }
        }

        /// <summary>
        /// Создание массива клиентов
        /// </summary>
        /// <returns></returns>
        public Customers[] CreateCustomersArray()
        {
            int length = ArrayLength(customersPath);
            Customers[] customer = new Customers[length];

            using (StreamReader streamReader = new StreamReader(customersPath))
            {
                string line;
                int currentIndex = 0;

                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] dataArray = line.Split('#');

                    customer[currentIndex] = new Customers(int.Parse(dataArray[0]), dataArray[1], dataArray[2], dataArray[3], dataArray[4], dataArray[5]);

                    currentIndex++;
                }
            }
            return customer;
        }

        /// <summary>
        /// Создание массива изменений
        /// </summary>
        /// <returns></returns>
        public CustomerChanges[] CreateChangesArray()
        {
            int length = ArrayLength(changesPath);
            CustomerChanges[] customerChanges = new CustomerChanges[length];

            using (StreamReader streamReader = new StreamReader(changesPath))
            {
                string line;
                int currentIndex = 0;

                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] dataArray = line.Split('#');

                    customerChanges[currentIndex] = new CustomerChanges(int.Parse(dataArray[0]), dataArray[1], dataArray[2], dataArray[3], dataArray[4]);

                    currentIndex++;
                }
            }

            return customerChanges;
        }

        /// <summary>
        /// Проверка количества строк в файле и установка длины массива
        /// </summary>
        /// <returns></returns>
        private int ArrayLength(string path)
        {
            FileChecking(path);
            int length = 0;

            using (StreamReader sw = new StreamReader(path))
            {
                string line;
                while ((line = sw.ReadLine()) != null)
                {
                    length++;
                }
            }
            return length;
        }

        /// <summary>
        /// Проверка наличия файла
        /// </summary>
        private void FileChecking(string path)
        {
            if (!File.Exists(path))
            {
                FileStream fileStream = new FileStream(path, FileMode.Create);
                fileStream.Close();
            }
        }
    }
}
