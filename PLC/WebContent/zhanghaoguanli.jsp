<%@page import="com.dao.UserDao"%>
<%@page import="java.util.List"%>
<%@page import="com.item.User"%>
<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>PLC管控</title>
<!-- Bootstrap Styles-->
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
<script src="js/jquery.dataTables.js"></script>
<script src="s/dataTables.bootstrap.js"></script>



<script>
	$(document).ready(function() {
		$('#dataTables-example').dataTable();
	});
</script>



<script>
			

	function edit(U_ID, U_name, U_pass, U_add, U_city, U_BM_ID, U_lv, U_title,
			U_IMSG, U_tel1, U_tel2, U_tel3, U_tel4, U_tel5, U_tel6, U_tel7,
			U_tel8) {
		$("#U_ID_e").val(U_ID);
		$("#U_name_e").val(U_name);
		$("#U_pass_e").val(U_pass);
		$("#U_add_e").val(U_add);
		$("#U_city_e").val(U_city);
		$("#U_BM_ID_e").val(U_BM_ID);
		$("#U_lv_e").val(U_lv);
		$("#U_title_e").val(U_title);
		$("#U_IMSG_e").val(U_IMSG);
		$("#U_tel1_e").val(U_tel1);
		$("#U_tel2_e").val(U_tel2);
		$("#U_tel3_e").val(U_tel3);
		$("#U_tel4_e").val(U_tel4);
		$("#U_tel5_e").val(U_tel5);
		$("#U_tel6_e").val(U_tel6);
		$("#U_tel7_e").val(U_tel7);
		$("#U_tel8_e").val(U_tel8);
	}

	function del1(U_ID, U_name) {
		$("#U_ID_d").val(U_ID);
		document.getElementById('U_name_d').innerHTML = U_name;
		document.getElementById('U_ID_d').innerHTML = U_ID;

	}
</script>

