<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>智能烟感报警系统</title>

<link href="css/bootstrap.css" rel="stylesheet">
<link href="css/custom-styles.css" rel="stylesheet">
<link href="css/dataTables.bootstrap.css" rel="stylesheet">
<script src="js/jquery-1.10.2.js"></script>
<script src="js/bootstrap.min.js"></script>
<link rel="stylesheet" href="css/cssCharts.css">
<link href="css/font-awesome.css" rel="stylesheet">
<script src="js/custom-scripts2.js"></script>
<script src="js/jquery.metisMenu.js"></script>
<link rel="stylesheet" href="css/bootstrapValidator.css">
<script type="text/javascript" src="js/bootstrapValidator.js"></script>



</head>
<body>
<div id="wrapper">

		<nav class="navbar navbar-default top-navbar"
			style="background-color: #F36A5A" role="navigation">
			<%@ include file="Header.jsp" %>

			<ul class="nav navbar-top-links navbar-right"></ul>
		</nav>

		<nav class="navbar-default navbar-side" role="navigation">
			<%@ include file="left.jsp" %>

		</nav>

		<div id="page-wrapper">
			<div class="header">
				<br>
				<br>
				<ol class="breadcrumb">
					<li><a href="NewFile.jsp">主界面</a></li>
					<li class="active">密码修改</li>
				</ol>
			</div>
			<div id="page-inner">

				<div class="row">
					<div class="col-md-12">


						<div class="panel panel-default">
							<div class="panel-heading">
								<div class="card-title">
									<div class="title">密码修改</div>
								</div>
							</div>
							<div class="panel-body">
								<form role="form" action="ZhanghaoServlet?action=mima" method="post">
									<div class="form-group">
										<label for="exampleInputEmail1">帐号:  ${sessionScope.username}</label> <input type="hidden"
											class="form-control" id="U_ID" name="U_ID" 
											value="${sessionScope.username}">
									</div>
									<div class="form-group">
										<label for="exampleInputPassword1">原始密码</label> <input
											type="password" class="form-control" id="U_pass"
											name="U_pass" placeholder="原始密码">
									</div>

									<div class="form-group">
										<label for="exampleInputPassword1">新密码</label> <input
											type="password" class="form-control" id="U_pass1"
											name="U_pass1" placeholder="新密码">
									</div>


									<div class="form-group">
										<label for="exampleInputPassword1">再次输入新密码</label> <input
											type="password" class="form-control" id="U_pass2"
											name="U_pass2" placeholder="再次输入新密码">
									</div>


									<button type="submit" class="btn btn-default" onclick="update()">确认修改</button>
								</form>
							</div>
						</div>




					</div>

				</div>


				<footer>
					<p>版权@2018。青岛专用集成电路设计工程研究中心版权所有</p>

				</footer>
			</div>

		</div>


	</div>


</body>
</html>
<script type="text/javascript">


<%String string = (String) request.getAttribute("c");
System.out.print(string);
		if (string == "a") {%>
alert("修改成功！");
<%}else if(string=="aa"){%>
alert("原密码错误，请联系管理员！");
<%}else if(string=="aaa"){%>
alert("密码不一致，请重新输入！");
<%}%>

</script>