module decoder(
    input A,
    input B,
    input C,
    output logic F0,
    output logic F1,
    output logic F2,
    output logic F3,
    output logic F4,
    output logic F5,
    output logic F6,
    output logic F7
);

	logic X0, X1, X2, X3, X4, X5, X6, X7;

	and AND0 (X0, ~A, ~B);
	and AND1 (F0, X0, ~C);
	and AND2 (X1, ~A, ~B);
	and AND3 (F1, X1, C);
	and AND4 (X2, ~A, B);
	and AND5 (F2, X2, ~C);
	and AND6 (X3, ~A, B);
	and AND7 (F3, X3, C);
	and AND8 (X4, A, ~B);
	and AND9 (F4, X4, ~C);
	and AND10 (X5, A, ~B);
	and AND11 (F5, X5, C);
	and AND12 (X6, A, B);
	and AND13 (F6, X6, ~C);
	and AND14 (X7, A, B);
	and AND15 (F7, X7, C);

endmodule