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
				equipment.setId(resultSet.getString(1));
				equipment.setLeixing(resultSet.getString(2));
				equipment.setName(resultSet.getString(3));
				equipment.setState(resultSet.getString(4));
				equipment.setOperator(resultSet.getString(5));
				equipment.setGlcompany(resultSet.getString(6));
				equipment.setAddress(resultSet.getString(7));
				equipment.setJingdu(resultSet.getDouble(8));
				equipment.setWeidu(resultSet.getDouble(9));
				equipment.setPhone(resultSet.getString(10));

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
			resultSet = sql.executeQuery(
					"select e.id,d.id,e.name,e.leixing,e.state,e.operator,e.glcompany,e.address,d.jingdu,d.weidu,e.phone from equipment e left join dingwei d on e.id=d.id");
			while (resultSet.next()) {
				equipment equipment = new equipment();
				equipment.setId(resultSet.getString(1));
				equipment.setLeixing(resultSet.getString(4));
				equipment.setName(resultSet.getString(3));
				equipment.setState(resultSet.getString(5));
				equipment.setOperator(resultSet.getString(6));
				equipment.setGlcompany(resultSet.getString(7));
				equipment.setAddress(resultSet.getString(8));
				equipment.setJingdu(resultSet.getDouble(9));
				equipment.setWeidu(resultSet.getDouble(10));
				equipment.setPhone(resultSet.getString(11));

				list.add(equipment);

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
			res = sql.execute("delete from equipment where id ='" + aString + "'");

			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
		}

		return a;

	}

	public int update(equipment equipment) {
		int a = 0;
		try {
			sql = conn.createStatement();

			res = sql.execute("update equipment set name = '" + equipment.getName() + "'," + "phone ='"
					+ equipment.getPhone() + "'," + "leixing = '" + equipment.getLeixing() + "'," + "operator = '"
					+ equipment.getOperator() + "'," + "glcompany = '" + equipment.getGlcompany() + "'," + "jingdu = '"
					+ equipment.getJingdu() + "'," + "weidu = '" + equipment.getWeidu() + "'," + "address = '"
					+ equipment.getAddress() + "'" + " where id = '" + equipment.getId() + "'");

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

			res = sql.execute("update dingwei set jingdu1 = '" + equipment.getJingdu() + "'," + "weidu1 = '"
					+ equipment.getWeidu() + "' where id = '" + equipment.getId() + "'");

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

			res = sql.execute("update dingwei set jingdu = '" + equipment.getJingdu() + "'," + "weidu = '"
					+ equipment.getWeidu() + "' where id = '" + equipment.getId() + "'");

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

			res = sql.execute("insert into equipment values('" + equipment.getId() + "','" + equipment.getLeixing()
					+ "','" + equipment.getName() + "','','" + equipment.getOperator() + "','"
					+ equipment.getGlcompany() + "','" + equipment.getAddress() + "','" + equipment.getJingdu() + "','"
					+ equipment.getWeidu() + "','" + equipment.getPhone() + "')");
			res = sql.execute("insert into dingwei values('" + equipment.getId() + "','" + equipment.getJingdu() + "','"
					+ equipment.getWeidu() + "')");

			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
		}

		return a;

	}

	public equipment Xinxi(equipment equipment) {
		try {
			sql = conn.createStatement();
			resultSet = sql.executeQuery(
					"select e.id,d.id,e.name,e.leixing,e.state,e.operator,e.glcompany,e.address,d.jingdu,d.weidu,e.phone from equipment e left join dingwei d on e.id=d.id where e.id ='"
							+ equipment.getId() + "'");
			if (resultSet.next()) {
				equipment.setId(resultSet.getString(1));
				equipment.setLeixing(resultSet.getString(4));
				equipment.setName(resultSet.getString(3));
				equipment.setState(resultSet.getString(5));
				equipment.setOperator(resultSet.getString(6));
				equipment.setGlcompany(resultSet.getString(7));
				equipment.setAddress(resultSet.getString(8));
				equipment.setJingdu(resultSet.getDouble(9));
				equipment.setWeidu(resultSet.getDouble(10));
				equipment.setPhone(resultSet.getString(11));

			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return equipment;

	}

}
