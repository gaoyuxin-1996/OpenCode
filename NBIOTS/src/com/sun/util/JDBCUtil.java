package com.sun.util;

import java.sql.*;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.sun.temp.TempData;

/**
 * 加载对应的库
 * 阿里云的mysql数据库
 * JDBC辅助�? 用于构建数据库连接（采用单例模式），封装了增、删、改、查的基本功能，方便具体的DAO类实�?
 */
public final class JDBCUtil {
	//private static String url = "jdbc:mysql://localhost:3306/db_NBIOT?characterEncoding=utf8";
	private static String url =null;
	private static String urlpart1="jdbc:mysql://localhost:3306/";
	private static String urlpart2="?characterEncoding=utf8";
	private static String name = "root";
	private static String password = "631572";
	private static Connection connnection = null;
	private PreparedStatement preparedStatement = null;
	private ResultSet resultSet = null;

	private static JDBCUtil jdbcUtil = null;

	public static JDBCUtil getInitJDBCUtil() {
		if (jdbcUtil == null) {
			synchronized (JDBCUtil.class) {   //线程加锁
				if (jdbcUtil == null) {
					jdbcUtil = new JDBCUtil();   //懒汉式加�?
				}
			}
		}
		return jdbcUtil;
	}

	private JDBCUtil() {
	}

	// 通过静�?�代码块注册数据库驱动，保证注册只执行一�?
	static {
		try {
			Class.forName("com.mysql.jdbc.Driver");
		} catch (ClassNotFoundException e) {
			e.printStackTrace();
		}
	}

	// 获得连接
	public Connection getConnection() {
		try {
			url=urlpart1+TempData.getInstance().Db_name+urlpart2;
			connnection = DriverManager.getConnection(url, name, password);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return connnection;

	}

	/**
	 * insert update delete SQL语句的执行的统一方法
	 * @param sql  SQL语句
	 * @param params   参数数组，若没有参数则为null
	 * @return 受影响的行数
	 */
	public int executeUpdate(String sql, Object[] params) {
		// 受影响的行数
		int affectedLine = 0;

		try {
			// 获得连接
			connnection = this.getConnection();
			// 调用SQL
			preparedStatement = connnection.prepareStatement(sql);
			// 参数赋�??
			if (params != null) {
				for (int i = 0; i < params.length; i++) {
					preparedStatement.setObject(i + 1, params[i]);
				}
			}
			// 执行更新操作
			affectedLine = preparedStatement.executeUpdate();

		} catch (SQLException e) {
			System.out.println(e.getMessage());
		} finally {
			// 释放资源
			closeAll();
		}
		return affectedLine;   //返回受影响的记录行数
	}

	/**
	 * SQL 查询将查询结果直接放入ResultSet�?
	 * 
	 * @param sql
	 *            SQL语句
	 * @param params
	 *            参数数组，若没有参数则为null
	 * @return 结果�?
	 */
	private ResultSet executeQueryRS(String sql, Object[] params) {
		try {
			// 获得连接
			connnection = this.getConnection();
			// 调用SQL
			preparedStatement = connnection.prepareStatement(sql);
			// 参数赋�??
			if (params != null) {
				for (int i = 0; i < params.length; i++) {
					preparedStatement.setObject(i + 1, params[i]);
				}
			}
			// 执行
			resultSet = preparedStatement.executeQuery();
		} catch (SQLException e) {
			System.err.println(e.getMessage());
		}
		return resultSet;
	}

	/**
	 * 获取结果集，并将结果放在List�?
	 * @param sql  SQL语句
	 * @return List 结果�?
	 */
	public List<Object> excuteQuery(String sql, Object[] params) {
		ResultSet rs = executeQueryRS(sql, params);// 执行SQL获得结果�?
		ResultSetMetaData rsmd = null;  // ResultSetMetaData接口, 用于获取关于ResultSet 对象中列的类型和属�?�信息的对象
		int columnCount = 0;// 结果集列�?
		try {
			rsmd = rs.getMetaData();  //获得结果集的元数据信�?
			columnCount = rsmd.getColumnCount();	// 获得结果集列�?
		} catch (SQLException e1) {
			System.err.println(e1.getMessage());
		}
		// 创建List
		List<Object> list = new ArrayList<Object>();
		try {
			// 以下代码将ResultSet的结果保存到List�?
			while (rs.next()) {  //循环遍历�?有的记录
				Map<String, Object> map = new HashMap<String, Object>();
				for (int i = 1; i <= columnCount; i++) {  //循环遍历�?条记录的�?有列
					map.put(rsmd.getColumnLabel(i), rs.getObject(i));  //将列名�?�对应列的�?�放进map对象
				}
				list.add(map);  //将一条记录放进List
			}
		} catch (SQLException e) {
			System.err.println(e.getMessage());
		} finally {
			// 关闭�?有资�?
			closeAll();
		}
		return list;
	}

	/**
	 * SQL 查询将查询结果：�?�?
	 * @param sql   SQL语句
	 * @param params  参数数组，若没有参数则为null
	 * @return 结果�?
	 */
	public Object executeQuerySingle(String sql, Object[] params) {
		Map<String, Object> map = new HashMap<String, Object>();
		try {
			// 获得连接
			connnection = this.getConnection();
			// 调用SQL
			preparedStatement = connnection.prepareStatement(sql);
			// 参数赋�??
			if (params != null) {
				for (int i = 0; i < params.length; i++) {
					preparedStatement.setObject(i + 1, params[i]);
				}
			}
			// 执行查询
			resultSet = executeQueryRS(sql, params);
			//resultSet = preparedStatement.executeQuery();
			ResultSetMetaData rsmd = null;
			// 结果集列�?
			int columnCount = 0;
			try {
				rsmd = resultSet.getMetaData();  //获取结果集的元数据信息，各个列的数据
				// 获得结果集列�?
				columnCount = rsmd.getColumnCount();
			} catch (SQLException e1) {
				System.err.println(e1.getMessage());
			}
			if (resultSet.next()) {  //找到�?行记�?
				for (int i = 1; i <= columnCount; i++) {  //遍历该行记录的所有列
					map.put(rsmd.getColumnLabel(i), resultSet.getObject(i));
				}
			}

		} catch (SQLException e) {
			System.out.println(e.getMessage());
		} finally {
			closeAll();
		}
		return map;
	}

	/**
	 * 关闭�?有资�?
	 */
	public void closeAll() {
		// 关闭结果集对�?
		if (resultSet != null) {
			try {
				resultSet.close();
			} catch (SQLException e) {
				System.out.println(e.getMessage());
			}
		}

		// 关闭PreparedStatement对象
		if (preparedStatement != null) {
			try {
				preparedStatement.close();
			} catch (SQLException e) {
				System.out.println(e.getMessage());
			}
		}

		// 关闭Connection 对象
		if (connnection != null) {
			try {
				connnection.close();
			} catch (SQLException e) {
				System.out.println(e.getMessage());
			}
		}
	}

}
