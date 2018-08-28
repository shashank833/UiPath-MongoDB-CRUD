using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using MongoDB.Driver.Linq;
using System.ComponentModel;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections;

namespace MongoDB_CRUD
{
    public class Update_Collection : CodeActivity
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
        public InArgument<string> Filter_Field { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<dynamic> Filter_Value { get; set; }
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Update_Field { get; set; }
        [Category("Input")]
        [RequiredArgument]
        public InArgument<dynamic> Update_Value { get; set; }
        public enum ddEnum
        {
            EQ,
            GT,
            GTE,
            LT,
            LTE,
        }
        [Category("Input")]
        [RequiredArgument]
        public ddEnum typeoffilter { get; set; }

        protected override void Execute(CodeActivityContext context)
        {

            var database_name = DB_Name.Get(context);
            var coll_name = Collection_Name.Get(context);
            var database_ip = DB_Server_IP.Get(context);
            string Query_Field = Filter_Field.Get(context);
            dynamic Query_Value = Filter_Value.Get(context);
            string Update_Key = Update_Field.Get(context);
            dynamic Update_Val = Update_Value.Get(context);
            Console.WriteLine(typeoffilter);
            List<string> output = new List<string>();

            MongoClient mongoclient = new MongoClient(database_ip);
            var db_list = mongoclient.ListDatabaseNames().ToList();
            if (db_list.Contains(database_name))
            {
                //Connects to the Exsisting Database or Creates new Database
                MongoDatabaseBase myDB = (MongoDatabaseBase)mongoclient.GetDatabase(database_name);
                //Creates new collection
                //var colll=myDB.CreateCollection(coll_name);
                var coll_list = myDB.ListCollectionNames().ToList();
                if (coll_list.Contains(coll_name))
                {
                    var collection = myDB.GetCollection<BsonDocument>(coll_name);
                    if (typeoffilter.ToString() == "EQ")
                    {
                        var query_e = Builders<BsonDocument>.Filter.Eq(Query_Field, Query_Value);
                        var update_1 = Builders<BsonDocument>.Update.Set(Update_Key, Update_Val); // update modifiers
                        collection.UpdateMany(query_e, update_1);
                    }
                    else if (typeoffilter.ToString() == "GT")
                    {
                        var query_e = Builders<BsonDocument>.Filter.Gt(Query_Field, Query_Value);
                        var update_1 = Builders<BsonDocument>.Update.Set(Update_Key, Update_Val); // update modifiers
                        collection.UpdateMany(query_e, update_1);
                    }
                    else if (typeoffilter.ToString() == "GTE")
                    {
                        var query_e = Builders<BsonDocument>.Filter.Gte(Query_Field, Query_Value);
                        var update_1 = Builders<BsonDocument>.Update.Set(Update_Key, Update_Val); // update modifiers
                        collection.UpdateMany(query_e, update_1);
                    }
                    else if (typeoffilter.ToString() == "LT")
                    {
                        var query_e = Builders<BsonDocument>.Filter.Lt(Query_Field, Query_Value);
                        var update_1 = Builders<BsonDocument>.Update.Set(Update_Key, Update_Val); // update modifiers
                        collection.UpdateMany(query_e, update_1);
                    }
                    else if (typeoffilter.ToString() == "LTE")
                    {
                        var query_e = Builders<BsonDocument>.Filter.Lte(Query_Field, Query_Value);
                        var update_1 = Builders<BsonDocument>.Update.Set(Update_Key, Update_Val); // update modifiers
                        collection.UpdateMany(query_e, update_1);
                    }
                }
                else
                {
                    Console.Write("Cannot find Collection");
                }
            }
            else
            {
                Console.Write("Cannot find DB");
            }
        }
    }
}


