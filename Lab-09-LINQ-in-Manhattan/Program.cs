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
            var neighborhoods = (from neighborhoodInfo in deserializedData.features
                            select new Feature
                            {
                                type = neighborhoodInfo.type,
                                geometry = new Geometry
                                {
                                    coordinates = neighborhoodInfo.geometry.coordinates
                                },
                                properties = new Properties
                                {
                                    zip = neighborhoodInfo.properties.zip,
                                    city = neighborhoodInfo.properties.city,
                                    state = neighborhoodInfo.properties.state,
                                    address = neighborhoodInfo.properties.address,
                                    borough = neighborhoodInfo.properties.borough,
                                    neighborhood = neighborhoodInfo.properties.neighborhood,
                                    county = neighborhoodInfo.properties.county
                                }
                            }).ToList();


            Console.WriteLine("Enter 1 to see all of the neighborhoods in Manhatan");
            Console.WriteLine("Enter 2 to filter all of the neighborhoods with the same name in Manhatan");
            Console.WriteLine("Enter 3 to remove all of the neighborhoods that are in the same neighborhood in Manhatan");


            string action = Console.ReadLine();


            if (neighborhoods != null)
            {
                switch (action)
                {

                    case "1":
                        AllNeighborhoods(neighborhoods);
                        break;
                    case "2":
                        Filter_Out_All_The_Neighborhoods_With_No_Name(neighborhoods);
                        break;

                    case "3":
                        RewriteAndConsolidate(neighborhoods);
                        break;
                    case "4":
                        Using_Opposing_LINQ_Method(neighborhoods);
                        break;

                }
            }

        }
        else
        {
            Console.WriteLine("No data found");
        }
    }


    public static void AllNeighborhoods(List<Feature> neighborhoods)
    {

        foreach (var neighborhood in neighborhoods)
        {
            Console.WriteLine($"Type: {neighborhood.type}");
            Console.WriteLine($"Coordinates: {neighborhood.geometry.ConcatinateCoordinates()}");
            Console.WriteLine($"Zip: {neighborhood.properties.zip}");
            Console.WriteLine($"City: {neighborhood.properties.city}");
            Console.WriteLine($"State: {neighborhood.properties.state}");
            Console.WriteLine($"Address: {neighborhood.properties.address}");
            Console.WriteLine($"Borough: {neighborhood.properties.borough}");
            Console.WriteLine($"Neighborhood: {neighborhood.properties.neighborhood}");
            Console.WriteLine($"County: {neighborhood.properties.county}");
            Console.WriteLine("----------------------------------");
        }

        Console.WriteLine("there are currently {0} neiborhoods in Manhatan", neighborhoods.Count);
    }




    public static void Filter_Out_All_The_Neighborhoods_With_No_Name(List<Feature> query)
    {
        var filterQuery = (from neighborhoodInfo in query
                           where !string.IsNullOrWhiteSpace(neighborhoodInfo.properties.neighborhood)
                           select new Feature
                           {
                               type = neighborhoodInfo.type,
                               geometry = new Geometry
                               {
                                   coordinates = neighborhoodInfo.geometry.coordinates
                               },
                               properties = new Properties
                               {
                                   zip = neighborhoodInfo.properties.zip,
                                   city = neighborhoodInfo.properties.city,
                                   state = neighborhoodInfo.properties.state,
                                   address = neighborhoodInfo.properties.address,
                                   borough = neighborhoodInfo.properties.borough,
                                   neighborhood = neighborhoodInfo.properties.neighborhood,
                                   county = neighborhoodInfo.properties.county
                               }
                           }).ToList();

        AllNeighborhoods(filterQuery);
    }






    public static void RemoveTheDuplicates(List<Feature> query)
    {
        var noDuplicates = (from neighborhoodInfo in query
                            where !string.IsNullOrEmpty(neighborhoodInfo.properties.neighborhood)
                            group neighborhoodInfo by neighborhoodInfo.properties.neighborhood into noDups
                            select noDups.First()).ToList();
        Console.WriteLine(noDuplicates.Count);
        AllNeighborhoods(noDuplicates);
    }




    public static void RewriteAndConsolidate(List<Feature> query)
    {
        var rewriteAndConsolidatedQuery = (from neighborhoodInfo in query
                           where !string.IsNullOrWhiteSpace(neighborhoodInfo.properties.neighborhood)
                           group neighborhoodInfo by neighborhoodInfo.properties.neighborhood into noDups

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

        var Opposing_Methods = query.Where(neighborhoodInfo => !string.IsNullOrWhiteSpace(neighborhoodInfo.properties.neighborhood)).GroupBy(
                                           neighborhoodInfo => neighborhoodInfo.properties.neighborhood).Select( noDups =>

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

