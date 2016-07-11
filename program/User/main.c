#include "stm32f10x.h"
#include "bsp_gpio.h"
#include "bsp_usart1.h"
#include "bsp_usart2.h"
#include "wifi_config.h"
#include "wifi_function.h"
#include "bsp_SysTick.h"
#include "lcd.h"   			//TFT显示屏
#include "sccb.h"			//初始化sccb接口	，ov7670通信
#include "exti.h"			//中断处理函数
#include "dht11.h"			//温湿度传感器
#include "motor.h"			//2个电机
#include "countspeed.h"			//电机自带的编码器
#include "HC-SR501.h"		//人体红外传感器
#include "ov7670.h"			//摄像头0V7670
#include "stdio.h"
#include <string.h>

int main(void)
{
	char *pStr;	
	char smalldata[20];							//存放温度，湿度，红外变量
	extern u16 iCount_L,iCount_R;				//在exit.c里面定义,左轮右轮的速度
	u8 temperature,humidity,find_people=0,MQ_2status=0;		//存放温度，湿度变量,红外结果
	u16 motor_speed=900;						//设置初始速度为900，arr=1000，PWM2模式
	//bootload模式下，有时候连接上wifi，但软件没办法监听网络端口
	//SCB->VTOR = FLASH_BASE | 0x20000; /* Vector Table Relocation in Internal FLASH. */
	
	WiFi_Config();                                                                  //初始化WiFi模块使用的接口和外设
	SysTick_Init();                                                                 //配置 SysTick 为 1ms 中断一次 
  ESP8266_STA_TCP_Client ();
	LCD_Init();									//初始化LCD显示屏	
	DHT11_Init();								//初始化DHT11	
	MotorDriver_Init();					//初始化电机的io口和PWM,自动重装载寄存器数值arr=1000,PWM2频率为24MHz/1000=24KHz
	//OV7670_Init();								//初始化摄像头	，坏了，注释掉先						  
	Sensor_Init();							//初始化人体红外传感
	QE_Init();										//初始化电机编码器
  LCD_ShowBlibli();
	while (1)
		{
			delay_ms(200);
			pStr = ESP8266_ReceiveString ( DISABLE );	//为了方便就直接读取0~9
			switch(pStr[11])
			{
				case '0' : 												
					break;
				
				case '1' :												//获得准确速度，处理iCount_L，iCount_R的值
					EXTI->IMR |=1<<3;//开放线3的中断
					EXTI->IMR |=1<<4;
					iCount_L=0;
					iCount_R=0;
					delay_ms(500);
					EXTI->IMR &=0<<3;//屏蔽线3的中断
					EXTI->IMR &=0<<4;
					PC_Usart ( "%d%d",iCount_R,iCount_L );
					sprintf ( smalldata, "%d,%d\r\n", iCount_L,iCount_R);
					ESP8266_SendString ( DISABLE,smalldata, strlen( smalldata), Multiple_ID_0 );
					ESP8266_SendString ( DISABLE," ",strlen(" "), Multiple_ID_0 );
					break; 
				
				case '2' :												//图传
					picture_send();
					break; 
				
				case '3' :												//获得温度，湿度,红外，由上位机的定时器定时收集
					if(HCRS501==0){		//红外检测到人就发出高电平		
						find_people=0;   		//表示没有人啊
					}
					else{
						find_people=1;				//表示有人
					}
					if(MQ_2==0){					//检测烟雾情况	
					
						MQ_2status=1; 				//发现有毒气体！
					}
					else{
						MQ_2status=0;				//环境安全
					}
					DHT11_Read_Data(&temperature,&humidity);
					sprintf ( smalldata, "%d,%d,%d,%d\r\n", temperature,humidity,find_people,MQ_2status);
					ESP8266_SendString ( DISABLE,smalldata, strlen( smalldata), Multiple_ID_0 );
					ESP8266_SendString ( DISABLE," ",strlen(" "), Multiple_ID_0 );
					break; 
					
				case '4' : 												//小车前进
					MotorDriver_L_Turn_Forward();			//左轮电机正转
					MotorDriver_R_Turn_Forward();			//右轮电机正转
					break; 
				case '5' :												//小车右转弯
					MotorDriver_L_Turn_Stop();				//左轮电机制动
					MotorDriver_R_Turn_Stop();				//右轮电机制动
					MotorDriver_L_Turn_Forward();			//左轮电机正转
					MotorDriver_R_Turn_Reverse();			//右轮电机反转
					break; 
				
				case '6' : 												//小车左转弯
					MotorDriver_L_Turn_Stop();				//左轮电机制动
					MotorDriver_R_Turn_Stop();				//右轮电机制动
					MotorDriver_R_Turn_Forward();			//右轮电机正转	
					MotorDriver_L_Turn_Reverse();			//左轮电机反转				
					break; 
				
				case '7' : 												//小车倒车
					MotorDriver_L_Turn_Reverse();			//左轮电机反转
					MotorDriver_R_Turn_Reverse();			//右轮电机反转
					break; 
				
				case '8' : 									//小车停车
					MotorDriver_L_Turn_Stop();				//左轮电机制动
					MotorDriver_R_Turn_Stop();				//右轮电机制动
					break; 
				
				case '9' : 									//变速
					motor_speed=(u16)(pStr[12]-'0')*100+(u16)(pStr[13]-'0')*10+(u16)(pStr[14]-'0');//获取速度
					TIM_SetCompare4(TIM1,motor_speed); 
					TIM_SetCompare4(TIM8,motor_speed); 
					break;  
					
				default : 
					break;
			}
			
		}
}

