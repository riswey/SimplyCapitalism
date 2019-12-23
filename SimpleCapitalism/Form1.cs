using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


/*
 * Turned off buyingshares
 * There is a leak of money. Without capitalism people spend and salary in equal measure. Investigate the leak
 * look at market worth and liquidity vs income 
 * 
 */


/* Companies hold Agents. Randomly assigned.
 * Agents are milked to produce labour 1ce a round.
 * This is divided into dividend and salary and evenly distributed between investors, workers respectively.
 * Agents can swap an investment each round.
 * They convert cash into investments each round.
 * 
 */

namespace SimpleCapitalism
{
    public partial class Form1 : Form
    {
        //public static int NCompany = 40;
        //public static int NAgent = 2000;

        public static Random r = new Random(DateTime.Now.Millisecond);

        public static List<Company> companies;
        public static List<Agent> agents;

        public static double AddShareholder(Company c, Agent a)
        {
            //Add agent to company stock holders
            c.investors.Add(a);
            //Add company agent inventments
            a.investments.Add(c);

            return -c.cost;
        }

        public static double RemoveShareholder(Company c, Agent a)
        {
            //Remove agent to company stock holders
            c.investors.Remove(a);
            //Remove company agent inventments
            a.investments.Remove(c);

            return c.cost;
        }

        public static double AgentStarved(Agent a)
        {
            //actually we should sell stock and only be removed when 0 stock!
            //also could borrow from a company, and pay %of debt. this is how money growth really created!
            double valueestate = a.cash;


            //remove from staff
            a.employer.staff.Remove(a);
            //remove shareholdings
            foreach(Company c in a.investments.ToList() )
            {
                valueestate += RemoveShareholder(c, a);
            }

            //decease agent
            agents.Remove(a);
            return valueestate;
        }

        public Form1()
        {
            InitializeComponent();

            chart1.ChartAreas[0].AxisY.Maximum = 100;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

            chart2.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chart2.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

        }

        void Init() {

            companies = new List<Company>();
            agents = new List<Agent>();

            for (int i = 0; i < numCompany.Value; i++)
            {
                Company c = new Company() {};
                c.index = companies.Count();       //needed to remove from 
                companies.Add(c);
            }

            for (int i=0;i<numAgent.Value;i++)
            {
                int idx = r.Next(companies.Count() - 1);

                double initCash = (double)numStartCash.Value;
                if (chkRandom.Checked) { initCash *= r.NextDouble(); }
                  
                Agent a = new Agent() {
                    employer = companies[idx],
                    cash = initCash,
                };
                //everyone has an employer (even capitalists)
                companies[idx].staff.Add(a);
                agents.Add(a);
            }

            for(int i=0; i<numCapitalists.Value;i++)
            {
                agents[i].capitalist = true;
                //give capitalists a 10x head start rather than stopping non-capitalists trading
                if (tentimescStart.Checked)
                    agents[i].cash *= 2;
            }
        }

        bool processing = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (processing) return;

            processing = true;

            txtPopulation.Text = agents.Count().ToString();

            Tick();

        }

        void Tick() {
            double mworth = agents.Sum(a => a.Worth());
            mworth += companies.Sum(c => c.cash);

            marketCap.Text = Math.Round(mworth,1).ToString();
            
            //Do company accounts (pay dividends and salaries)
            companies.ForEach(c => c.Accounts(companies));

            //buy shares with salaries
            agents.ForEach(i => i.BuyShares(companies, tentimescStart.Checked));

            
            //Subsistence
            //market.ForEach(c => c.Work() );

            /* Total cash spent on subsistence this round
             * A company cannot extract this frpom its own staff, it sells to everyone else
             * Avoid complexity of this by assuming everyone roughly shoulders the cost
             */

            if (agents.Count() == 0) return;

            double liquidity = 0, sales;
            foreach(Agent a in agents.ToList())
            {
                sales = a.PayForSubsistence(numGrowth.Value);
                if (sales == 0)
                {
                    //Agent Starved
                    liquidity += AgentStarved(a);
                }
                else
                {
                    liquidity += sales;
                }
            }

            //Companies would have recieved this in proportion to what they sold which is proportion of employees.
            double liquidityperagent = liquidity / agents.Count();

            double total = 0;
            companies.ForEach(c =>
            {
                c.cash += liquidityperagent * c.staff.Count();

                total += liquidityperagent * c.staff.Count();
            });
            //this replaces the work step in previous model.
            //Debug.WriteLine("Tot Liquid: " + total);


            //Metrics (rest of Tick)

            double min = 1E8;
            double max = 0;

            companies.ForEach(c => c.staff.ForEach(s =>
            {
                min = Math.Min(min, s.Worth() );
                max = Math.Max(max, s.Worth() );
            }));

            
            if (max - min < 100)
            {
                max = min + 100;
            }

            double binsize = (max - min) / 100;

            int[] bins = new int[101];
            companies.ForEach(c => c.staff.ForEach(s =>
            {
                int val = (int)Math.Round((s.Worth() - min)/binsize, 0);
                bins[val]++;
            }));

            //Pareto
            agents.Sort((a, b) => a.Worth().CompareTo( b.Worth() ) );

            //Display
            chart1.Series[0].Points.Clear();
            //chart1.ChartAreas[0].AxisX.Minimum = min;
            //chart1.ChartAreas[0].AxisX.Maximum = max;

            for (int i=0;i<bins.Length;i++)
            {
                chart1.Series[0].Points.AddY(bins[i]);
            }

            chart2.Series[0].Points.Clear();

            agents.ForEach(a => chart2.Series[0].Points.AddY(a.Worth()) );

            //if (agents.Count() > 0)
            //{
            //    chart2.Series[0].Points.AddY(agents[0].Worth());
            //    chart2.Series[0].Points.AddY(agents[agents.Count() - 1].Worth());
            //}

            processing = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Company.subsistence_level = (double)subsistencelevel.Value;
            Company.dividend_perc = (double)dividendperc.Value;         //1% paid to investors, 99% to staff

            Init();

            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }

