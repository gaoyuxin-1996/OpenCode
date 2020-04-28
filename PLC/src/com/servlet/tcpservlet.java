package com.servlet;

import java.io.IOException;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.Socket;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Scanner;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.dao.xinxiDao;
import com.item.zhi;

import net.sf.json.JSONObject;

@WebServlet(name = "tcpServlet", urlPatterns = "/tcpServlet")
public class tcpservlet extends HttpServlet {

	@Override
	protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		// TODO 自动生成的方法存根
		doPost(req, resp);
	}

	@Override
	protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		// TODO 自动生成的方法存根
		String action = req.getParameter("action");
		if ("zhi".equals(action)) {
			zhi(req, resp);
		} else if ("startorstop".equals(action)) {
			startorstop(req, resp);
		}

	}

	protected void startorstop(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		int data = Integer.valueOf(req.getParameter("data"));
		com.item.startorstop start = new com.item.startorstop();
		zhi zhi = new zhi();
		Date dt = new Date();
		// 最后的aa表示“上午”或“下午” HH表示24小时制 如果换成hh表示12小时制
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		xinxiDao xinxiDao = new xinxiDao();
		String ptid = xinxiDao.xinxi();
		int d = Integer.valueOf(ptid);
		start.setGid("H120-1810-2408-3746-3306-3224");
		start.setCid(1);
		start.setFunc(84);
		start.setPtid(d);
		start.setTime(sdf.format(dt));
		start.setRealSw(data);

		String resultStr = JSONObject.fromObject(start).toString();// 要返回的参数字符串

		System.out.println(resultStr);

		client(resultStr);

	}

	protected void zhi(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		int data = Integer.valueOf(req.getParameter("data"));
		String key = req.getParameter("key");
		System.err.println(req.getParameter("way"));
		int way = Integer.valueOf(req.getParameter("way"));
		xinxiDao xinxiDao = new xinxiDao();
		String ptid = xinxiDao.xinxi();
		int d = Integer.valueOf(ptid);
		com.item.client client = new com.item.client();
		zhi zhi = new zhi();
		Date dt = new Date();
		// 最后的aa表示“上午”或“下午” HH表示24小时制 如果换成hh表示12小时制
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");

		client.setGid("H120-1810-2408-3746-3306-3224");
		client.setCid(way);
		client.setFunc(83);
		client.setCode("45612354698453");
		client.setPtid(d);
		client.setPwd("12345678");
		client.setTime(sdf.format(dt));
		zhi.setK(key);
		zhi.setV(data);
		client.setCmd(JSONObject.fromObject(zhi));
		System.out.println(client.getCmd());
		String resultStr = JSONObject.fromObject(client).toString();// 要返回的参数字符串
		String s11 = resultStr.replace("\\", "");

		System.out.println(s11);

		client(s11);

	}

	public void client(String data) {
		Socket socket = null;
		try {
			System.out.println("connecting...");
			socket = new Socket("120.55.49.108", 60002);
			System.out.println("connection success");
			Scanner in = new Scanner(System.in);
			PrintWriter pw = new PrintWriter(new OutputStreamWriter(socket.getOutputStream(), "utf-8"), true);
			System.out.println("1");

			pw.println(data);

			System.out.println("2");

			pw.close();
			in.close();

		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			if (socket != null) {
				try {

				} catch (Exception e) {

				}
			}
		}

	}

}
