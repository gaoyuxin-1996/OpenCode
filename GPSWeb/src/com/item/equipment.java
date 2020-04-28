package com.item;

public class equipment {

	private String id;
	private String leixing;
	private String name;
	private String address;
	private String state;
	private String operator;
	private String glcompany;
	private double jingdu;
	private double weidu;
	private String phone;

	public equipment(String id, String leixing, String name, String address, String state, String operator,
			String glcompany, double jingdu, double weidu, String phone) {
		this.id = id;
		this.leixing = leixing;
		this.name = name;
		this.address = address;
		this.state = state;
		this.operator = operator;
		this.glcompany = glcompany;
		this.jingdu = jingdu;
		this.weidu = weidu;
		this.phone = phone;
	}

	public equipment() {
		// TODO 自动生成的构造函数存根
	}

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getLeixing() {
		return leixing;
	}

	public void setLeixing(String leixing) {
		this.leixing = leixing;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getAddress() {
		return address;
	}

	public void setAddress(String address) {
		this.address = address;
	}

	public String getState() {
		return state;
	}

	public void setState(String state) {
		this.state = state;
	}

	public String getOperator() {
		return operator;
	}

	public void setOperator(String operator) {
		this.operator = operator;
	}

	public String getGlcompany() {
		return glcompany;
	}

	public void setGlcompany(String glcompany) {
		this.glcompany = glcompany;
	}

	public double getJingdu() {
		return jingdu;
	}

	public void setJingdu(double jingdu) {
		this.jingdu = jingdu;
	}

	public double getWeidu() {
		return weidu;
	}

	public void setWeidu(double weidu) {
		this.weidu = weidu;
	}

	public String getPhone() {
		return phone;
	}

	public void setPhone(String phone) {
		this.phone = phone;
	}

}
