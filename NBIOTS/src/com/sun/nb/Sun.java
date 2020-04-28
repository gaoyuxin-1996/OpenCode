package com.sun.nb;

import java.io.BufferedInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.Base64;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import javax.servlet.Servlet;
import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.lang3.StringUtils;

import com.alibaba.fastjson.JSONObject;
//import com.aliyun.mns.common.ClientException;
import com.aliyun.mns.common.ClientException;
//import com.aliyuncs.DefaultAcsClient;
//import com.aliyuncs.IAcsClient;
///import com.aliyuncs.dyvmsapi.model.v20170525.SingleCallByTtsRequest;
//import com.aliyuncs.dyvmsapi.model.v20170525.SingleCallByTtsResponse;
//import com.aliyuncs.dyvmsapi.model.v20170525.SingleCallByVoiceResponse;
//import com.aliyuncs.profile.DefaultProfile;
//import com.aliyuncs.profile.IClientProfile;
//import com.aliyun.mns.common.ClientException;
import com.huawei.utils.Constant;
import com.huawei.utils.HttpsUtil;
import com.huawei.utils.JsonUtil;
import com.huawei.utils.StreamClosedHttpResponse;
import com.sun.ali.VoiceSmS;
//import com.sun.ali.VoiceSmS;
import com.sun.factory.ServiceFactory;
import com.sun.fastjson.Fj;
import com.sun.fastjson.Info_array_util;
import com.sun.fastjson.Info_data_util;
import com.sun.fastjson.Info_device_util;
import com.sun.fastjson.Info_util;
import com.sun.model.DeviceData;
import com.sun.model.FourMessage;
import com.sun.service.DeviceDataService;
import com.sun.service.FourMessageService;
import com.sun.temp.TempData;

/**
 * Servlet implementation class Sun
 */
@WebServlet("/Sun")
public class Sun extends HttpServlet {
	private static final long serialVersionUID = 1L;

	/**
	 * @see HttpServlet#HttpServlet()
	 */
	public Sun() {
		super();
		// TODO Auto-generated constructor stub
	}

	@Override
	public void init() throws ServletException {
		// TODO Auto-generated method stub
		super.init();
		/*
		 * Voicetask vt0=new Voicetask(0); Thread ts=new Thread(vt0); ts.start();
		 */

	}

	/**
	 * @see Servlet#init(ServletConfig)
	 */
	public void init(ServletConfig config) throws ServletException {
		// TODO Auto-generated method stub
	}

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse
	 *      response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		// TODO Auto-generated method stub
		// response.getWriter().append("Served at: ").append(request.getContextPath());
		/*
		 * Voicetask vt0=new Voicetask(0); Thread ts=new Thread(vt0); ts.start();
		 */
		/*
		 * try { alr(); } catch (com.aliyuncs.exceptions.ClientException e) { // TODO
		 * Auto-generated catch block e.printStackTrace(); }
		 */

	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse
	 *      response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		// TODO Auto-generated method stub
		// doGet(request, response);
		InputStream result = new BufferedInputStream(request.getInputStream());
		ByteArrayOutputStream sout = new ByteArrayOutputStream();
		int b = 0;
		while ((b = result.read()) != -1) {
			sout.write(b);
		}
		byte[] temp = sout.toByteArray();
		String re = new String(temp, "UTF-8");

		JSONObject jtype = new JSONObject();
		jtype = JSONObject.parseObject(re);

		if (jtype.get("notifyType").equals("deviceDataChanged"))
			System.out.println(jtype.get("deviceId"));
		String da = new String();
		String dp = new String();
		try {
			da = QueryDevices(jtype.get("deviceId").toString());
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		dp = Cjdata(da);
		System.out.println(dp);
		// System.out.println(dp.length());
		// // 報警則返回IMEI碼
		// if (dp.length() == 15) {
		// // 报警处理
		// ArrayList<String> ret = perinfo(dp);
		// System.out.println(ret.size());
		// if (ret.size() == 3) {
		// System.out.println("需要报警");
		// // 创建线程池
		// TempData.getInstance().Return_data = ret;
		// ExecutorService es = Executors.newFixedThreadPool(2);
		// System.out.println("创建线程池");
		// /*
		// * Voicetask vt1=new Voicetask(1); Smstask sm0=new Smstask(0); Smstask sm1=new
		// * Smstask(1);
		// */
		// System.out.println("报警中");
		// es.execute(new Voicetask(0));
		// es.execute(new Voicetask(1));
		// es.execute(new Smstask(0));
		// es.execute(new Smstask(1));
		// System.out.println("报警完成");
		// // es.shutdown();
		// } else {
		// System.out.println("用户不存在");
		// }
		// } else {
		// System.out.println("不是报警信息");
		// }

		if (dp.length() == 15) {
			ExecutorService es = Executors.newFixedThreadPool(2);
			es.execute(new VoicetaskFixId(dp, 0));

		}

	}

