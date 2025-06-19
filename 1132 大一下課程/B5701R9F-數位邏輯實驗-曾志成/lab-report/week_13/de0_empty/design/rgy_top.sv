module rgy_top(
    input logic clk,
    input logic reset,
    output logic [1:0]r, g, y
);
    logic [3:0]Q;

    updown comb(
        .clk(clk),
        .reset(reset),
        .x(1'b0),
        .Q(Q)
    );

    rgy_comb rgy(
        .q(Q),
        .r(r),
        .g(g),
        .y(y)
    );
endmodule