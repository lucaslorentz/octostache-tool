using McMaster.Extensions.CommandLineUtils;

namespace OctostacheTool
{
    [Command("octostache", Description = "Octostache Tools")]
    [HelpOption(Inherited = true)]
    [Subcommand(typeof(SubstituteCommand))]
    class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return 1;
        }
    }
}