	@SuppressWarnings("null")
	public String Cjdata(String da) {

		Info_util iu1 = new Info_util();
		Info_device_util du = new Info_device_util();
		Info_data_util fi = new Info_data_util();
		List<Info_array_util> olist = new ArrayList<Info_array_util>();
		List<Map<String, Object>> mlist = new ArrayList<Map<String, Object>>();

		DeviceDataService devicedataservice = ServiceFactory.getDeviceDataService();//
		DeviceData devicedata = new DeviceData();
		TempData.getInstance().Db_name = "db_NBIOT";
		byte[] b64b = null;
		String as = new String();
		/*
		 * 禁止在平台创建多个服务
		 */
		iu1 = Fj.parseObject(da, Info_util.class);
		du = Fj.parseObject(iu1.getDeviceInfo(), Info_device_util.class);
		olist = Fj.parseArray(iu1.getServices(), Info_array_util.class);
		fi = Fj.parseObject(olist.get(0).getData(), Info_data_util.class);
		System.out.println(fi.getData());
		System.out.println("查询数据库1");
		// data不为空进行数据解析和数据库插入
		if (!StringUtils.isEmpty(fi.getData())) {
			System.out.println("查询数据库2");
			b64b = Base64.getDecoder().decode(fi.getData());

			System.out.println(b64b.length);
			// LIGHT,OFF PRESS,OFF ADXL,MOVE
			if (new String(b64b).equals("ADXL,MOVE"))
				return as = "869976030123302";
			if ((new String(b64b).equals("LIGHT,OFF")) || (new String(b64b).equals("PRESS,OFF")))
				return as = "869976030118427";

			System.out.println("测试文物雕塑报警");

			if (b64b.length == 5) {
				System.out.println("查询数据库3");
				TempData.getInstance().Tb_name = "t_Device";
				// 数据库详情
				devicedata.setData(bytesToHexString(b64b));
				System.out.println("查询数据库4");
				devicedata.setDeviceType(du.getDeviceType());

				devicedata.setEventTime(olist.get(0).getEventTime());

				devicedata.setManufacturerId(du.getManufacturerId());
				devicedata.setManufacturerName(du.getManufacturerName());
				devicedata.setModel(du.getModel());
				devicedata.setName(du.getName());
				devicedata.setNodeId(du.getNodeId());
				devicedata.setServiceId(olist.get(0).getServiceId());
				devicedata.setStatus(du.getStatus());
				/*
				 * 查询数据是否存在 参数nodeid 返回boolean
				 */
				// "t_Device"表格
				System.out.println("查询数据库5");
				if ((devicedataservice.isExist(du.getNodeId())) == true) {
					System.out.println("更新数据库");
					devicedataservice.updateDeviceData(devicedata);
				} else {
					System.out.println("数据库内容不存在，插入数据库");
					devicedataservice.addDeviceData(devicedata);
				}
				TempData.getInstance().Tb_name = "t_Datas";
				devicedataservice.addDeviceData(devicedata);
				System.out.println("添加数据库完成6");

				switch ((b64b[2] & 0xff)) {
				case 0:
					System.out.println("正常");
					as = "normal";
					break;
				case 1:
					System.out.println("电池欠压");
					as = "normal";
					break;
				case 2:
					System.out.println("放大器故障");
					as = "normal";
					break;
				case 3:
					System.out.println("灵敏度故障");
					as = "normal";
					break;
				case 4:
					System.out.println("污染");
					as = "normal";
					break;
				case 5:
					System.out.println("预警");
					as = "normal";
					break;
				case 7:
					System.out.println("火警");
					as = du.getNodeId();
					break;
				default:
					break;
				}
			} else
				as = "wrongdata";

		} else
			as = "nodata";

		return as;
	}

