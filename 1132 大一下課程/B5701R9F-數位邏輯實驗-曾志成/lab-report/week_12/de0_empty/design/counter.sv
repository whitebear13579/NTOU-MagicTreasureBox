module counter(
    input clk,
    input reset,
    output [2:0] Q,
    output y
);

    logic a, b, c;
    logic Da, Db, Dc;

    dflipflop dff1 (.d(Da), .clk(clk), .reset(reset), .q(a));
    dflipflop dff2 (.d(Db), .clk(clk), .reset(reset), .q(b));
    dflipflop dff3 (.d(Dc), .clk(clk), .reset(reset), .q(c));

    assign Dc = ~c;
    assign Db = b^c;
    assign Da = (a & ~b) | (a & ~c) | ((~a & b) & c);

    assign y = a & b & c;
    assign Q = {a, b, c};
endmodule
