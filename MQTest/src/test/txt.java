package test;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

public class txt {
	public static void main(String args[]) throws Exception {
		List<String> list = new ArrayList<>();
		List<String> list1 = new ArrayList<>();
		List<Integer> list2 = new ArrayList<>();
		try {

			/* 读入TXT文件 */
			String pathname = "C:\\Users\\Administrator\\Desktop\\xz.txt"; // 绝对路径或相对路径都可以，这里是绝对路径，写入文件时演示相对路径
			File filename = new File(pathname);
			InputStreamReader reader = new InputStreamReader(new FileInputStream(filename));
			BufferedReader br = new BufferedReader(reader);
			File writename = new File("C:\\Users\\Administrator\\Desktop\\output.txt");
			writename.createNewFile();
			BufferedWriter out = new BufferedWriter(new FileWriter(writename));
			String line = "";
			String mess = "";
			String value = "";
			String next = "";
			String next2 = "";
			String next3 = "";
			String IO = "";
			int a;
			while (line != null) {
				line = br.readLine(); // 一次读入一行数据
				try {

					line = line.substring(12, line.length());
					if (line.charAt(0) == ' ') {
						line = line.substring(1, line.length());
					}
					if (line.charAt(0) == ' ') {
						line = line.substring(1, line.length());
					}
					if (line.charAt(1) == ' ') {
						a = Integer.parseInt(line.substring(0, 1));
						IO = line.substring(3, 7);
						if (IO.charAt(0) == ' ') {
							IO = IO.substring(1, IO.length());
						}
						if (IO.charAt(IO.length() - 1) == ' ') {
							IO = IO.substring(0, IO.length() - 1);
						}
						value = line.substring(10, 10 + a * 3);
					} else if (line.charAt(2) != ' ') {
						a = Integer.parseInt(line.substring(0, 3));
						System.out.println(a);
						IO = line.substring(5, 8);
						if (IO.charAt(IO.length() - 1) == ' ') {
							IO = IO.substring(0, IO.length() - 1);
						}
						System.out.println(IO);
						int c = a / 32;
						System.out.println(c);
						value = line.substring(12, 12 + 32 * 3 + 6);
						for (int i = 0; i < c - 1; i++) {
							next = br.readLine();
							value = value + "  " + next.substring(25, 25 + 32 * 3 + 6);
						}
						if (a > 32) {
							if (a % 32 > 0) {
								next = br.readLine();
								value = value + "  " + next.substring(25, 25 + (a - 32 * c) * 3 + (a - 32 * c) / 4 - 2);
							}
						}

					} else {
						a = Integer.parseInt(line.substring(0, 2));
						IO = line.substring(4, 7);
						if (IO.charAt(IO.length() - 1) == ' ') {
							IO = IO.substring(0, IO.length() - 1);
							int c = a / 32;
							value = line.substring(12, 12 + 32 * 3 + 6);
							for (int i = 0; i < c; i++) {
								next = br.readLine();
								value = value + "  " + next.substring(25, 25 + 32 * 3 + 6);
							}
							if (a > 32) {
								if (a % 32 > 0) {
									next = br.readLine();
									value = value + "  "
											+ next.substring(25, 25 + (a - 32 * c) * 3 + (a - 32 * c) / 4 - 2);
								}
							}
						}
						if (a > 32) {
							if (a > 96) {
								value = line.substring(11, 11 + 32 * 3 + 6);
								next = br.readLine();
								next2 = br.readLine();
								next3 = br.readLine();
								value = value + "  " + next.substring(25, 25 + 32 * 3 + 6) + "  "
										+ next2.substring(25, 25 + 32 * 3 + 6) + "  "
										+ next3.substring(25, 25 + (a - 96) * 3 + (a - 96) / 4 - 2);
							} else if (a > 64) {
								value = line.substring(11, 11 + 32 * 3 + 6);
								next = br.readLine();
								next2 = br.readLine();
								value = value + "  " + next.substring(25, 25 + 32 * 3 + 6) + "  "
										+ next2.substring(25, 25 + (a - 64) * 3 + (a - 64) / 4 - 2);
							} else {
								value = line.substring(11, 11 + 32 * 3 + 6);
								next = br.readLine();
								value = value + "  " + next.substring(25, 25 + (a - 32) * 3 + (a - 32) / 4 - 2);
							}
						} else {
							if (a >= 10) {
								value = line.substring(11, 11 + a * 3 + a / 4 - 2);
							} else {
								value = line.substring(10, 10 + a * 3 + a / 4 - 2);
							}
						}
						mess = mess + line + "\r\n";
					}
					list.add(IO);
					list1.add(value);
					list2.add(a);

				} catch (Exception e) {
					e.printStackTrace();
				}

			}
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