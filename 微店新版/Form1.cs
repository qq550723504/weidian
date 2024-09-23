// Decompiled with JetBrains decompiler
// Type: 微店新版.Form1
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 微店新版.HTTP;
using 微店新版.Tool;
using 微店新版.下单;
using 微店新版.注册;
using File = System.IO.File;


namespace 微店新版
{
    public class Form1 : Form
    {
        private DataRow rows;
        private DataTable DT = new DataTable();
        public static List<info> infos = new List<info>();
        private flurl flurl;
        private IContainer components = null;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Panel panel1;
        private TabPage tabPage2;
        private Panel panel2;
        private TabPage tabPage3;
        private Panel panel3;
        private TabPage tabPage4;
        private Panel panel4;
        private TabPage tabPage5;
        private Panel panel5;
        private TabPage tabPage6;
        private Panel panel6;
        private RichTextBox richTextBox1;
        private TabPage tabPage7;
        private GroupBox groupBox1;
        private Button button1;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private TextBox textBox5;
        private TextBox textBox6;
        private Label label5;
        private Label label6;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label label3;
        private Label label4;
        private GroupBox groupBox3;
        private Button button2;
        private TextBox textBox15;
        private Label label18;
        private TextBox textBox17;
        private Label label21;
        private CheckBox checkBox2;
        private TextBox textBox18;
        private Label label22;
        private CheckBox checkBox1;
        private Button button3;
        private TextBox textBox9;
        private Label label7;
        private TextBox textBox11;
        private Label label9;
        private TextBox textBox10;
        private Label label8;
        private TextBox textBox12;
        private TabPage tabPage8;
        private Panel panel7;
        private ComboBox comboBox1;
        private TabPage tabPage9;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn 快递编号;
        private DataGridViewTextBoxColumn 快递名称;
        private TextBox textBox7;
        private Label label10;
        private TabPage tabPage10;
        private Panel panel8;
        private TabPage tabPage11;
        private Panel panel9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private TabPage tabPage12;
        private Panel panel10;
        private TabPage tabPage13;
        private Panel panel11;
        private LinkLabel linkLabel1;

