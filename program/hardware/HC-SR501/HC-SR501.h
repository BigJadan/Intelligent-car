#ifndef __HCSR501_H
#define __HCSR501_H


#define HCRS501 PGin(6)   	//PG6
#define MQ_2 PGin(8)   			//PG8

void HC_SR501_Init(void);		//人体红外传感器
void MQ_2_Init(void);    //烟雾传感器

void Sensor_Init(void);
#endif
