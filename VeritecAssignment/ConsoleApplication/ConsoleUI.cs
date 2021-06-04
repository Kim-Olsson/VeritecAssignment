using System;

namespace VeritecAssignment.ConsoleApplication
{
    public class ConsoleUI
    {
        public static void LineBreak()
        {
            Console.WriteLine();
        }
        public static void Display(string Text)
        {
            Console.Write(Text);
        }
        public static void DisplayLine(string Text)
        {
            Console.WriteLine(Text);
        }
        public static void DisplayFormDataLine(string PropertyName, string PropertyValue)
        {
            Console.WriteLine(PropertyName + ": " + PropertyValue);
        }
        public static void DisplayFormData(string PropertyName, string PropertyValue)
        {
            Console.Write(PropertyName + ": " + PropertyValue);
        }
        public static void Label(string LabelText)
        {
            Console.Write(LabelText);
        }
        public static void PressAnyKey(string Label)
        {
            ConsoleUI.Label(Label);
            Console.ReadKey();
        }
        public static decimal InputCurrency(string Label, decimal Low, decimal High)
        {
            string input;

            while (true)
            {
                ConsoleUI.Label(Label + " ");
                input = Console.ReadLine();
                if (Validator.IsValidCurrency(input) && Validator.IsBetween(input, Low, High))
                    return (decimal.Parse(input));
            }
        }
        public static string SelectFromList(string Label, string[] Options)
        {
            string input;

            while (true)
            {
                ConsoleUI.Label(Label + " ");
                input = Console.ReadLine();

                if (Validator.IsValidSelectItem(input, Options))
                    return (input);
            }
        }
    }
}