module tb_multiplexer;
	logic i0, i1, i2, i3, s0, s1;
	logic Cout;

	multiplexer u_multiplexer(
		.i0(i0),
		.i1(i1),
		.i2(i2),
		.i3(i3),
		.s0(s0),
		.s1(s1),
		.Cout(Cout)
	);

	initial
	begin
			 s1 = 0; s0 = 0; i0 = 0; i1 = 1; i2 = 1; i3 = 0;
		#10 s1 = 0; s0 = 1; i0 = 0; i1 = 1; i2 = 1; i3 = 0;
		#10 s1 = 1; s0 = 0; i0 = 0; i1 = 1; i2 = 1; i3 = 0;	 
		#10 s1 = 1; s0 = 1; i0 = 0; i1 = 1; i2 = 1; i3 = 0;	 	 
		#10 $stop;
	end
	
		
	initial
	begin
		$monitor($time, " s1=%b, s0=%b, Cout=%b, i0=%b, i1=%b, i2=%b, i3=%b",s1,s0,Cout,i0,i1,i2,i3);
	end
	
endmodule