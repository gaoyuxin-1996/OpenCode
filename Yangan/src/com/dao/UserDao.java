package com.dao;

import java.util.ArrayList;
import java.util.List;

import com.item.User;

public class UserDao extends Dao {

	public int login(User user) {
		int a = 0;
		try {
			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from myclass where ph1='" + user.getPh1() + "' and password='"
					+ user.getPassword() + "'");
			if (resultSet.next()) {

				user.setPassword(resultSet.getString(2));
				a = 1;
			} else {
				resultSet = sql.executeQuery("select * from myclass where ph2='" + user.getPh1() + "' and password1='"
						+ user.getPassword1() + "'");
				if (resultSet.next()) {

					user.setPassword(resultSet.getString(2));
					a = 1;
				}
			}

		} catch (Exception e) {
			e.printStackTrace();
			a = 0;
		}
		return a;

	}

	public int select(User user) {
		int a = 0;
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from myclass where ph1='" + user.getPh1() + "'");
			if (resultSet.next()) {
				a = 1;
			} else {
				resultSet = sql.executeQuery("select * from myclass where ph2='" + user.getPh1() + "'");
				if (resultSet.next()) {
					a = 2;
				} else {
					a = 0;
				}
			}

		} catch (Exception e) {
			e.printStackTrace();
			a = 0;
		}
		return a;

	}

	public int fand(User user) {
		int a = 0;
		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from myclass where weixinid='" + user.getWeixinid()
					+ "' or weixinid1='" + user.getWeixinid() + "'");
			if (resultSet.next()) {
				a = 1;
			}

		} catch (Exception e) {
			e.printStackTrace();
			a = 0;
		}
		return a;

	}

	public User fandxiangqing(User user) {

		try {

			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from myclass where imei='" + user.getImei() + "'");
			if (resultSet.next()) {
				user.setImei(resultSet.getString(1));
				user.setPh1(resultSet.getString(2));
				user.setPh2(resultSet.getString(3));
				user.setLoc(resultSet.getString(4));
				user.setWeixinid(resultSet.getString(5));
				user.setPassword(resultSet.getString(7));
				user.setWeixinid1(resultSet.getString(6));
				user.setPassword1(resultSet.getString(8));

			}
		} catch (Exception e) {
			e.printStackTrace();

		}
		return user;

	}

	public List<String> Xinxi(User user) {
		List<String> list = new ArrayList<String>();
		try {
			sql = conn.createStatement();
			resultSet = sql.executeQuery(
					"select * from myclass where ph1='" + user.getPh1() + "' or ph2='" + user.getPh1() + "'");
			if (resultSet.next()) {
				list.add(resultSet.getString(1));

			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;

	}

	public List<User> weixinall(User user) {
		List<User> list = new ArrayList<User>();
		try {
			sql = conn.createStatement();
			resultSet = sql.executeQuery("select * from myclass where weixinid='" + user.getWeixinid()
					+ "' or weixinid1='" + user.getWeixinid() + "'");
			if (resultSet.next()) {
				user.setImei(resultSet.getString(1));
				user.setPh1(resultSet.getString(2));
				user.setPh2(resultSet.getString(3));
				user.setLoc(resultSet.getString(4));
				user.setWeixinid(resultSet.getString(5));
				user.setPassword(resultSet.getString(7));
				user.setWeixinid1(resultSet.getString(6));
				user.setPassword1(resultSet.getString(8));
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
			resultSet = sql.executeQuery("select * from myclass ");
			while (resultSet.next()) {
				User user = new User();
				user.setImei(resultSet.getString(1));
				user.setPh1(resultSet.getString(2));
				user.setPh2(resultSet.getString(3));
				user.setLoc(resultSet.getString(4));
				user.setWeixinid(resultSet.getString(5));
				user.setPassword(resultSet.getString(7));
				user.setWeixinid1(resultSet.getString(6));
				user.setPassword1(resultSet.getString(8));
				list.add(user);

			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;

	}

	public int mima(User user) {
		int a = 0;
		try {
			sql = conn.createStatement();

			res = sql.execute(
					"update myclass set password = '" + user.getPassword() + "' where ph1 ='" + user.getPh1() + "'");

			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
			a = 0;
		}

		return a;

	}

	public int bangdingph1(User user) {
		int a = 0;
		try {
			sql = conn.createStatement();

			res = sql.execute(
					"update myclass set weixinid = '" + user.getWeixinid() + "' where ph1 ='" + user.getPh1() + "'");

			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
			a = 0;
		}

		return a;

	}

	public int bangdingph2(User user) {
		int a = 0;
		try {
			sql = conn.createStatement();

			res = sql.execute(
					"update myclass set weixinid1 = '" + user.getWeixinid() + "' where ph2 ='" + user.getPh1() + "'");

			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
			a = 0;
		}

		return a;

	}

	public int updataph(User user) {
		int a = 0;
		try {
			sql = conn.createStatement();

			res = sql.execute("update myclass set ph1 = '" + user.getPh2() + "' where ph1 ='" + user.getPh1() + "'");

			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
			a = 0;
		}

		return a;

	}

	public int updataph1(User user) {
		int a = 0;
		try {
			sql = conn.createStatement();

			res = sql.execute("update myclass set ph2 = '" + user.getPh2() + "' where ph2 ='" + user.getPh1() + "'");

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
			res = sql.execute("delete from user where name ='" + aString + "'");

			a = 1;
		} catch (Exception e) {
			e.printStackTrace();
		}

		return a;

	}
}
