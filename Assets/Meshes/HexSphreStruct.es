set background #444

set raytracer::light [0,5000,5000]
set raytracer::samples 8
set raytracer::ambient-occlusion-samples 2
set raytracer::phong [0.6,0.6,0.9]

{ color white } Core

rule Core
{
	SphereStuct
}

rule SphereStuct w 4 md 30
{
	{ rx 4 y 1.625 } dbox
	{ rx -4 y -1.625 x 2.25 ry 8 } ubox
	{ x 4.5 ry 12 } SphereStuct
}

rule dbox w 3 maxdepth 4
{
	{ rx -8 y 3.25 x 2.25 ry 8 } dbox
	{ rx 8 } hex2
}
rule dbox { }

rule ubox w 3 maxdepth 4
{
	{ rx 8 y -3.25 x 2.25 ry 8 } ubox
	{ rx 8 } hex2
}
rule ubox { }

rule hex2 maxdepth 1 > hex
{
	{ z 1 } hex
	{ } Panel
}

rule hex w 2 maxdepth 6
{
	{ z 1 } hex
	{ } Panel
}
rule hex w 4 { }

rule PanelStruct
{
	1 * { z 2.0 } Panel
}

rule Panel md 1 w 1
{
	{ y 1.0 rz -60 y 1.0  } Panel
}

rule Panel md 6 w 16
{
	PanelPart
	{ y 1.0 rz -60 y 1.0  } Panel
}

rule PanelPart
{
	{ rz 90 s 0.2 0.2 0.1 } beamAssembly
	{ s 0.1 1.75 1 } box
}

rule RingStruct md 5
{
	{ rx -9 } RingSup1
	{ rx -9 x 1 y 2 } RingStruct
}

rule RingSup1 md 5
{
	{ ry 9 }RingSub1
	{ ry 9 x 2 } RingSup1
}

rule RingSup2 w 1 md 60
{
}

rule RingSub1 w 8 md 5
{
	{ }RingPart
	{ z 0.5 } RingSub1
	//{ ry 180 s 0.5 0.5 0.4 } beamAssembly
}

rule RingSub1 w 4 md 60
{
}

rule RingPart
{
	{ s 0.5 } PanelStruct
}

// Beam

rule beamAssembly w 1
{
	{ z -5 } beam
	{ z 5 } beam

	{ s 1 0.2 11 y 12 } box
	{ s 1 0.2 11 y -12 } box

	{ z 0.4 } vertPanel
	{ z 5 } vertPanel
	{ z -5 } vertPanel
}

rule beamAssembly w 6
{
}

rule vertPanel 
{
	{s 1 4 0.2} box
	{s 1 1 0.5} box
}

rule vertPanel md 15 > end 
{
	widePane
	{ y 1 } vertPanel
}

rule end 
{
}

rule vertPanel 
{
	{s 1 1 0.5 } box
	{s 0.2 4 0.2 x 2 } box
	{s 0.2 4 0.2 x -2 } box
	{s 1 0.2 0.2 y 10 } box
	{s 1 0.2 0.2 y -10 } box
}

rule beam 
{
	{ s 0.2 5 0.2 } box
}

rule widePane 
{
	thinBeam1
	{ y 5 } thinBeam1
	{ x 4.9 y 2.5 } thinBeamVert
	{ x -4.9 y 2.5 } thinBeamVert
	pane
}

rule pane 
{
	{s 10 5 0.05 y 0.5} box
}

rule pane 
{
	{ s 10 2.5 0.05 y 0.5 } box
}

rule thinBeam1 
{
	{ s 10 0.2 0.2 } box
}

rule thinBeamVert 
{
	{ s 0.2 5 0.2 } box
}