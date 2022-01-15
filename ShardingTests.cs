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
public class ShardingTests
{
    [TestMethod]
    public void ItGeneratesShardsFromDataTest()
    {

    }
}
