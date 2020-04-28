package com.dao;

import java.util.ArrayList;
import java.util.List;

import com.item.equipment;

public class equipmentDao extends Dao1 {

	public List<equipment> XinxiAll(List<String> list1) {

		List<equipment> list = new ArrayList<equipment>();

		try {
			sql = conn.createStatement();
			int a = list1.size();
			for (int c = 0; c < a; c++) {
				resultSet = sql.executeQuery("select * from t_Datas where nodeId = '" + list1.get(c) + "'");
				while (resultSet.next()) {
					equipment equipment = new equipment();
					equipment.setNodeId(resultSet.getString(1));
					equipment.setName(resultSet.getString(2));
					equipment.setManufacturerId(resultSet.getString(3));
					equipment.setManufacturerName(resultSet.getString(4));
					equipment.setDeviceType(resultSet.getString(5));
					equipment.setModel(resultSet.getString(6));
					equipment.setServiceId(resultSet.getString(7));
					equipment.setData(resultSet.getString(8));
					equipment.setEventTime(resultSet.getString(9));
					equipment.setStatus(resultSet.getString(10));

					list.add(equipment);

				}
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
				equipment.setNodeId(resultSet.getString(1));
				equipment.setName(resultSet.getString(2));
				equipment.setManufacturerId(resultSet.getString(3));
				equipment.setManufacturerName(resultSet.getString(4));
				equipment.setDeviceType(resultSet.getString(5));
				equipment.setModel(resultSet.getString(6));
				equipment.setServiceId(resultSet.getString(7));
				equipment.setData(resultSet.getString(8));
				equipment.setEventTime(resultSet.getString(9));
				equipment.setStatus(resultSet.getString(10));
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

	// public int update(equipment equipment) {
	// int a = 0;
	// try {
	// sql = conn.createStatement();
	//
	// res = sql.execute("update equipment set name = '" + equipment.getName() +
	// "'," + "phone ='"
	// + equipment.getPhone() + "'," + "leixing = '" + equipment.getLeixing() + "',"
	// + "operator = '"
	// + equipment.getOperator() + "'," + "glcompany = '" + equipment.getGlcompany()
	// + "'," + "jingdu = '"
	// + equipment.getJingdu() + "'," + "weidu = '" + equipment.getWeidu() + "'," +
	// "address = '"
	// + equipment.getAddress() + "'" + " where id = '" + equipment.getId() + "'");
	//
	// a = 1;
	// } catch (Exception e) {
	// a = 0;
	// e.printStackTrace();
	// }
	//
	// return a;
	//
	// }
	//
	// public void updategps(equipment equipment) {
	// int a = 0;
	// try {
	// sql = conn.createStatement();
	//
	// res = sql.execute("update dingwei set jingdu = '" + equipment.getJingdu() +
	// "'," + "weidu = '"
	// + equipment.getWeidu() + "' where id = '" + equipment.getId() + "'");
	//
	// a = 1;
	// } catch (Exception e) {
	// a = 0;
	// e.printStackTrace();
	// }
	//
	// return;
	//
	// }
	//
	// public int insert(equipment equipment) {
	// int a = 0;
	// try {
	// sql = conn.createStatement();
	//
	// res = sql.execute("insert into equipment values('" + equipment.getId() +
	// "','" + equipment.getLeixing()
	// + "','" + equipment.getName() + "','','" + equipment.getOperator() + "','"
	// + equipment.getGlcompany() + "','" + equipment.getAddress() + "','" +
	// equipment.getJingdu() + "','"
	// + equipment.getWeidu() + "','" + equipment.getPhone() + "')");
	// res = sql.execute("insert into dingwei values('" + equipment.getId() + "','"
	// + equipment.getJingdu() + "','"
	// + equipment.getWeidu() + "')");
	//
	// a = 1;
	// } catch (Exception e) {
	// e.printStackTrace();
	// }
	//
	// return a;
	//
	// }
	//
	// public equipment Xinxi(equipment equipment) {
	// try {
	// sql = conn.createStatement();
	// resultSet = sql.executeQuery(
	// "select
	// e.id,d.id,e.name,e.leixing,e.state,e.operator,e.glcompany,e.address,d.jingdu,d.weidu,e.phone
	// from equipment e left join dingwei d on e.id=d.id where e.id ='"
	// + equipment.getId() + "'");
	// if (resultSet.next()) {
	// equipment.setId(resultSet.getString(1));
	// equipment.setLeixing(resultSet.getString(4));
	// equipment.setName(resultSet.getString(3));
	// equipment.setState(resultSet.getString(5));
	// equipment.setOperator(resultSet.getString(6));
	// equipment.setGlcompany(resultSet.getString(7));
	// equipment.setAddress(resultSet.getString(8));
	// equipment.setJingdu(resultSet.getDouble(9));
	// equipment.setWeidu(resultSet.getDouble(10));
	// equipment.setPhone(resultSet.getString(11));
	//
	// }
	//
	// } catch (Exception e) {
	// e.printStackTrace();
	// }
	// return equipment;
	//
	// }

}
