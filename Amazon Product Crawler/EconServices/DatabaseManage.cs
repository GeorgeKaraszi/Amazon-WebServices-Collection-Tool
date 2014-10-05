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
 * Class:
 * 
 * DatabaseManage - A Class that handles all database connections and its 
 *		modifications.
 *		
 * Functions:
 * 
 * OpenConnection() - Opens connection to the database
 * CloseConnection() - Closes connection to the database
 * CleanString(..) - Cleans input strings that are heading to the databases 
 * Insert(...) - Inserts new tuples into the SQL database
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EconServices
{
	internal class DatabaseManage
	{
		private MySqlConnection connection;
		private string _server;
		private string _database;
		private string _dbIP;
		private string _dbUserName;
		private string _dbPassword;
		private string connectionstring;


		//------------------------------------------------------------------------------------
		/// <summary>
		/// Initial start of the database connection
		/// </summary>
		public DatabaseManage()
		{
			_server = "localhost";
			_database = "randomdb";
			_dbUserName = "root";
			_dbPassword = "root";
			connectionstring = "SERVER=" + _server + ";" + "DATABASE=" + _database +
			               ";" + "UID=" + _dbUserName + ";" + "PASSWORD=" + _dbPassword + ";";

			connection = new MySqlConnection(connectionstring);
		}

		//------------------------------------------------------------------------------------
		/// <summary>
		/// Opens connection to the database
		/// </summary>
		/// <returns>Connection status</returns>
		private bool OpenConnection()
		{
			try
			{
				connection.Open();
				return true;
			}
			catch (MySqlException ex)
			{

				switch (ex.Number)
				{
					case 0:
						MessageBox.Show("Cannot connect to server.");
						break;

					case 1045:
						MessageBox.Show("Invalid username/password, please try again");
						break;
				}

				return false;
			}
		}

		//------------------------------------------------------------------------------------
		/// <summary>
		/// Close connection to the server
		/// </summary>
		/// <returns></returns>
		private bool CloseConnection()
		{
			connection.Close();
			return true;
		}

		//------------------------------------------------------------------------------------
		/// <summary>
		/// Cleans up input strings before being added to the database. Usually its 
		/// a result of HTML code from the description box that Amazon provides.
		/// </summary>
		/// <param name="value">Cleaned string</param>
		public void CleanString(ref string value)
		{
			value = value.Replace('\'', ' ');
		}

		//------------------------------------------------------------------------------------
		/// <summary>
		/// Inserts into the database
		/// </summary>
		/// <param name="asin">ID of the product</param>
		/// <param name="des">Description</param>
		/// <param name="dim">Size of the product</param>
		/// <param name="price">Cost of the product</param>
		/// <param name="name">Name of the product</param>
		/// <param name="imageUrl">URL to the products image</param>
		/// <param name="destUrl">URL to the products whereabouts in amazon</param>
		public void Insert(string asin, string des, string dim, decimal price, 
			string name, string imageUrl, string destUrl)
		{
			string query;
			
			CleanString(ref des);
			CleanString(ref name);

			query = string.Format("INSERT INTO randomdb.product VALUES(" +
								"'{0}','{1}','{2}',{3},'{4}','{5}','{6}')", 
							asin, name, des, price, dim, imageUrl, destUrl);

			if (this.OpenConnection())
			{
				MySqlCommand cmd = new MySqlCommand(query, connection);
				try
				{
					cmd.ExecuteNonQuery();
				}
				catch (MySqlException ex)
				{

					this.CloseConnection();
					return;
				}

				this.CloseConnection();
			}



		}
	}
}
