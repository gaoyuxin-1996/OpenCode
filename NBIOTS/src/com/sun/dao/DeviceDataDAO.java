package com.sun.dao;
import java.sql.SQLException;
import java.util.List;
//仅仅针对t_Devcie表有效
import com.sun.model.DeviceData;
/**
 * 设备数据接口
 * @author sunshixing
 * */
public interface DeviceDataDAO {
	 /**
     * 插入数据
     * @param devicedata
     * @return int
     * @throws SQLException
     */
	 int insertDeviceData(DeviceData devicedata) throws SQLException;
	 /**
	  * 删除数据
	  * @param nodeId
	  * @return int
	  * @throws SQLException
	  */
	 int deleteDevicedata(String nodeId) throws SQLException;
	 /**
	     * 修改设备数据 使得表中为当前最新数据
	     * @param devicedata
	     * @return int
	     * @throws SQLException
	     */
	 int updateDevicedata(DeviceData devicedata) throws SQLException;
	 /**
	  * 根据nodeId查询记录
	  * @param nodeId
	  * @return list
	  * @throws SQLException
	  * */
	 
	 DeviceData searchByNodeId(String nodeId) throws SQLException;
	 /**
	  * 获取所有记录
	  * @return list
	  * @throw SQLException
	  * */
	 
	 List<DeviceData> getALLRecords() throws SQLException;
	 
	 

	 
	 
}
