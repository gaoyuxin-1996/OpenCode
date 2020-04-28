package com.sun.model;

public class FourMessage {
	
private String imei,ph1,ph2,loc;
	
	public FourMessage(){}
	public String toString(){
		return "imei="+imei+
				"ph1="+ph1+
				"ph2="+ph2+
				"loc="+loc;
	}
	
	public String getImei(){
		return imei;
	}
	
	public void setImei(String imei){
		this.imei=imei;
	}
	
	public String getPh1(){
		return ph1;
	}
	
	public void setPh1(String ph1){
		this.ph1=ph1;
	}
	
	public String getPh2(){
		return ph2;
	}
	
	public void setPh2(String ph2){
		this.ph2=ph2;
	}
	
	public String getLoc(){
		return loc;
	}
	
	public void setLoc(String loc){
		this.loc=loc;
	}

}
