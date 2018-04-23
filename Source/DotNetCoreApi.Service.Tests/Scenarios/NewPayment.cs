namespace DotNetCoreApi.Service.Tests.Scenarios
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Api;
    using AutoFixture;
    using DotNetCore.Contracts.Rest;
    using DotNetCoreApi.Service.Tests.Attributes;
    using FluentAssertions;
    using NUnit.Framework;
    using TestDoubles;
    using TestStack.BDDfy;

    [TestFixture]
    public class NewPayment
    {
        
        private Payment payment;
        private Guid paymentReference;
        private HttpResponseMessage response;

        [Test]
        public void CreatingANewValidPayment()
        {
            this.Given(_ => _.AValidNewPayment())
                .When(_ => _.TheCallToCreateAPaymentIsMade())
                .Then(_ => _.ThePaymentIsCreated())
                .And(_ => _.AndTheRedirectUriIsValid())
                .BDDfy();
        }

        [Test]
        public void CreateExistingPayment()
        {
            this.Given(_ => _.AnExistingPayment())
                .When(_ => _.TheCallToCreateAPaymentIsMade())
                .Then(_ => _.AConflictIsReturned())
                .BDDfy();
        }

        [Test]
        [IgnoreIfOutOfProcess]
        public void DatabaseUnavailableWhenCreatingPayment()
        {
            this.Given(_ => _.AValidNewPayment())
                .And(_ => _.TheDatabaseIsUnavailable())
                .When(_ => _.TheCallToCreateAPaymentIsMade())
                .Then(_ => _.ServiceUnavailableResponseIsReturned())
                .BDDfy();
        }

        private void AnExistingPayment()
        {
            this.AValidNewPayment();
            this.TheCallToCreateAPaymentIsMade();
        }

        private void AValidNewPayment()
        {
            this.paymentReference = Guid.NewGuid();
            this.payment = new Fixture().Create<Payment>();
        }

        private void TheDatabaseIsUnavailable()
        {
            this.paymentReference = Guid.Parse(CosmosDbStub.DatabaseUnavailableControlReference);
        }

        private void TheCallToCreateAPaymentIsMade()
        {
            using (var apiBuilder = new ApiFactory())
            {
                var api = apiBuilder.Create();
                this.response = api.CreatePayment(this.paymentReference.ToString(), this.payment).Result;
            }
        }

        private void ThePaymentIsCreated()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        private void AConflictIsReturned()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        private void ServiceUnavailableResponseIsReturned()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
        }

        private void AndTheRedirectUriIsValid()
        {
            var paymentRedirect = this.response.Content.ReadAs<PaymentRedirect>();
            var isValidUri = Uri.TryCreate(paymentRedirect.RedirectPath, UriKind.Absolute, out _);
            Assert.True(isValidUri);
        }
    }
}
