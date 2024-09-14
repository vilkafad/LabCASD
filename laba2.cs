using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6 { 
  internal class Program
{
}
public struct Complex
{
    public double x; public double y;
    public Complex(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
    public override string ToString()
    {
        return x.ToString() + " " + y.ToString() + "i";
    }
    public static Complex operator +(Complex a, Complex b)
    {
        return new Complex(a.x + b.x, a.y + b.y);
    }
    public static Complex operator -(Complex a, Complex b)
    {
        return new Complex(a.x - b.x, a.y - b.y);
    }
    public static Complex operator *(Complex a, Complex b) 
     {
            return new Complex(a.x * b.x - b.y * a.y, a.x * b.y + a.y * b.x);
}
public static Complex operator /(Complex a, Complex b)
{
    return new Complex(((a.x * b.x) + (a.y * b.y)) / ((Math.Pow(b.x, 2)) + (Math.Pow(b.y, 2))), ((a.y * b.x) - (a.x * b.y)) / ((Math.Pow(b.x, 2)) + (Math.Pow(b.y, 2))));
}
public double Module()
{
    return Math.Sqrt((Math.Pow(x, 2)) + (Math.Pow(y, 2)));
}
public double Argument()
{
    if (x > 0) return Math.Atan(y / x);
    if ((x < 0) && (y >= 0)) return Math.PI + Math.Atan(y / x);
    if ((x < 0) && (y < 0)) return (-Math.PI + Math.Atan(y / x)); 
    if ((x == 0) && (y > 0)) return (Math.PI / 2);
    if ((x == 0) && (y < 0)) return (-Math.PI / 2); 
    return 0;
}
public double ReturnValid()
{
    return x;
}
public double ReturnImaginary()
{
    return y;
}

static void Main(string[] args)

{     


            Console.Write("Введите действительную часть первого числа: ");
            string valid1 = Console.ReadLine();
            Console.Write("Введите мнимую часть первого числа: ");
            string imaginary1 = Console.ReadLine();

            double x1 = Convert.ToDouble(valid1);
            double y1 = Convert.ToDouble(imaginary1);


            Complex firstNumber = new Complex(x1, y1);

            bool exit = false;
            while (exit == false)
            {
                string valid2 = "";
                string imaginary2 = "";
                double x2 = 0.00;
                double y2 = 0.00;
                Complex secondNumber= new Complex(0, 0);

                Console.WriteLine("Выберете интересующий вариант:");
                Console.WriteLine("1) сложение");
                Console.WriteLine("2) вычитание");
                Console.WriteLine("3) умножение");
                Console.WriteLine("4) деление");
                Console.WriteLine("5) модуль");
                Console.WriteLine("6) аргумента");
                Console.WriteLine("7) вывести действительная часть");
                Console.WriteLine("8) вывести мнимую часть");
                Console.WriteLine("9) вывод комплексного числа");
                Console.WriteLine("Введите q или  Q для выхода");

                string choice = Console.ReadLine();

                switch (choice) {
                    case "1":


                        Console.Write("Введите действительную часть второго числа: ");
                        valid2 = Console.ReadLine();
                        Console.Write("Введите мнимую часть второго числа: ");
                        imaginary2 = Console.ReadLine();


                         x2 = Convert.ToDouble(valid2);
                         y2 = Convert.ToDouble(imaginary2);


                        secondNumber += new Complex(x2, y2);
                        Complex summa = firstNumber + secondNumber;

                        Console.WriteLine("Ответ: ");
                        Console.WriteLine(summa);

                        break;


                    case "2":


                        Console.Write("Введите действительную часть второго числа: ");
                        valid2 = Console.ReadLine();
                        Console.Write("Введите мнимую часть второго числа: ");
                        imaginary2 = Console.ReadLine();


                        x2 = Convert.ToDouble(valid2);
                        y2 = Convert.ToDouble(imaginary2);


                        secondNumber += new Complex(x2, y2);
                        Complex difference = firstNumber - secondNumber;

                        Console.WriteLine("Ответ: ");
                        Console.WriteLine(difference);

                        break;


                    case "3":

                        Console.Write("Введите действительную часть второго числа: ");
                        valid2 = Console.ReadLine();
                        Console.Write("Введите мнимую часть второго числа: ");
                        imaginary2 = Console.ReadLine();


                        x2 = Convert.ToDouble(valid2);
                        y2 = Convert.ToDouble(imaginary2);


                        secondNumber += new Complex(x2, y2);
                        Complex multiplication = firstNumber * secondNumber;

                        Console.WriteLine("Ответ: ");
                        Console.WriteLine(multiplication);

                        break;


                    case "4":

                        Console.Write("Введите действительную часть второго числа: ");
                        valid2 = Console.ReadLine();
                        Console.Write("Введите мнимую часть второго числа: ");
                        imaginary2 = Console.ReadLine();


                        x2 = Convert.ToDouble(valid2);
                        y2 = Convert.ToDouble(imaginary2);


                        secondNumber += new Complex(x2, y2);
                        Complex division = firstNumber / secondNumber;

                        Console.WriteLine("Ответ: ");
                        Console.WriteLine(division);

                        break;


                    case "5":

                        Console.WriteLine("Ответ: ");
                        Console.WriteLine(firstNumber.Module());

                            break;

                    case "6":

                        Console.WriteLine("Ответ: ");
                        Console.WriteLine(firstNumber.Argument());

                        break;


                    case "7":

                        Console.WriteLine("Ответ: ");
                        Console.WriteLine(firstNumber.ReturnValid());

                        break;


                    case "8":

                        Console.WriteLine("Ответ: ");
                        Console.WriteLine(firstNumber.ReturnImaginary());

                        break;

                    case "9":

                        Console.WriteLine("Ответ: ");
                        Console.WriteLine(firstNumber.ToString());

                        break;

                    case "q":
                        exit = true;
                        break;
                    case "Q":
                        exit = true;
                        break;


                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;

                }

            }

        }
   }
}
