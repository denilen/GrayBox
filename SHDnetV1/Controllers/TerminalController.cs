using Microsoft.AspNetCore.Mvc;
using SHDnet.Models;

namespace SHDnet.Controllers;

public class TerminalController : Controller
{
    private static List<string> _lines = new List<string>
    {
        "███████╗██╗  ██╗██████╗     ██╗███╗   ██╗███████╗████████╗",
        "██╔════╝██║  ██║██   ██║   ██╔╝████╗  ██║██╔════╝╚══██╔══╝",
        "███████╗███████║██   ██║  ██╔╝ ██╔██╗ ██║█████╗     ██║   ",
        "╚════██║██╔══██║██╔══██║ ██╔╝  ██║╚██╗██║██╔══╝     ██║   ",
        "███████║██║  ██║██████╔╝██╔╝   ██║ ╚████║███████╗   ██║   ",
        "╚══════╝╚═╝  ╚═╝╚═════╝ ╚═╝    ╚═╝  ╚═══╝╚══════╝   ╚═╝  "
    };

    private static List<string> _aiMemory = new List<string>();

    [HttpGet]
    public IActionResult Index()
    {
        var model = new CommandResult { Lines = _lines };
        return View(model);
    }

    [HttpPost]
    public IActionResult ExecuteCommand(string input)
    {
        var commandResult = HandleCommand(input);
        return Json(commandResult);
    }

    private CommandResult HandleCommand(string cmd)
    {
        string lowerCmd = cmd.ToLower();
        string output = "";

        switch (lowerCmd)
        {
            case "help":
                output =
                    "Available commands:\n  connect\n  info\n  launch\n  inject\n  obfuscate\n  analyze\n  ai\n  logs\n  memory\n  clear\n  download\n  theme";
                break;
            case "connect":
                output = "[+] Establishing connection...\n...\n...\n[+] Connection secured under quantum veil.";
                break;
            case "info":
                output = "SHD/NET: Shadow Hybrid Distributed Network.\nAnonymous. Resilient. Undetectable.";
                break;
            case "launch":
                output = "[LAUNCH]>>> Initiating shadow tunnel...\n[OK] Tunnel active.";
                break;
            case "inject":
                output = "[~] Deploying modules...\n[✓] rootkit.stealth loaded";
                break;
            case "obfuscate":
                output = "[✴] Obfuscation Matrix initializing...\n[✓] Encrypted misdirection online.";
                break;
            case "analyze":
                output = "Passive Analysis completed.";
                break;
            case "ai":
                output = "[🤖] AI mode initiated.";
                break;
            case "exit":
                output = "[x] Exiting AI mode.";
                break;
            case "logs":
                output = "Logs displayed.";
                break;
            case "memory":
                output = _aiMemory.Count > 0 ? string.Join("\n", _aiMemory) : "[Memory empty]";
                break;
            case "clear":
                _lines.Clear();
                break;
            case "download":
                output = "[+] Accessing secure file...";
                break;
            case "theme":
                output = "Theme toggled.";
                break;
            default:
                output = $"Unknown command: {cmd}";
                break;
        }

        _lines.Add($"> {cmd}\n{output}");
        return new CommandResult { Lines = _lines };
    }
}
