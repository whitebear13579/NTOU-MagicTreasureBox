module tb_fourSAS;

    logic [3:0]a;
    logic [3:0]b;
    logic m;
    logic c, v;
    logic [3:0] s;
    logic [6:0] seg;

    fourSAS u_fourSAS(
        .a(a),
        .b(b),
        .m(m),
        .c(c),
        .s(s),
        .v(v),
        .seg(seg)
    );

    initial
    begin
            a= 15; b = 15; m = 0;
        #10 a = 4; b = 7; m = 0;
        #10 a = 0; b = 15; m = 1; 
        #10 a = 13; b = 10; m = 1; 
        #10 a = 7; b = 8; m = 1; 
        #10 $stop;
    end

    initial
    begin
        $monitor($time, " a=%d, b=%d, m=%b, seg=%b, c=%b, v=%b, s=%b", a, b, m, seg, c, v, s);
    end
endmodule