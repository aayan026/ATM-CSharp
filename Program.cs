using bank;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Linq;
using static System.Console;

Random random = new Random();
int cvc1 = random.Next(100, 999);
int cvc2 = random.Next(100, 999);
int cvc3 = random.Next(100, 999);
int cvc4 = random.Next(100, 999);
int cvc5 = random.Next(100, 999);
int cvc6 = random.Next(100, 999);


Bankcard card1 = new Bankcard("BirBank", "Ayan Aliyeva", "4169738812399", "6789", cvc1.ToString(), "(05/29)", 2730.10);
Bankcard card2 = new Bankcard("BirBank", "Nermin Mursudova", "4169584489763525", "3829", cvc2.ToString(), "(09/12)", 500.89);
Bankcard card3 = new Bankcard("BirBank", "Omer Aliyev", "4169584498432367", "7895", cvc3.ToString(), "(02/23)", 349.93);
Bankcard card4 = new Bankcard("BirBank", "Burhan Orucov", "4169738898125434", "2524", cvc4.ToString(), "(04/01)", 834.43);
Bankcard card5 = new Bankcard("BirBank", "Benovse Rehimova", "4169584423763862", "8924", cvc5.ToString(), "(03/29)", 4034.93);
Bankcard card6 = new Bankcard("BirBank", "Nigar Xelilova", "4169738823489674", "4576", cvc6.ToString(), "(04/19)", 300.99);


Guid id1 = Guid.NewGuid();
Guid id2 = Guid.NewGuid();
Guid id3 = Guid.NewGuid();
Guid id4 = Guid.NewGuid();
Guid id5 = Guid.NewGuid();
Guid id6 = Guid.NewGuid();


Client client1 = new Client(id1, "Ayan", "Aliyeva", 16, 5000, card1);
Client client2 = new Client(id2, "Nermin", "Mursudova", 23, 5000, card2);
Client client3 = new Client(id3, "Omer", "Aliyev", 14, 3000, card3);
Client client4 = new Client(id4, "Burhan", "Orucov", 15, 5000, card4);
Client client5 = new Client(id5, "Benovse", "Rehimova", 19, 10000, card5);
Client client6 = new Client(id6, "Nigar", "Xelilova", 19, 4000, card6);





Operation[] operations = { };
Client[] clients = { client1, client3, client2, client4, client5, client6 };
Bank bank = new Bank(clients, operations);