	public String QueryDevices(String did) throws Exception {
		// ArrayList<String>ad=null;
		HttpsUtil httpsUtil = new HttpsUtil();
		httpsUtil.initSSLConfigForTwoWay();
		String accessToken = login(httpsUtil);
		String appId = Constant.APPID;
		// String deviceId ="11152517-a193-4dfd-9c1c-b63a19fd1f4d";
		String deviceId = did;
		String urlQueryDeviceData = Constant.QUERY_DEVICE_DATA + "/" + deviceId;
		Map<String, String> paramQueryDeviceData = new HashMap<>();
		paramQueryDeviceData.put("appId", appId);
		Map<String, String> header = new HashMap<>();
		header.put(Constant.HEADER_APP_KEY, appId);
		header.put(Constant.HEADER_APP_AUTH, "Bearer" + " " + accessToken);
		StreamClosedHttpResponse bodyQueryDeviceData = httpsUtil.doGetWithParasGetStatusLine(urlQueryDeviceData,
				paramQueryDeviceData, header);
		/*
		 * System.out.println("QueryDeviceData, response content:");
		 * System.out.print(bodyQueryDeviceData.getStatusLine());
		 * System.out.println(bodyQueryDeviceData.getContent()); System.out.println();
		 */
		/*
		 * iu1=Fj.parseObject(bodyQueryDeviceData.getContent(), Info_util.class);//��һ��
		 * du=Fj.parseObject(iu1.getDeviceInfo(), Info_device_util.class);//�ڶ���
		 * olist=Fj.parseArray(iu1.getServices(), Info_array_util.class);//��һ������
		 * fi=Fj.parseObject(olist.get(0).getData(), Info_data_util.class);
		 * System.out.println(fi.getData()); ad.add(du.getNodeId());
		 */
		return bodyQueryDeviceData.getContent();

	}

	public static String login(HttpsUtil httpsUtil) throws Exception {

		String appId = Constant.APPID;
		String secret = Constant.SECRET;
		String urlLogin = Constant.APP_AUTH;

		Map<String, String> paramLogin = new HashMap<>();
		paramLogin.put("appId", appId);
		paramLogin.put("secret", secret);

		StreamClosedHttpResponse responseLogin = httpsUtil.doPostFormUrlEncodedGetStatusLine(urlLogin, paramLogin);

		System.out.println("app auth success,return accessToken:");
		System.out.print(responseLogin.getStatusLine());
		System.out.println(responseLogin.getContent());
		System.out.println();

		Map<String, String> data = new HashMap<>();
		data = JsonUtil.jsonString2SimpleObj(responseLogin.getContent(), data.getClass());
		return data.get("accessToken");
	}

	public static final String bytesToHexString(byte[] buf) {
		StringBuilder sb = new StringBuilder(buf.length * 2);
		String tmp = "";
		for (int i = 0; i < buf.length; i++) {
			// 1.
			// sb.append(Integer.toHexString((buf[i] & 0xf0) >> 4));
			// sb.append(Integer.toHexString((buf[i] & 0x0f) >> 0));
			// //////////////////////////////////////////////////////////////////
			// 2.sodino��ϲ���ķ�ʽ���ٺ�...
			tmp = Integer.toHexString(0xff & buf[i]);
			tmp = tmp.length() == 1 ? "0" + tmp : tmp;
			sb.append(tmp);
		}
		return sb.toString();
	}

	/*
	 * 功能:查询用户注册信息 参数:nodeid 返回:ArrayList<String>
	 ***/
	public ArrayList<String> perinfo(String nid) {
		FourMessageService fourmessageservice = ServiceFactory.getFourMessageService();
		FourMessage fourmessage = new FourMessage();
		ArrayList<String> person_infomation = new ArrayList<String>();
		TempData.getInstance().Db_name = "testdb";
		TempData.getInstance().Tb_name = "myclass";
		System.out.println(nid);
		if (fourmessageservice.isExist(nid)) {
			System.out.println("exit");
			fourmessage = fourmessageservice.searchFourMessage(nid);
			System.out.println(fourmessage.getLoc());
			// 数据库信息
			person_infomation.add(fourmessage.getPh1());
			person_infomation.add(fourmessage.getPh2());
			person_infomation.add(fourmessage.getLoc());

		} else {

			person_infomation.add("noexit");
		}
		return person_infomation;

	}

	// 主叫号码
	class Voicetask implements Runnable {
		private int di;

