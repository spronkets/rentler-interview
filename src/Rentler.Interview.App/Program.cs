// See https://aka.ms/new-console-template for more information

using Rentler.Interview.Client;

var food = await FoodApi.GetFood();

Console.WriteLine(food);
