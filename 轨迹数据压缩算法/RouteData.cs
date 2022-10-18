using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 轨迹数据压缩算法
{
    class RouteData
    {

        public RouteData(int i, string m, double x, double y)
        {
            id = i;
            mark = m;
            x_point = x;
            y_point = y;
        }

        private int ID;
        private string Mark;
        private double X_coordinate;
        private double Y_coordinate;

        public string mark
        {
            get { return Mark; }
            set { Mark = value; }
        }
        public double x_point
        {
            get { return X_coordinate; }
            set { X_coordinate = value; }
        }
        public double y_point
        {
            get { return Y_coordinate; }
            set { Y_coordinate = value; }
        }

        public int id
        {
            get { return ID; }
            set { ID = value; }
        }


        public static double[] line(RouteData i, RouteData j)//求直线参数
        {
            double k = (j.y_point - i.y_point) / (j.x_point - i.x_point);
            double A = 1;
            double B = -k;
            double C = k * i.x_point - i.y_point;

            double[] line = { k, A, B, C };
            return line;
        }

        public static double vertical(RouteData i, RouteData j, RouteData l)//计算垂直距离
        {
            double k = (j.y_point - i.y_point) / (j.x_point - i.x_point);
            double A = -k;
            double B = 1;
            double C = k * i.x_point - i.y_point;

            double d = Math.Abs((l.x_point * A + l.y_point * B + C)) / Math.Sqrt(A * A + B * B);
            return d;
        }

        public static RouteData Max_route(List<RouteData> RD)//寻找最大路径点，返回路径点
        {
            List<double> Ver = Creat_list(RD);
            double max = 0.0;
            int j = 0;
            
            for (int i = 0; i < Ver.Count; i++)
            {
                if (Ver[i] > max)
                {
                    max = Ver[i];
                    j = i;
                }
            }

            RouteData Route_max = RD[j];
            return Route_max;
        }

        public static int Max_Dist(List<double> Ver)//寻找最大距离，返回下标
        {
            double max = 0.0;
            int j = 0;

            for (int i = 0; i < Ver.Count; i++)
            {
                if (Ver[i] > max)
                {
                    max = Ver[i];
                    j = i;
                }
            }
            return j;
        }

        public static List<RouteData> DP(double D, List<RouteData> RD)
        {
            List<double> Ver = Creat_list(RD);
            double max = Ver[Max_Dist(Ver)];
            List<RouteData> Routes = new List<RouteData>();
       
            if (max < D)
            {
                Routes.Add(RD[0]);
                Routes.Add(RD[RD.Count - 1]);
            }
            else
            {
                newRD = new List<RouteData>();
                Routes.Add(RD[0]);
                Routes.Add(RD[RD.Count - 1]);
                 DP1(D, RD);
                foreach (RouteData i in newRD)
                {

                    Routes.Add(i);
                }

            }
            return Routes;

        }

         public static List<RouteData> newRD = new List<RouteData>();

        public static void DP1(double D, List<RouteData> RD)
        {
            List<double> Ver = Creat_list(RD);
            double max = Ver[Max_Dist(Ver)];
            List<RouteData> routeDatas1 = new List<RouteData>();
            List<RouteData> routeDatas2 = new List<RouteData>();
            while (max > D)
            {
                //创建数组前列表
                for (int i = 0; i <= Max_Dist(Ver); i++)
                {
                    routeDatas1.Add(RD[i]);
                    
                }

                
                //创建数组后列表
                for (int i = Max_Dist(Ver); i < Ver.Count; i++)
                {
                    routeDatas2.Add(RD[i]);
                    
                }

                newRD.Add(Max_route(RD));
                DP1(D, routeDatas1);
                DP1(D, routeDatas2);
                break;
            }
        }


        public static List<double> Creat_list(List<RouteData> RD_list)
        {
            List<double> Vertical_list = new List<double>();
            for (int i = 0; i < RD_list.Count; i++)
            {
                double dist = RouteData.vertical(RD_list[0], RD_list[RD_list.Count - 1], RD_list[i]);
                Vertical_list.Add(dist);//计算第一轮距离
            }
            return Vertical_list;
        }

        public static void Sort_list(List<RouteData> RD)
        {
            List<double> Sort = new List<double>();
            for (int i = 0; i < RD.Count - 1; i++)
            {
                for (int j = 0; j < RD.Count - 1 - i; j++)
                {
                    if (RD[j].id > RD[j + 1].id)
                    {
                        RouteData newrd  = RD[j + 1];
                        RD[j + 1] = RD[j];
                        RD[j] = newrd;
                    }
                }
            }
        }

        public static string Display(List<RouteData> RD)
        {
            string result = "";
            foreach (RouteData i in RD)
            {
                result += i.mark + " " + i.x_point + " " + i.y_point + "\r\n"; 
            }

            return result;
        }

    }
}
