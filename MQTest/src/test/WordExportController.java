package test;

import java.io.File;
import java.io.FileOutputStream;
import java.math.BigInteger;
import java.util.List;

import org.apache.poi.xwpf.model.XWPFHeaderFooterPolicy;
import org.apache.poi.xwpf.usermodel.ParagraphAlignment;
import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.apache.poi.xwpf.usermodel.XWPFParagraph;
import org.apache.poi.xwpf.usermodel.XWPFRun;
import org.apache.poi.xwpf.usermodel.XWPFTable;
import org.apache.poi.xwpf.usermodel.XWPFTableRow;
import org.openxmlformats.schemas.wordprocessingml.x2006.main.CTP;
import org.openxmlformats.schemas.wordprocessingml.x2006.main.CTR;
import org.openxmlformats.schemas.wordprocessingml.x2006.main.CTSectPr;
import org.openxmlformats.schemas.wordprocessingml.x2006.main.CTShd;
import org.openxmlformats.schemas.wordprocessingml.x2006.main.CTTblWidth;
import org.openxmlformats.schemas.wordprocessingml.x2006.main.CTText;
import org.openxmlformats.schemas.wordprocessingml.x2006.main.STShd;
import org.openxmlformats.schemas.wordprocessingml.x2006.main.STTblWidth;

/**
 * Created by zhouhs on 2017/1/9.
 */
public class WordExportController {

	public void main(List<String> list, List<String> list1, List<Integer> list2) throws Exception {
		// Blank Document
		XWPFDocument document = new XWPFDocument();

		// Write the Document in file system
		FileOutputStream out = new FileOutputStream(new File("C:\\Users\\Administrator\\Desktop\\rule.docx"));

		// 添加标题
		XWPFParagraph titleParagraph = document.createParagraph();
		// 设置段落居中
		titleParagraph.setAlignment(ParagraphAlignment.CENTER);

		XWPFRun titleParagraphRun = titleParagraph.createRun();
		titleParagraphRun.setText("java");
		titleParagraphRun.setColor("000000");
		titleParagraphRun.setFontSize(20);

		// 段落
		XWPFParagraph firstParagraph = document.createParagraph();
		XWPFRun run = firstParagraph.createRun();
		run.setText("Java POI生成word表格");
		run.setColor("000000");
		run.setFontSize(16);

		// 设置段落背景颜色
		CTShd cTShd = run.getCTR().addNewRPr().addNewShd();
		cTShd.setVal(STShd.CLEAR);
		cTShd.setFill("97FFFF");

		// 换行
		XWPFParagraph paragraph1 = document.createParagraph();
		XWPFRun paragraphRun1 = paragraph1.createRun();
		paragraphRun1.setText("\r");

		// 基本信息表格
		XWPFTable infoTable = document.createTable();
		// 去表格边�?
		infoTable.getCTTbl().getTblPr().unsetTblBorders();

		// 列宽自动分割
		CTTblWidth infoTableWidth = infoTable.getCTTbl().addNewTblPr().addNewTblW();
		infoTableWidth.setType(STTblWidth.DXA);
		infoTableWidth.setW(BigInteger.valueOf(9072));

		// 两个表格之间加个换行
		XWPFParagraph paragraph = document.createParagraph();
		XWPFRun paragraphRun = paragraph.createRun();
		paragraphRun.setText("\r");

		// 工作经历表格
		XWPFTable ComTable = document.createTable();

		// 列宽自动分割
		CTTblWidth comTableWidth = ComTable.getCTTbl().addNewTblPr().addNewTblW();
		comTableWidth.setType(STTblWidth.DXA);
		comTableWidth.setW(BigInteger.valueOf(9072));

		// 表格第一�?
		XWPFTableRow comTableRowOne = ComTable.getRow(0);
		comTableRowOne.getCell(0).setText("方向");
		comTableRowOne.addNewTableCell().setText("长度");
		comTableRowOne.addNewTableCell().setText("数据");
		for (int i = 0; i < list.size(); i++) {
			XWPFTableRow comTableRowTwo = ComTable.createRow();
			comTableRowTwo.getCell(0).setText(list.get(i));
			comTableRowTwo.getCell(1).setText(String.valueOf(list2.get(i)));
			comTableRowTwo.getCell(2).setText(list1.get(i));
		}

		CTSectPr sectPr = document.getDocument().getBody().addNewSectPr();
		XWPFHeaderFooterPolicy policy = new XWPFHeaderFooterPolicy(document, sectPr);

		// 添加页眉
		CTP ctpHeader = CTP.Factory.newInstance();
		CTR ctrHeader = ctpHeader.addNewR();
		CTText ctHeader = ctrHeader.addNewT();
		String headerText = "Java POI create MS word file.";
		ctHeader.setStringValue(headerText);
		XWPFParagraph headerParagraph = new XWPFParagraph(ctpHeader, document);
		// 设置为右对齐
		headerParagraph.setAlignment(ParagraphAlignment.RIGHT);
		XWPFParagraph[] parsHeader = new XWPFParagraph[1];
		parsHeader[0] = headerParagraph;
		policy.createHeader(XWPFHeaderFooterPolicy.DEFAULT, parsHeader);

		// 添加页脚
		CTP ctpFooter = CTP.Factory.newInstance();
		CTR ctrFooter = ctpFooter.addNewR();
		CTText ctFooter = ctrFooter.addNewT();
		String footerText = "青岛本原微电子有限公司";
		ctFooter.setStringValue(footerText);
		XWPFParagraph footerParagraph = new XWPFParagraph(ctpFooter, document);
		headerParagraph.setAlignment(ParagraphAlignment.CENTER);
		XWPFParagraph[] parsFooter = new XWPFParagraph[1];
		parsFooter[0] = footerParagraph;
		policy.createFooter(XWPFHeaderFooterPolicy.DEFAULT, parsFooter);

		document.write(out);
		out.close();
		System.out.println("create_table document written success.");
	}

}
