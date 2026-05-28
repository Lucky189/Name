using AspNetExample.Domain.Repositories;
using AspNetExample.Service;
using AspNetExample.Service.Services;
using Moq;
using Xunit;

using MyDomainModel = AspNetExample.Domain.Models.MyModel;
using ServiceModel = AspNetExample.Service.Models.MyModel;

public class ServiceTests
{
    private readonly Mock<IRepository> _repoMock;
    private readonly Mock<IModelDescriptionProvider> _descMock;
    private readonly Service _service;

    public ServiceTests()
    {
        _repoMock = new Mock<IRepository>();
        _descMock = new Mock<IModelDescriptionProvider>();

        _service = new Service(_repoMock.Object, _descMock.Object);
    }

    [Fact]
    public void CreateModel_ShouldReturnConvertedModel()
    {
        var domainModel = new MyDomainModel("data", Guid.NewGuid());

        _repoMock
            .Setup(r => r.CreateModel("data"))
            .Returns(domainModel);

        var result = _service.CreateModel("data");

        Assert.NotNull(result);
        Assert.Equal(domainModel.Data, result.Data);
    }

    [Fact]
    public void GetModel_ShouldReturnModelWithDescription()
    {
        var id = Guid.NewGuid();
        var domainModel = new MyDomainModel("data", id);

        _repoMock
            .Setup(r => r.GetModel(id))
            .Returns(domainModel);

        _descMock
            .Setup(d => d.GetDescription(id))
            .Returns("test description");

        var result = _service.GetModel(id);

        Assert.NotNull(result);
        Assert.Equal("test description", result.Description);
        Assert.Equal("data", result.Data);
    }

    [Fact]
    public void UpdateModel_ShouldReturnUpdatedModel()
    {
        var id = Guid.NewGuid();
        var model = new ServiceModel("data", id);
        var domainModel = new MyDomainModel("data", id);

        _repoMock
            .Setup(r => r.UpdateModel(It.IsAny<MyDomainModel>()))
            .Returns(domainModel);

        var result = _service.UpdateModel(model);

        Assert.NotNull(result);
        Assert.Equal(model.Data, result.Data);
    }

    [Fact]
    public void DeleteModel_ShouldReturnTrue()
    {
        var id = Guid.NewGuid();

        _repoMock
            .Setup(r => r.DeleteModel(id))
            .Returns(true);

        var result = _service.DeleteModel(id);

        Assert.True(result);
    }
}