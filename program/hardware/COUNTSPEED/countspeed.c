#include	"stm32f10x.h"
#include	"countspeed.h"


void	Motor_Exti3_Config(void){
	
	EXTI_InitTypeDef	EXTI_InitStructure;
	GPIO_InitTypeDef	GPIO_InitStructure;
	NVIC_InitTypeDef	NVIC_InitStructure;
	
	/*ʹ��GPIOE��AFIO����ʱ��*/
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOE | RCC_APB2Periph_AFIO, ENABLE);
		
	/*��ʼ��PE.2�˿�ΪIN_FLOATINGģʽ*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_2;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
	GPIO_Init(GPIOE, &GPIO_InitStructure);
	
	/*��ʼ��PE.3�˿�ΪIN_FLOATINGģʽ*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_3;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
	GPIO_Init(GPIOE, &GPIO_InitStructure);
	
	/*ѡ��PE.03Ϊ�ⲿ�ж�*/
	GPIO_EXTILineConfig(GPIO_PortSourceGPIOE, GPIO_PinSource3);
	
	/*����Px.03ΪӲ���ж�ģʽ�������ش���*/
	EXTI_InitStructure.EXTI_Line = EXTI_Line3;
	EXTI_InitStructure.EXTI_Mode = EXTI_Mode_Interrupt;				//�����ش���
	EXTI_InitStructure.EXTI_Trigger = EXTI_Trigger_Rising;
	EXTI_InitStructure.EXTI_LineCmd = ENABLE;
	EXTI_Init(&EXTI_InitStructure);
	
	/*EXTI3 NVIC����*/
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_2);							//2 bits for pre-emption priority 2 bits for subpriority
	
	NVIC_InitStructure.NVIC_IRQChannel = EXTI3_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 3;		//ָ����ռʽ���ȼ���,��ȡ0~3
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0;					//ָ����Ӧʽ���ȼ���,��ȡ0~3
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_Init(&NVIC_InitStructure);
}

void	Motor_Exti4_Config(void){
	
	EXTI_InitTypeDef	EXTI_InitStructure;
	GPIO_InitTypeDef	GPIO_InitStructure;
	NVIC_InitTypeDef	NVIC_InitStructure;
	
	/*ʹ��GPIOE��AFIO����ʱ��*/
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOE | RCC_APB2Periph_AFIO, ENABLE);
	
	/*��ʼ��PE.4�˿�ΪIN_FLOATINGģʽ*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_4;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
	GPIO_Init(GPIOE, &GPIO_InitStructure);
	
	/*��ʼ��PE.05�˿�ΪIN_FLOATINGģʽ*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_5;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
	GPIO_Init(GPIOE, &GPIO_InitStructure);
	
	/*ѡ��PE.04Ϊ�ⲿ�ж�*/
	GPIO_EXTILineConfig(GPIO_PortSourceGPIOE, GPIO_PinSource4);
	
	/*����Px.04ΪӲ���ж�ģʽ�������ش���*/
	EXTI_InitStructure.EXTI_Line = EXTI_Line4;
	EXTI_InitStructure.EXTI_Mode = EXTI_Mode_Interrupt;
	EXTI_InitStructure.EXTI_Trigger = EXTI_Trigger_Rising;
	EXTI_InitStructure.EXTI_LineCmd = ENABLE;
	EXTI_Init(&EXTI_InitStructure);
	
	/*EXTI4 NVIC����*/
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_2);							//2 bits for pre-emption priority 2 bits for subpriority
	
	NVIC_InitStructure.NVIC_IRQChannel = EXTI4_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 3;		//ָ����ռʽ���ȼ���,��ȡ0~3
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 1;					//ָ����Ӧʽ���ȼ���,��ȡ0~3
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_Init(&NVIC_InitStructure);
}


void	QE_Init(void){
	
	Motor_Exti3_Config();
	Motor_Exti4_Config();
	
}
