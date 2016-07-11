#include "stm32f10x.h"
#include "bsp_gpio.h"
#include "bsp_usart1.h"
#include "bsp_usart2.h"
#include "wifi_config.h"
#include "wifi_function.h"
#include "bsp_SysTick.h"
#include "lcd.h"   			//TFT��ʾ��
#include "sccb.h"			//��ʼ��sccb�ӿ�	��ov7670ͨ��
#include "exti.h"			//�жϴ�����
#include "dht11.h"			//��ʪ�ȴ�����
#include "motor.h"			//2�����
#include "countspeed.h"			//����Դ��ı�����
#include "HC-SR501.h"		//������⴫����
#include "ov7670.h"			//����ͷ0V7670
#include "stdio.h"
#include <string.h>

int main(void)
{
	char *pStr;	
	char smalldata[20];							//����¶ȣ�ʪ�ȣ��������
	extern u16 iCount_L,iCount_R;				//��exit.c���涨��,�������ֵ��ٶ�
	u8 temperature,humidity,find_people=0,MQ_2status=0;		//����¶ȣ�ʪ�ȱ���,������
	u16 motor_speed=900;						//���ó�ʼ�ٶ�Ϊ900��arr=1000��PWM2ģʽ
	//bootloadģʽ�£���ʱ��������wifi�������û�취��������˿�
	//SCB->VTOR = FLASH_BASE | 0x20000; /* Vector Table Relocation in Internal FLASH. */
	
	WiFi_Config();                                                                  //��ʼ��WiFiģ��ʹ�õĽӿں�����
	SysTick_Init();                                                                 //���� SysTick Ϊ 1ms �ж�һ�� 
  ESP8266_STA_TCP_Client ();
	LCD_Init();									//��ʼ��LCD��ʾ��	
	DHT11_Init();								//��ʼ��DHT11	
	MotorDriver_Init();					//��ʼ�������io�ں�PWM,�Զ���װ�ؼĴ�����ֵarr=1000,PWM2Ƶ��Ϊ24MHz/1000=24KHz
	//OV7670_Init();								//��ʼ������ͷ	�����ˣ�ע�͵���						  
	Sensor_Init();							//��ʼ��������⴫��
	QE_Init();										//��ʼ�����������
  LCD_ShowBlibli();
	while (1)
		{
			delay_ms(200);
			pStr = ESP8266_ReceiveString ( DISABLE );	//Ϊ�˷����ֱ�Ӷ�ȡ0~9
			switch(pStr[11])
			{
				case '0' : 												
					break;
				
				case '1' :												//���׼ȷ�ٶȣ�����iCount_L��iCount_R��ֵ
					EXTI->IMR |=1<<3;//������3���ж�
					EXTI->IMR |=1<<4;
					iCount_L=0;
					iCount_R=0;
					delay_ms(500);
					EXTI->IMR &=0<<3;//������3���ж�
					EXTI->IMR &=0<<4;
					PC_Usart ( "%d%d",iCount_R,iCount_L );
					sprintf ( smalldata, "%d,%d\r\n", iCount_L,iCount_R);
					ESP8266_SendString ( DISABLE,smalldata, strlen( smalldata), Multiple_ID_0 );
					ESP8266_SendString ( DISABLE," ",strlen(" "), Multiple_ID_0 );
					break; 
				
				case '2' :												//ͼ��
					picture_send();
					break; 
				
				case '3' :												//����¶ȣ�ʪ��,���⣬����λ���Ķ�ʱ����ʱ�ռ�
					if(HCRS501==0){		//�����⵽�˾ͷ����ߵ�ƽ		
						find_people=0;   		//��ʾû���˰�
					}
					else{
						find_people=1;				//��ʾ����
					}
					if(MQ_2==0){					//����������	
					
						MQ_2status=1; 				//�����ж����壡
					}
					else{
						MQ_2status=0;				//������ȫ
					}
					DHT11_Read_Data(&temperature,&humidity);
					sprintf ( smalldata, "%d,%d,%d,%d\r\n", temperature,humidity,find_people,MQ_2status);
					ESP8266_SendString ( DISABLE,smalldata, strlen( smalldata), Multiple_ID_0 );
					ESP8266_SendString ( DISABLE," ",strlen(" "), Multiple_ID_0 );
					break; 
					
				case '4' : 												//С��ǰ��
					MotorDriver_L_Turn_Forward();			//���ֵ����ת
					MotorDriver_R_Turn_Forward();			//���ֵ����ת
					break; 
				case '5' :												//С����ת��
					MotorDriver_L_Turn_Stop();				//���ֵ���ƶ�
					MotorDriver_R_Turn_Stop();				//���ֵ���ƶ�
					MotorDriver_L_Turn_Forward();			//���ֵ����ת
					MotorDriver_R_Turn_Reverse();			//���ֵ����ת
					break; 
				
				case '6' : 												//С����ת��
					MotorDriver_L_Turn_Stop();				//���ֵ���ƶ�
					MotorDriver_R_Turn_Stop();				//���ֵ���ƶ�
					MotorDriver_R_Turn_Forward();			//���ֵ����ת	
					MotorDriver_L_Turn_Reverse();			//���ֵ����ת				
					break; 
				
				case '7' : 												//С������
					MotorDriver_L_Turn_Reverse();			//���ֵ����ת
					MotorDriver_R_Turn_Reverse();			//���ֵ����ת
					break; 
				
				case '8' : 									//С��ͣ��
					MotorDriver_L_Turn_Stop();				//���ֵ���ƶ�
					MotorDriver_R_Turn_Stop();				//���ֵ���ƶ�
					break; 
				
				case '9' : 									//����
					motor_speed=(u16)(pStr[12]-'0')*100+(u16)(pStr[13]-'0')*10+(u16)(pStr[14]-'0');//��ȡ�ٶ�
					TIM_SetCompare4(TIM1,motor_speed); 
					TIM_SetCompare4(TIM8,motor_speed); 
					break;  
					
				default : 
					break;
			}
			
		}
}

