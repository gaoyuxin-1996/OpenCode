package com.item;

public class equipment {

	private String nodeId;
	private String name;
	private String manufacturerId;
	private String manufacturerName;
	private String deviceType;
	private String model;
	private String serviceId;
	private String data;
	private String eventTime;
	private String status;

	public equipment() {
	}

	public String getNodeId() {
		return nodeId;
	}

	public void setNodeId(String nodeId) {
		this.nodeId = nodeId;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getManufacturerId() {
		return manufacturerId;
	}

	public void setManufacturerId(String manufacturerId) {
		this.manufacturerId = manufacturerId;
	}

	public String getManufacturerName() {
		return manufacturerName;
	}

	public void setManufacturerName(String manufacturerName) {
		this.manufacturerName = manufacturerName;
	}

	public String getDeviceType() {
		return deviceType;
	}

	public void setDeviceType(String deviceType) {
		this.deviceType = deviceType;
	}

	public String getModel() {
		return model;
	}

	public void setModel(String model) {
		this.model = model;
	}

	public String getServiceId() {
		return serviceId;
	}

	public void setServiceId(String serviceId) {
		this.serviceId = serviceId;
	}

	public String getData() {
		return data;
	}

	public void setData(String data) {
		this.data = data;
	}

	public String getEventTime() {
		return eventTime;
	}

	public void setEventTime(String eventTime) {
		this.eventTime = eventTime;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public equipment(String nodeId, String name, String manufacturerId, String manufacturerName, String deviceType,
			String model, String serviceId, String data, String eventTime, String status) {
		this.nodeId = nodeId;
		this.name = name;
		this.manufacturerId = manufacturerId;
		this.manufacturerName = manufacturerName;
		this.deviceType = deviceType;
		this.model = model;
		this.serviceId = serviceId;
		this.data = data;
		this.eventTime = eventTime;
		this.status = status;
	}

}
