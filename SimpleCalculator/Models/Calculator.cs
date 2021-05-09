using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCalculator.Models
{
    static class Calculator
    {
        public static double Calculate(string expression)
        {
            return Calculate(ToSuffixExpression(expression));
        }

        public static double Calculate(double num1, double num2, char oper) => oper switch
        {
            '+' => num2 + num1,
            '-' => num2 - num1,
            '*' => num2 * num1,
            '/' => num2 / num1,
             _  => throw new Exception($"Operator \"{oper}\" is not supported.")
        };

        private static double Calculate(List<string> SuffixExpression)
        {
            Stack<double> stack = new Stack<double>();
            foreach (string item in SuffixExpression)
            {
                try
                {
                    stack.Push(double.Parse(item));
                }
                catch
                {
                    stack.Push(item[0] switch
                    {
                        '(' => throw new Exception("\")\" expected."),
                         _  => Calculate(stack.Pop(), stack.Pop(), item[0])
                    });
                }
            }

            if (stack.Count > 1)
                throw new Exception("Invalid expression.");

            return stack.Pop();
        }

        private static List<string> ToInfixExpression(string expression)
        {
            List<string> ls = new List<string>();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

                if (i == 0 && (c == '-' || c == '+'))
                    ls.Add("0");

                if (char.IsDigit(c) || c == '.')
                {
                    sb.Append(c);
                }
                else if (!char.IsWhiteSpace(c))
                {
                    if (sb.Length != 0)
                    {
                        ls.Add(sb.ToString());
                        sb.Clear();
                    }
                    ls.Add(c.ToString());

                    if (c == '(' && i + 1 < expression.Length && (expression[i + 1] == '-' || expression[i + 1] == '+'))
                        ls.Add("0");
                }
            }

            if (sb.Length != 0)
                ls.Add(sb.ToString());

            return ls;
        }

        private static List<string> ToSuffixExpression(List<string> infixExpression)
        {
            Stack<string> stack = new Stack<string>();
            List<string> suffix = new List<string>();

            foreach (string str in infixExpression)
            {
                try
                {
                    double.Parse(str);
                    suffix.Add(str);
                }
                catch
                {
                    char c = str[0];
                    switch (c)
                    {
                        case '(':
                            stack.Push(str);
                            break;

                        case ')':
                            while (stack.Peek()[0] != '(')
                                suffix.Add(stack.Pop());
                            stack.Pop();
                            break;

                        default:
                            if (stack.Count != 0 && OperatorPriority(c) <= OperatorPriority(stack.Peek()[0]))
                                suffix.Add(stack.Pop());
                            stack.Push(str);
                            break;
                    }
                }
            }

            while (stack.Count != 0)
                suffix.Add(stack.Pop());

            return suffix;
        }

        private static List<string> ToSuffixExpression(string str)
        {
            return ToSuffixExpression(ToInfixExpression(str));
        }

        private static int OperatorPriority(char c) => c switch
        {
            '+' => 1,
            '-' => 1,
            '*' => 2,
            '/' => 2,
             _  => 0
        };

    }
}
