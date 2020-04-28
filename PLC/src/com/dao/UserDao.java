package com.dao;

import java.util.ArrayList;
import java.util.List;

import com.item.User;

public class UserDao extends Dao {

	public int login(User user) {
		int a = 0;
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery(
					"select * from user where name='" + user.getName() + "' and password='" + user.getPassword() + "'");
			if (resultSet.next()) {
				user.setName(resultSet.getString(1));
				user.setPassword(resultSet.getString(2));
				a = 1;
			}

		} catch (Exception e) {
			System.out.println("5");
			e.printStackTrace();
			a = 0;
		}
		return a;

	}

	public int fand(User user) {
		int a = 0;
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from user where weixinid='" + user.getWeixinid() + "'");
			if (resultSet.next()) {
				a = 1;
			}

		} catch (Exception e) {
			e.printStackTrace();
			a = 0;
		}
		return a;

	}

	public User Xinxi(User user) {
		List<User> list = new ArrayList<User>();
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from user where name ='" + user.getName() + "'");
			if (resultSet.next()) {
				user.setName(resultSet.getString(1));
				user.setPassword(resultSet.getString(2));
				user.setXingming(resultSet.getString(3));
				user.setAddress(resultSet.getString(4));
				user.setPhone(resultSet.getString(5));
				user.setGongsi(resultSet.getString(6));
				user.setBumen(resultSet.getString(7));
				user.setEmail(resultSet.getString(8));
				user.setQuanxian(resultSet.getInt(9));

			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return user;

	}

	public List<User> weixinall(User user) {
		List<User> list = new ArrayList<User>();
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from user where weixinid ='" + user.getWeixinid() + "'");
			if (resultSet.next()) {
				user.setName(resultSet.getString(1));
				user.setPassword(resultSet.getString(2));
				user.setXingming(resultSet.getString(3));
				user.setAddress(resultSet.getString(4));
				user.setPhone(resultSet.getString(5));
				user.setGongsi(resultSet.getString(6));
				user.setBumen(resultSet.getString(7));
				user.setEmail(resultSet.getString(8));
				user.setQuanxian(resultSet.getInt(9));
				list.add(user);
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;

	}

	public List<User> XinxiAll() {
		List<User> list = new ArrayList<User>();
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from user ");
			while (resultSet.next()) {
				User user = new User();
				user.setName(resultSet.getString(1));
				user.setPassword(resultSet.getString(2));
				user.setXingming(resultSet.getString(3));
				user.setAddress(resultSet.getString(4));
				user.setPhone(resultSet.getString(5));
				user.setGongsi(resultSet.getString(6));
				user.setBumen(resultSet.getString(7));
				user.setEmail(resultSet.getString(8));
				user.setQuanxian(resultSet.getInt(9));
				list.add(user);

			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;

	}

	public int insert(User user) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("insert into user values('" + user.getName() + "','" + user.getPassword()
					+ "','" + user.getXingming() + "','" + user.getAddress() + "','" + user.getPhone() + "','"
					+ user.getGongsi() + "','" + user.getBumen() + "','" + user.getEmail() + "','" + user.getQuanxian()
					+ "','')");
			psql.executeUpdate();
			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
		}

		return a;

	}

	public int update(User user) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("update user set name = '" + user.getName() + "'," + "phone ='"
					+ user.getPhone() + "'," + "password = '" + user.getPassword() + "'," + "email = '"
					+ user.getEmail() + "'," + "gongsi = '" + user.getGongsi() + "'," + "bumen = '" + user.getBumen()
					+ "'," + "quanxian = '" + user.getQuanxian() + "'," + "xingming = '" + user.getXingming() + "',"
					+ "address = '" + user.getAddress() + "'" + " where name = '" + user.getName() + "'");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
		}

		return a;

	}

	public int mima(User user) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement(
					"update user set password = '" + user.getPassword() + "' where name ='" + user.getName() + "'");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
			a = 0;
		}

		return a;

	}

	public int updataid(User user) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement(
					"update user set weixinid = '" + user.getWeixinid() + "' where name ='" + user.getName() + "'");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
			a = 0;
		}

		return a;

	}

	public int del(String aString) {
		int a = 0;
		try {

			sql = conn.createStatement();
			psql = conn.prepareStatement("delete from user where name ='" + aString + "'");
			psql.executeUpdate();

			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
		}

		return a;

	}
}
