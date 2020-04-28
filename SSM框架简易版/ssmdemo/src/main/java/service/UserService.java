package service;

import model.User;

public interface UserService {

	public User getUserByLogin(String username,String password);
	
	public User getUserByUsername(String username);
	
}
