using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Test_console
{
    class PPP
    {
        const Int32 CMaxSendDataNum = 6;    //CmdLen +1//
        const Int32 CMaxSendByteNum = 18;   //(CMaxSendDataNum+2)*2+2//
        const Byte DownCmd = 0x00;
        const Byte DownRsp = 0x40;

        SerialPort sp;

        public UInt32 SendPack(Byte FrmType, Byte[] Send_data, int SendByteNum)
        {
            int ToalSendByteNum, UartSendByteNum=0, PPPSendByteNum, Index;
            Byte[] UART_Sendbuffer = new Byte[CMaxSendByteNum];
            Byte[] PPP_Buffer = new Byte[CMaxSendDataNum];

            ToalSendByteNum = SendByteNum;

            //Receiver receive data according to the same principle///
            switch (FrmType)
            {
                case DownCmd:
                    PPP_Buffer[0] = FrmType;
                    ToalSendByteNum++;
                    UartSendByteNum = (SendByteNum + 3) * 2 + 2;
                    break;
                case DownRsp:
                    PPP_Buffer[0] = FrmType;
                    ToalSendByteNum++;
                    UartSendByteNum = (SendByteNum + 3) * 2 + 2;
                    break;
                default: break;
            }

            for (Index = 0; Index < Send_data.Length; Index++)
            {
                PPP_Buffer[Index+1] = Send_data[Index];
            }

            PPPSendByteNum = ProcPPPSend(PPP_Buffer, ToalSendByteNum + 2, UART_Sendbuffer);

            sp.Write(UART_Sendbuffer, 0, UartSendByteNum);

            return 0;
        }




     

        public int ProcPPPSend(Byte[] In_buffer, int len, Byte[] Out_buffer)
        {


            return 0;
        }






    }
}
