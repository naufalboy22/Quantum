using System;
using System.Threading;

public class SmartConveyorSorter
{
    // Simulasi sensor & aktuator
    static Random rnd = new Random();
    static bool simOverheat = false;
    static bool simCritical = false;
    static bool simObject = true;
    static bool simMetal = false;

    enum State { NORMAL = 0, WARNING = 1, CRITICAL = 2 }
    static State currentState = State.NORMAL;

    public static void Main()
    {
        Console.Title = "2042241024 - Naufaliano Saputra - Smart Conveyor Sorter";
        Console.Clear();
        Console.WriteLine("==================================================================");
        Console.WriteLine(" SMART CONVEYOR SORTER WITH QUANTUM-INSPIRED FSM - SIMULASI C#");
        Console.WriteLine("==================================================================");
        Console.WriteLine("Tekan 1 = Normal Operation");
        Console.WriteLine("Tekan 2 = Overheat (Warning)");
        Console.WriteLine("Tekan 3 = Critical Fault (Vibration/Encoder Error)");
        Console.WriteLine("Tekan sembarang tombol untuk mulai...");
        Console.ReadKey();

        while (true)
        {
            // Simulasi input keyboard
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                simOverheat = key == ConsoleKey.D2;
                simCritical = key == ConsoleKey.D3;
                if (key == ConsoleKey.D1) { simOverheat = false; simCritical = false; }
                simObject = true;
                simMetal = rnd.Next(0, 2) == 0; // acak material
            }

            // Baca sensor simulasi
            bool U = simObject;
            bool A = simOverheat;
            bool C = simCritical;
            bool Metal = simMetal;
            bool NonMetal = !simMetal;

            // QUANTUM FSM LOGIC (D1 = C, D0 = A & !C)
            if (C) currentState = State.CRITICAL;
            else if (A && !C) currentState = State.WARNING;
            else currentState = State.NORMAL;

            // Tampilan 
            Console.Clear();
            Console.WriteLine("==================================================================");
            Console.WriteLine(" SMART CONVEYOR SORTER WITH QUANTUM-INSPIRED FSM - C# SIMULATION");
            Console.WriteLine("==================================================================");
            Console.WriteLine($"Objek Terdeteksi   : {(U ? "YA" : "TIDAK")}");
            Console.WriteLine($"Suhu Motor         : {(A || C ? "96.8" : "67.3")} °C");
            Console.WriteLine($"Getaran / Encoder  : {(C ? "ERROR!" : "NORMAL")}");
            Console.WriteLine($"Jenis Material     : {(Metal ? "LOGAM" : NonMetal ? "NON-LOGAM" : "TIDAK ADA")}");
            Console.WriteLine();
            Console.WriteLine($"State FSM          : {currentState} ({(int)currentState:D2})");
            Console.WriteLine();
            Console.WriteLine($"Motor Conveyor     : {(C ? "OFF (Emergency)" : "ON ")}");
            Console.WriteLine($"Kipas Pendingin    : {(currentState != State.NORMAL ? "ON " : "OFF")}");
            Console.WriteLine($"LED Warning        : {(currentState != State.NORMAL ? "ON " : "OFF")}");
            Console.WriteLine($"Buzzer             : {(currentState == State.CRITICAL ? "ON " : "OFF")}");
            Console.WriteLine($"Electromagnetic Brake : {(currentState == State.CRITICAL ? "ON " : "OFF")}");
            Console.WriteLine($"Pneumatic Sorting  : {(U && currentState == State.NORMAL ? (Metal ? "KE JALUR LOGAM" : "KE JALUR NON-LOGAM") : "STOP")}");
            Console.WriteLine();
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Tekan 1/2/3 untuk ubah kondisi");

            Thread.Sleep(500);
        }
    }
}