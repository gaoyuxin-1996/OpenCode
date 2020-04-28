package com.sun.service;

import com.sun.model.FourMessage;

public interface FourMessageService {
	boolean isExist(String imei);//查询是否存在
	FourMessage searchFourMessage(String imei);//查询信息
}
