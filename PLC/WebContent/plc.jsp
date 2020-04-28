<%@page import="com.item.equipment"%>
<%@page import="java.util.List"%>
<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>PLC管控</title>

<link href="css/bootstrap.css" rel="stylesheet" />
<link href="css/custom-styles.css" rel="stylesheet" />
<link href="js/dataTables.bootstrap.css" rel="stylesheet" />
<script src="js/jquery-1.10.2.js"></script>
<script src="js/bootstrap.min.js"></script>
<link rel="stylesheet" href="css/cssCharts.css">
<link href="css/font-awesome.css" rel="stylesheet" />
<script src="js/custom-scripts2.js"></script>
<script src="js/jquery.metisMenu.js"></script>
<link rel="stylesheet" href="css/bootstrapValidator.css" />
<script type="text/javascript" src="js/bootstrapValidator.js"></script>
<script src="js/jquery.dataTables.js"></script>
<script src="js/dataTables.bootstrap.js"></script>
<script type="text/javascript"
	src="http://api.map.baidu.com/api?v=2.0&ak=iD2gwtGfo1p98lPenidUyx8h"></script>


<script type="text/javascript">
	function edit(SB_no, SB_company, SB_add, SB_x, SB_y, SB_type, SB_name,
			SB_tel, SB_chufa, SB_putong, SB_cunchu, SB_id) {
		$("#SB_no_e").val(SB_no);
		$("#SB_company_e").val(SB_company);
		$("#SB_add_e").val(SB_add);
		$("#SB_x_e").val(SB_x);
		$("#SB_y_e").val(SB_y);
		$("#SB_type_e").val(SB_type);
		$("#SB_name_e").val(SB_name);
		$("#SB_tel_e").val(SB_tel);
		$("#SB_chufa_e").val(SB_chufa);
		$("#SB_putong_e").val(SB_putong);
		$("#SB_cunchu_e").val(SB_cunchu);
		$("#SB_id_e").val(SB_id);
		$("#SB_fh_e").val(SB_fh);
		$("#city2").val("");
		getAddr2();

	};

	function del(SB_no, SB_name) {
		$("#SB_no_d").val(SB_no);
		$("#SB_name_d").val(SB_name);

	};

	function zy(SB_no, SB_name, U_ID) {
		$("#SB_no_zy").val(SB_no);
		document.getElementById('SB_name_zy').innerHTML = SB_name;
		$("#U_ID_zy").val(U_ID);
	};

	$(document).ready(function() {
		$('#dataTables-example').dataTable();
	});
	
	
	
	 function add() {

			

			$.ajax({
				url : "plcServlet?action=add",
				dataType : "json",
 				data : $('#form1').serialize(),
 				success : function(data) {
 				alert(data.status)
 				  window.location.reload(); 
 				}
 			});
		

		};
		 function update() {

			

			$.ajax({
				url : "plcServlet?action=update",
				dataType : "json",
  				data : $('#form2').serialize(),
  				success : function(data) {
  				alert(data.status)
  				  window.location.reload(); 
  				}
  			});
		

		};
		
        function del1() {
			$.ajax({
				url : "plcServlet?action=delete",
				dataType : "json",
  				data : $('#form3').serialize(),
  				success : function(data) {
  				alert(data.status)
  				  window.location.reload(); 
  				}
  			});

		};
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
		<!-- /. NAV SIDE  -->

		<div id="page-wrapper">
			<div class="header">
				<br /> <br />
				<ol class="breadcrumb">
					<li><a href="NewFile.jsp">主界面</a></li>
					<li class="active">档案管理</li>
					<li><span class="btn btn-primary" data-toggle="modal"
						data-target="#add">新增设备</span></li>
				</ol>
			</div>
			<div id="page-inner">

				<div class="row">
					<div class="col-md-12">
						<!-- Advanced Tables -->
						<div class="panel panel-default">
							<div class="panel-heading">


								<div class="panel-body table-responsive">


									<table class="table table-striped table-bordered table-hover"
										id="dataTables-example">
										<thead>
											<tr>
												<th>产品ID</th>
												<th>产品名称</th>
												<th>产品类别</th>
												<th>产品系列</th>
												<th>产品厂商</th>
												<th>PLC品牌</th>
												<th>PLC系列</th>
												<th>协议名称</th>
												<th>通讯方式</th>
												<th>触发模式采集周期(ms)</th>
												<th>普通模式采集周期</th>
												<th>存储模式采集周期</th>
												<th>创建人</th>
												<th>创建时间</th>
												<th>更新人</th>
												<th>更新时间</th>

												<th style="text-align: center">操作</th>
											</tr>
										</thead>
										<tbody>

											<%
												List<equipment> list = (List) request.getAttribute("list");
												if (list == null || list.size() < 1) {
													out.print("没有数据");
												} else {
													for (equipment equipment : list) {
											%>
											<tr class="gradeU">
												<td class="sorting_1"><%=equipment.getPlcid()%></td>
												<td><%=equipment.getPlcname()%></td>
												<td><%=equipment.getCategory()%></td>
												<td><%=equipment.getXilie()%></td>
												<td><%=equipment.getVendor()%></td>
												<td><%=equipment.getPlcbrand()%></td>
												<td><%=equipment.getPlcxilie()%></td>
												<td><%=equipment.getAgreement()%></td>
												<td><%=equipment.getWay1()%></td>
												<td><%=equipment.getTrigger()%></td>
												<td><%=equipment.getOrdinary()%></td>
												<td><%=equipment.getStorage()%></td>
												<td><%=equipment.getPlccreatename()%></td>
												<td><%=equipment.getPlcdate()%></td>
												<td><%=equipment.getPlcupdatename()%></td>
												<td><%=equipment.getPlcupdatedate()%></td>
												<%
													String table3 = "<input type='button'" + " class='btn btn-default'"
																	+ "onclick='window.location.href=\"equipmentServlet?action=guankonginfo&SB_no="
																	+ equipment.getId() + "\"'" + " value='管控'" + ">";
															String table1 = "<td style='text-align: center'" + "><input type=" + "button"
																	+ " class='btn btn-default'" + "data-toggle='modal'" + " data-target='#edit'"
																	+ " onclick=\"edit('" + equipment.getCategory() + "','" + equipment.getPlcname() + "','"
																	+ equipment.getXilie()+ "','" + equipment.getVendor() + "','" + equipment.getPlcbrand()
																	+ "','" + equipment.getPlcxilie() + "','" + equipment.getAgreement() + "','" + equipment.getWay1()
																	 + "','" + equipment.getTrigger() + "','" + equipment.getOrdinary() + "','" + equipment.getStorage()
																	 + "','" + equipment.getPlcid()
																	+ "'),getAddr2()\"" + " value='编辑'" + ">";

															String table2 = "<input type='button'" + " class='btn btn-default'" + " data-toggle='modal'"
																	+ " data-target='#del'" + " onclick=\"del('" + equipment.getPlcid() + "','"
																	+ equipment.getPlcname() + "')\"" + " value='删除'" + "></td></tr>";

															String table = table1 + table3 + table2;
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
							<!--End Advanced Tables -->
						</div>
					</div>


					<footer>
						<p>版权@2018。青岛专用集成电路设计工程研究中心版权所有</p>
					</footer>
				</div>





				<div class="modal fade" id="add" tabindex="-1" role="dialog"
					aria-labelledby="mymodallabel" aria-hidden="true">
					<form role="form" id="form1"
						method="post">
						<div class="modal-dialog" role="document">
							<div class="modal-content">
								<div class="modal-header">
									<button type="button" class="close" data-dismiss="modal"
										aria-label="Close">
										<span aria-hidden="true">&times;</span>
									</button>
									<h4 class="modal-title" id="myModalLabel">新增</h4>
								</div>
								<div class="modal-body">


									<div class="form-group">
										<label for="txt_departmentname">产品名称</label> <input
											type="text" class="form-control" id="SB_company"
											name="SB_company" placeholder="产品名称">
									</div>
									<div class="form-group">
										<label for="txt_departmentname">产品类别</label> <input
											type="text" class="form-control" id="SB_add" name="SB_add"
											placeholder="产品类别">
									</div>


									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">产品系列</label> <input
												type="text" class="form-control" id="SB_x" name="SB_x"
												 placeholder="产品系列"> <label
												for="txt_departmentname">产品厂商</label> <input type="text"
												class="form-control" id="SB_y" name="SB_y" 
												placeholder="产品厂商">
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">PLC品牌</label> <select
												class="selectbox  form-control" name="SB_no" id="SB_no">
												<option>西门子</option>
											</select>
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">PLC系列</label> <select
												class="selectbox  form-control" name="SB_type" id="SB_type"
												placeholder="PLC系列">
												<option>S7-200-SMART</option>
											</select>
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">协议名称</label><select
												class="selectbox  form-control" name="SB_name" id="SB_name"
												placeholder="协议名称">
												<option>siemens_s7_tcp</option>
											</select>
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">通讯方式</label> <select
												class="selectbox  form-control" name="SB_tel" id="SB_tel"
												placeholder="通讯方式">
												<option >网口</option>
											</select>
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">触发模式采集周期(ms)</label> <input
												type="text" class="form-control" id="SB_chufa" name="SB_chufa"
												value="0" placeholder="触发模式采集周期(ms)">
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">普通模式采集周期(ms)</label> <input
												type="text" class="form-control" id="SB_putong" name="SB_putong"
												value="0" placeholder="普通模式采集周期(ms)">
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">存储模式采集周期(ms)</label> <input
												type="text" class="form-control" id="SB_cunchu" name="SB_cunchu"
												value="0" placeholder="存储模式采集周期(ms)">
										</div>
									</div>


								</div>
								<div class="modal-footer">
									<button type="button" class="btn btn-default"
										data-dismiss="modal">
										<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>关闭
									</button>
									<button type="button" class="btn btn-primary" onclick="add()">保 存</button>

								</div>
							</div>
						</div>
					</form>
				</div>
				<!-- /. ADD  -->






				<div class="modal fade" id="edit" tabindex="-1" role="dialog"
					aria-labelledby="mymodallabel" aria-hidden="true">
					<form role="form" id="form2"
						method="post">
						<div class="modal-dialog" role="document">
							<div class="modal-content">
								<div class="modal-header">
									<button type="button" class="close" data-dismiss="modal"
										aria-label="Close">
										<span aria-hidden="true">&times;</span>
									</button>
									<h4 class="modal-title" id="myModalLabel">编辑</h4>
								</div>
								<div class="modal-body">

<div class="form-group" hidden="true">
										<label for="txt_departmentname">产品名称</label> <input
											type="text" class="form-control" id="SB_id_e"
											name="SB_id_e" placeholder="产品名称">
									</div>
								
									<div class="form-group">
										<label for="txt_departmentname">产品名称</label> <input
											type="text" class="form-control" id="SB_company_e"
											name="SB_company_e" placeholder="产品名称">
									</div>
									<div class="form-group">
										<label for="txt_departmentname">产品类别</label> <input
											type="text" class="form-control" id="SB_no_e" name="SB_no_e"
											placeholder="产品类别">
									</div>


									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">产品系列</label> <input
												type="text" class="form-control" id="SB_add_e" name="SB_add_e"
												 placeholder="产品系列"> <label
												for="txt_departmentname">产品厂商</label> <input type="text"
												class="form-control" id="SB_x_e" name="SB_x_e" 
												placeholder="产品厂商">
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">PLC品牌</label> <input readonly
												type="text" class="form-control" name="SB_y_e" id="SB_y_e">
												
											
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">PLC系列</label> <input readonly
												type="text" class="form-control" name="SB_type_e" id="SB_type_e"
												placeholder="PLC系列">
												
											
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">协议名称</label><input readonly
												type="text" class="form-control" name="SB_name_e" id="SB_name_e"
												placeholder="协议名称">
												
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">通讯方式</label> <input readonly
												type="text" class="form-control" name="SB_tel_e" id="SB_tel_e"
												placeholder="通讯方式">

											
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">触发模式采集周期(ms)</label> <input
												type="text" class="form-control" id="SB_chufa_e" name="SB_chufa_e"
												value="0" placeholder="触发模式采集周期(ms)">
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">普通模式采集周期(ms)</label> <input
												type="text" class="form-control" id="SB_putong_e" name="SB_putong_e"
												value="0" placeholder="普通模式采集周期(ms)">
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">存储模式采集周期(ms)</label> <input
												type="text" class="form-control" id="SB_cunchu_e" name="SB_cunchu_e"
												value="0" placeholder="存储模式采集周期(ms)">
										</div>
									</div>

								</div>
								<div class="modal-footer">
									<button type="button" class="btn btn-default"
										data-dismiss="modal">
										<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>关闭
									</button>
									<button type="button" class="btn btn-primary " onclick="update()">保 存</button>
								</div>
							</div>
						</div>
					</form>
				</div>
				<!-- /. EDIT  -->






				<div class="modal fade" id="del" tabindex="-1" role="dialog"
					aria-labelledby="mymodallabel" aria-hidden="true">
					<form role="form" id="form3"
						method="post">
						<div class="modal-dialog" role="document">
							<div class="modal-content">
								<div class="modal-header">
									<button type="button" class="close" data-dismiss="modal"
										aria-label="Close">
										<span aria-hidden="true">&times;</span>
									</button>
									<h4 class="modal-title" id="myModalLabel">删除</h4>
								</div>
								<div class="modal-body">


									<input type="text" clases="form-control" id="SB_no_d"
										name="SB_no_d" readonly placeholder="编号">


									<div class="form-group">
										<label for="txt_departmentname">您确定要删除本条记录吗？</label> <input
											type="hidden" class="form-control" id="SB_name_d"
											name="SB_name_d" readonly placeholder="编号">
									</div>


									<div class="modal-footer">
										<button type="button" class="btn btn-default"
											data-dismiss="modal">
											<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>关闭
										</button>
										<button type="button" onclick="del1()" class="btn btn-warning ">删除</button>
									</div>
								</div>
							</div>
						</div>
					</form>
				</div>





				<div class="modal fade" id="zy" tabindex="-1" role="dialog"
					aria-labelledby="mymodallabel" aria-hidden="true">
					<form role="form" action="" method="post">
						<div class="modal-dialog" role="document">
							<div class="modal-content">
								<div class="modal-header">
									<button type="button" class="close" data-dismiss="modal"
										aria-label="Close">
										<span aria-hidden="true">&times;</span>
									</button>
									<h4 class="modal-title" id="myModalLabel">设备转移</h4>
								</div>
								<div class="modal-body">


									<input type="hidden" clases="form-control" id="SB_no_zy"
										name="SB_no_zy" readonly placeholder="设备号">


									<div class="form-group">
										<label for="txt_departmentname">您确定要转移<span
											id="SB_name_zy" name="SB_name_zy"></span>到其它帐户管理吗？
										</label>

									</div>



									<div class="form-group">
										<label for="txt_departmentname">所属帐户</label> <select
											class="selectbox  btn-default" name="U_ID_zy" id="U_ID_zy">
											<option value="dongfangshiji">东方世纪</option>
											<option value="test">中移物联</option>
											<option value="test1">中移物联1</option>
											<option value="test10">中移物联10</option>
											<option value="test2">中移物联2</option>
											<option value="test3">中移物联3</option>
											<option value="test4">中移物联4</option>
											<option value="test5">中移物联5</option>
											<option value="test6">中移物联6</option>
											<option value="test7">中移物联7</option>
											<option value="test8">中移物联8</option>
											<option value="test9">中移物联9</option>
											<option value="test1">test1</option>
										</select>
									</div>

									<div class="modal-footer">
										<button type="button" class="btn btn-default"
											data-dismiss="modal">
											<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>关闭
										</button>
										<button type="submit" class="btn btn-warning ">转移</button>
									</div>
								</div>
							</div>
						</div>
					</form>
				</div>
				<!-- /. zy  -->



				<!-- /. PAGE INNER  -->
			</div>
			<!-- /. PAGE WRAPPER  -->
		</div>
		<!-- /. WRAPPER  -->

	</div>

</body>
</html>
<script type="text/javascript">
	
<%String string2 = (String) request.getAttribute("y");
			if (string2 == "aa") {%>
	alert("修改成功！");
<%}%>
	
<%String string = (String) request.getAttribute("b");
			if (string == "aa") {%>
	alert("删除成功！");
<%}%>
	
<%String string1 = (String) request.getAttribute("e");
			if (string1 == "aa") {%>
	alert("添加成功！");
<%}%>
	
</script>