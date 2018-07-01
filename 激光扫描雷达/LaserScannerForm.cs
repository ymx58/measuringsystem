using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms.DataVisualization.Charting;

namespace 激光扫描雷达
{

        #region 窗体加载及关闭事件
    public partial class LaserScannerForm : Form
    {
        #region 初始化
        //从串口中读取数据的委托
        public delegate void _SafeAddtrTextCall(string text);
        // 把信息显示在textbox上并计入日志的委托
        private delegate void ShowMSgDelegate(string msg);
        // 把异常信息显示在textbox上并计入日志的委托
        private delegate void ShowExceptionMSgDelegate(Exception msg);
        // 互斥元，保证些日志的时候，日志文件共享读写不出错
        Mutex mu = new Mutex(false);
        // 互斥元，保证写异常日志的时候，日志文件共享读写不出错
        Mutex muExceptionLog = new Mutex(false);
        List<int> Measurelist = new List<int>();
        List<int> Measurelist1 = new List<int>();

        Thread th;
        Socket socketSend;

        List<int> Mirror6p2xy = new List<int>(); //用于存放单帧距离校准的集合
        //List<double> Anglep2 = new List<double>();//用于存放角度的集合
        //List<int> Anglep2xy = new List<int>(); //用于存放单帧角度的集合

        List<int> showxleandirectionlist = new List<int>();//用于存放单帧x方向距离的集合
        List<int> showydirectionlist = new List<int>();//用于存放单帧y方向距离的集合
        List<int> show_ydirectionlist = new List<int>();
        bool messbox1 = true;

        int carlength, carbackedge, carfrontedge, lanbanheight, carlengthaverage, carbackedgeaverage, lanbanheightaverage, carfrontedgeaverage;
        List<int> carlengthlist = new List<int>();
        List<int> carbackedgelist = new List<int>();
        List<int> lanbanheightlist = new List<int>();
        List<int> carfrontedgelist = new List<int>();

        List<int> calfrontedgelist = new List<int>();
        bool messbox2 = true;
        bool calcarreturn = true;
        public LaserScannerForm()
        {
            InitializeComponent();
        }
        #endregion

        private void LaserScannerForm_Load(object sender, EventArgs e)
        {
            SetMSG("服务程序开启");
            //new 一个叫做s的系列       
            Series s = new Series();
            s.ChartType = SeriesChartType.Point;
            chart1.Series.Add(s);
        }

        private void LaserScannerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            b = false;
        }

        #endregion

        #region 开始扫描、停止扫描

