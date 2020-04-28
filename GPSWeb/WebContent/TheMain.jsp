<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta charset="utf-8" />
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

        $(document).ready(function () {
            GetDetails();
            setInterval("GetDetails()", 9000);

        });
        function GetDetails() {
            $.post("/Main/Detail", { order: "1" }, function (data) {           
                $("#AJAXMAIN").html(data);//这里是重点，右侧数据获取后要显示到div中  
            }, "text");
        }
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
											<a href="../tablelist/bjlist" style="color: #F36A5A">0</a>
										</h3>
										<small><a href="../tablelist/bjlist"
											style="color: #F36A5A">XX</a></small>
									</div>
									<div class="icon">
										<i class="fa fa-bell-o fa-5x red"></i>
									</div>

								</div>
							</div>
						</div>

						<div class="col-md-3 col-sm-12 col-xs-12">
							<div class="board">
								<div class="panel panel-primary">
									<div class="number">
										<h3>
											<a href="../tablelist/gzlist" style="color: #9B9B9B">12</a>
										</h3>
										<small> <a href="../tablelist/gzlist"
											style="color: #9B9B9B">XX</a>
										</small>
									</div>
									<div class="icon">
										<i class="fa fa-cog fa-5x" style="background-color: #9B9B9B"></i>
									</div>

								</div>
							</div>
						</div>

						<div class="col-md-3 col-sm-12 col-xs-12">
							<div class="board">
								<div class="panel panel-primary">
									<div class="number">

										<h3>

											<a href="../tablelist/qylist" style="color: #FABE28">0</a>
										</h3>

										<small><a href="../tablelist/qylist"
											style="color: #FABE28">XX</a></small>

									</div>
									<div class="icon">
										<i class="fa fa-signal fa-5x yellow"></i>
									</div>

								</div>
							</div>
						</div>



						<div class="col-md-3 col-sm-12 col-xs-12">
							<div class="board">
								<div class="panel panel-primary">
									<div class="number">
										<h3>
											<a href="../tablelist/zclist" style="color: #1ABC9C">0</a>
										</h3>

										<small> <a href="../tablelist/zclist"
											style="color: #1ABC9C">XX</a>

										</small>
									</div>
									<div class="icon">
										<i class="fa  fa-circle-o fa-5x green"></i>
									</div>

								</div>
							</div>
						</div>

					</div>
					<!-- /. ROW  -->





					<div class="row">
						<div class="col-xs-6 col-md-3">
							<div class="panel panel-default">
								<div class="panel-body easypiechart-panel">
									<h4>设备数量</h4>
									<div class="easypiechart" id="easypiechart-red"
										data-percent="0">
										<span class="percent">0</span>
									</div>
								</div>
							</div>
						</div>

						<div class="col-xs-6 col-md-3">
							<div class="panel panel-default">
								<div class="panel-body easypiechart-panel">
									<h4>XXXX</h4>
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
									<h4>XXXX</h4>
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
									<h4>XXXX</h4>
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
					<p>Copyright &copy; 2018.Company name All rights reserved.</p>
					</footer>
				</div>
			</div>
		</div>


		<script src="js/bootstrap.min.js"></script>
		<script src="js/jquery.metisMenu.js"></script>


		<script src="js/custom-scripts.js"></script>

		<script src="js/jquery.dataTables.js"></script>
		<script src="js/dataTables.bootstrap.js"></script>
		<script>
        $(document).ready(function () {
            $('#dataTables-example').dataTable();
        });
    </script>
</body>
</html>
<script type="text/javascript">
	
	var map = new BMap.Map("allmap");
	var point = new BMap.Point(120.0, 36.0);
	map.centerAndZoom(point, 15);
	var marker = new BMap.Marker(point); 
	map.addOverlay(marker);               
	marker.setAnimation(BMAP_ANIMATION_BOUNCE); 
   map.enableScrollWheelZoom(true);
</script>