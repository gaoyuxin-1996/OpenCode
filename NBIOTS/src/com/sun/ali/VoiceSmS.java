package com.sun.ali;

//������ƽ̨�����Ϣ
//
import java.util.ArrayList;

import com.alibaba.fastjson.JSONObject;
import com.aliyun.mns.common.ClientException;
import com.aliyuncs.DefaultAcsClient;
import com.aliyuncs.IAcsClient;
import com.aliyuncs.dysmsapi.model.v20170525.SendSmsRequest;
import com.aliyuncs.dysmsapi.model.v20170525.SendSmsResponse;
import com.aliyuncs.dyvmsapi.model.v20170525.SingleCallByTtsRequest;
import com.aliyuncs.dyvmsapi.model.v20170525.SingleCallByTtsResponse;
import com.aliyuncs.profile.DefaultProfile;
import com.aliyuncs.profile.IClientProfile;

public class VoiceSmS {
	// static final String product = "Dysmsapi";
	// static final String domain = "dysmsapi.aliyuncs.com";

	static final String accessKeyId = "LTAIXeNeCTwseDR7";
	static final String accessKeySecret = "fgXXaco10xxjKxKcyzoT0d1Lx2to8x";
	public static SingleCallByTtsResponse sce;
	public static SendSmsResponse sse;

	//
	public static SingleCallByTtsResponse singleCallByTtsMd(String NB_IMEI, int ind)
			throws ClientException, com.aliyuncs.exceptions.ClientException {
		final String product = "Dyvmsapi";
		final String domain = "dyvmsapi.aliyuncs.com";
		System.setProperty("sun.net.client.defaultConnectTimeout", "10000");
		System.setProperty("sun.net.client.defaultReadTimeout", "10000");
		IClientProfile profile = DefaultProfile.getProfile("cn-hangzhou", accessKeyId, accessKeySecret);
		DefaultProfile.addEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
		IAcsClient acsClient = new DefaultAcsClient(profile);
		SingleCallByTtsRequest request = new SingleCallByTtsRequest();
		request.setCalledShowNumber("053288983097");
		// 判断IMEI
		// 设置呼叫号码sun
		request.setCalledNumber("13668883025");
		if (NB_IMEI.equals("869976030118427")) {
			request.setTtsCode("TTS_152514255");
		}
		if (NB_IMEI.equals("869976030123302")) {
			request.setTtsCode("TTS_152514260");
		}

		SingleCallByTtsResponse singleCallByTtsResponse = acsClient.getAcsResponse(request);
		return singleCallByTtsResponse;
	}

	public static SingleCallByTtsResponse singleCallByTts(ArrayList<String> pnumber, int ind)
			throws ClientException, com.aliyuncs.exceptions.ClientException {
		final String product = "Dyvmsapi";
		final String domain = "dyvmsapi.aliyuncs.com";
		// ������������ʱʱ��
		System.setProperty("sun.net.client.defaultConnectTimeout", "10000");
		System.setProperty("sun.net.client.defaultReadTimeout", "10000");
		// ��ʼ��acsClient,�ݲ�֧��region��
		IClientProfile profile = DefaultProfile.getProfile("cn-hangzhou", accessKeyId, accessKeySecret);
		DefaultProfile.addEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
		IAcsClient acsClient = new DefaultAcsClient(profile);
		// ��װ�������-��������������̨-�ĵ���������
		SingleCallByTtsRequest request = new SingleCallByTtsRequest();
		// ����-�����Ժ�,������������̨���ҵ���������Ժ�
		request.setCalledShowNumber("053288983097");
		// ����-���к���
		// TempData.getInstance().Return_data.get(1)//���к���
		request.setCalledNumber(pnumber.get(ind));
		System.out.println(pnumber.get(ind));
		// request.setCalledNumber("15000000000");
		// ����-Ttsģ��ID
		request.setTtsCode("TTS_107815090");
		// ��ѡ-��ģ���д��ڱ���ʱ��Ҫ���ô�ֵloc
		// request.setTtsParam("{\"name\":"+pnumber.get(2)+"}");
		JSONObject jpe = new JSONObject();
		jpe.put("name", pnumber.get(2));
		// jpe.toString()
		request.setTtsParam(jpe.toJSONString());
		// request.setTtsParam("{\"name\":\"test\"}");
		// ��ѡ-�ⲿ��չ�ֶ�,��ID���ڻ�ִ��Ϣ�д��ظ����÷�
		request.setOutId("123");
		// hint �˴����ܻ��׳��쳣��ע��catch
		SingleCallByTtsResponse singleCallByTtsResponse = acsClient.getAcsResponse(request);
		return singleCallByTtsResponse;
	}

	public static SendSmsResponse sendSms(ArrayList<String> pnumber, int ind)
			throws ClientException, com.aliyuncs.exceptions.ClientException {
		final String product = "Dysmsapi";
		final String domain = "dysmsapi.aliyuncs.com";
		// ������������ʱʱ��
		System.setProperty("sun.net.client.defaultConnectTimeout", "10000");
		System.setProperty("sun.net.client.defaultReadTimeout", "10000");

		// ��ʼ��acsClient,�ݲ�֧��region��
		IClientProfile profile = DefaultProfile.getProfile("cn-hangzhou", accessKeyId, accessKeySecret);
		DefaultProfile.addEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
		IAcsClient acsClient = new DefaultAcsClient(profile);

		// ��װ�������-��������������̨-�ĵ���������
		SendSmsRequest request = new SendSmsRequest();
		// ����:�������ֻ���
		// ��Ҫ��д
		// TempData.getInstance().Return_data.get(0)
		// request.setPhoneNumbers("15000000000");
		request.setPhoneNumbers(pnumber.get(ind));
		System.out.println(pnumber.get(ind));
		// ����:����ǩ��-���ڶ��ſ���̨���ҵ���
		request.setSignName("阿里云短信测试专用");
		// ����:����ģ��-���ڶ��ſ���̨���ҵ�
		request.setTemplateCode("SMS_133965137");
		// ��ѡ:ģ���еı����滻JSON��,��ģ������Ϊ"�װ���${name},������֤��Ϊ${code}"ʱ,�˴���ֵΪ
		// php��ͬ
		JSONObject jpe = new JSONObject();
		jpe.put("name", pnumber.get(2));
		// request.setTemplateParam("{\"name\":\"Tom\", \"code\":\"123\"}");

		request.setTemplateParam(jpe.toJSONString());
		// ѡ��-���ж�����չ��(�����������û�����Դ��ֶ�)
		// request.setSmsUpExtendCode("90997");

		// ��ѡ:outIdΪ�ṩ��ҵ����չ�ֶ�,�����ڶ��Ż�ִ��Ϣ�н���ֵ���ظ�������
		request.setOutId("yourOutId");

		// hint �˴����ܻ��׳��쳣��ע��catch
		SendSmsResponse sendSmsResponse = acsClient.getAcsResponse(request);

		return sendSmsResponse;
	}

}
