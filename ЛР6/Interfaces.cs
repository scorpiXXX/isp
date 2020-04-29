using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ЛР6
{
        public class People : IAccount, IEssence, IPeople
        {
            int _sum = 100000;   //Баланс игрока
            public int age = 18;  //Возраст игрока
            public int experience = 0; //Опыт игрока
            public string education = "Нет";    //Образование
            public string career = "Нет";
            public int salary = 0;  //Зарплата игрока
            public bool college = false;
            public bool university = false;
            public bool secondUniversity = false;
            public bool oopCurses = false;
            public bool companyCurses = false;
            public bool readBook = false;
            public int countBook = 3;

            public string Name
            {
                get
                {
                    return "Василий";
                }
            }
            public string SurName
            {
                get
                {
                    return "Петрович";
                }
            }

            //Возвращает текущий баланс игрока
            public int CurrentSum
            {
                get
                {
                    return _sum;
                }
            }
            //Статус голода
            public int Hunger { get; set; }

            //Статус усталости
            public int Fatigue { get; set; }
            public void Put(int sum)
            {
                _sum += sum;
            }

            public bool Withdraw(int sum)
            {
                try
                {
                    if (_sum >= sum)
                    {
                        _sum -= sum;
                        return true;
                    }
                    else
                    {
                        throw new Exception("Недостаточно денежных средств!\rВам нехватает: " + (sum - _sum) + " руб.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        public class Bank : IAccount, IEssence
        {
            public int _sum = 0; //Счет игрока в банке
            public int credit = 0; //Кредит игрока в банке
            public int monthlyPayment = 0; //Ежемесячный платеж
            public int countPayment = 0; //Количество платежей
            public string Name
            {
                get
                {
                    return "Банк\"Студент\"";
                }
            }
            public string SurName
            {
                get
                {
                    return "Крупнейший банк";
                }
            }
            public int CurrentSum
            {
                get
                {
                    return _sum;
                }
            }
            public void Put(int sum)
            {
                _sum += sum;
            }

            public bool Withdraw(int sum)
            {
                try
                {
                    if (_sum >= sum)
                    {
                        _sum -= sum;
                        return true;
                    }
                    else
                    {
                        throw new Exception("Недостаточно денежных средств!\rВам нехватает: " + (sum - _sum) + " руб.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        interface IAccount //Интерфейс счета
        {
            int CurrentSum { get; }  //Текущая сумма на счету
            void Put(int sum);      //Положить деньги на счет
            bool Withdraw(int sum); //Взять со счета
        }
        interface IEssence //Интерфейс сущности
        {
            string Name { get; }
            string SurName { get; }
        }
        interface IPeople //Интерфейс человека
        {
            int Hunger { get; set; }
            int Fatigue { get; set; }
        }
    }