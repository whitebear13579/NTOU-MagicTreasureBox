vsim -voptargs=+acc work.testbench
view structure wave signals

do wave.do

log -r *
run -all

