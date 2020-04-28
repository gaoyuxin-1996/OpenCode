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
<script type="text/javascript" src="js/scripts3.js"></script>
<script src="js/jquery.dataTables.js"></script>
<script src="js/dataTables.bootstrap.js"></script>
<script type="text/javascript"
	src="http://api.map.baidu.com/api?v=2.0&ak=iD2gwtGfo1p98lPenidUyx8h"></script>


<script type="text/javascript">
var shouid = '<%=request.getAttribute("SB_no")%>' ;
ID("1",shouid);
var company=null;
	function edit(SB_no, SB_company, SB_add, SB_x, SB_y, SB_type, SB_name,
			SB_tel) {
		$("#SB_no_e").val(SB_no);
		$("#SB_company_e").val(SB_company);
		$("#SB_add_e").val(SB_add);
		$("#SB_x_e").val(SB_x);
		$("#SB_y_e").val(SB_y);
		$("#SB_type_e").val(SB_type);
		$("#SB_name_e").val(SB_name);
		$("#SB_tel_e").val(SB_tel);
		$("#SB_fh_e").val(SB_fh);
		$("#city2").val("");
		getAddr2();

	}

	function del(SB_no, SB_name) {
		$("#SB_no_d").val(SB_no);
		$("#SB_name_d").val(SB_name);

	}

	function zy(SB_no, SB_name, U_ID) {
		$("#SB_no_zy").val(SB_no);
		document.getElementById('SB_name_zy').innerHTML = SB_name;
		$("#U_ID_zy").val(U_ID);
	}
<%

