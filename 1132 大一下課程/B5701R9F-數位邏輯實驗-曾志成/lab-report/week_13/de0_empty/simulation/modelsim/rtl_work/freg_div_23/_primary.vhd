library verilog;
use verilog.vl_types.all;
entity freg_div_23 is
    port(
        clk             : in     vl_logic;
        reset           : in     vl_logic;
        clk_out         : out    vl_logic
    );
end freg_div_23;
