module testbench;


	logic [7:0] a;
	logic [7:0] b;
	logic [8:0] s_out, s_out1;
 



	
	initial begin

		#10 a = 10; 	b = 5;
		#10 a = 100; 	b = 15;
		#10 a = 100; 	b = 25;
		#10 a = 100; 	b = 56;
		#10 a = 0; 		b = 0;
		#10 a = 255; 	b = 255;
		#1000 $stop;
	end
endmodule