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
<link rel="stylesheet" href="css/on_off_switch.css" type="text/css">
<script type="text/javascript" src="js/bootstrapValidator.js"></script>
<script src="js/jquery.dataTables.js"></script>
<script src="js/dataTables.bootstrap.js"></script>
<script src="js/switch.js"></script>
<script type="text/javascript"
	src="http://api.map.baidu.com/api?v=2.0&ak=iD2gwtGfo1p98lPenidUyx8h"></script>


<script type="text/javascript">
var shouid = '<%=request.getAttribute("SB_no")%>';
	ID("1", shouid);
	var company = null;
	
	
	function edit(SB_bianliang, SB_danwei, SB_jicun, SB_address, SB_data, SB_wei, SB_buer,
			SB_chufa, SB_putong, SB_cunchu1, SB_cunchu, SB_duxie, SB_yanzheng) {

		$("#SB_bianliang1").val(SB_bianliang);
		$("#SB_danwei1").val(SB_danwei);
		$("#SB_jicun1").val(SB_jicun);
		$("#SB_address1").val(SB_address);
		$("#SB_data1").val(SB_data);
		$("#SB_wei1").val(SB_wei);
		$("#SB_buer1").val(SB_buer);
		$("#SB_chufa1").val(SB_chufa);
		$("#SB_putong1").val(SB_putong);
		$("#SB_cunchu11").val(SB_cunchu1);
		$("#SB_cunchu1").val(SB_cunchu);
		$("#SB_duxie1").val(SB_duxie);
		$("#SB_yanzheng1").val(SB_yanzheng);
		

	}

	function del(SB_no, SB_way, SB_name) {
		$("#SB_no_d").val(SB_no);
		$("#SB_way_d").val(SB_way);
		$("#SB_name_d").val(SB_name);

	}

	function zy(SB_no, SB_name, U_ID) {
		$("#SB_no_zy").val(SB_no);
		document.getElementById('SB_name_zy').innerHTML = SB_name;
		$("#U_ID_zy").val(U_ID);
	}
