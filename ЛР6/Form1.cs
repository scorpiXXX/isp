using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ЛР6
{
    public partial class Form1 : Form
    {
        public People gamer;
        public Bank bank;
        public DateTime gameDate;
        public int month = 1;
        public int bookPrice = 15;//Стартовая стоимость книги
        public Form1()
        {
            InitializeComponent();
            gamer = new People();
            bank = new Bank();

            //Инициализация стартовых параметров
            gameDate = new DateTime(2000, 1, 1);
            NameL.Text = gamer.Name;
            SurnameL.Text = gamer.SurName;
            gamer.Hunger = 50;
            gamer.Fatigue = 50;
            BankName.Text = bank.Name;
            BankSurname.Text = bank.SurName;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //Блок обновления информации
            AgeL.Text = gamer.age.ToString();
            BalanceL.Text = gamer.CurrentSum.ToString() + " руб.";
            HungerPB.Value = gamer.Hunger;
            FatiguePB.Value = gamer.Fatigue;
            EducationL.Text = gamer.education;
            ExperienceL.Text = gamer.experience.ToString();
            CareerL.Text = gamer.career;
            SalaryL.Text = gamer.salary.ToString();
            //gamer.Hunger -= 1;
            //gamer.Fatigue -= 1;
            BankBalanceL.Text = bank._sum.ToString() + " руб.";
            CreditL.Text = bank.credit.ToString() + " руб.";
            if (gamer.college)
            {
                CollegePB.Value += 1;
                if(CollegePB.Value == 1095)
                {
                    CollegeDateL.Text += "/" + DateL.Text;
                    gamer.experience += 1200;
                    gamer.college = false;
                    CollegePrice.Text = "Получено!";
                    gamer.education = "Среднее специальное";
                    MessageBox.Show("Вы окончили колледж!","Внимание!",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            if (gamer.university)
            {
                UniversityPB.Value += 1;
                if (UniversityPB.Value == 1095)
                {
                    UniversityDateL.Text += "/" + DateL.Text;
                    gamer.experience += 3600;
                    gamer.university = false;
                    UniversityPrice.Text = "Получено!";
                    gamer.education = "Высшее";
                    MessageBox.Show("Вы окончили ВУЗ!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (gamer.secondUniversity)
            {
                SecontdUniversityPB.Value += 1;
                if (SecontdUniversityPB.Value == 730)
                {
                    SecontdUniversityDateL.Text += "/" + DateL.Text;
                    gamer.experience += 5600;
                    gamer.secondUniversity = false;
                    SecondUniversityPrice.Text = "Получено!";
                    gamer.education = "Второе Высшее";
                    MessageBox.Show("Вы окончили ВУЗ и получили Второе Высшее образование!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (gamer.oopCurses)
            {
                OOPPB.Value += 1;
                if (OOPPB.Value == 90)
                {
                    OOPDate.Text += "/" + DateL.Text;
                    gamer.experience += 250;
                    gamer.oopCurses = false;
                    OOPPrice.Text = "Изучено!";
                    MessageBox.Show("Вы окончили курсы основ ООП!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (gamer.companyCurses)
            {
                CompanyPB.Value += 1;
                if (CompanyPB.Value == 90)
                {
                    CompanyDate.Text += "/" + DateL.Text;
                    gamer.experience += 500;
                    gamer.companyCurses = false;
                    CompanyPrice.Text = "Изучено!";
                    MessageBox.Show("Вы окончили курсы управления компанией", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (gamer.readBook)
            {
                BookPB.Value += 1;
                if (BookPB.Value == 34)
                {
                    gamer.experience += 50;
                    gamer.readBook = false;
                    bookPrice = Convert.ToInt32((bookPrice * 1.45));
                    BookPrice.Text = "Стоимость: " + bookPrice + " руб.";
                    gamer.countBook--;
                    BookPB.Value = 0;
                    MessageBox.Show("Вы прочли книгу!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            ////Блок предупреждений
            if (gamer.Hunger == 15)
            {
                GameTimer.Enabled = false;
                DialogResult dr = MessageBox.Show("Вы голодны!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (dr == DialogResult.OK) GameTimer.Enabled = true;
            }
            if (gamer.Fatigue == 15)
            {
                GameTimer.Enabled = false;
                DialogResult dr = MessageBox.Show("Вы устали!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (dr == DialogResult.OK) GameTimer.Enabled = true;
            }

            ////Блок проверки на конец игры
            //if (gamer.Hunger == 0)
            //{
            //    GameTimer.Enabled = false;
            //    MessageBox.Show("Вы умерли с голоду!","Конец игры!",MessageBoxButtons.OK,MessageBoxIcon.Error);                
            //}
            //if (gamer.Fatigue == 0)
            //{
            //    GameTimer.Enabled = false;
            //    MessageBox.Show("Вы умерли от усталости!", "Конец игры!", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            //}

            //Блок даты
            gameDate = gameDate.AddDays(1);
            DateL.Text = gameDate.Date.ToShortDateString();
            if(month != gameDate.Month)
            {
                gamer.Put(gamer.salary);
                if (bank.countPayment > 0) {
                    if (bank.CurrentSum >= bank.monthlyPayment)
                    {
                        bank.Withdraw(bank.monthlyPayment);
                        bank.countPayment--;
                    }
                    else if((bank.CurrentSum + gamer.CurrentSum) >= bank.monthlyPayment)
                    {
                        int payment = bank.monthlyPayment;
                        payment -= bank.CurrentSum;
                        bank.Withdraw(bank.CurrentSum);
                        gamer.Withdraw(payment);
                        bank.countPayment--;
                    }
                    else
                    {
                        GameTimer.Enabled = false;
                        MessageBox.Show("Вы обонкротились!\rИгра окончена","Внимание!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    if (bank.countPayment == 0)
                    {
                        bank.monthlyPayment = 0;
                        bank.credit = 0;
                        TakeBTN.Enabled = true;
                        MontlyPayment.Text = "0 руб.";
                    }
                }
                bank.Put(Convert.ToInt32(bank._sum * 0.05));
                month = gameDate.Month;
            }
        }

        private void McDonaldsBTN_Click(object sender, EventArgs e)
        {
            if (gamer.Withdraw(50)) //Цена еды
            { 
                if ((gamer.Hunger + 10) <= 100)
                {
                    gamer.Hunger += 10;
                }
                else
                {
                    gamer.Hunger += (100 - gamer.Hunger);
                }
            }
        }

        private void CanteenBTN_Click(object sender, EventArgs e)
        {
            if (gamer.Withdraw(120))    //Цена еды
            { 
                if ((gamer.Hunger + 25) <= 100)
                {
                    gamer.Hunger += 25;
                }
                else
                {
                    gamer.Hunger += (100 - gamer.Hunger);
                }
            }
        }

        private void RestaurantBTN_Click(object sender, EventArgs e)
        {
            if (gamer.Withdraw(210))    //Цена еды
            { 
                if ((gamer.Hunger + 60) <= 100)
                {
                    gamer.Hunger += 60;
                }
                else
                {
                    gamer.Hunger += (100 - gamer.Hunger);
                }
            }
        }

        private void SleepBTN_Click(object sender, EventArgs e)
        {
            if (gamer.Withdraw(20)) //Цена отдыха
            {                 
                if ((gamer.Fatigue + 10) <= 100)
                {
                    gamer.Fatigue += 10;
                }
                else
                {
                    gamer.Fatigue += (100 - gamer.Fatigue);
                }
            }
        }

        private void SanatoriumBTN_Click(object sender, EventArgs e)
        {
            if (gamer.Withdraw(120))    //Цена отдыха
            { 
                if ((gamer.Fatigue + 40) <= 100)
                {
                    gamer.Fatigue += 40;
                }
                else
                {
                    gamer.Fatigue += (100 - gamer.Fatigue);
                }
            }
        }

        private void SeaBTN_Click(object sender, EventArgs e)
        {
            if (gamer.Withdraw(270))    //Цена отдыха
            { 
                if ((gamer.Fatigue + 70) <= 100)
                {
                    gamer.Fatigue += 70;
                }
                else
                {
                    gamer.Fatigue += (100 - gamer.Fatigue);
                }
            }
        }

        private void CollegeBTN_Click(object sender, EventArgs e)
        {
            if (gamer.Withdraw(1400))
            {
                gamer.college = true;
                CollegeBTN.Enabled = false;
                CollegeDateL.Text = DateL.Text;
                CollegeEnterDateL.Visible = true;
                CollegeDateL.Visible = true;
                gamer.countBook += 3;
            }
        }

        private void UniversityBTN_Click(object sender, EventArgs e)
        {
            if (gamer.Withdraw(3200))
            {
                gamer.university = true;
                UniversityBTN.Enabled = false;
                UniversityDateL.Text = DateL.Text;
                UniversityEnterDateL.Visible = true;
                UniversityDateL.Visible = true;
                gamer.countBook += 3;
            }
        }

        private void SecontdUniversityBTN_Click(object sender, EventArgs e)
        {
            if (gamer.Withdraw(5600))
            {
                gamer.secondUniversity = true;
                SecontdUniversityBTN.Enabled = false;
                SecontdUniversityDateL.Text = DateL.Text;
                SecondUniversityEnterDateL.Visible = true;
                SecontdUniversityDateL.Visible = true;
                gamer.countBook += 3;
            }
        }

        private void StickerBTN_Click(object sender, EventArgs e)
        {
            gamer.salary = 100;
            StickerBTN.Enabled = false;
        }

        private void SysAdminBTN_Click(object sender, EventArgs e)
        {
            if(gamer.experience >= 150)
            {
                gamer.salary = 220;
                SysAdminBTN.Enabled = false;
            }
            else
            {
                MessageBox.Show("У вас недостаточно опыта для этой работы!\rЧитайте книги, посетите курсы или поступите в учебное заведение,\rчто бы увеличить свой опыт","Ошибка!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void JuniorBTN_Click(object sender, EventArgs e)
        {
            if (gamer.experience >= 1900)
            {
                gamer.salary = 350;
                JuniorBTN.Enabled = false;
            }
            else
            {
                MessageBox.Show("У вас недостаточно опыта для этой работы!\rЧитайте книги, посетите курсы или поступите в учебное заведение,\rчто бы увеличить свой опыт", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MiddleBTN_Click(object sender, EventArgs e)
        {
            if (gamer.experience >= 5500)
            {
                gamer.salary = 600;
                MiddleBTN.Enabled = false;
            }
            else
            {
                MessageBox.Show("У вас недостаточно опыта для этой работы!\rЧитайте книги, посетите курсы или поступите в учебное заведение,\rчто бы увеличить свой опыт", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SeniorBTN_Click(object sender, EventArgs e)
        {
            if (gamer.experience >= 5650)
            {
                gamer.salary = 950;
                SeniorBTN.Enabled = false;
            }
            else
            {
                MessageBox.Show("У вас недостаточно опыта для этой работы!\rЧитайте книги, посетите курсы или поступите в учебное заведение,\rчто бы увеличить свой опыт", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DirectorBTN_Click(object sender, EventArgs e)
        {
            if (gamer.experience >= 12050)
            {
                gamer.salary = 1500;
                DirectorBTN.Enabled = false;
            }
            else
            {
                MessageBox.Show("У вас недостаточно опыта для этой работы!\rЧитайте книги, посетите курсы или поступите в учебное заведение,\rчто бы увеличить свой опыт", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void WithdrawBTN_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (int.TryParse(WithdrawTB.Text, out count))
            {
                if (bank.Withdraw(count))
                {
                    gamer.Put(count);
                    MessageBox.Show("Денежные средства успешно переведены!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    WithdrawTB.Text = "";
                }
            }
        }

        private void PutBTN_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (int.TryParse(PutTB.Text, out count))
            {
                if (gamer.Withdraw(count))
                {
                    bank.Put(count);
                    MessageBox.Show("Денежные средства успешно переведены!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PutTB.Text = "";
                }
            }
        }

        private void TakeBTN_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (int.TryParse(TakeTB.Text, out count))
            {
                if (count<=5000)
                {
                    bank.Put(count);
                    bank.credit = Convert.ToInt32(count*1.2);
                    bank.countPayment = 12;
                    bank.monthlyPayment = Convert.ToInt32(Convert.ToInt32(count * 1.2) / 12);
                    MessageBox.Show("Кредит одобрен!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TakeTB.Text = "";
                    TakeBTN.Enabled = false;
                }
            }
        }

        private void TakeTB_TextChanged(object sender, EventArgs e)
        {
            int count = 0;
            if (int.TryParse(TakeTB.Text, out count))
            {
                if (count <= 5000)
                {
                    MontlyPayment.Text = Convert.ToInt32(Convert.ToInt32(count * 1.2) / 12).ToString();
                }
            }
        }

        private void OOPBTN_Click(object sender, EventArgs e)
        {
            if (gamer.experience >= 150)
            {
                gamer.oopCurses = true;
                OOPBTN.Enabled = false;
                OOPDate.Text = DateL.Text;
                OOPEnter.Visible = true;
                OOPDate.Visible = true;
                gamer.countBook += 3;
            }
        }

        private void CompanyBTN_Click(object sender, EventArgs e)
        {
            if (gamer.experience >= 11400)
            {
                gamer.companyCurses = true;
                CompanyBTN.Enabled = false;
                CompanyDate.Text = DateL.Text;
                CompanyEnter.Visible = true;
                CompanyDate.Visible = true;
                gamer.countBook += 3;
            }
        }

        private void BookBTN_Click(object sender, EventArgs e)
        {
            if(gamer.countBook != 0)
            {
                if (gamer.Withdraw(bookPrice))
                {
                    gamer.readBook = true;
                    BookBTN.Enabled = true;
                }
            }
        }
    }
}