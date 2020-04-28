package com.servlet;

import java.io.IOException;
import java.io.Writer;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import com.dao.dianbiaoDao;
import com.dao.plcDao;
import com.dao.xinxiDao;
import com.item.dianbiao;
import com.item.equipment;

import net.sf.json.JSONObject;

@WebServlet(name = "dianbiaoServlet", urlPatterns = "/dianbiaoServlet")
public class dianbiaoServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	public dianbiaoServlet() {
		super();

	}

	protected void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		doPost(request, response);
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		String action = request.getParameter("action");
		if ("all".equals(action)) {
			all(request, response);
		} else if ("add".equals(action)) {
			add(request, response);
		} else if ("xinxi".equals(action)) {
			xinxi(request, response);
		} else if ("update".equals(action)) {
			update(request, response);
		} else if ("delete".equals(action)) {
			delete(request, response);
		} else if ("dianbiao".equals(action)) {
			dianbiao(request, response);
		} else if ("shuru".equals(action)) {
			shuru(request, response);
		}
	}

	protected void shuru(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		HttpSession session = request.getSession();
		String aString = (String) session.getAttribute("username");
		if (aString == null) {

			request.getRequestDispatcher("Login.jsp").forward(request, response);
		} else {
			System.out.println("shuru");
			dianbiao dianbiao = new dianbiao();
			dianbiao.setGatewayid(request.getParameter("ptid"));
			dianbiao.setWay(Integer.valueOf(request.getParameter("way")));
			System.out.println(dianbiao.getWay());
			dianbiaoDao dao = new dianbiaoDao();
			System.out.println("1");
			List<dianbiao> list = dao.dianbiaoxinxi1(dianbiao);
			System.out.println("2");
			Map<String, Object> resultMap = new HashMap<String, Object>();
			resultMap.put("dianbiao", list);
			String resultStr = JSONObject.fromObject(resultMap).toString();
			Writer out = response.getWriter();
			out.write(resultStr);
			out.flush();
		}

	}

	protected void dianbiao(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		HttpSession session = request.getSession();
		String aString = (String) session.getAttribute("username");
		if (aString == null) {

			request.getRequestDispatcher("Login.jsp").forward(request, response);
		} else {

			com.item.xinxi xinxi = new com.item.xinxi();
			xinxi.setPtid(request.getParameter("ptid"));
			xinxiDao xinxiDao = new xinxiDao();
			xinxi = xinxiDao.ptid(xinxi);
			String json = JSONObject.fromObject(xinxi.getPonits()).toString();
			System.out.println(json);
			Writer out = response.getWriter();
			out.write(json);
			out.flush();
		}

	}

	protected void delete(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		String aString = request.getParameter("SB_no_d");
		System.out.println("delete");
		dianbiao dianbiao = new dianbiao();
		dianbiao.setGatewayid(aString);
		dianbiao.setName(request.getParameter("SB_name_d"));
		dianbiao.setWay(Integer.valueOf(request.getParameter("SB_way_d")));
		dianbiaoDao dianbiaoDao = new dianbiaoDao();
		int c = dianbiaoDao.del(dianbiao);
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
			request.getRequestDispatcher("controller2.jsp").forward(request, response);
		}

	}

	protected void xinxi(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		HttpSession session = request.getSession();
		String aString = (String) session.getAttribute("username");
		if (aString == null) {

			request.getRequestDispatcher("Login.jsp").forward(request, response);
		} else {
			System.out.println("xinxi");

			dianbiao dianbiao = new dianbiao();
			dianbiao.setGatewayid(request.getParameter("SB_plcid"));
			dianbiao.setWay(Integer.valueOf(request.getParameter("way")));
			System.out.println(request.getParameter("SB_plcid"));
			dianbiaoDao dao = new dianbiaoDao();
			List<dianbiao> list = dao.dianbiaoxinxi(dianbiao);
			Map<String, Object> resultMap = new HashMap<String, Object>();
			resultMap.put("data", list);
			String resultStr = JSONObject.fromObject(resultMap).toString();// 要返回的参数字符串
			System.out.println(resultStr);
			Writer out = response.getWriter();
			out.write(resultStr);
			out.flush();
		}

	}

	protected void add(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		HttpSession session = request.getSession();
		String aString = (String) session.getAttribute("username");
		if (aString == null) {

			request.getRequestDispatcher("Login.jsp").forward(request, response);
		} else {
			System.out.println("add");

			Map<String, Object> resultMap = new HashMap<String, Object>();
			dianbiao dianbiao = new dianbiao();
			dianbiao.setGatewayid(request.getParameter("SB_plcid"));
			dianbiao.setName(request.getParameter("SB_bianliang"));
			dianbiao.setUnit(request.getParameter("SB_danwei"));
			dianbiao.setRegister(request.getParameter("SB_jicun"));
			dianbiao.setAddress(request.getParameter("SB_address"));
			dianbiao.setData(request.getParameter("SB_data"));
			dianbiao.setWeiaddress(request.getParameter("SB_wei"));
			dianbiao.setBool(request.getParameter("SB_buer"));
			dianbiao.setChufa(request.getParameter("SB_chufa"));
			dianbiao.setPutong(request.getParameter("SB_putong"));
			dianbiao.setCunchu1(request.getParameter("SB_cunchu1"));
			dianbiao.setCunchu(request.getParameter("SB_cunchu"));
			dianbiao.setDuxie(request.getParameter("SB_duxie"));
			dianbiao.setYanzheng(request.getParameter("SB_yanzheng"));
			System.out.println(request.getParameter("SB_plcid"));
			System.out.println(request.getParameter("way"));
			int f = Integer.valueOf(request.getParameter("way"));
			dianbiao.setWay(f);
			dianbiaoDao dao = new dianbiaoDao();
			int e = dao.insert(dianbiao);

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

	}

	protected void update(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		HttpSession session = request.getSession();
		String aString = (String) session.getAttribute("username");
		if (aString == null) {

			request.getRequestDispatcher("Login.jsp").forward(request, response);
		} else {
			System.out.println("update");

			Map<String, Object> resultMap = new HashMap<String, Object>();
			dianbiao dianbiao = new dianbiao();

			dianbiao.setGatewayid(request.getParameter("SB_plcid"));
			;
			dianbiao.setName(request.getParameter("SB_bianliang1"));
			dianbiao.setUnit(request.getParameter("SB_danwei1"));
			dianbiao.setRegister(request.getParameter("SB_jicun1"));
			dianbiao.setAddress(request.getParameter("SB_address1"));
			dianbiao.setData(request.getParameter("SB_data1"));
			dianbiao.setWeiaddress(request.getParameter("SB_wei1"));
			dianbiao.setBool(request.getParameter("SB_buer1"));
			dianbiao.setChufa(request.getParameter("SB_chufa1"));
			dianbiao.setPutong(request.getParameter("SB_putong1"));
			dianbiao.setCunchu1(request.getParameter("SB_cunchu11"));
			dianbiao.setCunchu(request.getParameter("SB_cunchu1"));
			dianbiao.setDuxie(request.getParameter("SB_duxie1"));
			dianbiao.setYanzheng(request.getParameter("SB_yanzheng1"));
			dianbiao.setWay(Integer.valueOf(request.getParameter("way")));
			dianbiaoDao dao = new dianbiaoDao();
			int e = dao.update(dianbiao);

			if (e == 1) {
				request.setAttribute("e", "aa");
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

}
