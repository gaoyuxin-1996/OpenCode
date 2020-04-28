<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>PLC管控</title>

<link href="css/bootstrap.css" rel="stylesheet">
<link href="css/font-awesome.css" rel="stylesheet">
<link href="css/morris-0.4.3.min.css" rel="stylesheet">
<link href="css/custom-styles.css" rel="stylesheet">
<link rel="stylesheet" href="css/cssCharts.css">
<link href="css/dataTables.bootstrap.css" rel="stylesheet">
<script type="text/javascript"
	src="http://api.map.baidu.com/api?v=2.0&amp;ak=iD2gwtGfo1p98lPenidUyx8h"></script>
<script type="text/javascript"
	src="http://api.map.baidu.com/getscript?v=2.0&amp;ak=iD2gwtGfo1p98lPenidUyx8h&amp;services=&amp;t=20180629105709"></script>

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
<!-- Custom Js -->


<link rel="stylesheet" href="css/bootstrapValidator.css">
<script type="text/javascript" src="js/bootstrapValidator.js"></script>

</head>

<body style="">
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
				<br> <br>
				<ol class="breadcrumb">
					<li><a href="NewFile.jsp">主界面</a></li>
					<li class="active">离线设备</li>
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
														aria-sort="ascending"
														aria-label="设备类型: activate to sort column ascending"
														style="width: 129px;">操作人</th>
													<th class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="设备ID: activate to sort column ascending"
														style="width: 285px;">操作类型</th>
													<th class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="设备名称: activate to sort column ascending"
														style="width: 296px;">操作对象</th>
													<th class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="地址: activate to sort column ascending"
														style="width: 648px;">操作时间</th>
													<th class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="详情: activate to sort column ascending"
														style="width: 72px;">详情</th>
												</tr>
											</thead>
											<tbody>


												
											</tbody>
										</table>
									</div>
								</div>
							</div>
							<!--End Advanced Tables -->
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