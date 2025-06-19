transcript on
if {[file exists rtl_work]} {
	vdel -lib rtl_work -all
}
vlib rtl_work
vmap work rtl_work

vlog -sv -work work +incdir+C:/Users/USER/Downloads/tempWeek10/de0_empty/design {C:/Users/USER/Downloads/tempWeek10/de0_empty/design/adder4B.sv}
vlog -sv -work work +incdir+C:/Users/USER/Downloads/tempWeek10/de0_empty/simulation/tb {C:/Users/USER/Downloads/tempWeek10/de0_empty/simulation/tb/tb_adder4B.sv}

