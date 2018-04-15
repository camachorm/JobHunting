# NoughtsANdCrosses (AKA TicTacToe)

## How to run the application
This is a simple console application, just check it out, open it on Visual Studio, build it and finally run 
it either from the IDE directly or from the command prompt by executing NAC.exe (typically found under <ProjectFolder>\bin\Debug)

```
**Note:** This solution depends on NUNit 2.6.4 and Specflow to build properly, as long as you 
have nuget available it should automatically load the dependencies into the Solution root 
directory as follows:
"(SolutionDir)\packages\NUnit.2.6.4"
"(SolutionDir)\packages\SpecFlow.1.9.0"
**If not you will have to manually download it into the required location**
```

## How long did I spend writing this code
I think overall I used up 3 hours, intermittently jumping in and out of the project as time allowed.

## What I am pleased with
I think this code has enough structure in place to easily evolved into more complex requirements over time, laying out the groundwork for 
ease of adapating to change over time whilst still maintaining a robust code coverage on its core features

## What am I not pleased with or do better if I had more time
1. I would have made the Board dynamic instead of fixed size
2. I would have used an algorythm for calculating the points sequences that ammounted to a win instead of hardcoding the combinations
3. I would have added a MsBuild script that would provide build, test and run targets to make it easier for any user (including myself) to work without the IDE

## Tests for the code
There is a dedicated project for the Testing of the code base, and also a full report of the code coverage that you can find in "(ProjectFolder)\NAC.Tests\All tests from GameTests.html"

### Unit Tests
Based on the NUnit framework and stored on the NAC.TESTS project, under the Unit folder you will find the following Test classes
1. BoardTests.cs
2. GameTests.cs
3. PlayerTests.cs

Each of these files represents a chunk of the OO representation of the business logic and contains individual tests pertaining said logic, failling and successfull use cases as appropriate

### Unit Tests
Based on the NUnit framework and stored on the NAC.TESTS project, under the Unit folder you will find the following Test classes
1. BoardTests.cs
2. GameTests.cs
3. PlayerTests.cs

Each of these files represents a chunk of the OO representation of the business logic and contains individual tests pertaining said logic, failling and successfull use cases as appropriate

