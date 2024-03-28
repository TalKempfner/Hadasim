# Hadasim
Home exercises for the Hadasim program


Introduction:

This project provides a user-friendly ASP.NET Core application for managing memberhealth insurance member data, especially information about Covid. It allows organizations to efficiently add, edit, delete, and search for member information

Features: 

* Add, edit, and delete member profiles
* Search for members by Id or Name
* Track Covid history 

Installation: 

You need to have asp.net core 6.0 to run the project.

The databace is on the cloud so no need for local databace.

The project is hosted on GitHub. To install, clone the repository and run npm install in the project directory.


Configuration: 

1. Clone the repo
   ```sh
   git clone https://github.com/TalKempfner/Hadasim.git
   ```
2. Run the project
   ```sh
   dotnet build
   dotnet run
   ```
    then paste the given url into the address bar of your web browser 

Usage:
1. Watching all the health insurance members

<img src=https://github.com/TalKempfner/Hadasim/assets/117106989/31bbed69-9349-449c-b8a2-605b36151a16  width="400">

2. Searching for a member by name or ID

<img src=https://github.com/TalKempfner/Hadasim/assets/117106989/85925576-025e-4498-80bc-f8e05a14c09d  width="400">

3. Adding a new member

<img src=https://github.com/TalKempfner/Hadasim/assets/117106989/d0f53fb7-43de-48c9-9ee4-648eeefb847b  width="400">

including adding vaccination and illness dates for the patient

<img src=https://github.com/TalKempfner/Hadasim/assets/117106989/41984c7b-bdda-4284-9c30-8689855152de  width="400">

4. Viewing a member profile

<img src=https://github.com/TalKempfner/Hadasim/assets/117106989/7cfd6aca-5fcc-4a86-8bd1-629362a7dc59  width="400">

5. Editing a member's details

<img src=https://github.com/TalKempfner/Hadasim/assets/117106989/4cf6d124-8ca5-42ab-a3eb-8f646569ab47  width="400">

including editing the illness dates and editing/adding vaccinations

<img src=https://github.com/TalKempfner/Hadasim/assets/117106989/4e6e44c6-c793-4c3b-949d-71dc8c6f5348  width="400">

<img src=https://github.com/TalKempfner/Hadasim/assets/117106989/1f4507b4-8ef3-4060-8452-cca838c7219e  width="400">

6. Deleting a member and all his vaccinations and illness history

7. Viewing a chart that shows the number of people with covid each month for the last five years(you can choose the year)

<img src=https://github.com/TalKempfner/Hadasim/assets/117106989/12e947e6-51e6-4cc9-b270-813f3b6cf5e8  width="400">

8. Adding and Editing types of vaccines

<img src=https://github.com/TalKempfner/Hadasim/assets/117106989/d3a8ad4f-7145-40ad-b429-8e1e46bf34ab  width="400">


Concessions:

A member can only get sick once and get vaccinated up to 4 times

Bonuses:

1. Added the ability to upload a photo of the checkout member on the client side
2. Chart that shows how many active patients were each month in the last few years

Additional Notes:

* This project is built using ASP.NET Core.
* No external libraries such as React or Express were utilized in the development process.
* Manual testing was conducted, and efforts were made to ensure code quality and project integrity.


