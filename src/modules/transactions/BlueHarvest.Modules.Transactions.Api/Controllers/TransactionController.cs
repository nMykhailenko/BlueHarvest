using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Contracts;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Models.ResponseModels;
using BlueHarvest.Shared.Application.Models.RequestModels.Transactions;
using BlueHarvest.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlueHarvest.Modules.Transactions.Api.Controllers
{ 
	[ApiVersion("1.0")]
	public class TransactionController : BaseController
	{
		private readonly ITransactionService _transactionService;
		public TransactionController(ITransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		/// <summary>
		/// Create transaction.
		/// </summary>
		/// <param name="request">Create user request.</param>
		/// <param name="ct">Cancellation token</param>
		/// <returns></returns>
		[HttpPost($"{BaseApiPath}/{TransactionsModule.ModulePath}")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionResponse))]
		public async Task<IActionResult> AddTransaction([FromBody] CreateTransactionRequest request, CancellationToken ct)
		{
			var response = await _transactionService.CreateTransactionAsync(request, ct);

			return Ok(response);
		}

		/// <summary>
		/// Get transactions for user.
		/// </summary>
		[HttpGet($"{BaseApiPath}/users/"+"{userId:guid}/transactions")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionResponse))]
		public async Task<IActionResult> GetTransactions([FromRoute]Guid userId, CancellationToken ct)
		{
			var response = await _transactionService.GetTransactionsForUserAsync(userId, ct);

			return Ok(response);
		}
	}
}
