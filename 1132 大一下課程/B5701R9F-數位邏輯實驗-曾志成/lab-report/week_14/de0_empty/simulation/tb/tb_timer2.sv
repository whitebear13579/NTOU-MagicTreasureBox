module tb_timer2;
    logic clk, reset;
    logic [2:0] sec_tens, min_tens;
    logic [3:0] sec_units, min_units;

    day_timer u_day_timer(
        .clk(clk), .reset(reset), .sec_tens(sec_tens), .min_tens(min_tens), .sec_units(sec_units), .min_units(min_units)
    );

    always #5 clk = ~clk;
    initial begin
        clk = 0; reset = 1;
        #10 reset = 0;
        #10000 $stop;
    end
endmodule
