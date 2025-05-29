using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO.Pipes;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    class Bankcard
    {
        public string Bankname { get; set; }
        public string Fullname { get; set; }
        public string Pan { get; set; }
        public string Pin { get; set; }
        public string CVC { get; set; }
        public string ExpireDate { get; set; }
        public double Balance { get; set; }

        public Bankcard(string bankname, string fullname, string pan, string pin, string cVC, string expireDate, double balance)
        {
            Bankname = bankname;
            Fullname = fullname;
            Pan = pan;
            Pin = pin;
            CVC = cVC;
            ExpireDate = expireDate;
            Balance = balance;
        }
    }

    class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public uint Age { get; set; }
        public double Salary { get; set; }
        public Bankcard BankAccount { get; set; }

        public Client(Guid id, string name, string surname, uint age, double salary, Bankcard bankAccount)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            Salary = salary;
            BankAccount = bankAccount;
        }

    }
    class Operation
    {
        public string Description { get; set; }
        public DateTime Time { get; set; }

        public Operation(string description)
        {
            Description = description;
            Time = DateTime.Now;
        }


        public override string ToString() => ($@"
 | Emeliyyat: {Description} 
 | Tarix: {Time}");
    }

    class Bank
    {
        public Client[] clients { get; set; }
        public Operation[] operations { get; set; }
        public Bank(Client[] clients, Operation[] operations)
        {
            this.clients = clients;
            this.operations = operations ?? new Operation[0];
        }


        //functions
        public Client SearchPin(string PIN)
        {
            foreach (var i in clients)
            {
                if (i.BankAccount.Pin == PIN)
                {
                    return i;
                }
            }
            return null;
        }
        public Client SearchPan(string PAN)
        {
            foreach (var i in clients)
            {
                if (i.BankAccount.Pan == PAN)
                {
                    return i;
                }
            }
            return null;
        }

        public double BalansdanCixma(Client index, double price)
        {
            Console.WriteLine($" Sectiyiniz mebleg {price} AZN");
            while (true)
            {
                Console.WriteLine(" ~ Emeliyyati tesdiqleyirsiniz? (beli/xeyr) ");
                string answer = Console.ReadLine().ToLower();

                if (answer == "beli")
                {
                    if (index?.BankAccount.Balance > price)
                    {
                        double newprice = index.BankAccount.Balance -= price;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($" {price} AZN çıxıldı. Yeni balans: {newprice} AZN");
                        Console.ResetColor();
                        Console.WriteLine($"\n | Ana menyuya qayıtmaq üçün hər hansı düyməyə basın...");
                        Console.ReadKey();
                        return newprice;
                    }
                    else
                    {
                        throw new Exception(" ~ Kartinizda kifayet qeder mebleg yoxdur! ");
                    }
                }
                else if (answer == "xeyr")
                {
                    Console.WriteLine(" ~ Emeliyyat levg edildi!! ");
                    Console.WriteLine($"\n | Ana menyuya qayıtmaq üçün hər hansı düyməyə basın...");
                    Console.ReadKey();
                    return index.BankAccount.Balance;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($" ~ Emeliyyat zamani sehv bas verdi. Zehmet olmasa yeniden cehd edin.");
                    Console.ResetColor();
                    continue;
                }
            }
        }

        public void KartdanKarta(Client sender, Client receiver)
        {
            while (true)
            {
                Console.WriteLine(" | Gondermek istediyiniz meblegi daxil edin: ");
                string input = Console.ReadLine();
                if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double mebleg))
                {
                    if (mebleg <=1.0 )
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n ~ Gondereceyiniz mebleg 1 manatdan az olmamalidir!!!");
                        Console.ResetColor();
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($" ~ Kartinizdan {mebleg} AZN cixilacag. Emeliyyati tesdiqleyirsiniz? (beli/xeyr)");
                        string answer = Console.ReadLine().ToLower();

                        if (answer == "beli")
                        {

                            if (sender.BankAccount.Balance >= mebleg)
                            {
                                double newAmount = sender.BankAccount.Balance -= mebleg;
                                receiver.BankAccount.Balance += mebleg;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"\n Emeliyyat ugurla yerine yetirildi!! Qalan balans: {newAmount} AZN");
                                Console.ResetColor();
                                Console.WriteLine($"\n | Ana menyuya qayıtmaq üçün hər hansı düyməyə basın...");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                throw new Exception("\n ~ Kartinizda kifayet qeder mebleg yoxdur! Emeliyyat levg edildi..");
                                
                            }

                        }
                        else if (answer == "xeyr")
                        {
                            Console.WriteLine("\n ~ Emeliyyat levg edildi!! ");
                            Console.WriteLine($"\n | Ana menyuya qayıtmaq üçün hər hansı düyməyə basın...");
                            Console.ReadKey();
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("\n ~ Zehmet olmasa düzgün formatda mebleg daxil edin. ");
                    continue;
                }
            }
        }

        public void AddOperation(string desc)
        {
            int size = operations.Length;
            Operation[] newOperationArray = new Operation[size + 1];
            for (int i = 0; i < operations.Length; i++)
            {
                newOperationArray[i] = operations[i];
            }
            newOperationArray[size] = new Operation(desc);
            operations = newOperationArray;
        }
        public void ShowOperations()
        {
            Console.WriteLine("\n ~ Emeliyyatlariniz ~ ");
            if (operations == null || operations.Length == 0)
            {
                Console.WriteLine("\n  Burada hecne yoxdur..");
            }
            else
            {
                foreach (var item in operations)
                {
                    Console.WriteLine(item);
                }
            }
        }

    }
}
