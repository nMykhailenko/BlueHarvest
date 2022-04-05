using System;
using AutoMapper;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Mappings;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Models.ResponseModels;
using BlueHarvest.Modules.Transactions.Core.Domain.Entities;
using BlueHarvest.Shared.Application.Models.Enums.Transactions;
using BlueHarvest.Shared.Application.Models.RequestModels.Transactions;
using Bogus;
using FluentAssertions;
using Xunit;

namespace BlueHarvest.UnitTests.Modules.Transactions;

public class TransactionMappingProfileTests
{
    private readonly MapperConfiguration _mapperConfiguration;
    private readonly IMapper _sut;

    public TransactionMappingProfileTests()
    {
        _mapperConfiguration = new MapperConfiguration(
            cfg => cfg.AddProfile<TransactionMappingProfile>());
        _sut = _mapperConfiguration.CreateMapper();
    }

    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        // assert
        _mapperConfiguration.AssertConfigurationIsValid();
    }

    [Theory]
    [InlineData(TransactionOperation.Deposit)]
    [InlineData(TransactionOperation.Withdraw)]
    public void AutoMapper_ShouldMap_CreateTransactionRequest_To_Transaction_InProperWay(TransactionOperation operation)
    {
        // arrange
        var createTransactionRequest = new Faker<CreateTransactionRequest>()
            .StrictMode(true)
            .RuleFor(t => t.Amount, _ => _.Finance.Amount())
            .RuleFor(t => t.UserId, _ => Guid.NewGuid())
            .RuleFor(t => t.Operation, operation)
            .Generate();
    
        // act
        var actual = _sut.Map<Transaction>(createTransactionRequest);
        
        // assert
        actual.Should().NotBeNull();
        actual.Amount.Should().Be(createTransactionRequest.Amount);
        actual.UserId.Should().Be(createTransactionRequest.UserId);
        actual.Operation.ToString().Should().Be(createTransactionRequest.Operation.ToString());
    }
    
    [Theory]
    [InlineData(TransactionOperation.Deposit)]
    [InlineData(TransactionOperation.Withdraw)]
    public void AutoMapper_ShouldMap_Transaction_To_TransactionResponse_InProperWay(TransactionOperation operation)
    {
        // arrange
        var transaction = new Faker<Transaction>()
            .StrictMode(true)
            .RuleFor(t => t.Amount, _ => _.Finance.Amount())
            .RuleFor(t => t.UserId, _ => Guid.NewGuid())
            .RuleFor(t => t.Operation, operation)
            .RuleFor(t => t.Id, _ => Guid.NewGuid())
            .RuleFor(t => t.CreatedAt, _ => DateTimeOffset.UtcNow)
            .Generate();
    
        // act
        var actual = _sut.Map<TransactionResponse>(transaction);
        
        // assert
        actual.Should().NotBeNull();
        actual.Amount.Should().Be(transaction.Amount);
        actual.UserId.Should().Be(transaction.UserId);
        actual.Id.Should().Be(transaction.Id);
        actual.CreatedAt.Should().Be(transaction.CreatedAt);
        actual.Operation.Should().Be(transaction.Operation.ToString());
    }
}