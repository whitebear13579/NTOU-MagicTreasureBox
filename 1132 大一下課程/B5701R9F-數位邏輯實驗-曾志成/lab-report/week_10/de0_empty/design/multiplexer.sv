module multiplexer(
	input s0,
	input s1,
	input i0,
	input i1,
	input i2,
	input i3,
	output logic Cout
);

	logic X0, X1, X2, X3;
	logic Y0, Y1, Y2, Y3;
	logic Z0, Z1;

	and AND0(X0, i0, ~s1);
	and AND1(Y0, X0, ~s0);
	and AND2(X1, i1, ~s1);
	and AND3(Y1, X1, s0);
	and AND4(X2, i2, s1);
	and AND5(Y2, X2, ~s0);
	and AND6(X3, i3, s1);
	and AND7(Y3, X3, s0);
	or OR0(Z0, Y0, Y1);
	or OR1(Z1, Z0, Y2);
	or OR2(Cout, Z1, Y3);

endmodule