namespace DotNetCoreApi.Service.Tests.Scenarios
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Api;
    using AutoFixture;
    using DotNetCore.Contracts.Rest;
    using FluentAssertions;
    using NUnit.Framework;
    using TestDoubles;
    using TestStack.BDDfy;

    [TestFixture]
    public class GetPayment
    {
        private Payment payment;
        private Guid paymentReference;
        private HttpResponseMessage response;

        [Test]
        public void GettingAnExistingPayment()
        {
            this.Given(_ => _.AnExistingPayment())
                .When(_ => _.TheCallToGetThePaymentIsMade())
                .Then(_ => _.AnOkResponseIsReturned())
                .And(_ => _.ThePaymentIsReturnedCorrectly())
                .BDDfy();
        }

        [Test]
        public void GettingANonExistentPayment()
        {
            this.Given(_ => _.APaymentDoesNotExist())
                .When(_ => _.TheCallToGetThePaymentIsMade())
                .Then(_ => _.ANotFoundResponseIsReturned())
                .BDDfy();
        }

        [Test]
        [IgnoreIfOutOfProcess]
        public void DatabaseUnavailableWhenCreatingPayment()
        {
            this.Given(_ => _.AnExistingPayment())
                .And(_ => _.TheDatabaseIsUnavailable())
                .When(_ => _.TheCallToGetThePaymentIsMade())
                .Then(_ => _.ServiceUnavailableResponseIsReturned())
                .BDDfy();
        }

        private void AnExistingPayment()
        {
            this.paymentReference = Guid.NewGuid();
            this.payment = new Fixture().Create<Payment>();
            using (var apiBuilder = new ApiFactory())
            {
                var api = apiBuilder.Create();
                var result = api.CreatePayment(this.paymentReference.ToString(), this.payment).Result;
                if (!result.IsSuccessStatusCode)
                {
                    Assert.Fail("Could not create the payment as part of the setup.");
                }

            }
        }

        private void APaymentDoesNotExist()
        {
            this.paymentReference = Guid.NewGuid();
        }

        private void TheCallToGetThePaymentIsMade()
        {
            using (var apiBuilder = new ApiFactory())
            {
                var api = apiBuilder.Create();
                this.response = api.GetPayment(this.paymentReference.ToString()).Result;
            }
        }

        private void TheDatabaseIsUnavailable()
        {
            this.paymentReference = Guid.Parse(CosmosDbStub.DatabaseUnavailableControlReference);
        }

        private void AnOkResponseIsReturned()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private void ThePaymentIsReturnedCorrectly()
        {
            var paymentRespone = this.response.Content.ReadAs<Payment>();
            paymentRespone.Should().BeEquivalentTo(this.payment);
        }

        private void ANotFoundResponseIsReturned()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private void ServiceUnavailableResponseIsReturned()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
        }
    }
}
