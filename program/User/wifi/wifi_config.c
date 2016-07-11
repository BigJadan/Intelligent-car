#include "wifi_config.h"
#include "bsp_gpio.h"
#include "bsp_usart1.h"
#include "bsp_usart2.h"


struct  STRUCT_USARTx_Fram strEsp8266_Fram_Record = { 0 };


/**
  * @brief  WiFi_Config wifi 初始化
  * @param  无
  * @retval 无
  */
void WiFi_Config( void )
{
	WiFi_RST_INIT();
  WiFi_USART1_INIT(); 
	WiFi_USART2_INIT(); 
	WiFi_NVIC_INIT();
  
}


/// 配置NVIC中的UART2中断
void NVIC_Configuration( void )
{
	NVIC_InitTypeDef NVIC_InitStructure; 
	
	/* Configure the NVIC Preemption Priority Bits */  
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_2);

	/* Enable the USART2 Interrupt */
	NVIC_InitStructure.NVIC_IRQChannel = USART2_IRQn;	 
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_Init(&NVIC_InitStructure);

}


/*********************************************************end of file**************************************************/
