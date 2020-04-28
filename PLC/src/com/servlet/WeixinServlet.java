package com.servlet;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

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

		// if ("login".equals(action)) {
		// login(request, response);
		// } else if ("openid".equals(action)) {
		// openid(request, response);
		// } else if ("fand".equals(action)) {
		// fand(request, response);
		// } else if ("num".equals(action)) {
		// num(request, response);
		// } else if ("all".equals(action)) {
		// all(request, response);
		// } else if ("alldingwei".equals(action)) {
		// alldingwei(request, response);
		// }
	}
	//
	// protected void login(HttpServletRequest request, HttpServletResponse
	// response)
	// throws ServletException, IOException {
	// response.setContentType("text/json");
	// response.setCharacterEncoding("utf-8");
	// response.setHeader("Access-Control-Allow-Methods", "GET,POST");
	// String username = request.getParameter("username");
	// String password = request.getParameter("password");
	//
	// User user = new User();
	// user.setName(username);
	// user.setPassword(password);
	// UserDao userDao = new UserDao();
	// userDao.login(user);
	// int a = userDao.login(user);
	// if (a > 0) {
	//
	// System.out.println("username=" + username + " ,password=" + password);
	//
	// // 返回值给微信小程序
	// Writer out = response.getWriter();
	// out.write("进入后台");
	// out.flush();
	//
	// } else {
	//
	// }
	//
	// }
	//
	// protected void all(HttpServletRequest request, HttpServletResponse response)
	// throws ServletException, IOException {
	// response.setContentType("text/json");
	// response.setCharacterEncoding("utf-8");
	// response.setHeader("Access-Control-Allow-Methods", "GET,POST");
	// String openid = request.getParameter("openid");
	//
	// User user = new User();
	// user.setWeixinid(openid);
	// UserDao userDao = new UserDao();
	// userDao.weixinall(user);
	//
	// Map<String, Object> resultMap = new HashMap<String, Object>();
	// resultMap.put("status", user);
	//
	// String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串
	//
	// Writer out = response.getWriter();
	// out.write(resultStr);
	// out.flush();
	//
	// }
	//
	// protected void alldingwei(HttpServletRequest request, HttpServletResponse
	// response)
	// throws ServletException, IOException {
	// response.setContentType("text/json");
	// response.setCharacterEncoding("utf-8");
	// response.setHeader("Access-Control-Allow-Methods", "GET,POST");
	//
	// equipmentDao equipmentDao = new equipmentDao();
	// List<equipment> list = equipmentDao.XinxiAll();
	//
	// Map<String, Object> resultMap = new HashMap<String, Object>();
	// resultMap.put("status", list);
	//
	// String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串
	//
	// Writer out = response.getWriter();
	// out.write(resultStr);
	// out.flush();
	//
	// }
	//
	// protected void openid(HttpServletRequest request, HttpServletResponse
	// response)
	// throws ServletException, IOException {
	// response.setContentType("text/json");
	// response.setCharacterEncoding("utf-8");
	// response.setHeader("Access-Control-Allow-Methods", "GET,POST");
	// String username = request.getParameter("username");
	// String weixinid = request.getParameter("weixinid");
	//
	// User user = new User();
	// user.setName(username);
	// user.setWeixinid(weixinid);
	// UserDao userDao = new UserDao();
	// int a = userDao.updataid(user);
	//
	// if (a > 0) {
	//
	// System.out.println("username=" + username + " ,weixinid=" + weixinid);
	//
	// // 返回值给微信小程序
	// Writer out = response.getWriter();
	// out.write("成功");
	// out.flush();
	//
	// } else {
	//
	// }
	//
	// }
	//
	// protected void fand(HttpServletRequest request, HttpServletResponse response)
	// throws ServletException, IOException {
	// response.setContentType("text/json");
	// response.setCharacterEncoding("utf-8");
	// response.setHeader("Access-Control-Allow-Methods", "GET,POST");
	// String weixinid = request.getParameter("weixinid");
	//
	// User user = new User();
	//
	// user.setWeixinid(weixinid);
	// UserDao userDao = new UserDao();
	// int a = userDao.fand(user);
	//
	// if (a > 0) {
	//
	// System.out.println("weixnid=" + weixinid);
	//
	// // 返回值给微信小程序
	// Writer out = response.getWriter();
	// out.write("存在");
	// out.flush();
	//
	// } else {
	//
	// }
	//
	// }
	//
	// protected void num(HttpServletRequest request, HttpServletResponse response)
	// throws ServletException, IOException {
	// response.setContentType("text/json");
	// response.setCharacterEncoding("utf-8");
	// response.setHeader("Access-Control-Allow-Methods", "GET,POST");
	// equipmentDao equipmentDao = new equipmentDao();
	// List<equipment> list = equipmentDao.XinxiAll();
	// int a = list.size();
	// String s = String.valueOf(a);
	// System.out.println(a);
	// // 返回值给微信小程序
	// Writer out = response.getWriter();
	// out.write(s);
	// out.flush();
	//
	// }
}
