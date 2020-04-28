package com.sun.factory;

import com.sun.service.DeviceDataService;
import com.sun.service.FourMessageService;
import com.sun.service.impl.DeviceDataServiceImpl;
import com.sun.service.impl.FourMessageServiceImpl;

public class ServiceFactory {
	public static DeviceDataService getDeviceDataService() {		
		return new DeviceDataServiceImpl();		
	}
	public static FourMessageService getFourMessageService() {
		return new FourMessageServiceImpl();
	}	
}