		public Voicetask(int di) {

			this.di = di;
		}

		@Override
		public void run() {
			System.out.println("正在报警");
			// TODO Auto-generated method stub

			try {
				VoiceSmS.sce = VoiceSmS.singleCallByTts(TempData.getInstance().Return_data, di);
			} catch (ClientException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (com.aliyuncs.exceptions.ClientException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}

			System.out.println("文本转语音外呼---------------");
			System.out.println("RequestId=" + VoiceSmS.sce.getRequestId());
			System.out.println("Code=" + VoiceSmS.sce.getCode());
			System.out.println("Message=" + VoiceSmS.sce.getMessage());
			System.out.println("CallId=" + VoiceSmS.sce.getCallId());

		}

	}

	//
	class VoicetaskFixId implements Runnable {
		private int di;
		private String nbimei;

		public VoicetaskFixId(String nbimei, int di) {
			this.nbimei = nbimei;
			this.di = di;
		}

		@Override
		public void run() {
			System.out.println("文物或者雕塑报警");
			// TODO Auto-generated method stub
			try {
				// VoiceSmS.sce = VoiceSmS.singleCallByTts(TempData.getInstance().Return_data,
				// di);
				VoiceSmS.sce = VoiceSmS.singleCallByTtsMd(nbimei, di);
			} catch (ClientException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (com.aliyuncs.exceptions.ClientException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}

			System.out.println("文本转语音外呼---------------");
			System.out.println("RequestId=" + VoiceSmS.sce.getRequestId());
			System.out.println("Code=" + VoiceSmS.sce.getCode());
			System.out.println("Message=" + VoiceSmS.sce.getMessage());
			System.out.println("CallId=" + VoiceSmS.sce.getCallId());

		}

	}

	// 备用号码
	class Smstask implements Runnable {
		private int di;

		public Smstask(int di) {
			this.di = di;
		}

		@Override
		public void run() {
			// TODO Auto-generated method stub
			try {
				VoiceSmS.sse = VoiceSmS.sendSms(TempData.getInstance().Return_data, di);

			} catch (ClientException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (com.aliyuncs.exceptions.ClientException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}

		}

	}
	// 单独调试成功 注意endpoint
	/*
	 * public SingleCallByTtsResponse alr() throws
	 * com.aliyuncs.exceptions.ClientException { final String product = "Dyvmsapi";
	 * final String domain = "dyvmsapi.aliyuncs.com"; // final String accessKeyId =
	 * "LTAIXeNeCTwseDR7"; final String accessKeySecret =
	 * "fgXXaco10xxjKxKcyzoT0d1Lx2to8x"; //public static SingleCallByTtsResponse
	 * sce; System.setProperty("sun.net.client.defaultConnectTimeout", "10000");
	 * System.setProperty("sun.net.client.defaultReadTimeout", "10000");
	 * //��ʼ��acsClient,�ݲ�֧��region�� IClientProfile profile =
	 * DefaultProfile.getProfile("cn-hangzhou", accessKeyId, accessKeySecret);
	 * DefaultProfile.addEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
	 * IAcsClient acsClient = new DefaultAcsClient(profile);
	 * //��װ�������-��������������̨-�ĵ��������� SingleCallByTtsRequest request = new
	 * SingleCallByTtsRequest();
	 * 
	 * //����-�����Ժ�,������������̨���ҵ���������Ժ�
	 * request.setCalledShowNumber("053288983097"); //����-���к���
	 * //TempData.getInstance().Return_data.get(1)//���к���
	 * request.setCalledNumber("15689103279");
	 * //request.setCalledNumber("15000000000"); //����-Ttsģ��ID
	 * request.setTtsCode("TTS_107815090"); //��ѡ-��ģ���д��ڱ���ʱ��Ҫ���ô�ֵloc //
	 * request.setTtsParam("{\"name\":"+pnumber.get(2)+"}");
	 * request.setTtsParam("{\"name\":\"test\"}");
	 * //��ѡ-�ⲿ��չ�ֶ�,��ID���ڻ�ִ��Ϣ�д��ظ����÷� request.setOutId("123"); //hint
	 * �˴����ܻ��׳��쳣��ע��catch SingleCallByTtsResponse singleCallByTtsResponse =
	 * acsClient.getAcsResponse(request);
	 * 
	 * return singleCallByTtsResponse; }
	 */

}
