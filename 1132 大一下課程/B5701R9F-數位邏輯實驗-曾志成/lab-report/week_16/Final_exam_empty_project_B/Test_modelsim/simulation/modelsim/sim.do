#ex
vsim -voptargs=+acc work.seven_segment_decoder_tb

view structure wave signals

do wave.do

log -r *
run -all

