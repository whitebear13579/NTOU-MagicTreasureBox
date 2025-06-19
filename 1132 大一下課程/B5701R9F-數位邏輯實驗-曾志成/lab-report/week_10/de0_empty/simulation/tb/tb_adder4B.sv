module tb_adder4B;
	logic [3:0]a,b,s;
	logic c0, c4;
	
	adder4B u_adder4B_0(
		.a(a[0]),
		.b(b[0]),
		.c0(c0),
		.c4(c1),
		.s(s[0])
	);
	
	adder4B u_adder4B_1(
		.a(a[1]),
		.b(b[1]),
		.c0(c1),
		.c4(c2),
		.s(s[1])
	);
	
	adder4B u_adder4B_2(
		.a(a[2]),
		.b(b[2]),
		.c0(c2),
		.c4(c3),
		.s(s[2])
	);
	
	adder4B u_adder4B_3(
		.a(a[3]),
		.b(b[3]),
		.c0(c3),
		.c4(c4),
		.s(s[3])
	);
	
	initial
	begin
			 c0 = 0; a[0] = 1; a[1] = 1; a[2] = 1; a[3] = 0; b[0] = 0; b[1] = 1; b[2] = 1; b[3] = 0;
		#10 c0 = 0; a[0] = 0; a[1] = 0; a[2] = 0; a[3] = 1; b[0] = 1; b[1] = 0; b[2] = 0; b[3] = 1;
		#10 c0 = 1; a[0] = 0; a[1] = 0; a[2] = 1; a[3] = 1; b[0] = 0; b[1] = 0; b[2] = 0; b[3] = 1;	 
		#10 c0 = 1; a[0] = 1; a[1] = 0; a[2] = 1; a[3] = 0; b[0] = 0; b[1] = 1; b[2] = 0; b[3] = 1;
		#10 c0 = 1	; a[0] = 0; a[1] = 0; a[2] = 0; a[3] = 0; b[0] = 1; b[1] = 0; b[2] = 0; b[3] = 0;
		#10 $stop;
	end
	
		
	initial
	begin
		$monitor($time, " s=%b, c0=%b, b=%b, a=%b",s,c0,b,a);
	end
	
endmodule