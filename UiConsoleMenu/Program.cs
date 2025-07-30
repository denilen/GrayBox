namespace UiConsoleMenu
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var menuItems = new List<string>
            {
                "Start New Process",
                "Check System Status",
                "View Logs",
                "Configure Settings",
                "Run Diagnostics",
                "Update Software",
                "Manage Users",
                "Backup Data",
                "View System Info",
                "Help & About",
                "Exit"
            };

            var selectedIndex = 0;
            var exit = false;

            Console.CursorVisible = false;

            while (!exit)
            {
                Console.Clear();
                DisplayMenu(menuItems, selectedIndex);

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex == 0) ? menuItems.Count - 1 : selectedIndex - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == menuItems.Count - 1) ? 0 : selectedIndex + 1;
                        break;

                    case ConsoleKey.Enter:
                        if (selectedIndex == menuItems.Count - 1)
                        {
                            exit = true;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"Action for '{menuItems[selectedIndex]}' would be performed here.");
                            Console.WriteLine("\nPress any key to return to the menu...");
                            Console.ReadKey();
                        }

                        break;

                    case ConsoleKey.Escape:
                        exit = true;
                        break;
                }
            }

            Console.Clear();
            Console.WriteLine("Exiting application...");
            Console.CursorVisible = true;
        }

        private static void DisplayMenu(List<string> items, int selectedIndex)
        {
            // Calculate the width for the menu frame
            var width = items.Max(s => s.Length) + 8;
            const string title = "SYSTEM MENU";
            var titlePadding = (width - title.Length) / 2;

            // Draw top border
            Console.WriteLine("╔" + new string('═', width) + "╗");
            Console.WriteLine("║" + new string(' ', titlePadding) + title +
                              new string(' ', width - title.Length - titlePadding) + "║");
            Console.WriteLine("╠" + new string('═', width) + "╣");

            // Draw menu items
            for (var i = 0; i < items.Count; i++)
            {
                var currentItem = items[i];
                string line;

                if (i == selectedIndex)
                {
                    Console.Write("║");
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    line = $"  -> {currentItem}".PadRight(width);
                    Console.Write(line);
                    Console.ResetColor();
                    Console.WriteLine("║");
                }
                else
                {
                    line = $"     {currentItem}".PadRight(width);
                    Console.WriteLine("║" + line + "║");
                }

                // Draw a separator before the 'Exit' item
                if (i == items.Count - 2)
                {
                    Console.WriteLine("╟" + new string('─', width) + "╢");
                }
            }

            // Draw bottom border
            Console.WriteLine("╚" + new string('═', width) + "╝");

            // Draw instructions
            Console.WriteLine("Use ↑ and ↓ to navigate, Enter to select, Esc to exit.");
        }
    }
}