    public class Company
    {
        //public static double salary = 1;          //days work produces 1 currency
        public static double subsistence_level = 0.9;      //90% spent on subsisting, 10% invested
        public static double dividend_perc = 0.01;         //1% paid to investors, 99% to staff

        public int index;

        public List<Agent> staff = new List<Agent>();
        public List<Agent> investors = new List<Agent>();
        public double cost = 1;

        public double cash = 0;

        public void Accounts(List<Company> market)
        {
            double dividend = cash * dividend_perc;
            double dividendea = investors.Count() == 0 ? 0 : dividend / investors.Count();

            double salary;
            if (dividendea == 0) //no dividend was paid put back in cash
                salary = cash;
            else
                salary = cash - dividend;

            cash = 0;

            double salaryea = staff.Count() == 0 ? 0 : salary / staff.Count();

            //pay
            investors.ForEach(i => i.cash += dividendea );
            staff.ForEach(i => i.cash += salaryea );

        }

        /*
        public void Work()
        {
            //If staff generate work and this is able to be sold at 1
            staff.ForEach(i => this.cash += i.work);
            //In reality the work must be sold, which means someone must cough up. So deduct from the population,
            //This is farming, so no food = dead.
            //Sales is a relationship with rest of population, so this must be done
            //outside the company. It is now a global function.
        }
        */

    }

    public class Agent
    {
        public bool capitalist = false;
        private double _cash = 0;
        public double cash {
            get
            {
                return _cash;
            }
            set
            {
                dirty = true;
                _cash = value;
            }
        }

        //public double work = 1;
        public List<Company> investments = new List<Company>();
        //when you buy an asset you give cash so no change in worth (dirty unchanged)
        public Company employer = null;
        bool dirty = true;
        double worthcache = 0;

        public double Worth()
        {
            if (dirty == false) return worthcache;

            worthcache = cash;
            investments.ForEach(i => worthcache += i.cost);
            dirty = false;
            return worthcache;
        }

        //This provides the liquidity for the next round.
        //An agent may die if they can't afford subsistence.
        //Previous model took subsistence from wages.
        public double PayForSubsistence(decimal growth)
        {
            //no growth then all salary come from sales.
            //if all growth then salary is new printed cash
            cash -= (1 - (double)growth);
            if (cash < 0)
            {
                //can't pay
                cash += (1 - (double)growth);
                return 0;
            }

            return 1;
        }

        public void BuyShares(List<Company> companies, bool tentimesStartState) {

            //tentimesStart state changes behaviour to just give capitalists a 10x head start rather than exclude non-capitalists

            if (!capitalist && !tentimesStartState) return;

            int idx;

            //Move a share (if can afford)
            if (investments.Count > 0)
            {
                double tempcash = cash;
                idx = Form1.r.Next( companies.Count()-1 );
                tempcash += investments[0].cost;
                if (tempcash >= companies[idx].cost)
                {
                    //can afford
                    cash += investments[0].cost;            //sell
                    Form1.RemoveShareholder(investments[0], this);

                    cash -= companies[idx].cost;               //buy
                    Form1.AddShareholder(companies[idx], this);
                }
            }

            //OPTIMISE THIS \/

            //too slow if put all cash into shares
            int count = 0;
            while (cash > 5 && count++ < 100)
            {
                idx = Form1.r.Next( companies.Count );
                //unlucky you picked a stock you can't afford
                if (companies[idx].cost > cash)         //need to pay for subsistence
                    return;
                cash += Form1.AddShareholder(companies[idx], this);
            }

        }

    }
}
