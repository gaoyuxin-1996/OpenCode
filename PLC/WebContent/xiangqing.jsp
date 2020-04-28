<%@page import="com.item.equipment"%>
<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>PLC管控</title>

<link href="css/bootstrap.css" rel="stylesheet" />
<link href="css/font-awesome.css" rel="stylesheet" />
<link href="css/morris/morris-0.4.3.min.css" rel="stylesheet" />
<link href="css/custom-styles.css" rel="stylesheet" />
<link rel="stylesheet" href="css/cssCharts.css">
<link href="css/dataTables.bootstrap.css" rel="stylesheet" />
<script src="js/jquery-1.10.2.js"></script>
<script src="js/bootstrap.min.js"></script>
<script src="js/jquery.metisMenu.js"></script>
<script src="js/custom-scripts2.js"></script>
<script type="text/javascript"
	src="http://api.map.baidu.com/api?v=2.0&ak=iD2gwtGfo1p98lPenidUyx8h"></script>
<script src="js/jquery-1.10.2.js"></script>
  <%
    equipment equipment=(equipment)request.getAttribute("equipment");
    %>
</head>
<body>


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
				<br />
				<ol class="breadcrumb">


					<li><a href="NewFile.jsp">主界面</a></li>
					<li><span><a href="equipmentServlet?action=all">返回</a></span></li>
					<li><span><a href="equipmentServlet?action=xiangqing&SB_no=<%=equipment.getId()%>">刷新</a></span></li>

				</ol>
			</div>
			<div id="page-inner">

				<div class="row">
					<div class="col-md-12">
						<!-- Advanced Tables -->
						<div class="panel panel-default">
							<div class="panel-heading">

								<div class="panel-body table-responsive">
									<div id="allmap"
										style="height: 600px; width: 100%; font-size: 16px; float: left;">


									</div>
									<script src="js/jquery-1.9.1.js?v=1.0.0.19"
										type="text/javascript" charset="utf-8"></script>
									<script src="js/ipc.js?v=1.0.0.26" type="text/javascript"
										charset="utf-8"></script>
									<script src="js/common.js?v=1.0.0.19" type="text/javascript"
										charset="utf-8"></script>
									
								</div>
								<!--End Advanced Tables -->
							</div>
						</div>

						<footer>
							<p>版权@2018。青岛专用集成电路设计工程研究中心版权所有</p>
						</footer>
					</div>

					<!-- /. PAGE INNER  -->
				</div>
				<!-- /. PAGE WRAPPER  -->
			</div>
			<!-- /. WRAPPER  -->
		</div>

</body>
</html>


<script type="text/javascript">
    // 百度地图API功能

    
    var map = new BMap.Map("allmap");
    var point = new BMap.Point("<%=equipment.getJingdu()%>", "<%=equipment.getWeidu()%>");
    var marker = new BMap.Marker(point);  // 创建标注
    map.addOverlay(marker);              // 将标注添加到地图中
    map.centerAndZoom(point, 15);

    setMapEvent();

    var opts = {
        width: 400,     // 信息窗口宽度
        height: 80,     // 信息窗口高度
        title: "Message", // 信息窗口标题
        enableMessage: true, //设置允许信息窗发送短息
        message: ""
    }
    var infoWindow = new BMap.InfoWindow("设备编号:<%=equipment.getId()%> 名称:<%=equipment.getName()%>", opts);  // 创建信息窗口对象


    //地图事件设置函数：
    function setMapEvent() {
        map.enableDragging(); //启用地图拖拽事件，默认启用(可不写)
        map.enableScrollWheelZoom(); //启用地图滚轮放大缩小
        map.enableDoubleClickZoom(); //启用鼠标双击放大，默认启用(可不写)
        map.enableKeyboard(); //启用键盘上下左右键移动地图

    }
    map.openInfoWindow(infoWindow, point); //开启信息窗口

    marker.addEventListener("click", function () {
        map.openInfoWindow(infoWindow, point); //开启信息窗口
    });
</script>