#include "stm32f10x.h"
#include "motor.h"

#define	MOTOR_L_IN1_LOW			(GPIO_ResetBits(GPIOB, GPIO_Pin_6))
#define	MOTOR_L_IN1_HIGH		(GPIO_SetBits(GPIOB, GPIO_Pin_6))
#define	MOTOR_L_IN2_LOW			(GPIO_ResetBits(GPIOB, GPIO_Pin_5))
#define	MOTOR_L_IN2_HIGH		(GPIO_SetBits(GPIOB, GPIO_Pin_5))

#define	MOTOR_R_IN1_LOW			(GPIO_ResetBits(GPIOC, GPIO_Pin_11))
#define	MOTOR_R_IN1_HIGH		(GPIO_SetBits(GPIOC, GPIO_Pin_11))
#define	MOTOR_R_IN2_LOW			(GPIO_ResetBits(GPIOC, GPIO_Pin_10))
#define	MOTOR_R_IN2_HIGH		(GPIO_SetBits(GPIOC, GPIO_Pin_10))

//���ֳ�ʼ��
	
void	MotorDriver_L_Config(void){
	
	GPIO_InitTypeDef					GPIO_InitStructure ;
	TIM_TimeBaseInitTypeDef		TIM_BaseInitStructure;
	TIM_OCInitTypeDef					TIM_OCInitStructure;
	
	/*ʹ��GPIOB��TIM4����ʱ��*/
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB | RCC_APB2Periph_TIM1, ENABLE);
	
	/*��ʼ��PB6�˿�ΪOut_PPģʽ*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_6;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOB, &GPIO_InitStructure);
	
	/*��ʼ��PB8�˿�ΪOut_PPģʽ*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_5;
	GPIO_Init(GPIOB, &GPIO_InitStructure);
	
	/*��ʼ��PA7�˿�(TIM4_CH2)ΪAF_PPģʽ�������*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_11;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOA, &GPIO_InitStructure);
	
	/*��ʱ����������time3 �� ch2ͨ��*/
	TIM_BaseInitStructure.TIM_Prescaler = 3-1;									//ʱ��Ԥ��Ƶ��3,TIM8�ļ���ʱ��Ƶ��Ϊ24MHz
	TIM_BaseInitStructure.TIM_Period = 1000-1;									//�Զ���װ�ؼĴ�����ֵ,PWM2Ƶ��Ϊ24MHz/1000=24KHz
	TIM_BaseInitStructure.TIM_ClockDivision = 0;								//������Ƶ
	TIM_BaseInitStructure.TIM_CounterMode = TIM_CounterMode_Up;					//���ϼ���
	TIM_BaseInitStructure.TIM_RepetitionCounter = 0;							//�ظ��Ĵ��������ظ������������ظ�������ٴβŸ�����һ������ж�,
	TIM_TimeBaseInit(TIM1, &TIM_BaseInitStructure);

	TIM_OCStructInit(&TIM_OCInitStructure);
	
	TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM2;						    /*PWM2  ch2���ģʽPWMģʽ2�� �����ϼ���ʱ��
																				һ��TIMx_CNT<TIMx_CCR1ʱͨ��1Ϊ��Ч��ƽ������Ϊ
																				��Ч��ƽ�������¼���ʱ��һ��TIMx_CNT>TIMx_CCR1ʱͨ��1Ϊ��Ч��ƽ
																				������Ϊ��Ч��ƽ��*/
	TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;				//ʹ�ܸ�ͨ�����
	TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_High;					//�������
	TIM_OC4Init(TIM1, &TIM_OCInitStructure);									//��ָ��������ʼ��

	TIM_OC4PreloadConfig(TIM8, TIM_OCPreload_Enable);							//ʹ��TIM8��CCR�ϵ�Ԥװ�ؼĴ���
	TIM_ARRPreloadConfig(TIM1, ENABLE);											//ʹ��TIM8��ARR�ϵ�Ԥװ�ؼĴ���
	
	TIM_Cmd(TIM1, ENABLE);														//ʹ��TIM8
	TIM_CtrlPWMOutputs(TIM1, ENABLE);											//ʹ��TIM8��PWM																		//PWM���ʹ��			
}


