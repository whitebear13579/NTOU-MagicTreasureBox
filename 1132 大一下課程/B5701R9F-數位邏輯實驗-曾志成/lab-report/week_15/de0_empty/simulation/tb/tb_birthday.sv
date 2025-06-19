module tb_birthday;
	logic clk, reset;
	logic [3:0]date;

	birthday u_birthday(
		.clk(clk),
		.reset(reset),
		.date(date)
	);

	always #5 clk = ~clk;
	initial begin
		clk = 0; reset = 1;
		#10 reset = 0;
		#500 $stop;
	end
endmodule
