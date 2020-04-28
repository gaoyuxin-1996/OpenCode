package com.sun.dao.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;

import com.sun.dao.DeviceDataDAO;
import com.sun.model.DeviceData;
import com.sun.temp.TempData;
import com.sun.util.JDBCUtil;
//仅仅实现插入和删除
public class DeviceDataDAOImpl implements DeviceDataDAO{
    private JDBCUtil jdbcUtil=JDBCUtil.getInitJDBCUtil();
	@Override
	public int insertDeviceData(DeviceData devicedata) throws SQLException {
		// TODO Auto-generated method stub
		//插入记录
		int n=0;
		//String sql="INSERT INTO t_Device VALUES (?,?,?,?,?,?,?,?,?,?)";//notnull
		//使用之前一定要赋值赋值无法查询到对应的表
		String sql="INSERT INTO"+" "+TempData.getInstance().Tb_name+" "+"VALUES (?,?,?,?,?,?,?,?,?,?)";
		Object[] objects= {devicedata.getNodeId(),devicedata.getName(),devicedata.getManufacturerId(),
				           devicedata.getManufacturerName(),devicedata.getDeviceType(),
				           devicedata.getModel(),devicedata.getServiceId(),devicedata.getData(),
				           devicedata.getEventTime(),devicedata.getStatus()};
		n=jdbcUtil.executeUpdate(sql, objects);
		return n;
	}

	@Override
	public int deleteDevicedata(String nodeId) throws SQLException {
		// TODO Auto-generated method stub
		//根据设备imei进行记录删除
		int n=0;
		//String sql="DELETE FROM t_Device WHERE nodeId = ?";
		String sql="DELETE FROM "+" "+TempData.getInstance().Tb_name+" "+"WHERE nodeId = ?";
		Object[] objects= {nodeId};
		n=jdbcUtil.executeUpdate(sql,objects);	
		return n;
	}

	@Override
	public int updateDevicedata(DeviceData devicedata) throws SQLException {
		// TODO Auto-generated method stub
		//每次消息订阅只用来更新数据 事件时间和当前设备状态。
		int n=0;
		//String sql="UPDATE t_Device SET data = ?,eventTime = ?,status = ? WHERE nodeId = ?";
		String sql="UPDATE "+" "+TempData.getInstance().Tb_name+" "+" SET data = ?,eventTime = ?,status = ? WHERE nodeId = ?";
		System.out.println(sql);
		Object[] objects= {devicedata.getData(),devicedata.getEventTime(),devicedata.getStatus(),devicedata.getNodeId()};
		n=jdbcUtil.executeUpdate(sql, objects);
		System.out.println(n);
		return n;
	}

	@Override
	public DeviceData searchByNodeId(String nodeId) throws SQLException {
		// TODO Auto-generated method stub
		Connection connection = jdbcUtil.getConnection();	
		PreparedStatement statement = null;
        ResultSet set = null;
        String sql="SELECT * FROM"+" "+TempData.getInstance().Tb_name+" "+"WHERE nodeId = ?";
        statement = connection.prepareStatement(sql);
        statement.setString(1,nodeId);//对应的问号的位置
        set = statement.executeQuery();
		DeviceData devicedata=null;
		while(set.next()) {
			devicedata=new DeviceData();
			devicedata.setData(set.getString("data"));
			devicedata.setDeviceType(set.getString("deviceType"));
			devicedata.setEventTime(set.getString("eventTime"));
			devicedata.setManufacturerId(set.getString("manufacturerId"));
			devicedata.setManufacturerName(set.getString("manufacturerName"));
			devicedata.setModel(set.getString("model"));
			devicedata.setName(set.getString("name"));
			devicedata.setNodeId(set.getString("nodeId"));
			devicedata.setServiceId(set.getString("serviceId"));
			devicedata.setStatus(set.getString("status"));
		}
		set.close();
		statement.close();
		connection.close();		
		return devicedata;
	}

	@Override
	public List<DeviceData> getALLRecords() throws SQLException {
		// TODO Auto-generated method stub
		return null;
	}

}
