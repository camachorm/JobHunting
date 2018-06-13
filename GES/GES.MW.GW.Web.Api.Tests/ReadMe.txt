Comments and instructions:

- I chose to skip testing the WebAPI controllers themselves for brevity and time constraints, since the implementation pattern applied also skips any logic implementation on those components.

- Please update the web.config and the app.config with the path where the application will find the provided cities.json file. In a live application this would need to be provided by a 
 dynamic data source and handled more cleanly, but for brevity I chose to just use a static file provider with a snippet of the original file from the provided weather api url

 - You will find throughout the code some TODO comments explaining things I would do differently if I had more available time and often an explanation as to why I would do so differently

 - The project name is anonymized so that it cannot be related to your institution, since I will keep it on my github public repository

 - The project name acronyms stand for: 
	- MW: MiddleWare
	- GW: Gateway

- I renamed the projects because halfway through I was running out of time and thought I could save some time by merging the WebApi and the Angular app in one single project, 
  given the scale, this wouldn't be much of an immediate problem, and separating them could easily be achieved at any point in time

- Unfortunately I do not have any more time to spare so I could not add testing to the UI, but I would have included tests both for the controller and the service