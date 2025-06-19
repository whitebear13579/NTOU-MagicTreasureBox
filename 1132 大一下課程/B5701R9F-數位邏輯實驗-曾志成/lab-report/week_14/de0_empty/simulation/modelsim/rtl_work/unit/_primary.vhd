library verilog;
use verilog.vl_types.all;
entity unit is
    port(
        clk             : in     vl_logic;
        reset           : in     vl_logic;
        Cin             : in     vl_logic;
        q               : out    vl_logic_vector(3 downto 0);
        Carry           : out    vl_logic
    );
end unit;
