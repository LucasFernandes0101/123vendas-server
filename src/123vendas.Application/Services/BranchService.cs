using _123vendas.Application.Configurations;
using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;
using _123vendas.Domain.Exceptions;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace _123vendas.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _repository;
        private readonly IValidator<Branch> _validator;
        private readonly ILogger<BranchService> _logger;

        public BranchService(IBranchRepository repository, IValidator<Branch> validator, ILogger<BranchService> logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Branch> CreateAsync(Branch request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                return await _repository.AddAsync(request);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while creating a branch.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var branch = await _repository.GetByIdAsync(id);
                if (branch is null)
                    throw new NotFoundException($"Branch with ID {id} not found.");

                await _repository.DeleteAsync(branch);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while deleting the branch.", ex);
            }
        }

        public async Task<List<Branch>> GetAllAsync(int? id, bool? isActive, string? name)
        {
            try
            {
                Expression<Func<Branch, bool>> criteria = b => true;

                if (id.HasValue && id.Value > 0)
                {
                    criteria = criteria.And(b => b.Id == id.Value);
                }

                if (isActive.HasValue)
                {
                    criteria = criteria.And(b => b.IsActive == isActive.Value);
                }

                if (!string.IsNullOrWhiteSpace(name))
                {
                    criteria = criteria.And(b => b.Name.Contains(name));
                }

                var result = await _repository.GetAsync(criteria);

                return result.Items;
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while retrieving branches.", ex);
            }
        }


        public async Task<Branch> GetByIdAsync(int id)
        {
            try
            {
                var branch = await _repository.GetByIdAsync(id);
                if (branch == null)
                {
                    throw new NotFoundException($"Branch with ID {id} not found.");
                }

                return branch;
            }
            catch (Exception ex)
            {
                throw new BaseException("An error occurred while retrieving the branch.", ex);
            }
        }

        public async Task<Branch> UpdateAsync(int id, Branch request)
        {
            try
            {
                // Validação da entidade
                var validationResult = await _validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                var existingBranch = await _repository.GetByIdAsync(id);
                if (existingBranch == null)
                {
                    throw new NotFoundException($"Branch with ID {id} not found.");
                }

                // Atualizar propriedades da branch existente
                existingBranch.Name = request.Name;
                existingBranch.Address = request.Address;
                existingBranch.Phone = request.Phone;
                existingBranch.IsActive = request.IsActive;

                return await _repository.UpdateAsync(existingBranch);
            }
            catch (ValidationException ex)
            {
                throw ex;
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                throw; // Re-lançar a exceção
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the branch.");
                throw new BaseException("An error occurred while updating the branch.", ex);
            }
        }
    }
}
