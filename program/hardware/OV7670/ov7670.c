#include "ov7670.h"
#include "ov7670cfg.h"
#include "exti.h"
#include "bsp_SysTick.h"
#include "bsp_usart1.h"			 
#include "sccb.h"	
#include "lcd.h"
#include "wifi_config.h"
#include "wifi_function.h"
#include "stdio.h"
#include "string.h"
#include "malloc.h"	   
	    
//��ʼ��OV7670
//����0:�ɹ�
//��������ֵ:�������
u8 OV7670_Init(void)
{
	u8 temp;
	u16 i=0;	  
	//����IO
	RCC->APB2ENR|=1<<2;		//��ʹ������PORTAʱ��
	RCC->APB2ENR|=1<<3;		//��ʹ������PORTBʱ��
 	RCC->APB2ENR|=1<<4;		//��ʹ������PORTCʱ��
  	RCC->APB2ENR|=1<<5;		//��ʹ������PORTDʱ��
	RCC->APB2ENR|=1<<8;		//��ʹ������PORTGʱ��	   
 
  	GPIOA->CRH&=0XFFFFFFF0; 	   
  	GPIOA->CRH|=0X00000008; 	//PA8 ����  
	GPIOA->ODR|=1<<8; 
   	GPIOB->CRL&=0XFFF00FFF; 	  
  	GPIOB->CRL|=0X00033000; 	//PB3/4 ���
	GPIOB->ODR|=3<<3; 	    
  	GPIOC->CRL=0X88888888; 		//PC0~7 ����    
	GPIOC->ODR|=0x00ff; 
   	GPIOD->CRL&=0XF0FFFFFF; 	//PD6 ���   
  	GPIOD->CRL|=0X03000000; 	 
 	GPIOD->ODR|=1<<6; 
   	GPIOG->CRH&=0X00FFFFFF; 	 
	GPIOG->CRH|=0X33000000;	    
	GPIOG->ODR=7<<14;      		//PG14/15  ����� 
 	JTAG_Set(1);
 	SCCB_Init();        		//��ʼ��SCCB ��IO��	   	  
 	if(SCCB_WR_Reg(0x12,0x80))return 1;	//��λSCCB	  
	Delay_ms(50); 
	//��ȡ��Ʒ�ͺ�
 	temp=SCCB_RD_Reg(0x0b);   
	if(temp!=0x73)return 2;  
 	temp=SCCB_RD_Reg(0x0a);   
	if(temp!=0x76)return 2;
	//��ʼ������	  
	for(i=0;i<sizeof(ov7670_init_reg_tbl)/sizeof(ov7670_init_reg_tbl[0]);i++)
	{
	   	SCCB_WR_Reg(ov7670_init_reg_tbl[i][0],ov7670_init_reg_tbl[i][1]);
		delay_ms(2);
 	}
	EXTI8_Init();								//ʹ�ܶ�ʱ������
	OV7670_Window_Set(10,174,240,320);			//���ô���	  
  	OV7670_CS=0;
	TIM4_Int_Init(499,7199);					//Tout= ((arr+1)*(psc+1))/Tclk��50ms�ж�һ�Σ�����ܷ�ˢ��ͼƬ
   	return 0x00; 	//ok
} 
////////////////////////////////////////////////////////////////////////////
//OV7670��������
//��ƽ������
//0:�Զ�
//1:̫��sunny
//2,����cloudy
//3,�칫��office
//4,����home
void OV7670_Light_Mode(u8 mode)
{
	u8 reg13val=0XE7;//Ĭ�Ͼ�������Ϊ�Զ���ƽ��
	u8 reg01val=0;
	u8 reg02val=0;
	switch(mode)
	{
		case 1://sunny
			reg13val=0XE5;
			reg01val=0X5A;
			reg02val=0X5C;
			break;	
		case 2://cloudy
			reg13val=0XE5;
			reg01val=0X58;
			reg02val=0X60;
			break;	
		case 3://office
			reg13val=0XE5;
			reg01val=0X84;
			reg02val=0X4c;
			break;	
		case 4://home
			reg13val=0XE5;
			reg01val=0X96;
			reg02val=0X40;
			break;	
	}
	SCCB_WR_Reg(0X13,reg13val);//COM8���� 
	SCCB_WR_Reg(0X01,reg01val);//AWB��ɫͨ������ 
	SCCB_WR_Reg(0X02,reg02val);//AWB��ɫͨ������ 
}				  
//ɫ������
//0:-2
//1:-1
//2,0
//3,1
//4,2
void OV7670_Color_Saturation(u8 sat)
{
	u8 reg4f5054val=0X80;//Ĭ�Ͼ���sat=2,��������ɫ�ȵ�����
 	u8 reg52val=0X22;
	u8 reg53val=0X5E;
 	switch(sat)
	{
		case 0://-2
			reg4f5054val=0X40;  	 
			reg52val=0X11;
			reg53val=0X2F;	 	 
			break;	
		case 1://-1
			reg4f5054val=0X66;	    
			reg52val=0X1B;
			reg53val=0X4B;	  
			break;	
		case 3://1
			reg4f5054val=0X99;	   
			reg52val=0X28;
			reg53val=0X71;	   
			break;	
		case 4://2
			reg4f5054val=0XC0;	   
			reg52val=0X33;
			reg53val=0X8D;	   
			break;	
	}
	SCCB_WR_Reg(0X4F,reg4f5054val);	//ɫ�ʾ���ϵ��1
	SCCB_WR_Reg(0X50,reg4f5054val);	//ɫ�ʾ���ϵ��2 
	SCCB_WR_Reg(0X51,0X00);			//ɫ�ʾ���ϵ��3  
	SCCB_WR_Reg(0X52,reg52val);		//ɫ�ʾ���ϵ��4 
	SCCB_WR_Reg(0X53,reg53val);		//ɫ�ʾ���ϵ��5 
	SCCB_WR_Reg(0X54,reg4f5054val);	//ɫ�ʾ���ϵ��6  
	SCCB_WR_Reg(0X58,0X9E);			//MTXS 
}
//��������
//0:-2
//1:-1
//2,0
//3,1
//4,2
void OV7670_Brightness(u8 bright)
{
	u8 reg55val=0X00;//Ĭ�Ͼ���bright=2
  	switch(bright)
	{
		case 0://-2
			reg55val=0XB0;	 	 
			break;	
		case 1://-1
			reg55val=0X98;	 	 
			break;	
		case 3://1
			reg55val=0X18;	 	 
			break;	
		case 4://2
			reg55val=0X30;	 	 
			break;	
	}
	SCCB_WR_Reg(0X55,reg55val);	//���ȵ��� 
}
//�Աȶ�����
//0:-2
//1:-1
//2,0
//3,1
//4,2
void OV7670_Contrast(u8 contrast)
{
	u8 reg56val=0X40;//Ĭ�Ͼ���contrast=2
  	switch(contrast)
	{
		case 0://-2
			reg56val=0X30;	 	 
			break;	
		case 1://-1
			reg56val=0X38;	 	 
			break;	
		case 3://1
			reg56val=0X50;	 	 
			break;	
		case 4://2
			reg56val=0X60;	 	 
			break;	
	}
	SCCB_WR_Reg(0X56,reg56val);	//�Աȶȵ��� 
}
//��Ч����
//0:��ͨģʽ    
//1,��Ƭ
//2,�ڰ�   
//3,ƫ��ɫ
//4,ƫ��ɫ
//5,ƫ��ɫ
//6,����	    
void OV7670_Special_Effects(u8 eft)
{
	u8 reg3aval=0X04;//Ĭ��Ϊ��ͨģʽ
	u8 reg67val=0XC0;
	u8 reg68val=0X80;
	switch(eft)
	{
		case 1://��Ƭ
			reg3aval=0X24;
			reg67val=0X80;
			reg68val=0X80;
			break;	
		case 2://�ڰ�
			reg3aval=0X14;
			reg67val=0X80;
			reg68val=0X80;
			break;	
		case 3://ƫ��ɫ
			reg3aval=0X14;
			reg67val=0Xc0;
			reg68val=0X80;
			break;	
		case 4://ƫ��ɫ
			reg3aval=0X14;
			reg67val=0X40;
			reg68val=0X40;
			break;	
		case 5://ƫ��ɫ
			reg3aval=0X14;
			reg67val=0X80;
			reg68val=0XC0;
			break;	
		case 6://����
			reg3aval=0X14;
			reg67val=0XA0;
			reg68val=0X40;
			break;	 
	}
	SCCB_WR_Reg(0X3A,reg3aval);//TSLB���� 
	SCCB_WR_Reg(0X68,reg67val);//MANU,�ֶ�Uֵ 
	SCCB_WR_Reg(0X67,reg68val);//MANV,�ֶ�Vֵ 
}	
//����ͼ���������
//��QVGA���á�
void OV7670_Window_Set(u16 sx,u16 sy,u16 width,u16 height)
{
	u16 endx;
	u16 endy;
	u8 temp; 
	endx=sx+width*2;	//V*2
 	endy=sy+height*2;
	if(endy>784)endy-=784;
	temp=SCCB_RD_Reg(0X03);				//��ȡVref֮ǰ��ֵ
	temp&=0XF0;
	temp|=((endx&0X03)<<2)|(sx&0X03);
	SCCB_WR_Reg(0X03,temp);				//����Vref��start��end�����2λ
	SCCB_WR_Reg(0X19,sx>>2);			//����Vref��start��8λ
	SCCB_WR_Reg(0X1A,endx>>2);			//����Vref��end�ĸ�8λ

	temp=SCCB_RD_Reg(0X32);				//��ȡHref֮ǰ��ֵ
	temp&=0XC0;
	temp|=((endy&0X07)<<3)|(sy&0X07);
	SCCB_WR_Reg(0X17,sy>>3);			//����Href��start��8λ
	SCCB_WR_Reg(0X18,endy>>3);			//����Href��end�ĸ�8λ
}

