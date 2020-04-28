package com.item;

import net.sf.json.JSONObject;

public class client {

	private String gid;
	private int ptid;
	private int cid;
	private String time;
	private int func;
	private String pwd;
	private String code;
	private JSONObject cmd;

	public JSONObject getCmd() {
		return cmd;
	}

	public void setCmd(JSONObject jsonObject) {
		this.cmd = jsonObject;
	}

	public String getGid() {
		return gid;
	}

	public void setGid(String gid) {
		this.gid = gid;
	}

	public int getPtid() {
		return ptid;
	}

	public void setPtid(int ptid) {
		this.ptid = ptid;
	}

	public int getCid() {
		return cid;
	}

	public void setCid(int cid) {
		this.cid = cid;
	}

	public String getTime() {
		return time;
	}

	public void setTime(String time) {
		this.time = time;
	}

	public int getFunc() {
		return func;
	}

	public void setFunc(int func) {
		this.func = func;
	}

	public String getPwd() {
		return pwd;
	}

	public void setPwd(String pwd) {
		this.pwd = pwd;
	}

	public String getCode() {
		return code;
	}

	public void setCode(String code) {
		this.code = code;
	}

}
