<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Insert title here</title>
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
	<style type="text/css">
	body, html,#allmap {width: 100%;height: 100%;overflow: hidden;margin:0}
	</style>
	<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=mzUiUrREA3HGQE3B4T933XPHVKBQb3j8"></script>
	<title>GPS</title>
</head>
<body>
	<div id="allmap"></div>
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