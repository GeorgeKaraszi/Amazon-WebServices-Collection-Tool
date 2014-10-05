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
 * Class:
 *		AmazonItemHandler - Handles all the information that deals with Amazons web
 *			services. (AWS)
 * 
 * Functions:
 *		-AmazonItemHandler(...) - Initial Connections to AWS
 *		-GatherItem(..) - Gathers a single item from AWS via a given ASIN
 *		-GatherItems(..) - Gathers multiple items by a given ASIN. Then proceeds to crawl
 *			through a listing of ASIN that is tagged as similar. This listing of ASIN's
 *			is provided by each item that AWS's allows to be gathered.
 *		-QuerySimilar(..) - Backbone of gathering all similar items of a given ASIN
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NKCraddock.AmazonItemLookup;
using NKCraddock.AmazonItemLookup.Client;

namespace EconServices
{
    class AmazonItemHandler
    {
        //-------------------------------------------------------------------------------
        //Private varaibles

        private ProductApiConnectionInfo _connectionInfo;
        private AwsProductApiClient _apiClient;
        //-------------------------------------------------------------------------------
        public AmazonItemHandler(string pubKey, string privKey, string associteTag)
        {
            _connectionInfo = new ProductApiConnectionInfo();

            _connectionInfo.AWSAccessKey = pubKey;
            _connectionInfo.AWSSecretKey = privKey;
            _connectionInfo.AWSAssociateTag = associteTag;

            _apiClient = new AwsProductApiClient(_connectionInfo);
        }

        //-------------------------------------------------------------------------------
        /// <summary>
        /// Gather a single item
        /// </summary>
        /// <returns></returns>
        public AwsItem GatherItem(string itemId)
        {
            return _apiClient.ItemLookupByAsin(itemId);
        }

        //-------------------------------------------------------------------------------
        /// <summary>
        /// Gathers multible items from an amazon query
        /// </summary>
        /// <param name="itemId">ID of the itial item</param>
        /// <param name="similarCat">Only gather simular items from the same 
        ///                                       category as the itial item</param>
        /// <param name="maxItems">Amount of items to be gathered</param>
        /// <returns></returns>
        public AwsItem[] GatherItems(string itemId, bool similarCat, int maxItems)
        {
            List<AwsItem> itemList = new List<AwsItem>(); //List of items collected
            int remainingItems = 0;
            AwsItem[] queriedItems;

            itemList.Add(GatherItem(itemId));        //Inilize the list with requested item

            for (int i = 0; itemList.Count < maxItems; i++)
            {
                remainingItems = maxItems - itemList.Count;

                if (itemList[i] != null)
                {
                    queriedItems = QuerySimilar(itemList[i], similarCat, remainingItems);

                    if (queriedItems.Count() >= 1)
                    {
                        itemList.AddRange(queriedItems);
                    }
                }
                else
                {
                    break;
                }
            }



            return itemList.ToArray();
        }

        //-------------------------------------------------------------------------------
        /// <summary>
        /// Finds similar items from the givin item.
        /// </summary>
        /// <param name="item">item that needs similar products</param>
        /// <param name="similarCat">Match only the same category of the item</param>
        /// <param name="maxGather">Maximum number of similar items to get</param>
        /// <returns>Array containing all quieried and good items</returns>
        private AwsItem []QuerySimilar(AwsItem item, bool similarCat, int maxGather)
        {
            string itemId               = "";            //Item ASIN id from amazon
            AwsItem queryItem           = new AwsItem(); //Item being looked at
            List<AwsItem> queryItemList = new List<AwsItem>(); //List of good items
            int itemsRemaining          = maxGather;

            try
            {

                foreach (var n in item.SimilarProducts)
                {
                    if (itemsRemaining <= 0)
                        break;

                    itemId = n.ASIN;

                    queryItem = _apiClient.ItemLookupByAsin(itemId);

                    if (similarCat)
                    {
                        if (item.ItemAttributes["ProductGroup"] ==
                            queryItem.ItemAttributes["ProductGroup"])
                        {
                            queryItemList.Add(queryItem);
                            itemsRemaining--;
                        }
                    }
                    else
                    {
                        queryItemList.Add(queryItem);
                        itemsRemaining--;
                    }

                }
            }
            catch(Exception e)
            {
                Thread.Sleep(2000);
                return queryItemList.ToArray();
            }

            return queryItemList.ToArray();

        }
    }
}
