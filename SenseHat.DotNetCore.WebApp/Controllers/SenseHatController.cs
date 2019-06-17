#region Using

using Microsoft.AspNetCore.Mvc;
using SenseHat.DotNetCore.Common.Sensors;
using SenseHat.DotNetCore.Common.Services;
using System.Drawing;
using System.Net;

#endregion

namespace SenseHat.DotNetCore.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenseHatController : ControllerBase
    {
        #region Fields

        private readonly ISenseHatService senseHatService;

        #endregion

        #region Constructor

        public SenseHatController(ISenseHatService senseHatService)
        {
            this.senseHatService = senseHatService;
        }

        #endregion

        #region GET

        [HttpGet]
        [ProducesResponseType(typeof(SensorReadings), (int)HttpStatusCode.OK)]
        public ActionResult<SensorReadings> Get()
        {
            return senseHatService.SensorReadings;
        }

        #endregion

        #region POST

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult SetColor(string colorName)
        {
            var color = Color.FromName(colorName);

            if (color.IsKnownColor)
            {
                senseHatService.Fill(color);

                return Accepted();
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion
    }
}
