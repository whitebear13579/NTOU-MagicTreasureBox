module fourSAS(
    input [3:0] a,
    input [3:0] b,
    input m,
    output logic [3:0] s,
    output logic [6:0] seg,
    output logic c,
    output logic v
);

  logic C1, C2, C3;
  logic X0, X1, X2, X3;

  xor XOR0 (X0, b[0], m);
  xor XOR1 (X1, b[1], m);
  xor XOR2 (X2, b[2], m);
  xor XOR3 (X3, b[3], m);


  fulla f0 (
    .A   (a[0]),
    .B   (X0),
    .Cin (m),
    .S   (s[0]),
    .Cout(C1)
  );

  fulla f1 (
    .A   (a[1]),
    .B   (X1),
    .Cin (C1),
    .S   (s[1]),
    .Cout(C2)
  );

  fulla f2 (
    .A   (a[2]),
    .B   (X2),
    .Cin (C2),
    .S   (s[2]),
    .Cout(C3)
  );

  fulla f3 (
    .A   (a[3]),
    .B   (X3),
    .Cin (C3),
    .S   (s[3]),
    .Cout(c)
  );

  ssd ssd1 (
    .a   (s),
    .seg (seg)
  );

  xor XOR4 (v, C3, c);

endmodule
