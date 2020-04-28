package com.dao;

import com.item.xinxi;

public class xinxiDao extends Dao {

	public int insert(xinxi xinxi) {

		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("insert into xinxi(gid,ptid,cid,ponits) values('" + xinxi.getGid() + "','"
					+ xinxi.getPtid() + "','" + xinxi.getCid() + "','" + xinxi.getPonits() + "')");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
		}

		return a;

	}

	public int insertall(xinxi xinxi) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("insert into xinxiall(gid,ptid,cid,ponits) values('" + xinxi.getGid() + "','"
					+ xinxi.getPtid() + "','" + xinxi.getCid() + "','" + xinxi.getPonits() + "')");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
		}

		return a;

	}

	public int fand(xinxi xinxi) {
		int a = 0;
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select cid from xinxi where gid='" + xinxi.getGid() + "' and ptid = '"
					+ xinxi.getPtid() + "' and cid = '" + xinxi.getCid() + "'");
			if (resultSet.next()) {
				a = 1;
			}

		} catch (Exception e) {
			e.printStackTrace();
			a = 0;
		}
		return a;

	}

	public String xinxi() {
		String ptid = null;
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select ptid from xinxi where gid = '1'");
			if (resultSet.next()) {
				ptid = resultSet.getString(1);
			}

		} catch (Exception e) {
			e.printStackTrace();

		}
		return ptid;

	}

	public xinxi ptid(xinxi xinxi) {
		int a = 0;
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from xinxi where ptid = '" + xinxi.getPtid() + "'");
			if (resultSet.next()) {
				xinxi.setGid(resultSet.getString(1));
				xinxi.setPtid(resultSet.getString(2));
				xinxi.setCid(resultSet.getString(3));
				xinxi.setPonits(resultSet.getString(4));
			}

		} catch (Exception e) {
			e.printStackTrace();
			a = 0;
		}
		return xinxi;

	}

	public int update(xinxi xinxi) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("update xinxi set ponits = '" + xinxi.getPonits() + "' where gid = '"
					+ xinxi.getGid() + "' and cid = '" + xinxi.getCid() + "' and ptid = '" + xinxi.getPtid() + "'");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			a = 0;
			e.printStackTrace();
		}

		return a;

	}

	public int update1(xinxi xinxi) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("update xinxi set ptid = '" + xinxi.getPtid() + "' where gid = '1'");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			a = 0;
			e.printStackTrace();
		}

		return a;

	}
}
