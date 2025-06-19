module updown(
    input logic clk,
    input logic reset,
    input logic x,
    output logic [3:0]Q
);
    logic a, b, c, d, Da, Db, Dc, Dd;

    dflipflop diff1 ( .d(Da), .clk(clk) , .reset(reset), .q(a));
    dflipflop diff2 ( .d(Db), .clk(clk) , .reset(reset), .q(b));
    dflipflop diff3 ( .d(Dc), .clk(clk) , .reset(reset), .q(c));
    dflipflop diff4 ( .d(Dd), .clk(clk) , .reset(reset), .q(d));

    assign Dd = ~d;
    assign Dc = (~x & (c ^ d)) | ( x & (~c & ~d | c & d));
    assign Db = (~x & ( b & (~c | ~d) | ~b & c & d )) | ( x & ( b & (c | d) | ~b & ~c & ~d ));
    assign Da = (~x & ( a & ~b | a & ~c | a & ~d | ~a & b & c & d )) | ( x & ( a &  b | a &  c | a &  d | ~a & ~b & ~c & ~d ));

    assign Q = {a, b, c, d};
endmodule