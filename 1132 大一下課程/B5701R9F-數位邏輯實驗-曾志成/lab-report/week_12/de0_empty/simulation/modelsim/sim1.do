vsim -voptargs=+acc work.tb_counter
view structure wave signals

do wave.do

log -r *
run -all

