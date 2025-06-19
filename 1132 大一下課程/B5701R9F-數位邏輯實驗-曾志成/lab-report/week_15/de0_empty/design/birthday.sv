module birthday(
	input clk, reset,
	output logic [3:0]date
);

	logic a, b, c, d, Da, Db, Dc, Dd;

	dflipflop dff1(.d(Da), .reset(reset), .clk(clk), .q(a));
	dflipflop dff2(.d(Db), .reset(reset), .clk(clk), .q(b));
	dflipflop dff3(.d(Dc), .reset(reset), .clk(clk), .q(c));
	dflipflop dff4(.d(Dd), .reset(reset), .clk(clk), .q(d));

	assign Da = ((~a) & (~c)) | ((a) & (~d));
	assign Db = ((~a) & (~d)) | ((~a) & (~c)) | (a&c&d);
	assign Dc = (~c) | ((~a)&(~b));
	assign Dd = ((~a)&(~b)) | (a&c);

	assign date[3:0] = {a, b, c, d};

endmodule
