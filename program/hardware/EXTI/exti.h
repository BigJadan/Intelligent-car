#ifndef __EXTI_H
#define __EXIT_H	
#include <stm32f10x.h>   

//PA.15�˿�����ֵ
#define		PE2In		(GPIO_ReadInputDataBit(GPIOE, GPIO_Pin_2))
//PB.4�˿�����ֵ
#define		PE5In			(GPIO_ReadInputDataBit(GPIOE, GPIO_Pin_5))

void EXTIX_Init(void);	//�ⲿ�жϳ�ʼ��		 					    
void EXTI8_Init(void);	//�ⲿ�ж�8��ʼ��	

void TIM3_Int_Init(u16 arr,u16 psc);		//��ʱ���жϡ���ʼ�����������������ж�
void TIM4_Int_Init(u16 arr,u16 psc);		//��ʱ���жϡ���������ͷˢ��
#endif

























