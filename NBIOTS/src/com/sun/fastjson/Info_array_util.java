package com.sun.fastjson;

public class Info_array_util {
	private String serviceId,
               serviceType,
               data,
               eventTime,
               serviceInfo;
public String toString() {	
	
	return "serviceId="+serviceId+
			",serviceType="+serviceType+
			",data="+data+
			",eventTime="+eventTime+
			",serviceInfo="+serviceInfo;
}
public String getServiceId() {
    return serviceId;
}

public void setServiceId(String serviceId) {
    this.serviceId = serviceId;
}
public String getServiceType() {
    return  serviceType;
}

public void setServiceType(String  serviceType) {
    this.serviceType = serviceType;
}

public String getData() {
    return  data;
}

public void setData(String  data) {
    this.data = data;
}

public String getEventTime() {
    return  eventTime;
}

public void setEventTime(String eventTime) {
    this.eventTime = eventTime;
}
public String getServiceInfo() {
    return  serviceInfo;
}

public void setServiceInfo(String serviceInfo) {
    this.serviceInfo = serviceInfo;
}


}
