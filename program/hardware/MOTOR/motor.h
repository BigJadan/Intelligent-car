#ifndef _MOTORD_H_
#define _MOTORD_H_


void	MotorDriver_Init(void);						//电机驱动IO口外设初始化

void	MotorDriver_L_Turn_Forward(void);			//左轮电机正转
void	MotorDriver_L_Turn_Reverse(void);			//左轮电机反转
void	MotorDriver_R_Turn_Forward(void);			//右轮电机正转
void	MotorDriver_R_Turn_Reverse(void);			//右轮电机反转
void	MotorDriver_L_Turn_Stop(void);				//左轮电机制动
void	MotorDriver_R_Turn_Stop(void);				//右轮电机制动
#endif
