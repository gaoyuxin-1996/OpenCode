package com.cb.cbms.coreserver.servlet;

import java.io.IOException;

import javax.servlet.Filter;
import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

/**
 * 不允许直接访问jsp 所有对jsp的直接访问，跳转到首页面
 * 
 * @author eason
 * 
 */
public class JspFilter implements Filter {

	@Override
	public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain)
			throws IOException, ServletException {
		HttpServletRequest httpServletRequest = (HttpServletRequest) request;
		HttpServletResponse httpServletResponse = (HttpServletResponse) response;
		String url = httpServletRequest.getRequestURI();
		HttpSession session = ((HttpServletRequest) request).getSession();
		String aString = (String) session.getAttribute("username");
		if (aString == null && url.endsWith(".jsp")) {
			httpServletResponse.sendRedirect(httpServletRequest.getContextPath());
			request.getRequestDispatcher("Login.jsp").forward(request, response);
			return;
		}
		chain.doFilter(request, response);
	}

	@Override
	public void destroy() {

	}

	@Override
	public void init(FilterConfig arg0) throws ServletException {

	}

}