<%@page import="com.item.equipment"%>
<%@page import="java.util.List"%>
<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>智能烟感报警系统</title>

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
        function edit(SB_no, SB_company, SB_add, SB_x, SB_y, SB_type, SB_name, SB_tel) {
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

    			

        function zy(SB_no, SB_name,U_ID) {
            $("#SB_no_zy").val(SB_no);
            document.getElementById('SB_name_zy').innerHTML = SB_name;
            $("#U_ID_zy").val(U_ID); 
        }


    </script>
	

<script type="text/javascript">
        function getAddr() {
            var map = document.getElementById("dituContent");
            if (map.style.display == "none") {
                map.style.display = "block";
                initMap();
            }
            if (map.style.display != "none") {
                initMap();
            }
        }

        function getAddr2() {
            var map2 = document.getElementById("dituContent2");
            if (map2.style.display == "none") {
                map2.style.display = "block";
                initMap2();
            }
            if (map2.style.display != "none") {
                initMap2();
            }
        }
    </script>



<script>
        $(document).ready(function () {
            $('#dataTables-example').dataTable();
        });
    </script>


<script type="text/javascript">
    //创建和初始化地图函数：
    function initMap() {
        createMap(); //创建地图
        setMapEvent(); //设置地图事件


        var gc = new BMap.Geocoder();

        map.addEventListener("click", function (e) {
            map.clearOverlays();
            var pt = e.point;
            gc.getLocation(pt, function (rs) {
                var addComp = rs.addressComponents;
                var addr;
                addr = addComp.city + addComp.district + addComp.street + addComp.streetNumber;
                map.addOverlay(new BMap.Marker(pt));
                document.getElementById("SB_add").value = addr;
                document.getElementById("SB_x").value = pt.lng;
                document.getElementById("SB_y").value = pt.lat;


            });
        });






    }

    //创建地图函数：
    function createMap() {
        var map = new BMap.Map("dituContent"); //在百度地图容器中创建一个地图

       

        // 创建地址解析器实例
        var myGeo = new BMap.Geocoder();
        // 将地址解析结果显示在地图上,并调整地图视野
        
        myGeo.getPoint(document.getElementById("city").value, function (point) {
            if (point) {
                map.centerAndZoom(point, 14);

            }
        }, document.getElementById("city").value);






        window.map = map; //将map变量存储在全局
    }

    //地图事件设置函数：
    function setMapEvent() {
        map.enableDragging(); //启用地图拖拽事件，默认启用(可不写)
        map.enableScrollWheelZoom(); //启用地图滚轮放大缩小
        map.enableDoubleClickZoom(); //启用鼠标双击放大，默认启用(可不写)
        map.enableKeyboard(); //启用键盘上下左右键移动地图

    }


    initMap(); //创建和初始化地图
</script>





<script type="text/javascript">
    //创建和初始化地图函数：
    function initMap2() {
        createMap2(); //创建地图
        setMapEvent2(); //设置地图事件
        var gc = new BMap.Geocoder();

        map2.addEventListener("click", function (e) {
            map2.clearOverlays();
            var pt = e.point;
            gc.getLocation(pt, function (rs) {
                var addComp = rs.addressComponents;
                var addr;
                addr = addComp.city + addComp.district + addComp.street + addComp.streetNumber;
                map2.addOverlay(new BMap.Marker(pt));
                document.getElementById("SB_add_e").value = addr;
                document.getElementById("SB_x_e").value = pt.lng;
                document.getElementById("SB_y_e").value = pt.lat;


            });
        });



    }

    //创建地图函数：
    function createMap2() {
        var map2 = new BMap.Map("dituContent2"); //在百度地图容器中创建一个地图



        var point = new BMap.Point(parseFloat(document.getElementById("SB_x_e").value), parseFloat(document.getElementById("SB_y_e").value));
        map2.centerAndZoom(point, 12);
        var point2 = new BMap.Point(document.getElementById("SB_x_e").value, document.getElementById("SB_y_e").value);
        var marker = new BMap.Marker(point2); // 创建标注
        map2.addOverlay(marker);             // 将标注添加到地图中



        //创建地址解析器实例
        var myGeo = new BMap.Geocoder();
        // 将地址解析结果显示在地图上,并调整地图视野
        myGeo.getPoint(document.getElementById("city2").value, function (point) {
            if (point) {
                map2.centerAndZoom(point, 14);

            }
        }, document.getElementById("city2").value);






        window.map2 = map2; //将map变量存储在全局
    }

    //地图事件设置函数：
    function setMapEvent2() {
        map2.enableDragging(); //启用地图拖拽事件，默认启用(可不写)
        map2.enableScrollWheelZoom(); //启用地图滚轮放大缩小
        map2.enableDoubleClickZoom(); //启用鼠标双击放大，默认启用(可不写)
        map2.enableKeyboard(); //启用键盘上下左右键移动地图
    }
    initMap2(); //创建和初始化地图
</script>





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
		<!-- /. NAV SIDE  -->

		<div id="page-wrapper">
			<div class="header">
				<br />
				<br />
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
												<th>设备类型</th>
												<th>设备ID</th>
												<th>设备名称</th>
												<th>地址</th>
												<th>故障</th>
												<th>录入员</th>

												<th style="text-align: center">操作</th>

												<th style="text-align: center">详情</th>
											</tr>
										</thead>
										<tbody>

<%
													List<equipment> list = (List) request.getAttribute("list");
													if (list == null || list.size() < 1) {
														out.print("没有数据");
													} else {
														for (equipment equipment: list) {
												%>

												<tr class="gradeU">
													<td class="sorting_1"><%=equipment.getLeixing()%></td>
													<td title="29897055 心跳周期: 1440 最近心跳时间: 2018/5/10 8:50:11"><%=equipment.getId()%></td>
													<td><%=equipment.getName()%></td>
													<td><%=equipment.getAddress()%></td>
													<td><%=equipment.getState()%></td>
													<td><%=equipment.getOperator()%></td>
													
													<%
													String table1 = "<td style='text-align: center'" + "><input type=" + "button"
															+ " class='btn btn-default'" + "data-toggle='modal'" + " data-target='#edit'"
															+ " onclick=\"edit('" + equipment.getId() + "','" + equipment.getGlcompany() + "','"
															+ equipment.getAddress() + "','" + equipment.getJingdu() + "','" + equipment.getWeidu() + "','"
															+ equipment.getLeixing() + "','" + equipment.getName() + "','" + equipment.getPhone()  + 
															"'),getAddr2()\"" + " value='编辑'" + ">";

													String table2 = "<input type='button'"
															+ " class='btn btn-default'" + " data-toggle='modal'" + " data-target='#del'"
															+ " onclick=\"del( '" + equipment.getId() + "','" + equipment.getName() + "')\"" + " value='删除'"
															+ "><td style='text-align: center'"+"><a href="+"equipmentServlet?action=xiangqing&SB_no="+equipment.getId()+">详情</a></td></tr>";

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
							<!--End Advanced Tables -->
						</div>
					</div>


					<footer>
						<p>版权@2018。青岛专用集成电路设计工程研究中心版权所有</p>
					</footer>
				</div>





				<div class="modal fade" id="add" tabindex="-1" role="dialog"
					aria-labelledby="mymodallabel" aria-hidden="true">
					<form role="form" action="equipmentServlet?action=add" method="post">
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
										<div class="form-inline">
											<input type="text" class="form-control " id="city"
												name="city" value="青岛" placeholder="查询条件"> <input
												id="Button2" type="button" class="btn btn-primary"
												value="查询" onclick="getAddr()"
												style="font-size: 16px; width: 86px;" />
										</div>


										<div style="height: 300px;" id="dituContent"></div>

									</div>

									<div class="form-group">
										<label for="txt_departmentname">管理单位</label> <input
											type="text" class="form-control" id="SB_company"
											name="SB_company" placeholder="管理单位">
									</div>
									<div class="form-group">
										<label for="txt_departmentname">安装地址</label> <input
											type="text" class="form-control" id="SB_add" name="SB_add"
											placeholder="安装地址">
									</div>


									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">横向坐标</label> <input
												type="text" class="form-control" id="SB_x" name="SB_x"
												value="0" placeholder="X坐标"> <label
												for="txt_departmentname">竖向坐标</label> <input type="text"
												class="form-control" id="SB_y" name="SB_y" value="0"
												placeholder="Y坐标">
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">设备编号</label> <input
												type="text" class="form-control" id="SB_no" name="SB_no"
												placeholder="设备编号"> <label for="txt_departmentname">设备类型</label>
											<select class="selectbox  form-control" name="SB_type"
												id="SB_type" placeholder="设备类型">
												<option value="设备">设备</option>
												<option value="设备2">设备2</option>
												<option value="设备3">设备3</option>
												<option value="设备4">设备4</option>
												<option value="设备5">设备5</option>
												<option value="设备6">设备6</option>
											

											</select>
										</div>
									</div>

									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">设备名称</label> <input
												type="text" class="form-control" id="SB_name" name="SB_name"
												placeholder="设备名称"><label for="txt_departmentname">固定电话</label> <input
												type="text" class="form-control" id="SB_tel" name="SB_tel"
												placeholder="固定电话"> 
										</div>
									</div>




								</div>
								<div class="modal-footer">
									<button type="button" class="btn btn-default"
										data-dismiss="modal">
										<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>关闭
									</button>
									<button type="submit" class="btn btn-primary" >保 存</button>

								</div>
							</div>
						</div>
					</form>
				</div>
				<!-- /. ADD  -->






				<div class="modal fade" id="edit" tabindex="-1" role="dialog"
					aria-labelledby="mymodallabel" aria-hidden="true">
					<form role="form" action="equipmentServlet?action=update" method="post">
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
										<div class="form-inline">
											<input type="text" class="form-control " id="city2"
												name="city2" placeholder="查询条件"> <input id="Button3"
												type="button" class="btn btn-primary" value="查询"
												onclick="getAddr2()" style="font-size: 16px; width: 86px;" />
										</div>


										<div style="height: 300px;" id="dituContent2"></div>

									</div>

									<div class="form-group">
										<label for="txt_departmentname">管理单位</label> <input
											type="text" class="form-control" id="SB_company_e"
											name="SB_company_e" placeholder="管理单位">
									</div>
									<div class="form-group">
										<label for="txt_departmentname">安装地址</label> <input
											type="text" class="form-control" id="SB_add_e"
											name="SB_add_e" placeholder="安装地址">
									</div>


									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">横向坐标</label> <input
												type="text" class="form-control" id="SB_x_e" name="SB_x_e"
												placeholder="X坐标"> <label for="txt_departmentname">竖向坐标</label>
											<input type="text" class="form-control" id="SB_y_e"
												name="SB_y_e" placeholder="Y坐标">
										</div>
									</div>
									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">设备编号</label> <input
												type="text" class="form-control" id="SB_no_e" readonly
												name="SB_no_e" placeholder="设备编号"> <label
												for="txt_departmentname">设备类型</label> <select
												class="selectbox  form-control" name="SB_type_e"
												id="SB_type_e" placeholder="设备类型">
												<option value="设备">设备</option>
												<option value="设备2">设备2</option>
												<option value="设备3">设备3</option>
												<option value="设备4">设备4</option>
												<option value="设备5">设备5</option>
												<option value="设备6">设备6</option>
											</select>
										</div>
									</div>

									<div class="form-group">
										<div class="form-inline">
											<label for="txt_departmentname">设备名称</label> <input
												type="text" class="form-control" id="SB_name_e"
												name="SB_name_e" placeholder="设备名称"> <label for="txt_departmentname">固定电话</label> <input
												type="text" class="form-control" id="SB_tel_e"
												name="SB_tel_e" placeholder="固定电话"> 
										</div>
									</div>


								</div>
								<div class="modal-footer">
									<button type="button" class="btn btn-default"
										data-dismiss="modal">
										<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>关闭
									</button>
									<button type="submit" class="btn btn-primary " >保 存</button>
								</div>
							</div>
						</div>
					</form>
				</div>
				<!-- /. EDIT  -->






				<div class="modal fade" id="del" tabindex="-1" role="dialog"
					aria-labelledby="mymodallabel" aria-hidden="true">
					<form role="form" action="equipmentServlet?action=delete" method="post">
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
										<button type="submit" class="btn btn-warning " >删除</button>
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

<%String string3 = (String) request.getAttribute("e");
if (string3 == "erro") {%>
alert("您的设备数量已达到上限，请申请！");
<%}%>
</script>