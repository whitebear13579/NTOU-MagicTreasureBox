module fulla(
  input  logic A,
  input  logic B,
  input  logic Cin,
  output logic S,
  output logic Cout
);
  assign S    = A ^ B ^ Cin;
  assign Cout = (A & B) | (A & Cin) | (B & Cin);
endmodule
