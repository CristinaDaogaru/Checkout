using Checkout.Api.BussinessLogic.Dtos.Request;
using Checkout.Api.BussinessLogic.EndpointHandlers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutApi.WebApp.Controllers
{
    [Route("/baskets")]
    public class BasketController : Controller
    {
        private readonly IBasketHandler _basketHandler;
        private readonly IAddItemHandler _addItemHandler;

        public BasketController(IBasketHandler basketHandler, IAddItemHandler addItemHandler)
        {
            _basketHandler = basketHandler;
            _addItemHandler = addItemHandler;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NewBasketDto basket)
        {
            var result = await _basketHandler.AddNewBasket(basket);
            if (!result.IsFailed)
            {
                return Ok(result);
            }

            return BadRequest(CreateProblemDetails(result.Error));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var result = await _basketHandler.GetBasket(id);
            if (!result.IsFailed)
            {
                return Ok(result);
            }

            return BadRequest(CreateProblemDetails(result.Error));
        }

        [HttpPut]
        [Route("/{id:int}/article-line")]
        public async Task<ActionResult> AddItems([FromRoute] int id, [FromBody] NewItemDto item)
        {
            var result = await _addItemHandler.AddNewItemInBasket(item, id);
            if (!result.IsFailed)
            {
                return Ok(result);
            }

            return BadRequest(CreateProblemDetails(result.Error));
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult> Checkout([FromRoute] int id, [FromBody] CheckoutDto checkout)
        {
            var result = await _basketHandler.Checkout(checkout, id);
            if (!result.IsFailed)
            {
                return Ok(result);
            }

            return BadRequest(CreateProblemDetails(result.Error));
        }
        private ProblemDetails CreateProblemDetails(string error)
        {
            return new ProblemDetails()
            {
                Status = 400,
                Detail = error,
                Title = "BadRequest",
                Type = "https://httpstatuses.com/400",
            };
        }
    }
}
