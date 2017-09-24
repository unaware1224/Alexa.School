using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Alexa.School.Data.Providers.Nutrislice;
using Alexa.School.Web.Skills;
using JetBrains.Annotations;

namespace Alexa.School.Web.Controllers
{
    /// <summary>
    ///     The controller for responding to queries from Alexa.
    /// </summary>
    public class SkillsController : ApiController
    {
        #region Properties, Indexers

        /// <summary>
        ///     This is where we define which school we are pulling data from.
        /// </summary>
        [NotNull]
        public Data.School School { get; } = new Data.School(
                                                             name: "Pemberton",
                                                             foodProvider: new NutrisliceFoodMenuProvider(
                                                                                                          baseUri: new Uri(
                                                                                                                           uriString:
                                                                                                                           "http://henrico.nutrislice.com/menu/pemberton",
                                                                                                                           uriKind: UriKind.Absolute)));

        #endregion

        #region Public Methods

        /// <summary>
        ///     Route for handling the call from Alexa.
        /// </summary>
        /// <returns>The response from our skill.</returns>
        [Route(template: "school")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostAsync()
        {
            SchoolSkill speechlet = new SchoolSkill(alexaId: null, school: this.School);
            return await speechlet.GetResponseAsync(httpRequest: this.Request);
        }

        #endregion
    }
}