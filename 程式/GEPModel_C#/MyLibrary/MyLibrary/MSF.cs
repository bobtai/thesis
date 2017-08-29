using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLibrary
{
    public class MSF
    {
        //RSI
        public double RSI_MSF_L(double x) //RSI低，使用Z型。
        {
            double MSG;
            if (x <= 10) MSG = 1;
            else if (x > 10 & x < 40) MSG = (40 - x) / 30.0;
            else MSG = 0;
            return MSG;
        }

        public double RSI_MSF_M(double x) //RSI中，使用PI型。
        {
            double MSG;
            if (x <= 10) MSG = 0;
            else if (x > 10 & x < 40) MSG = (x - 10) / 30.0;
            else if (x >= 40 & x <= 60) MSG = 1;
            else if (x > 60 & x < 90) MSG = (90 - x) / 30.0;
            else MSG = 0;
            return MSG;
        }

        public double RSI_MSF_H(double x) //RSI高，使用S型。
        {
            double MSG;
            if (x <= 60) MSG = 0;
            else if (x > 60 & x < 90) MSG = (x - 60) / 30.0;
            else MSG = 1;
            return MSG;
        }

        //K
        public double K_MSF_L(double x) //K低，使用Z型。
        {
            double MSG;
            if (x <= 20) MSG = 1;
            else if (x > 20 & x < 40) MSG = (40 - x) / 20.0;
            else MSG = 0;
            return MSG;
        }

        public double K_MSF_M(double x) //K中，使用PI型。
        {
            double MSG;
            if (x <= 20) MSG = 0;
            else if (x > 20 & x < 40) MSG = (x - 20) / 20.0;
            else if (x >= 40 & x <= 60) MSG = 1;
            else if (x > 60 & x < 80) MSG = (80 - x) / 20.0;
            else MSG = 0;
            return MSG;
        }

        public double K_MSF_H(double x) //K高，使用S型。
        {
            double MSG;
            if (x <= 60) MSG = 0;
            else if (x > 60 & x < 80) MSG = (x - 60) / 20.0;
            else MSG = 1;
            return MSG;
        }

        //D
        public double D_MSF_L(double x) //D低，使用Z型。
        {
            double MSG;
            if (x <= 30) MSG = 1;
            else if (x > 30 & x < 40) MSG = (40 - x) / 10.0;
            else MSG = 0;
            return MSG;
        }

        public double D_MSF_M(double x) //D中，使用PI型。
        {
            double MSG;
            if (x <= 30) MSG = 0;
            else if (x > 30 & x < 40) MSG = (x - 30) / 10.0;
            else if (x >= 40 & x <= 60) MSG = 1;
            else if (x > 60 & x < 70) MSG = (70 - x) / 10.0;
            else MSG = 0;
            return MSG;
        }

        public double D_MSF_H(double x) //D高，使用S型。
        {
            double MSG;
            if (x <= 60) MSG = 0;
            else if (x > 60 & x < 70) MSG = (x - 60) / 10.0;
            else MSG = 1;
            return MSG;
        }

        //W%R
        public double WR_MSF_L(double x) //W%R低，使用Z型。
        {
            double MSG;
            if (x <= 20) MSG = 1;
            else if (x > 20 & x < 40) MSG = (40 - x) / 20.0;
            else MSG = 0;
            return MSG;
        }

        public double WR_MSF_M(double x) //W%R中，使用PI型。
        {
            double MSG;
            if (x <= 20) MSG = 0;
            else if (x > 20 & x < 40) MSG = (x - 20) / 20.0;
            else if (x >= 40 & x <= 60) MSG = 1;
            else if (x > 60 & x < 80) MSG = (80 - x) / 20.0;
            else MSG = 0;
            return MSG;
        }

        public double WR_MSF_H(double x) //W%R高，使用S型。
        {
            double MSG;
            if (x <= 60) MSG = 0;
            else if (x > 60 & x < 80) MSG = (x - 60) / 20.0;
            else MSG = 1;
            return MSG;
        }

        //PSY
        public double PSY_MSF_L(double x) //PSY低，使用Z型。
        {
            double MSG;
            if (x <= 10) MSG = 1;
            else if (x > 10 & x < 30) MSG = (30 - x) / 20.0;
            else MSG = 0;
            return MSG;
        }

        public double PSY_MSF_M(double x) //PSY中，使用PI型。
        {
            double MSG;
            if (x <= 10) MSG = 0;
            else if (x > 10 & x < 30) MSG = (x - 10) / 20.0;
            else if (x >= 30 & x <= 70) MSG = 1;
            else if (x > 70 & x < 90) MSG = (90 - x) / 20.0;
            else MSG = 0;
            return MSG;
        }

        public double PSY_MSF_H(double x) //PSY高，使用S型。
        {
            double MSG;
            if (x <= 70) MSG = 0;
            else if (x > 70 & x < 90) MSG = (x - 70) / 20.0;
            else MSG = 1;
            return MSG;
        }

        //BIAS
        public double BIAS_MSF_L(double x) //BIAS低，使用左三角型。
        {
            double MSG;
            if (x <= -15) MSG = 1;
            else if (x > -15 & x < -3) MSG = (x + 3) / -12.0;
            else MSG = 0;
            return MSG;
        }

        public double BIAS_MSF_M(double x) //BIAS中，使用中三角型。
        {
            double MSG;
            if (x <= -9) MSG = 0;
            else if (x > -9 & x < 0) MSG = (-9 - x) / -9.0;
            else if (x > 0 & x < 9) MSG = (9 - x) / 9.0;
            else MSG = 0;
            return MSG;
        }

        public double BIAS_MSF_H(double x) //BIAS高，使用右三角型。
        {
            double MSG;
            if (x <= 3) MSG = 0;
            else if (x > 3 & x < 15) MSG = (x - 3) / 12.0;
            else MSG = 1;
            return MSG;
        }

        //交易訊號，傳入歸屬度，回傳0-100，越低代表需賣出，越高代表需買入
        public double MSF_SELL(double MSG) //賣出，使用Z型。
        {
            double x;
            x = 20 + (1 - MSG) * 20;
            return x;
        }

        public double[] MSF_NOACT(double MSG) //不動作，使用PI型。
        {
            double[] x = new double[2];
            x[0] = 20 + MSG * 20;
            x[1] = 60 + (1 - MSG) * 20;
            return x;
        }

        public double MSF_BUY(double MSG) //買入，使用S型。
        {
            double x;
            x = 60 + MSG * 20;
            return x;
        }
    }
}
