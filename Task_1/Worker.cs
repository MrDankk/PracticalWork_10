using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    abstract class Worker : IEditor
    {
        /// <summary>
        /// Приветствие
        /// </summary>
        public abstract void Greeting();

        /// <summary>
        /// Информация о клиенте
        /// </summary>
        /// <param name="customer"></param>
        public virtual void PrintCustomers(Customers customer, CustomerChanges[] customerChanges)
        {
            Console.WriteLine($"Ф.И.О. - {customer.LastName} {customer.FirstName} {customer.MiddleName}\n");
            Console.WriteLine($"Номер телефона - {customer.PhoneNumber}\n");
            Console.WriteLine($"Номер паспорта - {customer.Passport}\n");

            PrintChanges(customerChanges, customer.ID);
        }

        /// <summary>
        /// Информация об изменениях
        /// </summary>
        /// <param name="customerChanges"></param>
        /// <param name="id"></param>
        private void PrintChanges(CustomerChanges[] customerChanges, int id)
        {
            
            for (int i = 0; i < customerChanges.Length; i++)
            {
                if (customerChanges != null && customerChanges[i].ID == id)
                {
                    Console.WriteLine($"{customerChanges[i].EditTime} " +
                                      $"\n{customerChanges[i].WorkerPost} " +
                                      $" изменил {customerChanges[i].DataChanges}" +
                                      $" {customerChanges[i].TypeChanges}");
                }
            }
        }

        /// <summary>
        /// Изменение информации
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="position"></param>
        public virtual void EditCustomer(Customers customer, byte index)
        {
            switch (index)
            {
                case 1:
                    customer.FirstName = Input("Имя");
                    break;
                case 2:
                    customer.LastName = Input("Фамилию");
                    break;
                case 3:
                    customer.MiddleName = Input("Отчество");
                    break;
                case 4:
                    customer.PhoneNumber = Input("Номер телефона");
                    break;
                case 5:
                    customer.Passport = Input("Серию и номер паспорта");
                    break;
                default:
                    Console.WriteLine("Некоректное значение");
                    break;
            }    
        }

        /// <summary>
        /// Добавление клиента
        /// </summary>
        /// <returns></returns>
        public virtual Customers NewCustomer()
        {
            string lastName = Input("Фамилию");
            string firstName = Input("Имя");
            string middleName = Input("Отчество");
            string phoneNumber = Input("Номер телефона");
            string passport = Input("Серию и номер паспорта");

            Customers customer = new Customers(0,firstName, lastName, middleName, phoneNumber, passport);

            return customer;
        }

        /// <summary>
        /// Проверка ввода информации
        /// </summary>
        /// <returns></returns>
        protected string Input(string inputField)
        {
            Console.WriteLine($"Введите {inputField}");
            string input;
            while (true)
            {
                input = Console.ReadLine().Trim();

                if (input != "")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Поле не может быть пустым");
                }
            }

            return input;
        }
    }
}