</head>
<body>
	<div id="wrapper">

		<nav class="navbar navbar-default top-navbar"
			style="background-color: #F36A5A" role="navigation">
			<%@ include file="Header.jsp"%>

			<ul class="nav navbar-top-links navbar-right"></ul>
		</nav>

		<nav class="navbar-default navbar-side" role="navigation">
			<%@ include file="left.jsp"%>

		</nav>

		<div id="page-wrapper">
			<div class="header">
				<br> <br>
				<ol class="breadcrumb">
					<li><a href="NewFile.jsp">主界面</a></li>
					<li class="active">帐号管理</li>
					<li><span class="btn btn-primary" data-toggle="modal"
						data-target="#add">新增帐号</span></li>
				</ol>
			</div>
			<div id="page-inner">

				<div class="row">
					<div class="col-md-12">
						<!-- Advanced Tables -->
						<div class="panel panel-default">
							<div class="panel-heading">
								<div class="panel-body table-responsive">
									<div id="dataTables-example_wrapper"
										class="dataTables_wrapper form-inline" role="grid">
										<table
											class="table table-striped table-bordered table-hover dataTable no-footer"
											id="dataTables-example"
											aria-describedby="dataTables-example_info">
											<thead>
												<tr role="row">
													<th class="sorting_asc" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="帐号: activate to sort column ascending"
														aria-sort="ascending" style="width: 266px;">帐号</th>
													<th class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="姓名: activate to sort column ascending"
														style="width: 204px;">姓名</th>
													<th class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="地址: activate to sort column ascending"
														style="width: 90px;">地址</th>
													<th class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="城市: activate to sort column ascending"
														style="width: 90px;">电话</th>
													<th class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="公司: activate to sort column ascending"
														style="width: 125px;">公司</th>
													<th class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="部门: activate to sort column ascending"
														style="width: 161px;">部门</th>
													<th class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="邮箱: activate to sort column ascending"
														style="width: 143px;">邮箱</th>
													<th style="text-align: center; width: 139px;"
														class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="编辑: activate to sort column ascending">编辑</th>
													<th style="text-align: center; width: 140px;"
														class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="删除: activate to sort column ascending">删除</th>
												</tr>
											</thead>
											<tbody>
												<%
													List<User> list = (List) request.getAttribute("list");
													if (list == null || list.size() < 1) {
														out.print("没有数据");
													} else {
														for (User user2 : list) {
												%>

												<tr class="gradeU odd">
													<td class="sorting_1"><%=user2.getName()%></td>
													<td><%=user2.getXingming()%></td>
													<td><%=user2.getAddress()%></td>
													<td><%=user2.getPhone()%></td>
													<td><%=user2.getGongsi()%></td>
													<td><%=user2.getBumen()%></td>
													<td><%=user2.getEmail()%></td>
													<%
														String table1 = "<td style='text-align: center'" + "><input type=" + "button"
																		+ " class='btn btn-default'" + "data-toggle='modal'" + " data-target='#edit'"
																		+ " onclick=\"edit('" + user2.getName() + "','" + user2.getXingming() + "','"
																		+ user2.getPassword() + "','" + user2.getAddress() + "','" + user2.getPhone() + "','"
																		+ user2.getGongsi() + "','" + user2.getBumen() + "','" + user2.getEmail() + "','"
																		+ user2.getQuanxian() + "')\"" + " value='编辑'" + "></td>";

																String table2 = "<td style='text-align: center'" + "><input type='button'"
																		+ " class='btn btn-default'" + " data-toggle='modal'" + " data-target='#del'"
																		+ " onclick=\"del1( '" + user2.getName() + "','" + user2.getXingming() + "')\"" + " value='删除'"
																		+ "></td></tr>";

																String table = table1 + table2;
													%>
													<%=table%>

												</tr>
												<%
													}

													}
												%>




											</tbody>
										</table>

									</div>
								</div>
							</div>
						</div>

						<div class="modal fade" id="add" tabindex="-1" role="dialog"
							aria-labelledby="mymodallabel" aria-hidden="true">
							<form role="form" action="ZhanghaoServlet?action=add" method="post"
								novalidate="novalidate" class="bv-form">
								<button type="submit" class="bv-hidden-submit"
									style="display: none; width: 0px; height: 0px;"></button>
								<div class="modal-dialog" role="document">
									<div class="modal-content">
										<div class="modal-header">
											<button type="button" class="close" data-dismiss="modal"
												aria-label="Close">
												<span aria-hidden="true">×</span>
											</button>
											<h4 class="modal-title" id="myModalLabel">新增</h4>
										</div>
										<div class="modal-body">


											<div class="form-group has-feedback">
												<label for="txt_departmentname">帐号</label> <input
													type="text" class="form-control" id="U_ID" name="U_ID"
													placeholder="帐号" data-bv-field="U_ID"><i
													class="form-control-feedback" data-bv-icon-for="U_ID"
													style="display: none;"></i> <small class="help-block"
													data-bv-validator="notEmpty" data-bv-for="U_ID"
													data-bv-result="NOT_VALIDATED" style="display: none;">不能为空</small><small
													class="help-block" data-bv-validator="stringLength"
													data-bv-for="U_ID" data-bv-result="NOT_VALIDATED"
													style="display: none;">长度超限</small><small
													class="help-block" data-bv-validator="regexp"
													data-bv-for="U_ID" data-bv-result="NOT_VALIDATED"
													style="display: none;">字母或数字组合</small>
											</div>

											<div class="form-group has-feedback">
												<label for="txt_departmentname">姓名</label> <input
													type="text" class="form-control" id="U_name" name="U_name"
													placeholder="姓名" data-bv-field="U_name"><i
													class="form-control-feedback" data-bv-icon-for="U_name"
													style="display: none;"></i> <small class="help-block"
													data-bv-validator="notEmpty" data-bv-for="U_name"
													data-bv-result="NOT_VALIDATED" style="display: none;">不能为空</small><small
													class="help-block" data-bv-validator="stringLength"
													data-bv-for="U_name" data-bv-result="NOT_VALIDATED"
													style="display: none;">长度超限</small>
											</div>
											<div class="form-group">
												<label for="txt_departmentname">密码</label> <input
													type="text" class="form-control" id="U_pass" name="U_pass"
													placeholder="密码">
											</div>
											<div class="form-group">
												<label for="txt_departmentname">地址</label> <input
													type="text" class="form-control" id="U_add" name="U_add"
													placeholder="地址">
											</div>
											<div class="form-group has-feedback">
												<label for="txt_departmentname">电话</label> <input
													type="text" class="form-control" id="U_tel1" name="U_tel1"
													placeholder="电话" data-bv-field="U_tel1"><i
													class="form-control-feedback" data-bv-icon-for="U_tel1"
													style="display: none;"></i> 
											</div>


											<div class="form-group has-feedback">
												<label for="txt_departmentname">公司</label> <input
													type="text" class="form-control" id="U_title"
													name="U_title" placeholder="公司" data-bv-field="U_title"><i
													class="form-control-feedback" data-bv-icon-for="U_title"
													style="display: none;"></i> <small class="help-block"
													data-bv-validator="notEmpty" data-bv-for="U_title"
													data-bv-result="NOT_VALIDATED" style="display: none;">不能为空</small><small
													class="help-block" data-bv-validator="stringLength"
													data-bv-for="U_title" data-bv-result="NOT_VALIDATED"
													style="display: none;">长度超限</small>
											</div>
											<div class="form-group has-feedback">
												<label for="txt_departmentname">部门</label> <input
													type="text" class="form-control" id="U_IMSG" name="U_IMSG"
													placeholder="部门" data-bv-field="U_IMSG"><i
													class="form-control-feedback" data-bv-icon-for="U_IMSG"
													style="display: none;"></i> <small class="help-block"
													data-bv-validator="notEmpty" data-bv-for="U_IMSG"
													data-bv-result="NOT_VALIDATED" style="display: none;">不能为空</small><small
													class="help-block" data-bv-validator="stringLength"
													data-bv-for="U_IMSG" data-bv-result="NOT_VALIDATED"
													style="display: none;">长度超限</small>
											</div>
											<div class="form-group has-feedback">
												<label for="txt_departmentname">邮箱</label> <input
													type="text" class="form-control" id="U_tel2" name="U_tel2"
													placeholder="邮箱" data-bv-field="U_tel2"><i
													class="form-control-feedback" data-bv-icon-for="U_tel2"
													style="display: none;"></i> 
											</div>
											<div class="form-group">
												<label for="txt_departmentname">权限</label> <input
													type="text" class="form-control" id="U_lv" name="U_lv"
													placeholder="权限">
											</div>


										</div>
										<div class="modal-footer">
											<button type="button" class="btn btn-default"
												data-dismiss="modal">
												<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>关闭
											</button>
											<button type="submit" class="btn btn-primary">保 存</button>
										</div>
									</div>
								</div>
							</form>
						</div>
						<!-- /. ADD  -->



						<div class="modal fade" id="edit" tabindex="-1" role="dialog"
							aria-labelledby="mymodallabel" aria-hidden="true">
							<form action="ZhanghaoServlet?action=update" method="post"
								class="bv-form" name="form1">
								<button type="submit" class="bv-hidden-submit"
									style="display: none; width: 0px; height: 0px;"></button>
								<div class="modal-dialog" role="document">
									<div class="modal-content">
										<div class="modal-header">
											<button type="button" class="close" data-dismiss="modal"
												aria-label="Close">
												<span aria-hidden="true">×</span>
											</button>
											<h4 class="modal-title" id="myModalLabel">编辑</h4>
										</div>



										<div class="modal-body">


											<div class="form-group has-feedback">
												<label for="txt_departmentname">帐号</label> <input
													type="text" class="form-control" id="U_ID_e" name="U_ID_e"
													placeholder="帐号" data-bv-field="U_ID_e"><i
													class="form-control-feedback" data-bv-icon-for="U_ID_e"
													style="display: none;"></i> <small class="help-block"
													data-bv-validator="notEmpty" data-bv-for="U_ID_e"
													data-bv-result="NOT_VALIDATED" style="display: none;">不能为空</small><small
													class="help-block" data-bv-validator="stringLength"
													data-bv-for="U_ID_e" data-bv-result="NOT_VALIDATED"
													style="display: none;">长度超限</small>
											</div>

											<div class="form-group has-feedback">
												<label for="txt_departmentname">姓名</label> <input
													type="text" class="form-control" id="U_name_e"
													name="U_name_e" placeholder="姓名" data-bv-field="U_name_e"><i
													class="form-control-feedback" data-bv-icon-for="U_name_e"
													style="display: none;"></i> <small class="help-block"
													data-bv-validator="notEmpty" data-bv-for="U_name_e"
													data-bv-result="NOT_VALIDATED" style="display: none;">不能为空</small><small
													class="help-block" data-bv-validator="stringLength"
													data-bv-for="U_name_e" data-bv-result="NOT_VALIDATED"
													style="display: none;">长度超限</small>
											</div>
											<div class="form-group">
												<label for="txt_departmentname">密码</label> <input
													type="text" class="form-control" id="U_pass_e"
													name="U_pass_e" placeholder="密码">
											</div>
											<div class="form-group">
												<label for="txt_departmentname">地址</label> <input
													type="text" class="form-control" id="U_add_e"
													name="U_add_e" placeholder="地址">
											</div>

											<div class="form-group">
												<label for="txt_departmentname">电话</label> <input
													type="text" class="form-control" id="U_city_e"
													name="U_city_e" placeholder="电话">
											</div>

											<div class="form-group">
												<label for="txt_departmentname">公司</label> <input
													type="text" class="form-control" id="U_BM_ID_e"
													name="U_BM_ID_e" placeholder="公司">
											</div>

											<div class="form-group">
												<label for="txt_departmentname">部门</label> <input
													type="text" class="form-control" id="U_lv_e" name="U_lv_e"
													placeholder="部门">
											</div>

											<div class="form-group">
												<label for="txt_departmentname">邮箱</label> <input
													type="text" class="form-control" id="U_title_e"
													name="U_title_e" placeholder="邮箱">
											</div>
											<div class="form-group">
												<label for="txt_departmentname">权限</label> <input
													type="text" class="form-control" id="U_IMSG_e"
													name="U_IMSG_e" placeholder="权限">
											</div>

										</div>



										<div class="modal-footer">
											<button type="button" class="btn btn-default"
												data-dismiss="modal">
												<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>关闭
											</button>
											<button type="submit" class="btn btn-default "
												>保 存</button>
										</div>
									</div>
								</div>
							</form>
						</div>

						<div class="modal fade" id="del" tabindex="-1" role="dialog"
							aria-labelledby="mymodallabel" aria-hidden="true">
							<form role="form" action="ZhanghaoServlet?action=del" method="post"
								novalidate="novalidate" class="bv-form">
								<button type="submit" class="bv-hidden-submit"
									style="display: none; width: 0px; height: 0px;"></button>
								<div class="modal-dialog" role="document">
									<div class="modal-content">
										<div class="modal-header">
											<button type="button" class="close" data-dismiss="modal"
												aria-label="Close">
												<span aria-hidden="true">×</span>
											</button>
											<h4 class="modal-title" id="myModalLabel">删除</h4>
										</div>
										<div class="modal-body">


											 <input
													type="hidden" class="form-control" id="U_ID_d" name="U_ID_d"
													placeholder="编号">


											<div class="form-group">
												<label for="txt_departmentname"> 您确定要删除<span
													id="U_name_d"></span>吗？
												</label>
											</div>


											<div class="modal-footer">
												<button type="button" class="btn btn-default"
													data-dismiss="modal">
													<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>关闭
												</button>
												<button type="submit" class="btn btn-warning " >删除</button>
											</div>
										</div>
									</div>
								</div>
							</form>
						</div>
						<!-- /. del  -->





					</div>

				</div>


				<footer>
					<p>版权@2018。青岛专用集成电路设计工程研究中心版权所有</p>

				</footer>
			</div>
			<!-- /. PAGE INNER  -->
		</div>
		<!-- /. page-wrapper  -->

	</div>
</body>
</html>

<script>
	

	
<%String string = (String) request.getAttribute("c");
			if (string == "aa") {%>
	alert("修改成功！");
<%}%>
	

		<%String string1 = (String) request.getAttribute("b");
					if (string1 == "aa") {%>
			alert("删除成功！");
		<%}%>
			
	
		<%String string2 = (String) request.getAttribute("d");
					if (string2 == "aa") {%>
			alert("添加成功！");
		<%}%>
		</script>