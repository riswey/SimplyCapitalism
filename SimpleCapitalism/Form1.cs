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
        public static int NCompany = 40;
        public static int NAgent = 2000;

        public static Random r = new Random(DateTime.Now.Millisecond);

        public static List<Company> market = new List<Company>();
        public static List<Agent> agents = new List<Agent>();

        public static void AddShareholder(Company c, Agent a)
        {
            //Add agent to company stock holders
            c.investors.Add(a);
            //Add company agent inventments
            a.investments.Add(c);
        }

        public static void RemoveShareholder(Company c, Agent a)
        {
            //Remove agent to company stock holders
            c.investors.Remove(a);
            //Remove company agent inventments
            a.investments.Remove(c);
        }

        public static double AgentStarved(Agent a)
        {
            //remove from staff
            a.employer.staff.Remove(a);
            //remove shareholdings
            double valueestate = 0; 
            a.investments.ForEach(i => {
                valueestate += i.cost;
                RemoveShareholder(i, a);
            });
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

            for (int i = 0; i < NCompany; i++)
            {
                Company c = new Company();
                c.index = market.Count();       //needed to remove from 
                market.Add(c);
            }

            for (int i=0;i<NAgent;i++)
            {
                int idx = r.Next(market.Count() - 1);
                Agent a = new Agent() {
                    employer = market[idx],
                    cash = 10
                };
                //everyone has an employer (even capitalists)
                market[idx].staff.Add(a);
                agents.Add(a);
            }

            timer1.Start();

        }

        bool processing = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (processing) return;

            processing = true;

            Tick();

            //if (InvokeRequired)
           // {
             //   this.Invoke(new Action(() => Tick() ));
                //processing = false;
            //}

        }

        void Tick() {
            //Do company accounts (pay dividends and salaries)
            market.ForEach(c => c.Accounts(market));

            //buy shares with salaries
            agents.ForEach(i => i.BuyShares(market));


            //Subsistence
            //market.ForEach(c => c.Work() );

            /* Total cash spent on subsistence this round
             * A company cannot extract this frpom its own staff, it sells to everyone else
             * Avoid complexity of this by assuming everyone roughly shoulders the cost
             */
             
            double liquidity = 0, sales;
            agents.ForEach(a =>
            {
                if ((sales = a.PayForSubsistence()) < 0)
                {
                    //Agent Starved
                    liquidity += AgentStarved(a);
                    liquidity -= 1 - sales;     // - 1-cash (is amount they had before --)
                }
                else
                {
                    liquidity += sales;
                }
            });

            //Companies would have recieved this in proportion to what they sold which is proportion of employees.
            double liquidityperagent = liquidity / agents.Count();

            market.ForEach(c => c.cash += liquidityperagent * c.staff.Count());
            //this replaces the work step in previous model.

            //Metrics
            double min = 1E8;
            double max = 0;

            market.ForEach(c => c.staff.ForEach(s =>
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
            market.ForEach(c => c.staff.ForEach(s =>
            {
                int val = (int)Math.Round((s.Worth() - min)/binsize, 0);
                bins[val]++;
            }));

            //Display
            chart1.Series[0].Points.Clear();
            //chart1.ChartAreas[0].AxisX.Minimum = min;
            //chart1.ChartAreas[0].AxisX.Maximum = max;

            for (int i=0;i<bins.Length;i++)
            {
                chart1.Series[0].Points.AddY(bins[i]);
            }

            processing = false;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //timer1.Start();
        }
    }

    public class Company
    {
        public int index;
        public static double salary = 1;                   //days work produces 1 currency
        public static double SubsistenceLevel = 0.9;       //90% spent on subsisting, 10% invested
        public static double dividendperc = 0.01f;         //1% paid to investors, 99% to staff

        public List<Agent> staff = new List<Agent>();
        public List<Agent> investors = new List<Agent>();
        public double cost = 1;

        public double cash = 0;

        public void Accounts(List<Company> market)
        {
            double dividend = cash * dividendperc;
            double salary = cash - dividend;

            double dividendea = investors.Count() == 0 ? 0 : dividend / investors.Count();
            double salaryea = staff.Count() == 0 ? 0 : salary / staff.Count();

            //pay
            investors.ForEach(i => i.cash += dividendea );
            staff.ForEach(i => i.cash += salaryea );

        }

        public void Work()
        {
            //If staff generate work and this is able to be sold at 1
            staff.ForEach(i => this.cash += i.work);
            //In reality the work must be sold, which means someone must cough up. So deduct from the population,
            //This is farming, so no food = dead.
            //Sales is a relationship with rest of population, so this must be done
            //outside the company. It is now a global function.
        }

    }

    public class Agent
    {
        public double cash = 0;
        public double work = 1;
        public List<Company> investments = new List<Company>();
        public Company employer = null;

        public double Worth()
        {
            double total = cash;
            investments.ForEach(i => total += i.cost);
            return total;
        }

        //This provides the liquidity for the next round.
        //An agent may die if they can't afford subsistence.
        //Previous model took subsistence from wages.
        public double PayForSubsistence()
        {
            --cash;
            return (cash < 0) ? cash : 1;
        }

        public void BuyShares(List<Company> companies) {
            int idx;
            //Move a share (if can afford)
            if (investments.Count > 0)
            {
                double tempcash = cash;
                idx = Form1.r.Next(Form1.NCompany);
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
            //int count = 0;
            //while (cash > 5 && count++ < 2)
            //{
                idx = Form1.r.Next(Form1.NCompany);
                //unlucky you picked a stock you can't afford
                if (companies[idx].cost > cash)         //need to pay for subsistence
                    return;
                Form1.AddShareholder(companies[idx], this);
                cash -= companies[idx].cost;
            //}

        }

    }
}
