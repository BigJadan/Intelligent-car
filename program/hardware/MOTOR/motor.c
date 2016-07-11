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

//左轮初始化
	
void	MotorDriver_L_Config(void){
	
	GPIO_InitTypeDef					GPIO_InitStructure ;
	TIM_TimeBaseInitTypeDef		TIM_BaseInitStructure;
	TIM_OCInitTypeDef					TIM_OCInitStructure;
	
	/*使能GPIOB、TIM4外设时钟*/
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB | RCC_APB2Periph_TIM1, ENABLE);
	
	/*初始化PB6端口为Out_PP模式*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_6;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOB, &GPIO_InitStructure);
	
	/*初始化PB8端口为Out_PP模式*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_5;
	GPIO_Init(GPIOB, &GPIO_InitStructure);
	
	/*初始化PA7端口(TIM4_CH2)为AF_PP模式推挽输出*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_11;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOA, &GPIO_InitStructure);
	
	/*定时器基本配置time3 的 ch2通道*/
	TIM_BaseInitStructure.TIM_Prescaler = 3-1;									//时钟预分频数3,TIM8的计数时钟频率为24MHz
	TIM_BaseInitStructure.TIM_Period = 1000-1;									//自动重装载寄存器数值,PWM2频率为24MHz/1000=24KHz
	TIM_BaseInitStructure.TIM_ClockDivision = 0;								//采样分频
	TIM_BaseInitStructure.TIM_CounterMode = TIM_CounterMode_Up;					//向上计数
	TIM_BaseInitStructure.TIM_RepetitionCounter = 0;							//重复寄存器，是重复计数，就是重复溢出多少次才给你来一个溢出中断,
	TIM_TimeBaseInit(TIM1, &TIM_BaseInitStructure);

	TIM_OCStructInit(&TIM_OCInitStructure);
	
	TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM2;						    /*PWM2  ch2输出模式PWM模式2－ 在向上计数时，
																				一旦TIMx_CNT<TIMx_CCR1时通道1为无效电平，否则为
																				有效电平；在向下计数时，一旦TIMx_CNT>TIMx_CCR1时通道1为有效电平
																				，否则为无效电平。*/
	TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;				//使能该通道输出
	TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_High;					//输出极性
	TIM_OC4Init(TIM1, &TIM_OCInitStructure);									//按指定参数初始化

	TIM_OC4PreloadConfig(TIM8, TIM_OCPreload_Enable);							//使能TIM8在CCR上的预装载寄存器
	TIM_ARRPreloadConfig(TIM1, ENABLE);											//使能TIM8在ARR上的预装载寄存器
	
	TIM_Cmd(TIM1, ENABLE);														//使能TIM8
	TIM_CtrlPWMOutputs(TIM1, ENABLE);											//使能TIM8，PWM																		//PWM输出使能			
}


//右轮初始化
void	MotorDriver_R_Config(void){

	GPIO_InitTypeDef					GPIO_InitStructure ;
	TIM_TimeBaseInitTypeDef		TIM_BaseInitStructure;
	TIM_OCInitTypeDef					TIM_OCInitStructure;

	/*使能GPIOc、TIM8外设时钟*/
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOC  | RCC_APB2Periph_TIM8, ENABLE);

	/*初始化PB10端口为Out_PP模式*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_10;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOC, &GPIO_InitStructure);

	/*初始化PB.111端口为Out_PP模式*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_11;
	GPIO_Init(GPIOC, &GPIO_InitStructure);

	/*初始化PC.09端口(TIM8_CH4)为AF_PP模式*/
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_9;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOC, &GPIO_InitStructure);

	/*定时器基本配置time8 的 ch4通道*/
	TIM_BaseInitStructure.TIM_Prescaler = 3-1;					//时钟预分频数psc=3,TIM8的计数时钟频率为24MHz
	TIM_BaseInitStructure.TIM_Period = 1000-1;					//自动重装载寄存器数值arr=1000,PWM2频率为24MHz/1000=24KHz
	TIM_BaseInitStructure.TIM_ClockDivision = 0;												//采样分频
	TIM_BaseInitStructure.TIM_CounterMode = TIM_CounterMode_Up;					//向上计数
	TIM_BaseInitStructure.TIM_RepetitionCounter = 0;										//重复寄存器
	TIM_TimeBaseInit(TIM8, &TIM_BaseInitStructure);

	TIM_OCStructInit(&TIM_OCInitStructure);
	TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM2;										//PWM2  ch2输出模式
	TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;				//使能该通道输出
	TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_High;						//输出极性
	TIM_OC4Init(TIM8, &TIM_OCInitStructure);														//按指定参数初始化

	TIM_OC4PreloadConfig(TIM8, TIM_OCPreload_Enable);										//使能TIM8在CCR上的预装载寄存器
	TIM_ARRPreloadConfig(TIM8, ENABLE);																	//使能TIM8在ARR上的预装载寄存器
	
	TIM_Cmd(TIM8, ENABLE);																							//打开TIM8
	TIM_CtrlPWMOutputs(TIM8, ENABLE);																		//PWM输出使能
}

void	MotorDriver_L_Turn_Forward(void){			//左轮电机正转
	
	MOTOR_L_IN1_LOW;
	MOTOR_L_IN2_HIGH;
}

void	MotorDriver_L_Turn_Reverse(void){			//左轮电机反转
	
	MOTOR_L_IN1_HIGH;
	MOTOR_L_IN2_LOW;
}

void	MotorDriver_R_Turn_Forward(void){			//右轮电机正转
	
	MOTOR_R_IN1_HIGH;
	MOTOR_R_IN2_LOW;
}

void	MotorDriver_R_Turn_Reverse(void){			//右轮电机反转
	
	MOTOR_R_IN1_LOW;
	MOTOR_R_IN2_HIGH;
}

void	MotorDriver_L_Turn_Stop(void)				//左轮电机制动
{
	MOTOR_L_IN1_HIGH;
	MOTOR_L_IN2_HIGH;
}
void	MotorDriver_R_Turn_Stop(void)				//右轮电机制动
{
	MOTOR_R_IN1_HIGH;
	MOTOR_R_IN2_HIGH;
}

void	MotorDriver_Init(void){
	
	MotorDriver_R_Config();
	MotorDriver_L_Config();
	
	MotorDriver_L_Turn_Stop();				//左轮电机制动
	MotorDriver_R_Turn_Stop();				//右轮电机制动
}
