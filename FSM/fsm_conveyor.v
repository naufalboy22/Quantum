module SmartConveyor_Control (
    input wire clk,        // Clock signal
    input wire rst_n,      // Active-low reset
    input wire A,          // Input Warning (Risiko Apapun)
    input wire C,          // Input Critical (Risiko Kritis Total)
    output wire O_Sort,    // Aktuator: Motor & Piston
    output wire O_Warn,    // Aktuator: LED & Kipas
    output wire O_Emg      // Aktuator: Buzzer & Brake
);
    // State registers (Q1 and Q0)
    reg Q1, Q0;
    wire D1, D0;  // Next state signals

    // Next state logic
    assign D1 = C;          // D1 = C (Critical state)
    assign D0 = A & (~C);   // D0 = A AND (~C) (Warning state)

    // State update logic (D Flip-Flops)
    always @(posedge clk or negedge rst_n) begin
        if (!rst_n) begin
            Q1 <= 1'b0; // Reset to State S0 (00)
            Q0 <= 1'b0;
        end else begin
            Q1 <= D1; // Update to the next state
            Q0 <= D0;
        end
    end

    // Actuator control logic (Output path)
    assign O_Sort = ~C;   // Motor & Piston ON if NOT Critical
    assign O_Warn = A | C; // LED & Cooling Fan ON if Warning or Critical
    assign O_Emg = C;     // Buzzer & Brake ON if Critical

endmodule