List<equipment> list = (List) request.getAttribute("list");
			String id = null;
			
			%>
			
			
			
			
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

							document.getElementById("imei").value = data.status.id;
							document.getElementById("name").value = data.status.name;
							document.getElementById("address").value = data.status.address;
							document.getElementById("wangguan").value = data.status.gatewayid;
							document.getElementById("way").value = data.status.gatewayname;
				
							$.ajax({
								url : "dianbiaoServlet?way="+document.getElementById("way").value+"&ptid="+document.getElementById("wangguan").value,
								dataType : "json",
								data : {
									action : "shuru"
									
								},
								success : function(msg) {

									var tbody = window.document.getElementById("biaoge");
									var tbody1 = window.document.getElementById("biaoge1");
									var tbody2 = window.document.getElementById("biaoge2");
									
				                    var str = "";
				                    var str1="";
				                    var str2="";
				                    var data = msg.dianbiao;
				                  var d="CPU输入";
				                    for (i in data) {
				                    	if(data[i].bool==d){
				                        str += "<tr>" +
				                            "<td align='center'>" + data[i].name + "</td>" +
				                            "<td align='center'><input type='text' value='"+data[i].zhi+"' id='"+data[i].name+"'></td>" +
				                            "<td align='center'><input type='button' value='写入' onclick=\"shuru('"+data[i].gatewayid+"','"+data[i].way+"','"+data[i].name+"')\"></td>" +

				"</tr>";
				                    }else if(data[i].bool=="PLC输入"){
				                        str1 += "<tr>" +
			                            "<td align='center'>" + data[i].name + "</td>" +
			                            "<td align='center'><input type='text' value='"+data[i].zhi+"' id='"+data[i].name+"'></td>" +
			                            "<td align='center'><input type='button' value='写入' onclick=\"shuru('"+data[i].gatewayid+"','"+data[i].way+"','"+data[i].name+"')\"></td>" +

			"</tr>";
				                    }else{
				                    	 str2 += "<tr>" +
				                            "<td align='center'>" + data[i].name + "</td>" +
				                            "<td align='center'><input type='text' value='"+data[i].zhi+"' id='"+data[i].name+"'></td>" +
				                            "<td align='center'><input type='button' value='写入' onclick=\"shuru('"+data[i].gatewayid+"','"+data[i].way+"','"+data[i].name+"')\"></td>" +

				"</tr>";
				                    }
				                    }
				                    tbody.innerHTML = str;
				                    tbody1.innerHTML = str1;
				                    tbody2.innerHTML = str2;
						
								}
							});
						}
					});
			
		}
	}
	
	function shuru(id, way, value) {

			$.ajax({
						url : "tcpServlet",
						dataType : "json",
						data : {
							action : "zhi",
							data : document.getElementById(value).value,
							key: value,
							way : way
							
						},
						success : function(data) {

				
						}
					});

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
	        var a=document.getElementById("input-num").value      
	        if(a>0){
	        document.getElementById("input-num").value=a-1;

	        }else{
	        	return;
	        }
	    })  
	  $("#input-jia").on('click', function() { 
	    	var a=document.getElementById("input-num").value
	    	var b=++a;
	        document.getElementById("input-num").value=b;       
	    })  
	   
	});
	
  
  
	
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
					<li class="active">设备管理</li>

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
										<a>设备编号：</a> <select style="width: 180px;" name="st_id"
											id="t_id"
											onchange="ID(this.options[this.selectedIndex].index,this.options[this.selectedIndex].value);">
											<option selected="selected" value="">--请选择设备编号--</option>
											<%
												if (list == null || list.size() < 1) {
													out.print("没有数据");
												} else {
													for (equipment equipment : list) {
											%>
											<option value="<%=equipment.getId()%>"><%=equipment.getId()%></option>
											<%
												}
												}
											%>
										</select> <a style="padding-left: 20px;">设备名称：</a> <select
											style="width: 180px;" name="st_name" id="t_name"
											onchange="Name(this.options[this.selectedIndex].index,this.options[this.selectedIndex].value);">
											<option selected="selected" value="">--请选择名称--</option>
											<%
												if (list == null || list.size() < 1) {
													out.print("没有数据");
												} else {
													for (equipment equipment : list) {
											%>
											<option value="<%=equipment.getName()%>"><%=equipment.getName()%></option>
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
										<a>设备编号：</a><input id="imei" type="text" style="border: 0px">
										<a>设备名称：</a><input id="name" type="text" style="border: 0px">
										<a>设备地址：</a><input
											id="address" type="text" style="border: 0px">
											<a>网关ID：</a><input
											id="wangguan" type="text" style="border: 0px">
											<a>通道：</a><input
											id="way" type="text" style="border: 0px">
									</div>
									<hr>
										<div class="panel panel-default">

										<table class="table table-striped table-bordered table-hover"
											id="dataTables-example">
											<thead>
												<tr>
													<th style="text-align: center">变量名</th>
                                                    <th style="text-align: center">CPU输入</th>
                                                    <th style="text-align: center">操作</th>

												</tr>

											</thead>
											<tbody id="biaoge">

											</tbody>
										</table>
									</div>
									
									
									<div class="panel panel-default">

										<table class="table table-striped table-bordered table-hover"
											id="dataTables-example">
											<thead>
												<tr>
													<th style="text-align: center">变量名</th>
                                                    <th style="text-align: center">PLC输入</th>
                                                    <th style="text-align: center">操作</th>

												</tr>

											</thead>
											<tbody id="biaoge1">

											</tbody>
										</table>
									</div>
									
									
									<div class="panel panel-default">

										<table class="table table-striped table-bordered table-hover"
											id="dataTables-example">
											<thead>
												<tr>
													<th style="text-align: center">变量名</th>
                                                    <th style="text-align: center">PLC输出</th>
                                                    <th style="text-align: center">操作</th>

												</tr>

											</thead>
											<tbody id="biaoge2">

											</tbody>
										</table>
									</div>
<hr>

<div>
<a>指示灯1</a>
<button id="zhi1" type="button" style="border-radius: 50%;width: 20px;height: 20px;background-color: blue;"></button>


</div>
									<div class="testswitch1">
										<div>
											<input class="testswitch-checkbox" id="onoffswitch" value="K1"
												type="checkbox"> <label class="testswitch-label"
												for="onoffswitch"> <span class="testswitch-inner"
												data-on="ON" data-off="OFF"></span> <span
												class="testswitch-switch"></span>
											</label> <a>&nbsp;&nbsp;K1</a>
										</div>

									</div>

									<div class="testswitch">

										<div>
											<input class="testswitch-checkbox1" id="onoffswitch1" value="K2"
												type="checkbox"> <label class="testswitch-label1"
												for="onoffswitch1"> <span class="testswitch-inner1"
												data-on="ON" data-off="OFF"></span> <span
												class="testswitch-switch1"></span>
											</label> <a>&nbsp;&nbsp;K2</a>
										</div>
									</div>
									<div class="testswitch">

										<div>
											<input class="testswitch-checkbox2" id="onoffswitch2" value="K3"
												type="checkbox"> <label class="testswitch-label2"
												for="onoffswitch2"> <span class="testswitch-inner2"
												data-on="ON" data-off="OFF"></span> <span
												class="testswitch-switch2"></span>
											</label> <a>&nbsp;&nbsp;K3</a>
										</div>
									</div>
									<div class="testswitch">

										<div>
											<input class="testswitch-checkbox3" id="onoffswitch3"
												type="checkbox"> <label class="testswitch-label3"
												for="onoffswitch3"> <span class="testswitch-inner3"
												data-on="ON" data-off="OFF"></span> <span
												class="testswitch-switch3"></span>
											</label> <a>&nbsp;&nbsp;4号按钮</a>
										</div>
									</div>
									<div class="testswitch">

										<div>
											<input class="testswitch-checkbox4" id="onoffswitch4"
												type="checkbox"> <label class="testswitch-label4"
												for="onoffswitch4"> <span class="testswitch-inner4"
												data-on="ON" data-off="OFF"></span> <span
												class="testswitch-switch4"></span>
											</label> <a>&nbsp;&nbsp;5号按钮</a>
										</div>
									</div>
									<div class="testswitch">

										<div>
											<input class="testswitch-checkbox5" id="onoffswitch5"
												type="checkbox"> <label class="testswitch-label5"
												for="onoffswitch5"> <span class="testswitch-inner5"
												data-on="ON" data-off="OFF"></span> <span
												class="testswitch-switch5"></span>
											</label> <a>&nbsp;&nbsp;6号按钮</a>
										</div>
									</div>
									<div class="testswitch">

										<div>
											<input class="testswitch-checkbox6" id="onoffswitch6"
												type="checkbox"> <label class="testswitch-label6"
												for="onoffswitch6"> <span class="testswitch-inner6"
												data-on="ON" data-off="OFF"></span> <span
												class="testswitch-switch6"></span>
											</label> <a>&nbsp;&nbsp;7号按钮</a>
										</div>
									</div>
									<div class="testswitch">

										<div>
											<input class="testswitch-checkbox7" id="onoffswitch7"
												type="checkbox"> <label class="testswitch-label7"
												for="onoffswitch7"> <span class="testswitch-inner7"
												data-on="ON" data-off="OFF"></span> <span
												class="testswitch-switch7"></span>
											</label> <a>&nbsp;&nbsp;8号按钮</a>
										</div>
									</div>
									<div class="testswitch">

										<div>
											<input class="testswitch-checkbox8" id="onoffswitch8"
												type="checkbox"> <label class="testswitch-label8"
												for="onoffswitch8"> <span class="testswitch-inner8"
												data-on="ON" data-off="OFF"></span> <span
												class="testswitch-switch8"></span>
											</label> <a>&nbsp;&nbsp;9号按钮</a>
										</div>
									</div>
									<div class="testswitch">

										<div>
											<input class="testswitch-checkbox9" id="onoffswitch9"
												type="checkbox"> <label class="testswitch-label9"
												for="onoffswitch9"> <span class="testswitch-inner9"
												data-on="ON" data-off="OFF"></span> <span
												class="testswitch-switch9"></span>
											</label> <a>&nbsp;&nbsp;10号按钮</a>
										</div>
									</div>
								</div>
								<div class="panel-body table-responsive">
									<div class="testswitch2" style="margin-left: 0px;]">

										<div>
											<input class="testswitch-checkbox10" id="onoffswitch10"
												type="checkbox"> <label class="testswitch-label10"
												for="onoffswitch10"> <span
												class="testswitch-inner10" data-on="ON" data-off="OFF"></span>
												<span class="testswitch-switch10"></span>
											</label> <a>&nbsp;&nbsp;11号按钮</a>
										</div>
									</div>
									<div class="testswitch2">

										<div>
											<input class="testswitch-checkbox11" id="onoffswitch11"
												type="checkbox"> <label class="testswitch-label11"
												for="onoffswitch11"> <span
												class="testswitch-inner11" data-on="ON" data-off="OFF"></span>
												<span class="testswitch-switch11"></span>
											</label> <a>&nbsp;&nbsp;12号按钮</a>
										</div>
									</div>
									<div class="testswitch2">

										<div>
											<input class="testswitch-checkbox12" id="onoffswitch12"
												type="checkbox"> <label class="testswitch-label12"
												for="onoffswitch12"> <span
												class="testswitch-inner12" data-on="ON" data-off="OFF"></span>
												<span class="testswitch-switch12"></span>
											</label> <a>&nbsp;&nbsp;13号按钮</a>
										</div>
									</div>
									<div class="testswitch2">

										<div>
											<input class="testswitch-checkbox13" id="onoffswitch13"
												type="checkbox"> <label class="testswitch-label13"
												for="onoffswitch13"> <span
												class="testswitch-inner13" data-on="ON" data-off="OFF"></span>
												<span class="testswitch-switch13"></span>
											</label> <a>&nbsp;&nbsp;14号按钮</a>
										</div>
									</div>
									<div class="testswitch2">

										<div>
											<input class="testswitch-checkbox14" id="onoffswitch14"
												type="checkbox"> <label class="testswitch-label14"
												for="onoffswitch14"> <span
												class="testswitch-inner14" data-on="ON" data-off="OFF"></span>
												<span class="testswitch-switch14"></span>
											</label> <a>&nbsp;&nbsp;15号按钮</a>
										</div>
									</div>
									<div class="testswitch2">

										<div>
											<input class="testswitch-checkbox15" id="onoffswitch15"
												type="checkbox"> <label class="testswitch-label15"
												for="onoffswitch15"> <span
												class="testswitch-inner15" data-on="ON" data-off="OFF"></span>
												<span class="testswitch-switch15"></span>
											</label> <a>&nbsp;&nbsp;16号按钮</a>
										</div>
									</div>
									<div class="testswitch2">

										<div>
											<input class="testswitch-checkbox16" id="onoffswitch16"
												type="checkbox"> <label class="testswitch-label16"
												for="onoffswitch16"> <span
												class="testswitch-inner16" data-on="ON" data-off="OFF"></span>
												<span class="testswitch-switch16"></span>
											</label> <a>&nbsp;&nbsp;17号按钮</a>
										</div>
									</div>
									<div class="testswitch2">

										<div>
											<input class="testswitch-checkbox17" id="onoffswitch17"
												type="checkbox"> <label class="testswitch-label17"
												for="onoffswitch17"> <span
												class="testswitch-inner17" data-on="ON" data-off="OFF"></span>
												<span class="testswitch-switch17"></span>
											</label> <a>&nbsp;&nbsp;18号按钮</a>
										</div>
									</div>
									<div class="testswitch2">

										<div>
											<input class="testswitch-checkbox18" id="onoffswitch18"
												type="checkbox"> <label class="testswitch-label18"
												for="onoffswitch18"> <span
												class="testswitch-inner18" data-on="ON" data-off="OFF"></span>
												<span class="testswitch-switch18"></span>
											</label> <a>&nbsp;&nbsp;19号按钮</a>
										</div>
									</div>
									<div class="testswitch2">

										<div>
											<input class="testswitch-checkbox19" id="onoffswitch19"
												type="checkbox"> <label class="testswitch-label19"
												for="onoffswitch19"> <span
												class="testswitch-inner19" data-on="ON" data-off="OFF"></span>
												<span class="testswitch-switch19"></span>
											</label> <a>&nbsp;&nbsp;数据启停</a>
										</div>
									</div>

								</div>
								<hr>
								<div class="panel-body table-responsive" style="float: left;">
									<a>速度控制</a> <br>
									<table>
										<tr>
											<td><input type="button" class="input-jian"
												id="input-jian" value="-" /></td>
											<td><input type="text" class="input-num" id="input-num"
												value="0" style="width: 50px;" /></td>
											<td><input type="button" class="input-jia"
												id="input-jia" value="+" /></td>
										</tr>
									</table>
								</div>

								<div class="panel-body table-responsive">
									<a>参数设置</a> <br> <br> <a>参数1:</a>&nbsp;&nbsp;<input
										type="text" id="" style="width: 50px;" />
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a>参数2:</a>&nbsp;&nbsp;<input
										type="text" id="" style="width: 50px;" />
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a>参数3:</a>&nbsp;&nbsp;<input
										type="text" id="" style="width: 50px;" />
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a>参数4:</a>&nbsp;&nbsp;<input
										type="text" id="" style="width: 50px;" />
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a>参数5:</a>&nbsp;&nbsp;<input
										type="text" id="" style="width: 50px;" />
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input type="submit"
										id="" value="确认提交" />
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