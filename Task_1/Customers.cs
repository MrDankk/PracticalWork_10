using System;

namespace Task_1
{
    class Customers
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Passport { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ID"> Индекс клиента </param>
        /// <param name="firstName"> Имя </param>
        /// <param name="lastName"> Фамилия </param>
        /// <param name="middleName"> Отчество </param>
        /// <param name="phoneNumber"> Номер телефона </param>
        /// <param name="passport"> Номер паспорта </param>
        public Customers(int ID, string lastName, string firstName, string middleName, string phoneNumber, string passport) 
        { 
            this.ID = ID;

            this.LastName = lastName;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.PhoneNumber = phoneNumber;
            this.Passport = passport;
        }

        public Customers(Customers customer) 
        { 
            this.ID = customer.ID;
            this.LastName = customer.LastName;
            this.FirstName = customer.FirstName;
            this.MiddleName = customer.MiddleName;
            this.PhoneNumber = customer.PhoneNumber;
            this.Passport = customer.Passport;
        }
    }
}
