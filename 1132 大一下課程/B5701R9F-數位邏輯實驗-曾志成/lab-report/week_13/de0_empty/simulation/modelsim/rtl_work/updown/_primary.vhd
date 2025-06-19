library verilog;
use verilog.vl_types.all;
entity updown is
    port(
        clk             : in     vl_logic;
        reset           : in     vl_logic;
        x               : in     vl_logic;
        Q               : out    vl_logic_vector(3 downto 0)
    );
end updown;
