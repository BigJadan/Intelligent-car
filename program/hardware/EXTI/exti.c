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

void EXTI9_5_IRQHandler(void) 			//外部中断5~9服务程序
{		 		
	if(EXTI->PR&(1<<8))//是8线的中断
	{     
		if(ov_sta<2)
		{
			if(ov_sta==0)
			{
				OV7670_WRST=0;	 	//复位写指针		  		 
				OV7670_WRST=1;	
				OV7670_WREN=1;		//允许写入FIFO
			}else OV7670_WREN=0;	//禁止写入FIFO 	 
			ov_sta++;
		}
	}
	EXTI->PR=1<<8;     //清除LINE8上的中断标志位			
} 


void EXTI8_Init(void)						//外部中断8初始化，等待OV7676的同步信号~
{												  
	Ex_NVIC_Config(GPIO_A,8,RTIR); 			//任意边沿触发			  
	MY_NVIC_Init(0,0,EXTI9_5_IRQn,2);		//抢占0,子优先级0，组2	   
}

void EXTI3_IRQHandler(void){
	
	//检测触发事件是否发生
	if(EXTI_GetITStatus(EXTI_Line3) != RESET){
			iCount_L++;				//左轮计数加1
		//清除事件中断挂起位
		EXTI_ClearITPendingBit(EXTI_Line3);
	}
}

void EXTI4_IRQHandler(void){
	
	//检测触发事件是否发生
	if(EXTI_GetITStatus(EXTI_Line4) != RESET){
			iCount_R++;				//右轮计数加1
		//清除事件中断挂起位
		EXTI_ClearITPendingBit(EXTI_Line4);
	}
}


void TIM4_Int_Init(u16 arr,u16 psc)//50ms中断一次
{
	RCC->APB1ENR|=1<<2;  //TIM4 时钟使能
	TIM4->ARR=arr; //设定计数器自动重装值
	TIM4->PSC=psc; //预分频器
	TIM4->DIER|=1<<0; //允许更新中断 
	TIM4->CR1|=0x01; //使能定时器 4
	MY_NVIC_Init(1,3,TIM4_IRQn,2);//抢占 1，子优先级 3，组 2
}

void TIM4_IRQHandler(void)			//50ms中断一次
{ 		    		  			    
	if(TIM4->SR&0X0001)//溢出中断
		camera_refresh();//更新显示		
	TIM4->SR&=~(1<<0);//清除中断标志位 	    
}
