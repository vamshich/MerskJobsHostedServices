Job Scheduler is the start up project
Base URL: https://localhost:44365/swagger/index.html


The following application has 3 endpoint:

1. /Jobs​/PostJob :This end point used to post the array of int to be sorted as a background task


2./Jobs/GetJobs :This end point used to query all the jobs that are submitted 


3.​/Jobs​/GetJob:  This end pointt used to query indivial job with job Id;



STEPS TO TEST:

1.Open the application in visual studio or any editor 
2.Run the appliation .It opens the default swaager page
3.Use /Jobs​/PostJob to post the input array to be sorted.
4.Use /Jobs/GetJobs to get the job details
5.Use ​/Jobs​/GetJob with job Id returned when we run /Jobs​/PostJob to get the job details for spefic job


Tech Stack:
1.Net Core
2.EntityFramework InMemory Database
3.Hosted Services to run background task used QueueBackground feature in .net core
4.AutoMapper
5.Swagger


