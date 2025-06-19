module ten_digit(
    input clk,
    input reset,
    input Cin,
    output [2:0]q,
    output logic Carry
);
    logic a, b, c, Da, Db, Dc;

    dflipflop dff1(
        .d(Da), .clk(clk), .reset(reset), .q(a)
    );

    dflipflop dff2(
        .d(Db), .clk(clk), .reset(reset), .q(b)
    );

    dflipflop dff3(
        .d(Dc), .clk(clk), .reset(reset), .q(c)
    );
    
    assign Da = ((~Cin)&a) | (a&(~c)) | (Cin&c&b);
    assign Db = ((~Cin)&b) | (b&(~c)) | (Cin&(~a)&(~b)&c);
    assign Dc = ((~Cin)&c) | (Cin&(~c));
    assign Carry = Cin & a & c;
    assign q[2:0] = {a,b,c};
endmodule