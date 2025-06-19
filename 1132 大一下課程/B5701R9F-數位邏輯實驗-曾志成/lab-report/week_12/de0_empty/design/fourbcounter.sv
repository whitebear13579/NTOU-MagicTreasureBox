module fourbcounter(
    input clk,
    input reset,
    output logic [3:0] Q
);
    logic a, b, c, d, Da, Db, Dc, Dd;
    dflipflop dff1(.d(Da), .clk(clk), .reset(reset), .q(a));
    dflipflop dff2(.d(Db), .clk(clk), .reset(reset), .q(b));
    dflipflop dff3(.d(Dc), .clk(clk), .reset(reset), .q(c));
    dflipflop dff4(.d(Dd), .clk(clk), .reset(reset), .q(d));

    assign Dd = (~d);
    assign Dc = (~c & d) | (c & ~d);
    assign Db = ((b & ~c) | (b & ~d)) | ((~b & c) & d);
    assign Da = ((a & ~b) | (a & ~c) | (a & ~d)) | (((~a & b) & c) & d);

    assign Q = {a, b, c, d};
endmodule