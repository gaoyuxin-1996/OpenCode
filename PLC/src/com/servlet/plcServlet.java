package com.servlet;

import java.io.IOException;
import java.io.Writer;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import com.dao.plcDao;
import com.item.equipment;

import net.sf.json.JSONObject;

@WebServlet(name = "plcServlet", urlPatterns = "/plcServlet")
public class plcServlet extends HttpServlet {

	private static final long serialVersionUID = 1L;

	protected void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		doPost(request, response);
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		String action = request.getParameter("action");
		if ("plcname".equals(action)) {
			plcname(request, response);
		} else if ("all".equals(action)) {
			all(request, response);
		} else if ("add".equals(action)) {
			add(request, response);
		} else if ("update".equals(action)) {
			update(request, response);
		} else if ("delete".equals(action)) {
			delete(request, response);
		} else if ("xiangqing".equals(action)) {
			xiangqing(request, response);
		}
	}

	protected void xiangqing(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");
		equipment equipment = new equipment();
		equipment.setPlcid(Integer.valueOf(request.getParameter("SB_no")));
		System.out.println(equipment.getPlcid());
		plcDao plcDao = new plcDao();
		plcDao.plcbyid1(equipment);
		request.setAttribute("equipment", equipment);
		Map<String, Object> resultMap = new HashMap<String, Object>();
		resultMap.put("status", equipment);
		String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串
		System.out.println(resultStr);
		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();

	}

	protected void delete(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		String aString = request.getParameter("SB_no_d");
		System.out.println(aString);
		plcDao plcDao = new plcDao();
		int c = plcDao.del(aString);
		Map<String, Object> resultMap = new HashMap<String, Object>();

		if (c == 1) {

			resultMap.put("status", "删除成功");

		} else {
			resultMap.put("status", "删除失败");
		}
		String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串
		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();
	}

	protected void plcname(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");
		plcDao plcDao = new plcDao();
		List<String> list = plcDao.plcname();
		Map<String, Object> resultMap = new HashMap<String, Object>();
		resultMap.put("status", list);
		String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串
		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();

	}

	protected void all(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		HttpSession session = request.getSession();
		String aString = (String) session.getAttribute("username");
		if (aString == null) {

			request.getRequestDispatcher("Login.jsp").forward(request, response);
		} else {
			System.out.println("all");
			plcDao plcDao = new plcDao();
			List<equipment> list = plcDao.plcall();
			request.setAttribute("list", list);
			request.getRequestDispatcher("plc.jsp").forward(request, response);
		}

	}

	protected void add(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		System.out.println("add");
		equipment equipment = new equipment();
		equipment.setPlcxilie(request.getParameter("SB_type"));
		plcDao plcDao = new plcDao();
		equipment = plcDao.plcbyid(equipment);
		HttpSession session = request.getSession();
		equipment.setName(request.getParameter("SB_company"));
		equipment.setCategory(request.getParameter("SB_add"));
		equipment.setPlcbrand(request.getParameter("SB_no"));
		equipment.setWay1(request.getParameter("SB_tel"));
		equipment.setCreatename((String) session.getAttribute("username"));
		System.out.println((String) session.getAttribute("username"));
		equipment.setTrigger(request.getParameter("SB_chufa"));
		equipment.setOrdinary(request.getParameter("SB_putong"));
		equipment.setStorage(request.getParameter("SB_cunchu"));
		String a = request.getParameter("SB_x");
		String c = request.getParameter("SB_y");
		equipment.setXilie(a);
		equipment.setVendor(c);
		equipment.setAgreement(request.getParameter("SB_name"));
		SimpleDateFormat df = new SimpleDateFormat("yyyy-MM-dd-HH:mm.ss");
		equipment.setDate(df.format(new Date()));
		int e = plcDao.insert(equipment);
		Map<String, Object> resultMap = new HashMap<String, Object>();

		if (e == 1) {
			request.setAttribute("e", "aa");
			resultMap.put("status", "添加成功");

		} else {
			resultMap.put("status", "添加失败");
		}
		String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串
		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();
	}

	protected void update(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		equipment equipment = new equipment();
		HttpSession session = request.getSession();
		equipment.setPlcxilie(request.getParameter("SB_type_e"));
		plcDao plcDao = new plcDao();
		equipment = plcDao.plcbyid(equipment);
		equipment.setName(request.getParameter("SB_company_e"));
		equipment.setPlcid(Integer.valueOf(request.getParameter("SB_id_e")));
		equipment.setCategory(request.getParameter("SB_no_e"));
		equipment.setPlcbrand(request.getParameter("SB_y_e"));
		equipment.setWay1(request.getParameter("SB_tel_e"));
		equipment.setUpdatename((String) session.getAttribute("username"));
		equipment.setTrigger(request.getParameter("SB_chufa_e"));
		equipment.setOrdinary(request.getParameter("SB_putong_e"));
		equipment.setStorage(request.getParameter("SB_cunchu_e"));
		String a = request.getParameter("SB_add_e");
		String c = request.getParameter("SB_x_e");
		equipment.setXilie(a);
		equipment.setVendor(c);
		equipment.setAgreement(request.getParameter("SB_name_e"));
		int e = plcDao.update(equipment);
		Map<String, Object> resultMap = new HashMap<String, Object>();

		if (e == 1) {
			resultMap.put("status", "修改成功");

		} else {
			resultMap.put("status", "修改失败");
		}
		String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串
		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();
	}
}
