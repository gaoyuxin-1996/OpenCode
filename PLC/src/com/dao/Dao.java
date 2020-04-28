package com.dao;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;

import javax.swing.JOptionPane;

public class Dao {
	protected static String dbClassName = "com.mysql.jdbc.Driver";
	protected static String dbUrl = "jdbc:mysql:"
			+ "//47.105.178.154:3306/plc?useUnicode=true&characterEncoding=utf8&autoReconnect=true";
	protected static String dbUser = "root";
	protected static String dbPwd = "admin";
	protected static String second = null;
	public static Statement sql;
	static ResultSet resultSet;
	public static PreparedStatement psql;
	public static Connection conn = null;

	static {

		try {
			if (conn == null) {
				Class.forName(dbClassName).newInstance();
				conn = DriverManager.getConnection(dbUrl, dbUser, dbPwd);
			}
		} catch (ClassNotFoundException e) {
			e.printStackTrace();
			JOptionPane.showMessageDialog(null, "缺少My SQl 驱动");
			System.exit(-1);
		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	public Dao() {
	}

}