        private void btnStartScan_Click(object sender, EventArgs e)
        {
            b = true;
            btnStopScan.Enabled = true;
            btnStartScan.Enabled = false;
            SetMSG("开始扫描");
            try
            {
                //创建负责通信的Socket
                socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(iptxt.Text);
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(pointtxt.Text));
                //获得要连接的远程服务器应用程序的IP地址和端口号
                socketSend.Connect(point);

                //开启一个新的线程不停的接收服务端发来的消息,并处理
                th = new Thread(datarecpro);               
                th.IsBackground = true;
                th.Start();                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ex:" + ex.Message);
            }
        }

        private void btnStopScan_Click(object sender, EventArgs e)
        {
            b = false;
            btnStartScan.Enabled = true;
            btnStopScan.Enabled = false;
            SetMSG("停止扫描");
            socketSend.Close();
        }

        #endregion

        #region 数据接收
        //控制是否接受数据
        volatile bool b = true;
        public void datarecpro()
        {
            while (b)
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024 * 3];
                    int r = socketSend.Receive(buffer);
                    Thread.Sleep(100); 
                    //将接收到的数据添加到Measurelist集合
                    for (int i =r; i >=0; i--)
                    {
                        Measurelist.Add(buffer[i]);
                    }
                    Measurelist.Reverse();
                    if (Measurelist.Count > 4424)
                    {
                        dataProcess();
                        Measurelist.Clear();
                    }
                }
                catch //(Exception e1)
                {
                    //MessageBox.Show(e1.Message);
                }
            }
        }

        #endregion

        #region 数据处理
        public void dataProcess()
        {
            try
            {
                for (int i = 0; i < Measurelist.Count - 2215; i++)
                {   //检验SYNC
                    if (!(Measurelist[i] == 0xFC && Measurelist[i + 1] == 0xFD && Measurelist[i + 2] == 0xFE && Measurelist[i + 3] == 0xFF && Measurelist[i + 2212] == 0xFC && Measurelist[i + 1 + 2212] == 0xFD && Measurelist[i + 2 + 2212] == 0xFE && Measurelist[i + 3 + 2212] == 0xFF))
                        continue;

                    Measurelist1.Clear();
                    //得到一帧完整数据
                    if (Measurelist.Count - i >= 2212)
                    {
                        for (int j = i; j < Measurelist[i + 5] * 256 + Measurelist[i + 4] + 8 + i; j++)
                        {
                            Measurelist1.Add(Measurelist[j]);
                        }
                    }
                    else
                        return;
                    //进行CHK校验                    
                    int cs = 0, dis;
                    for (int a = 6; a < Measurelist1.Count - 2; a++)
                    {
                        cs = cs + Measurelist1[a];
                    }

                    string cs1 = Convert.ToString(cs, 16);
                    string cs2 = cs1.Substring(cs1.Length - 4, 2);
                    string cs3 = cs1.Substring(cs1.Length - 2, 2);

                    string cs4 = Convert.ToString(Measurelist1[Measurelist1.Count - 1], 16);
                    if (Measurelist1[Measurelist1.Count - 1] < 16)
                        cs4 = "0" + cs4;
                    string cs5 = Convert.ToString(Measurelist1[Measurelist1.Count - 2], 16);
                    if (Measurelist1[Measurelist1.Count - 2] < 16)
                        cs5 = "0" + cs5;

                    if (!(cs2.Equals(cs4) && cs3.Equals(cs5)))
                        continue;

                    //添加测量距离和角度进集合                    
                    for (int a = 15; a < 563; a += 2)
                    {
                        dis = Convert.ToInt32((Measurelist1[a + 1] * 256 + Measurelist1[a]));
                        if (dis == 0)
                            continue;
                        Mirror6p2xy.Add(dis);
                    }

                    //找平行线
                    int n = Convert.ToInt32(num1.Value);
                    int xdirection, ydirection, xleandirection;
                    for (int a = 0; a < n; a++)
                    {   //x方向
                        xdirection = Convert.ToInt32(Mirror6p2xy[a] * Math.Cos((n - a) * 0.3516 * 3.14 / 180));
                        //y方向
                        ydirection = Convert.ToInt32(Mirror6p2xy[a] * Math.Sin((n - a) * 0.3516 * 3.14 / 180));
                        if (xdirection > 20000)
                            continue;
                        showydirectionlist.Add(ydirection);
                        xleandirection = Convert.ToInt32(xdirection * Math.Cos(Convert.ToInt32(num2.Value) * 3.14 / 180));
                        showxleandirectionlist.Add(xleandirection);
                    }
                    for (int a = n; a < 274; a++)
                    {
                        xdirection = Convert.ToInt32(Mirror6p2xy[a] * Math.Cos((a - n) * 0.3516 * 3.14 / 180));
                        ydirection = Convert.ToInt32(Mirror6p2xy[a] * Math.Sin((a - n) * 0.3516 * 3.14 / 180));
                        if (xdirection > 20000)
                            continue;
                        xleandirection = Convert.ToInt32(xdirection * Math.Cos(Convert.ToInt32(num2.Value) * 3.14 / 180));
                        showxleandirectionlist.Add(xleandirection);
                        showydirectionlist.Add(ydirection);
                    }

                    //滤除无效的数据点
                    removeElement(showxleandirectionlist);
                    removeElement(showydirectionlist);

                    //近端和远端各取五个点算平均值，绝对值的差值小于误差值则算作校准
                    int far = 0, near = 0;
                    for (int b = Convert.ToInt32(farTxt.Text); b < Convert.ToInt32(farTxt.Text) + 5; b++)
                    {
                        far = far + showydirectionlist[b];
                    }
                    far = far / 5;
                    for (int d = Convert.ToInt32(nearTxt.Text); d < Convert.ToInt32(nearTxt.Text) + 5; d++)
                    {
                        near = near + showydirectionlist[d];
                    }
                    near = near / 5;

                    errorLab.Text = Math.Abs(far - near).ToString();

                    if (Math.Abs(far - near) < Convert.ToInt32(errorTxt.Text) && messbox1 == true)
                    {
                        MessageBox.Show("校准成功!");
                        messbox1 = false;
                    }

                    //画曲线图
                    Invoke(new Action(delegate()
                    {
                        showcurve();
                    }));
                    
                    //Application.Restart();
                    //计算车长和高度
                    calCar(showxleandirectionlist, showydirectionlist);
                    calAlarm(showxleandirectionlist, showydirectionlist);

                    Mirror6p2xy.Clear();
                    showxleandirectionlist.Clear();
                    showydirectionlist.Clear();
                    show_ydirectionlist.Clear();

                    i = i + 2211;
                    if (calcarreturn == false)
                    {
                        calcarreturn = true;
                        return;
                    }

                    //将得到的车长，车高和车栏板高分别存入集合，以便之后取平均
                    carbackedgelist.Add(carbackedge);
                    carlengthlist.Add(carlength);
                    lanbanheightlist.Add(lanbanheight);
                    carfrontedgelist.Add(carfrontedge);
                    //取平均
                    if (carbackedgelist.Count >= Convert.ToInt32(averageTxt.Text) && carlengthlist.Count >= Convert.ToInt32(averageTxt.Text) && lanbanheightlist.Count >= Convert.ToInt32(averageTxt.Text) && carfrontedgelist.Count >= Convert.ToInt32(averageTxt.Text))
                    {
                        carlengthaverage = calaverage(carlengthlist);
                        carbackedgeaverage = calaverage(carbackedgelist);
                        lanbanheightaverage = calaverage(lanbanheightlist);
                        carfrontedgeaverage = calaverage(carfrontedgelist);
                        safeAddtrText("车长：" + Convert.ToString(carlengthaverage) + "车栏板高：" + Convert.ToString(lanbanheightaverage) + "后上边沿高：" + Convert.ToString(carbackedgeaverage) + "前上边沿高：" + Convert.ToString(carfrontedgeaverage) + "\r\n");
                        carlengthlist.Clear();
                        carbackedgelist.Clear();
                        lanbanheightlist.Clear();
                        carfrontedgelist.Clear();
                    }
                    

                }
            }
            catch (Exception e2)
            {
                MessageBox.Show("e2:" + e2.Message);
            }
        }

        //计算车长，后挡板上边沿离雷达距离，前挡板上边沿离雷达距离和后栏板高度
        public void calCar(List<int> xlist, List<int> ylist)
        {
            for (int i = 0; i < xlist.Count - 1 - Convert.ToInt32(downpointTxt.Text) - 4; i++)
            {
                if (i == xlist.Count - 6 - Convert.ToInt32(downpointTxt.Text))
                {
                    calcarreturn = false;
                    return;
                }

                if (ylist[i + 4] - ylist[i] < Convert.ToInt32(ysuddenchangeTxt.Text))
                    continue;

                for (int a = 1; a <= Convert.ToInt32(downpointTxt.Text); a++)
                {
                    if (Math.Abs(ylist[i + 4 + a] - ylist[i + 4]) <100)
                        continue;

                    calcarreturn = false;
                    return;
                }

                carbackedge = Math.Abs(ylist[i]);//车后挡板上边沿离雷达高度
                lanbanheight = Math.Abs(ylist[i + 4] - ylist[i+2]);//后栏板高度

                for (int c = i + 4; c < ylist.Count; c++)
                {
                    calfrontedgelist.Add(ylist[c]);
                }
                carfrontedge = Math.Abs(min(calfrontedgelist));// 车前挡板上边沿离雷达的高度
                calfrontedgelist.Clear();
                for (int b = i + 4; b < xlist.Count; b++)
                {
                    try
                    {
                        if (Math.Abs(ylist[i + 4] - ylist[b]) > 500)
                        {
                            carlength = xlist[b] - xlist[i];
                            return;
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("1处错误:" + e.Message);
                    }
                }

            }

        }
        bool bo, boo;
        public void calAlarm(List<int> xlist, List<int> ylist)
        {
            try
            {
                boo = true;

                for (int i = xlist.Count - 6; i >= 0; i--)
                {
                    bo = true;
                    //if (i == 6)
                    //{
                    //    return;
                    //}

                    for (int a = 1; a <= 5; a++)
                    {
                        if (Math.Abs(ylist[i + a] - ylist[i]) < 50)
                            continue;

                        bo = false;
                        break;
                    }

                    if (bo == false)
                        continue;

                    if (ylist[i] > 4000 && ylist[i + 2] > 4000 && ylist[i + 3] > 4000 && ylist[i + 4] > 4000 && ylist[i + 5] > 4000)
                        continue;

                    for (int b = i; b < xlist.Count; b++)
                    {
                        if (Math.Abs(ylist[i] - ylist[b]) > 500)
                        {
                            boo = false;
                            if (xlist[b] < Convert.ToInt32(alarmTxt.Text) && messbox2 == true)
                            {
                                MessageBox.Show("已到达停车地点!");
                                messbox2 = false;
                            }
                        }
                    } 
                    if (boo == false)
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("2处错误:" + e.Message);
            }
        }
        public void removeElement(List<int> list)
        {
            int x = list.Count;
            for (int i = 0; i < x; i++)
            {
                if (list.Count >= 183)
                    list.RemoveAt(182);
                else
                    break;
            }
        }

        //求集合的最小值
        public int min(List<int> list)
        {
            List<int> list1 = new List<int>();
            list1.AddRange(list);
            list1.Sort();
            return list1[0];
        }

        //曲线图显示         
        public void showcurve()
        {
            try
            {
                for (int i = 0; i < showydirectionlist.Count; i++)
                {
                    show_ydirectionlist.Add(-showydirectionlist[i]);
                }
                this.chart1.Series[0].Points.DataBindXY(showxleandirectionlist, show_ydirectionlist);
            }
            catch //(Exception e3)
            {
                //MessageBox.Show("e3:" + e3.Message);
            }
        }

        //求集合平均值
        public int calaverage(List<int> list)
        {
            int average = 0;
            for (int i = 0; i < list.Count; i++)
            {
                average = average + list[i];
            }
            return average = average / list.Count;
        }
        #endregion

        #region 显示数据
        //委托的实现  
        private void safeAddtrText(string text)
        {
            if (this.InvokeRequired)
            {
                _SafeAddtrTextCall callALL =
                   delegate(string s)
                   {
                       txtBoxAllReceive.AppendText(s);
                   };
                this.Invoke(callALL, text);
            }
            else
            {
                txtBoxAllReceive.AppendText(text);
            }
        }
        #endregion

        #region 消息和日志

        /// <summary>
        /// //设置程序运行中产生的消息并计入日志
        /// </summary>
        /// <param name="msg">消息</param>
        protected void SetMSG(string msg)
        {
            ShowMSgDelegate showmsgDelegate = new ShowMSgDelegate(SetmsgDelegateTargetFun);
            if (txtBoxMSG.InvokeRequired)
            {
                txtBoxMSG.BeginInvoke(showmsgDelegate, msg);
            }
            else
            {
                if (txtBoxMSG.Text.Length > 1024 * 512)
                {
                    txtBoxMSG.Text = string.Empty;
                }
                txtBoxMSG.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "：" + msg + "\r\n");
            }
            mu.WaitOne();
            //SiXi.Logs.Log.WriteDebugLog(msg);
            mu.ReleaseMutex();
        }
        /// <summary>
        /// 显示消息的委托的目标函数
        /// </summary>
        /// <param name="msg">要显示的消息</param>
        protected void SetmsgDelegateTargetFun(string msg)
        {
            if (txtBoxMSG.Text.Length > 1024 * 512)
            {
                txtBoxMSG.Text = string.Empty;
            }
            txtBoxMSG.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "：" + msg + "\r\n");
        }
        /// <summary>
        /// //显示程序运行中产生的异常消息并计入日志
        /// </summary>
        /// <param name="msg">消息</param>
        protected void SetExceptionMSG(Exception msg)
        {
            ShowExceptionMSgDelegate showmsgDelegate = new ShowExceptionMSgDelegate(SetExceptionmsgDelegateTargetFun);
            if (txtBoxMSG.InvokeRequired)
            {
                txtBoxMSG.BeginInvoke(showmsgDelegate, msg);
            }
            else
            {
                if (txtBoxMSG.Text.Length > 1024 * 512)
                {
                    txtBoxMSG.Text = string.Empty;
                }
                txtBoxMSG.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "：" + msg.ToString() + "\r\n");
            }
            muExceptionLog.WaitOne();
            //SiXi.Logs.Log.WriteDebugLog(msg.ToString());
            muExceptionLog.ReleaseMutex();
        }
        /// <summary>
        /// 显示异常消息的委托的目标函数
        /// </summary>
        /// <param name="msg">要显示的消息</param>
        protected void SetExceptionmsgDelegateTargetFun(Exception msg)
        {
            if (txtBoxMSG.Text.Length > 1024 * 512)
            {
                txtBoxMSG.Text = string.Empty;
            }
            txtBoxMSG.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "：" + msg.ToString() + "\r\n");
        }
        #endregion

       
    }
}