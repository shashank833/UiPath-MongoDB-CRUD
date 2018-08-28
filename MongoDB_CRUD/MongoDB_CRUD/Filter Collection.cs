using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.ComponentModel;
using System.Activities;

namespace MongoDB_CRUD
{
    public class Filter_Collection : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> DB_Name { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Collection_Name { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> DB_Server_IP { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string[]> Field { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string[]> Value { get; set; }

        [Category("Output")]

        public OutArgument<List<string>> Result { get; set; }
        protected override void Execute(CodeActivityContext context)
        {

            var database_name = DB_Name.Get(context);
            var coll_name = Collection_Name.Get(context);
            var database_ip = DB_Server_IP.Get(context);


            List<FilterDefinition<user_Collection_model>> filter_user = new List<FilterDefinition<user_Collection_model>>();
            List<string> output = new List<string>();

            //Connects to the Server
            MongoClient mongoclient = new MongoClient(database_ip);
            //List of all the Databases
            var db_list = mongoclient.ListDatabaseNames().ToList();
            if (db_list.Contains(database_name))
            {
                //Connects to the Existing Database
                MongoDatabaseBase myDB = (MongoDatabaseBase)mongoclient.GetDatabase(database_name);

                //List of all the collections in the database
                var coll_list = myDB.ListCollectionNames().ToList();
                if (coll_list.Contains(coll_name))
                {

                    var coll = myDB.GetCollection<user_Collection_model>(coll_name);
                    //The key value combination entered by the user
                    string[] key = Field.Get(context);
                    string[] value = Value.Get(context);
                    //The count of key value combination 
                    int count_key = key.Count();

                    //Builds a list of filters
                    for (int i = 0; i < count_key; i++)
                    {
                        var filter_1 = Builders<user_Collection_model>.Filter.Eq(key[i], value[i]);
                        filter_user.Add(filter_1);
                    }

                    var filter = Builders<user_Collection_model>.Filter.And(filter_user);
                    var results = coll.Find(filter).ToEnumerable().ToList();
                    for (int j = 0; j < results.Count(); j++)
                    {
                        var i = results[j].CatchAll.ToString();
                        output.Add(i);
                    }


                    Result.Set(context, output);
                }
                else
                {
                    Console.WriteLine("collection does not exsist");
                }
            }
            else
            {
                Console.WriteLine("DB does not exsist");
            }
        }
    }
}