extern u8 ov_sta;	//��exit.c���涨��
//����LCD��ʾ
void camera_refresh(void)
{
	u32 j;
 	u16 color;	 
	if(ov_sta==2)
	{
		LCD_Scan_Dir(U2D_L2R);		//���ϵ���,������ 
		LCD_SetCursor(0x00,0x0000);	//���ù��λ�� 
		LCD_WriteRAM_Prepare();     //��ʼд��GRAM	
		OV7670_RRST=0;				//��ʼ��λ��ָ�� 
		OV7670_RCK=0;
		OV7670_RCK=1;
		OV7670_RCK=0;
		OV7670_RRST=1;				//��λ��ָ����� 
		OV7670_RCK=1;  
		
		for(j=0;j<76800;j++)
		{
					OV7670_RCK=0;
					color = GPIOC->IDR&0XFF;	//������
					OV7670_RCK=1;
					
					color<<=8;
					OV7670_RCK=0;
					color |= GPIOC->IDR&0XFF;	//������
					OV7670_RCK=1;
					
					LCD->LCD_RAM=color;   			

		}   							 
		EXTI->PR=1<<8;     			//���LINE8�ϵ��жϱ�־λ
		ov_sta=0;					//��ʼ��һ�βɼ�
		LCD_Scan_Dir(DFT_SCAN_DIR);	//�ָ�Ĭ��ɨ�跽�� 
	} 
}

