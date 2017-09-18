using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLibrary
{
    public class Gene2//基因二：MGF編碼，資金配置策略基因
    {
        string[] mgf = new string[4];//基因長度=4，分別代表買進和賣出期貨口數
        int[] rnc = new int[5];//rnc陣列長度=5
        int rncMin = 5;//Rnc值域下限：期貨口數
        int rncMax = 11;//Rnc值域上限：期貨口數

        Random rnd = new Random(Guid.NewGuid().GetHashCode());

        public string[] GeneInitial()//Dc初始化
        {
            for (int i = 0; i < 2; i++)
            {
                mgf[i] = "?";
            }
            for (int i = 2; i < 4; i++)
            {
                mgf[i] = rnd.Next(0, rnc.Length).ToString();
            }
            return mgf;
        }

        public int[] RncInitial()//RNC陣列初始化
        {
            for (int i = 0; i < 5; i++)
                rnc[i] = rnd.Next(rncMin, rncMax+1);
            return rnc;
        }

        public string[] DcMutation(string[] dc)//Dc單點突變
        {
            int posi = rnd.Next(0, 2);//突變點
            int[] num = { 0, 1, 2, 3, 4 };
            int temp = num[0];
            num[0] = num[Convert.ToInt32(dc[posi])];
            num[Convert.ToInt32(dc[posi])] = temp;
            dc[posi] = num[rnd.Next(1, 5)].ToString();
            return dc;
        }

        public string[] DcInversion(string[] dc)//Dc二位元反轉
        {
            string temp = dc[0];
            dc[0] = dc[1];
            dc[1] = temp;
            return dc;
        }

        public int[] RNCMutation(int[] rnc)//RNC陣列一~三點突變
        {
            int[] position = new int[3];
            for (int i = 0; i < 3; i++)
            {
                position[i] = rnd.Next(0, 5);
                rnc[position[i]] = rnd.Next(rncMin, rncMax + 1);
            }
            return rnc;
        }

        public string[] Gene2Express(string[] gene, int[] rnc)
        {
            string[] rule = new string[2];

            for (int i = 0; i < gene.Length; i++)
                rule[i] = rnc[Convert.ToInt32(gene[i])].ToString();

            return rule;
        }
    }
}
