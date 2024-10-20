using Task9;

public class ReversePolishNotation
{
    static public int WeightOperator(string opertator)
    {
        switch (opertator)
        {
            case "+":
                return 2;
            case "-":
                return 2;
            case "*":
            case "/":
            case "//":
                return 3;
            case "^":
                return 4;
            case "sqrt":
            case "ln":
            case "cos":
            case "sin":
            case "tg":
            case "ctg":
            case "abs":
            case "log":
            case "min":
            case "max":
            case "mod":
            case "exp":
            case "trunc":
                return 5;
            case "%":
                return 6;
            default: return 0;
        }
    }
    private static double Execute(string op, params double[] x) => op switch
    {
        "+" => x[0] + x[1],
        "-" => x[0] - x[1],
        "*" => x[0] * x[1],
        "/" => x[0] / x[1],
        "^" => Math.Pow(x[0], x[1]),
        "//" => Math.Floor(x[0] / x[1]),
        "exp" => Math.Exp(x[0]),
        "sqrt" => Math.Sqrt(x[0]),
        "abs" => Math.Abs(x[0]),
        "ln" => Math.Log(x[0]),
        "log" => Math.Log10(x[0]),
        "sin" => Math.Sin(x[0]),
        "cos" => Math.Cos(x[0]),
        "tg" => Math.Tan(x[0]),
        "ctg" => 1 / Math.Tan(x[0]),
        "max" => x[0] > x[1] ? x[0] : x[1],
        "min" => x[0] < x[1] ? x[0] : x[1],
        "trunc" => Math.Truncate(x[0]),
        "mod" => (int)x[0] % (int)x[1],
        _ => 0
    };
    private static string GetStringNumber(string expr, ref int pos)
    {
        string output = "";
        for (; pos < expr.Length; pos++)
        {
            if (Char.IsDigit(expr[pos]) || expr[pos] == ',')
            {
                output += expr[pos];
            }
            else
            {
                pos--;
                break;
            }
        }
        return output;
    }
    private static string GetStringText(string expr, ref int pos)
    {
        string output = "";
        for (; pos < expr.Length; pos++)
        {
            if (Char.IsLetter(expr[pos]) || expr[pos] == '/')
            {
                output += expr[pos];
            }
            else
            {
                pos--;
                break;
            }
        }
        return output;
    }
    public static string ToPostfix(string infixExpr)
    {
        string postFix = "";
        MyStack<string> stack = new MyStack<string>();
        for (int i = 0; i < infixExpr.Length; i++)
        {
            //1
            if (Char.IsDigit(infixExpr[i]))
            {
                postFix += GetStringNumber(infixExpr, ref i) + " ";
            }
            else if (Char.IsLetter(infixExpr[i]) || (infixExpr[i] == '/' && infixExpr[i + 1] == '/'))
            {
                //2
                string name = "";
                name += GetStringText(infixExpr, ref i);
                if (WeightOperator(name) != 0)
                {
                    //b
                    while (!stack.Empty() && (WeightOperator(stack.Peek()) >= WeightOperator(name)))
                    {
                        postFix += stack.Peek();
                        stack.Pop();
                    }
                    //a
                    stack.Push(name);
                }
            }
            else if (WeightOperator(Convert.ToString(infixExpr[i])) != 0)
            {
                //b
                while (!stack.Empty() && (WeightOperator(stack.Peek()) >= WeightOperator(Convert.ToString(infixExpr[i]))))
                {
                    postFix += stack.Peek() + " ";
                    stack.Pop();
                }
                //a
                stack.Push(Convert.ToString(infixExpr[i]));
            }
            //3
            else if (infixExpr[i] == '(' && i + 1 != infixExpr.Length && infixExpr[i + 1] == '-')
            {
                if (Char.IsDigit(infixExpr[i + 2]))
                {
                    i += 2;
                    string n = GetStringNumber(infixExpr, ref i);
                    postFix += Convert.ToString('-') + n + " ";
                    i += 1;
                }

            }
            else if (infixExpr[i] == '(') stack.Push(Convert.ToString(infixExpr[i]));

            //4
            else if (infixExpr[i] == ')')
            {
                while (stack.Peek() != "(")
                {
                    postFix += stack.Peek() + " ";
                    stack.Pop();
                }
                stack.Pop();
            }
        }
        while (!stack.Empty())
        {
            postFix += stack.Peek() + " ";
            stack.Pop();
        }
        return postFix;
    }
    public static double Calc(string postFix)
    {
        string name = "";
        MyStack<double> component = new MyStack<double>();
        for (int i = 0; i < postFix.Length; i++)
        {
            name = "";
            if (Char.IsDigit(postFix[i]))
            {
                string number = GetStringNumber(postFix, ref i);
                component.Push(Convert.ToDouble(number));
            }
            else if (Char.IsLetter(postFix[i]) || (postFix[i] == '/' && postFix[i + 1] == '/'))
            {
                name += GetStringText(postFix, ref i);
            }
            else if ((i + 1 < postFix.Length || i + 2 < postFix.Length) && postFix[i] == '-')
            {
                if (Char.IsDigit(postFix[i + 2]))
                {
                    i += 1;
                    string number;
                    number = GetStringNumber(postFix, ref i);
                    double x = Convert.ToDouble(number);
                    component.Push(-x);
                    i += 1;
                }
            }
            else if (WeightOperator(Convert.ToString(postFix[i])) != 0)
            {
                double y = component.Peek();
                component.Pop();
                double x = component.Peek();
                component.Pop();
                component.Push(Execute(Convert.ToString(postFix[i]), x, y));
            }
            if (WeightOperator(name) != 0 && name == "//")
            {
                double y = component.Peek();
                component.Pop();
                double x = component.Peek();
                component.Pop();
                component.Push(Execute(name, x, y));
            }

            if (WeightOperator(name) != 0)
            {

                if (WeightOperator(name) == 5 && (name != "mod" || name != "min" || name != "max"))
                {
                    double y = component.Peek();
                    component.Pop();
                    component.Push(Execute(Convert.ToString(name), y));
                }
                else if (WeightOperator(name) == 5 && (name == "mod" || name == "min" || name == "max"))
                {
                    double y = component.Peek();
                    component.Pop();
                    double x = component.Peek();
                    component.Pop();
                    component.Push(Execute(Convert.ToString(name), x, y));
                }
            }
        }
        return component.Peek();
    }
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Пожалуйста, передайте параметры.");
            return;
        }
        string joinedArgs = string.Join(",", args);
        string g = ToPostfix(joinedArgs);
        Console.WriteLine(g);
        double res = Calc(g);
        Console.WriteLine(res);
    }

}