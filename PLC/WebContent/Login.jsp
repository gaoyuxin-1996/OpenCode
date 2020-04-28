<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>PLC管控</title>

<script type="text/javascript"
	src="https://cdn.bootcss.com/jquery/2.2.4/jquery.js"> </script>
<link rel="stylesheet" type="text/css"
	href="https://cdn.bootcss.com/bootstrap/3.3.7/css/bootstrap.min.css" />
<script type="text/javascript"
	src="https://cdn.bootcss.com/jquery/2.2.4/jquery.js">
</script>
<script type="text/javascript"
	src="https://cdn.bootcss.com/bootstrap/3.3.7/js/bootstrap.js"></script>
<link href="https://nb.longmain.cn/css/custom-styles.css"
	rel="stylesheet" />
<link rel="stylesheet" href="https://nb.longmain.cn/css/font-awesome.min.css">
<link rel="stylesheet" href="https://nb.longmain.cn/css/form-elements.css">
<link rel="stylesheet" href="https://nb.longmain.cn/css/style.css">
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
<body style="background-color: #F5F5F5;">
<div class="page-header" style="padding-left: 160px;">
			<h1>
				PLC管控 <small>1.0</small>
			</h1>
		</div>
<div class="top-content">

		<div class="inner-bg">
			<div class="container">
				<div class="row">
					<div class="col-sm-8 col-sm-offset-2 text">
						<h1>
							<strong> 
								<br />
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
									<label class="sr-only" for="form-username">Phone</label> <input
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
		
		</div>

	</div>




</body>