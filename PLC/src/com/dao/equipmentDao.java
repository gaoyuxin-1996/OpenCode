package com.dao;

import java.util.ArrayList;
import java.util.List;

import com.item.equipment;

public class equipmentDao extends Dao {

	public List<equipment> XinxiAll() {

		List<equipment> list = new ArrayList<equipment>();
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from equipment ");
			while (resultSet.next()) {
				equipment equipment = new equipment();
				equipment.setId(resultSet.getInt(1));
				equipment.setName(resultSet.getString(2));
				equipment.setCustomer(resultSet.getString(3));
				equipment.setHead(resultSet.getString(4));
				equipment.setPhone(resultSet.getString(5));
				equipment.setAddress(resultSet.getString(6));
				equipment.setJingdu(resultSet.getDouble(7));
				equipment.setWeidu(resultSet.getDouble(8));
				equipment.setPlcid(resultSet.getInt(9));
				equipment.setGatewayid(resultSet.getString(10));
				equipment.setCreatename(resultSet.getString(11));
				equipment.setDate(resultSet.getString(12));
				equipment.setUpdatename(resultSet.getString(13));
				equipment.setUpdatedate(resultSet.getDate(14));
				equipment.setPlcname(resultSet.getString(15));
				equipment.setCategory(resultSet.getString(16));
				equipment.setPlcbrand(resultSet.getString(17));
				equipment.setPlcxilie(resultSet.getString(18));
				equipment.setAgreement(resultSet.getString(19));
				equipment.setWay(resultSet.getString(20));
				equipment.setGatewayname(resultSet.getInt(21));

				list.add(equipment);

			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;

	}

	public List<equipment> XinxiAll1() {

		List<equipment> list = new ArrayList<equipment>();
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from equipment");
			while (resultSet.next()) {
				equipment equipment = new equipment();
				equipment.setId(resultSet.getInt(1));
				equipment.setName(resultSet.getString(2));
				equipment.setCustomer(resultSet.getString(3));
				equipment.setHead(resultSet.getString(4));
				equipment.setPhone(resultSet.getString(5));
				equipment.setAddress(resultSet.getString(6));
				equipment.setJingdu(resultSet.getDouble(7));
				equipment.setWeidu(resultSet.getDouble(8));
				equipment.setPlcid(resultSet.getInt(9));
				equipment.setGatewayid(resultSet.getString(10));
				equipment.setPlcname(resultSet.getString(15));
				equipment.setCategory(resultSet.getString(16));
				equipment.setPlcbrand(resultSet.getString(17));
				equipment.setPlcxilie(resultSet.getString(18));
				equipment.setAgreement(resultSet.getString(19));
				equipment.setWay(resultSet.getString(20));
				equipment.setGatewayname(resultSet.getInt(21));

				list.add(equipment);
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;

	}

	public List<String> companyall() {

		List<String> list = new ArrayList<String>();
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select glcompany from equipment group by glcompany");
			while (resultSet.next()) {
				list.add(resultSet.getString(1));

			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;

	}

	public int del(String aString) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("delete from equipment where id ='" + aString + "'");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			a = 0;
			e.printStackTrace();
		}

		return a;

	}

	public int updatewangguan(equipment equipment) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("update equipment set gatewayid = '" + equipment.getGatewayid() + "',"
					+ "gatewayname ='" + equipment.getGatewayname() + "'" + " where id = '" + equipment.getId() + "'");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			a = 0;
			e.printStackTrace();
		}
		return a;

	}

	public int update(equipment equipment) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("update equipment set name = '" + equipment.getName() + "'," + "phone ='"
					+ equipment.getPhone() + "'," + "jingdu = '" + equipment.getJingdu() + "'," + "customer ='"
					+ equipment.getCustomer() + "'," + "head = '" + equipment.getHead() + "'," + "updatename = '"
					+ equipment.getUpdatename() + "'," + "weidu = '" + equipment.getWeidu() + "'," + "address = '"
					+ equipment.getAddress() + "'" + " where id = '" + equipment.getId() + "'");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			a = 0;
			e.printStackTrace();
		}
		return a;

	}

	public void updategps(equipment equipment) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("update dingwei set jingdu1 = '" + equipment.getJingdu() + "'," + "weidu1 = '"
					+ equipment.getWeidu() + "' where id = '" + equipment.getId() + "'");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			a = 0;
			e.printStackTrace();
		}

		return;

	}

	public void updategps1(equipment equipment) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("update dingwei set jingdu = '" + equipment.getJingdu() + "'," + "weidu = '"
					+ equipment.getWeidu() + "' where id = '" + equipment.getId() + "'");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			a = 0;
			e.printStackTrace();
		}

		return;

	}

	public int insert(equipment equipment) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement(
					"insert into equipment(name,customer,head,phone,address,jingdu,weidu,plcid,createname,date,chanpinname,category,plcbrand,plcxilie,agreement,way) values('"
							+ equipment.getName() + "','" + equipment.getCustomer() + "','" + equipment.getHead()
							+ "','" + equipment.getPhone() + "','" + equipment.getAddress() + "','"
							+ equipment.getJingdu() + "','" + equipment.getWeidu() + "','" + equipment.getPlcid()
							+ "','" + equipment.getCreatename() + "','" + equipment.getDate() + "','"
							+ equipment.getPlcname() + "','" + equipment.getCategory() + "','" + equipment.getPlcbrand()
							+ "','" + equipment.getXilie() + "','" + equipment.getAgreement() + "','"
							+ equipment.getWay1() + "')");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
		}

		return a;

	}

	public equipment Xinxi(equipment equipment) {
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from equipment where id ='" + equipment.getId() + "'");
			if (resultSet.next()) {

				equipment.setId(resultSet.getInt(1));
				equipment.setName(resultSet.getString(2));
				equipment.setCustomer(resultSet.getString(3));
				equipment.setHead(resultSet.getString(4));
				equipment.setPhone(resultSet.getString(5));
				equipment.setAddress(resultSet.getString(6));
				equipment.setJingdu(resultSet.getDouble(7));
				equipment.setWeidu(resultSet.getDouble(8));
				equipment.setPlcid(resultSet.getInt(9));
				equipment.setGatewayid(resultSet.getString(10));
				equipment.setPlcname(resultSet.getString(15));
				equipment.setCategory(resultSet.getString(16));
				equipment.setPlcbrand(resultSet.getString(17));
				equipment.setPlcxilie(resultSet.getString(18));
				equipment.setAgreement(resultSet.getString(19));
				equipment.setWay(resultSet.getString(20));
				equipment.setGatewayname(resultSet.getShort(21));

			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return equipment;

	}

	public List<String> Xinxi1(equipment equipment) {
		List<String> list = new ArrayList<String>();
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select id from equipment where plcid ='" + equipment.getPlcid() + "'");
			while (resultSet.next()) {
				list.add(String.valueOf(resultSet.getInt(1)));

			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;

	}

	public List<equipment> Xinxibycompany(String glcompany) {
		List<equipment> list = new ArrayList<equipment>();
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from equipment where chanpinname ='" + glcompany + "'");
			while (resultSet.next()) {
				equipment equipment = new equipment();
				equipment.setId(resultSet.getInt(1));
				equipment.setName(resultSet.getString(2));
				equipment.setCustomer(resultSet.getString(3));
				equipment.setHead(resultSet.getString(4));
				equipment.setPhone(resultSet.getString(5));
				equipment.setAddress(resultSet.getString(6));
				equipment.setJingdu(resultSet.getDouble(7));
				equipment.setWeidu(resultSet.getDouble(8));
				equipment.setPlcid(resultSet.getInt(9));
				equipment.setGatewayid(resultSet.getString(10));

				equipment.setPlcname(resultSet.getString(15));
				equipment.setCategory(resultSet.getString(16));
				equipment.setPlcbrand(resultSet.getString(17));
				equipment.setPlcxilie(resultSet.getString(18));
				equipment.setAgreement(resultSet.getString(19));
				equipment.setWay(resultSet.getString(20));
				equipment.setGatewayname(resultSet.getShort(21));
				list.add(equipment);
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;

	}

	public List<String> Xinxiplcid() {
		List<String> list = new ArrayList<String>();
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select chanpinname from equipment group by chanpinname");
			while (resultSet.next()) {

				list.add(resultSet.getString(1));
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;

	}
}
