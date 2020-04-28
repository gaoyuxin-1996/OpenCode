<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
<meta content="" name="description" />
<meta content="webthemez" name="author" />
<title>智能烟感报警系统</title>
<link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
<link href="css/NewFile1.css" rel="stylesheet" />
<link href="css/font-awesome.min.css" rel="stylesheet" />
<link href="css/dataTables.bootstrap.css" rel="stylesheet" />
<script type="text/javascript"
	src="http://api.map.baidu.com/api?v=2.0&ak=mzUiUrREA3HGQE3B4T933XPHVKBQb3j8"></script>
<script type="text/javascript"
	src="http://api.map.baidu.com/library/TextIconOverlay/1.2/src/TextIconOverlay_min.js"></script>
<script type="text/javascript" src="js/MarkerClusterer.js"></script>
<script src="js/jquery-1.10.2.js"></script>
<script type="text/javascript">
	$(document).ready(function() {
		GetDetails();
		setInterval("GetDetails()", 9000);

	});
	function GetDetails() {
		$.post("/Main/Detail", {
			order : "1"
		}, function(data) {
			$("#AJAXMAIN").html(data);//这里是重点，右侧数据获取后要显示到div中  
		}, "text");
	}
	
		
	
</script>
<%
String name=(String)session.getAttribute("username");
int a=(int)request.getAttribute("num");
%>
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
				<br />
			</div>
			<div id="page-inner">
				<div id="AJAXMAIN">

					<!-- Morris Chart Js -->
					<script src="js/raphael-2.1.0.min.js"></script>
					<script src="js/morris.js"></script>
					<script src="js/easypiechart.js"></script>
					<script src="js/easypiechart-data.js"></script>
					<script src="js/jquery.chart.js"></script>

					<div class="row">
						<div class="col-md-3 col-sm-12 col-xs-12">
							<div class="board">
								<div class="panel panel-primary">
									<div class="number">
										<h3>
											<a style="color: #F36A5A">0</a>
										</h3>
										<small><a 
											style="color: #F36A5A">报警</a></small>
									</div>
							
								</div>
							</div>
						</div>

						<div class="col-md-3 col-sm-12 col-xs-12">
							<div class="board">
								<div class="panel panel-primary">
									<div class="number">
										<h3>
											<a  style="color: #9B9B9B"><%=a %></a>
										</h3>
										<small> <a 
											style="color: #9B9B9B">设备</a>
										</small>
									</div>
									

								</div>
							</div>
						</div>

						<div class="col-md-3 col-sm-12 col-xs-12">
							<div class="board">
								<div class="panel panel-primary">
									<div class="number">

										<h3>

											<a  style="color: #FABE28">0</a>
										</h3>

										<small><a 
											style="color: #FABE28">低电</a></small>

									</div>
									

								</div>
							</div>
						</div>



						<div class="col-md-3 col-sm-12 col-xs-12">
							<div class="board">
								<div class="panel panel-primary">
									<div class="number">
										<h3>
											<a  style="color: #1ABC9C">0</a>
										</h3>

										<small> <a 
											style="color: #1ABC9C">离线</a>

										</small>
									</div>
									

								</div>
							</div>
						</div>

					</div>


					<div class="row">
						<div class="col-xs-6 col-md-3">
							<div class="panel panel-default">
								<div class="panel-body easypiechart-panel">
									<h4>设备数量</h4>
									<div class="easypiechart" id="easypiechart-red"
										data-percent="<%=a %>">
										<span class="percent"><%=a %></span>
									</div>
								</div>
							</div>
						</div>

						<div class="col-xs-6 col-md-3">
							<div class="panel panel-default">
								<div class="panel-body easypiechart-panel">
									<h4>离线设备</h4>
									<div class="easypiechart" id="easypiechart-black"
										data-percent="100">
										<span class="percent">100%</span>
									</div>
								</div>
							</div>
						</div>



						<div class="col-xs-6 col-md-3">
							<div class="panel panel-default">
								<div class="panel-body easypiechart-panel">
									<h4>报警设备</h4>
									<div class="easypiechart" id="easypiechart-orange"
										data-percent="0">
										<span class="percent">0%</span>

									</div>
								</div>
							</div>
						</div>
						<div class="col-xs-6 col-md-3">
							<div class="panel panel-default">
								<div class="panel-body easypiechart-panel">
									<h4>低电设备</h4>
									<div class="easypiechart" id="easypiechart-teal"
										data-percent="0">
										<span class="percent">0%</span>

									</div>
								</div>
							</div>

						</div>
					</div>
					<div class="row">
						<div class="col-md-12">
							<!-- Advanced Tables -->
							<div class="panel panel-default">

								<div class="panel-body table-responsive">
									<div id="allmap" style="width: 100%; height: 550px;"></div>
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
</div>

		<script src="js/bootstrap.min.js"></script>
		<script src="js/jquery.metisMenu.js"></script>
		<script src="js/custom-scripts.js"></script>
		<script src="js/jquery.dataTables.js"></script>
		<script src="js/dataTables.bootstrap.js"></script>
		<script>
			$(document).ready(function() {
				$('#dataTables-example').dataTable();
			});
		</script>
</body>
</html>
<script type="text/javascript">
	// 百度地图API功能
	var map = new BMap.Map("allmap");

	//map.setMapStyle({ style: 'midnight' });
	map.centerAndZoom(new BMap.Point(119.794, 31.335), 6);
	map.enableScrollWheelZoom();

	map.addControl(new BMap.MapTypeControl());
	map.addControl(new BMap.NavigationControl()); // 添加平移缩放控件
	map.addControl(new BMap.ScaleControl()); // 添加比例尺控件
	map.addControl(new BMap.OverviewMapControl()); //添加缩略地图控件

	var MAX = 3000;
	var markers = [];

	var i = 0

	var jingdu = new Array();
	var weidu = new Array();

	var Msgs = new Array();

	var opts = {
		width : 350, // 信息窗口宽度
		height : 80, // 信息窗口高度
		title : "信息窗口", // 信息窗口标题
		enableMessage : true
	//设置允许信息窗发送短息
	};

	function addClickHandler(content, marker) {
		marker.addEventListener("click", function(e) {

			openInfo(content, e)
		});
	}

	function openInfo(content, e) {
		var p = e.target;
		var point = new BMap.Point(p.getPosition().lng, p.getPosition().lat);
		var infoWindow = new BMap.InfoWindow(content, opts); // 创建信息窗口对象
		map.openInfoWindow(infoWindow, point); //开启信息窗口
	}

	//最简单的用法，生成一个marker数组，然后调用markerClusterer类即可。
	var markerClusterer = new BMapLib.MarkerClusterer(map, {
		markers : markers
	});
	
	<%
	String string=(String)request.getAttribute("MESS");
	if (string=="cuowu") {
		%>
		alert("您没有权限进行该操作，请联系管理员！");
		<%
	}
	%>
</script>