package com.sun.service;

import java.util.List;

import com.sun.model.DeviceData;

public interface DeviceDataService {
	/*
	 * 
	 * 
	 * **/
	boolean addDeviceData(DeviceData devicedata);
    boolean deleteDeviceData(String nodeId);
    boolean updateDeviceData(DeviceData devicedata);
    boolean isExist(String nodeId);
    DeviceData searchDeviceData(String nodeId);
    List<DeviceData> getALL();
    
}
