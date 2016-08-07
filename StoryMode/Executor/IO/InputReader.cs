namespace Executor.IO
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Executor.Contracts;

    public class InputReader : IReader
    {
        private const string EndCommand = "quit";

        private IInterpreter interpreter;

        public InputReader(IInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public void StartReadingCommands()
        {
            OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
            string input = Console.ReadLine();
            input = input.Trim();

            while (input != EndCommand)
            {
                this.interpreter.InterpredCommand(input);
                OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
                input = Console.ReadLine();
                input = input.Trim();
            }

            if (SessionData.taskPool.Count != 0)
            {
                Task.WaitAll(SessionData.taskPool.ToArray());
            }
        }
    }
}
