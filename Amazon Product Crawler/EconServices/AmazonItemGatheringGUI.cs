/*Author:George Karaszi
 * 
 * Description:Using Amazons web services, the program here takes a ASIN 
 *	(Amazon's unique ID for all its products) and gathers all its information 
 *	that Amazon allows to be given out. The main strength of this program comes from 
 *	being able to craw through similar items guaranteeing that each similar item is 
 *	unique. Thus allowing a database of our own creation to be filled with products 
 *	that amazon is currently selling. This is however limited to actual physical 
 *	products and not that of digital form.
 *	
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKCraddock.AmazonItemLookup;

namespace EconServices
{
    public partial class AmazonItemGatheringGUI : Form
    {
        private BackgroundWorker bw;
        private AmazonItemHandler _amazonItems;
        public List<AwsItem> _itemList = new List<AwsItem>();


        public AmazonItemGatheringGUI()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------

        private void AmazonItemGatheringGUI_Load(object sender, EventArgs e)
        {
            bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_StartWork);

			//Amazons web service start. Below are service ID's to my account
            _amazonItems = new AmazonItemHandler("AmazonIDGoesHere",
                "rgK8fkdeLRiiaJ0KIQCE0bt7fsN/JkC8rvfLRExg",
                "wmst-20");
        }

        private void startBtn_Click(object sender, EventArgs e)
        {

            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
                startBtn.Enabled = false;
                startBtn.Text = "Working...";
            }

        }

        private void bw_StartWork(object sender, DoWorkEventArgs e)
        {
            int numberOfItems = Int32.Parse(numOfItemsNUD.Text);
            string prodID = productIdTxt.Text;
            DatabaseManage DBM = new DatabaseManage();;
			_itemList.AddRange(_amazonItems.GatherItems(prodID, nonCatCB.Checked, numberOfItems));

            mainLV.Invoke((MethodInvoker)delegate(){mainLV.Items.Clear(); } );
			
            foreach (AwsItem aw in _itemList)
            {
				DBM.Insert(aw.ASIN, aw.Description, 
					aw.ItemAttributes["PackageDimensions"], 
					Convert.ToDecimal(aw.ListPrice),
					aw.ItemAttributes["Title"],
					aw.GetImageUrl(AwsImageType.LargeImage), 
					aw.DetailPageURL);

                string[] row =
                {
                    aw.ASIN, aw.ItemAttributes["Title"],
                    aw.ItemAttributes["ProductGroup"],
                    aw.DetailPageURL,
                    "true"
                };

                if (mainLV.InvokeRequired)
                {
                    mainLV.Invoke((MethodInvoker) delegate()
                    {
                        mainLV.Items.Add(new ListViewItem(row));
                    }
                        );
                }


            }

            if (startBtn.InvokeRequired)
            {
                startBtn.Invoke((MethodInvoker)delegate()
                {
                    startBtn.Enabled = true;
                    startBtn.Text = "Gather Items";
                });
            }
        }
        }
}
