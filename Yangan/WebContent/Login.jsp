<%@page import="org.springframework.web.context.request.RequestScope"%>
<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>NB-GPS定位</title>

<link rel="stylesheet"
	href="http://fonts.googleapis.com/css?family=Roboto:400,100,300,500">
<link href="css/bootstrap.min.css" rel="stylesheet" />
<link rel="stylesheet" href="css/bootstrap.min.css">
<link rel="stylesheet" href="css/font-awesome.min.css">
<link rel="stylesheet" href="css/form-elements.css">
<link rel="stylesheet" href="css/style.css">
<link rel="shortcut icon" href="/assets/ico/favicon.png">

<script language="javascript">
function checked(form){
	
	
	
}
</script>
<%
String a=(String)request.getAttribute("returnvalue");
String mess;
if(a=="失败"){
	 mess="<p style='color: red'>账号或密码错误请重新输入！</p>";
}else{
	 mess="请输入您的用户名和密码:";
}

%>
</head>
<body>
	<div class="top-content">

		<div class="inner-bg">
			<div class="container">
				<div class="row">
					<div class="col-sm-8 col-sm-offset-2 text">
						<h1>
							<strong> 
								<br />NB-GPS定位
							</strong>
						</h1>
						
					</div>
				</div>
				<div class="row">
					<div class="col-sm-6 col-sm-offset-3 form-box">
						<div class="form-top">
							<div class="form-top-left">
								<h3>登陆界面</h3>

								<p><%=mess%></p>
							</div>
						
						</div>
						<div class="form-bottom">
							<form method="post" action="UserServlet" class="login-form"
								name="form" onsubmit="return checked(this)">
								<div class="form-group">
									<label class="sr-only" for="form-username">Username</label> <input
										class="form-username form-control" id="form-username"
										name="username" placeholder="用户名" type="text" value="" />


								</div>
								<div class="form-group">
									<label class="sr-only" for="form-password">Password</label> <input
										class="form-password form-control" id="form-password"
										name="password" placeholder="密码" type="password" value="" />

								</div>

								<button type="submit" class="btn">登 陆</button>
							</form>

						</div>
					</div>
				</div>

			</div>
			<div class="copyrights">版权@2018。青岛专用集成电路设计工程研究中心版权所有。</div>
		</div>

	</div>
	<div class="backstretch"
		style="left: 0px; top: 0px; overflow: hidden; margin: 0px; padding: 0px; height: 951px; width: 1920px; z-index: -999999; position: fixed;">
		<img src="image/1.jpg"
			style="position: absolute; margin: 0px; padding: 0px; border: none; width: 1920px; height: 1280px; max-height: none; max-width: none; z-index: -999999; left: 0px; top: -164.5px;">
	</div>



	<script src="js/jquery-1.11.1.min.js"></script>
	<script src="js/jquery.backstretch.min.js"></script>
	<script src="js/scripts.js"></script>
</body>
</html>