using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context = context;

        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {

            var thing = _context.Products.Find(42);

            if (thing == null)
            {
                return NotFound(new Errors.ApiResponse(404));
            }

            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {

            var thing = _context.Products.Find(42);
            //null reference since thing is assigned to null
            var thingToReturn = thing.ToString();

            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new Errors.ApiResponse(400));
        }

        //we will pass a string instead of an integer to test for a validation error
        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }


    }
}