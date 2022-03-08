using System.IO;
using Bogus.DataSets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralFabric.Models;

namespace NeuralFabric.Tests;

[TestClass]
public class TapestryTests
{
    private IConfiguration _configuration;
    private ILogger _logger;
    private ILoggerFactory _loggerFactory;
    private IServiceCollection _services;

    [TestInitialize]
    public void PreTestSetup()
    {
        var mockConfiguration = new Mock<IConfiguration>();

        this._configuration = mockConfiguration.Object;
        this._services = new Mock<IServiceCollection>().Object;
        this._logger = new Mock<ILogger>().Object;

        var mockPathSection = new Mock<IConfigurationSection>();
        mockPathSection.Setup(expression: x => x.Value).Returns(value: Path.GetTempPath());

        var mockNodeSection = new Mock<IConfigurationSection>();
        mockNodeSection.Setup(expression: x => x.GetSection(It.Is<string>(k => k == "BasePath"))).Returns(value: mockPathSection.Object);

        mockConfiguration.Setup(expression: x => x.GetSection(It.Is<string>(k => k == "NodeOptions")))
            .Returns(value: mockNodeSection.Object);

        var factoryMock = new Mock<ILoggerFactory>();

        factoryMock
            .SetupAllProperties()
            .Setup(expression: f => f.CreateLogger(It.IsAny<string>())).Returns(value: this._logger);

        this._loggerFactory = factoryMock.Object;
    }

    [TestMethod]
    public void TapestryInitializationTest()
    {
        var collectionName = $"{new Lorem().Word().ToLower()}Collection";
        var tapestry = new Tapestry(
            logger: this._logger,
            configuration: this._configuration,
            collectionName: "test");
    }
}
