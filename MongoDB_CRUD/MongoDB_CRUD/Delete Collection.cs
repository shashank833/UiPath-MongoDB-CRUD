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
    public class Delete_Collection : CodeActivity
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

        public enum ddEnum
        {
            Yes,
            No,
        }
        [Category("Input")]
        [RequiredArgument]
        public ddEnum DB_Delete_Flag { get; set; }

        protected override void Execute(CodeActivityContext context)
        {

            var database_name = DB_Name.Get(context);
            var coll_name = Collection_Name.Get(context);
            var database_ip = DB_Server_IP.Get(context);


            //Connects to the Server
            MongoClient mongoclient = new MongoClient(database_ip);
            //List of all the Databases
            //Connects to the Existing Database
            if (DB_Delete_Flag.ToString() == "No")
            {
                var db_list = mongoclient.ListDatabaseNames().ToList();
                if (db_list.Contains(database_name))
                {
                    MongoDatabaseBase myDB = (MongoDatabaseBase)mongoclient.GetDatabase(database_name);

                    var coll_list = myDB.ListCollectionNames().ToList();
                    if (coll_list.Contains(coll_name))
                    {
                        myDB.DropCollection(coll_name);

                    }
                    else
                    {
                        Console.WriteLine("Collection does not exsist");
                    }
                }
                else
                {
                    Console.WriteLine("DB does not exsist");
                }
            }
            else if (DB_Delete_Flag.ToString() == "Yes")
            {
                mongoclient.DropDatabase(database_name);
            }
        }
    }
}
