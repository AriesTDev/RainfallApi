using Rainfall.Api.Repositories;

namespace Rainfall.Api.Test
{
	[TestFixture]
	public class RainfallControllerTests
	{
		private RainfallController _controller;
		private Mock<IRainfallRepository> _mockRepository;

		[SetUp]
		public void Setup()
		{
			_mockRepository = new Mock<IRainfallRepository>();
			_controller = new RainfallController(_mockRepository.Object);
		}


		[Test]
		public async Task GetRainfallReadings_ValidStationId_ReturnsListOfRainfallReadings()
		{
			// Arrange
			var validStationId = "123"; // Set up valid input data
			var expectedReadings = new BaseResponse<RainfallReadingResponse>() { StatusCode = Enums.StatusCode.Success, Result = new RainfallReadingResponse() { Readings = new List<RainfallReading>() } }; // Set up expected readings

			_mockRepository.Setup(repo => repo.GetRainfallByStationId(validStationId, 0))
						   .ReturnsAsync(expectedReadings);

			// Act
			IActionResult actionResult = await _controller.GetRainfallByStationId(validStationId, 0);

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(actionResult); // Check if the result is an OkObjectResult

			var okObjectResult = (OkObjectResult)actionResult;
			var model = (RainfallReadingResponse)okObjectResult.Value; // Get the value of the result

			Assert.IsNotNull(model); // Check if the value is not null or empty
		}

		[Test]
		public async Task GetRainfallReadings_InvalidStationId_ReturnsBadRequest()
		{
			// Arrange
			var invalidStationId = ""; // Set up invalid input data
			var expectedReadings = new BaseResponse<RainfallReadingResponse>() { StatusCode = Enums.StatusCode.BadRequest, Result = new RainfallReadingResponse() { Readings = new List<RainfallReading>() } }; // Set up expected readings

			_mockRepository.Setup(repo => repo.GetRainfallByStationId(invalidStationId, 0))
						   .ReturnsAsync(expectedReadings);

			// Act
			IActionResult actionResult = await _controller.GetRainfallByStationId(invalidStationId, 0);

			// Assert
			Assert.IsInstanceOf<BadRequestObjectResult>(actionResult); // Check if the result is a BadRequestResult
		}

		[Test]
		public async Task GetRainfallReadings_NoReadingsFound_ReturnsNotFound()
		{
			// Arrange
			var nonExistentStationId = "999"; // Set up non-existent input data
			var expectedReadings = new BaseResponse<RainfallReadingResponse>() { StatusCode = Enums.StatusCode.NotFound, Result = new RainfallReadingResponse() { Readings = new List<RainfallReading>() } }; // Set up expected readings

			_mockRepository.Setup(repo => repo.GetRainfallByStationId(nonExistentStationId, 0))
						   .ReturnsAsync(expectedReadings);

			// Act
			IActionResult actionResult = await _controller.GetRainfallByStationId(nonExistentStationId, 0);

			// Assert
			Assert.IsInstanceOf<NotFoundObjectResult>(actionResult); // Check if the result is a NotFoundResult
		}

		[Test]
		public async Task GetRainfallReadings_ValidStationIdButEmpty_ReturnsEmptyList()
		{
			// Arrange
			var emptyStationId = "456"; // Set up station ID with no readings
			var expectedReadings = new BaseResponse<RainfallReadingResponse>() { StatusCode = Enums.StatusCode.Success, Result = new RainfallReadingResponse() { Readings = new List<RainfallReading>() } }; // Set up expected readings

			_mockRepository.Setup(repo => repo.GetRainfallByStationId(emptyStationId, 0))
						   .ReturnsAsync(expectedReadings);

			// Act
			IActionResult actionResult = await _controller.GetRainfallByStationId(emptyStationId, 0);

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(actionResult); // Check if the result is an OkObjectResult

			var okObjectResult = (OkObjectResult)actionResult;
			var model = (RainfallReadingResponse)okObjectResult.Value; // Get the value of the result

			Assert.IsNotNull(model.Readings); // Check if the value is not null
			Assert.IsEmpty(model.Readings); // Check if the value is an empty list
		}

	}
}
