package com.sun.service.impl;

import java.sql.SQLException;
import java.util.List;

import com.huawei.utils.StringUtil;
import com.sun.dao.DeviceDataDAO;
import com.sun.factory.DAOFactory;
import com.sun.model.DeviceData;
import com.sun.service.DeviceDataService;

public class DeviceDataServiceImpl implements DeviceDataService{
    private DeviceDataDAO deviceDAO=DAOFactory.getDeviceData();
    
	@Override
	public boolean addDeviceData(DeviceData devicedata) {
		// TODO Auto-generated method stub
		boolean flag=false;
		try {
			if(deviceDAO.insertDeviceData(devicedata)==1) {
				flag=true;
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return flag;
	}
    
	@Override
	public boolean deleteDeviceData(String nodeId) {
		// TODO Auto-generated method stub
		boolean flag=false;
		try {
			if(deviceDAO.deleteDevicedata(nodeId)==1) {
				flag=true;		
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return flag;
	}

	@Override
	public boolean updateDeviceData(DeviceData devicedata) {
		// TODO Auto-generated method stub
		boolean flag=false;
		try {
			if(deviceDAO.updateDevicedata(devicedata)==1) {
				
				flag=true;
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return flag;
	}

	@Override
	public boolean isExist(String nodeId) {
		// TODO Auto-generated method stub
		boolean flag=false;
		try {
			System.out.println("进行数据库查询");
			DeviceData da=deviceDAO.searchByNodeId(nodeId);
			System.out.println("数据库查询完毕");
			/*if(StringUtil.strIsNullOrEmpty(da.getData())) {
				System.out.println(da.getData());	
				flag=true;				
			}*/
			if(deviceDAO.searchByNodeId(nodeId)!=null) {
				System.out.println(da.getEventTime());				
				flag=true;
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return flag;
	}

	@Override
	public DeviceData searchDeviceData(String nodeId) {
		
		// TODO Auto-generated method stub
		DeviceData devicedatads=null;
		try {
			devicedatads=deviceDAO.searchByNodeId(nodeId);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return devicedatads;
	}

	@Override
	public List<DeviceData> getALL() {
		// TODO Auto-generated method stub
		List<DeviceData> devicedatas=null;
		try {
			devicedatas=deviceDAO.getALLRecords();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return devicedatas;
	}
	

}
