package com.sun.factory;

import com.sun.dao.DeviceDataDAO;
import com.sun.dao.FourMessageDAO;
import com.sun.dao.impl.DeviceDataDAOImpl;
import com.sun.dao.impl.FourMessageDAOImpl;
import com.sun.model.DeviceData;

public class DAOFactory {
	public static DeviceDataDAO getDeviceData() {
		return new DeviceDataDAOImpl();
	}
    public static  FourMessageDAO getFourMessage() {
    	return new FourMessageDAOImpl();
    }
}
