using System;

namespace Task_1
{
    internal class Program
    {
        static void Main()
        {
            Repository repository = new Repository();

            repository.CustomersPath = "Customers.txt";
            repository.ChangesPath = "CustomersEdit.txt";

            byte postIndex = ChoosingPost();
            Worker worker =  AppointmentPost(postIndex);

            MenuSelection(repository, worker, postIndex);
        }

        /// <summary>
        /// Выбор должности сотрудника
        /// </summary>
        /// <returns></returns>
        static byte ChoosingPost()
        {
            Console.WriteLine("Выберете должность" +
                              "\n1.Консультант" +
                              "\n2.Менеджер");
            
            while(true)
            {
                byte index = InputByte();

                switch (index)
                {
                    case 1:
                        return index;
                    case 2:
                        return index;
                    default:
                        Console.WriteLine("Произошла ошибка попробуйте еще раз");
                        break;
                }
            }
        }

        /// <summary>
        /// Назначение должности сотрудника
        /// </summary>
        /// <param name="index">Индекс должности</param>
        /// <returns></returns>
        static Worker AppointmentPost(byte index)
        {
            switch (index)
            {
                case 1:
                    return new Consultant();
                default:
                    return new Manager();
            }
        }

        /// <summary>
        /// Управление меню выбора
        /// </summary>
        /// <param name="repository">Репозиторий</param>
        /// <param name="worker">Работник</param>
        /// <param name="postIndex">Индекс должности</param>
        static void MenuSelection(Repository repository,Worker worker, byte postIndex)
        {
            Console.WriteLine("Добро пожаловать");
            worker.Greeting();
            Console.ReadKey();

            bool menuIsActive = true;
            while(menuIsActive)
            {
                Console.Clear();
                MenuText(postIndex);
                byte input = InputByte();

                switch (input)
                {
                    case 1:
                        PrintCustomers(repository, worker);
                        Console.ReadKey();
                        break;
                    case 2:
                        EditCustomer(repository, worker);
                        Console.ReadKey();
                        break;
                    case 3:
                        if (postIndex == 2)
                        {
                            Customers customer = worker.NewCustomer();

                            if (customer != null)
                            {
                                repository.AddCustomer(customer);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно прав");
                        }
                        Console.ReadKey();
                        break;
                    case 0:
                        menuIsActive = false;
                        Console.WriteLine("До свидания");
                        break;
                    default:
                        Console.WriteLine("Ошибка");
                        break;
                }
            }
        }

        /// <summary>
        /// Показать всех клиентов
        /// </summary>
        /// /// <param name="repository">Репозиторий</param>
        /// <param name="worker">Работник</param>
        static void PrintCustomers(Repository repository, Worker worker)
        {
            Console.Clear();

            Customers[] customers = repository.CreateCustomersArray();
            CustomerChanges[] customerChanges = repository.CreateChangesArray();

            for (int i = 0; i < customers.Length; i++)
            {
                worker.PrintCustomers(customers[i],customerChanges);

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Изменение данных клиента
        /// </summary>
        /// <param name="repository">Репозиторий</param>
        /// <param name="worker">Работник</param>
        static void EditCustomer(Repository repository, Worker worker)
        {
            Console.Clear();

            Console.WriteLine("Введите номер телефона клиента" +
                              "\nдля выхода введите 0");

            string input = InputString();

            Customers customer = repository.GetWorkerByPhoneNumber(input);

            if(customer != null)
            {
                Console.WriteLine("Что вы хотите изменить?\n1.Имя\n2.Фамилия\n3.Отчество\n4.Номер телефона\n5.Номер паспорта");

                byte inputIndex = InputByte();

                string typeChange = TypeChange(inputIndex,customer);
                string dataChange = DataChange(inputIndex,customer);
                string workerPost = WorkerPost(worker);

                worker.EditCustomer(customer, inputIndex);

                typeChange = string.Join(" ", typeChange, " на ", TypeChange(inputIndex,customer));

                repository.ApplyingChange(customer);
                repository.SaveDataChanges(customer.ID.ToString(),DateTime.Now.ToString(), dataChange, typeChange, workerPost);
            }
            else
            {
                Console.WriteLine("Такого клиента нет");
            }
        }

        /// <summary>
        /// Изменения
        /// </summary>
        /// <param name="inputIndex"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        static string TypeChange(byte inputIndex, Customers customer)
        {
            string typeChanges = string.Empty;

            switch (inputIndex)
            {
                case 1:
                    typeChanges = customer.FirstName.ToString();
                    break;
                case 2:
                    typeChanges = customer.LastName.ToString();
                    break;
                case 3:
                    typeChanges = customer.MiddleName.ToString();
                    break;
                case 4:
                    typeChanges = customer.PhoneNumber.ToString();
                    break;
                case 5:
                    typeChanges = customer.Passport.ToString();
                    break;
            }

            return typeChanges;
        }

        /// <summary>
        /// Что было изменено
        /// </summary>
        /// <param name="inputIndex"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        static string DataChange(byte inputIndex, Customers customer)
        {
            string dataChange = string.Empty;

            switch (inputIndex)
            {
                case 1:
                    dataChange = "Имя";
                    break;
                case 2:
                    dataChange = "Фамилию";
                    break;
                case 3:
                    dataChange = "Отчество";
                    break;
                case 4:
                    dataChange = "Номер телефона";
                    break;
                case 5:
                    dataChange = "Номер паспорта";
                    break;
            }

            return dataChange;
        }

        /// <summary>
        /// Кто вносил изменения
        /// </summary>
        /// <param name="worker"></param>
        /// <returns></returns>
        static string WorkerPost(Worker worker)
        {
            string workerPost = string.Empty;

            if (worker.GetType() == typeof(Manager))
            {
                workerPost = "Менеджер";
            }
            else if (worker.GetType() == typeof(Consultant))
            {
                workerPost = "Консультант";
            }

            return workerPost;
        }

        /// <summary>
        /// Проверка ввода числа в byte
        /// </summary>
        /// <returns></returns>
        static byte InputByte()
        {
            while (true)
            {
                byte result;
                string input = Console.ReadLine();

                if(Byte.TryParse(input, out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Попробуйте еще раз\n");
                }
            }
        }

        /// <summary>
        /// Проверка ввода строки
        /// </summary>
        /// <returns></returns>
        static string InputString()
        {
            while (true)
            {
                string input = Console.ReadLine().Trim();

                if (String.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Поле ввода не может быть пустым\n");
                }
                else 
                {
                    return input;
                }
            }
        }

        /// <summary>
        /// Отображение меню выбора
        /// </summary>
        /// <param name="postIndex"></param>
        static void MenuText(byte postIndex)
        {
            Console.WriteLine("1.Посмотреть клиентов" +
                              "\n2.Изменить данные клиента" +
                              "\n3.Добавить нового клиента");
            Console.WriteLine("0.Выход");
        }
    }
}
