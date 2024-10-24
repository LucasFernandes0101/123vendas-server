using _123vendas.Domain.Entities;
using _123vendas.Domain.Enums;
using _123vendas.Domain.Exceptions;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace _123vendas.Application.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IBranchProductRepository _branchProductRepository;
    private readonly IValidator<Sale> _validator;
    private readonly ILogger<SaleService> _logger;

    public SaleService(ISaleRepository saleRepository,
                       ISaleItemRepository saleItemRepository,
                       IBranchProductRepository branchProductRepository,
                       IValidator<Sale> validator,
                       ILogger<SaleService> logger)
    {
        _saleRepository = saleRepository;
        _saleItemRepository = saleItemRepository;
        _branchProductRepository = branchProductRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Sale> CreateSaleAsync(Sale request)
    {
        try
        {
            var sale = new Sale
            {
                BranchId = request.BranchId,
                CustomerId = request.CustomerId,
                Date = DateTime.UtcNow,
                Items = new List<SaleItem>(),
                TotalAmount = 0
            };

            await ProcessSaleItemsAsync(sale, request.Items);

            await ValidateSaleAsync(sale);

            var savedSale = await _saleRepository.AddAsync(sale);

            await SaveSaleItemsAsync(savedSale.Id, sale.Items);

            return savedSale;
        }
        catch (Exception ex) when (ex is ValidationException || ex is NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while saving sale. Please try again later.", ex);
        }
    }

    public async Task<Sale> CancelSaleItemAsync(int saleId, int saleItemId)
    {
        try
        {
            var sale = await GetSaleWithItemsOrThrowAsync(saleId);

            var saleItem = sale?.Items?.FirstOrDefault(i => i.Id == saleItemId);

            if (saleItem is null)
                throw new NotFoundException($"Sale item with ID {saleItemId} not found in sale ID {saleId}.");

            ValidateSaleItemForCancellation(saleItem);

            CancelSaleItem(saleItem);

            await _saleItemRepository.UpdateAsync(saleItem);

            sale.TotalAmount = CalculateTotalAmount(sale.Items);

            await _saleRepository.UpdateAsync(sale);

            return sale;
        }
        catch (Exception ex) when (ex is NotFoundException || ex is SaleItemAlreadyCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while canceling the sale item.", ex);
        }
    }

    public async Task<List<Sale>> GetAllAsync(int? id,
                                              int? branchId,
                                              int? customerId,
                                              SaleStatus? status,
                                              DateTime? startDate,
                                              DateTime? endDate,
                                              int page = 1,
                                              int maxResults = 10)
    {
        try
        {
            if (page <= 0 || maxResults <= 0)
                throw new InvalidPaginationParametersException("Page number and max results must be greater than zero.");

            var criteria = BuildCriteria(id, branchId, customerId, status, startDate, endDate);

            var result = await _saleRepository.GetAsync(page, maxResults, criteria);

            return result.Items;
        }
        catch (Exception ex) when (ex is not InvalidPaginationParametersException)
        {
            throw new ServiceException("An error occurred while retrieving sales.", ex);
        }
    }

    public async Task<Sale?> GetByIdAsync(int id)
    {
        try
        {
            return await GetSaleWithItemsOrThrowAsync(id);
        }
        catch (Exception ex) when (ex is not NotFoundException)
        {
            throw new ServiceException("An error occurred while retrieving the sale.", ex);
        }
    }

    public async Task<Sale> UpdateSaleAsync(int saleId, Sale request)
    {
        try
        {
            var existingSale = await GetSaleOrThrowAsync(saleId);

            ValidateSaleForUpdate(existingSale);

            var updatedSale = UpdateSaleProperties(existingSale, request);

            await ValidateSaleAsync(updatedSale);

            return await _saleRepository.UpdateAsync(updatedSale);
        }
        catch (Exception ex) when (ex is NotFoundException || ex is SaleAlreadyCanceledException || ex is ValidationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while updating the sale.", ex);
        }
    }

    public async Task<Sale> CancelSaleAsync(int saleId)
    {
        try
        {
            var sale = await _saleRepository.GetByIdAsync(saleId);

            if (sale is null)
                throw new NotFoundException($"Sale with ID {saleId} not found.");

            if (sale.Status == SaleStatus.Canceled)
                throw new SaleAlreadyCanceledException($"This sale is already canceled.");

            sale.Status = SaleStatus.Canceled;
            sale.CancelledAt = DateTime.UtcNow;

            var updatedSale = await _saleRepository.UpdateAsync(sale);

            return updatedSale;
        }
        catch (Exception ex) when (ex is NotFoundException || ex is InvalidOperationException || ex is SaleAlreadyCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while canceling the sale. Please try again later.", ex);
        }
    }

    public async Task DeleteAsync(int saleId)
    {
        try
        {
            var existingSale = await GetSaleOrThrowAsync(saleId);

            await _saleRepository.DeleteAsync(existingSale);
        }
        catch (Exception ex) when (ex is not NotFoundException)
        {
            throw new ServiceException("An error occurred while deleting the sale. Please try again later.", ex);
        }
    }

    #region CreateSale

    private async Task ProcessSaleItemsAsync(Sale sale, List<SaleItem> items)
    {
        short sequence = 1;

        foreach (var item in items)
        {
            var saleItem = await ProcessSaleItemAsync(sale.BranchId, item, sequence);
            sale.Items.Add(saleItem);
            sale.TotalAmount += saleItem.Price;

            sequence++;
        }
    }

    private async Task<SaleItem> ProcessSaleItemAsync(int branchId, SaleItem requestItem, short sequence)
    {
        var branchProduct = await GetBranchProductOrThrowAsync(branchId, requestItem.ProductId);

        var saleItem = new SaleItem
        {
            ProductId = branchProduct.ProductId,
            ProductName = branchProduct.ProductName,
            UnitPrice = branchProduct.Price,
            Quantity = requestItem.Quantity,
            Discount = requestItem.Discount ?? 0,
            Sequence = sequence
        };

        saleItem.Price = CalculateItemPrice(saleItem);

        return saleItem;
    }

    private decimal CalculateItemPrice(SaleItem item)
    {
        var discountMultiplier = 1 - (item.Discount / 100 ?? 0);
        return item.UnitPrice * item.Quantity * discountMultiplier;
    }

    private async Task SaveSaleItemsAsync(int saleId, List<SaleItem> items)
    {
        foreach (var saleItem in items)
        {
            saleItem.SaleId = saleId;
            await _saleItemRepository.AddAsync(saleItem);
        }
    }

    # endregion

    private void ValidateSaleForUpdate(Sale sale)
    {
        if (sale.Status == SaleStatus.Canceled)
            throw new SaleAlreadyCanceledException("Cannot update a canceled sale.");
    }

    private Sale UpdateSaleProperties(Sale existingSale, Sale request)
    {
        var updatedSale = existingSale;

        updatedSale.Status = request.Status;
        updatedSale.Date = request.Date;
        updatedSale.CustomerId = request.CustomerId;
        updatedSale.BranchId = request.BranchId;
        updatedSale.TotalAmount = request.TotalAmount;

        return updatedSale;
    }

    private void ValidateSaleItemForCancellation(SaleItem saleItem)
    {
        if (saleItem.IsCancelled)
            throw new SaleItemAlreadyCanceledException("This item is already cancelled.");
    }

    private void CancelSaleItem(SaleItem saleItem)
    {
        saleItem.IsCancelled = true;
        saleItem.CancelledAt = DateTime.UtcNow;
    }

    private decimal CalculateTotalAmount(List<SaleItem> items)
    {
        return items.Where(item => !item.IsCancelled).Sum(item => item.Price);
    }

    private async Task<Sale> GetSaleOrThrowAsync(int saleId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale is null)
            throw new NotFoundException($"Sale with ID {saleId} not found.");

        return sale;
    }

    private async Task<Sale?> GetSaleWithItemsOrThrowAsync(int id)
    {
        var sale = await _saleRepository.GetWithItemsByIdAsync(id);

        if (sale is null)
            throw new NotFoundException($"Sale with ID {id} not found.");

        return sale;
    }

    private async Task<BranchProduct> GetBranchProductOrThrowAsync(int branchId, int productId)
    {
        var branchProduct = (await _branchProductRepository.GetAsync(1, 1,
            p => p.IsActive && p.BranchId == branchId && p.ProductId == productId))
            .Items.FirstOrDefault();

        if (branchProduct is null)
            throw new NotFoundException($"Product ID {productId} not found or inactive in branch ID {branchId}.");

        return branchProduct;
    }

    private async Task ValidateSaleAsync(Sale sale)
    {
        var validationResult = await _validator.ValidateAsync(sale);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }

    private Expression<Func<Sale, bool>> BuildCriteria(int? id,
                                                          int? branchId,
                                                          int? customerId,
                                                          SaleStatus? status,
                                                          DateTime? startDate,
                                                          DateTime? endDate)
    {
        return b =>
            (!id.HasValue || b.Id == id.Value) &&
            (!branchId.HasValue || b.BranchId == branchId.Value) &&
            (!customerId.HasValue || b.CustomerId == customerId.Value) &&
            (!status.HasValue || b.Status == status.Value) &&
            (!startDate.HasValue || b.CreatedAt >= startDate.Value) &&
            (!endDate.HasValue || b.CreatedAt <= endDate.Value);
    }
}