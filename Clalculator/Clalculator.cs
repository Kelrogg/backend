using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clalculator
{
    internal class Clalculator
    {
        private enum Operation
        {
            ADDITION = '+',
            SUBTRACTION = '-',
            MULTIPLICATION = '*',
            DIVISION = '/'
        }

        private struct Params
        {
            public double leftOperand;
            public double rightOperand;
            public Operation operation;
        }

        private static Params ParseParams()
        {
            double leftOperand = GetNumber("first operand: ");
            double rightOperand = GetNumber("second operand: ");
            Operation operation = GetOperation("operation: ");

            return new Params { leftOperand = leftOperand, rightOperand = rightOperand, operation = operation };
        }

        static int Main(string[] args)
        {
            try
            {
                Params myParams = ParseParams();
                WriteResult(myParams, Eval(myParams));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error: " + e.Message);
                return 1;
            }

            return 0;
        }

        private static double GetNumber(string? message = null)
        {
            if (message != null)
            {
                Console.Write(message);
            }

            if (!double.TryParse(Console.ReadLine(), out double number))
            {
                throw new InvalidCastException("cannot read number");
            }

            if (double.IsPositiveInfinity(number) || double.IsNegativeInfinity(number))
            {
                throw new OverflowException("number is too big");
            }

            return number;
        }

        private static Operation GetOperation(string? message = null)
        {
            if (message != null)
            {
                Console.Write(message);
            }

            string operation = null;

            if ((operation = Console.ReadLine()) == null)
            {
                throw new InvalidCastException("cannot read operation");
            }

            return operation switch
            {
                "+" => Operation.ADDITION,
                "-" => Operation.SUBTRACTION,
                "/" => Operation.DIVISION,
                "*" => Operation.MULTIPLICATION,
                _ => throw new ArgumentException("wrong operation")
            };
        }

        private static double Eval(Params myParams)
        {

            Operation operation = myParams.operation;
            double leftOperand = myParams.leftOperand;
            double rightOperand = myParams.rightOperand;

            if (operation == Operation.DIVISION && rightOperand.Equals(0))
            {
                throw new DivideByZeroException("zero division");
            }

            double res = operation switch
            {
                Operation.ADDITION => leftOperand + rightOperand,
                Operation.SUBTRACTION => leftOperand - rightOperand,
                Operation.MULTIPLICATION => leftOperand * rightOperand,
                Operation.DIVISION => leftOperand / rightOperand,
                _ => throw new NotImplementedException("operation doesn`t exist"),
            };

            if (double.IsPositiveInfinity(res) || double.IsNegativeInfinity(res))
            {
                throw new OverflowException("number is too big");
            }

            return res;
        }

        private static void WriteResult(Params myParams, double result)
        {
            Console.WriteLine($"{myParams.leftOperand:0.00} {(char)myParams.operation} {myParams.rightOperand:0.00} = {result:0.00}");
        }
    }
}
