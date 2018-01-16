using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkMaker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (args.Length < 1)
                {
                    throw new Exception("You must supply at least one argument");
                }
                foreach (var arg in args)
                {
                    bool firstDiv = true;
                    int divCount = 0;
                    Console.WriteLine("<!doctype html>");
                    Console.WriteLine("<html lang=\"en\">");
                    Console.WriteLine("<head>");
                    Console.WriteLine("<title>My Links</title>");
                    Console.WriteLine("<style type='text/css'>");
                    Console.WriteLine("html, body { height: 100%; }\n " +
                        "#linklist, #linklist a {\ndisplay: flex;\n   flex-flow: column wrap;\n   max-height: 100%; }\n" +
                        "#linklist a { margin-right: 2em }");
                    Console.WriteLine("</style>");
                    Console.WriteLine("<script>");
                    Console.WriteLine("</script>");
                    Console.WriteLine("</head>");
                    Console.WriteLine("<body>");

                    Console.WriteLine("<div id=\"linklist\">");
                    foreach (var line in File.ReadLines(arg).Where(x=>x.Length > 0))
                    {
                        line.Trim();
                        if (line[0] == '>')
                        {
                            if (firstDiv)
                            {
                                firstDiv = false;
                            }
                            else
                            {
                                Console.WriteLine("</div>");
                            }
                            divCount++;
                            Console.WriteLine($"<div>\n    <p class=\"section\">{line.Substring(1).Trim()}</p>");
                        }
                        else if (line[0] == '<')
                        {
                            Console.WriteLine($"{line}");
                        }
                        else
                        {
                            var tokens = line.Split();
                            var link = tokens[0];
                            var rest = string.Join(" ", tokens.Skip(1));
                            Console.WriteLine($"<a href=\"{link}\">{rest}</a>");
                        }
                    }
                    if (divCount > 0)
                    {
                        Console.WriteLine("</div>");
                    }
                    Console.WriteLine("</div>");
                    Console.WriteLine("</body>");
                    Console.WriteLine("</html>");
                }
            }
            catch (Exception ex)
            {
                var codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                var progname = Path.GetFileNameWithoutExtension(codeBase);
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }

        }
    }
}
