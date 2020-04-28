<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8"%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>智能烟感报警系统</title>

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
														style="width: 129px;">设备类型</th>
													<th class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="设备ID: activate to sort column ascending"
														style="width: 285px;">设备ID</th>
													<th class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="设备名称: activate to sort column ascending"
														style="width: 296px;">设备名称</th>
													<th class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="地址: activate to sort column ascending"
														style="width: 648px;">地址</th>
													<th class="sorting" tabindex="0"
														aria-controls="dataTables-example" rowspan="1" colspan="1"
														aria-label="详情: activate to sort column ascending"
														style="width: 72px;">详情</th>
												</tr>
											</thead>
											<tbody>


												<tr class="gradeU odd">
													<td class="sorting_1"><img src="img/设备.jpg" width="30"
														height="30">设备</td>


													<td
														title="863703037704538 心跳周期: 1440 最近心跳时间: 2018/6/29 15:27:18"
														class=" ">863703037704538</td>
													<td class=" "></td>
													<td class=" ">杭州市滨江区物联网街</td>
													<td style="text-align: center" class=" "><a
														href="/show/Index?SB_no=863703037704538">详情</a></td>


												</tr>
												<tr class="gradeU even">
													<td class="sorting_1"><img src="img/设备.jpg" width="30"
														height="30">设备</td>
													<td
														title="863703037716417 心跳周期: 1440 最近心跳时间: 2018/7/9 15:15:30"
														class=" ">863703037716417</td>
													<td class=" ">863703037716417</td>
													<td class=" ">泉州市晋江市崇祥街151号</td>
													<td style="text-align: center" class=" "><a
														href="/show/Index?SB_no=863703037716417">详情</a></td>


												</tr>
												<tr class="gradeU odd">
													<td class="sorting_1"><img src="img/设备.jpg" width="30"
														height="30">设备</td>
													<td
														title="863703037726200 心跳周期: 1440 最近心跳时间: 2018/7/16 15:09:22"
														class=" ">863703037726200</td>
													<td class=" ">测试烟感</td>
													<td class=" ">昆明市盘龙区万宏路254号金尚壹号2902室</td>
													<td style="text-align: center" class=" "><a
														href="/show/Index?SB_no=863703037726200">详情</a></td>


												</tr>
												<tr class="gradeU even">
													<td class="sorting_1"><img src="img/设备.jpg" width="30"
														height="30">设备</td>


													<td
														title="863703037864324 心跳周期: 1440 最近心跳时间: 2018/6/22 10:09:06"
														class=" ">863703037864324</td>

													<td class=" ">长沙</td>
													<td class=" ">长沙市岳麓区金谷路</td>



													<td style="text-align: center" class=" "><a
														href="/show/Index?SB_no=863703037864324">详情</a></td>


												</tr>
												<tr class="gradeU odd">
													<td class="sorting_1"><img src="img/设备.jpg" width="30"
														height="30">设备</td>


													<td
														title="863703037847493 心跳周期: 1440 最近心跳时间: 2018/7/12 15:47:07"
														class=" ">863703037847493</td>

													<td class=" ">电信卡测试</td>
													<td class=" ">南京市江宁区水阁路</td>



													<td style="text-align: center" class=" "><a
														href="/show/Index?SB_no=863703037847493">详情</a></td>


												</tr>
												<tr class="gradeU even">
													<td class="sorting_1"><img src="img/设备.jpg" width="30"
														height="30">设备</td>


													<td
														title="863703037725699 心跳周期: 1440 最近心跳时间: 2018/7/12 16:53:00"
														class=" ">863703037725699</td>

													<td class=" ">电信卡测试2</td>
													<td class=" ">南京市江宁区水阁路</td>



													<td style="text-align: center" class=" "><a
														href="/show/Index?SB_no=863703037725699">详情</a></td>


												</tr>
												<tr class="gradeU odd">
													<td class="sorting_1"><img src="img/设备.jpg" width="30"
														height="30">设备</td>


													<td
														title="863703037862633 心跳周期: 1440 最近心跳时间: 2018/7/12 11:25:58"
														class=" ">863703037862633</td>

													<td class=" ">东方世纪烟感001</td>
													<td class=" ">南京市江宁区水阁路30号</td>



													<td style="text-align: center" class=" "><a
														href="/show/Index?SB_no=863703037862633">详情</a></td>


												</tr>
												<tr class="gradeU even">
													<td class="sorting_1"><img src="img/设备.jpg" width="30"
														height="30">设备</td>


													<td
														title="863703037848913 心跳周期: 1440 最近心跳时间: 2018/7/12 11:42:19"
														class=" ">863703037848913</td>

													<td class=" ">东方世纪烟感002</td>
													<td class=" ">南京市江宁区水阁路30号</td>



													<td style="text-align: center" class=" "><a
														href="/show/Index?SB_no=863703037848913">详情</a></td>


												</tr>
												<tr class="gradeU odd">
													<td class="sorting_1"><img src="img/设备.jpg" width="30"
														height="30">设备</td>


													<td
														title="863703037737827 心跳周期: 1440 最近心跳时间: 2018/6/15 15:56:55"
														class=" ">863703037737827</td>

													<td class=" ">烟感</td>
													<td class=" ">宁波市江北区赛特威尔电子</td>



													<td style="text-align: center" class=" "><a
														href="/show/Index?SB_no=863703037737827">详情</a></td>


												</tr>
												<tr class="gradeU even">
													<td class="sorting_1"><img src="img/设备.jpg" width="30"
														height="30">设备</td>


													<td
														title="863703037875346 心跳周期: 1440 最近心跳时间: 2018/8/3 16:16:17"
														class=" ">863703037875346</td>

													<td class=" ">烟感</td>
													<td class=" ">长沙市天心区友谊路</td>



													<td style="text-align: center" class=" "><a
														href="/show/Index?SB_no=863703037875346">详情</a></td>


												</tr>
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