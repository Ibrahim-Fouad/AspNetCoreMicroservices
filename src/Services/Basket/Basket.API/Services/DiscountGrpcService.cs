using Discount.Grpc.Protos;
using Grpc.Core;

namespace Basket.API.Services;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
    {
        _discountProtoServiceClient = discountProtoServiceClient;
    }

    public Task<CopounModel[]> GetDiscountsAsync(params string[] productNames)
    {
        var models = productNames.Select(productName => new GetDiscountRequest()
        {
            ProductName = productName
        }).ToArray();

        var requests = models.Select(model => _discountProtoServiceClient.GetDiscountAsync(model)).ToArray();

        return Task.WhenAll(requests.Select(r => r.ResponseAsync));
    }

    public Task<CopounModel[]> GetDiscountsAsync(IEnumerable<string> productNames) => GetDiscountsAsync(productNames.ToArray());
}