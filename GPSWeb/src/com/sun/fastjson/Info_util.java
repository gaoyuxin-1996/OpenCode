/*
 *根据json复杂程度进行修改
 * */
package com.sun.fastjson;
public class Info_util {
	
	private String 
	deviceId,
	gatewayId,
	nodeType,
	createTime,
	lastModifiedTime,
	deviceInfo,
	services,
	connectionInfo,
	devGroupIds;
	@Override
	public String toString() {
			
		return "deviceId="+ deviceId +
				",gatewayId="+ gatewayId +
				",nodeType="+ nodeType +
				",createTime="+ createTime +
				",lastModifiedTime="+ lastModifiedTime +
				",deviceInfo="+ deviceInfo +
				",services="+ services +
				",connectionInfo="+ connectionInfo +
				",devGroupIds="+ devGroupIds;
		}
	
	public String getDeviceId(){
		
		return deviceId;
	}
	public void setDeviceId(String deviceId){
		this.deviceId=deviceId;
		
	}	
	public String getGatewayId(){
		
		return gatewayId;
	}
	public void setGatewayId(String gatewayId){
		this.gatewayId=gatewayId;		
	}
	public String getNodeType(){
		
		return nodeType;
	}
	public void setNodeType(String nodeType){
		this.nodeType=nodeType;
		
	}
	public String getLastModifiedTime(){
		
		return lastModifiedTime;
	}
	public void setLastModifiedTime(String lastModifiedTime){
		this.lastModifiedTime=lastModifiedTime;
		
	}
	public String getCreateTime(){
		
		return createTime;
	}
	public void setCreateTime(String createTime){
		this.createTime=createTime;
		
	}
	public String getDeviceInfo(){
		
		return deviceInfo;
	}
	public void setDeviceInfo(String deviceInfo){
		this.deviceInfo=deviceInfo;
		
	}
	public String getServices(){
		
		return services;
	}
	public void setServices(String services){
		this.services=services;
		
	}
public String getConnectionInfo(){
		
		return connectionInfo;
	}
	public void setConnectionInfo(String connectionInfo){
		this.connectionInfo=connectionInfo;
		
	}
public String getDevGroupIds(){
		
		return devGroupIds;
	}
	public void setDevGroupIds(String devGroupIds){
		this.devGroupIds=devGroupIds;
		
	}
	
	
	
	
	
	

}
