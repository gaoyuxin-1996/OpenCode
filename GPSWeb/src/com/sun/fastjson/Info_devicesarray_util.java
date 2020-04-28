package com.sun.fastjson;

public class Info_devicesarray_util {
	
	private int totalCount,pageNo,pageSize;
	private String devices;
	public String toString()
	{
		
		return "totalCount="+totalCount+
				"pageNo="+pageNo+
				"pageSize="+pageSize+
				"devices="+devices;
		
	}
	
	public String getDevices(){
		
		return devices;
	}
	public void setDevices(String devices){
		this.devices=devices;		
	}	
	
	public int getTotalCount() {
		
		return totalCount;
		
	}
	public void setTotalCount(int totalCount) {
		this.totalCount=totalCount;		
		
	}
	public int getPageNo() {
		
		return pageNo;
		
	}
	public void setPageNo(int pageNo) {
		this.pageNo=pageNo;		
		
	}
	public int getPageSize() {
		
		return pageSize;
		
	}
	public void setPageSize(int pageSize) {
		this.pageSize=pageSize;		
		
	}
	
	
	
	
	
	
	 
}
