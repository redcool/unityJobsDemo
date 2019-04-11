# unityJobsDemo
unity's jobs demo code

cpu : i7 8700 3.2g
memory : 16h
video : gtx 1060 5g

--------------
数量(1000)		模式			Max		Medium		Min

--------------
SimpleMover		Mono		6.xms	3.xms		0.6xms
	1000个
(SimpleMoverSystem) momo	2.8ms	1.6ms		0.4ms

--------------
Mover			ecs			2.xms	1.xms		0.5ms
UpdateFunction				2.xms	1.xms		0.5ms

--------------
MoverJob		ecs+job		0.97ms	0.3ms		0.19ms
