namespace ZX_Spectrum_128;

internal class ZxSpectrum128
{
    private readonly byte[] _memory;
    private readonly byte[] _rom;
    private ushort _pc;                      // Program Counter
    private ushort _sp;                      // Stack Pointer
    private byte _a, _b, _c, _d, _e, _h, _l; // Регистры
    private bool[] _flags;                   // Флаги процессора
    private const int ScreenWidth = 256;
    private const int ScreenHeight = 192;
    private byte[] _screenBuffer;
    private bool _isRunning;

    private ZxSpectrum128()
    {
        _memory = new byte[65536]; // 64K memory
        _rom = new byte[16384];    // 16K ROM
        _screenBuffer = new byte[ScreenWidth * ScreenHeight];
        _flags = new bool[8];

        Initialize();
    }

    private void Initialize()
    {
        _pc = 0;
        _sp = 0xFFFF;
        _a = _b = _c = _d = _e = _h = _l = 0;

        _isRunning = true;

        // loading ROM (simplified version)
        LoadRom();
    }

    private void LoadRom()
    {
        // In a real emulator, there should be a real ROM loading
        // Fill in zero for demonstration
        for (var i = 0; i < _rom.Length; i++)
        {
            _rom[i] = 0;
        }
    }

    private void Run()
    {
        Console.WriteLine("ZX-Spectrum 128 Emulator");
        Console.WriteLine("Press Ctrl+C for exit");

        while (_isRunning)
        {
            ExecuteNextInstruction();
            UpdateScreen();

            // Basic synchronization
            Thread.Sleep(100);
        }
    }

    private void ExecuteNextInstruction()
    {
        var opcode = _memory[_pc++];

        // Basic implementation of several instructions
        switch (opcode)
        {
            case 0x00: // NOP
                break;

            case 0x3E: // LD A,n
                _a = _memory[_pc++];

                break;

            case 0x47: // LD B,A
                _b = _a;

                break;

            case 0x76: // HALT
                _isRunning = false;

                break;

            default:
                Console.WriteLine($"Unknown instruction: 0x{opcode:X2}");

                break;
        }
    }

    private void UpdateScreen()
    {
        // In a real emulator, there should be a screen of the screen
        // For demonstration, just clean the console
        Console.Clear();
        Console.WriteLine($"PC: 0x{_pc:X4} A: 0x{_a:X2} B: 0x{_b:X2}");
    }

    public static void Main()
    {
        var emulator = new ZxSpectrum128();

        emulator.Run();
    }
}