<%List<equipment> list = (List) request.getAttribute("list");
			String id = null;%>
	function ID(id, value) {

		if (id == "") {
			alert("请选择设备编号");

			return;
		} else {
			$
					.ajax({
						url : "equipmentServlet",
						dataType : "json",
						data : {
							SB_no : value,
							action : "xiangqing1"
						},
						success : function(data) {

							document.getElementById("imei").value = data.status.plcid;
							document.getElementById("name").value = data.status.plcname;
							document.getElementById("wangguan").value = data.status.gatewayid;
							document.getElementById("way").value = data.status.gatewayname;
							$.ajax({
								url : "dianbiaoServlet?action=xinxi",
								dataType : "json",
								data : {
									SB_plcid : document.getElementById("wangguan").value,
									way : data.status.gatewayname
								},
								success : function(msg) {
								 var tbody = window.document.getElementById("biaoge");
							
				                    var str = "";
				                    var data = msg.data;
				                  
				                    for (i in data) {
				                        str += "<tr>" +
				                            "<td align='center'>" + data[i].gatewayid + "</td>" +
				                            "<td align='center'>" + data[i].name + "</td>" +
				                            "<td align='center'>" + data[i].unit + "</td>" +
				                            "<td align='center'>" + data[i].register + "</td>" +
				                            "<td align='center'>" + data[i].address + "</td>" +
				                            "<td align='center'>" + data[i].data + "</td>" +
				                            "<td align='center'>" + data[i].weiaddress + "</td>" +
				                            "<td align='center'>" + data[i].bool + "</td>" +
				                            "<td align='center'>" + data[i].chufa + "</td>" +
				                            "<td align='center'>" + data[i].putong + "</td>" +
				                            "<td align='center'>" + data[i].cunchu1 + "</td>" +
				                            "<td align='center'>" + data[i].cunchu + "</td>" +
				                            "<td align='center'>" + data[i].duxie + "</td>" +
				                            "<td align='center'>" + data[i].yanzheng + "</td>" +
				                            "<td align='center'><input type= 'button' data-target='#del' data-toggle='modal' onclick=\"del('"+data[i].gatewayid+"','"+data[i].way+"','"+data[i].name+"')\" value='删除'><input type='button' data-target='#edit' data-toggle='modal' onclick=\"edit('"+
				                            		data[i].name+"','"+data[i].unit+"','"+data[i].register+"','"+data[i].address+"','"+data[i].data+"','"+data[i].weiaddress+"','"+data[i].bool+"','"+data[i].chufa+"','"+data[i].putong+"','"+data[i].cunchu1+"','"+data[i].cunchu+"','"+data[i].duxie+"','"+data[i].yanzheng+"')\" value='编辑'></td>" +
				                            "</tr>";
				                    }
				                    tbody.innerHTML = str;
				                


								}
							});
						}
					});
		}
	}

	function Companyall() {

		document.getElementById("t_company").length = 0;

		document.getElementById("t_company").options
				.add(new Option("--选择的一级菜单--", 0));
		document.getElementById("t_company").options
				.add(new Option("--默认全选--", 0));
		$.ajax({
			url : "plcServlet",
			dataType : "json",
			data : {
				action : "plcname"
			},
			success : function(data) {
				var i = 0;

				while (data.status[i] != null) {

					document.getElementById("t_company").options.add(new Option(
							data.status[i]));
					
					
					i++;
				}
				var obj=document.getElementById("t_company");
				 for(var i=0;i<obj.options.length;i++){
				      if(obj.options[i].text==company){
				        obj.options[i].selected=true;
				        break;
				      }
				      }
			}
		});

	}
	
	function Company(id, value) {

		if(value==0){
			value="";
		}
		company=value;
				document.getElementById("t_id").length = 0;
				document.getElementById("t_id").options
						.add(new Option("--请选择设备编号--", 0));

				$.ajax({
					url : "equipmentServlet",
					dataType : "json",
					data : {
						SB_no : value,
						action : "xiangqing3"
					},
					success : function(data) {
						var i = 0;

						while (data.status[i] != null) {

							document.getElementById("t_id").options.add(new Option(
									data.status[i]));
						
							i++;
						}

					}
				});

			};
	
	

	$(document).ready(function() {
		$("#input-jian").on('click', function() {
			var a = document.getElementById("input-num").value
			if (a > 0) {
				document.getElementById("input-num").value = a - 1;

			} else {
				return;
			}
		})
		$("#input-jia").on('click', function() {
			var a = document.getElementById("input-num").value
			var b = ++a;
			document.getElementById("input-num").value = b;
		})

	});
	
	 function add() {

		var a=document.getElementById('wangguan').value
		var d=document.getElementById('imei').value

			$.ajax({
				url : "dianbiaoServlet?action=add&SB_plcid="+a+"&way="+document.getElementById('way').value,
				dataType : "json",
				data : $('#form1').serialize(),
				success : function(data) {
					alert(data.status);
					
ID(d,d);

				}
			});
		

		};
		 function update() {

			 var d=document.getElementById('imei').value
			 var a=document.getElementById('wangguan').value
			$.ajax({
				url : "dianbiaoServlet?action=update&SB_plcid="+a+"&way="+document.getElementById('way').value,
				dataType : "json",
				data : $('#form2').serialize(),
				success : function(data) {
				alert(data.status)
				
				ID(d,d);
				}
			});
		

		};
		
     function del1() {
    	 var d=document.getElementById('imei').value
			$.ajax({
				url : "dianbiaoServlet?action=delete",
				dataType : "json",
				data : $('#form3').serialize(),
				success : function(data) {
				alert(data.status)
				ID(d,d);
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
					<li class="active">点表管理</li>
					<li><span class="btn btn-primary" data-toggle="modal"
						data-target="#add">新增点表</span></li>
				</ol>
			</div>
			<div id="page-inner">
				<div class="row">
					<div class="col-md-12">
						<div class="panel panel-default">
							<div class="panel-heading">
								<div class="panel-body table-responsive">
<div>


										<a>产品名称：</a> <select style="width: 180px;" name="st_company"
											id="t_company" onclick="Companyall()" 
											onchange="Company(this.options[this.selectedIndex].index,this.options[this.selectedIndex].value);">
											<option selected="selected" value="">--可选择的一级菜单--</option>
											<option value="">--默认全选--</option>
											<option></option>
											<option></option>
											<option></option>
											<option></option>
											<option></option>
										</select>
										<hr>
									</div>
									<div>
										<a>产品编号：</a> <select style="width: 180px;" name="st_id"
											id="t_id"
											onchange="ID(this.options[this.selectedIndex].index,this.options[this.selectedIndex].value);">
											<option selected="selected" value="">--请选择设备编号--</option>
											<%
												if (list == null || list.size() < 1) {
													out.print("没有数据");
												} else {
													for (equipment equipment : list) {
											%>
											<option value="<%=equipment.getPlcid()%>"><%=equipment.getPlcid()%></option>
											<%
												}
												}
											%>
										</select> 
									</div>

								</div>

							</div>

						</div>
						<div class="panel panel-default">
							<div class="panel-heading">
								<div class="panel-body table-responsive">
									<div>
										<a>产品编号：</a><input id="imei" type="text" style="border: 0px">
										<a>产品名称：</a><input id="name" type="text" style="border: 0px">
										<a>网关ID：</a><input id="wangguan" type="text" style="border: 0px">
										<a>通道：</a><input id="way" type="text" style="border: 0px">

									</div>

									<div class="panel panel-default">

										<table class="table table-striped table-bordered table-hover"
											id="dataTables-example">
											<thead>
												<tr>
													<th>网关IDID</th>
													<th>变量名</th>
													<th>单位</th>
													<th>寄存器类型</th>
													<th>地址</th>
													<th>数据类型</th>
													<th>位地址</th>
													<th>模式</th>
													<th>触发模式</th>
													<th>普通模式</th>
													<th>存储模式</th>
													<th>存储</th>
													<th>读写权限</th>
													<th>验证方式</th>

													<th style="text-align: center">操作</th>
												</tr>

											</thead>
											<tbody id="biaoge">

											</tbody>
										</table>

									</div>



									<footer>
										<p>版权@2018。青岛专用集成电路设计工程研究中心版权所有</p>
									</footer>
								</div>





								<div class="modal fade" id="add" tabindex="-1" role="dialog"
									aria-labelledby="mymodallabel" aria-hidden="true">
									<form role="form" id="form1" method="post">
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
														<label for="txt_departmentname">变量名-N</label> <input
															type="text" class="form-control" id="SB_bianliang"
															name="SB_bianliang" placeholder="变量名-N">
													</div>
													<div class="form-group">
														<label for="txt_departmentname">单位-U</label> <input
															type="text" class="form-control" id="SB_danwei"
															name="SB_danwei" placeholder="单位-U">
													</div>


													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">寄存器类型-R</label> <select
																class="selectbox  form-control" name="SB_jicun"
																id="SB_jicun">
																<option>I</option>
																<option>Q</option>
																<option>M</option>
																<option>V</option>
															</select> <br>
															<br> <label for="txt_departmentname">地址-A</label> <input
																type="number" class="form-control" id="SB_address"
																name="SB_address" placeholder="地址-A">
														</div>
													</div>
													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">数据类型</label> <select
																class="selectbox  form-control" name="SB_data"
																id="SB_data">
																<option>Bool</option>
																<option>Int8</option>
																<option>Uint8</option>
																<option>Int16</option>
																<option>Unit16</option>
																<option>Int32</option>
																<option>Uint32</option>
															</select>
														</div>
													</div>
													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">位地址-O</label> <select
																class="selectbox  form-control" name="SB_wei"
																id="SB_wei">
																<option>0</option>
																<option>1</option>
																<option>2</option>
																<option>3</option>
																<option>4</option>
																<option>5</option>
																<option>6</option>
																<option>7</option>
															</select>
														</div>
													</div>
													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">模式</label><select
																class="selectbox  form-control" name="SB_buer"
																id="SB_buer" placeholder="普通模式-F1">
																<option>CPU输入</option>
																<option>PLC输入</option>
																<option>PLC输出</option>
															</select>


														</div>
													</div>
													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">触发模式-F1</label> <select
																class="selectbox  form-control" name="SB_chufa"
																id="SB_chufa" placeholder="触发模式-F1">
																<option selected="selected">不采集</option>
																<option>采集</option>
															</select>
														</div>
													</div>
													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">普通模式-F1</label> <select
																class="selectbox  form-control" name="SB_putong"
																id="SB_putong" placeholder="普通模式-F1">
																<option selected="selected">不采集</option>
																<option>采集</option>
															</select>
														</div>
													</div>
													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">存储模式-F3</label> <select
																class="selectbox  form-control" name="SB_cunchu1"
																id="SB_cunchu1" placeholder="存储模式-F1">
																<option selected="selected">不采集</option>
																<option>采集</option>
															</select>
														</div>
													</div>
													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">存储-S</label> <select
																class="selectbox  form-control" name="SB_cunchu"
																id="SB_cunchu" placeholder="存储-S">
																<option selected="selected">不存储</option>

															</select>
														</div>
													</div>

													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">读写权限-W</label> <select
																class="selectbox  form-control" name="SB_duxie"
																id="SB_duxie" placeholder="存储-S">
																<option selected="selected">只读</option>

															</select>
														</div>
														<div class="form-group">
															<div class="form-inline">
																<label for="txt_departmentname">验证方式-V</label> <select
																	class="selectbox  form-control" name="SB_yanzheng"
																	id="SB_yanzheng" placeholder="存储-S">
																	<option selected="selected">无</option>
																	<option>范围</option>
																	<option>枚举</option>
																</select>
															</div>
														</div>
													</div>
												</div>
												<div class="modal-footer">
													<button type="button" class="btn btn-default"
														data-dismiss="modal">
														<span class="glyphicon glyphicon-remove"
															aria-hidden="true"></span>关闭
													</button>
													<button type="button" class="btn btn-primary"
														onclick="add()">保 存</button>

												</div>
											</div>
										</div>
									</form>
								</div>
								<!-- /. ADD  -->






								<div class="modal fade" id="edit" tabindex="-1" role="dialog"
									aria-labelledby="mymodallabel" aria-hidden="true">
									<form role="form" id="form2" method="post">
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


													<div class="form-group">
														<label for="txt_departmentname">变量名-N</label> <input
															type="text" class="form-control" id="SB_bianliang1"
															name="SB_bianliang1" placeholder="变量名-N">
													</div>
													<div class="form-group">
														<label for="txt_departmentname">单位-U</label> <input
															type="text" class="form-control" id="SB_danwei1"
															name="SB_danwei1" placeholder="单位-U">
													</div>


													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">寄存器类型-R</label> <select
																class="selectbox  form-control" name="SB_jicun1"
																id="SB_jicun1">
																<option>I</option>
																<option>Q</option>
																<option>M</option>
																<option>V</option>
															</select> <br>
															<br> <label for="txt_departmentname">地址-A</label> <input
																type="number" class="form-control" id="SB_address1"
																name="SB_address1" placeholder="地址-A">
														</div>
													</div>
													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">数据类型</label> <select
																class="selectbox  form-control" name="SB_data1"
																id="SB_data1">
																<option>Bool</option>
																<option>Int8</option>
																<option>Uint8</option>
																<option>Int16</option>
																<option>Unit16</option>
																<option>Int32</option>
																<option>Uint32</option>
															</select>
														</div>
													</div>
													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">位地址-O</label> <select
																class="selectbox  form-control" name="SB_wei1"
																id="SB_wei1">
																<option>0</option>
																<option>1</option>
																<option>2</option>
																<option>3</option>
																<option>4</option>
																<option>5</option>
																<option>6</option>
																<option>7</option>
															</select>
														</div>
													</div>
													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">布尔量说明-I</label><input
																type="text" class="selectbox  form-control"
																name="SB_buer1" id="SB_buer1" placeholder="0：正常|1：故障">


														</div>
													</div>
													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">触发模式-F1</label> <select
																class="selectbox  form-control" name="SB_chufa1"
																id="SB_chufa1" placeholder="触发模式-F1">
																<option selected="selected">不采集</option>
																<option>采集</option>
															</select>
														</div>
													</div>
													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">普通模式-F1</label> <select
																class="selectbox  form-control" name="SB_putong1"
																id="SB_putong1" placeholder="普通模式-F1">
																<option selected="selected">不采集</option>
																<option>采集</option>
															</select>
														</div>
													</div>
													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">存储模式-F3</label> <select
																class="selectbox  form-control" name="SB_cunchu11"
																id="SB_cunchu11" placeholder="存储模式-F1">
																<option selected="selected">不采集</option>
																<option>采集</option>
															</select>
														</div>
													</div>
													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">存储-S</label> <select
																class="selectbox  form-control" name="SB_cunchu1"
																id="SB_cunchu1" placeholder="存储-S">
																<option selected="selected">不存储</option>

															</select>
														</div>
													</div>

													<div class="form-group">
														<div class="form-inline">
															<label for="txt_departmentname">读写权限-W</label> <select
																class="selectbox  form-control" name="SB_duxie1"
																id="SB_duxie1" placeholder="存储-S">
																<option selected="selected">只读</option>

															</select>
														</div>
														<div class="form-group">
															<div class="form-inline">
																<label for="txt_departmentname">验证方式-V</label> <select
																	class="selectbox  form-control" name="SB_yanzheng1"
																	id="SB_yanzheng1" placeholder="存储-S">
																	<option selected="selected">无</option>
																	<option>范围</option>
																	<option>枚举</option>
																</select>
															</div>
														</div>
													</div>
												</div>
												<div class="modal-footer">
													<button type="button" class="btn btn-default"
														data-dismiss="modal">
														<span class="glyphicon glyphicon-remove"
															aria-hidden="true"></span>关闭
													</button>
													<button type="button" class="btn btn-primary "
														onclick="update()">保 存</button>
												</div>
											</div>
										</div>
									</form>
								</div>
								<!-- /. EDIT  -->






								<div class="modal fade" id="del" tabindex="-1" role="dialog"
									aria-labelledby="mymodallabel" aria-hidden="true">
									<form role="form" id="form3" method="post">
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

<input type="text" clases="form-control" id="SB_way_d"
														name="SB_way_d" readonly placeholder="通道">
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
															<span class="glyphicon glyphicon-remove"
																aria-hidden="true"></span>关闭
														</button>
														<button type="button" onclick="del1()"
															class="btn btn-warning ">删除</button>
													</div>
												</div>
											</div>
										</div>
									</form>
								</div>


							</div>

						</div>

					</div>
				</div>
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