while (true)
{
    WriteLine("\n | Salam! Zehmet olmasa kartinizin PIN kodu daxil edin: ");
    string pinCode = ReadLine();
    Client index = bank.SearchPin(pinCode);
    if (index != null)
    {
        string[] choice = { " | 1. Balans ", " | 2.Nagd pul ", " | 3.Emeliyyatlarin siyahisi ", " | 4.Kartdan Karta kocurme", " | 5.Cixis" };
        int secimIndex = 0;

        while (true)
        {
            Clear();
            WriteLine($"\n ~ {index.Name} {index.Surname} xos gelmisiniz! zehmet olmasa asagidakilardan birini secin: \n");
            for (int i = 0; i < choice.Length; i++)
            {
                if (i == secimIndex)
                {
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteLine($"  {choice[i]}");
                    ResetColor();
                }

                else
                    WriteLine($" {choice[i]}");
            }

            var key = ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
                secimIndex = (secimIndex == 0) ? choice.Length - 1 : secimIndex - 1;
            else if (key == ConsoleKey.DownArrow)
                secimIndex = (secimIndex + 1) % choice.Length;
            else if (key == ConsoleKey.Enter)
            {
                if (secimIndex == 0)
                {
                    WriteLine($" ~ Balansiniz: {index.BankAccount.Balance}");
                    WriteLine($"\n | Ana menyuya qayıtmaq üçün hər hansı düyməyə basın...");
                    ReadKey();
                    continue;
                }
                else if (secimIndex == 1)
                {
                    string[] secimler = { " | 1. 10 AZN ", " | 2. 20 AZN ", " | 3. 50 AZN", " | 4. 100 AZN ", " | 5.Diger.." };
                    int secimindex = 0;

                    while (true)
                    {
                        Clear();
                        WriteLine(" | cixarmag istediyiniz meblegi daxil edin: ");
                        for (int i = 0; i < secimler.Length; i++)
                        {
                            if (i == secimindex)
                            {
                                ForegroundColor = ConsoleColor.Cyan;
                                WriteLine($" {secimler[i]}");
                                ResetColor();

                            }
                            else
                                WriteLine($" {secimler[i]}");
                        }

                        var key2 = ReadKey(true).Key;

                        if (key2 == ConsoleKey.UpArrow)
                            secimindex = (secimindex == 0) ? secimler.Length - 1 : secimindex - 1;
                        else if (key2 == ConsoleKey.DownArrow)
                            secimindex = (secimindex + 1) % secimler.Length;
                        else if (key2 == ConsoleKey.Enter)
                            break;
                    }

                    switch (secimindex)
                    {
                        case 0:
                            try
                            {
                                bank.BalansdanCixma(index, 10); ForegroundColor = ConsoleColor.Green;

                                bank.AddOperation($" ~ Kartinizdan {10} AZN cixildi ");
                                ResetColor();

                            }
                            catch (Exception ex)
                            {
                                WriteLine(ex.Message);
                            }
                            break;
                        case 1:
                            try
                            {
                                bank.BalansdanCixma(index, 20);
                                ForegroundColor = ConsoleColor.Green;

                                bank.AddOperation($" ~ Kartinizdan {20} AZN cixildi ");
                                ResetColor();


                            }
                            catch (Exception ex)
                            {
                                WriteLine(ex.Message);
                            }
                            break;
                        case 2:
                            try
                            {
                                bank.BalansdanCixma(index, 50);
                                ForegroundColor = ConsoleColor.Green;
                                bank.AddOperation($" ~ Kartinizdan {50} AZN cixildi ");
                                ResetColor();


                            }
                            catch (Exception ex)
                            {
                                WriteLine(ex.Message);
                            }
                            break;
                        case 3:

                            try
                            {
                                bank.BalansdanCixma(index, 100);
                                ForegroundColor = ConsoleColor.Green;
                                bank.AddOperation($" ~ Kartinizdan {100} AZN cixildi ");
                                ResetColor();

                            }
                            catch (Exception ex)
                            {
                                WriteLine(ex.Message);
                            }
                            break;
                        default:
                            while (true)
                            {
                                WriteLine(" | cixarmag istediyiniz meblegi daxil edin: ");
                                string input = ReadLine();
                                if (double.TryParse(input, out double money))
                                {
                                    if (money < 1)
                                    {
                                        WriteLine(" ~ Mebleg 1 manatdan az olmamalidir!!");
                                        continue;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            bank.BalansdanCixma(index, money);
                                            ForegroundColor = ConsoleColor.Green;
                                            bank.AddOperation($" ~ Kartinizdan {money} AZN cixildi. Xais edirik pulunuzu goturun. ");
                                            ResetColor();
                                            break;
                                        }
                                        catch (Exception ex)
                                        {
                                            ForegroundColor = ConsoleColor.Red;
                                            WriteLine(ex.Message);
                                            ResetColor();
                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    ForegroundColor = ConsoleColor.Red;
                                    WriteLine(" ~ Yanlis secim! yeniden cehd edin.. ");
                                    ResetColor();

                                    continue;
                                }
                            }
                            break;
                    }

                }
                else if (secimIndex == 2)
                {
                    Clear();
                    bank.ShowOperations();
                    System.Threading.Thread.Sleep(2000);
                    WriteLine($"\n | Ana menyuya qayıtmaq üçün hər hansı düyməyə basın...");
                    ReadKey();
                }
                else if (secimIndex == 3)
                {
                    while (true)
                    {
                        WriteLine("\n ~ Kocurtmek istediyiniz kartin nomresini daxil edin (16 reqemli kod) ");
                        string numbers = ReadLine();
                        Client receiver = bank.SearchPan(numbers);
                        if (receiver != null)
                        {
                            int count = receiver.Name.Length - 1;
                            int count2 = receiver.Surname.Length - 1;
                            string sifreliName = receiver.Name[0].ToString() + new string('*', count);
                            string sifreliSurname = receiver.Surname[0].ToString() + new string('*', count2);
                            WriteLine($"\n | {sifreliName} {sifreliSurname} \n");
                            try
                            {
                                bank.KartdanKarta(index, receiver);
                                bank.AddOperation($" ~ Kartdan karta kocurme ");
                            }
                            catch (Exception ex)
                            {
                                WriteLine(ex.Message);
                                WriteLine($"\n | Ana menyuya qayıtmaq üçün hər hansı düyməyə basın...");
                                ReadKey();

                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            WriteLine($" Daxil etdiyiniz nomre yanlisdir!! yeniden cehd edin..");
                            Console.ResetColor();
                            continue;
                        }

                        break;
                    }

                }
                else
                {
                    break;
                }

            }
            else
            {
                continue;
            }
        }
        break;
    }

    else
    {
        ForegroundColor = ConsoleColor.Red;
        WriteLine(" ~ PIN kod yanlisdir...");
        ResetColor();
    }



}

