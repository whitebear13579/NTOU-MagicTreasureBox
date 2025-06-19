module studentID(
	input clk, reset,
	output logic [3:0]id
);
	logic a, b, c, d, Da, Db, Dc, Dd;

	dflipflop dff1(.d(Da), .reset(reset), .clk(clk), .q(a));
	dflipflop dff2(.d(Db), .reset(reset), .clk(clk), .q(b));
	dflipflop dff3(.d(Dc), .reset(reset), .clk(clk), .q(c));
	dflipflop dff4(.d(Dd), .reset(reset), .clk(clk), .q(d));

	assign Da = (b&c) | (c&(~d)) | ((~b)&(~c)&d);
	assign Db = ((~a)&b) | ((~a)&c);
	assign Dc = ((~c)&d) | (a&c);
	assign Dd = ((~b)&c) | ((~a)&(~d)) | ((b)&(~c)&(d));

	assign id[3:0] = {a, b, c, d};

endmodule
