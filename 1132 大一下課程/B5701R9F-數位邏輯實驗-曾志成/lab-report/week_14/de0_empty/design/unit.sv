module unit(
    input clk,
    input reset,
    input Cin,
    output logic [3:0]q,
    output logic Carry
);
    logic a, b, c, d, Da, Db, Dc, Dd;
    dflipflop dff1(
        .d(Da), .clk(clk), .reset(reset), .q(a)
    );

    dflipflop dff2(
        .d(Db), .clk(clk), .reset(reset), .q(b)
    );

    dflipflop dff3(
        .d(Dc), .clk(clk), .reset(reset), .q(c)
    );

    dflipflop dff4(
        .d(Dd), .clk(clk), .reset(reset), .q(d)
    );

    assign Da = (~Cin & a) | (a & ~d) | (Cin & b & c & d);
    assign Db = (~Cin & b) | (b & ~c) | (b & ~d) | (Cin & ~b & c & d);
    assign Dc = (~Cin & c) | (c & ~d) | (Cin & ~a & ~c & d);
    assign Dd = ((~Cin)&d) | (Cin&(~d));
    assign Carry = Cin & a & d;
    assign q[3:0] = {a,b,c,d};
endmodule