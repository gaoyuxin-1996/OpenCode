package com.sun.dao.impl;

import java.sql.SQLException;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

import com.sun.dao.FourMessageDAO;
import com.sun.model.FourMessage;
import com.sun.temp.TempData;
import com.sun.util.JDBCUtil;

public class FourMessageDAOImpl implements FourMessageDAO {
	private JDBCUtil jdbcUtil=JDBCUtil.getInitJDBCUtil();
	//通过IMEI(NOdID)来查找
	public FourMessage searchByImei(String imei) throws SQLException{
		Connection connection = jdbcUtil.getConnection();	
		PreparedStatement statement = null;
        ResultSet set = null;
        //String sql = "SELECT * FROM t_goods WHERE g_name = ?";
        //testdb ->myclass
        String sql="SELECT * FROM"+" "+TempData.getInstance().Tb_name+" "+"WHERE imei = ?";
        statement = connection.prepareStatement(sql);
        statement.setString(1,imei);
        set = statement.executeQuery();
        FourMessage fourmessage=null;
        while(set.next()) {
        	fourmessage=new FourMessage();
        	fourmessage.setImei(set.getString("imei"));
        	fourmessage.setPh1(set.getString("ph1"));
        	fourmessage.setPh2(set.getString("ph2"));
        	fourmessage.setLoc(set.getString("loc"));
        	
        }      
        set.close();
        statement.close();
        connection.close();
		return fourmessage;
	}
}
