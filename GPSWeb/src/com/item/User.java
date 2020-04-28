package com.item;

public class User {

	private String name;
	private String password;
	private String xingming;
	private String address;
	private String phone;
	private String gongsi;
	private String bumen;
	private String email;
	private int quanxian;
	private String weixinid;

	public User() {

	}

	public User(String name, String password, String xingming, String address, String phone, String gongsi,
			String bumen, String email, int quanxian, String weixinid) {
		this.name = name;
		this.password = password;
		this.xingming = xingming;
		this.address = address;
		this.phone = phone;
		this.gongsi = gongsi;
		this.bumen = bumen;
		this.email = email;
		this.quanxian = quanxian;
		this.weixinid = weixinid;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public String getXingming() {
		return xingming;
	}

	public void setXingming(String xingming) {
		this.xingming = xingming;
	}

	public String getAddress() {
		return address;
	}

	public void setAddress(String address) {
		this.address = address;
	}

	public String getPhone() {
		return phone;
	}

	public void setPhone(String phone) {
		this.phone = phone;
	}

	public String getGongsi() {
		return gongsi;
	}

	public void setGongsi(String gongsi) {
		this.gongsi = gongsi;
	}

	public String getBumen() {
		return bumen;
	}

	public void setBumen(String bumen) {
		this.bumen = bumen;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public int getQuanxian() {
		return quanxian;
	}

	public void setQuanxian(int quanxian) {
		this.quanxian = quanxian;
	}

	public String getWeixinid() {
		return weixinid;
	}

	public void setWeixinid(String weixinid) {
		this.weixinid = weixinid;
	}

}
