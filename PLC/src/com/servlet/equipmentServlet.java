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

import com.dao.equipmentDao;
import com.dao.plcDao;
import com.item.equipment;

import net.sf.json.JSONObject;

@WebServlet(name = "equipmentServlet", urlPatterns = "/equipmentServlet")
public class equipmentServlet extends HttpServlet {

	private static final long serialVersionUID = 2831681681692282221L;

	protected void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		doPost(request, response);
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		String action = request.getParameter("action");
		if ("all".equals(action)) {
			all(request, response);
		} else if ("delete".equals(action)) {
			del(request, response);
		} else if ("add".equals(action)) {
			add(request, response);
		} else if ("update".equals(action)) {
			update(request, response);
		} else if ("xiangqing".equals(action)) {
			xiangqing(request, response);
		} else if ("fenbu".equals(action)) {
			fenbu(request, response);
		} else if ("num".equals(action)) {
			num(request, response);
		} else if ("companyall".equals(action)) {
			companyall(request, response);
		} else if ("companygroup".equals(action)) {
			companygroup(request, response);
		} else if ("del".equals(action)) {
			del(request, response);
		} else if ("guankong".equals(action)) {
			guankong(request, response);
		} else if ("guankonginfo".equals(action)) {
			guankonginfo(request, response);
		} else if ("jiankong".equals(action)) {
			jiankong(request, response);
		} else if ("xiangqing1".equals(action)) {
			xiangqing1(request, response);
		} else if ("xiangqing2".equals(action)) {
			xiangqing2(request, response);
		} else if ("xiangqing3".equals(action)) {
			xiangqing3(request, response);
		} else if ("idall".equals(action)) {
			idall(request, response);
		} else if ("bangwang".equals(action)) {
			bangwang(request, response);
		}

	}

	protected void jiankong(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		HttpSession session = request.getSession();
		String aString = (String) session.getAttribute("username");
		if (aString == null) {

			request.getRequestDispatcher("Login.jsp").forward(request, response);
		} else {

			request.setAttribute("SB_no", request.getParameter("SB_no"));
			request.getRequestDispatcher("monitoring.jsp").forward(request, response);
		}

	}

	protected void idall(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		System.out.println("idal");
		equipmentDao equipmentDao = new equipmentDao();
		List<equipment> list = equipmentDao.XinxiAll1();
		Map<String, Object> resultMap = new HashMap<String, Object>();
		resultMap.put("status", list);
		String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串
		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();

	}

	protected void bangwang(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		System.out.println("bangwnag");
		equipmentDao equipmentDao = new equipmentDao();
		equipment equipment = new equipment();
		equipment.setId(Integer.valueOf(request.getParameter("st_shebei")));
		equipment.setGatewayid(request.getParameter("st_wangguan"));
		equipment.setGatewayname(Integer.valueOf(request.getParameter("st_tongdao")));
		int c = equipmentDao.updatewangguan(equipment);
		Map<String, Object> resultMap = new HashMap<String, Object>();

		if (c == 1) {

			resultMap.put("status", "绑定成功");

		} else {
			resultMap.put("status", "绑定失败");
		}
		String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串
		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();
	}

	protected void num(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		equipmentDao equipmentDao = new equipmentDao();
		List<equipment> list = equipmentDao.XinxiAll();
		int a = list.size();
		request.setAttribute("num", a);
		request.getRequestDispatcher("NewFile.jsp").forward(request, response);

	}

	protected void guankonginfo(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		request.setAttribute("SB_no", request.getParameter("SB_no"));
		request.getRequestDispatcher("equipmentServlet?action=guankong").forward(request, response);

	}

	protected void guankong(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		HttpSession session = request.getSession();
		String aString = (String) session.getAttribute("username");
		if (aString == null) {

			request.getRequestDispatcher("Login.jsp").forward(request, response);
		} else {
			equipmentDao equipmentDao = new equipmentDao();
			List<equipment> list = equipmentDao.XinxiAll();
			request.setAttribute("list", list);
			request.setAttribute("SB_no", request.getParameter("SB_no"));
			request.getRequestDispatcher("controller.jsp").forward(request, response);
		}
	}

	protected void xiangqing(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		HttpSession session = request.getSession();
		equipment equipment = new equipment();
		equipment.setId(Integer.valueOf(request.getParameter("SB_no")));
		equipmentDao equipmentDao = new equipmentDao();
		equipmentDao.Xinxi(equipment);
		request.setAttribute("equipment", equipment);
		request.getRequestDispatcher("xiangqing.jsp").forward(request, response);
		System.out.println("d");
	}

	protected void xiangqing3(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		HttpSession session = request.getSession();
		equipment equipment = new equipment();
		equipment.setPlcid(Integer.valueOf(request.getParameter("SB_no")));
		equipmentDao equipmentDao = new equipmentDao();
		List<String> list = equipmentDao.Xinxi1(equipment);
		Map<String, Object> resultMap = new HashMap<String, Object>();
		resultMap.put("status", list);

		String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串

		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();
	}

	protected void xiangqing1(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");
		HttpSession session = request.getSession();
		equipment equipment = new equipment();
		int r = Integer.parseInt(request.getParameter("SB_no"));
		equipment.setId(r);
		equipmentDao equipmentDao = new equipmentDao();
		equipmentDao.Xinxi(equipment);
		request.setAttribute("equipment", equipment);
		Map<String, Object> resultMap = new HashMap<String, Object>();
		resultMap.put("status", equipment);

		String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串

		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();

	}

	protected void xiangqing2(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");
		HttpSession session = request.getSession();
		equipment equipment = new equipment();
		equipment.setName(request.getParameter("SB_no"));
		equipmentDao equipmentDao = new equipmentDao();
		equipmentDao.Xinxi1(equipment);
		request.setAttribute("equipment", equipment);
		Map<String, Object> resultMap = new HashMap<String, Object>();
		resultMap.put("status", equipment);

		String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串

		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();

	}

	protected void companygroup(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");
		equipmentDao equipmentDao = new equipmentDao();
		Map<String, Object> resultMap = new HashMap<String, Object>();
		List<equipment> list;

		list = equipmentDao.Xinxibycompany(request.getParameter("SB_no"));
		resultMap.put("status", list);

		String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串
		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();

	}

	protected void companyall(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		System.out.println("compayall");
		response.setContentType("text/json");
		response.setCharacterEncoding("utf-8");
		response.setHeader("Access-Control-Allow-Methods", "GET,POST");
		equipmentDao equipmentDao = new equipmentDao();
		Map<String, Object> resultMap = new HashMap<String, Object>();
		List<String> list = equipmentDao.Xinxiplcid();
		resultMap.put("status", list);

		String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串
		Writer out = response.getWriter();
		out.write(resultStr);
		out.flush();

	}

	protected void fenbu(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		HttpSession session = request.getSession();
		System.out.println((String) session.getAttribute("username"));
		String aString = (String) session.getAttribute("username");
		System.out.println("f");
		if (aString == null) {
			request.getRequestDispatcher("Login.jsp").forward(request, response);
		} else {
			equipmentDao equipmentDao = new equipmentDao();
			List<equipment> list = equipmentDao.XinxiAll();
			request.setAttribute("list", list);
			request.getRequestDispatcher("Fenbu.jsp").forward(request, response);
		}

	}

	protected void all(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		HttpSession session = request.getSession();
		String aString = (String) session.getAttribute("username");
		if (aString == null) {

			request.getRequestDispatcher("Login.jsp").forward(request, response);
		} else {
			System.out.println("all");
			equipmentDao equipmentDao = new equipmentDao();
			List<equipment> list = equipmentDao.XinxiAll();
			request.setAttribute("list", list);
			request.getRequestDispatcher("Dangan.jsp").forward(request, response);
		}

	}

	protected void del(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

		String aString = request.getParameter("SB_no_d");
		equipmentDao equipmentDao = new equipmentDao();
		int c = equipmentDao.del(aString);
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

	protected void add(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		System.out.println("add");
		equipment equipment = new equipment();
		equipment.setPlcid(Integer.valueOf(request.getParameter("SB_type")));
		equipmentDao equipmentDao = new equipmentDao();
		plcDao plcDao = new plcDao();
		equipment = plcDao.plcbyid(equipment);
		HttpSession session = request.getSession();
		equipment.setName(request.getParameter("SB_company"));
		equipment.setAddress(request.getParameter("SB_add"));
		equipment.setCustomer(request.getParameter("SB_no"));
		equipment.setHead(request.getParameter("SB_tel"));
		equipment.setCreatename((String) session.getAttribute("username"));
		String a = request.getParameter("SB_x");
		double d = Double.parseDouble(a);
		// DecimalFormat decimalFormat = new DecimalFormat("###0.000000000");// 格式化设置
		String c = request.getParameter("SB_y");
		double b = Double.parseDouble(c);
		equipment.setJingdu(d);
		equipment.setWeidu(b);
		equipment.setPhone(request.getParameter("SB_name"));
		SimpleDateFormat df = new SimpleDateFormat("yyyy-MM-dd-HH:mm.ss");
		equipment.setDate(df.format(new Date()));
		int e = equipmentDao.insert(equipment);
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
		System.out.println(Integer.valueOf(request.getParameter("SB_tel_e")));
		equipment.setId(Integer.valueOf(request.getParameter("SB_tel_e")));
		equipment.setName(request.getParameter("SB_company_e"));
		equipment.setCustomer(request.getParameter("SB_no_e"));
		equipment.setHead(request.getParameter("SB_type_e"));
		equipment.setUpdatename((String) session.getAttribute("username"));
		equipment.setAddress(request.getParameter("SB_add_e"));
		String a = request.getParameter("SB_x_e");
		double d = Double.parseDouble(a);
		// DecimalFormat decimalFormat = new DecimalFormat("###0.000000000");// 格式化设置
		String c = request.getParameter("SB_y_e");
		double b = Double.parseDouble(c);

		equipment.setJingdu(d);
		equipment.setWeidu(b);
		equipment.setPhone(request.getParameter("SB_name_e"));

		equipmentDao equipmentDao = new equipmentDao();
		int e = equipmentDao.update(equipment);
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
