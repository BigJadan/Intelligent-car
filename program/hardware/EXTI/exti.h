#ifndef __EXTI_H
#define __EXIT_H	
#include <stm32f10x.h>   

//PA.15端口输入值
#define		PE2In		(GPIO_ReadInputDataBit(GPIOE, GPIO_Pin_2))
//PB.4端口输入值
#define		PE5In			(GPIO_ReadInputDataBit(GPIOE, GPIO_Pin_5))

void EXTIX_Init(void);	//外部中断初始化		 					    
void EXTI8_Init(void);	//外部中断8初始化	

void TIM3_Int_Init(u16 arr,u16 psc);		//定时器中断。初始化函数，烟雾，红外中断
void TIM4_Int_Init(u16 arr,u16 psc);		//定时器中断。用于摄像头刷新
#endif

























