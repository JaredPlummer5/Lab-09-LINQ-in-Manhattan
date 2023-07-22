using System;
using System.IO;
using System.Collections.Generic;
using Lab_09_LINQ_in_Manhattan.Classes;
using Newtonsoft.Json;

namespace Lab_09_LINQ_in_Manhattan;

class Program
{
    static void Main(string[] args)
    {
        string json = File.ReadAllText("../../../data.json"); // replace "data.json" with your file path
        ManhatanNeiborhoods? deserializedData = JsonConvert.DeserializeObject<ManhatanNeiborhoods>(json);

        if (deserializedData != null && deserializedData.features != null)
        {
            var features = (from featureInfo in deserializedData.features
                            select new Feature
                            {
                                type = featureInfo.type,
                                geometry = new Geometry
                                {
                                    coordinates = featureInfo.geometry.coordinates
                                },
                                properties = new Properties
                                {
                                    zip = featureInfo.properties.zip,
                                    city = featureInfo.properties.city,
                                    state = featureInfo.properties.state,
                                    address = featureInfo.properties.address,
                                    borough = featureInfo.properties.borough,
                                    neighborhood = featureInfo.properties.neighborhood,
                                    county = featureInfo.properties.county
                                }
                            }).ToList();


            Console.WriteLine("Enter 1 to see all of the neighborhoods in Manhatan");
            Console.WriteLine("Enter 2 to filter all of the neighborhoods with the same name in Manhatan");
            Console.WriteLine("Enter 3 to remove all of the neighborhoods that are in the same neighborhood in Manhatan");


            string action = Console.ReadLine();


            if (features != null)
            {
                switch (action)
                {

                    case "1":
                        AllNeighborhoods(features);
                        break;
                    case "2":
                        Filter_Out_All_The_Neighborhoods_With_No_Name(features);
                        break;

                    case "3":
                        RewriteAndConsolidate(features);
                        break;
                    case "4":
                        Using_Opposing_LINQ_Method(features);
                        break;

                }
            }

        }
        else
        {
            Console.WriteLine("No data found");
        }
    }


    public static void AllNeighborhoods(List<Feature> features)
    {

        foreach (var item in features)
        {
            Console.WriteLine($"Type: {item.type}");
            Console.WriteLine($"Coordinates: {item.geometry.ConcatinateCoordinates()}");
            Console.WriteLine($"Zip: {item.properties.zip}");
            Console.WriteLine($"City: {item.properties.city}");
            Console.WriteLine($"State: {item.properties.state}");
            Console.WriteLine($"Address: {item.properties.address}");
            Console.WriteLine($"Borough: {item.properties.borough}");
            Console.WriteLine($"Neighborhood: {item.properties.neighborhood}");
            Console.WriteLine($"County: {item.properties.county}");
            Console.WriteLine("----------------------------------");
        }

        Console.WriteLine("there are currently {0} neiborhoods in Manhatan", features.Count);
    }




    public static void Filter_Out_All_The_Neighborhoods_With_No_Name(List<Feature> query)
    {
        var filterQuery = (from featureInfo in query
                           where !string.IsNullOrWhiteSpace(featureInfo.properties.neighborhood)
                           select new Feature
                           {
                               type = featureInfo.type,
                               geometry = new Geometry
                               {
                                   coordinates = featureInfo.geometry.coordinates
                               },
                               properties = new Properties
                               {
                                   zip = featureInfo.properties.zip,
                                   city = featureInfo.properties.city,
                                   state = featureInfo.properties.state,
                                   address = featureInfo.properties.address,
                                   borough = featureInfo.properties.borough,
                                   neighborhood = featureInfo.properties.neighborhood,
                                   county = featureInfo.properties.county
                               }
                           }).ToList();

        AllNeighborhoods(filterQuery);
    }






    public static void RemoveTheDuplicates(List<Feature> query)
    {
        var noDuplicates = (from featureInfo in query
                            where !string.IsNullOrEmpty(featureInfo.properties.neighborhood)
                            group featureInfo by featureInfo.properties.neighborhood into noDups
                            select noDups.First()).ToList();
        Console.WriteLine(noDuplicates.Count);
        AllNeighborhoods(noDuplicates);
    }




    public static void RewriteAndConsolidate(List<Feature> query)
    {
        var rewriteAndConsolidatedQuery = (from featureInfo in query
                           where !string.IsNullOrWhiteSpace(featureInfo.properties.neighborhood)
                           group featureInfo by featureInfo.properties.neighborhood into noDups

                           select new Feature
                           {
                               type = noDups.First().type,
                               geometry = new Geometry
                               {
                                   coordinates = noDups.First().geometry.coordinates
                               },
                               properties = new Properties
                               {
                                   zip = noDups.First().properties.zip,
                                   city = noDups.First().properties.city,
                                   state = noDups.First().properties.state,
                                   address = noDups.First().properties.address,
                                   borough = noDups.First().properties.borough,
                                   neighborhood = noDups.First().properties.neighborhood,
                                   county = noDups.First().properties.county
                               }
                           }).ToList();
        AllNeighborhoods(rewriteAndConsolidatedQuery);
    }



    public static void Using_Opposing_LINQ_Method(List<Feature> query)
    {

        var Opposing_Methods = query.Where( featureInfo => !string.IsNullOrWhiteSpace(featureInfo.properties.neighborhood)).GroupBy(
                                           featureInfo => featureInfo.properties.neighborhood).Select( noDups =>

                                            new Feature
                                           {
                                               type = noDups.First().type,
                                               geometry = new Geometry
                                               {
                                                   coordinates = noDups.First().geometry.coordinates
                                               },
                                               properties = new Properties
                                               {
                                                   zip = noDups.First().properties.zip,
                                                   city = noDups.First().properties.city,
                                                   state = noDups.First().properties.state,
                                                   address = noDups.First().properties.address,
                                                   borough = noDups.First().properties.borough,
                                                   neighborhood = noDups.First().properties.neighborhood,
                                                   county = noDups.First().properties.county
                                               }
                                           }).ToList();

        Console.WriteLine($"Using opposing methods count: {Opposing_Methods.Count}");

        AllNeighborhoods(Opposing_Methods);

    }

}

