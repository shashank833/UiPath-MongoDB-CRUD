# UiPath-MongoDB-CRUD

This Nuget package has four methods exposed
1)Create Collection
2)Update collection
3)Filter Collection
4)Delete Collection

Create Collection: It has three arguments
                    1) Server ip-> String-> Ip address of the MongoDB server
                    2) Database Name -> String -> The Name of the existing/new database
                    3) Collection Name-> String -> The Name of the existing/new Collection

Update Collection : It has eight arguments
                    1) Server ip-> String-> Ip address of the MongoDB server
                    2) Database Name -> String -> The Name of the database
                    3) Collection Name-> String -> The Name of the Collection
                    4) Type of Filter -> ddEnum -> The condition for update query
                    5) Filter Field -> String -> The Field on which the condition is to be checked
                    6) Filter Value -> Dynamic -> The Field on which the condition is to be checked
                    7) Update Field -> String -> The Field  which the is to be Updated
                    8) Update value -> Dynamic -> The Value to be updated
                    
Filter Collection : It has five arguments
                    1) Server ip-> String-> Ip address of the MongoDB server
                    2) Database Name -> String -> The Name of the database
                    3) Collection Name-> String -> The Name of the Collection
                    4)Field -> Array of String -> Fields on which the selection is to be performed
                    5)Value -> Array of String -> Valued on which the selection is to be performed
Please note: Field and Value is a key value pair
Ex: field= {field1, field2}, value= {value1, value2}


Delete Collection: It has three arguments
                    1) Server ip-> String-> Ip address of the MongoDB server
                    2) Database Name -> String -> The Name of the database
                    3) Collection Name-> String -> The Name of the Collection
                    
