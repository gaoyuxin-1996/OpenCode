/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50529
Source Host           : localhost:3306
Source Database       : mytest

Target Server Type    : MYSQL
Target Server Version : 50529
File Encoding         : 65001

Date: 2016-09-30 09:01:04
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `username` varchar(11) NOT NULL,
  `password` varchar(20) NOT NULL,
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `asset` float(10,2) DEFAULT '0.00',
  `status` smallint(1) DEFAULT '1',
  `createtime` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `updatetime` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('15988888888', '55555', '1', '238.00', '1', '2016-09-10 10:39:33', '2016-09-26 11:42:10');
INSERT INTO `user` VALUES ('15977777777', '12', '2', '971.00', '1', '2016-09-10 11:43:19', '2016-09-10 11:43:19');
