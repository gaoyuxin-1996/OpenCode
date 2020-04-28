package com.dao;

import java.util.ArrayList;
import java.util.List;

import com.item.dianbiao;

public class dianbiaoDao extends Dao {

	public List<dianbiao> dianbiaoall() {
		List<dianbiao> list = new ArrayList<dianbiao>();
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from plc");
			while (resultSet.next()) {
				dianbiao dianbiao = new dianbiao();
				dianbiao.setGatewayid(resultSet.getString(1));
				dianbiao.setName(resultSet.getString(2));
				dianbiao.setUnit(resultSet.getString(3));
				dianbiao.setRegister(resultSet.getString(4));
				dianbiao.setAddress(resultSet.getString(5));
				dianbiao.setData(resultSet.getString(6));
				dianbiao.setWeiaddress(resultSet.getString(7));
				dianbiao.setBool(resultSet.getString(8));
				dianbiao.setChufa(resultSet.getString(9));
				dianbiao.setCunchu1(resultSet.getString(10));
				dianbiao.setCunchu(resultSet.getString(11));
				dianbiao.setDuxie(resultSet.getString(12));
				dianbiao.setYanzheng(resultSet.getString(13));
				dianbiao.setPutong(resultSet.getString(14));
				dianbiao.setZhi(resultSet.getString(15));
				list.add(dianbiao);
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;
	}

	public List<dianbiao> dianbiaoxinxi1(dianbiao dianbiao) {
		List<dianbiao> list = new ArrayList<dianbiao>();
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from dianbiao where gatewayid = '" + dianbiao.getGatewayid()
					+ "' and way = " + dianbiao.getWay());
			while (resultSet.next()) {
				dianbiao = new dianbiao();
				dianbiao.setGatewayid(resultSet.getString(1));
				dianbiao.setName(resultSet.getString(2));
				dianbiao.setBool(resultSet.getString(8));
				dianbiao.setZhi(resultSet.getString(15));
				dianbiao.setWay(resultSet.getInt(16));
				list.add(dianbiao);
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;
	}

	public List<dianbiao> dianbiaoxinxi(dianbiao dianbiao) {
		List<dianbiao> list = new ArrayList<dianbiao>();
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from dianbiao where gatewayid = '" + dianbiao.getGatewayid()
					+ "' and way = " + dianbiao.getWay());
			while (resultSet.next()) {
				dianbiao = new dianbiao();
				dianbiao.setGatewayid(resultSet.getString(1));
				dianbiao.setName(resultSet.getString(2));
				dianbiao.setUnit(resultSet.getString(3));
				dianbiao.setRegister(resultSet.getString(4));
				dianbiao.setAddress(resultSet.getString(5));
				dianbiao.setData(resultSet.getString(6));
				dianbiao.setWeiaddress(resultSet.getString(7));
				dianbiao.setBool(resultSet.getString(8));
				dianbiao.setChufa(resultSet.getString(9));
				dianbiao.setCunchu1(resultSet.getString(10));
				dianbiao.setCunchu(resultSet.getString(11));
				dianbiao.setDuxie(resultSet.getString(12));
				dianbiao.setYanzheng(resultSet.getString(13));
				dianbiao.setPutong(resultSet.getString(14));
				dianbiao.setZhi(resultSet.getString(15));
				dianbiao.setWay(resultSet.getInt(16));
				list.add(dianbiao);
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;
	}

	public int insert(dianbiao dianbiao) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("insert into dianbiao values('" + dianbiao.getGatewayid() + "','"
					+ dianbiao.getName() + "','" + dianbiao.getUnit() + "','" + dianbiao.getRegister() + "','"
					+ dianbiao.getAddress() + "','" + dianbiao.getData() + "','" + dianbiao.getWeiaddress() + "','"
					+ dianbiao.getBool() + "','" + dianbiao.getChufa() + "','" + dianbiao.getCunchu1() + "','"
					+ dianbiao.getCunchu() + "','" + dianbiao.getDuxie() + "','" + dianbiao.getYanzheng() + "','"
					+ dianbiao.getPutong() + "','-','" + dianbiao.getWay() + "')");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			a = 0;
			e.printStackTrace();
		}

		return a;

	}

	public int del(dianbiao dianbiao) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("delete from dianbiao where gatewayid ='" + dianbiao.getGatewayid()
					+ "' and name = '" + dianbiao.getName() + "' and way = " + dianbiao.getWay());
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			a = 0;
			e.printStackTrace();
		}

		return a;

	}

	public int update(dianbiao dianbiao) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("update dianbiao set unit ='" + dianbiao.getUnit() + "'," + "register = '"
					+ dianbiao.getRegister() + "'," + "address ='" + dianbiao.getAddress() + "'," + "data = '"
					+ dianbiao.getData() + "'," + "weiaddress = '" + dianbiao.getWeiaddress() + "'," + "bool = '"
					+ dianbiao.getBool() + "'," + "chufa = '" + dianbiao.getChufa() + "'," + "cunchu ='"
					+ dianbiao.getCunchu1() + "'," + "cunchu1 = '" + dianbiao.getCunchu() + "'," + "duxie ='"
					+ dianbiao.getDuxie() + "'," + "yanzheng = '" + dianbiao.getYanzheng() + "'," + "putong = '"
					+ dianbiao.getPutong() + "'" + " where gatewayid = '" + dianbiao.getGatewayid() + "' and name = '"
					+ dianbiao.getName() + "' and way = " + dianbiao.getWay());
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			a = 0;
			e.printStackTrace();
		}

		return a;

	}

	public int update1(dianbiao dianbiao) {
		int a = 0;
		try {

			psql = conn.prepareStatement("update dianbiao set zhi = '" + dianbiao.getZhi() + "'"
					+ " where gatewayid = '" + dianbiao.getGatewayid() + "' and name = '" + dianbiao.getName()
					+ "' and way =" + dianbiao.getWay());
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			a = 0;
			e.printStackTrace();
		}

		return a;

	}

	public int update2(dianbiao dianbiao) {
		int a = 0;
		try {

			psql = conn.prepareStatement("update dianbiao set zhi = '" + dianbiao.getZhi() + "'"
					+ " where gatewayid = '" + dianbiao.getGatewayid() + "' and name = '" + dianbiao.getName()
					+ "' and way =" + dianbiao.getWay());
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			a = 0;
			e.printStackTrace();
		}

		return a;

	}

}
