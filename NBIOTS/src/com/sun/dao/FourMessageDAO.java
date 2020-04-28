package com.sun.dao;

import java.sql.SQLException;

import com.sun.model.FourMessage;

public interface FourMessageDAO {
	//查询用户
	 FourMessage searchByImei(String imei) throws SQLException ;
		// TODO Auto-generated method stub
	

}
