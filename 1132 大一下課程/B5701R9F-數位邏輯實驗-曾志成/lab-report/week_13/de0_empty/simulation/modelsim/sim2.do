vsim -voptargs=+acc work.tb_rgy
view structure wave signals

do wave.do

log -r *
run -all