u8 picture_send(void)
{
	u16 tx,ty,picnum=0; 	
	char *pst;							//ͼ�����ݰ�
	char temp[20];
	pst=(char*)mymalloc(0,1024);		//��������bi4width��С���ֽڵ��ڴ����� ,��240�����,480���ֽھ͹���.
	if(pst==NULL)return 1;					//�ڴ�����ʧ��.
	
	//TIM4->CR1&=0x00; //�رն�ʱ�� 4 ����Ϊ����ͷ ���ˣ�����Ҫʹ�ö�ʱ��4����ȡ����ͷ�������ˣ����ǲ��ùܶ�ʱ��4��
	printf ( "pciture data:" );
	/* �����ٶ�̫��̫��
	for(ty=0;ty<360;ty+=1)
	{
		for(tx=0;tx<240;tx+=1)
		{
			sprintf(temp,"%x",LCD_ReadPoint(tx,ty));
			strcat(pst,temp);
			picnum++;
			if(picnum==240) 
			{
				picnum=0;
				ESP8266_SendString ( DISABLE,pst,strlen( pst), Multiple_ID_0 );
				ESP8266_SendString ( DISABLE," ",strlen(" "), Multiple_ID_0 );
				pst[0]='\0';
			}
		}
	}
	*/
	for(ty=0;ty<360;ty+=3)
	{
		for(tx=0;tx<240;tx+=3)
		{
			sprintf(temp,"%x",LCD_ReadPoint(tx,ty));
			strcat(pst,temp);
			picnum++;
			if(picnum==240) 
			{
				picnum=0;
				ESP8266_SendString ( DISABLE,pst,strlen( pst), Multiple_ID_0 );
				ESP8266_SendString ( DISABLE," ",strlen(" "), Multiple_ID_0 );
				pst[0]='\0';
			}
		}
	}
	printf ( "send ok" );
	myfree(0,pst);	 	//�ͷ��ڴ�
	//TIM4->CR1|=0x01; //ʹ�ܶ�ʱ�� 4
	return 0;
}



















