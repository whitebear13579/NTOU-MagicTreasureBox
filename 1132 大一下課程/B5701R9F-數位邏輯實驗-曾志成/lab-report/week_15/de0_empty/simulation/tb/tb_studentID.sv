module tb_studentID;
	logic clk, reset;
	logic [3:0]id;

	studentID u_studentID(
		.clk(clk),
		.reset(reset),
		.id(id)
	);

	always #5 clk = ~clk;
	initial begin
		clk = 0; reset = 1;
		#10 reset = 0;
		#500 $stop;
	end
endmodule
