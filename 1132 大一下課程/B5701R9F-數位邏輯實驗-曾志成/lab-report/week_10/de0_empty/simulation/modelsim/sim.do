vsim -voptargs=+acc work.tb_adder4B
view structure wave signals

do wave.do

log -r *
run -all

