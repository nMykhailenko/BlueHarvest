using AutoMapper;
using BlueHarvest.Modules.Transactions.Core.Application.Common.Contracts.Database;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Contracts;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Models.ResponseModels;
using BlueHarvest.Modules.Transactions.Core.Domain.Entities;
using BlueHarvest.Shared.Application.Models.RequestModels.Transactions;

namespace BlueHarvest.Modules.Transactions.Core.Application.Transactions
{
	public class TransactionService : ITransactionService
	{
		private readonly IMapper _mapper;
		private readonly ITransactionRepository _transactionRepository;
		public TransactionService(
			IMapper mapper,
			ITransactionRepository transactionRepository)
		{
			_mapper = mapper;
			_transactionRepository = transactionRepository;
		}

		public async Task<TransactionResponse> CreateTransactionAsync(CreateTransactionRequest request, CancellationToken ct)
		{
			var transactionToAdd = _mapper.Map<Transaction>(request);

			var addedTransaction = await _transactionRepository.AddAsync(transactionToAdd, ct);
			await _transactionRepository.SaveChangesAsync(ct);

			var response = _mapper.Map<TransactionResponse>(addedTransaction);

			return response;
		}

		public async Task<IReadOnlyCollection<TransactionResponse>> GetTransactionsForUserAsync(Guid userId, CancellationToken ct)
		{
			var transactions = await _transactionRepository.GetByUserId(userId, ct);

			var transactionResponses = transactions
				.Select(x => _mapper.Map<TransactionResponse>(x)).ToList();

			return transactionResponses;
		}
	}
}
