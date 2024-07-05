using System;
using System.Reflection;

namespace Task_1
{
    class Consultant : Worker
    {
        /// <summary>
        /// Приветствие
        /// </summary>
        public override void Greeting()
        {
            Console.Clear();
            Console.WriteLine("Здравствуйте консультант, некоторые функции недоступны");
        }

        /// <summary>
        /// Информация о клиенте
        /// </summary>
        /// <param name="customer">Клиент</param>
        public override void PrintCustomers(Customers customer, CustomerChanges[] customerChanges)
        {
            Customers newCustomer = new Customers(customer);

            newCustomer.Passport = Censorship(customer.Passport);

            base.PrintCustomers(newCustomer, customerChanges);
        }

        /// <summary>
        /// Добавление клиента
        /// </summary>
        /// <returns></returns>
        public override Customers NewCustomer()
        {
            return null;
        }

        /// <summary>
        /// Изменение данных клиента
        /// </summary>
        /// <param name="customer"> Клиент </param>
        /// <param name="index"> Изменяемое поле </param>
        public override void EditCustomer(Customers customer, byte index)
        {
            if (index == 4)
            {
                base.EditCustomer(customer, index);
            }
            else
            {
                Console.WriteLine("Недостаточно прав");
            }
        }

        /// <summary>
        /// Цензура строки
        /// </summary>
        /// <param name="original">Изначальное слово</param>
        /// <returns></returns>
        private string Censorship(string original)
        {
            char[] originalCharArr = original.ToCharArray();

            for (int i = 0; i < originalCharArr.Length; i++)
            {
                originalCharArr[i] = '*';
            }

            string censorship = new string(originalCharArr);

            return censorship;
        }
    }
}