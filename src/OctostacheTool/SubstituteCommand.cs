using McMaster.Extensions.CommandLineUtils;
using Octostache;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace OctostacheTool
{
    [Command(Description = "Substitute variables in file")]
    class SubstituteCommand
    {
        [Required]
        [Argument(0, "Input", "Input file. Use - for stdin")]
        public string Input { get; set; }

        [Argument(1, "Output", "Output file. Use - for stdout. Default to input file")]
        public string Output { get; set; }

        private async Task<int> OnExecute()
        {
            var octoVariables = new VariableDictionary();

            var envVariables = Environment.GetEnvironmentVariables();
            foreach (var key in envVariables.Keys)
            {
                octoVariables.Add(key.ToString(), envVariables[key]?.ToString());
            }

            var content = await ReadInput(Input);

            var replaced = octoVariables.Evaluate(content, out var error);

            await WriteOutput(Output ?? Input, replaced);

            if (!string.IsNullOrEmpty(error))
            {
                Console.Error.WriteLine(error);
                return 1;
            }

            return 0;
        }

        private static async Task<string> ReadInput(string input)
        {
            switch (input)
            {
                case "-":
                    return await Console.In.ReadToEndAsync();
                default:
                    return await File.ReadAllTextAsync(input);
            }
        }

        private static async Task WriteOutput(string output, string content)
        {
            switch (output)
            {
                case "-":
                    await Console.Out.WriteAsync(content);
                    break;
                default:
                    await File.WriteAllTextAsync(output, content);
                    break;
            }
        }
    }
}
