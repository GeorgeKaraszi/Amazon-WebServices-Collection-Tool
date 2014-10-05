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
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EconServices
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AmazonItemGatheringGUI());
        }
    }
}
