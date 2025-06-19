onerror {resume}
quietly WaveActivateNextPane {} 0
add wave -noupdate -divider adder
add wave -noupdate -radix hexadecimal /seven_segment_decoder_tb/a
add wave -noupdate /seven_segment_decoder_tb/seg
TreeUpdate [SetDefaultTree]
WaveRestoreCursors {{Cursor 1} {18 ps} 0}
quietly wave cursor active 1
configure wave -namecolwidth 123
configure wave -valuecolwidth 40
configure wave -justifyvalue left
configure wave -signalnamewidth 1
configure wave -snapdistance 10
configure wave -datasetprefix 0
configure wave -rowmargin 4
configure wave -childrowmargin 2
configure wave -gridoffset 0
configure wave -gridperiod 1
configure wave -griddelta 40
configure wave -timeline 0
configure wave -timelineunits ns
update
WaveRestoreZoom {0 ps} {168 ps}
