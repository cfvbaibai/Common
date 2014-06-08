using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfvbaibai.CommonUtils
{
    /// A more powerful console API
    public static class PowerConsole
    {
        /// <summary>
        /// Write message with specified console color.
        /// </summary>
        /// <param name="foreColor">Console foreground color.</param>
        /// <param name="bgColor">Console background color.</param>
        /// <param name="message">The message to write.</param>
        public static void WriteWithColor(ConsoleColor foreColor, ConsoleColor bgColor, string message)
        {
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = bgColor;
            try
            {
                Console.WriteLine(message);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        public static void WriteWithColor(ConsoleColor foreColor, string message)
        {
            WriteWithColor(foreColor, Console.BackgroundColor, message);
        }

        public static void WriteWithColorF(ConsoleColor foreColor, ConsoleColor bgColor, string format, params object[] args)
        {
            WriteWithColor(foreColor, bgColor, string.Format(format, args));
        }

        public static void Error(string message)
        {
            WriteWithColor(ConsoleColor.Red, message);
        }

        public static void ErrorF(string format, params object[] args)
        {
            Error(string.Format(format, args));
        }

        public static void Warning(string message)
        {
            PowerConsole.WriteWithColor(ConsoleColor.Yellow, message);
        }

        public static void WarningF(string format, params object[] args)
        {
            Warning(string.Format(format, args));
        }

        public static void Info(string message)
        {
            WriteWithColor(ConsoleColor.White, message);
        }

        public static void InfoF(string format, params object[] args)
        {
            Info(string.Format(format, args));
        }

        public static void Debug(string message)
        {
            WriteWithColor(ConsoleColor.Gray, message);
        }

        public static void DebugF(string format, params object[] args)
        {
            Debug(string.Format(format, args));
        }

        public static void Success(string message)
        {
            WriteWithColor(ConsoleColor.Green, message);
        }

        public static void SuccessF(string format, params object[] args)
        {
            Success(string.Format(format, args));
        }

        public static void Step(string message)
        {
            WriteWithColor(ConsoleColor.Cyan, message);
        }

        public static void StepF(string format, params object[] args)
        {
            Step(string.Format(format, args));
        }
    }
}
