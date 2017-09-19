# 碩士論文

應用 [機械學習(Machine Learning)](https://zh.wikipedia.org/wiki/%E6%9C%BA%E5%99%A8%E5%AD%A6%E4%B9%A0)，

結合 [模糊理論(Fuzzy Theory)](http://wiki.mbalib.com/zh-tw/%E6%A8%A1%E7%B3%8A%E7%90%86%E8%AE%BA) 和 [基因表達規劃法(Gene Expression Programming)](https://en.wikipedia.org/wiki/Gene_expression_programming)，

找出 [台指期貨(TX)](http://www.taifex.com.tw/chinese/2/TX.asp) 買賣時機點和交易口數配置，並加入加減碼和停損停利等避險機制。

## 程式

程式架構圖如下：

![alt tag](https://raw.githubusercontent.com/bobtai/thesis/master/%E7%A8%8B%E5%BC%8F/%E7%A8%8B%E5%BC%8F%E6%9E%B6%E6%A7%8B%E5%9C%96.png)

### MyLibrary

使用 C# .NET 開發模糊理論(Fuzzy.cs)和基因表達規劃法(GEP.cs)演算法，並編譯成 dll 檔供 MultiCharts .NET 參考。

### MultiCharts

參考 MyLibrary.dll 撰寫投資策略，並導入歷史資料進行訓練和測試，找出一組最佳的投資策略。

## 簡報

[模糊基因表達規劃法在台指期貨投資策略探勘之研究_簡報](https://github.com/bobtai/thesis/blob/master/%E7%B0%A1%E5%A0%B1/%E6%A8%A1%E7%B3%8A%E5%9F%BA%E5%9B%A0%E8%A1%A8%E9%81%94%E8%A6%8F%E5%8A%83%E6%B3%95%E5%9C%A8%E5%8F%B0%E6%8C%87%E6%9C%9F%E8%B2%A8%E6%8A%95%E8%B3%87%E7%AD%96%E7%95%A5%E6%8E%A2%E5%8B%98%E4%B9%8B%E7%A0%94%E7%A9%B6.pdf)

## 論文

[模糊基因表達規劃法在台指期貨投資策略探勘之研究_論文](https://github.com/bobtai/thesis/blob/master/%E8%AB%96%E6%96%87/%E6%A8%A1%E7%B3%8A%E5%9F%BA%E5%9B%A0%E8%A1%A8%E9%81%94%E8%A6%8F%E5%8A%83%E6%B3%95%E5%9C%A8%E5%8F%B0%E6%8C%87%E6%9C%9F%E8%B2%A8%E6%8A%95%E8%B3%87%E7%AD%96%E7%95%A5%E6%8E%A2%E5%8B%98%E4%B9%8B%E7%A0%94%E7%A9%B6.pdf)

## 資料集

資料的區間為期七年，包含 2007/07/01 到 2014/03/31 的每日台指期交易資料。

期貨交易屬時間序列(Time Series)問題，採移動視窗(Sliding Window)方式訓練和測試資料。

訓練期為 9 個月，測試期為 3 個月，一次移動 3 個月，總共能移動 23 次，共計 24 個訓練和測試區間。

### 台指期_每日三大法人交易量和未平倉量

欄位由左而右分別為：日期、三大法人交易量和未平倉量。

### 台指期_每日交易資料

欄位由左而右分別為：日期、開盤價、最高價、最低價、收盤價和總交易量(口數)。


