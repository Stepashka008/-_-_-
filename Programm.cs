using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая_2._0
{
    class Programm
    {
        private List<Bumaga> bumagas = new List<Bumaga>();
        private double sum;
        private double Asum = 0, Osum = 0;
        public List<Bumaga> B { get {  return bumagas; } }
        public double main(double sum, List<double> percents) // Главный метод
        {
            this.sum = sum;
            Init(percents); // Инициализация бумаг
            Vklad(); // Условие по вкладу
            if (VkladBigAkAndObl()) // Процент по вкладу больше чем по акциям и облигациям
            {
                VkladBig(); // Оставляем 8 рублей на акции и облигации
            }
            AkObl(); // Условие, что вложения в акции равны облигациям
            AkC(); // Инвестируем 25% влаженных в акции в акцию C
            InvestingInBigAkzAndBigOblig(); // Инвестируем в большие акции и облигации
            return Zelevaiya(); // Расчет целевой функции
        }
        public double dopInvesting(double sum) // Расчёт доп инвестиций
        {
            this.sum = sum;
            AkObl(); // Распределяем деньги поровну в акции и облигации
            AkC(); // Инвестируем 25% влаженных в акции в акцию C
            InvestingInBigAkzAndBigOblig(); // Инвестируем в большие акции и облигации
            return Zelevaiya(); // Расчет целевой функции
        }
        private void Init(List<double> percents) // Инициализация бумаг
        {
            string[] names = { "AA", "AB", "AC", "OA", "OB", "V" };
            for (int i = 0; i < percents.Count; i++)
            {
                bumagas.Add(new Bumaga(names[i], 0, percents[i]));
            }
        }
        private void Vklad() // Условие по вкладу
        {
            foreach (var bumaga in bumagas)
            {
                if (bumaga.NAME == "V")
                {
                    bumaga.SUM += 10000;
                    sum -= 10000;
                    break;
                }
            }
        }
        private void AkObl() // Условие, что вложения в акции равны облигациям
        {
            Asum = sum / 2;
            Osum = sum / 2;
            sum = 0;
        }
        private bool VkladBigAkAndObl() // А процент по вкладу не больше акций и облигаций
        {
            double prA = 0, prO = 0, prV = 0;
            foreach (var bumaga in bumagas)
            {
                if (bumaga.NAME == "V")
                {
                    if (prV < bumaga.PERCENT)
                    {
                        prV = bumaga.PERCENT;
                    }
                }
                else if (bumaga.NAME[0] == 'A')
                {
                    if (prA < bumaga.PERCENT)
                    {
                        prA = bumaga.PERCENT;
                    }
                }
                else
                {
                    if (prO < bumaga.PERCENT)
                    {
                        prO = bumaga.PERCENT;
                    }
                }
            }
            return prV > prA && prV > prO;
        }
        private void AkC() // Инвестируем 25% влаженных в акции в акцию C
        {
            foreach (var bumaga in bumagas)
            {
                if (bumaga.NAME == "AC")
                {
                    if (bumaga.SUM < 12500)
                    {
                        double okonchat = bumaga.SUM + (Asum * 0.25);
                        if (okonchat < 12500)
                        {
                            bumaga.SUM += Asum * 0.25;
                            Asum -= Asum * 0.25; ;
                            break;
                        }
                        else
                        {
                            double zakonch = 12500 - bumaga.SUM;
                            bumaga.SUM += zakonch;
                            Asum -= zakonch;
                            break;
                        }
                    }
                }
            }
        }
        private void InvestingInBigAkzAndBigOblig() // Инвестирование в самые круппные акции и облигации
        {
            Bumaga Akz = null, Obl = null;
            foreach (var bumaga in bumagas)
            {
                if (bumaga.NAME[0] == 'A')
                {
                    if (Akz == null) Akz = bumaga;
                    else
                    {
                        if (Akz.PERCENT < bumaga.PERCENT) Akz = bumaga;
                    }
                }
                else if (bumaga.NAME[0] == 'O')
                {
                    if (Obl == null) Obl = bumaga;
                    else
                    {
                        if (Obl.PERCENT < bumaga.PERCENT) Obl = bumaga;
                    }
                }
            }
            Akz.SUM += Asum; Asum = 0;
            Obl.SUM += Osum; Osum = 0;
        }
        private double Zelevaiya()
        {
            double finalSum = 0;
            foreach (var bumaga in bumagas)
            {
                finalSum += bumaga.SUM * bumaga.PERCENT;
            }
            return finalSum;
        }
        private void VkladBig()
        {
            foreach (var bumaga in bumagas)
            {
                if (bumaga.NAME == "V")
                {
                    bumaga.SUM += sum - 8;
                    sum = 8;
                    break;
                }
            }
        }

    }
}