        public Form1(DataRow rows)
        {
            InitializeComponent();
            this.rows = rows;
            //string empty1 = string.Empty;
            //string empty2 = string.Empty;
            string str1;
            string str2;
            if (rows["role"].ToString() == "Y")
            {
                str1 = "超级管理员";
                str2 = "无时间限制";
            }
            else if (rows["role"].ToString() == "N")
            {
                str2 = rows["expire"].ToString();
                str1 = "包月用户";
            }
            else
            {
                str1 = "用户";
                str2 = "无时间限制";
            }
            toolStripStatusLabel1.Text = "欢迎" + rows["username"].ToString() + str1 + "登录，您的账号到期时间是：" + str2 + "，您的可使用账号数量为：" + rows["count"].ToString() + "个";
            COMM.TOKENS = rows["username"].ToString();
            Model.KD_Root kdRoot = JsonConvert.DeserializeObject<Model.KD_Root>("{\"status\":{\"code\":0,\"message\":\"OK\",\"description\":\"\"},\"result\":{\"common_express\":[{\"default_express\":1,\"express_code\":\"shentong\",\"express_company\":\"申通快递\",\"id\":4},{\"default_express\":1,\"express_code\":\"yuantong\",\"express_company\":\"圆通速递\",\"id\":2},{\"default_express\":1,\"express_code\":\"zhongtong\",\"express_company\":\"中通速递\",\"id\":3},{\"default_express\":1,\"express_code\":\"huitongkuaidi\",\"express_company\":\"百世快递\",\"id\":5},{\"default_express\":1,\"express_code\":\"yunda\",\"express_company\":\"韵达快递\",\"id\":6},{\"default_express\":1,\"express_code\":\"shunfeng\",\"express_company\":\"顺丰速运\",\"id\":1},{\"default_express\":1,\"express_code\":\"zhaijisong\",\"express_company\":\"宅急送\",\"id\":7},{\"default_express\":1,\"express_code\":\"lianbangkuaidi\",\"express_company\":\"联邦快递\",\"id\":8},{\"default_express\":1,\"express_code\":\"ems\",\"express_company\":\"EMS\",\"id\":9},{\"default_express\":1,\"express_code\":\"quanfengkuaidi\",\"express_company\":\"全峰快递\",\"id\":20},{\"default_express\":1,\"express_code\":\"youshuwuliu\",\"express_company\":\"优速快递\",\"id\":22},{\"default_express\":1,\"express_code\":\"kuaijiesudi\",\"express_company\":\"快捷快递\",\"id\":23},{\"default_express\":1,\"express_code\":\"debangwuliu\",\"express_company\":\"德邦\",\"id\":24},{\"default_express\":1,\"express_code\":\"youzhengguonei\",\"express_company\":\"中国邮政\",\"id\":25},{\"default_express\":1,\"express_code\":\"weitepai\",\"express_company\":\"微特派\",\"id\":26},{\"default_express\":1,\"express_code\":\"longbanwuliu\",\"express_company\":\"龙邦速递\",\"id\":27},{\"default_express\":1,\"express_code\":\"suer\",\"express_company\":\"速尔快递\",\"id\":28},{\"default_express\":1,\"express_code\":\"wanxiangwuliu\",\"express_company\":\"万象物流\",\"id\":29},{\"default_express\":1,\"express_code\":\"dayangwuliu\",\"express_company\":\"大洋物流\",\"id\":30},{\"default_express\":1,\"express_code\":\"dhl\",\"express_company\":\"DHL（国际件）\",\"id\":600},{\"default_express\":1,\"express_code\":\"emsguoji\",\"express_company\":\"EMS（国际件）\",\"id\":601},{\"default_express\":1,\"express_code\":\"fedexcn\",\"express_company\":\"Fedex（国际件）\",\"id\":602},{\"default_express\":1,\"express_code\":\"fedexus\",\"express_company\":\"Fedex（美国件）\",\"id\":603},{\"default_express\":1,\"express_code\":\"fedex\",\"express_company\":\"Fedex（英国件）\",\"id\":604},{\"default_express\":1,\"express_code\":\"tnt\",\"express_company\":\"TNT\",\"id\":605},{\"default_express\":1,\"express_code\":\"tntau\",\"express_company\":\"TNT（澳大利亚件）\",\"id\":606},{\"default_express\":1,\"express_code\":\"tntuk\",\"express_company\":\"TNT（英国件）\",\"id\":607},{\"default_express\":1,\"express_code\":\"ups\",\"express_company\":\"UPS\",\"id\":608},{\"default_express\":1,\"express_code\":\"usps\",\"express_company\":\"USPS\",\"id\":609},{\"default_express\":1,\"express_code\":\"skynetmalaysia\",\"express_company\":\"Skynet\",\"id\":610},{\"default_express\":1,\"express_code\":\"japanposten\",\"express_company\":\"日本邮政\",\"id\":611},{\"default_express\":1,\"express_code\":\"koreapost\",\"express_company\":\"韩国邮政\",\"id\":612},{\"default_express\":1,\"express_code\":\"auspost\",\"express_company\":\"澳大利亚邮政\",\"id\":613},{\"default_express\":1,\"express_code\":\"hkpost\",\"express_company\":\"香港邮政\",\"id\":614},{\"default_express\":1,\"express_code\":\"speedpost\",\"express_company\":\"新加坡邮政大包\",\"id\":615},{\"default_express\":1,\"express_code\":\"singpost\",\"express_company\":\"新加坡邮政小包\",\"id\":616},{\"default_express\":1,\"express_code\":\"parcelforce\",\"express_company\":\"英国邮政大包\",\"id\":617},{\"default_express\":1,\"express_code\":\"royalmail\",\"express_company\":\"英国邮政小包\",\"id\":618},{\"default_express\":1,\"express_code\":\"malaysiaems\",\"express_company\":\"马来西亚邮政大包\",\"id\":619},{\"default_express\":1,\"express_code\":\"malaysiapost\",\"express_company\":\"马来西亚邮政小包\",\"id\":620},{\"default_express\":1,\"express_code\":\"newzealand\",\"express_company\":\"新西兰邮政\",\"id\":621},{\"default_express\":1,\"express_code\":\"guotongkuaidi\",\"express_company\":\"国通快递\",\"id\":38248},{\"default_express\":1,\"express_code\":\"tiantian\",\"express_company\":\"天天快递\",\"id\":42975},{\"default_express\":1,\"express_code\":\"zengyisudi\",\"express_company\":\"增益\",\"id\":260702},{\"default_express\":1,\"express_code\":\"ztky\",\"express_company\":\"中铁物流\",\"id\":260703},{\"default_express\":1,\"express_code\":\"zhongtiewuliu\",\"express_company\":\"中铁快运\",\"id\":260704},{\"default_express\":1,\"express_code\":\"ganzhongnengda\",\"express_company\":\"能达\",\"id\":260705},{\"default_express\":1,\"express_code\":\"yitongfeihong\",\"express_company\":\"一统飞鸿\",\"id\":260706},{\"default_express\":1,\"express_code\":\"rufengda\",\"express_company\":\"如风达\",\"id\":260707},{\"default_express\":1,\"express_code\":\"saiaodi\",\"express_company\":\"赛澳递\",\"id\":260708},{\"default_express\":1,\"express_code\":\"haihongwangsong\",\"express_company\":\"海红网送\",\"id\":260709},{\"default_express\":1,\"express_code\":\"tonghetianxia\",\"express_company\":\"通和天下\",\"id\":260710},{\"default_express\":1,\"express_code\":\"zhengzhoujianhua\",\"express_company\":\"郑州建华\",\"id\":260711},{\"default_express\":1,\"express_code\":\"sxhongmajia\",\"express_company\":\"红马甲\",\"id\":260712},{\"default_express\":1,\"express_code\":\"zhimakaimen\",\"express_company\":\"芝麻开门\",\"id\":260713},{\"default_express\":1,\"express_code\":\"lejiedi\",\"express_company\":\"乐捷递\",\"id\":260714},{\"default_express\":1,\"express_code\":\"lijisong\",\"express_company\":\"立即送\",\"id\":260715},{\"default_express\":1,\"express_code\":\"yinjiesudi\",\"express_company\":\"银捷\",\"id\":260716},{\"default_express\":1,\"express_code\":\"menduimen\",\"express_company\":\"门对门\",\"id\":260717},{\"default_express\":1,\"express_code\":\"hebeijianhua\",\"express_company\":\"河北建华\",\"id\":260718},{\"default_express\":1,\"express_code\":\"fengxingtianxia\",\"express_company\":\"风行天下\",\"id\":260719},{\"default_express\":1,\"express_code\":\"shangcheng\",\"express_company\":\"尚橙\",\"id\":260720},{\"default_express\":1,\"express_code\":\"neweggozzo\",\"express_company\":\"新蛋奥硕\",\"id\":260721},{\"default_express\":1,\"express_code\":\"jiajiwuliu\",\"express_company\":\"佳吉快运\",\"id\":301465},{\"default_express\":1,\"express_code\":\"sd138\",\"express_company\":\"泰国138国际物流\",\"id\":301466},{\"default_express\":1,\"express_code\":\"chengguangkuaidi\",\"express_company\":\"程光快递\",\"id\":301467},{\"default_express\":1,\"express_code\":\"changjiang\",\"express_company\":\"长江国际速递\",\"id\":301468},{\"default_express\":1,\"express_code\":\"lantiankuaidi\",\"express_company\":\"蓝天快递\",\"id\":301469},{\"default_express\":1,\"express_code\":\"pcaexpress\",\"express_company\":\"PCA Express\",\"id\":301470},{\"default_express\":1,\"express_code\":\"ems\",\"express_company\":\"邮政快包\",\"id\":329724},{\"default_express\":1,\"express_code\":\"youzhengguonei\",\"express_company\":\"邮政小包\",\"id\":341539},{\"default_express\":1,\"express_code\":\"exfresh\",\"express_company\":\"安鲜达\",\"id\":544780},{\"default_express\":1,\"express_code\":\"arkexpress\",\"express_company\":\"方舟速递\",\"id\":566616},{\"default_express\":1,\"express_code\":\"qinyuan\",\"express_company\":\"秦远国际物流\",\"id\":566617},{\"default_express\":1,\"express_code\":\"annengwuliu\",\"express_company\":\"安能物流\",\"id\":576454},{\"default_express\":1,\"express_code\":\"alog\",\"express_company\":\"心怡物流\",\"id\":581394},{\"default_express\":1,\"express_code\":\"ycgky\",\"express_company\":\"远成快运\",\"id\":581395},{\"default_express\":1,\"express_code\":\"jd\",\"express_company\":\"京东快递\",\"id\":616246},{\"default_express\":1,\"express_code\":\"jieanda\",\"express_company\":\"捷安达国际速递\",\"id\":618531},{\"default_express\":1,\"express_code\":\"higo\",\"express_company\":\"黑狗物流\",\"id\":620358},{\"default_express\":1,\"express_code\":\"ewe\",\"express_company\":\"EWE全球快递\",\"id\":623368},{\"default_express\":1,\"express_code\":\"jiuyescm\",\"express_company\":\"九曳供应链\",\"id\":638437},{\"default_express\":1,\"express_code\":\"zhaijibian\",\"express_company\":\"宅急便\",\"id\":651663},{\"default_express\":1,\"express_code\":\"fsexp\",\"express_company\":\"全速快递\",\"id\":654748},{\"default_express\":1,\"express_code\":\"dhlde\",\"express_company\":\"DHL-德国\",\"id\":659318},{\"default_express\":1,\"express_code\":\"yourscm\",\"express_company\":\"雅澳物流\",\"id\":660775},{\"default_express\":1,\"express_code\":\"xinfengwuliu\",\"express_company\":\"信丰物流\",\"id\":662589},{\"default_express\":1,\"express_code\":\"kuayue\",\"express_company\":\"跨越速运\",\"id\":663678},{\"default_express\":1,\"express_code\":\"freakyquick\",\"express_company\":\"FQ狂派速递\",\"id\":665154},{\"default_express\":1,\"express_code\":\"polarexpress\",\"express_company\":\"极地快递\",\"id\":673954},{\"default_express\":1,\"express_code\":\"zhuanyunsifang\",\"express_company\":\"转运四方\",\"id\":678065},{\"default_express\":1,\"express_code\":\"auexpress\",\"express_company\":\"澳邮中国快运\",\"id\":678754},{\"default_express\":1,\"express_code\":\"kingfreight\",\"express_company\":\"货运皇\",\"id\":678759},{\"default_express\":1,\"express_code\":\"valueway\",\"express_company\":\"美通物流\",\"id\":683061},{\"default_express\":1,\"express_code\":\"pjbest\",\"express_company\":\"品骏\",\"id\":684855},{\"default_express\":1,\"express_code\":\"ueq\",\"express_company\":\"UEQ\",\"id\":685148},{\"default_express\":1,\"express_code\":\"cncexp\",\"express_company\":\"C&C国际速递\",\"id\":690153},{\"default_express\":1,\"express_code\":\"ftd\",\"express_company\":\"富腾达国际\",\"id\":690159},{\"default_express\":1,\"express_code\":\"euguoji\",\"express_company\":\"易邮国际\",\"id\":691476},{\"default_express\":1,\"express_code\":\"ruidianyouzheng\",\"express_company\":\"瑞典邮政\",\"id\":692140},{\"default_express\":1,\"express_code\":\"baishiwuliu\",\"express_company\":\"百世快运\",\"id\":694213},{\"default_express\":1,\"express_code\":\"turtle\",\"express_company\":\"海龟国际快递\",\"id\":698552},{\"default_express\":1,\"express_code\":\"yfsuyun\",\"express_company\":\"驭丰速运\",\"id\":698981},{\"default_express\":1,\"express_code\":\"nsf\",\"express_company\":\"新顺丰(NSF)\",\"id\":702719},{\"default_express\":1,\"express_code\":\"lianhaowuliu\",\"express_company\":\"联昊通速递\",\"id\":702784},{\"default_express\":1,\"express_code\":\"qexpress\",\"express_company\":\"易达通快递\",\"id\":703280},{\"default_express\":1,\"express_code\":\"efs\",\"express_company\":\"平安快递EFS\",\"id\":703281},{\"default_express\":1,\"express_code\":\"xinbangwuliu\",\"express_company\":\"新邦物流\",\"id\":706034},{\"default_express\":1,\"express_code\":\"quanyikuaidi\",\"express_company\":\"全一快递\",\"id\":706273},{\"default_express\":1,\"express_code\":\"nanjingshengbang\",\"express_company\":\"晟邦物流\",\"id\":710568},{\"default_express\":1,\"express_code\":\"lntjs\",\"express_company\":\"特急送\",\"id\":710688},{\"default_express\":1,\"express_code\":\"adapost\",\"express_company\":\"安达速递\",\"id\":711787},{\"default_express\":1,\"express_code\":\"fastgo\",\"express_company\":\"速派快递\",\"id\":726469},{\"default_express\":1,\"express_code\":\"ytchengnuoda\",\"express_company\":\"圆通承诺达\",\"id\":730694},{\"default_express\":1,\"express_code\":\"zhongtongkuaiyun\",\"express_company\":\"中通快运\",\"id\":731717},{\"default_express\":1,\"express_code\":\"rrs\",\"express_company\":\"日日顺物流\",\"id\":735439},{\"default_express\":1,\"express_code\":\"oneexpress\",\"express_company\":\"一速递\",\"id\":735654},{\"default_express\":1,\"express_code\":\"etong\",\"express_company\":\"E通\",\"id\":735666},{\"default_express\":1,\"express_code\":\"beebird\",\"express_company\":\"锋鸟物流\",\"id\":736031},{\"default_express\":1,\"express_code\":\"xlobo\",\"express_company\":\"贝海国际速递\",\"id\":736109},{\"default_express\":1,\"express_code\":\"excocotree\",\"express_company\":\"可可树美中速运\",\"id\":737987},{\"default_express\":1,\"express_code\":\"adapost\",\"express_company\":\"安达速递\",\"id\":742641},{\"default_express\":1,\"express_code\":\"chinaicip\",\"express_company\":\"卓志速运\",\"id\":742642},{\"default_express\":1,\"express_code\":\"ztky\",\"express_company\":\"中铁快运\",\"id\":742643},{\"default_express\":1,\"express_code\":\"zhongtongguoji\",\"express_company\":\"中通国际\",\"id\":742644},{\"default_express\":1,\"express_code\":\"jtexpress\",\"express_company\":\"极兔速递\",\"id\":748760},{\"default_express\":1,\"express_code\":\"haidaibao\",\"express_company\":\"海带宝\",\"id\":751001},{\"default_express\":1,\"express_code\":\"tnjex\",\"express_company\":\"明通国际快递\",\"id\":755771},{\"default_express\":1,\"express_code\":\"zhongyouex\",\"express_company\":\"众邮快递\",\"id\":756750},{\"default_express\":1,\"express_code\":\"tiandihuayu\",\"express_company\":\"天地华宇\",\"id\":756823},{\"default_express\":1,\"express_code\":\"danniao\",\"express_company\":\"丹鸟\",\"id\":758632},{\"default_express\":1,\"express_code\":\"suning\",\"express_company\":\"苏宁物流\",\"id\":758712},{\"default_express\":1,\"express_code\":\"idamalu\",\"express_company\":\"大马鹿\",\"id\":761177},{\"default_express\":1,\"express_code\":\"highsince\",\"express_company\":\"Highsince海欣斯\",\"id\":761702},{\"default_express\":1,\"express_code\":\"changjiang\",\"express_company\":\"长江国际速递\",\"id\":764753},{\"default_express\":1,\"express_code\":\"sendtochina\",\"express_company\":\"速递中国\",\"id\":765224},{\"default_express\":1,\"express_code\":\"cjkoreaexpress\",\"express_company\":\"大韩通运\",\"id\":767141},{\"default_express\":1,\"express_code\":\"ilogen\",\"express_company\":\"logen路坚\",\"id\":767153},{\"default_express\":1,\"express_code\":\"sxjdfreight\",\"express_company\":\"顺心捷达\",\"id\":768050},{\"default_express\":1,\"express_code\":\"yimidida\",\"express_company\":\"壹米滴答\",\"id\":768051},{\"default_express\":1,\"express_code\":\"\",\"express_company\":\"其他\",\"id\":770593},{\"default_express\":1,\"express_code\":\"astexpress\",\"express_company\":\"安世通快递\",\"id\":773175},{\"default_express\":1,\"express_code\":\"ubonex\",\"express_company\":\"优邦速运\",\"id\":776132},{\"default_express\":1,\"express_code\":\"yuxinwuliu\",\"express_company\":\"宇鑫物流\",\"id\":790099},{\"default_express\":1,\"express_code\":\"fengwang\",\"express_company\":\"丰网速运\",\"id\":790498},{\"default_express\":1,\"express_code\":\"jinguangsudikuaijian\",\"express_company\":\"京广速递\",\"id\":791069},{\"default_express\":1,\"express_code\":\"yaofeikuaidi\",\"express_company\":\"耀飞同城快递\",\"id\":792105},{\"default_express\":1,\"express_code\":\"huisenky\",\"express_company\":\"汇森速运\",\"id\":811402},{\"default_express\":1,\"express_code\":\"jinguangsudikuaijian\",\"express_company\":\"京广速递\",\"id\":812450},{\"default_express\":1,\"express_code\":\"suteng\",\"express_company\":\"速腾快递\",\"id\":819228},{\"default_express\":1,\"express_code\":\"PADTF\",\"express_company\":\"平安达腾飞\",\"id\":825667},{\"default_express\":1,\"express_code\":\"JYM\",\"express_company\":\"加运美快递\",\"id\":830859},{\"default_express\":1,\"express_code\":\"yundakuaiyun\",\"express_company\":\"韵达快运\",\"id\":835687}]}}");
            DataTable dt = new DataTable();
            dt.Columns.Add(nameof(快递编号));
            dt.Columns.Add(nameof(快递名称));
            foreach (Model.KD_Common_express kdCommonExpress in kdRoot.result.common_express)
            {
                DataRow row = dt.NewRow();
                row[nameof(快递编号)] = kdCommonExpress.id;
                row[nameof(快递名称)] = kdCommonExpress.express_company;
                dt.Rows.Add(row);
                if (dt != null && dt.Rows.Count > 0)
                    UpdateGV(dt);
            }
        }

