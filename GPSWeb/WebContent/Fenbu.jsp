<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>
<%@page import="com.item.equipment"%>
<%@page import="java.util.List"%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>智能烟感报警系统</title>
<link href="css/bootstrap.css" rel="stylesheet" />
<link href="css/font-awesome.css" rel="stylesheet" />
<link href="css/morris-0.4.3.min.css" rel="stylesheet" />
<link href="css/custom-styles.css" rel="stylesheet" />
<link rel="stylesheet" href="css/cssCharts.css">
<link href="css/dataTables.bootstrap.css" rel="stylesheet" />
<script src="js/jquery-1.10.2.js"></script>
<script src="js/bootstrap.min.js"></script>
<script src="js/jquery.metisMenu.js"></script>
<script src="js/custom-scripts2.js"></script>
<script src="js/jquery.dataTables.js"></script>
<script src="js/dataTables.bootstrap.js"></script>
<script>
        $(document).ready(function () {
            $('#dataTables-example').dataTable();
        });
    </script>
<link rel="stylesheet" href="css/bootstrapValidator.css" />
<script type="text/javascript" src="js/bootstrapValidator.js"></script>
<script type="text/javascript"
	src="http://api.map.baidu.com/api?v=2.0&ak=iD2gwtGfo1p98lPenidUyx8h"></script>
<script type="text/javascript"
	src="http://api.map.baidu.com/library/TextIconOverlay/1.2/src/TextIconOverlay_min.js"></script>
<script type="text/javascript" src="js/MarkerClusterer.js"></script>
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
				<br /> <br />
				<ol class="breadcrumb">

					<li><a href="NewFile.jsp">主界面</a></li>
					<li class="active"><a href="equipmentServlet?action=fenbu">设备分布</a></li>
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
										style="height: 900px; width: 100%; margin: 0px auto; font-size: 16px;">
									</div>

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


</body>
</html>

<script type="text/javascript">
    // 百度地图API功能
    var map = new BMap.Map("allmap");

    //map.setMapStyle({ style: 'midnight' });
    map.centerAndZoom(new BMap.Point(119.794, 31.335), 6);
    map.enableScrollWheelZoom();

    map.addControl(new BMap.MapTypeControl());
    map.addControl(new BMap.NavigationControl());               // 添加平移缩放控件
    map.addControl(new BMap.ScaleControl());                    // 添加比例尺控件
    map.addControl(new BMap.OverviewMapControl());              //添加缩略地图控件
    var MAX = 3000;
    var markers = [];
    var i = 0;
      var jingdu = new Array();
      var weidu = new Array();

    var Msgs = new Array();


          var opts = {
        width: 350,     // 信息窗口宽度
        height: 80,     // 信息窗口高度
        title: "信息窗口", // 信息窗口标题
        enableMessage: true//设置允许信息窗发送短息
    };


         
         
         <%
			List<equipment> list = (List) request.getAttribute("list");
			if (list == null || list.size() < 1) {
				out.print("没有数据");
			} else {
				for (equipment equipment: list) {
					List<String> list1;
					
		%>

		
	    var pt = new BMap.Point(<%=equipment.getJingdu()%>, <%=equipment.getWeidu()%>);
        var marker = new BMap.Marker(pt);  // 创建标注
     var content = "编号:<%=equipment.getId()%>  名称：<%=equipment.getName()%>  类型：<%=equipment.getLeixing()%>";
     addClickHandler(content, marker); //添加点击事件
        markers.push(marker);
	    
		

	<%
				}

				}
			%>






     function addClickHandler(content,marker){
marker.addEventListener("click",function(e){

openInfo(content,e)}
);
}


function openInfo(content,e){
var p = e.target;
var point = new BMap.Point(p.getPosition().lng, p.getPosition().lat);
var infoWindow = new BMap.InfoWindow(content,opts); // 创建信息窗口对象
map.openInfoWindow(infoWindow,point); //开启信息窗口
}





    //最简单的用法，生成一个marker数组，然后调用markerClusterer类即可。
    var markerClusterer = new BMapLib.MarkerClusterer(map, { markers: markers });
</script>