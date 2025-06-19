module tb_rgy;
    logic [1:0]r, y, g;
    logic clk;
    logic reset;

    rgy_top u_rgy_top(
        .clk(clk),
        .reset(reset),
        .r(r),
        .g(g),
        .y(y)
    );

    always #5 clk = ~clk;
    initial begin
        clk = 0; reset = 1;
        #10 reset = 0;
        #200 $stop;
    end
endmodule