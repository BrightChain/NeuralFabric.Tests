using System.Configuration;
using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralFabric.Models;
using Moq;
using Bogus;
using Bogus.DataSets;
using Microsoft.Extensions.DependencyInjection;
using ConfigurationSection = Microsoft.Extensions.Configuration.ConfigurationSection;

namespace NeuralFabric.Tests;

[TestClass]
public class TapestryTest
{
    private ILoggerFactory _loggerFactory;
    private IConfiguration _configuration;
    private IServiceCollection _services;
    private ILogger _logger;

    [TestInitialize]
    public void PreTestSetup()
    {
        var mockConfiguration = new Mock<IConfiguration>();

        this._configuration = mockConfiguration.Object;
        this._services = new Mock<IServiceCollection>().Object;
        this._logger = new Mock<ILogger>().Object;

        Mock<IConfigurationSection> mockPathSection = new Mock<IConfigurationSection>();
        mockPathSection.Setup(x => x.Value).Returns(Path.GetTempPath());

        var mockNodeSection = new Mock<IConfigurationSection>();
        mockNodeSection.Setup(x => x.GetSection(It.Is<string>(k => k == "BasePath"))).Returns(mockPathSection.Object);

        mockConfiguration.Setup(x => x.GetSection(It.Is<string>(k => k == "NodeOptions"))).Returns(mockNodeSection.Object);

        var factoryMock = new Mock<ILoggerFactory>();

        factoryMock
            .SetupAllProperties()
            .Setup(f => f.CreateLogger(It.IsAny<string>())).Returns(this._logger);

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