        private void UpdateGV(DataTable dt)
        {
            if (dataGridView1.InvokeRequired)
            {
                BeginInvoke(new UpdateDataGridView(UpdateGV), dt);
            }
            else
            {
                try
                {
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void log(string text)
        {
            if (textBox12.InvokeRequired)
                textBox12.Invoke(new SetTextHandler(log), text);
            else
                textBox12.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + "\r\n");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            log("任何操作，都需要点保存设置，否则不会生效");
            log("扫码登录店铺后，选择店铺后，就不需要再做其他操作，程序会自动关闭网页");
            if (IniFiles.ExistINIFile())
            {
                COMM.proxy_ip = IniFiles.IniReadValue("代理", "host");
                COMM.proxy_port = int.Parse(IniFiles.IniReadValue("代理", "proxy"));
                COMM.username = IniFiles.IniReadValue("代理", "username");
                COMM.password = IniFiles.IniReadValue("代理", "password");
                COMM.pt_account = IniFiles.IniReadValue("平台", "account");
                COMM.pt_pass = IniFiles.IniReadValue("平台", "pass");
                COMM.isgj = IniFiles.IniReadValue("其他", "改价") == "是";
                COMM.isgz = IniFiles.IniReadValue("其他", "关注") == "是";
                COMM.ts = IniFiles.IniReadValue("其他", "核销间隔");
                COMM.paypass = IniFiles.IniReadValue("其他", "pass");
                COMM.card = IniFiles.IniReadValue("其他", "card");
                COMM.order_count = IniFiles.IniReadValue("其他", "order_count");
                string str1 = IniFiles.IniReadValue("店铺", "param");
                textBox1.Text = COMM.pt_account;
                textBox2.Text = COMM.pt_pass;
                textBox4.Text = COMM.proxy_ip;
                textBox3.Text = COMM.proxy_port.ToString();
                textBox6.Text = COMM.username;
                textBox5.Text = COMM.password;
                textBox7.Text = COMM.order_count;
                checkBox1.Checked = COMM.isgj;
                textBox18.Text = IniFiles.IniReadValue("其他", "改价价格");
                checkBox2.Checked = COMM.isgz;
                textBox9.Text = COMM.ts;
                textBox10.Text = COMM.paypass;
                textBox11.Text = COMM.card;
                textBox17.Text = IniFiles.IniReadValue("其他", "店铺ID");
                COMM.price = textBox18.Text;
                COMM.shopid = textBox17.Text;
                if (!string.IsNullOrWhiteSpace(str1))
                {
                    COMM.plist = new List<param>();
                    string str2 = str1;
                    char[] chArray = new char[1] { '，' };
                    foreach (string str3 in str2.Split(chArray))
                    {
                        string[] strArray = str3.Split('-');
                        COMM.plist.Add(new param()
                        {
                            ztid = strArray[1],
                            remark = strArray[0]
                        });
                        comboBox1.Items.Add(str3);
                    }
                }
                comboBox1.SelectedIndex = 0;
                flurl = new flurl();
            }
            panel1.Controls.Clear();
            账号注册 账号注册 = new 账号注册
            {
                Dock = DockStyle.Fill
            };
            panel1.Controls.Add(账号注册);
            panel7.Controls.Clear();
            密码设置 密码设置 = new 密码设置
            {
                Dock = DockStyle.Fill
            };
            panel7.Controls.Add(密码设置);
            panel2.Controls.Clear();
            下单.下单 下单 = new 下单.下单
            {
                Dock = DockStyle.Fill
            };
            panel2.Controls.Add(下单);
            panel3.Controls.Clear();
            支付 支付 = new 支付
            {
                Dock = DockStyle.Fill
            };
            panel3.Controls.Add(支付);
            panel4.Controls.Clear();
            核销 核销 = new 核销();
            核销.Dock = DockStyle.Fill;
            panel4.Controls.Add(核销);
            panel5.Controls.Clear();
            评论 评论 = new 评论();
            评论.Dock = DockStyle.Fill;
            panel5.Controls.Add(评论);
            panel8.Controls.Clear();
            速刷下单 速刷下单 = new 速刷下单();
            速刷下单.Dock = DockStyle.Fill;
            panel8.Controls.Add(速刷下单);
            panel9.Controls.Clear();
            水印 水印 = new 水印();
            水印.Dock = DockStyle.Fill;
            panel9.Controls.Add(水印);
            panel10.Controls.Clear();
            采集评论 采集评论 = new 采集评论();
            采集评论.Dock = DockStyle.Fill;
            panel10.Controls.Add(采集评论);
            panel11.Controls.Clear();
            一键复制上架 一键复制上架 = new 一键复制上架();
            一键复制上架.Dock = DockStyle.Fill;
            panel11.Controls.Add(一键复制上架);
            string path = Environment.CurrentDirectory + "\\UA.txt";
            if (File.Exists(path))
            {
                foreach (string readAllLine in File.ReadAllLines(path, Encoding.UTF8))
                {
                    if (!string.IsNullOrEmpty(readAllLine))
                        COMM.agentlist.Add(readAllLine);
                }
            }
            if (rows == null)
                Controls.Remove(tabControl1);
            if (rows["role"].ToString() != "Y")
            {
                tabControl1.TabPages["tabPage8"].Parent = null;
                tabControl1.TabPages["tabPage1"].Parent = null;
                groupBox1.Visible = false;
            }
            else
                groupBox1.Visible = true;
            tabControl1.TabPages["tabPage10"].Parent = null;
            if (!(rows["S"].ToString() == "N"))
                return;
            tabControl1.TabPages["tabPage11"].Parent = null;
            tabControl1.TabPages["tabPage12"].Parent = null;
            tabControl1.TabPages["tabPage13"].Parent = null;
        }

        public void update()
        {
            COMM.proxy_ip = IniFiles.IniReadValue("代理", "host");
            COMM.proxy_port = int.Parse(IniFiles.IniReadValue("代理", "proxy"));
            COMM.username = IniFiles.IniReadValue("代理", "username");
            COMM.password = IniFiles.IniReadValue("代理", "password");
            COMM.pt_account = IniFiles.IniReadValue("平台", "account");
            COMM.pt_pass = IniFiles.IniReadValue("平台", "pass");
            COMM.isgj = IniFiles.IniReadValue("其他", "改价") == "是";
            COMM.price = IniFiles.IniReadValue("其他", "改价价格");
            COMM.isgz = IniFiles.IniReadValue("其他", "关注") == "是";
            COMM.shopid = IniFiles.IniReadValue("其他", "店铺ID");
            COMM.ts = IniFiles.IniReadValue("其他", "核销间隔");
            COMM.paypass = IniFiles.IniReadValue("其他", "pass");
            COMM.card = IniFiles.IniReadValue("其他", "card");
            COMM.order_count = IniFiles.IniReadValue("其他", "order_count");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IniFiles.IniWriteValue("平台", "account", textBox1.Text);
            IniFiles.IniWriteValue("平台", "pass", textBox2.Text);
            IniFiles.IniWriteValue("代理", "HOST", textBox4.Text);
            IniFiles.IniWriteValue("代理", "proxy", textBox3.Text);
            IniFiles.IniWriteValue("代理", "username", textBox6.Text);
            IniFiles.IniWriteValue("代理", "password", textBox5.Text);
            IniFiles.IniWriteValue("其他", "改价", checkBox1.Checked ? "是" : "否");
            IniFiles.IniWriteValue("其他", "改价价格", textBox18.Text);
            IniFiles.IniWriteValue("其他", "关注", checkBox2.Checked ? "是" : "否");
            IniFiles.IniWriteValue("其他", "店铺ID", textBox17.Text);
            IniFiles.IniWriteValue("其他", "核销间隔", textBox9.Text);
            IniFiles.IniWriteValue("其他", "pass", textBox10.Text);
            IniFiles.IniWriteValue("其他", "card", textBox11.Text);
            IniFiles.IniWriteValue("其他", "order_count", textBox7.Text);
            COMM.cookie = textBox15.Text;
            log("设置保存成功");
            update();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox18.Enabled = true;
                COMM.price = textBox18.Text;
            }
            else
            {
                textBox18.Enabled = false;
                COMM.price = "";
            }
            COMM.isgj = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox17.Enabled = true;
                COMM.shopid = textBox17.Text;
            }
            else
            {
                textBox17.Enabled = false;
                COMM.shopid = "";
            }
            COMM.isgz = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e) => GetToken();

