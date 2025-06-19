module day_timer(
    input clk,
    input reset,
    output logic [2:0] sec_tens, min_tens,
    output logic [3:0] sec_units, min_units
);  
    // Day Timer 00:00 ~ 23:59
    assign n_reset = reset | min_units[2] & min_tens[1];
    logic sec_unit_carry, sec_ten_carry, min_unit_carry, min_ten_carry;

    unit sec_unit(
        .clk(clk), .reset(n_reset), .Cin(1'b1), .q(sec_units), .Carry(sec_unit_carry)
    );

    ten_digit sec_ten(
        .clk(clk), .reset(n_reset), .Cin(sec_unit_carry), .q(sec_tens), .Carry(sec_ten_carry)
    );

    unit min_unit(
        .clk(clk), .reset(n_reset), .Cin(sec_ten_carry), .q(min_units), .Carry(min_unit_carry)
    );

    ten_digit min_ten(
        .clk(clk), .reset(n_reset), .Cin(min_unit_carry), .q(min_tens), .Carry(min_ten_carry)
    );
endmodule