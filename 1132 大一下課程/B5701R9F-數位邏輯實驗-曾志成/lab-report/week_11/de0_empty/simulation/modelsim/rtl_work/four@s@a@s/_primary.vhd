library verilog;
use verilog.vl_types.all;
entity fourSAS is
    port(
        a               : in     vl_logic_vector(3 downto 0);
        b               : in     vl_logic_vector(3 downto 0);
        m               : in     vl_logic;
        s               : out    vl_logic_vector(3 downto 0);
        seg             : out    vl_logic_vector(6 downto 0);
        c               : out    vl_logic;
        v               : out    vl_logic
    );
end fourSAS;
