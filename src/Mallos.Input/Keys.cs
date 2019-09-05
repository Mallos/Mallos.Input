namespace Mallos.Input
{
    /// <summary> 
    /// Identifies keys on a keyboard. 
    /// </summary>	
	public enum Keys : int
    {
        Unknown = 0,

        /* Direction arrows */
        Left = 37,
        Up = 38,
        Right = 39,
        Down = 40,

        /* */
        Tab = 9,
        Return = 13,
        Enter = Return,
        Escape = 27,
        Space = 32,
        Back = 8,
        BackSpace = Back,
        Insert = 45,
        Delete = 46,
        PageUp = 33,
        PageDown = 34,
        End = 35,
        Home = 36,
        CapsLock = 20,
        Scroll = 145,
        ScrollLock = Scroll,
        PrintScreen = 44,
        Pause = 19,
        NumLock = 144,

        // Power
        Sleep = 95,

        // Windows
        LWin = 91,
        RWin = 92,

        // Applications
        Apps = 93,
        LaunchMail = 180,
        SelectMedia = 181,
        LaunchApplication1 = 182,
        LaunchApplication2 = 183,

        // Browser
        BrowserBack = 166,
        BrowserForward = 167,
        BrowserRefresh = 168,
        BrowserStop = 169,
        BrowserSearch = 170,
        BrowserFavorites = 171,
        BrowserHome = 172,

        // Volume
        VolumeMute = 173,
        VolumeDown = 174,
        VolumeUp = 175,

        // Media
        MediaNextTrack = 176,
        MediaPreviousTrack = 177,
        MediaStop = 178,
        MediaPlayPause = 179,

        /* Modifiers */
        LShiftKey = 160,
        RShiftKey = 161,
        LeftShift = LShiftKey,
        RightShift = RShiftKey,
        LControlKey = 162,
        RControlKey = 163,
        LeftControl = LControlKey,
        RightControl = RControlKey,
        LAltKey = 164,
        RAltKey = 165,
        LeftAlt = LAltKey,
        RightAlt = RAltKey,
        Menu = 18,

        /* Function keys */
        F1 = 112,
        F2 = 113,
        F3 = 114,
        F4 = 115,
        F5 = 116,
        F6 = 117,
        F7 = 118,
        F8 = 119,
        F9 = 120,
        F10 = 121,
        F11 = 122,
        F12 = 123,

        /* Keypad keys */
        NumPad0 = 96,
        NumPad1 = 97,
        NumPad2 = 98,
        NumPad3 = 99,
        NumPad4 = 100,
        NumPad5 = 101,
        NumPad6 = 102,
        NumPad7 = 103,
        NumPad8 = 104,
        NumPad9 = 105,

        Multiply = 106,
        Add = 107,
        Separator = 108,
        Subtract = 109,
        Minus = Subtract,
        Decimal = 110,
        Divide = 111,

        /* Numbers */
        D0 = 48,
        D1 = 49,
        D2 = 50,
        D3 = 51,
        D4 = 52,
        D5 = 53,
        D6 = 54,
        D7 = 55,
        D8 = 56,
        D9 = 57,

        /* Letters (Uppercase 97-122) */
        A = 65,
        B = 66,
        C = 67,
        D = 68,
        E = 69,
        F = 70,
        G = 71,
        H = 72,
        I = 73,
        J = 74,
        K = 75,
        L = 76,
        M = 77,
        N = 78,
        O = 79,
        P = 80,
        Q = 81,
        R = 82,
        S = 83,
        T = 84,
        U = 85,
        V = 86,
        W = 87,
        X = 88,
        Y = 89,
        Z = 90,

        /* Symbols */
        OemSemicolon = 186,
        OemGrave = OemTilde,
        OemPlus = 187,
        OemComma = 188,
        OemMinus = 189,
        OemPeriod = 190,
        OemQuestion = 191,
        OemTilde = 192,
        OemOpenBrackets = 219,
        OemPipe = 220,
        OemCloseBrackets = 221,
        OemQuotes = 222,
        OemBackslash = 226,
        OemClear = 254,
    }
}
