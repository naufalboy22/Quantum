module tb_SmartConveyor_Control;

    reg clk;               // Clock signal
    reg rst_n;             // Active-low reset
    reg A;                 // Input Warning
    reg C;                 // Input Critical
    wire O_Sort;           // Aktuator: Motor & Piston
    wire O_Warn;           // Aktuator: LED & Kipas
    wire O_Emg;            // Aktuator: Buzzer & Brake

    // Instantiate the Smart Conveyor Control module
    SmartConveyor_Control uut (
        .clk(clk),
        .rst_n(rst_n),
        .A(A),
        .C(C),
        .O_Sort(O_Sort),
        .O_Warn(O_Warn),
        .O_Emg(O_Emg)
    );

    // Clock generation
    initial begin
        clk = 0;
        forever #5 clk = ~clk;  // Generate clock with period 10
    end

    // Test sequence
    initial begin
        // Initialize signals
        rst_n = 0;
        A = 0;
        C = 0;

        // Apply reset
        #10 rst_n = 1;

        // Test: NORMAL state (S0)
        #10 A = 0; C = 0;  // No warning, no critical
        #20 A = 1; C = 0;  // Warning condition
        #20 A = 0; C = 1;  // Critical condition
        #20 A = 0; C = 0;  // Back to normal state
        #20 $finish;
    end

    // Display outputs for monitoring
    initial begin
        $monitor("At time %t: A = %b, C = %b, O_Sort = %b, O_Warn = %b, O_Emg = %b", 
                 $time, A, C, O_Sort, O_Warn, O_Emg);
    end

endmodule
