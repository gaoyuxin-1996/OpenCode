package test;

import javax.jms.Connection;
import javax.jms.ConnectionFactory;
import javax.jms.DeliveryMode;
import javax.jms.JMSException;
import javax.jms.Message;
import javax.jms.MessageConsumer;
import javax.jms.MessageListener;
import javax.jms.MessageProducer;
import javax.jms.Queue;
import javax.jms.Session;
import javax.jms.TextMessage;
import javax.jms.Topic;

import org.apache.activemq.ActiveMQConnectionFactory;

class MQtest {

	// ��Ե�ģʽ��һ����Ϣֻ�ܱ�һ�������߽�����Ϣδ����ʱ�����洢
	public void testMQProducerQueue() throws Exception {
		// 1�������������Ӷ�����Ҫ�ƶ�ip�Ͷ˿ں�
		ConnectionFactory connectionFactory = new ActiveMQConnectionFactory("tcp://47.92.236.118:61616");
		// 2��ʹ�����ӹ�������һ�����Ӷ���
		Connection connection = connectionFactory.createConnection();
		// 3����������
		connection.start();
		// 4��ʹ�����Ӷ��󴴽��Ự��session������
		Session session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
		// 5��ʹ�ûỰ���󴴽�Ŀ����󣬰���queue��topic��һ��һ��һ�Զࣩ
		Queue queue = session.createQueue("CQSIOT");
		// 6��ʹ�ûỰ���󴴽������߶���
		MessageProducer producer = session.createProducer(queue);
		// 7��ʹ�ûỰ���󴴽�һ����Ϣ����
		TextMessage textMessage = session.createTextMessage(
				"413130313135333532393232373338363533353230333032343830373331303138303730303031202031333234383237362020202020202020302E30342020202020202020302E30302020202020302E3030303030303030302E303030303030302E3030303334392E33302032302E3030303030312E303030303030303030");

		textMessage.setStringProperty("select", "868474045394903");
		// 8��������Ϣ
		producer.send(queue, textMessage, DeliveryMode.PERSISTENT, 4, 3000);
		// 9���ر���Դ
		producer.close();
		session.close();
		connection.close();
	}

	public void TestMQConsumerQueue() throws Exception {
		// 1�������������Ӷ�����Ҫ�ƶ�ip�Ͷ˿ں�
		ConnectionFactory connectionFactory = new ActiveMQConnectionFactory("tcp://47.92.236.118:61616");
		// 2��ʹ�����ӹ�������һ�����Ӷ���
		Connection connection = connectionFactory.createConnection();
		// 3����������
		connection.start();
		// 4��ʹ�����Ӷ��󴴽��Ự��session������
		Session session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
		// 5��ʹ�ûỰ���󴴽�Ŀ����󣬰���queue��topic��һ��һ��һ�Զࣩ
		Queue queue = session.createQueue("CQSIOT");
		// 6��ʹ�ûỰ���󴴽������߶���
		MessageConsumer consumer = session.createConsumer(queue, "select='868474045394903'");
		// 7����consumer����������һ��messageListener��������������Ϣ
		consumer.setMessageListener(new MessageListener() {

			@Override
			public void onMessage(Message message) {
				// TODO Auto-generated method stub
				if (message instanceof TextMessage) {
					TextMessage textMessage = (TextMessage) message;
					try {
						System.out.println(textMessage.getText());
					} catch (JMSException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}
			}
		});
		// 8������ȴ������û���Ϣ
		System.in.read();
		// 9���ر���Դ
		consumer.close();
		session.close();
		connection.close();
	}

	// ��������ģʽ����Ϣ�����ᱻ�־û��洢����Ϣ������ᱻ���������߽���
	public void TestTopicProducer() throws Exception {
		ConnectionFactory connectionFactory = new ActiveMQConnectionFactory("tcp://120.55.49.108:61616");
		Connection connection = connectionFactory.createConnection();
		connection.start();
		Session session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
		Topic topic = session.createTopic("QSIOT");
		MessageProducer producer = session.createProducer(topic);
		TextMessage textMessage = session.createTextMessage("����һ���豸ָ��״̬�����ı䡣ָ��ID:12522. ״̬��Ϊ��TIME_OUT");
		textMessage.setStringProperty("select", "1");
		producer.send(textMessage);
		producer.close();
		session.close();
		connection.close();
	}

	public void TestTopicConsumer() throws Exception {
		// 1�������������Ӷ�����Ҫ�ƶ�ip�Ͷ˿ں�
		ConnectionFactory connectionFactory = new ActiveMQConnectionFactory("tcp://120.55.49.108:61616");
		// 2��ʹ�����ӹ�������һ�����Ӷ���
		Connection connection = connectionFactory.createConnection();
		// 3����������
		connection.start();
		// 4��ʹ�����Ӷ��󴴽��Ự��session������
		Session session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
		// 5��ʹ�ûỰ���󴴽�Ŀ����󣬰���queue��topic��һ��һ��һ�Զࣩ
		Topic topic = session.createTopic("test-topic");
		// 6��ʹ�ûỰ���󴴽������߶���
		MessageConsumer consumer = session.createConsumer(topic);
		// 7����consumer����������һ��messageListener��������������Ϣ
		consumer.setMessageListener(new MessageListener() {

			@Override
			public void onMessage(Message message) {
				// TODO Auto-generated method stub
				if (message instanceof TextMessage) {
					TextMessage textMessage = (TextMessage) message;
					try {
						System.out.println(textMessage.getText());
					} catch (JMSException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}
			}
		});
		// 8������ȴ������û���Ϣ
		System.in.read();
		// 9���ر���Դ
		consumer.close();
		session.close();
		connection.close();
	}

	public static void main(String[] args) throws Exception {
		MQtest test = new MQtest();
		test.TestMQConsumerQueue();

	}
}