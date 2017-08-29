using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLibrary
{
    public class Gene1//基因一：決策樹編碼，進出場策略基因
    {
        string[] indicator = new string[6] { "R", "K", "D", "W", "B", "P" };//屬性節點
        string[] signal = new string[3] { "s", "n", "b" };//終端節點
        int HeadLength = 4;//頭部長度=4
        int TailLength = 9;//尾部長度=9
        int RncMin = 3;//Rnc值域下限：天期
        int RncMax = 21;//Rnc值域上限：天期
        string[] gene = new string[13];//基因長度=13
        string[] dc = new string[4];//Dc長度=4
        int[] rnc = new int[15];//Rnc長度=15

        Random rnd = new Random(Guid.NewGuid().GetHashCode());

        public string[] GeneInitial()//基因初始化
        {
            //頭部
            gene[0] = indicator[rnd.Next(0, indicator.Length)];
            for (int i = 1; i < HeadLength; i++)
            {
                if(rnd.Next(1, 11) <= 5)
                    gene[i] = indicator[rnd.Next(0, indicator.Length)];
                else
                    gene[i] = signal[rnd.Next(0, signal.Length)];
            }
            //尾部
            for (int i = HeadLength; i < (HeadLength + TailLength); i++)
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
                rnc[i] = rnd.Next(RncMin, RncMax);//技術指標天期值域為1-20分鐘
            return rnc;
        }

        public string[] GeneMutation(string[] gene)//基因1-3點突變
        {
            for (int i = 0; i < 3; i++)
            {
                int posi = rnd.Next(0, gene.Length);
                if(posi == 0)
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
            }
            return gene;
        }

        public string[] GeneInversion(string[] gene)//4位元反轉
        {
            string[] temp = new string[4];
            int posi = rnd.Next(4, gene.Length - 3);
            for (int i = 0; i < 4; i++)
                temp[i] = gene[posi + (3 - i)];
            for (int i = 0; i < 4; i++)
                gene[posi + i] = temp[i];
            return gene;
        }

        public string[] ISTransposition(string[] gene)//2位元插入轉換
        {
            int posi = rnd.Next(1, gene.Length - 1);
            int insposi = rnd.Next(1, HeadLength-1);
            string[] temp = new string[HeadLength];
            for (int i = 0; i < insposi; i++)
                temp[i] = gene[i];
            for (int i = 0; i < 2; i++)
                temp[insposi + i] = gene[posi + i];
            for (int i = insposi + 2; i < HeadLength; i++)
                temp[i] = gene[insposi++];
            for (int i = 0; i < HeadLength; i++)
                gene[i] = temp[i];
            return gene;
        }

        public string[] RISTransposition(string[] gene)//3位元根插入轉換
        {
            int[] posi = new int[HeadLength-1];//記錄函數節點的位置
            int a = 0;
            for (int i = 1; i < HeadLength; i++)//找出頭部中函數節點位置
            {
                for (int j = 0; j < indicator.Length; j++)
                {
                    if (gene[i] == indicator[j])
                    {
                        posi[a] = i;
                        a++;
                    }
                }
            }
            int startposi = posi[rnd.Next(0, a)];
            string[] temp = new string[HeadLength];
            for (int i = 0; i < 3; i++)
                temp[i] = gene[startposi + i];
            for (int i = 3; i < HeadLength; i++)
                temp[i] = gene[i - 3];
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
            int[] num = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            int temp = num[0];
            num[0] = num[Convert.ToInt32(dc[posi])];
            num[Convert.ToInt32(dc[posi])] = temp;
            dc[posi] = num[rnd.Next(1, rnc.Length)].ToString();
            return dc;
        }

        public string[] DcInversion(string[] dc)//Dc三位元反轉
        {
            int position = rnd.Next(0, dc.Length - 2);
            string temp = dc[position];
            dc[position] = dc[position + 2];
            dc[position + 2] = temp;
            return dc;
        }

        public string[] DcTransposition(string[] dc)//2位元插入轉換
        {
            int posi = rnd.Next(0, dc.Length - 1);
            int[] num = { 0, 1, 2, 3, 4 };
            int temp = num[0];
            num[0] = num[posi];
            num[posi] = temp;
            int inserposi = rnd.Next(1, dc.Length - 1);
            string[] tem = new string[dc.Length];
            for (int i = 0; i < inserposi; i++)
                tem[i] = dc[i];
            for (int i = inserposi; i < inserposi + 2; i++)
                tem[i] = dc[posi++];
            for (int i = inserposi + 2; i < dc.Length; i++)
                tem[i] = dc[++inserposi];
            return tem;
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

        public string[,] Gene1Express(string[] gene1, int[] rnc)//基因表示
        {
            string[] tritree = new string[gene.Length]; //完整三元樹陣列
            for (int i = 0; i < 4; i++)
            {
                tritree[i] = gene1[i];
            }
            int c = 4;
            int t = 4;
            for (int i = 1; i < HeadLength; i++)
            {
                if (gene1[i] == "R" || gene1[i] == "K" || gene1[i] == "D" || gene1[i] == "W" || gene1[i] == "B")
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tritree[t + j] = gene1[c + j];
                    }
                    c += 3;
                    t += 3;
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tritree[t + j] = "o";
                    }
                    t += 3;
                }
            }

            string[,] rule = new string[9, 7];//規則最多9條，規則長度最長=7
            int count = 0;
            if (tritree[1] == "R" || tritree[1] == "K" || tritree[1] == "D" || tritree[1] == "W" || tritree[1] == "B")
            {
                rule[count, 0] = rnc[Convert.ToInt32(gene1[13])].ToString();
                rule[count, 1] = tritree[0];
                rule[count, 2] = "L";
                rule[count, 3] = rnc[Convert.ToInt32(gene1[14])].ToString();
                rule[count, 4] = tritree[1];
                rule[count, 5] = "L";
                rule[count, 6] = tritree[4];
                count += 1;
                rule[count, 0] = rnc[Convert.ToInt32(gene1[13])].ToString();
                rule[count, 1] = tritree[0];
                rule[count, 2] = "L";
                rule[count, 3] = rnc[Convert.ToInt32(gene1[14])].ToString();
                rule[count, 4] = tritree[1];
                rule[count, 5] = "M";
                rule[count, 6] = tritree[5];
                count += 1;
                rule[count, 0] = rnc[Convert.ToInt32(gene1[13])].ToString();
                rule[count, 1] = tritree[0];
                rule[count, 2] = "L";
                rule[count, 3] = rnc[Convert.ToInt32(gene1[14])].ToString();
                rule[count, 4] = tritree[1];
                rule[count, 5] = "H";
                rule[count, 6] = tritree[6];
                count += 1;
            }
            else
            {
                rule[count, 0] = rnc[Convert.ToInt32(gene1[13])].ToString();
                rule[count, 1] = tritree[0];
                rule[count, 2] = "L";
                rule[count, 4] = tritree[1];
                count += 1;
            }

            if (tritree[2] == "R" || tritree[2] == "K" || tritree[2] == "D" || tritree[2] == "W" || tritree[2] == "B")
            {
                rule[count, 0] = rnc[Convert.ToInt32(gene1[13])].ToString();
                rule[count, 1] = tritree[0];
                rule[count, 2] = "M";
                rule[count, 3] = rnc[Convert.ToInt32(gene1[15])].ToString();
                rule[count, 4] = tritree[2];
                rule[count, 5] = "L";
                rule[count, 6] = tritree[7];
                count += 1;
                rule[count, 0] = rnc[Convert.ToInt32(gene1[13])].ToString();
                rule[count, 1] = tritree[0];
                rule[count, 2] = "M";
                rule[count, 3] = rnc[Convert.ToInt32(gene1[15])].ToString();
                rule[count, 4] = tritree[2];
                rule[count, 5] = "M";
                rule[count, 6] = tritree[8];
                count += 1;
                rule[count, 0] = rnc[Convert.ToInt32(gene1[13])].ToString();
                rule[count, 1] = tritree[0];
                rule[count, 2] = "M";
                rule[count, 3] = rnc[Convert.ToInt32(gene1[15])].ToString();
                rule[count, 4] = tritree[2];
                rule[count, 5] = "H";
                rule[count, 6] = tritree[9];
                count += 1;
            }
            else
            {
                rule[count, 0] = rnc[Convert.ToInt32(gene1[13])].ToString();
                rule[count, 1] = tritree[0];
                rule[count, 2] = "M";
                rule[count, 3] = tritree[2];
                count += 1;
            }

            if (tritree[3] == "R" || tritree[3] == "K" || tritree[3] == "D" || tritree[3] == "W" || tritree[3] == "B")
            {
                rule[count, 0] = rnc[Convert.ToInt32(gene1[13])].ToString();
                rule[count, 1] = tritree[0];
                rule[count, 2] = "H";
                rule[count, 3] = rnc[Convert.ToInt32(gene1[16])].ToString();
                rule[count, 4] = tritree[3];
                rule[count, 5] = "L";
                rule[count, 6] = tritree[10];
                count += 1;
                rule[count, 0] = rnc[Convert.ToInt32(gene1[13])].ToString();
                rule[count, 1] = tritree[0];
                rule[count, 2] = "H";
                rule[count, 3] = rnc[Convert.ToInt32(gene1[16])].ToString();
                rule[count, 4] = tritree[3];
                rule[count, 5] = "M";
                rule[count, 6] = tritree[11];
                count += 1;
                rule[count, 0] = rnc[Convert.ToInt32(gene1[13])].ToString();
                rule[count, 1] = tritree[0];
                rule[count, 2] = "H";
                rule[count, 3] = rnc[Convert.ToInt32(gene1[16])].ToString();
                rule[count, 4] = tritree[3];
                rule[count, 5] = "H";
                rule[count, 6] = tritree[12];
                count += 1;
            }
            else
            {
                rule[count, 0] = rnc[Convert.ToInt32(gene1[13])].ToString();
                rule[count, 1] = tritree[0];
                rule[count, 2] = "H";
                rule[count, 3] = tritree[3];
                count += 1;
            }
            return rule;
        }
    }
}
