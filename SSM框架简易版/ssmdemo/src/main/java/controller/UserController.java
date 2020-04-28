package controller;

import javax.servlet.http.HttpServletRequest;

import model.User;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import service.UserService;

import com.alibaba.fastjson.JSONObject;

@Controller
@RequestMapping("/user")
public class UserController {

	@Autowired
	private UserService userService;
	@Autowired
	private HttpServletRequest request;
	
	@RequestMapping(value="/login")
	@ResponseBody
	public JSONObject login(){
		
		//初始化用到的工具
		JSONObject result = new JSONObject();
		String msg = "";
		int status = 0;
		
		//接收ajax传来的数据
		String username = request.getParameter("username");
		String password = request.getParameter("password");
		
		//处理业务逻辑（可以看到控制台打出的sql）
		//select XXXX from user where username=xxx and password = xxx
		User loginUser = userService.getUserByLogin(username, password);
		//如果上面那条sql执行的结果返回null，要么就是没有username，要么就是有username，但是password错了
		//不为null 的时候说明用户名和密码正确
		if(loginUser != null) {
			result.put("loginUserId", loginUser.getId());
			status = 1;
			msg = "Login success!";//以前写的时候写中文会乱码，懒得搞了，就英文随便用用把
		}else {
			msg = "User not exists or wrong password";
		}
		
		
		result.put("status", status);
		result.put("msg", msg);
		
		//向前端传回数据(返回json对象必须添加@ResponseBody注解，springMvc要求的)
		return result;
	}
	
}
