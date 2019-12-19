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
        public static int NAgent = 1000;

        public static Random r = new Random(DateTime.Now.Millisecond);

        public static List<Company> market = new List<Company>();
        
        public Form1()
        {
            InitializeComponent();

            chart1.ChartAreas[0].AxisY.Maximum = 100;
            chart1.ChartAreas[0].AxisY.Minimum = 0;

            for (int i = 0; i < NCompany; i++)
            {
                Company c = new Company();
                c.index = market.Count();       //needed to remove from 
                market.Add(c);
            }

            for (int i=0;i<NAgent;i++)
            {
                Agent a = new Agent() { };
                //everyone works
                int idx = r.Next(market.Count() - 1);
                market[idx].staff.Add(a);
            }

            //timer1.Start();

        }

        bool processing = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (processing) return;

            processing = true;

            market.ForEach(c => c.Period(market));

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

            bins.ToList().ForEach(v =>
                chart1.Series[0].Points.AddY( v )
            );

            processing = false;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            timer1.Start();
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

        public void Period(List<Company> market)
        {
            double dividend = cash * dividendperc;
            double salary = cash - dividend;

            double dividendea = investors.Count() == 0 ? 0 : dividend / investors.Count();
            double salaryea = staff.Count() == 0 ? 0 : salary / staff.Count();

            //pay
            investors.ForEach(i => i.cash += dividendea );
            staff.ForEach(i => i.cash += salaryea );

            //work
            staff.ForEach(i => this.cash += i.work);

            //buy shares
            staff.ForEach(i => i.BuyShares(market) );

        }

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

    }

    public class Agent
    {
        public double cash = 0;
        public double work = 1;
        public List<Company> investments = new List<Company>();

        public double Worth()
        {
            double total = cash;
            investments.ForEach(i => total += i.cost);
            return total;
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
                    Company.RemoveShareholder(investments[0], this);

                    cash -= companies[idx].cost;               //buy
                    Company.AddShareholder(companies[idx], this);
                }
            }

            //while (true)
            //{
                idx = Form1.r.Next(Form1.NCompany);
                //unlucky you picked a stock you can't afford
                if (companies[idx].cost > cash)
                    
                //break;

                Company.AddShareholder(companies[idx], this);

                cash -= companies[idx].cost;
            //}

        }

    }
}
