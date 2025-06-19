vsim -voptargs=+acc work.tb_fourSAS
view structure wave signals

do wave.do

log -r *
run -all

