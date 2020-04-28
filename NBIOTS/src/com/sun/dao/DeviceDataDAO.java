package com.sun.dao;
import java.sql.SQLException;
import java.util.List;
//�������t_Devcie����Ч
import com.sun.model.DeviceData;
/**
 * �豸���ݽӿ�
 * @author sunshixing
 * */
public interface DeviceDataDAO {
	 /**
     * ��������
     * @param devicedata
     * @return int
     * @throws SQLException
     */
	 int insertDeviceData(DeviceData devicedata) throws SQLException;
	 /**
	  * ɾ������
	  * @param nodeId
	  * @return int
	  * @throws SQLException
	  */
	 int deleteDevicedata(String nodeId) throws SQLException;
	 /**
	     * �޸��豸���� ʹ�ñ���Ϊ��ǰ��������
	     * @param devicedata
	     * @return int
	     * @throws SQLException
	     */
	 int updateDevicedata(DeviceData devicedata) throws SQLException;
	 /**
	  * ����nodeId��ѯ��¼
	  * @param nodeId
	  * @return list
	  * @throws SQLException
	  * */
	 
	 DeviceData searchByNodeId(String nodeId) throws SQLException;
	 /**
	  * ��ȡ���м�¼
	  * @return list
	  * @throw SQLException
	  * */
	 
	 List<DeviceData> getALLRecords() throws SQLException;
	 
	 

	 
	 
}
