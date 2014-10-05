using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for DatabaseConnection
/// </summary>
public class DatabaseConnection
{
	private MySqlConnection connection;
	private string username;
	private string password;
	private string server;
	private string database;

	public DatabaseConnection()
	{
		username = "root";
		password = "root";
		server = "localhost";
		database = "amazon";
		string connectionstring = "SERVER=" + server + ";" + "DATABASE=" + database +
					   ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";

		connection = new MySqlConnection(connectionstring);

		//
		// TODO: Add constructor logic here
		//
	}

	private bool OpenConnection()
	{
		connection.Open();
		return true;
	}

	private bool CloseConnection()
	{
		connection.Close();
		return true;
	}

	//---------------------------------------------------------------------------------
	//Generates a list of all products to be displayed.

	public List<string>[] GetDefaultBrowserList()
	{
		string query = "select asin, name, img, price from product";
		List<string>[] list = new List<string>[4]; //Generate array list
		list[0] = new List<string>();
		list[1] = new List<string>();
		list[2] = new List<string>();
		list[3] = new List<string>();


		if (OpenConnection()) //Open MYSQL connection
		{
			//Query table
			MySqlCommand cmd = new MySqlCommand(query, connection);
			try
			{
				//Read query results
				MySqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					list[0].Add(reader["asin"].ToString());
					list[1].Add(reader["name"].ToString());
                    list[2].Add(reader["price"].ToString());
                    list[3].Add(reader["img"].ToString());
				}
			}
			catch (MySqlException e)
			{
				return null;
				throw;
			}
			CloseConnection(); //Close Connection
		}
		return list;
	}

	//-------------------------------------------------------------------------------
	//Gets information about a certain product

    public List<string> GetProduct(string asin)
    {
        string query = string.Format("select * from product where asin = '{0}'", asin);
        List<string> product = new List<string>();

        MySqlCommand cmd = new MySqlCommand(query, connection);

		if (OpenConnection()) //Open MYSQL connection
        {
            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                product.Add(reader["asin"].ToString());
                product.Add(reader["name"].ToString());
                product.Add(reader["disc"].ToString());
                product.Add(reader["dim"].ToString());
                product.Add(reader["img"].ToString());
                product.Add(reader["cat"].ToString());
                product.Add(reader["price"].ToString());
                product.Add(reader["pgs"].ToString());
                product.Add(reader["pub"].ToString());
                product.Add(reader["pubdate"].ToString());
                product.Add(reader["dest"].ToString());
            }
            catch (MySqlException e)
            {
                CloseConnection();
                return null;
            }
        }

        CloseConnection();
        return product;
    }


	//-------------------------------------------------------------------------------
	//Gets a list of all cart items that the user wants to buy

    public List<string>[] GetCartItems()
    {
        string query = "select * from cart c inner join product p ON p.asin = c.asin";
        List<string>[] list = new List<string>[5];
		list[0] = new List<string>();
		list[1] = new List<string>();
		list[2] = new List<string>();
		list[3] = new List<string>();
		list[4] = new List<string>();


		if (OpenConnection()) //Open MYSQL connection
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    list[0].Add(reader["name"].ToString());
                    list[1].Add(reader["price"].ToString());
                    list[2].Add(reader["qty"].ToString());
                    list[3].Add(reader["img"].ToString());
                    list[4].Add(reader["asin"].ToString());

                }
            }
            catch (MySqlException e)
            {
                CloseConnection(); //Close MYSQL Connection
                return null;
            }
        }

        CloseConnection(); //Close MYSQL connection
        return list;
    }

	//-------------------------------------------------------------------------------
	//Update the quantity tuple for the given items in the cart
	public void ChangeShoppingQuantity(string asin, string quantity)
	{
		var asinArray = asin.Split(',');
		var quantityArray = quantity.Split(',');
		if (OpenConnection())
		{
			for (int i = 0; i < asinArray.Length; i++)
			{
				string query = 
					string.Format("UPDATE cart set qty={0} where asin='{1}'", 
					quantityArray[i], asinArray[i]);

				MySqlCommand cmd = new MySqlCommand(query, connection);
				try
				{
					cmd.ExecuteNonQuery();
				}
				catch (MySqlException e)
				{

				}
			}
		}

		CloseConnection();
	}

	//-------------------------------------------------------------------------------
	//Add item to the cart to be processed

	public void AddToCart(string asin)
	{
		string query = string.Format("INSERT INTO cart Values('{0}', 1)", asin);
		if (OpenConnection())
		{
			MySqlCommand cmd = new MySqlCommand(query, connection);
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (MySqlException e)
			{

			}
			CloseConnection();
		}
	}

	//-------------------------------------------------------------------------------
	//Delete tuple from cart if quantity is 0
	public void DeleteFromCart(string asin, string quantity)
	{
		var asinArray = asin.Split(',');
		var quantityArray = quantity.Split(',');

		if (OpenConnection())
		{
			for (int i = 0; i < asinArray.Length; i++)
			{
				string query = string.Format("delete from cart where asin = '{0}'",
					asinArray[i]);
				if (quantityArray[i] == "0")
				{
					MySqlCommand cmd = new MySqlCommand(query, connection);
					try
					{
						cmd.ExecuteNonQuery();

					}
					catch (MySqlException e)
					{

					}
				}
			}
		}
		CloseConnection();
	}


}