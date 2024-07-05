using System;
using System.IO;

namespace Task_1
{
    class Manager : Worker
    {
        /// <summary>
        /// Приветствие
        /// </summary>
        public override void Greeting()
        {
            Console.Clear();
            Console.WriteLine("Здравствуйте менеджер, вам доступны все функции.");
        }
    }
}
