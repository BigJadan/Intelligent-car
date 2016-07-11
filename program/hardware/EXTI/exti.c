#include "exti.h"
#include "bsp_usart1.h"
#include "ov7670.h"
#include "wifi_config.h"
#include "wifi_function.h"
#include "bsp_SysTick.h"
#include <string.h>
#include <stdio.h> 
u8 ov_sta;
u16 iCount_L=0,iCount_R=0;

void EXTI9_5_IRQHandler(void) 			//�ⲿ�ж�5~9�������
{		 		
	if(EXTI->PR&(1<<8))//��8�ߵ��ж�
	{     
		if(ov_sta<2)
		{
			if(ov_sta==0)
			{
				OV7670_WRST=0;	 	//��λдָ��		  		 
				OV7670_WRST=1;	
				OV7670_WREN=1;		//����д��FIFO
			}else OV7670_WREN=0;	//��ֹд��FIFO 	 
			ov_sta++;
		}
	}
	EXTI->PR=1<<8;     //���LINE8�ϵ��жϱ�־λ			
} 


void EXTI8_Init(void)						//�ⲿ�ж�8��ʼ�����ȴ�OV7676��ͬ���ź�~
{												  
	Ex_NVIC_Config(GPIO_A,8,RTIR); 			//������ش���			  
	MY_NVIC_Init(0,0,EXTI9_5_IRQn,2);		//��ռ0,�����ȼ�0����2	   
}

void EXTI3_IRQHandler(void){
	
	//��ⴥ���¼��Ƿ���
	if(EXTI_GetITStatus(EXTI_Line3) != RESET){
			iCount_L++;				//���ּ�����1
		//����¼��жϹ���λ
		EXTI_ClearITPendingBit(EXTI_Line3);
	}
}

void EXTI4_IRQHandler(void){
	
	//��ⴥ���¼��Ƿ���
	if(EXTI_GetITStatus(EXTI_Line4) != RESET){
			iCount_R++;				//���ּ�����1
		//����¼��жϹ���λ
		EXTI_ClearITPendingBit(EXTI_Line4);
	}
}


void TIM4_Int_Init(u16 arr,u16 psc)//50ms�ж�һ��
{
	RCC->APB1ENR|=1<<2;  //TIM4 ʱ��ʹ��
	TIM4->ARR=arr; //�趨�������Զ���װֵ
	TIM4->PSC=psc; //Ԥ��Ƶ��
	TIM4->DIER|=1<<0; //��������ж� 
	TIM4->CR1|=0x01; //ʹ�ܶ�ʱ�� 4
	MY_NVIC_Init(1,3,TIM4_IRQn,2);//��ռ 1�������ȼ� 3���� 2
}

void TIM4_IRQHandler(void)			//50ms�ж�һ��
{ 		    		  			    
	if(TIM4->SR&0X0001)//����ж�
		camera_refresh();//������ʾ		
	TIM4->SR&=~(1<<0);//����жϱ�־λ 	    
}
