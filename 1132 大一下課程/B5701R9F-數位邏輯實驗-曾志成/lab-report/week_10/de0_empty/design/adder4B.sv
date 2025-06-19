module adder4B(
	input [3:0]a,
	input [3:0]b,
	input c0,
	output logic c4,
	output logic s
);

	logic c1, c2, c3;
	
	xor xor1(c1,a,b);
	xor xor2(s,c1,c0);
	and and1(c2,c1,c0);
	and and2(c3,a,b);
	or or1(c4,c2,c3);
	
endmodule