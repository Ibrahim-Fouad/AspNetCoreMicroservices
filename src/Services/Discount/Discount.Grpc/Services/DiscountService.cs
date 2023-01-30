using AutoMapper;
using Discount.Grpc;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IMapper _mapper;

    public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public override async Task<CopounModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.CopounModel);
        var result = await _discountRepository.CreateDiscount(coupon);
        if (result is 0)
            throw new RpcException(new Status(StatusCode.Unknown,
                "Unknown exception has occurred, Can not create coupon"));
        request.CopounModel.Id = result;
        return request.CopounModel;
    }

    public override async Task<CopounModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _discountRepository.GetDiscount(request.ProductName);
        if (coupon is null || coupon.Id is 0)
            throw new RpcException(new Status(StatusCode.NotFound, "Coupon is not found"));
        var result = _mapper.Map<CopounModel>(coupon);
        return result;
    }

    public override async Task<DeleteDiscountModel> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var result = await _discountRepository.DeleteDiscount(request.ProductName);
        if (result is false)
            throw new RpcException(new Status(StatusCode.Unknown, "Unknown exception has occurred"));

        return new DeleteDiscountModel
        {
            Success = result
        };
    }

    public override async Task<CopounModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.CopounModel);
        var result = await _discountRepository.UpdateDiscount(coupon);
        if (result is false)
            throw new RpcException(new Status(StatusCode.Unknown, "Unknown exception has occurred"));

        return request.CopounModel;
    }
}