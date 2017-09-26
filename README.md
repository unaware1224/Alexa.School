# Alexa.School
[![Build status](https://ci.appveyor.com/api/projects/status/a5roybe8vrbrpyw7/branch/master?svg=true)](https://ci.appveyor.com/project/unaware1224/alexa-school/branch/master)

This started as a way to explain to my kids what I do for a living. Each morning my kids had a decision to make, whether or not to bring or buy lunch that day at school.  So I wrote a quick application to extract the menus so they could ask Alexa what was lunch.  When I released the skill, I got lots of requests to add other schools, and decided to release it so that anyone can add their own school or extend the functionality for everyone to enjoy.

There are three steps to making this work:
 1. Configure your school
 2. Deploy the code to something publically available
 3. Configure your skill in Amazon
 
## Configure your school
 - Currently, I have only written to retrieve menus from [Nutrislice](http://www.nutrislice.com/)
 - Simply edit the School property in the [Alexa.School.Web.Controllers.SkillsController](https://github.com/unaware1224/Alexa.School/blob/master/Alexa.School.Web/Controllers/SkillsController.cs#L22-L28)
   - Set the school name
   - Set the URL to the menu in Nutrislice

## Deploy to something publicly available
Alexa needs to be able to reach your service publically, and the important part is your service needs to be behind HTTPS.  I used Azure because the free tier is more than sufficient, and HTTPs is available, you just do not have a pretty url (https://*.azurewebsites.net)

## Configure your skill in Amazon/Alexa
 - Sign up for a Amazon developer account (note: if you will not be publicly releasing your skill, use the same account your Alexa is tied to)
 - Create a new Alexa Skills Kit, here are some suggestions on configuration:
   - Skill information
     - Invocation name: my kids had fun with these, coming with with silly words or characters from books/movies ... though we ended with either the name of the principal or the school.
   - Interaction Model
    - Intent Schema (see below)
    - Custom Slot Types
      - Type: LIST_OF_MEALS
       - Values: 
         - lunch
         - breakfast
   - Sample Utterances
     - WhatsOnTheMenuIntent what is for {Meal} {Date}
     - WhatsOnTheMenuIntent what is for {Meal} on {Date}
   - Interaction Model
     - Endpoint type: HTTPS
     - Default: The endpoint wherever you deployed this
   - SSL Certificate
     - Since mine is in Azure, I choose "My development endpoint is a sub-domain of a domain that has a wildcard certificate from a certificate authority"
    

IMPORTANT: If you plan on making your skill publicly available, you need to copy the Application ID from the Skill Information section, and add that to the SchoolSkill constructor in your controller.

Intent schema
```
   {
  "intents": [
    {
      "slots": [
        {
          "name": "Meal",
          "type": "LIST_OF_MEALS"
        },
        {
          "name": "Date",
          "type": "AMAZON.DATE"
        }
      ],
      "intent": "WhatsOnTheMenuIntent"
    },
    {
      "intent": "AMAZON.HelpIntent"
    },
    {
      "intent": "AMAZON.StopIntent"
    },
    {
      "intent": "AMAZON.CancelIntent"
    }
  ]
}
```
