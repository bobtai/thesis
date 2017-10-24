#
碩士論文

應用 [機械學習 \(Machine Learning\)](https://zh.wikipedia.org/wiki/机器学习)，

結合 [模糊理論 \(Fuzzy Theory\)](http://wiki.mbalib.com/zh-tw/模糊理论) 和 [基因表達規劃法 \(Gene Expression Programming\)](https://en.wikipedia.org/wiki/Gene_expression_programming)，

找出 [台指期貨\(TX\)](http://www.taifex.com.tw/chinese/2/TX.asp) `買賣時機點`和`交易口數配置`，並加入`加減碼`和`停損停利`等避險機制。

## 程式

程式架構如下：

![alt tag](https://raw.githubusercontent.com/bobtai/thesis/master/程式/architecture.png)

### MyLibrary

使用 C\# .NET 開發`模糊理論 (Fuzzy.cs)`和`基因表達規劃法 (GEP.cs)`演算法，

並編譯成 dll 檔供 MultiCharts .NET 參考。

### MultiCharts

首先，`技術指標.pln`根據資料集求出 KD、RSI 和 BIAS 等指標值，

其後，`投資策略.pln`會參考 MyLibrary.dll 的演算法寫出一個投資策略，

最後根據定義的策略模擬期貨交易，據以訓練和測試模型，

目的是找出一組最賺錢期貨的投資策略。

## 簡報

[模糊基因表達規劃法在台指期貨投資策略探勘之研究\_簡報](https://github.com/bobtai/thesis/blob/master/簡報/模糊基因表達規劃法在台指期貨投資策略探勘之研究.pdf)

## 論文

[模糊基因表達規劃法在台指期貨投資策略探勘之研究\_論文](https://github.com/bobtai/thesis/blob/master/論文/模糊基因表達規劃法在台指期貨投資策略探勘之研究.pdf)

## 資料集

資料的區間為期`七年`，包含`2007/07/01`到`2014/03/31`的台指期交易資料。

期貨交易屬`時間序列 (Time Series)`問題，必須採`移動視窗 (Sliding Window)`方式訓練和測試資料。

`訓練期`為 9 個月，`測試期`為 3 個月，一次移動 3 個月，總共能移動 23 次，共計 24 個訓練和測試區間。

### 台指期\_每日三大法人交易量和未平倉量

欄位由左而右分別為：`日期`、`三大法人交易量`和`未平倉量`。

### 台指期\_每日交易資料

欄位由左而右分別為：`日期`、`開盤價`、`最高價`、`最低價`、`收盤價`和`總交易量`。
