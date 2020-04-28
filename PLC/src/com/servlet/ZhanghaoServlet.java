package com.servlet;

import java.io.IOException;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import com.dao.UserDao;
import com.item.User;

@WebServlet(name = "ZhanghaoServlet", urlPatterns = "/ZhanghaoServlet")
public class ZhanghaoServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	public ZhanghaoServlet() {
		super();

	}

	protected void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		doPost(request, response);
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		String action = request.getParameter("action");
		if ("update".equals(action)) {
			update(request, response);
		} else if ("doPost1".equals(action)) {
			doPost1(request, response);
		} else if ("list".equals(action)) {
			list(request, response);
		} else if ("del".equals(action)) {
			del(request, response);
		} else if ("add".equals(action)) {
			add(request, response);
		} else if ("mima".equals(action)) {
			mima(request, response);
		} else if ("out".equals(action)) {
			out(request, response);
		}
	}

	protected void doPost1(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		HttpSession session = request.getSession();
		String aString = (String) session.getAttribute("username");
		User user = new User();
		user.setName(aString);
		UserDao userDao = new UserDao();
		userDao.Xinxi(user);
		if (user.getQuanxian() == 1) {
			request.setAttribute("username", aString);
			request.setAttribute("user", user);

			request.getRequestDispatcher("ZhanghaoServlet?action=list").forward(request, response);
		} else if (user.getQuanxian() == 2) {
			request.setAttribute("username", aString);
			request.setAttribute("MESS", "cuowu");
			request.getRequestDispatcher("NewFile.jsp").forward(request, response);
		} else if (user.getQuanxian() == 0) {

			request.getRequestDispatcher("Login.jsp").forward(request, response);
		}
	}

	protected void list(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		User user = new User();
		UserDao userDao = new UserDao();
		List<User> list = userDao.XinxiAll();

		request.setAttribute("user", user);
		request.setAttribute("list", list);
		request.getRequestDispatcher("zhanghaoguanli.jsp").forward(request, response);

	}

	protected void update(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		User user = new User();

		user.setName((String) request.getParameter("U_ID_e"));
		user.setPassword((String) request.getParameter("U_pass_e"));
		user.setXingming((String) request.getParameter("U_name_e"));
		user.setAddress((String) request.getParameter("U_add_e"));
		user.setPhone((String) request.getParameter("U_city_e"));
		user.setGongsi((String) request.getParameter("U_BM_ID_e"));
		user.setBumen((String) request.getParameter("U_lv_e"));
		user.setEmail((String) request.getParameter("U_title_e"));
		String a = request.getParameter("U_IMSG_e");
		int i = Integer.parseInt(a);
		user.setQuanxian(i);
		UserDao userDao = new UserDao();
		int c = userDao.update(user);

		if (c == 1) {
			request.setAttribute("c", "aa");

			request.getRequestDispatcher("ZhanghaoServlet?action=doPost1").forward(request, response);

		} else if (c == 0) {

			request.getRequestDispatcher("NewFile.jsp").forward(request, response);
		}
	}

	protected void mima(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

		User user = new User();
		user.setName((String) request.getParameter("U_ID"));
		user.setPassword((String) request.getParameter("U_pass"));
		String password1 = request.getParameter("U_pass1");
		String password2 = request.getParameter("U_pass2");
		UserDao userDao = new UserDao();
		int a = userDao.login(user);
		System.out.println((String) request.getParameter("U_pass"));
		System.out.println(password1);
		System.out.println(password2);
		System.out.println(a);
		if (password1.equals(password2) == false) {
			request.setAttribute("c", "aaa");
			request.getRequestDispatcher("mima.jsp").forward(request, response);
		} else if (a == 0) {
			request.setAttribute("c", "aa");
			request.getRequestDispatcher("mima.jsp").forward(request, response);
		} else if (a == 1 && password1.equals(password2)) {
			user.setPassword(password1);
			int c = userDao.mima(user);
			System.out.println(c);
			if (c == 1) {
				request.setAttribute("c", "a");
				request.getRequestDispatcher("mima.jsp").forward(request, response);

			} else if (c == 0) {

				request.getRequestDispatcher("mima.jsp").forward(request, response);
			}
		}
	}

	protected void add(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

		User user = new User();
		user.setName((String) request.getParameter("U_ID"));
		user.setPassword((String) request.getParameter("U_pass"));
		user.setXingming((String) request.getParameter("U_name"));
		user.setAddress((String) request.getParameter("U_add"));
		user.setPhone((String) request.getParameter("U_tel1"));
		user.setGongsi((String) request.getParameter("U_title"));
		user.setBumen((String) request.getParameter("U_IMSG"));
		user.setEmail((String) request.getParameter("U_tel2"));
		String a = request.getParameter("U_lv");
		int i = Integer.parseInt(a);
		user.setQuanxian(i);
		UserDao userDao = new UserDao();
		int c = userDao.insert(user);

		if (c == 1) {
			request.setAttribute("d", "aa");

			request.getRequestDispatcher("ZhanghaoServlet?action=doPost1").forward(request, response);

		} else if (c == 0) {

			request.getRequestDispatcher("ZhanghaoServlet?action=num").forward(request, response);
		}
	}

	protected void del(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

		String aString = request.getParameter("U_ID_d");
		String aString1 = request.getParameter("U_name_d");
		UserDao userDao = new UserDao();
		int c = userDao.del(aString);

		if (c == 1) {
			request.setAttribute("b", "aa");

			request.getRequestDispatcher("ZhanghaoServlet?action=doPost1").forward(request, response);

		} else if (c == 0) {

			request.getRequestDispatcher("NewFile.jsp").forward(request, response);
		}
	}

	protected void out(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		HttpSession session = request.getSession();
		session.setAttribute("username", null);
		request.getRequestDispatcher("Login.jsp").forward(request, response);

	}
}
