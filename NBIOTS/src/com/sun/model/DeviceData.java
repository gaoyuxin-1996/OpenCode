package com.sun.model;


/**
 * t_DeviceChange数据表
 * created by sunshixing
 * */
public class DeviceData {
	    private String nodeId,name,manufacturerId,manufacturerName,
	                   deviceType,model,serviceId,data,eventTime,status;
	    public DeviceData() {}//构造函数
	    //重载toString
	    public String toString() {
	    	
	    	return "nodeId="+nodeId+"name="+name+"manufacturerId"+manufacturerId+
	    			"manufacturerName="+manufacturerName+"deviceType="+deviceType+
	    			"model="+model+"serviceId="+serviceId+"data="+data+"eventTime="+eventTime+
	    			"status="+status;	
	    	
	    }
	
	   //nodeId
	   public String getNodeId() {
	        return nodeId;
	    }

	    public void setNodeId(String nodeId) {
	        this.nodeId = nodeId;
	    }
	    
	    //name
	    public String getName() {
	    	return name;
	    }
	    public void setName(String name){
	    	
	    	this.name=name;
	    }
	    
	    //manufacturerId
	    public String getManufacturerId() {
	    	
	    	return manufacturerId;
	    }
	    public void setManufacturerId(String manufacturerId) {
	    	
	    	this.manufacturerId=manufacturerId;
	    }
	    
	    //manufacturerName
        public String getManufacturerName() {
	    	
	    	return manufacturerName;
	    }
	    public void setManufacturerName(String manufacturerName) {
	    	
	    	this.manufacturerName=manufacturerName;
	    }
	    //deviceType
	    public String getDeviceType() {
	    	
	    	return deviceType;
	    }
	    public void setDeviceType(String deviceType) {
	    	this.deviceType=deviceType;
       }
	   //model 
	   public String getModel() {
		   
		   return model;
	   }
	   public void setModel(String model) {
		   
		   this.model=model;
		   
	   }
    //serviceId
	   public String getServiceId() {
		   
		   return serviceId;
	   }
	   public void setServiceId(String serviceId) {
		   
		   this.serviceId=serviceId;
	   }
	 //data
      public String getData() {
    	  return data;    	  
      }	   
      public void setData(String data) {
    	  
    	  this.data=data;
      }
      //eventTime
      public String getEventTime() {
    	  
    	  return eventTime;
      }
      public void setEventTime(String eventTime) {
    	  
    	  this.eventTime=eventTime;
      }
      
      //status
      public String getStatus() {
    	  
    	  return status;
      }
      public void setStatus(String status) {
    	  
    	  this.status=status;
      }
      
}
