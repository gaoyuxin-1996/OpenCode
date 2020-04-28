package com.servlet;

import java.io.IOException;
import java.io.Writer;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Random;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.http.HttpResponse;
import org.apache.http.util.EntityUtils;

import com.dao.UserDao;
import com.dao.equipmentDao;
import com.item.User;
import com.item.equipment;
import com.wgh.filter.HttpUtils;

import net.sf.json.JSONArray;
import net.sf.json.JSONObject;

@WebServlet(name = "WeixinServlet", urlPatterns = "/WeixinServlet")
public class WeixinServlet extends HttpServlet {

	private static final long serialVersionUID = 1L;

	public WeixinServlet() {
		super();

	}

	protected void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		doPost(request, response);
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		String action = request.getParameter("action");

		if ("login".equals(action)) {
			login(request, response);
		} else if ("fand".equals(action)) {
			fand(request, response);
		} else if ("num".equals(action)) {
			num(request, response);
		} else if ("all".equals(action)) {
			all(request, response);
		} else if ("duanxin".equals(action)) {
			duanxin(request, response);
		} else if ("fand1".equals(action)) {
			fand1(request, response);
		} else if ("xiangqing".equals(action)) {
			xiangqing(request, response);
		} else if ("userxinxi".equals(action)) {
			userxinxi(request, response);
		} else if ("update".equals(action)) {
			update(request, response);
		} else if ("ceshi".equals(action)) {
			ceshi(request, response);
		}
	}

	protected void login(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");
		String openid = request.getParameter("openid");
		String ph = request.getParameter("ph");

		User user = new User();
		user.setWeixinid(openid);
		user.setPh1(ph);
		UserDao userDao = new UserDao();
		Writer out = response.getWriter();

		int a = userDao.bangdingph1(user);
		int b = userDao.bangdingph2(user);
		System.out.println(a);
		System.out.println(b);
		if (a + b != 0) {

			out.write("允许登陆");
			out.flush();

		}

	}

	protected void update(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");
		String ph1 = request.getParameter("ph1");
		String ph = request.getParameter("ph");

		User user = new User();
		user.setPh2(ph1);
		user.setPh1(ph);
		UserDao userDao = new UserDao();
		Writer out = response.getWriter();

		int a = userDao.updataph(user);
		int b = userDao.updataph1(user);

		System.out.println(a);
		System.out.println(b);
		if (a + b != 0) {

			out.write("修改成功");
			out.flush();

		}

	}

	protected void all(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");

		equipmentDao equipmentDao = new equipmentDao();
		UserDao userDao = new UserDao();
		User user = new User();
		user.setWeixinid(request.getParameter("openid"));
		List<User> list = userDao.weixinall(user);
		List<String> list2 = new ArrayList<String>();
		int a = list.size();
		for (int i = 0; i < a; i++) {
			String id = list.get(i).getImei();
			list2.add(id);
		}
		List<equipment> list3 = equipmentDao.XinxiAll(list2);
		for (int i = 0; i < list3.size(); i++) {
			String state = list3.get(i).getData();
			String baojing = state.substring(4, 6);
			String time = list3.get(i).getEventTime();
			String year = time.substring(0, 4);
			String month = time.substring(4, 6);
			String data = time.substring(6, 8);
			String hour = time.substring(9, 11);
			String points = time.substring(11, 13);
			String seconds = time.substring(13, 15);
			list3.get(i).setEventTime(year + "-" + month + "-" + data + "-" + hour + ":" + points + "." + seconds);
			int b = Integer.parseInt(baojing);
			if (b == 07) {
				list3.get(i).setData("火警");
			} else if (b == 01) {
				list3.get(i).setData("低电");
			} else {
				list3.get(i).setData("正常");
			}
		}
		System.out.println(list3.get(0).getData());
		Map<String, Object> resultMap = new HashMap<String, Object>();
		resultMap.put("allnum", list3);

		String resultStr = JSONObject.fromObject(resultMap).toString();

		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();

	}

	protected void userxinxi(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");

		UserDao userDao = new UserDao();
		User user = new User();
		user.setWeixinid(request.getParameter("openid"));
		List<User> list = userDao.weixinall(user);
		Map<String, Object> resultMap = new HashMap<String, Object>();
		resultMap.put("allnum", list.get(0));

		String resultStr = JSONObject.fromObject(resultMap).toString();

		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();

	}

	protected void xiangqing(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");

		UserDao userDao = new UserDao();
		User user = new User();
		user.setImei(request.getParameter("nodeId"));
		user = userDao.fandxiangqing(user);
		Map<String, Object> resultMap = new HashMap<String, Object>();
		resultMap.put("xiangqing", user);

		String resultStr = JSONObject.fromObject(resultMap).toString();

		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();

	}

	protected void fand(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");
		String weixinid = request.getParameter("weixinid");

		User user = new User();

		user.setWeixinid(weixinid);

		UserDao userDao = new UserDao();
		int a = userDao.fand(user);

		if (a > 0) {

			System.out.println("weixnid=" + weixinid);

			Writer out = response.getWriter();
			out.write("存在");
			out.flush();

		} else {

		}

	}

	protected void fand1(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");
		String ph1 = request.getParameter("ph");

		User user = new User();

		user.setPh1(ph1);

		UserDao userDao = new UserDao();
		int a = userDao.select(user);
		System.out.println(a);
		Writer out = response.getWriter();
		if (a == 0) {

			out.write("不存在");
			out.flush();

		} else if (a == 1) {
			out.write("1");
			out.flush();
		} else {
			out.write("2");
			out.flush();
		}
	}

	protected void num(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");

		equipmentDao equipmentDao = new equipmentDao();
		UserDao userDao = new UserDao();
		User user = new User();
		user.setWeixinid(request.getParameter("openid"));
		List<User> list = userDao.weixinall(user);
		List<String> list2 = new ArrayList<String>();
		int a = list.size();
		int bnum = 0;
		int qnum = 0;
		for (int i = 0; i < a; i++) {
			String id = list.get(i).getImei();
			list2.add(id);
		}
		List<equipment> list3 = equipmentDao.XinxiAll(list2);
		for (int i = 0; i < list3.size(); i++) {
			String state = list3.get(i).getData();
			String baojing = state.substring(4, 6);
			System.out.println(baojing);
			int b = Integer.parseInt(baojing);
			if (b == 07) {
				bnum = bnum + 1;
			} else if (b == 01) {
				qnum = qnum + 1;
			}
		}

		Writer out = response.getWriter();
		String b = String.valueOf(bnum);
		String q = String.valueOf(qnum);
		String s = String.valueOf(a);
		Map<String, Object> resultMap = new HashMap<String, Object>();
		resultMap.put("allnum", s);
		resultMap.put("baojing", b);
		resultMap.put("quedian", q);

		String resultStr = JSONObject.fromObject(resultMap).toString();
		out.write(resultStr);
		out.flush();

	}

	protected void duanxin(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");
		int radomInt = new Random().nextInt(999999);
		String phone = request.getParameter("phone");
		String code = String.valueOf(radomInt);
		String host = "https://fesms.market.alicloudapi.com";
		String path = "/sms/";
		String method = "GET";
		String appcode = "18714613cdb5456f8206ddcb29576aa9";
		Map<String, String> headers = new HashMap<String, String>();
		// 最后在header中的格式(中间是英文空格)为Authorization:APPCODE 83359fd73fe94948385f570e3c139105
		headers.put("Authorization", "APPCODE " + appcode);
		Map<String, String> querys = new HashMap<String, String>();
		querys.put("code", code);
		querys.put("phone", phone);
		querys.put("skin", "1");
		querys.put("sign", "1");
		// JDK 1.8示例代码请在这里下载： http://code.fegine.com/Tools.zip
		try {
			/*
			 * 相应的依赖请参照
			 * https://github.com/aliyun/api-gateway-demo-sign-java/blob/master/pom.xml
			 * 相关jar包（非pom）直接下载： http://code.fegine.com/aliyun-jar.zip
			 */
			HttpResponse response1 = HttpUtils.doGet(host, path, method, headers, querys);
			// System.out.println(response.toString());如不输出json, 请打开这行代码，打印调试头部状态码。
			// 状态码: 200 正常；400 URL无效；401 appCode错误； 403 次数用完； 500 API网管错误
			// 获取response的body
			System.out.println(EntityUtils.toString(response1.getEntity()));
		} catch (Exception e) {
			e.printStackTrace();
		}
		Writer out = response.getWriter();
		out.write(code);
		out.flush();
	}

	protected void ceshi(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");
		String host = "http://api.map.baidu.com";
		String path = "/geoconv/v1/";
		String method = "GET";
		String ak = "iD2gwtGfo1p98lPenidUyx8h";
		String zuobiao = "114.21892734521,29.575429778924";
		Map<String, String> headers = new HashMap<String, String>();
		// 最后在header中的格式(中间是英文空格)为Authorization:APPCODE 83359fd73fe94948385f570e3c139105
		headers.put("Authorization", "APPCODE " + ak);
		Map<String, String> querys = new HashMap<String, String>();
		querys.put("coords", zuobiao);
		querys.put("from", "3");
		querys.put("to", "5");
		querys.put("ak", "iD2gwtGfo1p98lPenidUyx8h");
		// JDK 1.8示例代码请在这里下载： http://code.fegine.com/Tools.zip
		try {
			/*
			 * 相应的依赖请参照
			 * https://github.com/aliyun/api-gateway-demo-sign-java/blob/master/pom.xml
			 * 相关jar包（非pom）直接下载： http://code.fegine.com/aliyun-jar.zip
			 */
			HttpResponse response1 = HttpUtils.doGet(host, path, method, headers, querys);
			// System.out.println(response.toString());如不输出json, 请打开这行代码，打印调试头部状态码。
			// 状态码: 200 正常；400 URL无效；401 appCode错误； 403 次数用完； 500 API网管错误
			// 获取response的body

			JSONObject jsonObject = JSONObject.fromObject(EntityUtils.toString(response1.getEntity()));

			JSONArray json = JSONArray.fromObject(jsonObject.get("result"));

			jsonObject = JSONObject.fromObject(json.get(0));
			System.out.println(jsonObject.get("x"));
			System.out.println(jsonObject.get("y"));
		} catch (Exception e) {
			e.printStackTrace();
		}
		Writer out = response.getWriter();
		out.write(response.getStatus());
		out.flush();
	}
}
