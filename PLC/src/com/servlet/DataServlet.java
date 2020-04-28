package com.servlet;

import java.io.BufferedInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.dao.dianbiaoDao;
import com.dao.xinxiDao;
import com.item.dianbiao;
import com.item.xinxi;

import net.sf.json.JSONArray;
import net.sf.json.JSONObject;

@WebServlet(name = "DataServlet", urlPatterns = "/DataServlet")
public class DataServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	public DataServlet() {
		super();

	}

	protected void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		doPost(request, response);
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		System.out.println("data");
		InputStream result = new BufferedInputStream(request.getInputStream());
		ByteArrayOutputStream sout = new ByteArrayOutputStream();
		int b = 0;
		while ((b = result.read()) != -1) {
			sout.write(b);
		}
		byte[] temp = sout.toByteArray();
		String re = new String(temp, "UTF-8");
		System.out.println(re);
		JSONObject json_test = JSONObject.fromObject(re);
		xinxi xinxi = new xinxi();
		xinxiDao xinxiDao = new xinxiDao();
		xinxi.setPtid(String.valueOf(json_test.getString("ptid")));
		xinxi.setGid(json_test.getString("gid"));
		xinxi.setCid(String.valueOf(json_test.getInt("cid")));
		JSONArray array = JSONArray.fromObject(json_test.getJSONObject("points"));
		dianbiao dianbiao = new dianbiao();
		dianbiaoDao dianbiaoDao = new dianbiaoDao();
		dianbiao.setGatewayid(json_test.getString("gid"));
		dianbiao.setWay(Integer.valueOf(json_test.getString("cid")));
		xinxiDao.update1(xinxi);
		List<dianbiao> list = dianbiaoDao.dianbiaoxinxi(dianbiao);
		int y = list.size();
		int u;
		String jjString = null;
		for (u = 0; u < y; u++) {
			jjString = json_test.getJSONObject("points").getString(list.get(u).getName());
			dianbiao.setName(list.get(u).getName());
			dianbiao.setZhi(jjString);
			dianbiaoDao.update1(dianbiao);
		}
		String jsonstr = array.toString();
		xinxi.setPonits(jsonstr);
		// xinxiDao.insertall(xinxi);

	}

}
