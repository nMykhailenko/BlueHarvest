using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueHarvest.Modules.Transactions.Core.Application.Common.Contracts.Database;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Contracts;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Mappings;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Models.ResponseModels;
using BlueHarvest.Modules.Transactions.Core.Domain.Entities;
using BlueHarvest.Shared.Application.Models.Enums.Transactions;
using BlueHarvest.Shared.Application.Models.RequestModels.Transactions;
using Bogus;
using FluentAssertions;
using Moq;
using Xunit;

namespace BlueHarvest.UnitTests.Modules.Transactions;

public class TransactionServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ITransactionRepository> _transactionRepositoryMock;

    private ITransactionService _sut;

    public TransactionServiceTests()
    {
        var mapperConfiguration = new MapperConfiguration(
            cfg => cfg.AddProfile<TransactionMappingProfile>());
        _mapper = mapperConfiguration.CreateMapper();

        _transactionRepositoryMock = new Mock<ITransactionRepository>();
        _sut = new TransactionService(_mapper, _transactionRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateTransactionAsync_ShouldReturn_CreatedTransaction()
    {
        // arrange
        var createTransactionRequest = new Faker<CreateTransactionRequest>()
            .StrictMode(true)
            .RuleFor(t => t.Amount, _ => _.Finance.Amount())
            .RuleFor(t => t.UserId, _ => Guid.NewGuid())
            .RuleFor(t => t.Operation, TransactionOperation.Deposit)
            .Generate();

        var transactionToAdd = _mapper.Map<Transaction>(createTransactionRequest);
        transactionToAdd.Id = Guid.NewGuid();

        var expectedTransaction = _mapper.Map<TransactionResponse>(transactionToAdd);

        _transactionRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Transaction>(), CancellationToken.None))
            .ReturnsAsync(transactionToAdd);
        _transactionRepositoryMock
            .Setup(x => x.SaveChangesAsync(CancellationToken.None))
            .Returns(Task.CompletedTask);

        // act
        var actual = await _sut.CreateTransactionAsync(createTransactionRequest, CancellationToken.None);

        // assert
        actual.Should().Be(expectedTransaction);
        _transactionRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Transaction>(), CancellationToken.None), Times.Once);
        _transactionRepositoryMock.Verify(
            x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetTransactionsForUserAsync_ShouldReturn_ListOfTransactions()
    {
        // arrange
        var userId = Guid.NewGuid();
        var transactions = new Faker<Transaction>()
            .StrictMode(true)
            .RuleFor(t => t.Amount, _ => _.Finance.Amount())
            .RuleFor(t => t.UserId, _ => userId)
            .RuleFor(t => t.Operation, TransactionOperation.Deposit)
            .RuleFor(t => t.Id, _ => Guid.NewGuid())
            .RuleFor(t => t.CreatedAt, _ => DateTimeOffset.UtcNow)
            .Generate(5);

        _transactionRepositoryMock
            .Setup(x => x.GetByUserId(userId, CancellationToken.None))
            .ReturnsAsync(transactions);

        var expectedTransactions = transactions
            .Select(x => _mapper.Map<TransactionResponse>(x));

        // act
        var actual = await _sut.GetTransactionsForUserAsync(userId, CancellationToken.None);

        // assert
        actual.All(x => x.UserId == userId).Should().BeTrue();
        actual.Should().BeEquivalentTo(expectedTransactions);
    }
}