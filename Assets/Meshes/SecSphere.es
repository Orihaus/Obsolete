// Arc Sphere.es
// Inspired by Marius Watz spheres:
// http://www.flickr.com/photos/watz/363931023/

set maxobjects  104000
set background #444

set raytracer::light [0,5000,5000]
set raytracer::samples 8
set raytracer::ambient-occlusion-samples 2
set raytracer::phong [0.6,0.6,0.9]

2 * {  } 1 * {  color #222  }  r1
2 * {  } 1 * {  color #444  }  r1
2 * {  } 1 * {  color #fff }  r1

rule r1 w 4
{
	ubox
	dbox
	{ x 1 ry 3 } r1 
}

rule r1  w 14 {  r2 }

rule r2 w 10 
{  
	{ x 1 ry 3 } r2
}

rule r2 {  r1 }

rule dbox w 8 maxdepth 15 
{
	{ y -1 rx 2.9  } dbox
	{rx -1.45}sbox
}

rule dbox { }

rule ubox w 8 maxdepth 15 
{
	{ y 1 rx -2.9  }  ubox
	{rx 1.45}sbox
}

rule ubox { }

rule sbox w 1
{
	{ z 2 rz 90 s 0.3 0.3 0.5 } beamAssembly
	{ s 1.2 1.2 0.6 ry 5 }  box
}

rule sbox w 4
{
	{ s 1.2 1.2 0.6 ry 5 }  box
}

rule sbox w 2
{
	{ s 1.2 1.2 0.2 ry 5 }  box
}

// Libs

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

rule RingStruct md 10
{
	{ ry 4.5 } RingSup1
	{ ry 4.5 x 1 y 2 } RingStruct
}

rule RingSup1 md 10
{
	{ ry 4.5 }RingSub1
	{ ry 4.5 x 2 } RingSup1
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