        /// <summary>
        /// 登录接码平台
        /// </summary>
        public async void GetToken()
        {
            Haozhu haozhu = new Haozhu(flurl);
            string result = await haozhu.GetToken();
            log(result);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
                COMM.shop_param = "";
            else
                COMM.shop_param = comboBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    ChromeDriverService defaultService = ChromeDriverService.CreateDefaultService();
                    defaultService.HideCommandPromptWindow = true;
                    ChromeOptions options = new ChromeOptions();
                    options.AddExcludedArgument("enable-automation");
                    options.AddArgument("--mute-audio");
                    options.AddArgument("--auto-open-devtools-for-tabs");
                    options.AddAdditionalCapability("useAutomationExtension", false);
                    options.AddArguments("lang=zh_CN.UTF-8");
                    options.AddArgument("--disable-gpu");
                    ChromeDriver chromeDriver = new ChromeDriver(defaultService, options);
                    chromeDriver.Navigate().GoToUrl("https://d.weidian.com/weidian-pc/login/index.html#/");
                    chromeDriver.Manage().Window.Maximize();
                    chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000.0);
                    chromeDriver.FindElement(By.ClassName("qrcode-img")).Click();
                    while (!chromeDriver.Url.Contains("pc-vue-micro-index/index"))
                        Thread.Sleep(1000);
                    Thread.Sleep(3000);
                    string cookiestr = "";
                    ICookieJar cookies = chromeDriver.Manage().Cookies;
                    for (int index = 0; index < cookies.AllCookies.Count; ++index)
                        cookiestr = cookiestr + cookies.AllCookies[index].Name + "=" + cookies.AllCookies[index].Value + ";";
                    Invoke(new EventHandler(delegate (object p0, EventArgs p1)
              {
                    textBox15.Text = cookiestr;
                }));
                    COMM.cookie = cookiestr;
                    log("店铺获取成功");
                    try
                    {
                        chromeDriver.Close();
                        chromeDriver.Quit();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                catch (Exception ex)
                {
                    log("店铺CK获取异常");
                }
            });
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
        }

        private async void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(COMM.cookie))
            {
                log("请登录店铺，手动输入的cookie请点击保存设置后再获取自提点");
            }
            else
            {
                string[] strArray = COMM.cookie.Split(';');
                for (int index = 0; index < strArray.Length; ++index)
                {
                    string part = strArray[index];
                    if (!string.IsNullOrWhiteSpace(part))
                    {
                        string[] pair = part.Trim().Split('=');
                        string key = pair[0];
                        string value = pair[1];
                        if (key == "sid")
                        {
                            textBox17.Text = value;
                            comboBox1.Items.Clear();
                            string res = await flurl.GET999("https://thor.weidian.com/address/sellerGetAddressList/2.0?param=%7B%22page_num%22%3A0%2C%22page_size%22%3A20%2C%22name_like%22%3A%22%22%2C%22type%22%3A2%7D&wdtoken=8cb9c17a&_=1708406092074", COMM.cookie);
                            if (!string.IsNullOrWhiteSpace(res))
                            {
                                JObject json = COMM.GetToJsonList(res);
                                if (json != null && json["result"] != null && json["result"]["shopAddressDetailRespDTOList"] != null && json["result"]["shopAddressDetailRespDTOList"].Count<JToken>() > 0)
                                {
                                    foreach (JToken item in json["result"]["shopAddressDetailRespDTOList"])
                                        comboBox1.Items.Add(item["name"].ToString() + "-" + item["id"].ToString());
                                    comboBox1.SelectedIndex = 0;
                                }
                                log("自提点获取成功");
                                json = null;
                            }
                            res = null;
                        }
                        pair = null;
                        key = null;
                        value = null;
                        part = null;
                    }
                }
                strArray = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Form1));
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            tabControl1 = new TabControl();
            tabPage6 = new TabPage();
            panel6 = new Panel();
            richTextBox1 = new RichTextBox();
            tabPage1 = new TabPage();
            panel1 = new Panel();
            tabPage8 = new TabPage();
            panel7 = new Panel();
            tabPage2 = new TabPage();
            panel2 = new Panel();
            tabPage3 = new TabPage();
            panel3 = new Panel();
            tabPage4 = new TabPage();
            panel4 = new Panel();
            tabPage5 = new TabPage();
            panel5 = new Panel();
            tabPage7 = new TabPage();
            textBox12 = new TextBox();
            button3 = new Button();
            groupBox3 = new GroupBox();
            textBox7 = new TextBox();
            label10 = new Label();
            comboBox1 = new ComboBox();
            textBox11 = new TextBox();
            label9 = new Label();
            textBox10 = new TextBox();
            label8 = new Label();
            textBox9 = new TextBox();
            label7 = new Label();
            button2 = new Button();
            textBox15 = new TextBox();
            label18 = new Label();
            textBox17 = new TextBox();
            label21 = new Label();
            checkBox2 = new CheckBox();
            textBox18 = new TextBox();
            label22 = new Label();
            checkBox1 = new CheckBox();
            groupBox2 = new GroupBox();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            groupBox1 = new GroupBox();
            button1 = new Button();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label2 = new Label();
            label1 = new Label();
            tabPage9 = new TabPage();
            dataGridView1 = new DataGridView();
            快递编号 = new DataGridViewTextBoxColumn();
            快递名称 = new DataGridViewTextBoxColumn();
            tabPage10 = new TabPage();
            panel8 = new Panel();
            tabPage11 = new TabPage();
            panel9 = new Panel();
            tabPage12 = new TabPage();
            panel10 = new Panel();
            tabPage13 = new TabPage();
            panel11 = new Panel();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            linkLabel1 = new LinkLabel();
            statusStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage6.SuspendLayout();
            panel6.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage8.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            tabPage5.SuspendLayout();
            tabPage7.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage9.SuspendLayout();
            ((ISupportInitialize)dataGridView1).BeginInit();
            tabPage10.SuspendLayout();
            tabPage11.SuspendLayout();
            tabPage12.SuspendLayout();
            tabPage13.SuspendLayout();
            SuspendLayout();
            statusStrip1.Items.AddRange(new ToolStripItem[1]
            {
        toolStripStatusLabel1
            });
            statusStrip1.Location = new Point(0, 603);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1164, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage8);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage7);
            tabControl1.Controls.Add(tabPage9);
            tabControl1.Controls.Add(tabPage10);
            tabControl1.Controls.Add(tabPage11);
            tabControl1.Controls.Add(tabPage12);
            tabControl1.Controls.Add(tabPage13);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1164, 603);
            tabControl1.TabIndex = 3;
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
            tabPage6.Controls.Add(panel6);
            tabPage6.Location = new Point(4, 22);
            tabPage6.Name = "tabPage6";
            tabPage6.Size = new Size(1156, 577);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "程序说明";
            tabPage6.UseVisualStyleBackColor = true;
            panel6.BackColor = Color.Transparent;
            panel6.Controls.Add(richTextBox1);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(1156, 577);
            panel6.TabIndex = 0;
            richTextBox1.BackColor = Color.Black;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Font = new Font("楷体", 15f, FontStyle.Bold, GraphicsUnit.Point, 134);
            richTextBox1.ForeColor = Color.Red;
            richTextBox1.Location = new Point(0, 0);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(1156, 577);
            richTextBox1.TabIndex = 0;
            tabPage1.BackColor = Color.LightGray;
            tabPage1.Controls.Add(panel1);
            tabPage1.Location = new Point(4, 22);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1156, 577);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "账号注册";
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1150, 571);
            panel1.TabIndex = 0;
            tabPage8.Controls.Add(panel7);
            tabPage8.Location = new Point(4, 22);
            tabPage8.Name = "tabPage8";
            tabPage8.Size = new Size(1156, 577);
            tabPage8.TabIndex = 7;
            tabPage8.Text = "设置密码";
            tabPage8.UseVisualStyleBackColor = true;
            panel7.Dock = DockStyle.Fill;
            panel7.Location = new Point(0, 0);
            panel7.Name = "panel7";
            panel7.Size = new Size(1156, 577);
            panel7.TabIndex = 0;
            tabPage2.BackColor = Color.RosyBrown;
            tabPage2.Controls.Add(panel2);
            tabPage2.Location = new Point(4, 22);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1156, 577);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "刷单";
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(1150, 571);
            panel2.TabIndex = 0;
            tabPage3.BackColor = Color.Coral;
            tabPage3.Controls.Add(panel3);
            tabPage3.Location = new Point(4, 22);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1156, 577);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "支付";
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(3, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(1150, 571);
            panel3.TabIndex = 0;
            tabPage4.BackColor = Color.SandyBrown;
            tabPage4.Controls.Add(panel4);
            tabPage4.Location = new Point(4, 22);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1156, 577);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "核销";
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(1150, 571);
            panel4.TabIndex = 0;
            tabPage5.BackColor = Color.DimGray;
            tabPage5.Controls.Add(panel5);
            tabPage5.Location = new Point(4, 22);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(1156, 577);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "评论";
            panel5.BackColor = SystemColors.InactiveCaption;
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(3, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(1150, 571);
            panel5.TabIndex = 0;
            tabPage7.Controls.Add(textBox12);
            tabPage7.Controls.Add(button3);
            tabPage7.Controls.Add(groupBox3);
            tabPage7.Controls.Add(groupBox2);
            tabPage7.Controls.Add(groupBox1);
            tabPage7.Location = new Point(4, 22);
            tabPage7.Name = "tabPage7";
            tabPage7.Size = new Size(1156, 577);
            tabPage7.TabIndex = 6;
            tabPage7.Text = "系统设置";
            tabPage7.UseVisualStyleBackColor = true;
            textBox12.BackColor = Color.Black;
            textBox12.Font = new Font("宋体", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
            textBox12.ForeColor = Color.Red;
            textBox12.Location = new Point(115, 285);
            textBox12.Multiline = true;
            textBox12.Name = "textBox12";
            textBox12.ReadOnly = true;
            textBox12.ScrollBars = ScrollBars.Vertical;
            textBox12.Size = new Size(704, 256);
            textBox12.TabIndex = 102;
            button3.Font = new Font("宋体", 15f, FontStyle.Bold, GraphicsUnit.Point, 134);
            button3.ForeColor = Color.Red;
            button3.Location = new Point(8, 150);
            button3.Name = "button3";
            button3.Size = new Size(406, 129);
            button3.TabIndex = 101;
            button3.Text = "保存设置";
            button3.UseVisualStyleBackColor = true;
            button3.Click += new EventHandler(button3_Click);
            groupBox3.Controls.Add(linkLabel1);
            groupBox3.Controls.Add(textBox7);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(comboBox1);
            groupBox3.Controls.Add(textBox11);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(textBox10);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(textBox9);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(button2);
            groupBox3.Controls.Add(textBox15);
            groupBox3.Controls.Add(label18);
            groupBox3.Controls.Add(textBox17);
            groupBox3.Controls.Add(label21);
            groupBox3.Controls.Add(checkBox2);
            groupBox3.Controls.Add(textBox18);
            groupBox3.Controls.Add(label22);
            groupBox3.Controls.Add(checkBox1);
            groupBox3.Location = new Point(420, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(301, 276);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "公共设置";
            groupBox3.Enter += new EventHandler(groupBox3_Enter);
            textBox7.Location = new Point(178, 75);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(85, 21);
            textBox7.TabIndex = 104;
            label10.AutoSize = true;
            label10.Location = new Point(120, 79);
            label10.Name = "label10";
            label10.Size = new Size(53, 12);
            label10.TabIndex = 105;
            label10.Text = "下单数量";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[1]
            {
         "请选择店铺下单数据"
            });
            comboBox1.Location = new Point(80, 244);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(183, 20);
            comboBox1.TabIndex = 103;
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            textBox11.Location = new Point(80, 218);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(183, 21);
            textBox11.TabIndex = 15;
            label9.AutoSize = true;
            label9.Location = new Point(21, 222);
            label9.Name = "label9";
            label9.Size = new Size(53, 12);
            label9.TabIndex = 45;
            label9.Text = "身份六位";
            textBox10.Location = new Point(80, 191);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(183, 21);
            textBox10.TabIndex = 14;
            label8.AutoSize = true;
            label8.Location = new Point(21, 195);
            label8.Name = "label8";
            label8.Size = new Size(53, 12);
            label8.TabIndex = 43;
            label8.Text = "支付密码";
            textBox9.Location = new Point(80, 164);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(183, 21);
            textBox9.TabIndex = 13;
            label7.AutoSize = true;
            label7.Location = new Point(21, 168);
            label7.Name = "label7";
            label7.Size = new Size(53, 12);
            label7.TabIndex = 41;
            label7.Text = "核销间隔";
            button2.Location = new Point(80, 135);
            button2.Name = "button2";
            button2.Size = new Size(183, 23);
            button2.TabIndex = 100;
            button2.Text = "扫码登录店铺";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            textBox15.Location = new Point(80, 102);
            textBox15.Name = "textBox15";
            textBox15.Size = new Size(183, 21);
            textBox15.TabIndex = 10;
            label18.AutoSize = true;
            label18.Location = new Point(9, 106);
            label18.Name = "label18";
            label18.Size = new Size(65, 12);
            label18.TabIndex = 14;
            label18.Text = "主店铺登录";
            textBox17.Enabled = false;
            textBox17.Location = new Point(178, 48);
            textBox17.Name = "textBox17";
            textBox17.Size = new Size(85, 21);
            textBox17.TabIndex = 9;
            label21.AutoSize = true;
            label21.Location = new Point(120, 52);
            label21.Name = "label21";
            label21.Size = new Size(53, 12);
            label21.TabIndex = 9;
            label21.Text = "店铺编号";
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(9, 50);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(96, 16);
            checkBox2.TabIndex = 7;
            checkBox2.Text = "启用自动关注";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += new EventHandler(checkBox2_CheckedChanged);
            textBox18.Enabled = false;
            textBox18.Location = new Point(179, 16);
            textBox18.Name = "textBox18";
            textBox18.Size = new Size(84, 21);
            textBox18.TabIndex = 8;
            label22.AutoSize = true;
            label22.Location = new Point(120, 20);
            label22.Name = "label22";
            label22.Size = new Size(53, 12);
            label22.TabIndex = 6;
            label22.Text = "改价价格";
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(9, 18);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(96, 16);
            checkBox1.TabIndex = 6;
            checkBox1.Text = "启用自动改价";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);
            groupBox2.Controls.Add(textBox5);
            groupBox2.Controls.Add(textBox6);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(textBox3);
            groupBox2.Controls.Add(textBox4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label4);
            groupBox2.Location = new Point(214, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 141);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "代理平台";
            textBox5.Location = new Point(42, 103);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(122, 21);
            textBox5.TabIndex = 5;
            textBox6.Location = new Point(42, 74);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(122, 21);
            textBox6.TabIndex = 4;
            label5.AutoSize = true;
            label5.Location = new Point(6, 107);
            label5.Name = "label5";
            label5.Size = new Size(29, 12);
            label5.TabIndex = 10;
            label5.Text = "pass";
            label6.AutoSize = true;
            label6.Location = new Point(7, 78);
            label6.Name = "label6";
            label6.Size = new Size(35, 12);
            label6.TabIndex = 9;
            label6.Text = "uname";
            textBox3.Location = new Point(42, 47);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(122, 21);
            textBox3.TabIndex = 3;
            textBox4.Location = new Point(42, 18);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(122, 21);
            textBox4.TabIndex = 2;
            label3.AutoSize = true;
            label3.Location = new Point(6, 51);
            label3.Name = "label3";
            label3.Size = new Size(35, 12);
            label3.TabIndex = 1;
            label3.Text = "proxy";
            label4.AutoSize = true;
            label4.Location = new Point(7, 22);
            label4.Name = "label4";
            label4.Size = new Size(29, 12);
            label4.TabIndex = 0;
            label4.Text = "host";
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(8, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 141);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "接码平台";
            groupBox1.Visible = false;
            button1.Location = new Point(42, 82);
            button1.Name = "button1";
            button1.Size = new Size(122, 23);
            button1.TabIndex = 99;
            button1.Text = "登录";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            textBox2.Location = new Point(42, 55);
            textBox2.Name = "textBox2";
            textBox2.PasswordChar = '*';
            textBox2.Size = new Size(122, 21);
            textBox2.TabIndex = 1;
            textBox1.Location = new Point(42, 18);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(122, 21);
            textBox1.TabIndex = 0;
            label2.AutoSize = true;
            label2.Location = new Point(6, 59);
            label2.Name = "label2";
            label2.Size = new Size(29, 12);
            label2.TabIndex = 1;
            label2.Text = "密码";
            label1.AutoSize = true;
            label1.Location = new Point(7, 22);
            label1.Name = "label1";
            label1.Size = new Size(29, 12);
            label1.TabIndex = 0;
            label1.Text = "账号";
            tabPage9.Controls.Add(dataGridView1);
            tabPage9.Location = new Point(4, 22);
            tabPage9.Name = "tabPage9";
            tabPage9.Size = new Size(1156, 577);
            tabPage9.TabIndex = 8;
            tabPage9.Text = "快递一览表";
            tabPage9.UseVisualStyleBackColor = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(快递编号, 快递名称);
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Margin = new Padding(2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Size = new Size(1156, 577);
            dataGridView1.TabIndex = 57;
            快递编号.DataPropertyName = "快递编号";
            快递编号.FillWeight = 10f;
            快递编号.HeaderText = "快递编号";
            快递编号.Name = "快递编号";
            快递名称.DataPropertyName = "快递名称";
            快递名称.FillWeight = 10f;
            快递名称.HeaderText = "快递名称";
            快递名称.Name = "快递名称";
            tabPage10.Controls.Add(panel8);
            tabPage10.Location = new Point(4, 22);
            tabPage10.Name = "tabPage10";
            tabPage10.Size = new Size(1156, 577);
            tabPage10.TabIndex = 9;
            tabPage10.Text = "速刷模式";
            tabPage10.UseVisualStyleBackColor = true;
            panel8.Dock = DockStyle.Fill;
            panel8.Location = new Point(0, 0);
            panel8.Name = "panel8";
            panel8.Size = new Size(1156, 577);
            panel8.TabIndex = 0;
            tabPage11.Controls.Add(panel9);
            tabPage11.Location = new Point(4, 22);
            tabPage11.Name = "tabPage11";
            tabPage11.Padding = new Padding(3);
            tabPage11.Size = new Size(1156, 577);
            tabPage11.TabIndex = 10;
            tabPage11.Text = "图片加水印";
            tabPage11.UseVisualStyleBackColor = true;
            panel9.Dock = DockStyle.Fill;
            panel9.Location = new Point(3, 3);
            panel9.Name = "panel9";
            panel9.Size = new Size(1150, 571);
            panel9.TabIndex = 1;
            tabPage12.Controls.Add(panel10);
            tabPage12.Location = new Point(4, 22);
            tabPage12.Name = "tabPage12";
            tabPage12.Padding = new Padding(3);
            tabPage12.Size = new Size(1156, 577);
            tabPage12.TabIndex = 11;
            tabPage12.Text = "采集评论";
            tabPage12.UseVisualStyleBackColor = true;
            panel10.Dock = DockStyle.Fill;
            panel10.Location = new Point(3, 3);
            panel10.Name = "panel10";
            panel10.Size = new Size(1150, 571);
            panel10.TabIndex = 2;
            tabPage13.Controls.Add(panel11);
            tabPage13.Location = new Point(4, 22);
            tabPage13.Name = "tabPage13";
            tabPage13.Size = new Size(1156, 577);
            tabPage13.TabIndex = 12;
            tabPage13.Text = "一键复制上架";
            tabPage13.UseVisualStyleBackColor = true;
            panel11.Dock = DockStyle.Fill;
            panel11.Location = new Point(0, 0);
            panel11.Name = "panel11";
            panel11.Size = new Size(1156, 577);
            panel11.TabIndex = 3;
            dataGridViewTextBoxColumn1.DataPropertyName = "快递编号";
            dataGridViewTextBoxColumn1.FillWeight = 10f;
            dataGridViewTextBoxColumn1.HeaderText = "快递编号";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 577;
            dataGridViewTextBoxColumn2.DataPropertyName = "快递名称";
            dataGridViewTextBoxColumn2.FillWeight = 10f;
            dataGridViewTextBoxColumn2.HeaderText = "快递名称";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Width = 576;
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(9, 248);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(65, 12);
            linkLabel1.TabIndex = 106;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "获取自提点";
            linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1164, 625);
            Controls.Add(tabControl1);
            Controls.Add(statusStrip1);
            Name = "微店";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "微店";
            Load += new EventHandler(Form1_Load);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage6.ResumeLayout(false);
            panel6.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage8.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            tabPage7.ResumeLayout(false);
            tabPage7.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage9.ResumeLayout(false);
            ((ISupportInitialize)dataGridView1).EndInit();
            tabPage10.ResumeLayout(false);
            tabPage11.ResumeLayout(false);
            tabPage12.ResumeLayout(false);
            tabPage13.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private delegate void UpdateDataGridView(DataTable dt);

        public delegate void SetTextHandler(string text);
    }
}
