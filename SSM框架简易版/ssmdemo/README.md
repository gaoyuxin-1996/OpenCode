#SSMDemo

部署注意：  可以参考博客  http://www.cnblogs.com/smartLamb/p/6768201.html
1. 执行一下 src/main/resources/ 下的init.sql 用于初始化数据
2. 由于本项目是使用h5 页面进行ajax交互的，请求接口是以 “/” 根目录路径下的，不会带上项目名称，而tomcat默认的部署又会把接口地址带上项目名称，导致接口请求不到，报404错误，有以下两个解决方案
    - 1.直接把项目的war包部署到 tomcat 的 webapps/ROOT文件夹下，并把ROOT下原来的文件删除（线上常用）
    - 2.给项目配置tomcat后，启动tamcat，然后如下图所示，把Eclipse中的 Servers项目（自动生成的），对应工程名XXX-config的server.xml中的Context标签中的path属性写成 “/”,然后重启tomcat

![这里只是案例，如果你的项目名为SSM，则找到SSM-config](https://git.oschina.net/uploads/images/2017/0426/112249_f7f92837_1325538.png "这里只是案例，如果你的项目名为SSM，则找到SSM-config")
![输入图片说明](https://git.oschina.net/uploads/images/2017/0426/112918_059539fb_1325538.png "在这里输入图片标题")