//���ֳ�ʼ��
void	MotorDriver_R_Config(void){

	GPIO_InitTypeDef					GPIO_InitStructure ;
	TIM_TimeBaseInitTypeDef		TIM_BaseInitStructure;
	TIM_OCInitTypeDef					TIM_OCInitStructure;

	/*ʹ��GPIOc��TIM8����ʱ��*/
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOC  | RCC_APB2Periph_TIM8, ENABLE);

	/*��ʼ��PB10�˿�ΪOut_PPģʽ*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_10;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOC, &GPIO_InitStructure);

	/*��ʼ��PB.111�˿�ΪOut_PPģʽ*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_11;
	GPIO_Init(GPIOC, &GPIO_InitStructure);

	/*��ʼ��PC.09�˿�(TIM8_CH4)ΪAF_PPģʽ*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_9;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOC, &GPIO_InitStructure);

	/*��ʱ����������time8 �� ch4ͨ��*/
	TIM_BaseInitStructure.TIM_Prescaler = 3-1;					//ʱ��Ԥ��Ƶ��psc=3,TIM8�ļ���ʱ��Ƶ��Ϊ24MHz
	TIM_BaseInitStructure.TIM_Period = 1000-1;					//�Զ���װ�ؼĴ�����ֵarr=1000,PWM2Ƶ��Ϊ24MHz/1000=24KHz
	TIM_BaseInitStructure.TIM_ClockDivision = 0;												//������Ƶ
	TIM_BaseInitStructure.TIM_CounterMode = TIM_CounterMode_Up;					//���ϼ���
	TIM_BaseInitStructure.TIM_RepetitionCounter = 0;										//�ظ��Ĵ���
	TIM_TimeBaseInit(TIM8, &TIM_BaseInitStructure);

	TIM_OCStructInit(&TIM_OCInitStructure);
	TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM2;										//PWM2  ch2���ģʽ
	TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;				//ʹ�ܸ�ͨ�����
	TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_High;						//�������
	TIM_OC4Init(TIM8, &TIM_OCInitStructure);														//��ָ��������ʼ��

	TIM_OC4PreloadConfig(TIM8, TIM_OCPreload_Enable);										//ʹ��TIM8��CCR�ϵ�Ԥװ�ؼĴ���
	TIM_ARRPreloadConfig(TIM8, ENABLE);																	//ʹ��TIM8��ARR�ϵ�Ԥװ�ؼĴ���
	
	TIM_Cmd(TIM8, ENABLE);																							//��TIM8
	TIM_CtrlPWMOutputs(TIM8, ENABLE);																		//PWM���ʹ��
}

void	MotorDriver_L_Turn_Forward(void){			//���ֵ����ת
	
	MOTOR_L_IN1_LOW;
	MOTOR_L_IN2_HIGH;
}

void	MotorDriver_L_Turn_Reverse(void){			//���ֵ����ת
	
	MOTOR_L_IN1_HIGH;
	MOTOR_L_IN2_LOW;
}

void	MotorDriver_R_Turn_Forward(void){			//���ֵ����ת
	
	MOTOR_R_IN1_HIGH;
	MOTOR_R_IN2_LOW;
}

void	MotorDriver_R_Turn_Reverse(void){			//���ֵ����ת
	
	MOTOR_R_IN1_LOW;
	MOTOR_R_IN2_HIGH;
}

void	MotorDriver_L_Turn_Stop(void)				//���ֵ���ƶ�
{
	MOTOR_L_IN1_HIGH;
	MOTOR_L_IN2_HIGH;
}
void	MotorDriver_R_Turn_Stop(void)				//���ֵ���ƶ�
{
	MOTOR_R_IN1_HIGH;
	MOTOR_R_IN2_HIGH;
}

void	MotorDriver_Init(void){
	
	MotorDriver_R_Config();
	MotorDriver_L_Config();
	
	MotorDriver_L_Turn_Stop();				//���ֵ���ƶ�
	MotorDriver_R_Turn_Stop();				//���ֵ���ƶ�
}
