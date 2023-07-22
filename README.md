# Lab-09-LINQ-in-Manhattan

## Manhattan Neighborhoods Explorer

This is a simple console application that allows users to explore neighborhoods in Manhattan. This application loads a JSON dataset that contains information about neighborhoods, including their coordinates, zip code, city, state, address, borough, neighborhood, and county. Users can query this dataset in different ways to get the information they need.

![Application screenshot](url-to-screenshot.png)

## Features

1. Display all neighborhoods in Manhattan.
2. Filter neighborhoods based on names.
3. Consolidate neighborhoods with the same name.
4. Opposing LINQ method application.
5. Remove duplicate neighborhood entries.

## How to Use 

Follow these steps to use the application:

1. Clone the repository to your local machine.

```shell
git clone https://github.com/your-username/Manhattan-Neighborhoods-Explorer.git
```

2. Navigate to the directory where you cloned the repository.

```shell
cd path-to-your-directory/Manhattan-Neighborhoods-Explorer
```

3. Run the application.

```shell
dotnet run
```

4. The console will prompt you to enter a command.

```shell
Enter 1 to see all of the neighborhoods in Manhattan
Enter 2 to filter all of the neighborhoods with the same name in Manhattan
Enter 3 to remove all of the neighborhoods that are in the same neighborhood in Manhattan
```

5. Enter a number based on what action you want to take, and the application will execute that action.

## Additional Details

This application uses .NET Core 3.1 and Newtonsoft.Json for JSON deserialization. Make sure to install the .NET Core SDK and the Newtonsoft.Json NuGet package before running the application.

The dataset is a JSON file located in the `data.json` file. If you want to use a different dataset, replace the content of `data.json` with your own JSON data.

This project was developed as part of the Lab 09 assignment in the Code 401 Advanced Software Development in ASP.NET Core course at Code Fellows.

## Contact 

If you have any questions or want to contribute to this project, please contact the repository owner.

Please note: This is a fictitious sample README provided as an example and the links, commands, and information provided may not work in a real-world scenario. You would replace the information here with your actual project information.