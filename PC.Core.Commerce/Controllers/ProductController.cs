using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PC.Core.Commerce.Controllers
{
	[Authorize(Policy = "DisneyBoss")]
	[Route("api/[controller]")]
	public class ProductController
	{
		[Authorize(Policy = "DisneyUser")]
		[HttpGet]
		public IActionResult Get()
		{
			return new OkObjectResult(new [] 
			{
				new { Id = 1, Name = "Product01" },
				new { Id = 2, Name = "Product02" },
				new { Id = 3, Name = "Product03" }
			});
		}
		
		[HttpPost]
		public IActionResult AddProduct([FromBody] ProductRequest product)
		{
			return new OkObjectResult(new { Id = 4, Name = product.Name });
		}
	}

	public class ProductRequest
	{
		public string Name { get; set; }
	}
}
