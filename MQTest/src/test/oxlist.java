package test;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;

public class oxlist {
	public static void main(String[] args) throws IOException {
		File writename = new File("C:\\Users\\Administrator\\Desktop\\xx.txt");
		writename.createNewFile();
		BufferedWriter out = new BufferedWriter(new FileWriter(writename));
		int a = 2;
		String mess = "{";
		for (int i = 0; i < 10999; i++) {
			mess = mess + "0,0xff,";
			System.err.println(a);
			a = a + 2;
		}
		System.out.println(a);
		mess = mess + "0,0xff}";
		out.write(mess);
		out.flush(); // 把缓存区内容压入文件
		out.close(); // 最后记得关闭文件
	}
}
