//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace PC.Core.Auth.WebApi.Controllers
//{
//	[Route("api/[controller]")]
//	[Authorize(Policy = "DisneyBoss")]
//	public class ProductController
//	{
//		[HttpGet]
//		[Authorize(Policy = "DisneyUser")]
//		public IActionResult Get()
//		{
//			return new OkObjectResult(new [] 
//			{
//				new { Id = 1, Name = "Product01" },
//				new { Id = 2, Name = "Product02" },
//				new { Id = 3, Name = "Product03" }
//			});
//		}

//		[HttpPost]
//		public IActionResult AddProduct([FromBody] ProductRequest product)
//		{
//			return new OkObjectResult(new { Id = 4, Name = product.Name });
//		}
//	}

//	public class ProductRequest
//	{
//		public string Name { get; set; }
//	}
//}
