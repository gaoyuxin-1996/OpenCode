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
			$.ajax({
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
							shishi(value);
							
							
							
						}
					})
		}
	};

	
	

	
	function shishi() {
		
		$.ajax({
			url : "dianbiaoServlet",
			dataType : "json",
			data : {
				SB_plcid : document.getElementById("wangguan").value,
				way:document.getElementById("way").value,
				action : "xinxi"
			},
			success : function(msg) {
			
				var tbody = window.document.getElementById("biaoge");
				
                var str = "";
                var data1 = msg.data;
              
                for (i in data1) {
                    str += "<tr>" +
                        "<td align='center'>" + data1[i].name + "</td>" +
                        "<td align='center'>" + data1[i].bool + "</td>" +
                        "<td align='center'>" + data1[i].zhi + "</td>" +
                        "</tr>";
                        
                }
                tbody.innerHTML = str;
            
			}
		})
		 setTimeout(function(){shishi();},1000);
	}
	
	function Companyall() {

		document.getElementById("t_company").length = 0;

		document.getElementById("t_company").options
				.add(new Option("--选择一级菜单--", 0));
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
					<li class="active">实时监控</li>

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
											id="t_company" onfocus="Companyall()"
											onchange="Company(this.options[this.selectedIndex].index,this.options[this.selectedIndex].value);">
											<option selected="selected" value="">--选择一级菜单--</option>
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
										<a>设备地址：</a><input id="address" type="text"
											style="border: 0px;width: 250px">
											<a>网关ID：</a><input
											id="wangguan" type="text" style="border: 0px;width: 250px;">
											<a>通道：</a><input
											id="way" type="text" style="border: 0px">
									</div>
									<br>
									<div class="panel panel-default">

										<table class="table table-striped table-bordered table-hover"
											id="dataTables-example">
											<thead>
												<tr>
													<th>变量名</th>
													<th>变量类型</th>
													<th>变量值</th>

												</tr>

											</thead>
											<tbody id="biaoge">

											</tbody>
										</table>


									</div>
									<hr>
									<div>
									视频模块
									
					<div>				
<object classid="clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95"  
    codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,1,5,217"  
    id="MediaPlayer" type=application/x-oleobject width="560" height="360"  
    standby="Loading Microsoft Windows Media Player components..."  
    VIEWASTEXT align="middle">  
    <!--播放的文件地址-->  
    <param name="Filename" value="https://f.us.sinaimg.cn/002zkjF3lx07pwEC0muI0104020096G20k010.mp4?label=mp4_720p&template=1280x720.27.0&Expires=1543371667&ssig=rXFmwMe4Jz&KID=unistore,video" valuetype="ref" />  
    <!--是否自动调整播放大小-->  
    <param name="AutoSize" value="0" />  
    <!--是否自动播放-->  
    <param name="AutoStart" value="0" />  
    <param name="AudioStream" value="-1" />  
    <param name="AnimationAtStart" value="0" />  
    <param name="AllowScan" value="-1" />  
    <param name="BaseURL" value="" />  
    <param name="AllowChangeDisplaySize" value="0" />  
    <param name="AutoRewind" value="0" />  
    <!--左右声道平衡,最左-9640,最右9640-->  
    <param name="Balance" value="0" />  
    <!--缓冲时间-->  
    <param name="BufferingTime" value="5" />  
    <param name="CaptioningID" value="" />  
    <param name="ClickToPlay" value="0" />  
    <param name="CursorType" value="32512" />  
    <!--当前播放进度 -1 表示不变,0表示开头 单位是秒,比如10表示从第10秒处开始播放,值必须是-1.0或大于等于0-->  
    <param name="CurrentPosition" value="-1" />  
    <param name="CurrentMarker" value="0" />  
    <param name="DefaultFrame" value="1" />  
    <param name="DisplayBackColor" value="0" />  
    <param name="DisplayForeColor" value="16777215" />  
    <param name="DisplayMode" value="0" />  
    <!--视频1-50%, 0-100%, 2-200%,3-全屏 其它的值作0处理,小数则采用四舍五入然后按前的处理-->  
    <param name="DisplaySize" value="0" />  
    <param name="Enabled" value="-1" />  
    <!-- 是否用右键弹出菜单控制-->  
    <param name="EnableContextMenu" value="-1" />  
    <param name="EnablePositionControls" value="0" />  
    <param name="EnableFullScreenControls" value="0" />  
    <!--是否允许拉动播放进度条到任意地方播放-->  
    <param name="EnableTracker" value="1" />  
    <param name="InvokeURLs" value="-1" />  
    <param name="Language" value="-1" />  
    <!--是否静音-->  
    <param name="Mute" value="0" />  
    <!--重复播放次数,0为始终重复-->  
    <param name="PlayCount" value="1" />  
    <param name="PreviewMode" value="0" />  
    <!--播放速率控制,1为正常,允许小数-->  
    <param name="Rate" value="1" />  
    <!--SAMI样式-->  
    <param name="SAMIStyle" value="" />  
    <!--SAMI语言-->  
    <param name="SAMILang" value="" />  
    <!--字幕ID-->  
    <param name="SAMIFilename" value="" />  
    <!--是否显示字幕,为一块黑色,下面会有一大块黑色,一般不显示-->  
    <param name="ShowCaptioning" value="0" />  
    <!--是否显示控制,比如播放,停止,暂停-->  
    <param name="ShowControls" value="-1" />  
    <!--是否显示音量控制-->  
    <param name="ShowAudioControls" value="-1" />  
    <!--显示节目信息,比如版权等-->  
    <param name="ShowDisplay" value="0" />  
    <!--是否启用上下文菜单-->  
    <param name="ShowGotoBar" value="0" />  
    <!--是否显示往前往后及列表,如果显示一般也都是灰色不可控制-->  
    <param name="ShowPositionControls" value="-1" />  
    <!-- 默认是1 -->  
    <!--当前播放信息,显示是否正在播放,及总播放时间和当前播放到的时间-->  
    <param name="ShowStatusBar" value="-1" />  
    <!-- 默认是1 -->  
    <!--是否显示当前播放跟踪条,即当前的播放进度条-->  
    <param name="ShowTracker" value="-1" />  
    <!-- 默认是1 -->  
    <!--显示部的宽部,如果小于视频宽,则最小为视频宽,或者加大到指定值,并自动加大高度.此改变只改变四周的黑框大小,不改变视频大小-->  
    <param name="VideoBorderWidth" value="0" />  
    <!--显示黑色框的颜色, 为RGB值,比如ffff00为黄色-->  
    <param name="VideoBorderColor" value="0" />  
    <!--音量大小,负值表示是当前音量的减值,值自动会取绝对值,最大为0,最小为-9640-->  
    <param name="Volume" value="-1070" />  
    <!--如果是0可以允许全屏,否则只能在窗口中查看-->  
    <param name="WindowlessVideo" value="0" />  
    <param name="TransparentAtStart" value="-1" />  
    <!-- 默认是0 -->  
    <param name="VideoBorder3D" value="0" />  
    <param name="SelectionStart" value="0" />  
    <param name="SelectionEnd" value="true" />  
    <param name="SendOpenStateChangeEvents" value="-1" />  
    <param name="SendWarningEvents" value="-1" />  
    <param name="SendErrorEvents" value="-1" />  
    <param name="SendKeyboardEvents" value="0" />  
    <param name="SendMouseClickEvents" value="0" />  
    <param name="SendMouseMoveEvents" value="0" />  
    <param name="SendPlayStateChangeEvents" value="-1" />  
</object>  
									
									
					</div>				
									
									
									</div>
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