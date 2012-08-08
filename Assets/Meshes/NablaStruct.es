set background #444

set raytracer::light [0,5000,5000]
set raytracer::samples 4
set raytracer::ambient-occlusion-samples 4
set raytracer::phong [0.6,0.6,0.9]

// 10, 20 , 21, 23, 33 , 104, 106, 118, 119, 127, 128, 133, 136, 138, 139
//1, 66,69,70,73, 79 

set maxdepth 30

{ ry -90 b 0.2 } R1

// Floor box
//{  x 30 y 30 z -3 s 900 900 1 color red } box

Rule R1 {
dbox
set seed initial
{ z 0.6 rx 5  }  R1
}

Rule R1 {
 dbox
{ z 0.5 rx -90 }  R1
}

Rule R1 {
dbox
{ z 0.6 rz 90 }  R1
}

Rule R1 {
 dbox
{ z 0.6 rz -90 }  R1
}

Rule R1 weight 0.01 {

} 

Rule dbox {
	{ color white s 2 2 0.25 }  box
}

Rule dbox weight 0.5 {
	{ ry 90 s 0.5 } R1
}

Rule dbox weight 0.5 {
	{ rx 90 } R1
} 