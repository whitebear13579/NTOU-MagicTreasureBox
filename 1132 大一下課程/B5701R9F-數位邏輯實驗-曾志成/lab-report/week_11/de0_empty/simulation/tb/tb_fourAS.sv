module tb_fourAS;

    logic [3:0]a;
    logic [3:0]b;
    logic m, c, v;
    logic [3:0]s;

    fourAS u_fourAS(
        .a(a),
        .b(b),
        .m(m),
        .c(c),
        .s(s),
        .v(v)
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
        $monitor($time, " a=%d, b=%d, m=%b, s=%d, c=%b, v=%b", a, b, m, s, c, v);
    end
endmodule

