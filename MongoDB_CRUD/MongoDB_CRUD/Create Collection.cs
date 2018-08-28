using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB_CRUD
{
    public class Create_Collection : CodeActivity
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

        protected override void Execute(CodeActivityContext context)
        {

            var database_name = DB_Name.Get(context);
            var coll_name = Collection_Name.Get(context);
            var database_ip = DB_Server_IP.Get(context);
            //Connects to the Server
            MongoClient mongoclient = new MongoClient(database_ip);
            //List of all the Databases
            //Connects to the Existing Database
            MongoDatabaseBase myDB = (MongoDatabaseBase)mongoclient.GetDatabase(database_name);

            var coll_list = myDB.ListCollectionNames().ToList();
            if (coll_list.Contains(coll_name))
            {

                var coll = myDB.GetCollection<BsonDocument>(coll_name);
            }
            else
            {
                myDB.CreateCollection(coll_name);
            }

        }
    }
}
