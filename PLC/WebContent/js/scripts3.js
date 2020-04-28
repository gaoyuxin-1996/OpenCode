$(document).ready(function() {
	$("#onoffswitch").on('click', function() {
		clickSwitch()
	});

	var clickSwitch = function() {
		if ($("#onoffswitch").is(':checked')) {

$.ajax({
url : "tcpServlet",
dataType : "json",
data : {
	action : "zhi",  
	key:"K1",
	way: 1,
	data : 1
},
success : function(data) {



}
});
		} else {

			$.ajax({
				url : "tcpServlet",
				dataType : "json",
				data : {
					action : "zhi",  
					key:"K1",
					way: 1,
					data : 0
				},
				success : function(data) {

				}
			});
		}
	};
});
$(document).ready(function() {
	$("#onoffswitch1").on('click', function() {
		clickSwitch()
	});

	var clickSwitch = function() {
		if ($("#onoffswitch1").is(':checked')) {
			$.ajax({
				url : "tcpServlet",
				dataType : "json",
				data : {
					action : "zhi", 
					key:"K2",
					way: 1,
					data : 1
				},
				success : function(data) {


				}
			});
		} else {
			$.ajax({
				url : "tcpServlet",
				dataType : "json",
				data : {
					action : "zhi", 
					key:"K2",
					way: 1,
					data : 0
				},
				success : function(data) {


				}
			});
		}
	};
});
$(document).ready(function() {
	$("#onoffswitch2").on('click', function() {
		clickSwitch()
	});

	var clickSwitch = function() {
		if ($("#onoffswitch2").is(':checked')) {
			$.ajax({
				url : "tcpServlet",
				dataType : "json",
				data : {
					key:"K3",
					action:"zhi",
					way: 1,
					data : 1
				},
				success : function(data) {


				}
			});
		} else {
			$.ajax({
				url : "tcpServlet",
				dataType : "json",
				data : {
					key:"K3",
					action:"zhi",
					way: 1,
					data : 0
				},
				success : function(data) {



				}
			});
		}
	};
})

$(document).ready(function() {
	$("#onoffswitch19").on('click', function() {
		clickSwitch()
	});

	var clickSwitch = function() {
		if ($("#onoffswitch19").is(':checked')) {

$.ajax({
url : "tcpServlet",
dataType : "json",
data : {
	action : "startorstop",  
	data : 1
},
success : function(data) {



}
});
		} else {

			$.ajax({
				url : "tcpServlet",
				dataType : "json",
				data : {
					action : "startorstop",  
					data : 0
				},
				success : function(data) {

				}
			});
		}
	};
});