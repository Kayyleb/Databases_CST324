// Copyright(c) 2017 Google Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not
// use this file except in compliance with the License. You may obtain a copy of
// the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations under
// the License.

using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab8
{
    public class Quickstart
    {
        private static void InitializeProjectId(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            Console.WriteLine("Created Cloud Firestore client with project ID: {0}", project);
        }

        // Option 1: add static users (Ada)
        private static async Task AddData1(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            DocumentReference docRef = db.Collection("users").Document("alovelace");
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "First", "Ada" },
                { "Last", "Lovelace" },
                { "Born", 1815 }
            };
            await docRef.SetAsync(user);
            Console.WriteLine("Added Ada (alovelace) to the users collection.");
        }

        // Option 1 (continued): add static users (Alan)
        private static async Task AddData2(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            DocumentReference docRef = db.Collection("users").Document("aturing");
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "First", "Alan" },
                { "Middle", "Mathison" },
                { "Last", "Turing" },
                { "Born", 1912 }
            };
            await docRef.SetAsync(user);
            Console.WriteLine("Added Alan (aturing) to the users collection.");
        }

        // Option 2: display users (Ada and Alan)
        private static async Task RetrieveAllDocuments(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            CollectionReference usersRef = db.Collection("users");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
            Console.WriteLine("\nUsers collection:");
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Console.WriteLine("User document id: {0}", document.Id);
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                Console.WriteLine("  First:  {0}", documentDictionary["First"]);
                if (documentDictionary.ContainsKey("Middle"))
                {
                    Console.WriteLine("  Middle: {0}", documentDictionary["Middle"]);
                }
                Console.WriteLine("  Last:   {0}", documentDictionary["Last"]);
                Console.WriteLine("  Born:   {0}", documentDictionary["Born"]);
                Console.WriteLine();
            }
        }


        // Option 3: add user-entered data (at least 2 fields) to your own collection
        private static async Task AddCustomData(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);

            Console.WriteLine("\nAdding to custom collection (\"myCollection\")");

            Console.Write("Enter a name: ");
            string name = Console.ReadLine();

            Console.Write("Enter a description: ");
            string description = Console.ReadLine();

            CollectionReference coll = db.Collection("myCollection");

            Dictionary<string, object> item = new Dictionary<string, object>
            {
                { "Name", name },
                { "Description", description },
            };

            await coll.AddAsync(item);
        }

        // Option 4: display the custom collection you designed
        private static async Task DisplayCustomCollection(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            CollectionReference coll = db.Collection("myCollection");
            QuerySnapshot snapshot = await coll.GetSnapshotAsync();

            Console.WriteLine("\nDocuments in myCollection:");
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Dictionary<string, object> data = document.ToDictionary();
                Console.WriteLine("Doc id: {0}", document.Id);
                Console.WriteLine("  Name:        {0}", data["Name"]);
                Console.WriteLine("  Description: {0}", data["Description"]);
                if (data.ContainsKey("CreatedAt"))
                {
                    Console.WriteLine("  CreatedAt:   {0}", data["CreatedAt"]);
                }
                Console.WriteLine();
            }
        }

        public static void Main(string[] args)
        {
            string project = "lab-2-474618";
            InitializeProjectId(project);

            int choice = -1;

            do
            {
                Console.WriteLine("\n==== MENU ====");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Add static users to the database (Ada and Alan)");
                Console.WriteLine("2. Display users");
                Console.WriteLine("3. Add user-entered data to your own collection");
                Console.WriteLine("4. Display your custom collection");
                Console.Write("Choose an option: ");

                string input = Console.ReadLine();

                bool parsed = int.TryParse(input, out choice);

                if (!parsed)
                {
                    Console.WriteLine("Invalid input. Enter a number 0–4.");
                }
                else
                {
                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("Exiting...");
                            break;

                        case 1:
                            AddData1(project).Wait();
                            AddData2(project).Wait();
                            break;

                        case 2:
                            RetrieveAllDocuments(project).Wait();
                            break;

                        case 3:
                            AddCustomData(project).Wait();
                            break;

                        case 4:
                            DisplayCustomCollection(project).Wait();
                            break;

                        default:
                            Console.WriteLine("Please enter a number between 0 and 4.");
                            break;
                    }
                }

            } while (choice != 0);
        }
    }
}
