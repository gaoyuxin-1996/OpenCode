package test;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.InputStreamReader;

public class yilie {
	public static void main(String[] args) throws Exception {
		String messall = null;
		String messall2 = null;
		int o = 1;
		try {
			for (int u = 0; u < 2; u++) {
				/* 读入TXT文件 */

				String pathname = "C:\\Users\\Administrator\\Desktop\\172_ALL\\05\\92_" + o + ".txt"; // 绝对路径或相对路径都可以，这里是绝对路径，写入文件时演示相对路径
				File filename = new File(pathname);
				InputStreamReader reader = new InputStreamReader(new FileInputStream(filename));
				BufferedReader br = new BufferedReader(reader);
				String line = "";
				while (line != null) {
					line = br.readLine(); // 一次读入一行数据
					messall = messall + line;

				}

				br.close();
				o = o + 1;
			}
			messall = messall.replaceAll("null", "");
			System.out.println(messall);
			for (int u = 0; u < 92; u++) {
				for (int i = 0; i < 2; i++) {
					messall2 = messall2 + messall.substring(u * 2 + 92 * i * 2, u * 2 + 92 * i * 2 + 2);
					messall2 = messall2.replaceAll("null", "");
				}
				messall2 = messall2 + "\r\n";
			}
			System.out.println(messall2);
			File writename = new File("C:\\Users\\Administrator\\Desktop\\172_ALL\\05\\all92.txt");
			writename.createNewFile();
			BufferedWriter out = new BufferedWriter(new FileWriter(writename));
			out.write(messall2);
			out.flush(); // 把缓存区内容压入文件
			out.close(); // 最后记得关闭文件
		} catch (Exception e) {
			e.printStackTrace();
		}

	}
}
