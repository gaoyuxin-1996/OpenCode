package com.servlet;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import com.dao.UserDao;
import com.item.User;

@WebServlet(name = "UserServlet", urlPatterns = "/UserServlet")
public class UserServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	public UserServlet() {
		super();

	}

	protected void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		doPost(request, response);
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		String username = request.getParameter("username");
		String password = request.getParameter("password");
		User user = new User();
		user.setName(username);
		user.setPassword(password);
		UserDao userDao = new UserDao();
		userDao.login(user);
		int a = userDao.login(user);
		if (a > 0) {
			System.out.println("aa");
			System.out.println(user.getName());
			HttpSession session = request.getSession();
			session.setAttribute("username", user.getName());
			request.getRequestDispatcher("equipmentServlet?action=num").forward(request, response);
		} else {
			request.setAttribute("returnvalue", "失败");
			request.getRequestDispatcher("Login.jsp").forward(request, response);
		}

	}

}
