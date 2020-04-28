package service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import dao.UserDao;
import model.User;

@Service("userService")
public class UserServiceImpl implements UserService{
	
	@Autowired
	private UserDao userDao;

	@Override
	public User getUserByLogin(String username, String password) {
		return userDao.getUserByLogin(new User(username,password));
	}

	@Override
	public User getUserByUsername(String username) {
		return userDao.getUserByUsername(username);
	}


}
