module testbench;
	logic clk, reset;
	logic [3:0] q;

	accumulator_top accumulator_0(
		.clk		(clk		),
		.reset		(reset		),
		.q			(q	)
	);
	
	always #5 clk = ~clk;	//每過 5 單位時間，clk 反向
	
	initial begin
			clk = 0; reset = 1;
		#10 reset = 0;
		#200 $stop;
	end
endmodule