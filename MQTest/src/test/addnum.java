package test;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

public class addnum {
	public static void main(String[] args) throws Exception {
		List<String> list = new ArrayList<>();
		List<String> list1 = new ArrayList<>();
		List<Integer> list2 = new ArrayList<>();
		try {

			/* 读入TXT文件 */
			String pathname = "C:\\Users\\Administrator\\Desktop\\po.txt"; // 绝对路径或相对路径都可以，这里是绝对路径，写入文件时演示相对路径
			File filename = new File(pathname);
			InputStreamReader reader = new InputStreamReader(new FileInputStream(filename));
			BufferedReader br = new BufferedReader(reader);
			File writename = new File("C:\\Users\\Administrator\\Desktop\\output.txt");
			writename.createNewFile();
			BufferedWriter out = new BufferedWriter(new FileWriter(writename));
			String line = "";
			String mess = "";
			String value = "";
			int a = 0;
			int c = 0;
			int b = 2056;
			int y = 0;
			while (line != null) {
				line = br.readLine(); // 一次读入一行数据
				try {
					value = line.substring(11, 21);
					if (value.charAt(4) != ' ') {

						if (value.substring(7, 9).equals("IN")) {
							System.out.println(value);
							if (value.charAt(3) == ' ') {
								a = Integer.parseInt(value.substring(4, 5));
							} else if (value.charAt(2) == ' ') {
								a = Integer.parseInt(value.substring(3, 5));
							} else if (value.charAt(1) == ' ') {
								a = Integer.parseInt(value.substring(2, 5));
							} else if (value.charAt(0) == ' ') {
								a = Integer.parseInt(value.substring(1, 5));
								if (a == b) {
									y = c + a;
									b = 3;
								}
							} else if (value.charAt(0) != ' ') {
								a = Integer.parseInt(value.substring(0, 5));
							}
							c = c + a;
						}
					}
				} catch (Exception e) {
					e.printStackTrace();
				}

			}

			System.out.println(y);
			out.write(mess);
			out.flush(); // 把缓存区内容压入文件
			br.close();
			out.close(); // 最后记得关闭文件

		} catch (Exception e) {
			e.printStackTrace();
		}
		WordExportController wordExportController = new WordExportController();
		wordExportController.main(list, list1, list2);
	}
}
