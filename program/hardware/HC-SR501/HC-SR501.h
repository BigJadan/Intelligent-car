#ifndef __HCSR501_H
#define __HCSR501_H


#define HCRS501 PGin(6)   	//PG6
#define MQ_2 PGin(8)   			//PG8

void HC_SR501_Init(void);		//������⴫����
void MQ_2_Init(void);    //��������

void Sensor_Init(void);
#endif
