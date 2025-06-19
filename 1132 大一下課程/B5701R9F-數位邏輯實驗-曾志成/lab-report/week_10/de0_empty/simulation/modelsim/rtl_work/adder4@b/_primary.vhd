library verilog;
use verilog.vl_types.all;
entity adder4B is
    port(
        a               : in     vl_logic_vector(3 downto 0);
        b               : in     vl_logic_vector(3 downto 0);
        c0              : in     vl_logic;
        c4              : out    vl_logic;
        s               : out    vl_logic
    );
end adder4B;
