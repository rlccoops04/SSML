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
        public static string mainFormula; // основная формула(не изменяется)
        public static string formulaForCalculating; // формула которую мы постепенно упрощаем для получения результата
        public static int[] valuex;
        public static int[] valuey;
        public static int[] results;
        public static int resultscount;
        public static int[] Mnozhestvo;
        public static int countnumbers = 0;
        public static List listPussyBigBlack; // список для скобок вместе с их индексами
        public static int count1 = 0; // число (
        public static int count2 = 0; // число )
        public static int countbkt = 0; // общее число ) (
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
            Console.WriteLine($"| X | Y | {mainFormula} |");
            for(int i = 0; i < k; i++)
            {
                for(int j = 0; j < k; j++)
                {
                    Console.Write($"| {valuex[i]} | {valuey[j]} | {String.Concat(Enumerable.Repeat(" ", mainFormula.Length/2))}{results[resultscount]}{String.Concat(Enumerable.Repeat(" ", mainFormula.Length / 2 - 1))} |");
                    resultscount += 1;
                    Console.WriteLine();
                }
            }
        }
        public static void PrintOne(int[] valuex, int[] results) // вывод для одной переменной
        {
            Console.WriteLine("\nТаблица:\n");
            resultscount = 0;
            Console.WriteLine($"| X | {mainFormula} |");
            for (int i = 0; i < k; i++)
            {
                Console.Write($"| {valuex[i]} | {String.Concat(Enumerable.Repeat(" ", mainFormula.Length / 2))}{results[resultscount]}{String.Concat(Enumerable.Repeat(" ", mainFormula.Length / 2 - 1))} |");
                resultscount += k;
                Console.WriteLine();
            }
        }
        public static void countBkt(string Formula) // подсчет скобок
        {
            listPussyBigBlack = new List();
            listPussyBigBlack.pNext = null;
            List current = listPussyBigBlack;
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
        public static int FindValue(int x, int y, string formula,int k) // нахождение x=>y
        {
            string x_to_string = x.ToString();
            string y_to_string = y.ToString();
            if (formula.Contains("x"))
            {
                formula = formula.Replace('x', Convert.ToChar(x_to_string));
            }
            if (formula.Contains("y"))
            {
                formula = formula.Replace('y', Convert.ToChar(y_to_string));
            }
            if (formula[0] - '0' < formula[3] - '0')
            {
                return k - 1;
            }
            else
            {
                return (k - 1 - (formula[0] - '0') + (formula[3] - '0')) % k;
            }
        } 
        public static int FindValueJ(int x, string formula, int k)
        {
            string x_to_string = x.ToString();
            if (formula.Contains("x"))
            {
                formula = formula.Replace('x', Convert.ToChar(x_to_string));
            }
            return ((formula[1] + 1) - '0') % k;
            /*            if(formula[3] == '1')
                        {
                            return k - 1;
                        }
                        else
                        {
                            return 0;
                        }*/
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
        {;
            Console.WriteLine("Работу выполнил студент группы 4211 Зигангиров Булат Мисбахович\n"
                             + "\ng = 11, n = 6 -> (g+n-1)mod6 + 1 = 5; s = 1; (g+n-1)mod7 + 1 = 3; (g+n-1)mod3 + 1 = 2\n"
                             + "\nФункция одного аргумента: J1(x)\nФункция двух аргументов: x=>y\nСтандартная форма представления функции: вторая форма\n"
                             + "\nВвод функций: \nJ1(x) = J_1(x)\nx=>y = x=>y\n\n");
            bool mainmenu = true;
            int choicemenu;
            int countsave;
            int countnumbers;
            while (mainmenu)
            {
                Console.Write("Введите число k: ");
                k = Input();
                Console.Write("Введите число n: ");
                n = Input();
                string str;
                string currentOperation;
                bool menu = true;
                valuex = new int[k];
                valuey = new int[k];
                resultscount = 0;
                results = new int[k * k];
                for (int i = 0; i < k; i++)
                {
                    valuex[i] = i;
                    valuey[i] = i;
                }
                Console.Write("Введите основную формулу в соответствии с заданным вводом функции: ");
                mainFormula = Console.ReadLine();
                if (!(mainFormula.Contains("x") || mainFormula.Contains("y") || mainFormula.Contains("=>") || mainFormula.Contains("J")))
                {
                    Console.WriteLine("Ошибка ввода.");
                    Console.Write("Введите основную формулу в соответствии с заданным вводом функции: ");
                    mainFormula = Console.ReadLine();
                }
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < k; j++)
                    {
                        int x = valuex[i];
                        int y = valuey[j];
                        int result = -1;
                        formulaForCalculating = mainFormula;
                        formulaForCalculating = formulaForCalculating.Insert(mainFormula.Length, ")"); // преобразуем формулу для корректной работы вычисления действий
                        formulaForCalculating = formulaForCalculating.Insert(0, "(");
                        /*countBkt(FormulaForCalculating);*/
                        while (formulaForCalculating.Length != 1) // начинаем вычисление для x = i, y = j
                        {
                            countBkt(formulaForCalculating); // всегда обновляем скобки
                            List current = listPussyBigBlack;
                            while (current.pNext.key != ')')
                            {
                                current = current.pNext;
                            }
                            currentOperation = "";
                            for (int t = current.index + 1; t < current.pNext.index; t++)
                            {
                                currentOperation += formulaForCalculating[t]; // определяем текущее действие(по индексам скобок)
                            }
                            if (currentOperation.Contains("(") || currentOperation.Contains(")"))
                            {
                                Console.WriteLine("Ошибка ввода.");
                                return;
                            }
                            if (currentOperation.Length == 1) // если действие состоит из одного элемента, определяем значение
                            {
                                if (currentOperation == "x")
                                {
                                    result = x;
                                }
                                else if (currentOperation == "y")
                                {
                                    result = y;
                                }
                            }
                            else if (currentOperation.Contains("_")) // если действие J, вызываем соотв. функцию
                            {
/*                                if (currentOperation.Length != 4)
                                {
                                    formulaForCalculating = formulaForCalculating.Remove(current.index + 1, currentOperation.Length);
                                    currentOperation = currentOperation.Insert(currentOperation.IndexOf('J') + 4, ")");
                                    currentOperation = currentOperation.Insert(currentOperation.IndexOf('J'), "(");
                                    formulaForCalculating = formulaForCalculating.Insert(current.index + 1, currentOperation);
                                    continue;
                                }*/
                                result = FindValueJ(x, currentOperation, k);
                            }
                            else // иначе вызываем =>
                            {
                                if (currentOperation.Length != 4)
                                {
                                    formulaForCalculating = formulaForCalculating.Insert(current.index + 5, ")");
                                    formulaForCalculating = formulaForCalculating.Insert(current.index + 1, "(");
                                    continue;
                                }
                                result = FindValue(x, y, currentOperation, k);

                            }
                            formulaForCalculating = formulaForCalculating.Remove(current.index, current.pNext.index - current.index + 1); // удаляем выполненное действие
                            formulaForCalculating = formulaForCalculating.Insert(current.index, result.ToString()); // вставляем полученное в действии значение
                            pop(listPussyBigBlack, current.index); // удаляем скобочки
                            pop(listPussyBigBlack, current.pNext.index);
                            countbkt -= 2;
                        }
                        results[resultscount] = result; // заносим в массив результатов
                        resultscount += 1;
                        listPussyBigBlack = null;
                    }
                }
                while(menu)
                {
                    Console.WriteLine("1) Таблица истинности\n2) Вторая форма(аналог полинома Жегалкина)\n3) Проверка на сохранения множества функцией\n4) Повторить ввод\n5) Выход\nВвод: ");
                    choicemenu = int.Parse(Console.ReadLine());
                    switch (choicemenu)
                    {
                        case 1:
                            if (n == 2)
                            {
                                Print(valuex, valuey, results);
                            }
                            else
                            {
                                PrintOne(valuex, results);
                            }
                            break;
                        case 2:
                            if (n == 2)
                            {
                                AnalogPolynomZhegalkina(valuex, valuey, results);
                            }
                            else
                            {
                                AnalogPolynomOne(valuex, results);
                            }
                            break;
                        case 3:
                            Console.WriteLine($"\nВведите множество для проверки сохранения этого множества функцией: ");
                            str = Console.ReadLine();
                            countnumbers = 0;
                            Mnozhestvo = new int[str.Length];
                            for (int i = 0; i < str.Length; i++)
                            {
                                if (char.IsDigit(str[i]))
                                {
                                    Mnozhestvo[countnumbers] = str[i] - '0';
                                    countnumbers += 1;
                                }
                            }
                            resultscount = 0;
                            countsave = 0;
                            Console.WriteLine($"Строки, сохраняющие множество:\n| X | Y | f(x,y)");
                            if (n == 2)
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
                                int curr;
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
                            if (countsave == resultscount)
                            {
                                Console.WriteLine($"Функция сохраняет множество {{{str}}}");
                            }
                            else
                            {
                                Console.WriteLine($"Функция не сохраняет множество {{{str}}}");
                            }
                            break;
                        case 4:
                            menu = false;
                            break;
                        case 5:
                            menu = false;
                            mainmenu = false;
                            break;
                        default:
                            Console.WriteLine("Неверный ввод.\n");
                            break;
                    }
                }
            }
        }
    }
}
