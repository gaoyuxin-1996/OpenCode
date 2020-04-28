package dao;

import model.User;

public interface UserDao {
	
    User getUserByLogin(User user);
    
    User getUserByUsername(String username);
    
}