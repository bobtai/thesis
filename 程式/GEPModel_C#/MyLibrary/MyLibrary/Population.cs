using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLibrary
{
    public class Population
    {
        Gene1 g1 = new Gene1();
        Gene2 g2 = new Gene2();
        Gene3 g3 = new Gene3();
        Gene4 g4 = new Gene4();
        Gene5 g5 = new Gene5();
        Random rnd = new Random(Guid.NewGuid().GetHashCode());

        public string[,] PopuInitial(int popunum)//族群染色體初始化，傳入族群個數
        {
            string[] chrom = new string[39];//染色體長度=39
            string[,] popu = new string[popunum, chrom.Length];
            for (int i = 0; i < popunum; i++)
            {
                g1.GeneInitial().CopyTo(chrom, 0);
                g1.DcInitial().CopyTo(chrom, 13);
                g2.GeneInitial().CopyTo(chrom, 17);
                g3.GeneInitial().CopyTo(chrom, 21);
                g3.DcInitial().CopyTo(chrom, 28);
                g4.GeneInitial().CopyTo(chrom, 31);
                g5.GeneInitial().CopyTo(chrom, 35);
                for (int j = 0; j < chrom.Length; j++)
                    popu[i, j] = chrom[j];
            }
            return popu;
        }

        public int[,] PopuRncInitial(int popunum)//族群RNC初始化，傳入族群個數
        {
            int[] rnc = new int[40];//RNC總長度=40
            int[,] popurnc = new int[popunum, rnc.Length];
            for (int i = 0; i < popunum; i++)
            {
                g1.RncInitial().CopyTo(rnc, 0);
                g2.RncInitial().CopyTo(rnc, 15);
                g3.RncInitial().CopyTo(rnc, 20);
                g4.RncInitial().CopyTo(rnc, 30);
                g5.RncInitial().CopyTo(rnc, 35);
                for (int j = 0; j < rnc.Length; j++)
                    popurnc[i, j] = rnc[j];
            }
            return popurnc;
        }

        public int[] Selection(double[] fit, string[,] popu)//選擇複製，使用菁英法和輪盤法。
        {
            List<int> list = new List<int>();
            int count = 0;
            double maxFit = 0;
            int maxPosi = 0;
            int[] number = new int[fit.Length];
            for (int i = 0; i < popu.GetLength(0); i++)
            {
                if (fit[i] == 0)
                    fit[i] = -20.0;
                if (fit[i] > maxFit)
                {
                    maxFit = fit[i];
                    maxPosi = i;
                }

                count = Convert.ToInt32(fit[i] + 50) / 5;
                for (int j = 0; j < count; j++)
                    list.Add(i);
            }
            number[0] = maxPosi;
            for (int i = 1; i < popu.GetLength(0); i++)
            {
                number[i] = list[rnd.Next(0, list.Count)];//fit儲存複製後的染色體編號
            }
            return number;
        }

        public string[,] PopuEvolution(string[,] popu)//族群演化
        {
            int MutationRate =20;//突變率
            int InversionRate = 10;//反轉率
            int IStransRate = 15;//IS轉換率
            int RIStransRate = 15;//RIS轉換率
            int OpRecomRate = 10;//單點重組率
            int TpRecomRate = 10;//雙點重組率
            int DcMutationRate = 20;//Dc突變率
            int DcInversionRate = 20;//Dc反轉率
            int DcTransRate = 20;//Dc轉換率

            for (int i = 0; i < popu.GetLength(0); i++)//突變、反轉和轉換
            {
                
                string[] temp1 = new string[13];
                string[] temp2 = new string[7];
                //突變
                for (int j = 0; j < temp1.Length; j++)
                    temp1[j] = popu[i, j];
                if (rnd.Next(1, 101) <= MutationRate)
                {
                    temp1 = g1.GeneMutation(temp1);//基因1突變
                    for (int j = 0; j < temp1.Length; j++)
                        popu[i, j] = temp1[j];
                }
                for (int j = 21; j < 21+temp2.Length; j++)
                    temp2[j-21] = popu[i, j];
                if (rnd.Next(1, 101) <= MutationRate)
                {
                    temp2 = g3.GeneMutation(temp2);//基因3突變
                    for (int j = 21; j < 21+temp2.Length; j++)
                        popu[i, j] = temp2[j-21];
                }

                //反轉
                for (int j = 0; j < temp1.Length; j++)
                    temp1[j] = popu[i, j];
                if (rnd.Next(1, 101) <= InversionRate)
                {
                    temp1 = g1.GeneInversion(temp1);//基因1反轉
                    for (int j = 0; j < temp1.Length; j++)
                        popu[i, j] = temp1[j];
                }
                for (int j = 21; j < 21 + temp2.Length; j++)
                    temp2[j - 21] = popu[i, j];
                if (rnd.Next(1, 101) <= InversionRate)
                {
                    temp2 = g3.GeneInversion(temp2);//基因3反轉
                    for (int j = 21; j < 21 + temp2.Length; j++)
                        popu[i, j] = temp2[j - 21];
                }

                //IS轉換
                for (int j = 0; j < temp1.Length; j++)
                    temp1[j] = popu[i, j];
                if (rnd.Next(1, 101) <= IStransRate)
                {
                    temp1 = g1.ISTransposition(temp1);//基因1 IS轉換
                    for (int j = 0; j < temp1.Length; j++)
                        popu[i, j] = temp1[j];
                }
                for (int j = 21; j < 21 + temp2.Length; j++)
                    temp2[j - 21] = popu[i, j];
                if (rnd.Next(1, 101) <= IStransRate)
                {
                    temp2 = g3.ISTransposition(temp2);//基因3 IS轉換
                    for (int j = 21; j < 21 + temp2.Length; j++)
                        popu[i, j] = temp2[j - 21];
                }

                //RIS轉換
                for (int j = 0; j < temp1.Length; j++)
                    temp1[j] = popu[i, j];
                if (rnd.Next(1, 101) <= RIStransRate)
                {
                    temp1 = g1.RISTransposition(temp1);//基因1 RIS轉換
                    for (int j = 0; j < temp1.Length; j++)
                        popu[i, j] = temp1[j];
                }
                for (int j = 21; j < 21 + temp2.Length; j++)
                    temp2[j - 21] = popu[i, j];
                if (rnd.Next(1, 101) <= RIStransRate)
                {
                    temp2 = g3.RISTransposition(temp2);//基因3 RIS轉換
                    for (int j = 21; j < 21 + temp2.Length; j++)
                        popu[i, j] = temp2[j - 21];
                }
                
                string[] dc1 = new string[4];
                string[] dc2 = new string[2];
                string[] dc3 = new string[3];
                string[] dc4 = new string[2];
                string[] dc5 = new string[2];
                //Dc突變
                for (int j = 13; j < 13 + dc1.Length; j++)
                    dc1[j-13] = popu[i, j];
                if (rnd.Next(1, 101) <= DcMutationRate)
                {
                    dc1 = g1.DcMutation(dc1);//基因1 Dc突變
                    for (int j = 13; j < 13 + dc1.Length; j++)
                        popu[i, j] = dc1[j-13];
                }

                for (int j = 19; j < 19 + dc2.Length; j++)
                    dc2[j - 19] = popu[i, j];
                if (rnd.Next(1, 101) <= DcMutationRate)
                {
                    dc2 = g2.DcMutation(dc2);//基因2 Dc突變
                    for (int j = 19; j < 19 + dc2.Length; j++)
                        popu[i, j] = dc2[j - 19];
                }

                for (int j = 28; j < 28 + dc3.Length; j++)
                    dc3[j - 28] = popu[i, j];
                if (rnd.Next(1, 101) <= DcMutationRate)
                {
                    dc3 = g3.DcMutation(dc3);//基因3 Dc突變
                    for (int j = 28; j < 28 + dc3.Length; j++)
                        popu[i, j] = dc3[j - 28];
                }

                for (int j = 33; j < 33 + dc4.Length; j++)
                    dc4[j - 33] = popu[i, j];
                if (rnd.Next(1, 101) <= DcMutationRate)
                {
                    dc4 = g4.DcMutation(dc4);//基因4 Dc突變
                    for (int j = 33; j < 33 + dc4.Length; j++)
                        popu[i, j] = dc4[j - 33];
                }

                for (int j = 37; j < 37 + dc5.Length; j++)
                    dc5[j - 37] = popu[i, j];
                if (rnd.Next(1, 101) <= DcMutationRate)
                {
                    dc5 = g5.DcMutation(dc5);//基因5 Dc突變
                    for (int j = 37; j < 37 + dc5.Length; j++)
                        popu[i, j] = dc5[j - 37];
                }

                //Dc反轉
                for (int j = 13; j < 13 + dc1.Length; j++)
                    dc1[j - 13] = popu[i, j];
                if (rnd.Next(1, 101) <= DcInversionRate)
                {
                    dc1 = g1.DcInversion(dc1);//基因1 Dc反轉
                    for (int j = 13; j < 13 + dc1.Length; j++)
                        popu[i, j] = dc1[j - 13];
                }

                for (int j = 19; j < 19 + dc2.Length; j++)
                    dc2[j - 19] = popu[i, j];
                if (rnd.Next(1, 101) <= DcInversionRate)
                {
                    dc2 = g2.DcInversion(dc2);//基因2 Dc反轉
                    for (int j = 19; j < 19 + dc2.Length; j++)
                        popu[i, j] = dc2[j - 19];
                }

                for (int j = 28; j < 28 + dc3.Length; j++)
                    dc3[j - 28] = popu[i, j];
                if (rnd.Next(1, 101) <= DcInversionRate)
                {
                    dc3 = g3.DcInversion(dc3);//基因3 Dc反轉
                    for (int j = 28; j < 28 + dc3.Length; j++)
                        popu[i, j] = dc3[j - 28];
                }

                for (int j = 33; j < 33 + dc4.Length; j++)
                    dc4[j - 33] = popu[i, j];
                if (rnd.Next(1, 101) <= DcInversionRate)
                {
                    dc4 = g4.DcInversion(dc4);//基因4 Dc反轉
                    for (int j = 33; j < 33 + dc4.Length; j++)
                        popu[i, j] = dc4[j - 33];
                }

                for (int j = 37; j < 37 + dc5.Length; j++)
                    dc5[j - 37] = popu[i, j];
                if (rnd.Next(1, 101) <= DcInversionRate)
                {
                    dc5 = g5.DcInversion(dc5);//基因5 Dc反轉
                    for (int j = 37; j < 37 + dc5.Length; j++)
                        popu[i, j] = dc5[j - 37];
                }

                //Dc轉換
                for (int j = 13; j < 13 + dc1.Length; j++)
                    dc1[j - 13] = popu[i, j];
                if (rnd.Next(1, 101) <= DcTransRate)
                {
                    dc1 = g1.DcTransposition(dc1);//基因1 Dc轉換
                    for (int j = 13; j < 13 + dc1.Length; j++)
                        popu[i, j] = dc1[j - 13];
                }
            }
            
            //基因1單點重組
            if (rnd.Next(1, 101) <= OpRecomRate)
            {
                //取得2個非重複染色體
                int chrom1 = rnd.Next(0, popu.GetLength(0));
                int[] num = new int[popu.GetLength(0)];
                int temp;
                for (int i = 0; i < popu.GetLength(0); i++) num[i] = i;
                temp = num[chrom1];
                num[chrom1] = num[0];
                num[0] = temp;
                int chrom2 = num[rnd.Next(1, popu.GetLength(0))];

                string[] gene1 = new string[13];
                string[] gene2 = new string[13];
                for (int i = 0; i < 13; i++)
                {
                    gene1[i] = popu[chrom1, i];
                    gene2[i] = popu[chrom2, i];
                }

                int recomposi = rnd.Next(1, 12);//重組點
                string[] gene1temp = new string[13];
                string[] gene2temp = new string[13];
                g1.OpRecombination(gene1, gene2, recomposi, 1).CopyTo(gene1temp, 0);
                g1.OpRecombination(gene1, gene2, recomposi, 2).CopyTo(gene2temp, 0);
                for (int i = 0; i < 13; i++)
                {
                    popu[chrom1, i] = gene1temp[i];
                    popu[chrom2, i] = gene2temp[i];
                }
            }

            //基因3單點重組
            if (rnd.Next(1, 101) <= OpRecomRate)
            {
                //取得2個非重複染色體
                int chrom1 = rnd.Next(0, popu.GetLength(0));
                int[] num = new int[popu.GetLength(0)];
                int temp;
                for (int i = 0; i < popu.GetLength(0); i++) num[i] = i;
                temp = num[chrom1];
                num[chrom1] = num[0];
                num[0] = temp;
                int chrom2 = num[rnd.Next(1, popu.GetLength(0))];

                string[] gene1 = new string[7];
                string[] gene2 = new string[7];
                for (int i = 21; i < 28; i++)
                {
                    gene1[i-21] = popu[chrom1, i];
                    gene2[i-21] = popu[chrom2, i];
                }

                int recomposi = rnd.Next(1, 6);//重組點
                string[] gene1temp = new string[7];
                string[] gene2temp = new string[7];
                g3.OpRecombination(gene1, gene2, recomposi, 1).CopyTo(gene1temp, 0);
                g3.OpRecombination(gene1, gene2, recomposi, 2).CopyTo(gene2temp, 0);
                for (int i = 21; i < 28; i++)
                {
                    popu[chrom1, i] = gene1temp[i-21];
                    popu[chrom2, i] = gene2temp[i-21];
                }
            }

            //基因1雙點重組
            if (rnd.Next(1, 101) <= TpRecomRate)
            {
                //取得2個非重複染色體
                int chrom1 = rnd.Next(0, popu.GetLength(0));
                int[] num = new int[popu.GetLength(0)];
                int temp;
                for (int i = 0; i < popu.GetLength(0); i++) num[i] = i;
                temp = num[chrom1];
                num[chrom1] = num[0];
                num[0] = temp;
                int chrom2 = num[rnd.Next(1, popu.GetLength(0))];

                string[] gene1 = new string[13];
                string[] gene2 = new string[13];
                for (int i = 0; i < 13; i++)
                {
                    gene1[i] = popu[chrom1, i];
                    gene2[i] = popu[chrom2, i];
                }
                //取得2個非重複重組點
                int recomposi1 = rnd.Next(1, 12);//重組點1
                int[] num1 = new int[13];
                int tem;
                for (int i = 0; i < 13; i++) num1[i] = i;
                tem = num1[recomposi1];
                num1[recomposi1] = num1[1];
                num1[1] = tem;
                int recomposi2 = num1[rnd.Next(2, 12)];//重組點2
                if (recomposi1 > recomposi2)
                {
                    tem = recomposi1;
                    recomposi1 = recomposi2;
                    recomposi2 = tem;
                }

                string[] gene1temp = new string[13];
                string[] gene2temp = new string[13];
                g1.TpRecombination(gene1, gene2, recomposi1, recomposi2, 1).CopyTo(gene1temp, 0);
                g1.TpRecombination(gene1, gene2, recomposi1, recomposi2, 2).CopyTo(gene2temp, 0);
                for (int i = 0; i < 13; i++)
                {
                    popu[chrom1, i] = gene1temp[i];
                    popu[chrom2, i] = gene2temp[i];
                }
            }

            //基因3雙點重組
            if (rnd.Next(1, 101) <= TpRecomRate)
            {
                //取得2個非重複染色體
                int chrom1 = rnd.Next(0, popu.GetLength(0));
                int[] num = new int[popu.GetLength(0)];
                int temp;
                for (int i = 0; i < popu.GetLength(0); i++) num[i] = i;
                temp = num[chrom1];
                num[chrom1] = num[0];
                num[0] = temp;
                int chrom2 = num[rnd.Next(1, popu.GetLength(0))];

                string[] gene1 = new string[7];
                string[] gene2 = new string[7];
                for (int i = 21; i < 28; i++)
                {
                    gene1[i-21] = popu[chrom1, i];
                    gene2[i-21] = popu[chrom2, i];
                }
                //取得2個非重複重組點
                int recomposi1 = rnd.Next(1, 6);//重組點1
                int[] num1 = new int[7];
                int tem;
                for (int i = 0; i < 7; i++) num1[i] = i;
                tem = num1[recomposi1];
                num1[recomposi1] = num1[1];
                num1[1] = tem;
                int recomposi2 = num1[rnd.Next(2, 6)];//重組點2
                if (recomposi1 > recomposi2)
                {
                    tem = recomposi1;
                    recomposi1 = recomposi2;
                    recomposi2 = tem;
                }

                string[] gene1temp = new string[7];
                string[] gene2temp = new string[7];
                g3.TpRecombination(gene1, gene2, recomposi1, recomposi2, 1).CopyTo(gene1temp, 0);
                g3.TpRecombination(gene1, gene2, recomposi1, recomposi2, 2).CopyTo(gene2temp, 0);
                for (int i = 21; i < 28; i++)
                {
                    popu[chrom1, i] = gene1temp[i-21];
                    popu[chrom2, i] = gene2temp[i-21];
                }
            }
            return popu;
        }
        public int[,] RncEvolution(int[,] rncpopu)//Rnc演化
        {
            int RNCMutationRate = 25;//Rnc突變率

            for (int i = 0; i < rncpopu.GetLength(0); i++)//Rnc突變
            {
                int[] rnc1 = new int[15];
                int[] rnc2 = new int[5];
                int[] rnc3 = new int[10];
                int[] rnc4 = new int[5];
                int[] rnc5 = new int[5];

                for (int j = 0; j < rnc1.Length; j++)
                    rnc1[j] = rncpopu[i, j];
                if (rnd.Next(1, 101) <= RNCMutationRate)
                {
                    rnc1 = g1.RNCMutation(rnc1);//基因1 Rnc突變
                    for (int j = 0; j < rnc1.Length; j++)
                        rncpopu[i, j] = rnc1[j];
                }

                for (int j = 15; j < 15 + rnc2.Length; j++)
                    rnc2[j-15] = rncpopu[i, j];
                if (rnd.Next(1, 101) <= RNCMutationRate)
                {
                    rnc2 = g2.RNCMutation(rnc2);//基因2 Rnc突變
                    for (int j = 15; j < 15 + rnc2.Length; j++)
                        rncpopu[i, j] = rnc2[j-15];
                }

                for (int j = 20; j < 20 + rnc3.Length; j++)
                    rnc3[j - 20] = rncpopu[i, j];
                if (rnd.Next(1, 101) <= RNCMutationRate)
                {
                    rnc3 = g3.RNCMutation(rnc3);//基因3 Rnc突變
                    for (int j = 20; j < 20 + rnc3.Length; j++)
                        rncpopu[i, j] = rnc3[j - 20];
                }

                for (int j = 30; j < 30 + rnc4.Length; j++)
                    rnc4[j - 30] = rncpopu[i, j];
                if (rnd.Next(1, 101) <= RNCMutationRate)
                {
                    rnc4 = g4.RNCMutation(rnc4);//基因4 Rnc突變
                    for (int j = 30; j < 30 + rnc4.Length; j++)
                        rncpopu[i, j] = rnc4[j - 30];
                }

                for (int j = 35; j < 35 + rnc5.Length; j++)
                    rnc5[j - 35] = rncpopu[i, j];
                if (rnd.Next(1, 101) <= RNCMutationRate)
                {
                    rnc5 = g5.RNCMutation(rnc5);//基因5 Rnc突變
                    for (int j = 35; j < 35 + rnc5.Length; j++)
                        rncpopu[i, j] = rnc5[j - 35];
                }
            }
            return rncpopu;
        }
    }
}
