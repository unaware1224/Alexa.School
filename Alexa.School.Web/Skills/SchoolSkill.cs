using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alexa.School.Data.Menus.Food;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.UI;
using JetBrains.Annotations;

namespace Alexa.School.Web.Skills
{
    /// <summary>
    ///     Facilitates translating requests from Alexa into responses.
    /// </summary>
    public class SchoolSkill : SpeechletAsync
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SchoolSkill" /> class.
        /// </summary>
        /// <param name="alexaId">Your unique application ID given by Amazon (only required if you publish your skill).</param>
        /// <param name="school">The school data.</param>
        public SchoolSkill([CanBeNull] string alexaId, [NotNull] Data.School school)
        {
            this.AlexaId = alexaId;
            this.DefaultSchool = school;
            this.RePrompt = $"To get a menu, ask, what is for lunch today? Or, what is for breakfast tomorrow ?";
            this.WelcomeMessage = $"Welcome to the {school.Name} skill. {this.RePrompt}";
        }

        #endregion

        #region Properties, Indexers

        /// <summary>
        ///     Gets or sets the Alexa ID to validate. Only needed for official skills.
        /// </summary>
        [CanBeNull]
        public string AlexaId { get; }

        /// <summary>
        ///     Used for single school skills instead of querying by school.
        /// </summary>
        [NotNull]
        public Data.School DefaultSchool { get; }

        /// <summary>
        ///     The welcome message for this skill.
        /// </summary>
        [NotNull]
        public string WelcomeMessage { get; }

        /// <summary>
        ///     What to prompt the user with when we cannot figure out what they are asking.
        /// </summary>
        [NotNull]
        public string RePrompt { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Handles all requests from Alexa.
        /// </summary>
        /// <param name="intentRequest">The request from Alexa.</param>
        /// <param name="session">The session information.</param>
        /// <returns>Our response to the request.</returns>
        public override async Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session)
        {
            // Get intent from the request object.
            Intent intent = intentRequest?.Intent;
            string intentName = intent?.Name;

            // NOTE: If the session is started with an intent, no welcome message will be rendered
            // rather, the intent specific response will be returned.
            if ("WhatsOnTheMenuIntent".Equals(value: intentName))
            {
                return await this.GetMenuAsync(session: session);
            }

            // common built-in replies
            {
                if ("AMAZON.HelpIntent".Equals(value: intentName))
                {
                    return this.BuildResponse(title: "Help", message: this.RePrompt, shouldEndSession: false);
                }

                if ("AMAZON.StopIntent".Equals(value: intentName) || "AMAZON.CancelIntent".Equals(value: intentName))
                {
                    return this.BuildResponse(title: "Goodbye", message: "Goodbye", shouldEndSession: true);
                }

                return this.BuildResponse(title: "Help", message: this.RePrompt, shouldEndSession: false);
            }
        }

        /// <summary>
        ///     For when someone starts the skill without asking a question.
        /// </summary>
        /// <param name="launchRequest">The launch request.</param>
        /// <param name="session">The session information.</param>
        /// <returns>Our response to the request.</returns>
        public override Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session)
        {
            return Task.FromResult(result: this.BuildResponse(title: "Welcome", message: this.WelcomeMessage, shouldEndSession: false));
        }

        public override Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session)
        {
            return Task.FromResult(result: this.BuildResponse(title: "Welcome", message: this.WelcomeMessage, shouldEndSession: false));
        }

        public override Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session)
        {
            return Task.FromResult(result: this.BuildResponse(title: "Welcome", message: this.WelcomeMessage, shouldEndSession: false));
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Creates the food menu response from the session.
        /// </summary>
        /// <param name="session">The session to pull the key pieces out from.</param>
        /// <returns>Our response to the request.</returns>
        private async Task<SpeechletResponse> GetMenuAsync([NotNull] Session session)
        {
            session.Attributes.TryGetValue(key: "Meal", value: out string mealQuery);
            session.Attributes.TryGetValue(key: "Date", value: out string dateQuery);

            MenuType? menuType = MenuTypes.GetMenuType(value: mealQuery);

            if (menuType == null)
            {
                return this.BuildResponse(
                                          title: "Meal not found",
                                          message: "To find a meal, please say either breakfast or lunch.",
                                          shouldEndSession: true);
            }

            DateTime date;
            if (!DateTime.TryParse(s: dateQuery, result: out date))
            {
                date = DateTime.Today;
            }

            try
            {
                List<string> menuItems = await this.DefaultSchool.FoodProvider.GetMenuAsync(type: menuType.Value, date: date);

                string title = $"{this.DefaultSchool.Name} {menuType.Value.ToString() .ToLowerInvariant()} menu";

                if (menuItems.Count == 0)
                {
                    return
                            this.BuildResponse(
                                               title: title,
                                               message:
                                               $"There is nothing available for {menuType.Value.ToString() .ToLowerInvariant()} on {date:dddd}",
                                               shouldEndSession: true);
                }

                return this.BuildResponse(
                                          title: title,
                                          message:
                                          $"On {date:dddd} at {this.DefaultSchool.Name}, {menuType.Value.ToString() .ToLowerInvariant()} includes {string.Join(separator: ", ", values: menuItems)}.",
                                          shouldEndSession: true);
            }
            catch (Exception ex)
            {
                return this.BuildResponse(
                                          title: "Error",
                                          message: "We were unable to get the menu, " + ex.Message,
                                          shouldEndSession: true);
            }
        }

        /// <summary>
        ///     Creates and returns the visual and spoken response with shouldEndSession flag
        /// </summary>
        /// <param name="title">title for the companion application home card</param>
        /// <param name="message">message content for speech and companion application home card</param>
        /// <param name="shouldEndSession">should the session be closed</param>
        /// <returns>SpeechletResponse spoken and visual response for the given input</returns>
        private SpeechletResponse BuildResponse(
            [NotNull] string title,
            [NotNull] string message,
            bool shouldEndSession)
        {
            // Create the Simple card content.
            SimpleCard card = new SimpleCard
                              {
                                  Title = title,
                                  Content = message
                              };

            // Create the plain text message.
            PlainTextOutputSpeech speech = new PlainTextOutputSpeech
                                           {
                                               Text = message
                                           };

            // Create the skill response.
            SpeechletResponse response = new SpeechletResponse
                                         {
                                             ShouldEndSession = shouldEndSession,
                                             OutputSpeech = speech,
                                             Card = card
                                         };

            // if session open, create re-prompt
            if (!shouldEndSession)
            {
                response.Reprompt = new Reprompt
                                    {
                                        OutputSpeech = new PlainTextOutputSpeech
                                                       {
                                                           Text = this.RePrompt
                                                       }
                                    };
            }

            return response;
        }

        #endregion
    }
}