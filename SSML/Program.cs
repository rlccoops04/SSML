using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SSML
{
    class Program
    {
        public static int n; // кол во значимых переменных
        public static int k; 
        public static string MainFormula; // основная формула( не изменяется)
        public static string FormulaForCalculating; // формула которую мы постепенно упрощаем для получения результата
        public static int[] valuex;
        public static int[] valuey;
        public static int[] results;
        public static int resultscount;
        public static int[] Mnozhestvo;
        public static int countnumbers = 0;
        public static List listbkt; // список для скобочек вместе с их индексами
        public static int count1 = 0; // число (
        public static int count2 = 0; // число )
        public static int countbkt = 0; // общее число )(
        public class List
        {
            public char key;
            public int index;
            public List pNext;
        }
        public static void pop(List list, int index) // для работы со списком (удаление)
        {
            List current = list;
            List previous = current;
            while(current.index != index)
            {
                previous = current;
                current = current.pNext;
            }
            previous.pNext = current.pNext;
        }
        public static int Input() // проверка ввода
        {
            int b;
            error:
            try
            {
                b = int.Parse(Console.ReadLine());
                return b;
            }
            catch
            {
                Console.Write("Неверный ввод!. Введите корректное значение: ");
                goto error;
            }
        }
        public static void Print(int[] valuex, int[] valuey, int[] results) // вывод для 2 перменных
        {
            Console.WriteLine("\nТаблица:\n");
            resultscount = 0;
            Console.WriteLine($"| X | Y | {MainFormula} |");
            for(int i = 0; i < k; i++)
            {
                for(int j = 0; j < k; j++)
                {
                    Console.Write($"| {valuex[i]} | {valuey[j]} | {String.Concat(Enumerable.Repeat(" ", MainFormula.Length/2))}{results[resultscount]}{String.Concat(Enumerable.Repeat(" ", MainFormula.Length / 2 - 1))} |");
                    resultscount += 1;
                    Console.WriteLine();
                }
            }
        }
        public static void PrintOne(int[] valuex, int[] results) // вывод для одной переменной
        {
            Console.WriteLine("\nТаблица:\n");
            resultscount = 0;
            Console.WriteLine($"| X | {MainFormula} |");
            for (int i = 0; i < k; i++)
            {
                Console.Write($"| {valuex[i]} | {String.Concat(Enumerable.Repeat(" ", MainFormula.Length / 2))}{results[resultscount]}{String.Concat(Enumerable.Repeat(" ", MainFormula.Length / 2 - 1))} |");
                resultscount += k;
                Console.WriteLine();
            }
        }
        public static void countBkt(string Formula) // подсчет скобок
        {
            listbkt = new List();
            listbkt.pNext = null;
            List current = listbkt;
            for(int i = 0; i < Formula.Length; i++)
            {
                if(Formula[i] == '(')
                {
                    while(current.pNext != null)
                    {
                        current = current.pNext;
                    }
                    List pTemp = new List();
                    current.pNext = pTemp;
                    pTemp.key = '(';
                    pTemp.pNext = null;
                    pTemp.index = i;
                    count1 += 1;
                    countbkt++;
                }
                else if(Formula[i] == ')')
                {
                    while (current.pNext != null)
                    {
                        current = current.pNext;
                    }
                    List pTemp = new List();
                    current.pNext = pTemp;
                    pTemp.key = ')';
                    pTemp.pNext = null;
                    pTemp.index = i;
                    count2 += 1;
                    countbkt++;
                }
            }
        }
        public static int FindValue(int x, int y, string formula,int k)
        {
            if (formula[0] == 'x' && formula[3] == 'x')
            {
                return k - 1;
            }
            else if (formula[0] == 'x' && formula[3] == 'y')
            {
                if (x < y)
                {
                    return k - 1;
                }
                else
                {
                    return (k - 1 - x + y) % k;
                }
            }
            else if (formula[0] == 'x' && char.IsDigit(formula[3]))
            {
                if(x< formula[3] - '0')
                {
                    return k - 1;
                }
                else
                {
                    return (k - 1 - x + (formula[3] - '0')) % k;
                }
            }
            else if(formula[0] == 'y' && formula[3] == 'x')
            {
                if(y < x)
                {
                    return k - 1;
                }
                else
                {
                    return (k - 1 - y + x) % k;
                }
            }
            else if(formula[0] == 'y' && formula[3] == 'y')
            {
                return k - 1;
            }
            else if(formula[0] == 'y' && char.IsDigit(formula[3]))
            {
                if(y < formula[3] - '0')
                {
                    return k - 1;
                }
                else
                {
                    return (k - 1 - y + (formula[3] - '0')) % k;
                }
            }
            else if(char.IsDigit(formula[0]) && formula[3] == 'x')
            {
                if (formula[0] -'0' < x)
                {
                    return k - 1;
                }
                else
                {
                    return (k - 1 - (formula[0] - '0') + x) % k;
                }
            }
            else if (char.IsDigit(formula[0]) && formula[3] == 'y')
            {
                if (formula[0] - '0' < y)
                {
                    return k - 1;
                }
                else
                {
                    return (k - 1 - (formula[0] - '0') + y) % k;
                }
            }
            else if (char.IsDigit(formula[0]) && char.IsDigit(formula[3]))
            {
                if (formula[0] - '0' < formula[3] - '0')
                {
                    return k - 1;
                }
                else
                {
                    return (k - 1 - (formula[0] - '0') + (formula[3] - '0')) % k;
                }
            }
            return -1;
        } // нахождение x=>y
        public static int FindValueJ(string formula, int k)
        {
            if(formula[3] == '1')
            {
                return k - 1;
            }
            else
            {
                return 0;
            }
        } // J_1(x)
        public static void AnalogPolynomZhegalkina(int[] valuex, int[] valuey, int[] results)
        {
            resultscount = 0;
            int current;
            string form = "";
            Console.WriteLine("\n2я форма (один из аналогов полинома Жегалкина):");
            for(int i = 0; i < k; i++)
            {
                for(int j = 0; j < k; j++)
                {
                    current = results[resultscount];
                    if (current != 0)
                    {
                        if(Math.Abs(current-k) < current)
                        {
                            form += " - ";
                            if (Math.Abs(current-k) == 1)
                            {
                                form += $"j{i}(x)*j{j}(y)";
                            }
                            else
                            {
                                form += $"{Math.Abs(current-k)}*j{i}(x)*j{j}(y)";
                            }
                        }
                        else
                        {
                            form += " + ";
                            if (current == 1)
                            {
                                form += $"j{i}(x)*j{j}(y)";
                            }
                            else
                            {
                                form += $"{current}*j{i}(x)*j{j}(y)";
                            }
                        }
                    }
                    resultscount++;
                }
            }
            Console.WriteLine(form + "\n");
        }
        public static void AnalogPolynomOne(int[] valuex, int[] results)
        {
            resultscount = 0;
            int current;
            string form = "";
            Console.WriteLine("\n2я форма (один из аналогов полинома Жегалкина):");
            for (int i = 0; i < k; i++)
            {
                current = results[resultscount];
                if (current != 0)
                {
                    if (Math.Abs(current - k) < current)
                    {
                        form += " - ";
                        if (Math.Abs(current - k) == 1)
                        {
                            form += $"j{i}(x)";
                        }
                        else
                        {
                            form += $"{Math.Abs(current - k)}*j{i}(x)";
                        }
                    }
                    else
                    {
                        form += " + ";
                        if (current == 1)
                        {
                            form += $"j{i}(x)";
                        }
                        else
                        {
                            form += $"{current}*j{i}(x)";
                        }
                    }
                }
                resultscount++;
            }
            Console.WriteLine(form + "\n");
        }
        static void Main()
        {
            Console.WriteLine("Работу выполнил студент группы 4211 Зигангиров Булат Мисбахович\n"
                             + "\ng = 11, n = 6 -> (g+n-1)mod6 + 1 = 5; s = 1; (g+n-1)mod7 + 1 = 3; (g+n-1)mod3 + 1 = 2\n"
                             + "\nФункция одного аргумента: J1(x)\nФункция двух аргументов: x=>y\nСтандартная форма представления функции: вторая форма\n"
                             + "\nВвод функций: \nJ1(x) = J_1(x)\nx=>y = x=>y\n\n");
            Console.Write("Введите число k: ");
            k = Input();
            Console.Write("Введите число n: ");
            n = Input();
            string str;
            valuex = new int[k];
            valuey = new int[k];
            resultscount = 0;
            results = new int[k*k];
            for (int i = 0; i < k; i++)
            {
                valuex[i] = i;
                valuey[i] = i;
            }
            Console.Write("Введите основную формулу в соответствии с заданным вводом функции: ");
            MainFormula = Console.ReadLine();
            if(!(MainFormula.Contains("x") || MainFormula.Contains("y") || MainFormula.Contains("=>") || MainFormula.Contains("J")))
            {
                Console.WriteLine("Ошибка ввода.");
                return;
            }
            for (int i = 0; i < k; i++)
            {
                for(int j = 0; j < k; j++)
                {
                    int x = valuex[i];
                    int y = valuey[j];
                    int result = -1;
                    FormulaForCalculating = MainFormula;
                    FormulaForCalculating = FormulaForCalculating.Insert(MainFormula.Length, ")"); // преобразуем формулу для корректной работы вычисления действий
                    FormulaForCalculating = FormulaForCalculating.Insert(0, "(");
                    /*countBkt(FormulaForCalculating);*/
                    while (FormulaForCalculating.Length != 1) // начинаем вычисление для x = i, y = j
                    {
                        countBkt(FormulaForCalculating); // всегда обновляем кол-во скобочек
                        List current = listbkt;
                        while (current.pNext.key != ')')
                        {
                            current = current.pNext;
                        }
                        string MicroFormula = "";
                        for (int t = current.index + 1; t < current.pNext.index; t++)
                        {
                            MicroFormula += FormulaForCalculating[t]; // определяем текущее действие(по индексам скобочек)
                        }
                        if(MicroFormula.Contains("(") || MicroFormula.Contains(")"))
                        {
                            Console.WriteLine("Ошибка ввода.");
                            return;
                        }
                        if(MicroFormula.Length == 1) // если действие состоит из одного элемента, определяем значение
                        {
                            if (MicroFormula == "x")
                            {
                                result = x;
                            }
                            else if (MicroFormula == "y")
                            {
                                result = y;
                            }
                        }
                        else if (MicroFormula[0] == 'J') // если действие J, вызываем соотв. функцию
                        {
                            if(MicroFormula.Length != 4)
                            {
                                Console.WriteLine("Ошибка ввода.");
                                return;
                            }
                            result = FindValueJ(MicroFormula, k);
                        }
                        else // иначе вызываем x=>y
                        {
                            if (MicroFormula.Length != 4)
                            {
                                Console.WriteLine("Ошибка ввода.");
                                return;
                            }
                            result = FindValue(x, y, MicroFormula, k);

                        }
                        FormulaForCalculating = FormulaForCalculating.Remove(current.index, current.pNext.index - current.index + 1); // удаляем выполненное действие
                        FormulaForCalculating = FormulaForCalculating.Insert(current.index, result.ToString()); // вставляем полученное в действии значение
                        pop(listbkt, current.index); // удаляем скобочки
                        pop(listbkt, current.pNext.index);
                        countbkt -= 2;
                    }
                    results[resultscount] = result; // заносим в массив результатов
                    resultscount += 1;
                }
            }
            if (n == 2)
            {
                Print(valuex, valuey, results);
                AnalogPolynomZhegalkina(valuex, valuey, results);
            }
            else
            {
                PrintOne(valuex, results);
                AnalogPolynomOne(valuex, results);
            }
            Console.WriteLine($"\nВведите множество для проверки сохранения этого множества функцией: ");
            str = Console.ReadLine();
            int countnumbers = 0;
            Mnozhestvo = new int[str.Length];
            for(int i = 0; i < str.Length; i++)
            {
                if(char.IsDigit(str[i]))
                {
                    Mnozhestvo[countnumbers] = str[i] - '0';
                    countnumbers += 1;
                }
            }
            resultscount = 0;
            int countsave = 0;
            Console.WriteLine($"Строки, сохраняющие множество:\n| X | Y | f(x,y)");
            if(n==2)
            {
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < k; j++)
                    {
                        if (Mnozhestvo.Contains(valuex[i]) && Mnozhestvo.Contains(valuey[j]) && Mnozhestvo.Contains(results[resultscount]))
                        {
                            Console.WriteLine($"| {valuex[i]} | {valuey[j]} | {results[resultscount]}");
                            countsave++;
                        }
                        resultscount += 1;
                    }
                }
            }
            else
            {
                for (int j = 0; j < k; j++)
                {
                    if (Mnozhestvo.Contains(valuex[j]) && Mnozhestvo.Contains(results[resultscount]))
                    {
                        Console.WriteLine($"| {valuex[j]} | {results[resultscount]}");
                        countsave++;
                    }
                    resultscount += k;
                }
            }
            if(countsave == resultscount)
            {
                Console.WriteLine($"Функция сохраняет множество {{{str}}}");
            }
            else
            {
                Console.WriteLine($"Функция не сохраняет множество {{{str}}}");
            }
            Console.ReadKey();
        }
    }
}
