package com.sun.service.impl;

import java.sql.SQLException;

import com.sun.dao.FourMessageDAO;
import com.sun.factory.DAOFactory;
import com.sun.model.FourMessage;
import com.sun.service.FourMessageService;

public class FourMessageServiceImpl implements FourMessageService {
private FourMessageDAO messageDAO=DAOFactory.getFourMessage();
	@Override
	public boolean isExist(String imei) {
		// TODO Auto-generated method stub
		boolean flag=false;
		try {
			if(messageDAO.searchByImei(imei)!=null) {
				
				flag=true;
				
			}

			
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return flag;
	}

	@Override
	public FourMessage searchFourMessage(String imei) {
		// TODO Auto-generated method stub
		FourMessage fourmessages=null;
		try {
			fourmessages=messageDAO.searchByImei(imei);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return fourmessages;
	}

}
