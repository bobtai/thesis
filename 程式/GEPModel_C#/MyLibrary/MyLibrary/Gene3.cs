using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLibrary
{
    public class Gene3//基因三：決策樹編碼，加減碼策略基因
    {
        string[] indicator = new string[2] { "A", "B" };//屬性節點
        string[] signal = new string[2] { "d", "i" };//終端節點
        int HeadLength = 3;//頭部長度=3
        int RncMin = -10000;//Rnc值域下限：籌碼指標值域
        int RncMax = 10000;//Rnc值域上限：籌碼指標值域
        string[] gene = new string[7];//頭部長度=3，尾部長度=4
        string[] dc = new string[3];//dc長度=3
        int[] rnc = new int[10];//rnc陣列長度=10

        Random rnd = new Random(Guid.NewGuid().GetHashCode());

        public string[] GeneInitial()//基因初始化
        {
            //頭部
            gene[0] = indicator[rnd.Next(0, indicator.Length)];
            for (int i = 1; i < HeadLength; i++)
            {
                if (rnd.Next(1, 11) <= 5)
                    gene[i] = indicator[rnd.Next(0, indicator.Length)];
                else
                    gene[i] = signal[rnd.Next(0, signal.Length)];
            }
            //尾部
            for (int i = HeadLength; i < gene.Length; i++)
            {
                gene[i] = signal[rnd.Next(0, signal.Length)];
            }
            return gene;
        }

        public string[] DcInitial()//Dc初始化
        {
            for (int i = 0; i < dc.Length; i++)
            {
                dc[i] = rnd.Next(0, rnc.Length).ToString();
            }
            return dc;
        }

        public int[] RncInitial()//RNC陣列初始化
        {
            for (int i = 0; i < rnc.Length; i++)
                rnc[i] = rnd.Next(RncMin, RncMax);//買賣超、未平倉量口數
            return rnc;
        }

        public string[] GeneMutation(string[] gene)//基因1點突變
        {
            int posi = rnd.Next(0, gene.Length);
            if (posi == 0)
                gene[posi] = indicator[rnd.Next(0, indicator.Length)];
            else if (posi >= 1 && posi < HeadLength)
            {
                if (rnd.Next(1, 11) <= 5)
                    gene[posi] = indicator[rnd.Next(0, indicator.Length)];
                else
                    gene[posi] = signal[rnd.Next(0, signal.Length)];
            }
            else
            {
                gene[posi] = signal[rnd.Next(0, signal.Length)];
            }
            return gene;
        }

        public string[] GeneInversion(string[] gene)//2位元反轉
        {
            string temp ;
            int posi = rnd.Next(3, gene.Length-1);
            temp = gene[posi];
            gene[posi] = gene[posi + 1];
            gene[posi + 1] = temp;
            return gene;
        }

        public string[] ISTransposition(string[] gene)//1位元插入轉換
        {
            int posi = rnd.Next(2, gene.Length-1);
            gene[1] = gene[posi];
            return gene;
        }

        public string[] RISTransposition(string[] gene)//2位元根插入轉換
        {
            int[] posi = new int[HeadLength - 1];//記錄函數節點的位置
            int a = 0;
            for (int i = 1; i < HeadLength; i++)//找出頭部中函數節點位置
            {
                for (int j = 0; j < indicator.Length; j++)
                {
                    if (gene[i].Equals(indicator[j]))
                    {
                        posi[a] = i;
                        a++;
                    }
                }
            }
            int startposi = posi[rnd.Next(0, a)];
            string[] temp = new string[HeadLength];
            for (int i = 0; i < 2; i++)
                temp[i] = gene[startposi + i];
            for (int i = 2; i < HeadLength; i++)
                temp[i] = gene[i - 2];
            for (int i = 0; i < HeadLength; i++)
                gene[i] = temp[i];
            return gene;
        }

        public string[] OpRecombination(string[] gene1, string[] gene2, int posi, int a)//單點重組
        {
            string[] temp1 = new string[gene.Length];
            string[] temp2 = new string[gene.Length];
            for (int i = 0; i < posi; i++)
            {
                temp1[i] = gene2[i];
                temp2[i] = gene1[i];
            }
            for (int i = posi; i < gene.Length; i++)
            {
                temp1[i] = gene1[i];
                temp2[i] = gene2[i];
            }
            return (a == 1) ? temp1 : temp2;
        }

        public string[] TpRecombination(string[] gene1, string[] gene2, int posi1, int posi2, int a)//雙點重組
        {
            string[] temp1 = new string[gene.Length];
            string[] temp2 = new string[gene.Length];
            for (int i = 0; i < posi1; i++)
            {
                temp1[i] = gene1[i];
                temp2[i] = gene2[i];
            }
            for (int i = posi1; i < posi2; i++)
            {
                temp1[i] = gene2[i];
                temp2[i] = gene1[i];
            }
            for (int i = posi2; i < gene.Length; i++)
            {
                temp1[i] = gene1[i];
                temp2[i] = gene2[i];
            }
            return (a == 1) ? temp1 : temp2;
        }

        public string[] DcMutation(string[] dc)//Dc單點突變
        {
            int posi = rnd.Next(0, dc.Length);//突變點
            int[] num = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int temp = num[0];
            num[0] = num[Convert.ToInt32(dc[posi])];
            num[Convert.ToInt32(dc[posi])] = temp;
            dc[posi] = num[rnd.Next(1, rnc.Length)].ToString();
            return dc;
        }

        public string[] DcInversion(string[] dc)//Dc二位元反轉
        {
            int position = rnd.Next(0, dc.Length - 1);
            string temp = dc[position];
            dc[position] = dc[position + 1];
            dc[position + 1] = temp;
            return dc;
        }

        public int[] RNCMutation(int[] rnc)//RNC陣列1~3點突變
        {
            int[] position = new int[3];
            for (int i = 0; i < 3; i++)
            {
                position[i] = rnd.Next(0, rnc.Length);
                rnc[position[i]] = rnd.Next(RncMin, RncMax);
            }
            return rnc;
        }

        public string[,] Gene3Express(string[] gene3, int[] rnc)
        {
            string[] bintree = new string[7]; //完整二元樹陣列
            for (int i = 0; i < 3; i++)
            {
                bintree[i] = gene3[i];
            }
            int c = 3;
            int t = 3;
            for (int i = 1; i < HeadLength; i++)
            {
                if (gene3[i] == "A" || gene3[i] == "B")
                {
                    for (int j = 0; j < 2; j++)
                    {
                        bintree[t + j] = gene3[c + j];
                    }
                    c += 2;
                    t += 2;
                }
                else
                {
                    for (int j = 0; j < 2; j++)
                    {
                        bintree[t + j] = "o";
                    }
                    t += 2;
                }
            }

            string[,] rule = new string[4, 7];
            int count = 0;

            if (bintree[1] == "A" || bintree[1] == "B")
            {
                rule[count, 0] = bintree[0];
                rule[count, 1] = "<=";
                rule[count, 2] = rnc[Convert.ToInt32(gene3[7])].ToString();
                rule[count, 3] = bintree[1];
                rule[count, 4] = "<=";
                rule[count, 5] = rnc[Convert.ToInt32(gene3[8])].ToString();
                rule[count, 6] = bintree[3];
                count += 1;
                rule[count, 0] = bintree[0];
                rule[count, 1] = "<=";
                rule[count, 2] = rnc[Convert.ToInt32(gene3[7])].ToString();
                rule[count, 3] = bintree[1];
                rule[count, 4] = ">";
                rule[count, 5] = rnc[Convert.ToInt32(gene3[8])].ToString();
                rule[count, 6] = bintree[4];
                count += 1;
            }
            else
            {
                rule[count, 0] = bintree[0];
                rule[count, 1] = "<=";
                rule[count, 2] = rnc[Convert.ToInt32(gene3[7])].ToString();
                rule[count, 3] = bintree[1];
                count += 1;
            }

            if (bintree[2] == "A" || bintree[2] == "B")
            {
                rule[count, 0] = bintree[0];
                rule[count, 1] = ">";
                rule[count, 2] = rnc[Convert.ToInt32(gene3[7])].ToString();
                rule[count, 3] = bintree[2];
                rule[count, 4] = "<=";
                rule[count, 5] = rnc[Convert.ToInt32(gene3[9])].ToString();
                rule[count, 6] = bintree[5];
                count += 1;
                rule[count, 0] = bintree[0];
                rule[count, 1] = ">";
                rule[count, 2] = rnc[Convert.ToInt32(gene3[7])].ToString();
                rule[count, 3] = bintree[2];
                rule[count, 4] = ">";
                rule[count, 5] = rnc[Convert.ToInt32(gene3[9])].ToString();
                rule[count, 6] = bintree[6];
                count += 1;
            }
            else
            {
                rule[count, 0] = bintree[0];
                rule[count, 1] = ">";
                rule[count, 2] = rnc[Convert.ToInt32(gene3[7])].ToString();
                rule[count, 3] = bintree[2];
                count += 1;
            }
            return rule;
        }
    }
}
