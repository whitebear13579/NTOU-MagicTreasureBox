transcript on
if {[file exists rtl_work]} {
	vdel -lib rtl_work -all
}
vlib rtl_work
vmap work rtl_work

vlog -sv -work work +incdir+C:/Users/pan/Downloads/Final_exam_empty_project/Test_modelsim/simulation/tb {C:/Users/pan/Downloads/Final_exam_empty_project/Test_modelsim/simulation/tb/seven_segment_decoder_tb.sv}
vlog -sv -work work +incdir+C:/Users/pan/Downloads/Final_exam_empty_project/Test_modelsim/design {C:/Users/pan/Downloads/Final_exam_empty_project/Test_modelsim/design/seven_segment_decoder.sv}

