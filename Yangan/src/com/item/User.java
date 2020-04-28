package com.item;

public class User {

	private String imei;
	private String ph1;
	private String ph2;
	private String loc;
	private String weixinid;
	private String weixinid1;
	private String password;
	private String password1;

	public User() {

	}

	public User(String imei, String ph1, String ph2, String loc, String weixinid, String weixinid1, String password,
			String password1) {
		this.imei = imei;
		this.ph1 = ph1;
		this.ph2 = ph2;
		this.loc = loc;
		this.weixinid = weixinid;
		this.weixinid1 = weixinid1;
		this.password = password;
		this.password1 = password1;
	}

	public String getImei() {
		return imei;
	}

	public void setImei(String imei) {
		this.imei = imei;
	}

	public String getPh1() {
		return ph1;
	}

	public String getWeixinid() {
		return weixinid;
	}

	public void setWeixinid(String weixinid) {
		this.weixinid = weixinid;
	}

	public String getWeixinid1() {
		return weixinid1;
	}

	public void setWeixinid1(String weixinid1) {
		this.weixinid1 = weixinid1;
	}

	public void setPh1(String ph1) {
		this.ph1 = ph1;
	}

	public String getPh2() {
		return ph2;
	}

	public void setPh2(String ph2) {
		this.ph2 = ph2;
	}

	public String getLoc() {
		return loc;
	}

	public void setLoc(String loc) {
		this.loc = loc;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public String getPassword1() {
		return password1;
	}

	public void setPassword1(String password1) {
		this.password1 = password1;
	}

}
