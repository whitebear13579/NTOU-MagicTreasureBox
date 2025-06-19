module tb_counter;

    logic [2:0] Q;
    logic y;
    logic clk, reset;

    counter u_counter(
        .clk(clk),
        .reset(reset),
        .Q(Q),
        .y(y)
    );
    always #5 clk = ~clk;
    initial begin
        clk = 0;
        reset = 1;
        #10 reset = 0;
        #500 $stop;
    end 

endmodule