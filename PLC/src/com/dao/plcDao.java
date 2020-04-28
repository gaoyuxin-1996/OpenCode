package com.dao;

import java.util.ArrayList;
import java.util.List;

import com.item.equipment;

public class plcDao extends Dao {

	public List<String> plcname() {

		List<String> list = new ArrayList<String>();
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select id from plc");
			while (resultSet.next()) {
				list.add(resultSet.getString(1));
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;

	}

	public equipment plcbyid(equipment equipment) {

		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from plc where id = '" + equipment.getPlcid() + "'");
			while (resultSet.next()) {
				equipment.setPlcid(resultSet.getInt(1));
				equipment.setPlcname(resultSet.getString(2));
				equipment.setCategory(resultSet.getString(3));
				equipment.setXilie(resultSet.getString(4));
				equipment.setVendor(resultSet.getString(5));
				equipment.setPlcbrand(resultSet.getString(6));
				equipment.setPlcxilie(resultSet.getString(7));
				equipment.setAgreement(resultSet.getString(8));
				equipment.setWay1(resultSet.getString(9));
				equipment.setTrigger(resultSet.getString(10));
				equipment.setOrdinary(resultSet.getString(11));
				equipment.setStorage(resultSet.getString(12));
				equipment.setPlccreatename(resultSet.getString(13));
				equipment.setPlcdate(resultSet.getString(14));
				equipment.setPlcupdatename(resultSet.getString(15));
				equipment.setPlcupdatedate(resultSet.getDate(16));

			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return equipment;
	}

	public equipment plcbyid1(equipment equipment) {

		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from plc where id = '" + equipment.getPlcid() + "'");
			while (resultSet.next()) {
				equipment.setPlcid(resultSet.getInt(1));
				equipment.setPlcname(resultSet.getString(2));
				equipment.setCategory(resultSet.getString(3));
				equipment.setXilie(resultSet.getString(4));
				equipment.setVendor(resultSet.getString(5));
				equipment.setPlcbrand(resultSet.getString(6));
				equipment.setPlcxilie(resultSet.getString(7));
				equipment.setAgreement(resultSet.getString(8));
				equipment.setWay1(resultSet.getString(9));
				equipment.setTrigger(resultSet.getString(10));
				equipment.setOrdinary(resultSet.getString(11));
				equipment.setStorage(resultSet.getString(12));

			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return equipment;
	}

	public List<equipment> plcall() {
		List<equipment> list = new ArrayList<equipment>();
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from plc");
			while (resultSet.next()) {
				equipment equipment = new equipment();
				equipment.setPlcid(resultSet.getInt(1));
				equipment.setPlcname(resultSet.getString(2));
				equipment.setCategory(resultSet.getString(3));
				equipment.setXilie(resultSet.getString(4));
				equipment.setVendor(resultSet.getString(5));
				equipment.setPlcbrand(resultSet.getString(6));
				equipment.setPlcxilie(resultSet.getString(7));
				equipment.setAgreement(resultSet.getString(8));
				equipment.setWay1(resultSet.getString(9));
				equipment.setTrigger(resultSet.getString(10));
				equipment.setOrdinary(resultSet.getString(11));
				equipment.setStorage(resultSet.getString(12));
				equipment.setPlccreatename(resultSet.getString(13));
				equipment.setPlcdate(resultSet.getString(14));
				equipment.setPlcupdatename(resultSet.getString(15));
				equipment.setPlcupdatedate(resultSet.getDate(16));
				list.add(equipment);
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;
	}

	public int insert(equipment equipment) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement(
					"insert into plc(name,category,xilie,vendor,plcbrand,plcxilie,agreement,way,tigger,ordinary,storage,createname,date) values('"
							+ equipment.getName() + "','" + equipment.getCategory() + "','" + equipment.getXilie()
							+ "','" + equipment.getVendor() + "','" + equipment.getPlcbrand() + "','"
							+ equipment.getPlcxilie() + "','" + equipment.getAgreement() + "','" + equipment.getWay1()
							+ "','" + equipment.getTrigger() + "','" + equipment.getOrdinary() + "','"
							+ equipment.getStorage() + "','" + equipment.getCreatename() + "','" + equipment.getDate()
							+ "')");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			a = 0;
			e.printStackTrace();
		}

		return a;

	}

	public int del(String aString) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("delete from plc where id ='" + aString + "'");
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
			psql = conn.prepareStatement("update plc set name = '" + equipment.getName() + "'," + "category ='"
					+ equipment.getCategory() + "'," + "xilie = '" + equipment.getXilie() + "'," + "vendor ='"
					+ equipment.getVendor() + "'," + "tigger = '" + equipment.getTrigger() + "'," + "ordinary = '"
					+ equipment.getOrdinary() + "'," + "storage = '" + equipment.getStorage() + "'," + "updatename = '"
					+ equipment.getUpdatename() + "'" + " where id = '" + equipment.getPlcid() + "'");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			a = 0;
			e.printStackTrace();
		}

		return a;

	}
}
