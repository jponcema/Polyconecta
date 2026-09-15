using PolyConecta.Api.Common;
using Xunit;

namespace PolyConecta.IntegrationTests;

public class ApiResponseTests
{
    [Fact]
    public void ApiResponse_Ok_ReturnsSuccessWithData()
    {
        var response = ApiResponse<string>.Ok("test-payload");
        Assert.True(response.Success);
        Assert.Equal("test-payload", response.Data);
        Assert.Empty(response.Errors);
        Assert.False(string.IsNullOrEmpty(response.TraceId));
    }

    [Fact]
    public void ApiResponse_Fail_ReturnsFailureWithErrors()
    {
        var response = ApiResponse<string>.Fail("invalid operation");
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.Single(response.Errors);
        Assert.Equal("invalid operation", response.Errors[0]);
    }

    [Fact]
    public void PagedResult_CalculatesTotalPagesCorrectly()
    {
        var items = new List<string> { "item1", "item2" };
        var paged = new PagedResult<string>(items, pageNumber: 1, pageSize: 10, totalCount: 25);

        Assert.Equal(3, paged.TotalPages);
        Assert.True(paged.HasNextPage);
        Assert.False(paged.HasPreviousPage);
    }
}
