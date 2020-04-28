package com.sun.temp;
/*
 * 懒汉模式
 * 单例
 * **/

import java.util.ArrayList;

public class TempData {
	private static TempData instance=null;
	public static synchronized TempData getInstance() {
		if(instance==null) {
		       instance=new TempData();	
		}	
		return instance;
	}
	private TempData() {}
	
	public String Data=null;//jason流数据
	public String Db_name=null,Tb_name=null;
	public ArrayList<String> Return_data=null;
	/*public enum dyp{
		normal,nodata,wrongdata
	}*/

}
