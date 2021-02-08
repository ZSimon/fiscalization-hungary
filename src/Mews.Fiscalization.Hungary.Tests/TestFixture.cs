using Mews.Fiscalization.Core.Model;
using Mews.Fiscalization.Hungary.Models;
using System;

namespace Mews.Fiscalization.Hungary.Tests
{
    public static class TestFixture
    {
        private static readonly Login Login = Login.Create(Environment.GetEnvironmentVariable("login") ?? "dqvqrztyce5tpbc").Success.Get();
        private static readonly string Password = Environment.GetEnvironmentVariable("password") ?? "StudioLurdy";
        private static readonly SigningKey SigningKey = SigningKey.Create(Environment.GetEnvironmentVariable("signing_key") ?? "29-8287-2620a6417b53399QGN2PRMS5").Success.Get();
        private static readonly TaxpayerIdentificationNumber TaxPayerId = TaxpayerIdentificationNumber.Create(Countries.Hungary, Environment.GetEnvironmentVariable("tax_payer_id") ?? "12311383").Success.Get();
        private static readonly EncryptionKey EncryptionKey = EncryptionKey.Create(Environment.GetEnvironmentVariable("encryption_key") ?? "db85399QGN2PU93Z").Success.Get();

        public static NavClient GetNavClient()
        {
            var technicalUser = new TechnicalUser(
                login: Login,
                password: Password,
                signingKey: SigningKey,
                taxId: TaxPayerId,
                encryptionKey: EncryptionKey
            );
            var softwareIdentification = new SoftwareIdentification(
                id: "108366532106649392",
                name: "Dynamics NAV",
                type: SoftwareType.LocalSoftware,
                mainVersion: "2009 R2",
                developerName: "Simon",
                developerContact: "simon.zupan@studio-moderna.com"
            );
            return new NavClient(technicalUser, softwareIdentification, NavEnvironment.Test);
        }
    }
}
