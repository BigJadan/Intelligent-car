#include "HC-SR501.h"
#include "stm32f10x.h"
								    
//人体红外传感器初始化函数-----检测PG6是否高电平
void HC_SR501_Init(void)
{
	GPIO_InitTypeDef GPIO_InitStructure;
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOG,ENABLE);
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_6;  
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPD;  //置成输入，下拉
	GPIO_Init(GPIOG, &GPIO_InitStructure); 
} 

void MQ_2_Init(void)
{
	GPIO_InitTypeDef GPIO_InitStructure;
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOG,ENABLE);
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_8;  
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPD;  //置成输入，下拉
	GPIO_Init(GPIOG, &GPIO_InitStructure); 
}

void Sensor_Init(void)
{
	HC_SR501_Init();
	MQ_2_Init();
}




