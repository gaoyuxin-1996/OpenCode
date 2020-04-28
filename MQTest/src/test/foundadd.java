package test;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

public class foundadd {
	public static void main(String[] args) throws Exception {
		List<String> list = new ArrayList<>();
		List<String> list1 = new ArrayList<>();
		List<Integer> list2 = new ArrayList<>();
		try {

			/* 读入TXT文件 */
			String pathname = "C:\\Users\\Administrator\\Desktop\\172_ALL\\172_01.txt"; // 绝对路径或相对路径都可以，这里是绝对路径，写入文件时演示相对路径
			File filename = new File(pathname);
			InputStreamReader reader = new InputStreamReader(new FileInputStream(filename));
			BufferedReader br = new BufferedReader(reader);
			String line = "";
			String mess = "";
			String value = "";
			String o1 = null;
			String o2 = "start";
			int a = 0;
			int c = 0;
			int b = 17;
			int i = 1;
			String fir = null;
			while (line != null) {
				line = br.readLine(); // 一次读入一行数据
				try {
					value = line.substring(11, 157);
					if (value.charAt(4) != ' ') {

						if (value.substring(7, 9).equals("IN")) {
							if (value.charAt(3) == ' ') {
								a = Integer.parseInt(value.substring(4, 5));
							} else if (value.charAt(2) == ' ') {
								a = Integer.parseInt(value.substring(3, 5));
								if (a > b) {
									System.err.println(a);
								}
								if (a > b) {
									fir = fir + value.substring(14, 117);
									for (int f = 0; f < a / 32; f++) {
										line = br.readLine();
										value = line.substring(11, 157);
										fir = fir + value.substring(14, 117);
									}
									fir = fir.replace("null", "");
									for (int k = 0; k < fir.replace(" ", "").length() / 2 - 1; k++) {

										o1 = o1 + fir.replace(" ", "").substring(k * 2, k * 2 + 2) + " ";

									}
									o1 = o1 + fir.replace(" ", "").substring(fir.replace(" ", "").length() - 1 * 2,
											fir.replace(" ", "").length() - 1 * 2 + 2);
									File writename = new File(
											"C:\\Users\\Administrator\\Desktop\\172_ALL\\06\\" + i + "_" + a + ".txt");
									writename.createNewFile();
									BufferedWriter out = new BufferedWriter(new FileWriter(writename));
									out.write(o1.replace("null", ""));
									out.flush(); // 把缓存区内容压入文件
									out.close(); // 最后记得关闭文件
									i = i + 1;
								}

								o1 = null;

								o2 = fir;
								fir = null;
							} else if (value.charAt(1) == ' ') {
								a = Integer.parseInt(value.substring(2, 5));
								if (a == b) {
									fir = fir + value.substring(14, 117);
									System.out.println(fir);
									for (int f = 0; f < 5; f++) {

										line = br.readLine();
										value = line.substring(11, 157);
										fir = fir + value.substring(14, 117);
									}
									fir = fir.replace("null", "");
									for (int k = 0; k < fir.replace(" ", "").length() / 2; k++) {

										o1 = o1 + fir.replace(" ", "").substring(k * 2, k * 2 + 2) + " ";

									}
									File writename = new File(
											"C:\\Users\\Administrator\\Desktop\\172_ALL\\04\\172_" + i + ".txt");
									writename.createNewFile();
									BufferedWriter out = new BufferedWriter(new FileWriter(writename));
									out.write(o1.replace("null", ""));
									out.flush(); // 把缓存区内容压入文件
									out.close(); // 最后记得关闭文件
									i = i + 1;
								}

								o1 = null;

								o2 = fir;
								fir = null;
							} else if (value.charAt(0) == ' ') {
								a = Integer.parseInt(value.substring(1, 5));
								// if (a == b) {
								// fir = fir + value.substring(14, 117);
								// for (int f = 0; f < 64; f++) {
								//
								// line = br.readLine();
								// value = line.substring(11, 157);
								// fir = fir + value.substring(14, 117);
								// }
								// fir = fir.replace("null", "");
								//
								// for (int k = 0; k < 2056; k++) {
								//
								// o1 = o1 + fir.replace(" ", "").substring(k * 2, k * 2 + 2) + "\r\n";
								//
								// }
								// File writename = new File(
								// "C:\\Users\\Administrator\\Desktop\\mess\\output" + i + ".txt");
								// writename.createNewFile();
								// BufferedWriter out = new BufferedWriter(new FileWriter(writename));
								// out.write(o1.replace("null", ""));
								// out.flush(); // 把缓存区内容压入文件
								// out.close(); // 最后记得关闭文件
								// }
								// i = i + 1;
								//
								// o1 = null;
								//
								// o2 = fir;
								// fir = null;
							} else if (value.charAt(0) != ' ') {
								a = Integer.parseInt(value.substring(0, 5));
								// if (a == b) {
								// fir = fir + value.substring(14, 117);
								// for (int f = 0; f < 682; f++) {
								//
								// line = br.readLine();
								// value = line.substring(11, 157);
								// fir = fir + value.substring(14, 117);
								// }
								// fir = fir.replace("null", "");
								// fir = fir.replaceAll(" 00 00 00 00", "");
								// for (int k = 0; k < fir.replace(" ", "").length() / 2; k++) {
								//
								// o1 = o1 + fir.replace(" ", "").substring(k * 2, k * 2 + 2) + "\r\n";
								//
								// }
								// File writename = new File(
								// "C:\\Users\\Administrator\\Desktop\\mess1\\21848_" + i + ".txt");
								// writename.createNewFile();
								// BufferedWriter out = new BufferedWriter(new FileWriter(writename));
								// out.write(o1.replace("null", ""));
								// out.flush(); // 把缓存区内容压入文件
								// out.close(); // 最后记得关闭文件
								// i = i + 1;
								// }
								//
								// o1 = null;
								//
								// o2 = fir;
								// fir = null;
							}
							c = c + a;
						}
					}
				} catch (Exception e) {
					e.printStackTrace();
				}

			}

			br.close();

		} catch (

		Exception e) {
			e.printStackTrace();
		}
		WordExportController wordExportController = new WordExportController();
		wordExportController.main(list, list1, list2);
	}
}
